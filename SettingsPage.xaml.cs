using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TriviaGame.Resources;

public partial class SettingsPage : ContentPage, INotifyPropertyChanged
{
    private bool isDarkMode;
    private double timerDuration;
    private double fontSize;
    private double numberOfRounds;
    private double numberOfQuestions;
    private double volume;
    public event PropertyChangedEventHandler PropertyChanged;

    public double Volume
    {
        get
        {
            return Preferences.Get("Volume", 1.0);
        }
        set
        {
            if(volume != value)
            {
                volume = value;
                Preferences.Set("Volume", value);
                TriviaGame.Audio.Volume = value;
                OnPropertyChanged();
            }
        }
    }

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

    public double NumberOfRounds
    {
        get
        {
            return numberOfRounds;
        }
        set
        {
            var newValue = Math.Round(value);
            if (numberOfRounds != newValue)
            {
                numberOfRounds = newValue;
                Preferences.Set("NumberOfRounds", newValue);
                OnPropertyChanged();
            }
        }
    }

    public double NumberOfQuestions
    {
        get
        {
            return numberOfQuestions;
        }
        set
        {
            var newValue = Math.Round(value);
            if (numberOfQuestions != newValue)
            {
                numberOfQuestions = newValue;
                Preferences.Set("NumberOfQuestions", newValue);
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
        numberOfRounds = Preferences.Get("NumberOfRounds", 5.0);
        numberOfQuestions = Preferences.Get("NumberOfQuestions", 5.0);
        volume = Preferences.Get("Volume", 0.6);
        BindingContext = this;
        
        SetTheme(IsDarkMode);
        ApplyFontSize(FontSize);
        VolumeSlider.Value = volume;
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
        Preferences.Set("NumberOfRounds", NumberOfRounds);
        Preferences.Set("NumberOfQuestions", NumberOfQuestions);
        Preferences.Set("Volume", Volume);

        Application.Current.UserAppTheme = IsDarkMode ? AppTheme.Dark : AppTheme.Light;
        Application.Current.Resources["DefaultFontSize"] = FontSize;

        await Navigation.PopAsync();
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
