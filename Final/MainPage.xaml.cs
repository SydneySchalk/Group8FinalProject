using Final.Models;
using System.Diagnostics;

namespace Final
{
    public partial class MainPage : ContentPage
    {
        private DatabaseAccess _database;

        public MainPage()
        {
            InitializeComponent();
            _database = new DatabaseAccess();
            LoadFish();
        }

        // Example: Add Fish
        async void OnAddFishClicked(object sender, EventArgs e)
        {
            async void OnAddFishClicked(object sender, EventArgs e)
            {
                try
                {
                    var fishList = new List<Fish> {
                        new Fish { FishID = 1, Name = "Pufferfish", Spring = false, Summer = true, Fall = false, Winter = false, Sun = true, Rain = false, Time = "12pm-4pm", Image = "Images/pufferfish.png", Obtained = false }
                    };

                    await _database.AddFishBatchAsync(fishList);

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        // UI confirmation of fish added
                    });
                }
                catch (InvalidOperationException ex)
                {
                    Debug.WriteLine($"AddFish Error: {ex.Message}");
                }
            }

        }

        // Add Location
        async void OnAddLocationClicked(object sender, EventArgs e)
        {
            var locationList = new List<Models.Location>
            {
                new Models.Location { LocationID = 1, LocationName = "Mountain Lake" },
                //...
            };
            await _database.AddLocationBatchAsync(locationList);
        }

        // Associate Fish with Location
        async void OnAssociateFishWithLocationClicked(object sender, EventArgs e)
        {
            // Assuming you have Fish ID 1 and Location ID 1
            await _database.AssociateFishWithLocationAsync(1, 1);
        }

        // Get FishLocations (Fish and their Locations)
        async void OnViewFishLocationsClicked(object sender, EventArgs e)
        {
            var fishLocations = await _database.GetFishLocationsAsync();

        }

        private async void LoadFish()
        {
            var fishWithLocations = await App.Database.GetFishWithLocationsAsync();

            var displayList = fishWithLocations.Select(fwl => new
            {
                Fish = fwl.Fish,
                LocationsDisplay = string.Join(", ", fwl.Locations)
            }).ToList();

            FishCollectionView.ItemsSource = displayList;
        }
    }

}
