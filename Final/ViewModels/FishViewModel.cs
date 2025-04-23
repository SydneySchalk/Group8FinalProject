using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Final.Models;

namespace Final.ViewModels
{

    public class FishViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Grouping<string, Fish>> FishGrouped { get; }
        private ObservableCollection<Grouping<string, Fish>> _filteredFishGrouped;
        public ObservableCollection<Grouping<string, Fish>> FilteredFishGrouped
        {
            get => _filteredFishGrouped;
            set
            {
                _filteredFishGrouped = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterFish();
            }
        }

        public FishViewModel()
        {
            var fishList = new List<Fish>
       {
           new Fish { Name = "Pufferfish", Season = "Summer", Weather = "Sun", Time = "12pm-4pm", Image = "Pufferfish.png", Obtained = false },
           new Fish { Name = "Albacorre", Season = "Fall,Winter", Weather = "None", Time = "6am-11am,6pm-2am", Image = "Albacore.png", Obtained = false },
           new Fish { Name = "Anchovy", Season = "Spring,Fall", Weather = "None", Time = "Anytime", Image = "Anchovy.png", Obtained = false },
           new Fish { Name = "Angler", Season = "Fall", Weather = "None", Time = "Anytime", Image = "Angler.png", Obtained = false },
           new Fish { Name = "Blobfish", Season = "Winter", Weather = "None", Time = "5pm-2am", Image = "Blobfish.png", Obtained = false },
           new Fish { Name = "Bluediscus", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Bluediscus.png", Obtained = false },
           new Fish { Name = "Bream", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "6pm-2am", Image = "Bream.png", Obtained = false },
           new Fish { Name = "Bullhead", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Bullhead.png", Obtained = false },
           new Fish { Name = "Carp", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Carp.png", Obtained = false },
           new Fish { Name = "Catfish", Season = "Spring,Summer,Fall,Winter", Weather = "Rain", Time = "Anytime", Image = "Bullhead.png", Obtained = false },
           new Fish { Name = "Chub", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Chub.png", Obtained = false },
           new Fish { Name = "Crimsonfish", Season = "Summer", Weather = "None", Time = "Anytime", Image = "Crimsonfish.png", Obtained = false },
           new Fish { Name = "Dorado", Season = "Summer", Weather = "None", Time = "6am-7pm", Image = "Dorado.png", Obtained = false },
           new Fish { Name = "Eel", Season = "Spring,Fall", Weather = "Rain", Time = "4pm-2am", Image = "Eel.png", Obtained = false },
           new Fish { Name = "Flounder", Season = "Spring,Summer", Weather = "None", Time = "6am-8pm", Image = "Flounder.png", Obtained = false },
           new Fish { Name = "Ghostfish", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Ghostfish.png", Obtained = false },
           new Fish { Name = "Glacierfish", Season = "Winter", Weather = "None", Time = "6am-11pm", Image = "Glacierfish.png", Obtained = false },
           new Fish { Name = "Gobi", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Gobi.png", Obtained = false },
           new Fish { Name = "Halibut", Season = "Spring,Summer,Winter", Weather = "None", Time = "Anytime", Image = "Halibut.png", Obtained = false },
           new Fish { Name = "Herring", Season = "Spring,Winter", Weather = "None", Time = "Anytime", Image = "Herring.png", Obtained = false },
            new Fish { Name = "Ice Pip", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Ice_Pip.png", Obtained = false },
            new Fish { Name = "Largemouth Bass", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "6am-7pm", Image = "Largemouth_Bass.png", Obtained = false },
            new Fish { Name = "Lava Eel", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Lava_Eel.png", Obtained = false },
            new Fish { Name = "Legend", Season = "Spring", Weather = "Rain", Time = "6am-11pm", Image = "Legend.png", Obtained = false },
            new Fish { Name = "Lingcod", Season = "Winter", Weather = "None", Time = "Anytime", Image = "Lingcod.png", Obtained = false },
            new Fish { Name = "Lionfish", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Lionfish.png", Obtained = false },
            new Fish { Name = "Midnight Carp", Season = "Fall,Winter", Weather = "None", Time = "10pm-2am", Image = "Midnight_Carp.png", Obtained = false },
            new Fish { Name = "Midnight Squid", Season = "Winter", Weather = "None", Time = "5pm-2am", Image = "Midnight_Squid.png", Obtained = false },
            new Fish { Name = "Mutant Carp", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Mutant_Carp.png", Obtained = false },
            new Fish { Name = "Octopus", Season = "Summer", Weather = "None", Time = "6am-1pm", Image = "Octopus.png", Obtained = false },
            new Fish { Name = "Perch", Season = "Winter", Weather = "None", Time = "Anytime", Image = "Perch.png", Obtained = false },
            new Fish { Name = "Pike", Season = "Summer,Winter", Weather = "None", Time = "Anytime", Image = "Pike.png", Obtained = false },
            new Fish { Name = "Rainbow Trout", Season = "Summer", Weather = "Sun", Time = "6am-7pm", Image = "Rainbow_Trout.png", Obtained = false },
            new Fish { Name = "Red Mullet", Season = "Summer,Winter", Weather = "None", Time = "6am-7pm", Image = "Red_Mullet.png", Obtained = false },
            new Fish { Name = "Red Snapper", Season = "Summer,Fall", Weather = "Rain", Time = "6am-7pm", Image = "Red_Snapper.png", Obtained = false },
            new Fish { Name = "Salmon", Season = "Fall,Winter", Weather = "None", Time = "6am-7pm", Image = "Salmon.png", Obtained = false },
            new Fish { Name = "Sandfish", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "6am-8pm", Image = "Sandfish.png", Obtained = false },
            new Fish { Name = "Sardine", Season = "Spring,Fall,Winter", Weather = "None", Time = "6am-7pm", Image = "Sardine.png", Obtained = false },
            new Fish { Name = "Scorpion Carp", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "6am-8pm", Image = "Scorpion_Carp.png", Obtained = false },
            new Fish { Name = "Sea Cucumber", Season = "Fall,Winter", Weather = "None", Time = "6am-7pm", Image = "Sea_Cucumber.png", Obtained = false },
            new Fish { Name = "Shad", Season = "Spring,Summer,Fall", Weather = "Rain", Time = "9am-2am", Image = "Shad.png", Obtained = false },
            new Fish { Name = "Slimejack", Season = "Spring,Summer,Fall", Weather = "None", Time = "Anytime", Image = "Slimejack.png", Obtained = false },
            new Fish { Name = "Smallmouth Bass", Season = "Spring,Fall", Weather = "None", Time = "Anytime", Image = "Smallmouth_Bass.png", Obtained = false },
            new Fish { Name = "Spook Fish", Season = "Winter", Weather = "None", Time = "5pm-2am", Image = "Spook_Fish.png", Obtained = false },
            new Fish { Name = "Squid", Season = "Winter", Weather = "None", Time = "6pm-2am", Image = "Squid.png", Obtained = false },
            new Fish { Name = "Stingray", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Stingray.png", Obtained = false },
            new Fish { Name = "Stonefish", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Stonefish.png", Obtained = false },
            new Fish { Name = "Sturgeon", Season = "Summer,Winter", Weather = "None", Time = "6am-7pm", Image = "Sturgeon.png", Obtained = false },
            new Fish { Name = "Sunfish", Season = "Spring,Summer", Weather = "Sun", Time = "6am-7pm", Image = "Sunfish.png", Obtained = false },
            new Fish { Name = "Super Cucumber", Season = "Summer,Fall,Winter", Weather = "None", Time = "6pm-2am", Image = "Super_Cucumber.png", Obtained = false },
            new Fish { Name = "Tiger Trout", Season = "Fall,Winter", Weather = "None", Time = "6am-7pm", Image = "Tiger_Trout.png", Obtained = false },
            new Fish { Name = "Tilapia", Season = "Summer,Fall", Weather = "None", Time = "6am-2pm", Image = "Tilapia.png", Obtained = false },
            new Fish { Name = "Tuna", Season = "Summer,Winter", Weather = "None", Time = "6am-2pm", Image = "Tuna.png", Obtained = false },
            new Fish { Name = "Void Salmon", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Void_Salmon.png", Obtained = false },
            new Fish { Name = "Walleye", Season = "Fall", Weather = "Rain", Time = "12pm-2am", Image = "Walleye.png", Obtained = false },
            new Fish { Name = "Woodskip", Season = "Spring,Summer,Fall,Winter", Weather = "None", Time = "Anytime", Image = "Woodskip.png", Obtained = false }

            };

            var sortedFishList = fishList.OrderBy(f => f.Name).ToList();

            FishGrouped = new ObservableCollection<Grouping<string, Fish>>
            {
                new Grouping<string, Fish>("All Fish", sortedFishList)
            };

            // Initialize FilteredFishGrouped with all fish
            FilteredFishGrouped = new ObservableCollection<Grouping<string, Fish>>(FishGrouped);
        }

        private void FilterFish()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                // If search text is empty, show all fish
                FilteredFishGrouped = new ObservableCollection<Grouping<string, Fish>>(FishGrouped);
            }
            else
            {
                // Filter fish based on search text
                var filteredFish = FishGrouped
                    .SelectMany(g => g)
                    .Where(f => f.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                f.Season.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                f.Time.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                f.Weather.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(f => f.Name)
                    .ToList();

                FilteredFishGrouped = new ObservableCollection<Grouping<string, Fish>>
                {
                    new Grouping<string, Fish>("Some Fish", filteredFish)
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; }

            public Grouping(K key, IEnumerable<T> items) : base(items)
            {
                Key = key;
            }
        }
    }
}


