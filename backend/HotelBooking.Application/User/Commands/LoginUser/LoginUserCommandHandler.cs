using HotelBooking.Application.Dtos;
using HotelBooking.Application.Interfaces.Services;
using HotelBooking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;

using DomainUser = HotelBooking.Domain.Entities.User;

namespace HotelBooking.Application.User.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthorizeResponse>>
    {
        private readonly UserManager<DomainUser> _userManager;
        private readonly SignInManager<DomainUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<LoginUserCommandHandler> _logger;
        public LoginUserCommandHandler(UserManager<DomainUser> userManager,
            SignInManager<DomainUser> signInManager,
            IJwtService jwtService,
            ILogger<LoginUserCommandHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _logger = logger;
        }
        public async Task<Result<AuthorizeResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var parsed = DomainUser.ParseCredential(request.Request.Credential);
            if (parsed.IsFailure)
                return Result<AuthorizeResponse>.Failure(parsed.Error);

            var user = parsed.Value.Type == CredentialType.Email
                ? await _userManager.FindByEmailAsync(parsed.Value.Value)
                : await _userManager.FindByNameAsync(parsed.Value.Value);

            if (user == null)
                return Result<AuthorizeResponse>.Failure("User not found");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Request.Password, false);
            if (!signInResult.Succeeded)
            {
                _logger.LogWarning("Invalid login attempt for user {UserId}", user.Id);
                return Result<AuthorizeResponse>.Failure("Invalid credentials");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(user.Id, roles);

            var response = new AuthorizeResponse(token);
            return Result<AuthorizeResponse>.Success(response);
        }
    }
}
