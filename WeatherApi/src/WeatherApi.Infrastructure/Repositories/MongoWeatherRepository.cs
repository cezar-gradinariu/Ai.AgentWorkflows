using MongoDB.Driver;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Repositories;

namespace WeatherApi.Infrastructure.Repositories;

/// <summary>
/// MongoDB implementation of the weather repository.
/// </summary>
public class MongoWeatherRepository : IWeatherRepository
{
    private readonly IMongoCollection<WeatherDataPoint> _collection;

    public MongoWeatherRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<WeatherDataPoint>("weatherdatapoints");
        
        // Create compound index on city and timestamp for efficient queries
        var indexKeys = Builders<WeatherDataPoint>.IndexKeys
            .Ascending(x => x.City)
            .Ascending(x => x.Timestamp);
        
        var indexModel = new CreateIndexModel<WeatherDataPoint>(indexKeys);
        _collection.Indexes.CreateOneAsync(indexModel);
    }

    public async Task<List<WeatherDataPoint>> GetWeatherAsync(string city, DateTime from, DateTime to)
    {
        var filter = Builders<WeatherDataPoint>.Filter.Eq(x => x.City, city) &
                     Builders<WeatherDataPoint>.Filter.Gte(x => x.Timestamp, from) &
                     Builders<WeatherDataPoint>.Filter.Lte(x => x.Timestamp, to);

        return await _collection.Find(filter)
            .SortBy(x => x.Timestamp)
            .ToListAsync();
    }

    public async Task UpsertWeatherDataPointsAsync(IEnumerable<WeatherDataPoint> dataPoints)
    {
        var tasks = dataPoints.Select(dp => UpsertWeatherDataPointAsync(dp));
        await Task.WhenAll(tasks);
    }

    public async Task UpsertWeatherDataPointAsync(WeatherDataPoint dataPoint)
    {
        // Find existing data for the same city and timestamp
        var filter = Builders<WeatherDataPoint>.Filter.Eq(x => x.City, dataPoint.City) &
                     Builders<WeatherDataPoint>.Filter.Eq(x => x.Timestamp, dataPoint.Timestamp);

        var existing = await _collection.Find(filter).FirstOrDefaultAsync();

        // Only update if no existing data or if new data is newer
        if (existing == null)
        {
            // Insert new data
            await _collection.InsertOneAsync(dataPoint);
        }
        else if (dataPoint.LastUpdated > existing.LastUpdated)
        {
            // Update with newer data
            dataPoint.Id = existing.Id; // Keep the same ID
            await _collection.ReplaceOneAsync(filter, dataPoint);
        }
        // If dataPoint.LastUpdated <= existing.LastUpdated, ignore (data is older or same age)
    }
}

