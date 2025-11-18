using MappyApplication.Models.user;

namespace MappyApplication.Contract;

public interface IAuthManager
{
    Task<AuthResponse> Register(AuthRegister authRegister);
    Task<AuthResponse> Login(AuthLogin authLogin);
    Task<AuthResponse> Refresh();
    Task Logout();
    Task<UserDto> GetUser();
}