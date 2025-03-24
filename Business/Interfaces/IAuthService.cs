namespace Business.Interfaces;

using Domain.Models;

public interface IAuthService
{
    Task<bool> LoginAsync(MemberLoginForm loginForm);
    Task<bool> SignUpAsync(MemberSignUpForm signUpForm);
    Task LogoutAsync();
}
