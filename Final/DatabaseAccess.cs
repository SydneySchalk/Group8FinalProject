using Final.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class DatabaseAccess
    {
        private SQLiteAsyncConnection _database;

        // Initialize database and create table
        public async Task Init()
        {
            if (_database != null)
                return;

            // Initialize SQLite connection
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            // Create table for the model
            var fish_result = await _database.CreateTableAsync<Fish>();
            var location_result = await _database.CreateTableAsync<Models.Location>();
            var fish_location_result = await _database.CreateTableAsync<FishLocation>();

        }

        // Add Fish
        public async Task<int> AddFishAsync(Fish fish)
        {
            await Init();
            return await _database.InsertAsync(fish);
        }

        // Add Location
        public async Task<int> AddLocationAsync(Models.Location location)
        {
            await Init();
            return await _database.InsertAsync(location);
        }

        // Add FishLocation (relation between Fish and Location)
        public async Task<int> AddFishLocationAsync(FishLocation fishLocation)
        {
            await Init();
            return await _database.InsertAsync(fishLocation); // Inserts FishLocation into the database
        }

        // Get all Fish
        public async Task<List<Fish>> GetFishAsync()
        {
            await Init();
            return await _database.Table<Fish>().ToListAsync(); // Returns list of all Fish
        }

        // Get all Locations
        public async Task<List<Models.Location>> GetLocationsAsync()
        {
            await Init();
            return await _database.Table<Models.Location>().ToListAsync(); // Returns list of all Locations
        }

        // Get all FishLocation (relationships between Fish and Location)
        public async Task<List<FishLocation>> GetFishLocationsAsync()
        {
            await Init();
            return await _database.Table<FishLocation>().ToListAsync(); // Returns list of all FishLocation relationships
        }

        // Associate Fish with a Location
        public async Task<int> AssociateFishWithLocationAsync(int fishId, int locationId)
        {
            var fishLocation = new FishLocation
            {
                FishID = fishId,
                LocationID = locationId
            };
            return await AddFishLocationAsync(fishLocation); // Inserts the relationship into the FishLocation table
        }

        public async Task<int> AddFishBatchAsync(List<Fish> fishList)
        {
            await Init();
            return await _database.InsertAllAsync(fishList); // Inserts all fish into the database
        }
        public async Task<int> AddLocationBatchAsync(List<Models.Location> locationList)
        {
            await Init();
            return await _database.InsertAllAsync(locationList); // Inserts all locations into the database
        }

    }
}


