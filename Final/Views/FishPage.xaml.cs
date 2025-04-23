using Final.ViewModels;

namespace Final.Views
{
    public partial class FishPage : ContentPage
    {
        private readonly FishViewModel viewModel;

        public FishPage()
        {
            InitializeComponent();
            viewModel = new FishViewModel();
            BindingContext = viewModel;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.SearchText = e.NewTextValue;
        }
    }
}
