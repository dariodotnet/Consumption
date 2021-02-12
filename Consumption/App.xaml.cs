using Xamarin.Forms;

namespace Consumption
{
    using SQLite;
    using System.IO;
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
