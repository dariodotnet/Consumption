namespace Consumption
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DateTime.Date = System.DateTime.UtcNow;
        }

        private void Tag_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateEntries();
        }

        private void Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateEntries();
        }

        private void ValidateEntries()
        {
            double.TryParse(Quantity.Text, out var result);
            Save.IsEnabled = !string.IsNullOrWhiteSpace(Tag.Text) && result >= 0.01;
        }

        private async void Save_Clicked(object sender, System.EventArgs e)
        {
            double.TryParse(Quantity.Text, out var result);

            var item = new Item();
            item.DateTime = DateTime.Date;
            item.Description = Description.Text;
            item.Tag = Tag.Text;
            item.Quantity = result;
            App.DataBase.Insert(item);

            await Navigation.PopAsync();
        }
    }
}