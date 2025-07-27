using Verbum.API.DTOs.Comment;
using Verbum.API.DTOs.Community;
using Verbum.API.DTOs.Post;
using Verbum.API.DTOs.User;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Models;

namespace Verbum.API.Services;

public class AuthService : IAuthService {
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<bool> Register(CreateUserDto user) {
        var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);

        // User exists
        if (existingUser != null) {
            return false;
        }

        var newUser = new User {
            Username = user.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
        };

        await _userRepository.AddAsync(newUser);
        return true;
    }

    public async Task<UserCompleteDto?> Login(UserLoginDto user) {
        var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
        if (existingUser == null) {
            return null;
        }

        // Check if hashed password matches password stored in db
        bool isMatch = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);

        return isMatch
            ? new UserCompleteDto {
                Id = existingUser.Id, Username = existingUser.Username,
                Posts = existingUser.Posts.Select(p => new PostSimpleDto {
                        Id = p.Id,
                        User = new UserSimpleDto { Id = p.User.Id, Username = p.User.Username },
                        CommentsCount = existingUser.Comments.Count,
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
                Comments = existingUser.Comments.Select(c => new CommentDto {
                    Id = c.Id,
                    Author = new UserSimpleDto { Id = c.User.Id, Username = c.User.Username },
                    CreatedAt = c.CreatedAt,
                    Text = c.Text,
                    UpdatedAt = c.UpdatedAt,
                    Votes = c.Votes.Aggregate(0, (acc, curr) => acc + curr.Value)
                }).ToList(),
                Communities = existingUser.CommunitiesJoined.Select(c => new CommunitySimpleDto {
                    Id = c.Community.Id,
                    UserId = c.UserId,
                    MembersCount = c.Community.Members.Count,
                    Description = c.Community.Description,
                    Name = c.Community.Name
                }).ToList()
            }
            : null;
    }
}