namespace TriviaGame
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            bool isDarkMode = Preferences.Get("IsDarkMode", false);
            double fontSize = Preferences.Get("FontSize", 16.0);

            UserAppTheme = isDarkMode ? AppTheme.Dark : AppTheme.Light;
            Application.Current.Resources["DefaultFontSize"] = fontSize;

            MainPage = new AppShell();
        }
    }
}
