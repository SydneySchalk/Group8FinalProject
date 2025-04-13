using Final.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Location = Final.Models.Location;

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


        //<-----Adding Stuff----->//


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
            return await _database.InsertAsync(fishLocation);
        }

        
        //<-----Getting Stuff----->//


        // Get all Fish
        public async Task<List<Fish>> GetFishAsync()
        {
            await Init();
            return await _database.Table<Fish>().ToListAsync();
        }

        // Get all Locations
        public async Task<List<Models.Location>> GetLocationsAsync()
        {
            await Init();
            return await _database.Table<Models.Location>().ToListAsync();
        }

        // Get all FishLocation (relationships between Fish and Location)
        public async Task<List<FishLocation>> GetFishLocationsAsync()
        {
            await Init();
            return await _database.Table<FishLocation>().ToListAsync();
        }

        public async Task<List<FishWithLocations>> GetFishWithLocationsAsync()
        {
            var fishList = await GetFishAsync();
            var locations = await GetLocationsAsync();
            var fishLocations = await GetFishLocationsAsync();

            var result = fishList.Select(fish => new FishWithLocations
            {
                Fish = fish,
                Locations = fishLocations
                    .Where(fl => fl.FishID == fish.FishID)
                    .Select(fl => locations.FirstOrDefault(loc => loc.LocationID == fl.LocationID))
                    .Where(loc => loc != null)
                    .ToList()
            }).ToList();

            return result;
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

        

        public class FishWithLocations
        {
            public Fish Fish { get; set; }
            public List<Location> Locations { get; set; }
            public string LocationsDisplay => string.Join(", ", Locations.Select(l => l.LocationName));
        }

        public async Task AddSampleData()
        {
            // Adding fish
            var fishList = new List<Fish>
            {
                new Fish { Name = "Pufferfish", Spring = false, Summer = true, Fall = false, Winter = false, Sun = true, Rain = false, Time = "12pm-4pm", Image = "pufferfish.png", Obtained = false },
                new Fish { Name = "Pufferfish", Spring = false, Summer = true, Fall = false, Winter = false, Sun = true, Rain = false, Time = "12pm-4pm", Image = "pufferfish.png", Obtained = false },
                new Fish { Name = "Pufferfish", Spring = false, Summer = true, Fall = false, Winter = false, Sun = true, Rain = false, Time = "12pm-4pm", Image = "pufferfish.png", Obtained = false }
            };

            await AddFishBatchAsync(fishList);

            // Adding locations
            var locationList = new List<Location>
            {
                new Location { LocationName = "River" },
                new Location { LocationName = "Lake" },
                new Location { LocationName = "Ocean" }
            };

            await AddLocationBatchAsync(locationList);

            // Associating fish with locations
            await AssociateFishWithLocationAsync(1, 1);
            await AssociateFishWithLocationAsync(2, 2);
            await AssociateFishWithLocationAsync(3, 3);
        }
    }
}


