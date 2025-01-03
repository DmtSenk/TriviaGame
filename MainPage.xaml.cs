﻿using TriviaGame.Resources;
using TriviaGame.ViewModel;

namespace TriviaGame
{
    public partial class MainPage : ContentPage
    {
        private GameSettingsViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new GameSettingsViewModel();
            BindingContext = viewModel;
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
            var playerNames = new List<string>();
            foreach (var child in PlayersNames.Children)
            {
                if (child is Entry entry && !string.IsNullOrWhiteSpace(entry.Text))
                {
                    playerNames.Add(entry.Text);
                }
            }

            if (playerNames.Count == 0)
            {
                await DisplayAlert("Error", "Please enter player name.", "OK");
                return;
            }

            string gameMode = viewModel.SelectedGameMode;

            await Navigation.PushAsync(new GamePageSettings(gameMode, playerNames));
        }

        private async void ButtonSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void Info_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Info","Trivia Game\nCategories: General Knowledge, Film, Music, Video Games, History" +
                "\nDifficulties: Easy, Medium, Hard" +
                "\nGame Modes:" +
                "\t\n- Classic: classic trivia game" +
                "\t\n- Race: answer questions with fixed timer" +
                "\t\n- Survival: three lives to answer questions" +
                "\t\n- Steak: earn more points for streak" +
                "\t\n- Elimination: one wrong answer and you eliminated" +
                "\nIn settings you can change theme, round timer(except Race mode), amount of questions and rounds","OK");
        }

        private async void Leaderboard_Clicked(object sender, EventArgs e)
        {
            string scores = Preferences.Get("Leaderboard", "");
            if (string.IsNullOrEmpty(scores))
            {
                await DisplayAlert("Leaderboard", "No scores yet!", "OK");
            }
            else
            {
                await DisplayAlert("Leaderboard", scores, "OK");
            }
        }
    }
}
