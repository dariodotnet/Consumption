namespace Consumption
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Item> ItemsCollection { get; set; }

        public MainPage()
        {
            InitializeComponent();

            ItemsCollection = new ObservableCollection<Item>();

            BindingContext = this;

            TagPicker.PropertyChanged += TagPickerOnPropertyChanged;
        }

        private void TagPickerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(TagPicker.ItemsSource)))
            {
                TagPicker.IsVisible = TagPicker.ItemsSource.Count > 0;
                Footer.IsVisible = TagPicker.IsVisible;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();

            //Checking the Items binding is working
            //Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            //{
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        ItemsCollection.Add(new Item
            //        {
            //            DateTime = DateTime.UtcNow,
            //            Description = "Probando timer",
            //            Id = 1,
            //            Quantity = 1.24,
            //            Tag = "Electricidad"
            //        });
            //    });
            //    return false;
            //});
        }

        private void TagPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (string)TagPicker.SelectedItem;

            Items.ItemsSource = App.DataBase
                .Table<Item>().Where(x => x.Tag.Equals(selected)).ToList();
        }

        private void LoadData()
        {
            var items = App.DataBase
                .Table<Item>().ToList();


            var tags = items.GroupBy(x => x.Tag)
                .Select(x => x.Key)
                .ToList();

            TagPicker.ItemsSource = tags;

            if (tags.Any())
            {
                TagPicker.SelectedIndex = 0;
            }
        }
    }
}