namespace TriviaGame.Resources;

public partial class SettingsPage : ContentPage
{
    public bool IsDarkMode
    {
        get => Preferences.Get("IsDarkMode", false);
        set
        {
            Preferences.Set("IsDarkMode", value);
            SetTheme(value);
        }
    }

    public double TimerDuration
    {
        get => Preferences.Get("TimerDuration", (double)30);
        set => Preferences.Set("TimerDuration", value);
    }

    public double FontSize
    {
        get => Preferences.Get("FontSize", (double)16);
        set => Preferences.Set("FontSize", value);
    }

    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = this;
        SetTheme(IsDarkMode);
        ApplyFontSize(FontSize);
    }

    private void SetTheme(bool isDarkMode)
    {
        if (isDarkMode)
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }

    private void ApplyFontSize(double fontSize)
    {
        Application.Current.Resources["DefaultFontSize"] = fontSize;
    }

    private async void OnSaveSettingsClicked(object sender, EventArgs e)
    {
        Preferences.Set("IsDarkMode", IsDarkMode);
        Preferences.Set("TimerDuration", TimerDuration);
        Preferences.Set("FontSize", FontSize);

        Application.Current.UserAppTheme = IsDarkMode ? AppTheme.Dark : AppTheme.Light;
        Application.Current.Resources["DefaultFontSize"] = FontSize;

        await Navigation.PopAsync();
    }
}
