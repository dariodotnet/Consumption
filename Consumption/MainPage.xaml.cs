namespace Consumption
{
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "Dario", "Manuel", "Jorge", "Rafael", "Mario", "Alejandro"
            };
        }
    }
}