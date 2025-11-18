using MappyApplication.Contract;
using MappyApplication.Data;
using MappyApplication.Exceptions;
using Microsoft.EntityFrameworkCore;
using Token = MappyApplication.Data.Token;

namespace MappyApplication.Repository;

public class TokenRepository : GenricRepository<Token>, ITokenRepository
{
    private readonly MappyDBContext _dbContext;
    private readonly ILogger<TokenRepository> _logger;


    public TokenRepository(MappyDBContext dbContext, ILogger<TokenRepository> logger) : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task UpdateAllValidTokenByUserIdAndDeviceId(string userId, string deviceId)
    {
        await _dbContext.Database.ExecuteSqlAsync($@"
        UPDATE Tokens
        SET Revoked = true, Expired = true
        WHERE UserId = {userId}
          AND DeviceId = {deviceId}
          AND Revoked = false
          AND Expired = false
    ");
    }

    public Token FindByRefreshToken(string refreshToken)
    {
        var token = _dbContext.Tokens.SingleOrDefault(r => r.RefreshToken == refreshToken);
        if (token == null) throw new AppErrorResponse("Invalid token");
        return token;
    }

    public Token FindByAccessToken(string accessToken)
    {
        var token = _dbContext.Tokens.SingleOrDefault(t => t.AccessToken == accessToken);
        if (token == null) throw new AppErrorResponse("Invalid token");
        return token;
    }
}