using TriviaGame.Resources;

namespace TriviaGame
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            UpdatePlayerEntries(1);
        }

        private void AmountOfPLayersSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            int playersCount = (int)Math.Round(e.NewValue);
            AmOfPlayers.Text = playersCount + " Player" + (playersCount > 1 ? "s" : "");
            UpdatePlayerEntries(playersCount);
        }
        private void UpdatePlayerEntries(int playersCount)
        {
            PlayersNames.Children.Clear();
            for (int i = 0; i < playersCount; i++)
            {
                PlayersNames.Children.Add(new Entry{ Placeholder = "Input player name"});
            }
        }

        private async void ButtonStart_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void ButtonSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
