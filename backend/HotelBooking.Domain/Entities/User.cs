using HotelBooking.Domain.Enums;
using HotelBooking.Domain.Model;
using HotelBooking.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Common;

namespace HotelBooking.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        private User() { }

        private User(string? email, string? phone)
        {
            if (email != null)
            {
                Email = email;
                UserName = email;
                EmailConfirmed = false;
            }
            if (phone != null)
            {
                PhoneNumber = phone;
                UserName = phone;
                PhoneNumberConfirmed = false;
            }
        }


        public static Result<User> Create(string credential)
        {
            var parsed = ParseCredential(credential);

            if (parsed.IsFailure)
                return Result<User>.Failure(parsed.Error);

            var user = parsed.Value.Type == CredentialType.Email
                ? new User(parsed.Value.Value, null)
                : new User(null, parsed.Value.Value);

            return Result<User>.Success(user);
        }

        public static Result<CredentialInfo> ParseCredential(string credential)
        {
            if (string.IsNullOrWhiteSpace(credential))
                return Result<CredentialInfo>.Failure("Credential cannot be empty");

            if (credential.Contains("@"))
            {
                try
                {
                    var email = new Email(credential);
                    return Result<CredentialInfo>.Success(new CredentialInfo(CredentialType.Email, email.Value));
                }
                catch (Exception ex)
                {
                    return Result<CredentialInfo>.Failure($"Invalid email format: {ex.Message}");
                }
            }

            if (char.IsDigit(credential[0]) || credential.StartsWith("+"))
            {
                try
                {
                    var phone = new PhoneNumber(credential);
                    return Result<CredentialInfo>.Success(new CredentialInfo (CredentialType.Phone, phone.Value));
                }
                catch (Exception ex)
                {
                    return Result<CredentialInfo>.Failure($"Invalid phone format: {ex.Message}");
                }
            }

            return Result<CredentialInfo>.Failure("Credential is neither a valid email nor a valid phone number");
        }
    }
}
