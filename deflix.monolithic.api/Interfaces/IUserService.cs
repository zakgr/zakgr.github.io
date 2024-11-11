using deflix.monolithic.api.DTOs;
using deflix.monolithic.api.Types;

namespace deflix.monolithic.api.Interfaces
{
    public interface IUserService
    {
        bool Register(UserRegisterDto userDto);
        User? Authenticate(string username, string password);
        UserDto? GetUserProfile(int userId);
        bool UpdateUserProfile(int userId, UserProfileUpdateDto profileUpdateDto);
        string CreateSessionToken(int userId);
        User? GetUserBySessionToken(string encodedToken);
    }

}
