using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.Post;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;

namespace Verbum.API.Services;

public class UserService : IUserService {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<UserSimpleDto?> GetUser(string username) {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        return user == null ? null : new UserSimpleDto { Id = user.Id, Username = user.Username };
    }

    public async Task<UserCompleteDto?> GetUserCompleteById(int id) {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null) {
            return null;
        }

        return new UserCompleteDto {
            Id = user.Id, Username = user.Username,
            Posts = user.Posts.Select(p => new PostSimpleDto {
                    Id = p.Id,
                    User = new UserSimpleDto { Id = p.User.Id, Username = p.User.Username },
                    CommentsCount = user.Comments.Count,
                    Created = p.CreatedAt,
                    Community = new CommunitySimpleDto {
                        Id = p.Community.Id,
                        Name = p.Community.Name,
                        Description = p.Community.Description,
                        MembersCount = p.Community.Members.Count,
                        UserId = p.Community.UserId
                    }
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
                Name = c.Community.Name
            }).ToList()
        };
    }


    public async Task<UserSimpleDto> UpdateUser(UpdateUserDto dto) {
        var user = await _userRepository.GetUserByIdAsync(dto.Id);
        if (user == null) {
            return null;
        }

        user.Username = dto.Username;
        user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var updatedUser = await _userRepository.UpdateAsync(user);
        return updatedUser == null ? null : new UserSimpleDto { Id = updatedUser.Id, Username = updatedUser.Username };
    }

    public async Task<bool> DeleteUser(int id) {
        if (id == null) {
            return false;
        }

        bool result = await _userRepository.DeleteAsync(id);
        return result;
    }
}