using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.Post;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Results;

namespace Verbum.API.Services;

public class UserService : IUserService {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<ServiceResult<UserSimpleDto>> GetUser(string username) {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null) {
            return ServiceResult<UserSimpleDto>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        return ServiceResult<UserSimpleDto>.Success(new UserSimpleDto { Id = user.Id, Username = user.Username });
        ;
    }

    public async Task<ServiceResult<UserCompleteDto>> GetUserCompleteById(int id) {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null) {
            return ServiceResult<UserCompleteDto>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        return ServiceResult<UserCompleteDto>.Success(new UserCompleteDto {
                Id = user.Id, Username = user.Username,
                Posts = user.Posts.Select(p => new PostSimpleDto {
                        Id = p.Id,
                        User = new UserSimpleDto { Id = p.User.Id, Username = p.User.Username },
                        CommentsCount = user.Comments.Count,
                        Created = p.CreatedAt,
                        CommunityId = p.CommunityId,
                        Votes = p.Votes.Aggregate(0, (acc, curr) => acc + curr.Value),
                        Title = p.Title
                    })
                    .ToList(),
                Comments = user.Comments.Select(c => new CommentDto {
                    Id = c.Id,
                    Author = new UserSimpleDto { Id = c.User.Id, Username = c.User.Username },
                    CreatedAt = c.CreatedAt,
                    Text = c.Text,
                    UpdatedAt = c.UpdatedAt,
                    Votes = c.Votes.Aggregate(0, (acc, curr) => acc + curr.Value)
                }).ToList(),
                Communities = user.CommunitiesJoined.Select(c => new CommunitySimpleDto {
                    Id = c.Community.Id,
                    UserId = c.UserId,
                    MembersCount = c.Community.Members.Count,
                    Description = c.Community.Description,
                    Name = c.Community.Name,
                    isMember = c.Community.Members.Any(m => m.UserId == id),
                    isOwner = c.Community.UserId == id
                }).ToList()
            }
        );
    }


    public async Task<ServiceResult<UserSimpleDto>> UpdateUser(UpdateUserDto dto) {
        var user = await _userRepository.GetUserByIdAsync(dto.Id);
        if (user == null) {
            return ServiceResult<UserSimpleDto>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        var userWithSameName = await _userRepository.GetUserByUsernameAsync(dto.Username);

        if (userWithSameName != null && userWithSameName.Id != user.Id) {
            return ServiceResult<UserSimpleDto>.Error(ServiceResultStatus.Conflict, "Username already exists!");
        }

        user.Username = dto.Username;
        user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        await _userRepository.UpdateAsync(user);

        return ServiceResult<UserSimpleDto>.Success(new UserSimpleDto { Id = user.Id, Username = user.Username });
    }

    public async Task<ServiceResult<bool>> DeleteUser(int id) {
        if (id == null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.Unauthorized, "Unauthorized!");
        }

        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null) {
            return ServiceResult<bool>.Error(ServiceResultStatus.NotFound, "User not found!");
        }

        await _userRepository.DeleteAsync(id);
        return ServiceResult<bool>.Success(true);
    }
}