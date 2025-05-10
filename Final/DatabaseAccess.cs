using Final.Models;
using Microsoft.Maui.Devices.Sensors;
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
                new Fish { FishID = 1, Name = "Pufferfish", Spring = false, Summer = true, Fall = false, Winter = false, Sun = true, Rain = false, Time = "12pm-4pm", Image = "Pufferfish.png", Obtained = false },
                new Fish { FishID = 2, Name = "Albacorre", Spring = false, Summer = false, Fall = true, Winter = true, Sun = false, Rain = false, Time = "6am-11am,6pm-2am", Image = "Albacore.png", Obtained = false },
                new Fish { FishID = 3, Name = "Anchovy", Spring = true, Summer = false, Fall = true, Winter = false, Sun = false, Rain = false, Time = "Anytime", Image = "Anchovy.png", Obtained = false },
                new Fish { FishID = 4, Name = "Angler", Spring = false, Summer = false, Fall = true, Winter = false, Sun = false, Rain = false, Time = "Anytime", Image = "Angler.png", Obtained = false },
                new Fish { FishID = 5, Name = "Blobfish", Spring = false, Summer = false, Fall = false, Winter = true, Sun = false, Rain = false, Time = "5pm-2am", Image = "Blobfish.png", Obtained = false },
                new Fish { FishID = 6, Name = "Bluediscus", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Bluediscus.png", Obtained = false },
                new Fish { FishID = 7, Name = "Bream", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "6pm-2am", Image = "Bream.png", Obtained = false },
                new Fish { FishID = 8, Name = "Bullhead", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Bullhead.png", Obtained = false },
                new Fish { FishID = 9, Name = "Carp", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Carp.png", Obtained = false },
                new Fish { FishID = 10, Name = "Catfish", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = true, Time = "Anytime", Image = "Bullhead.png", Obtained = false },
                new Fish { FishID = 11, Name = "Chub", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Chub.png", Obtained = false },
                new Fish { FishID = 12, Name = "Crimsonfish", Spring = false, Summer = true, Fall = false, Winter = false, Sun = false, Rain = false, Time = "Anytime", Image = "Crimsonfish.png", Obtained = false },
                new Fish { FishID = 13, Name = "Dorado", Spring = false, Summer = true, Fall = false, Winter = false, Sun = false, Rain = false, Time = "6am-7pm", Image = "Dorado.png", Obtained = false },
                new Fish { FishID = 14, Name = "Eel", Spring = true, Summer = false, Fall = true, Winter = false, Sun = false, Rain = true, Time = "4pm-2am", Image = "Eel.png", Obtained = false },
                new Fish { FishID = 15, Name = "Flounder", Spring = true, Summer = true, Fall = false, Winter = false, Sun = false, Rain = false, Time = "6am-8pm", Image = "Flounder.png", Obtained = false },
                new Fish { FishID = 16, Name = "Ghostfish", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Ghostfish.png", Obtained = false },
                new Fish { FishID = 17, Name = "Glacierfish", Spring = false, Summer = false, Fall = false, Winter = true, Sun = false, Rain = false, Time = "6am-11pm", Image = "Glacierfish.png", Obtained = false },
                new Fish { FishID = 18, Name = "Gobi", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Gobi.png", Obtained = false },
                new Fish { FishID = 19, Name = "Halibut", Spring = true, Summer = true, Fall = false, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Halibut.png", Obtained = false },
                new Fish { FishID = 20, Name = "Herring", Spring = true, Summer = false, Fall = false, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Herring.png", Obtained = false },
                new Fish { FishID = 21, Name = "Ice Pip", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Ice_Pip.png", Obtained = false },
                new Fish { FishID = 22, Name = "Largemouth Bass", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "6am-7pm", Image = "Largemouth_Bass.png", Obtained = false },
                new Fish { FishID = 23, Name = "Lava Eel", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Lava_Eel.png", Obtained = false },
                new Fish { FishID = 24, Name = "Legend", Spring = true, Summer = false, Fall = false, Winter = false, Sun = false, Rain = true, Time = "6am-11pm", Image = "Legend.png", Obtained = false },
                new Fish { FishID = 25, Name = "Lingcod", Spring = false, Summer = false, Fall = false, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Lingcod.png", Obtained = false },
                new Fish { FishID = 26, Name = "Lionfish", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Lionfish.png", Obtained = false },
                new Fish { FishID = 27, Name = "Midnight Carp", Spring = false, Summer = false, Fall = true, Winter = true, Sun = false, Rain = false, Time = "10pm-2am", Image = "Midnight_Carp.png", Obtained = false },
                new Fish { FishID = 28, Name = "Midnight Squid", Spring = false, Summer = false, Fall = false, Winter = true, Sun = false, Rain = false, Time = "5pm-2am", Image = "Midnight_Squid.png", Obtained = false },
                new Fish { FishID = 29, Name = "Mutant Carp", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Mutant_Carp.png", Obtained = false },
                new Fish { FishID = 30, Name = "Octopus", Spring = false, Summer = true, Fall = false, Winter = false, Sun = false, Rain = false, Time = "6am-1pm", Image = "Octopus.png", Obtained = false },
                new Fish { FishID = 31, Name = "Perch", Spring = false, Summer = false, Fall = false, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Perch.png", Obtained = false },
                new Fish { FishID = 32, Name = "Pike", Spring = false, Summer = true, Fall = false, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Pike.png", Obtained = false },
                new Fish { FishID = 33, Name = "Rainbow Trout", Spring = false, Summer = true, Fall = false, Winter = false, Sun = true, Rain = false, Time = "6am-7pm", Image = "Rainbow_Trout.png", Obtained = false },
                new Fish { FishID = 34, Name = "Red Mullet", Spring = false, Summer = true, Fall = false, Winter = true, Sun = false, Rain = false, Time = "6am-7pm", Image = "Red_Mullet.png", Obtained = false },
                new Fish { FishID = 35, Name = "Red Snapper", Spring = false, Summer = true, Fall = true, Winter = false, Sun = false, Rain = true, Time = "6am-7pm", Image = "RRed_Snapper.png", Obtained = false },
                new Fish { FishID = 36, Name = "Salmon", Spring = false, Summer = false, Fall = true, Winter = true, Sun = false, Rain = false, Time = "6am-7pm", Image = "Salmon.png", Obtained = false },
                new Fish { FishID = 37, Name = "Sandfish", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "6am-8pm", Image = "Sandfish.png", Obtained = false },
                new Fish { FishID = 38, Name = "Sardine", Spring = true, Summer = false, Fall = true, Winter = true, Sun = false, Rain = false, Time = "6am-7pm", Image = "Sardine.png", Obtained = false },
                new Fish { FishID = 39, Name = "Scoorpion Carp", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "6am-8pm", Image = "Scorpion_Carp.png", Obtained = false },
                new Fish { FishID = 40, Name = "Sea Cucumber", Spring = false, Summer = false, Fall = true, Winter = true, Sun = false, Rain = false, Time = "6am-7pm", Image = "Sea_Cucumber.png", Obtained = false },
                new Fish { FishID = 41, Name = "Shad", Spring = true, Summer = true, Fall = true, Winter = false, Sun = false, Rain = true, Time = "9am-2am", Image = "Shad.png", Obtained = false },
                new Fish { FishID = 42, Name = "Slimejack", Spring = true, Summer = true, Fall = true, Winter =false, Sun = false, Rain = false, Time = "Anytime", Image = "Slimejack.png", Obtained = false },
                new Fish { FishID = 43, Name = "Smallmouth Bass", Spring = true, Summer = false, Fall = true, Winter = false, Sun = false, Rain = false, Time = "Anytime", Image = "Smallmouth_Bass.png", Obtained = false },
                new Fish { FishID = 44, Name = "Spook Fish", Spring = false, Summer = false, Fall = false, Winter = true, Sun = false, Rain = false, Time = "5pm-2am", Image = "Spook_Fish.png", Obtained = false },
                new Fish { FishID = 45, Name = "Squid", Spring = false, Summer = false, Fall = false, Winter = true, Sun = false, Rain = false, Time = "6pm-2am", Image = "Squid.png", Obtained = false },
                new Fish { FishID = 46, Name = "Stingray", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Stingray.png", Obtained = false },
                new Fish { FishID = 47, Name = "Stonefish", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Stonefish.png", Obtained = false },
                new Fish { FishID = 48, Name = "Sturgeon", Spring = false, Summer = true, Fall = false, Winter = true, Sun = false, Rain = false, Time = "6am-7pm", Image = "Sturgeon.png", Obtained = false },
                new Fish { FishID = 49, Name = "Sunfish", Spring = true, Summer = true, Fall = false, Winter = false, Sun =true, Rain = false, Time = "6am-7pm", Image = "Sunfish.png", Obtained = false },
                new Fish { FishID = 50, Name = "Super Cucumber", Spring = false, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "6pm-2am", Image = "Super_Cucumberr.png", Obtained = false },
                new Fish { FishID = 51, Name = "Tiger Trout", Spring = false, Summer = false, Fall = true, Winter = true, Sun = false, Rain = false, Time = "6am-7pm", Image = "Tiger_Trout.png", Obtained = false },
                new Fish { FishID = 52, Name = "Tilapia", Spring = false, Summer = true, Fall = true, Winter = false, Sun = false, Rain = false, Time = "6am-2pm", Image = "Tilapia.png", Obtained = false },
                new Fish { FishID = 53, Name = "Tuna", Spring = false, Summer = true, Fall = false, Winter = true, Sun = false, Rain = false, Time = "6am-2pm", Image = "Tuna.png", Obtained = false },
                new Fish { FishID = 54, Name = "Void Salmon", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Void_Salmon.png", Obtained = false },
                new Fish { FishID = 55, Name = "Walleye", Spring = false, Summer = false, Fall = true, Winter = false, Sun = false, Rain = true, Time = "12pm-2am", Image = "Walleye.png", Obtained = false },
                new Fish { FishID = 56, Name = "Woodskip", Spring = true, Summer = true, Fall = true, Winter = true, Sun = false, Rain = false, Time = "Anytime", Image = "Woodskip.png", Obtained = false }
            };

            await AddFishBatchAsync(fishList);



            // Adding locations
            var locationList = new List<Location>
{
                new Location { LocationID = 1,LocationName = "River" },
                new Location { LocationID = 2, LocationName = "Lake" },
                new Location { LocationID = 3, LocationName = "Ocean" },
                new Location { LocationID = 4, LocationName = "NightMarket" },
                new Location { LocationID = 5, LocationName = "Mines" },
                new Location { LocationID = 6, LocationName = "Waterfalls" },
                new Location { LocationID = 7, LocationName = "Sewers" },
                new Location { LocationID = 8, LocationName = "Desert" },
                new Location { LocationID = 9, LocationName = "Mutant Bug Lair" },
                new Location { LocationID = 10, LocationName = "Ginger Island" },
                new Location { LocationID = 11, LocationName = "Witch's Swamp" },
                new Location { LocationID = 12, LocationName = "Secret Woods" }
            };

            await AssociateFishWithLocationAsync(1, 3);
            await AssociateFishWithLocationAsync(2, 3);
            await AssociateFishWithLocationAsync(3, 1);
            await AssociateFishWithLocationAsync(5, 4); //(Review wit group)
            await AssociateFishWithLocationAsync(6, 1);
            await AssociateFishWithLocationAsync(7, 1);
            await AssociateFishWithLocationAsync(8, 2);
            await AssociateFishWithLocationAsync(9, 2);
            await AssociateFishWithLocationAsync(10, 1);
            await AssociateFishWithLocationAsync(11, 1);
            await AssociateFishWithLocationAsync(12, 3);
            await AssociateFishWithLocationAsync(13, 1);
            await AssociateFishWithLocationAsync(14, 3);
            await AssociateFishWithLocationAsync(15, 3);
            await AssociateFishWithLocationAsync(16, 5);
            await AssociateFishWithLocationAsync(17, 1);
            await AssociateFishWithLocationAsync(18, 6);
            await AssociateFishWithLocationAsync(19, 3);
            await AssociateFishWithLocationAsync(20, 3);
            await AssociateFishWithLocationAsync(21, 5);
            await AssociateFishWithLocationAsync(22, 2);
            await AssociateFishWithLocationAsync(23, 5);
            await AssociateFishWithLocationAsync(24, 2);
            await AssociateFishWithLocationAsync(25, 1);
            await AssociateFishWithLocationAsync(26, 3);
            await AssociateFishWithLocationAsync(27, 2);
            await AssociateFishWithLocationAsync(28, 4);
            await AssociateFishWithLocationAsync(29, 7);
            await AssociateFishWithLocationAsync(30, 3);
            await AssociateFishWithLocationAsync(31, 1);
            await AssociateFishWithLocationAsync(32, 1);
            await AssociateFishWithLocationAsync(33, 1);
            await AssociateFishWithLocationAsync(34, 3);
            await AssociateFishWithLocationAsync(35, 3);
            await AssociateFishWithLocationAsync(36, 1);
            await AssociateFishWithLocationAsync(37, 8);
            await AssociateFishWithLocationAsync(38, 3);
            await AssociateFishWithLocationAsync(39, 8);
            await AssociateFishWithLocationAsync(40, 3);
            await AssociateFishWithLocationAsync(41, 1);
            await AssociateFishWithLocationAsync(42, 9);
            await AssociateFishWithLocationAsync(43, 2);
            await AssociateFishWithLocationAsync(44, 4);
            await AssociateFishWithLocationAsync(45, 3);
            await AssociateFishWithLocationAsync(46, 10);
            await AssociateFishWithLocationAsync(47, 5);
            await AssociateFishWithLocationAsync(48, 2);
            await AssociateFishWithLocationAsync(49, 1);
            await AssociateFishWithLocationAsync(50, 3);
            await AssociateFishWithLocationAsync(51, 1);
            await AssociateFishWithLocationAsync(52, 3);
            await AssociateFishWithLocationAsync(53, 3);
            await AssociateFishWithLocationAsync(54, 11);
            await AssociateFishWithLocationAsync(55, 1);
            await AssociateFishWithLocationAsync(56, 12);
        }
    }
}


