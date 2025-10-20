using MongoDB.Driver;
using WeatherApi.Application.Services.Implementations;
using WeatherApi.Domain.Entities;
using WeatherApi.Infrastructure.Persistence;

namespace WeatherApi.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly MongoDbContext _context;

    public RefreshTokenRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens.Find(t => t.Token == token).FirstOrDefaultAsync();
    }

    public async Task<List<RefreshToken>> GetByUserIdAsync(string userId)
    {
        return await _context.RefreshTokens.Find(t => t.UserId == userId).ToListAsync();
    }

    public async Task CreateAsync(RefreshToken token)
    {
        await _context.RefreshTokens.InsertOneAsync(token);
    }

    public async Task UpdateAsync(RefreshToken token)
    {
        await _context.RefreshTokens.ReplaceOneAsync(t => t.Id == token.Id, token);
    }
}

