using Xamarin.Forms;

namespace Consumption
{
    using SQLite;
    using System;
    using System.IO;
    using System.Linq;
    using Xamarin.Essentials;

    public partial class App : Application
    {
        private readonly string _databasePath;
        public static SQLiteConnection DataBase { get; set; }

        public App()
        {
            InitializeComponent();
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, "consumption.db3");

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            DataBase = new SQLiteConnection(_databasePath);

            DataBase.CreateTable<Item>();

            var dummyData = App.DataBase.Table<Item>().ToList();

            if (dummyData == null || !dummyData.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    var tag = "Electricity";
                    if (i > 49)
                    {
                        tag = "Gas";
                    }

                    var item = new Item
                    {
                        DateTime = DateTime.UtcNow.AddDays(-100).AddDays(i),
                        Description = $"Dummy data {i}",
                        Tag = tag
                    };
                    DataBase.Insert(item);
                }
            }
        }

        protected override void OnSleep()
        {
            DataBase?.Close();
        }

        protected override void OnResume()
        {
            DataBase = new SQLiteConnection(_databasePath);
        }
    }
}
