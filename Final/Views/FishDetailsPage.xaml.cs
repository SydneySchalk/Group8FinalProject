using Final.Models;

namespace Final.Views
{
    public partial class FishDetailsPage : ContentPage
    {
        public FishDetailsPage(Fish selectedFish)
        {
            InitializeComponent();
            BindingContext = selectedFish;
        }
    }
}
