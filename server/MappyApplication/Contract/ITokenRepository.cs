using MappyApplication.Data;

namespace MappyApplication.Contract;

public interface ITokenRepository : IGenricRepository<Token>
{
    Task UpdateAllValidTokenByUserIdAndDeviceId(string userId, string deviceId);
    Token FindByRefreshToken(string refreshToken);
    Token FindByAccessToken(string accessToken);
}