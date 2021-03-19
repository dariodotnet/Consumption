namespace Consumption
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        private DetailPage _detailPage;

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

            if (_detailPage != null)
            {
                _detailPage.LastTag -= DetailPageOnLastTag;
                _detailPage = null;
            }

            LoadData();
        }

        private void DetailPageOnLastTag(object sender, string e)
        {
            var items = App.DataBase
                .Table<Item>().ToList();

            var tags = items.GroupBy(x => x.Tag)
                .Select(x => x.Key)
                .ToList();

            if (TagPicker.SelectedIndex >= 0)
            {
                var index = tags.IndexOf(e);

                if (TagPicker.SelectedIndex != index)
                {
                    if (TagPicker.Items.Count != tags.Count)
                    {
                        TagPicker.ItemsSource = tags;
                    }
                    
                    TagPicker.SelectedIndex = index;
                }
            }
        }

        private void TagPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (string)TagPicker.SelectedItem;

            Items.ItemsSource = App.DataBase
                .Table<Item>().Where(x => x.Tag.Equals(selected)).ToList();
        }

        private void LoadData()
        {
            try
            {
                var items = App.DataBase
                    .Table<Item>().ToList();


                var tags = items.GroupBy(x => x.Tag)
                    .Select(x => x.Key)
                    .ToList();

                if (TagPicker.ItemsSource == null || TagPicker.ItemsSource.Count != tags.Count)
                {
                    TagPicker.ItemsSource = tags;
                }

                if (tags.Any() && TagPicker.SelectedIndex < 0)
                {
                    TagPicker.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private async void EmptyListButton_Clicked(object sender, EventArgs e)
        {
            _detailPage = new DetailPage();
            await Navigation.PushAsync(_detailPage);
            
            _detailPage.LastTag += DetailPageOnLastTag;
        }
    }
}