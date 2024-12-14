using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TriviaGame.Resources;

public partial class SettingsPage : ContentPage, INotifyPropertyChanged
{
    private bool isDarkMode;
    private double timerDuration;
    private double fontSize;
    public event PropertyChangedEventHandler PropertyChanged;

    public bool IsDarkMode
    {
        get
        {
            return Preferences.Get("IsDarkMode", false);
        }
        set
        {
            Preferences.Set("IsDarkMode", value);
            SetTheme(value);
            OnPropertyChanged();
        }
    }

    public double TimerDuration
    {
        get
        {
            return timerDuration;
        }
        set
        {
            if (timerDuration != value)
            {
                timerDuration = value;
                Preferences.Set("TimerDuration", value);
                OnPropertyChanged();
            }
        }
    }

    public double FontSize
    {
        get
        {
            return fontSize;
        }
        set
        {
            if (fontSize != value)
            {
                fontSize = value;
                Preferences.Set("FontSize", value);
                OnPropertyChanged();
            }
        }
    }

    public SettingsPage()
    {
        InitializeComponent();
        
        isDarkMode = Preferences.Get("IsDarkMode", false);
        timerDuration = Preferences.Get("TimerDuration", 30.0);
        fontSize = Preferences.Get("FontSize", 16.0);
        
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
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
