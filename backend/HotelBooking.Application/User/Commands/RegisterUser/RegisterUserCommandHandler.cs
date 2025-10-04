using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Application.Interfaces.Services;
using HotelBooking.Application.Dtos;

using DomainUser = HotelBooking.Domain.Entities.User;
using HotelBooking.Domain.Enums;

namespace HotelBooking.Application.User.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthorizeResponse>>
    {
        private readonly UserManager<DomainUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(UserManager<DomainUser> userManager,
            IJwtService jwtService,
            ILogger<RegisterUserCommandHandler> logger)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<Result<AuthorizeResponse>> Handle(RegisterUserCommand command, CancellationToken cancellationToken = default)
        {

            var userResult = DomainUser.Create(command.Request.Credential);
            if (userResult.IsFailure)
            {
                _logger.LogError("User creation failed: {Error}", userResult.Error);
                return Result<AuthorizeResponse>.Failure($"User creation failed: {userResult.Error}");
            }

            var user = userResult.Value;
            if (user == null)
            {
                _logger.LogError("User creation failed: User object is null.");
                return Result<AuthorizeResponse>.Failure("User creation failed: User object is null.");
            }
            if (user.Email != null && await _userManager.FindByEmailAsync(user.Email.ToString()) != null)
            {
                _logger.LogError("User registration failed: Email {Email} is already in use.", user.Email);
                return Result<AuthorizeResponse>.Failure($"Email {user.Email} is already in use.");
            }
            if (user.PhoneNumber != null && await _userManager.Users.AnyAsync(u => u.PhoneNumber == user.PhoneNumber.ToString(), cancellationToken))
            {
                _logger.LogError("User registration failed: Phone {Phone} is already in use.", user.PhoneNumber);
                return Result<AuthorizeResponse>.Failure($"Phone {user.PhoneNumber} is already in use.");
            }
            var result = await _userManager.CreateAsync(user, command.Request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("User registration failed: {Errors}", errors);
                return Result<AuthorizeResponse>.Failure($"User registration failed: {errors}");
            }
            await _userManager.AddToRoleAsync(user, UserRole.User.ToString());

            var roles = await _userManager.GetRolesAsync(user);

            _logger.LogInformation("User {UserId} registered successfully.", user.Id);
            
            var token = _jwtService.GenerateToken(user.Id, roles);

            return Result<AuthorizeResponse>.Success(new AuthorizeResponse(token));
        }
    }
}
