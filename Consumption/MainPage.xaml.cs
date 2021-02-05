namespace Consumption
{
    using System;
    using System.Linq;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var items = App.DataBase
                .Table<Item>().ToList();


            var tags = items.GroupBy(x => x.Tag)
                                .Select(x => x.Key)
                                .ToList();

            TagPicker.ItemsSource = tags;

            if (tags.Any())
            {
                TagPicker.SelectedIndex = 0;

                Items.ItemsSource = items.Where(x => x.Tag.Equals(tags[0]));
            }
        }

        private void TagPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (string)TagPicker.SelectedItem;

            Items.ItemsSource = App.DataBase
                .Table<Item>().Where(x => x.Tag.Equals(selected)).ToList();
        }
    }
}