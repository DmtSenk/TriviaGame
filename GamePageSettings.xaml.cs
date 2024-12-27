using TriviaGame.ViewModel;
using TriviaGame.TriviaDB;
using System.Text.Json;
using System.Diagnostics;
namespace TriviaGame;

public partial class GamePageSettings : ContentPage
{
    private GameSettingsViewModel viewModel;
    private List<string> playersName;
    private string gameMode;
    private List<(Picker catPicker, Picker diffPicker) > roundPickers = new List<(Picker, Picker)>();



    public GamePageSettings(string gameMode, List<string> playersName)
	{
		InitializeComponent();
        this.gameMode = gameMode;
        this.playersName = playersName;
        viewModel = new GameSettingsViewModel();
        BindingContext = viewModel;
        CreateRoundPicker();
    }

    private void CreateRoundPicker()
    {
        int totalRounds = (int)Preferences.Get("NumberOfRounds", 5.0);

        for (int i = 0; i < totalRounds; i++)
        {
            var stack = new VerticalStackLayout { Spacing = 5 };

            var roundLabel = new Label
            {
                Text = $"Rounds {i+1}"
            };
            stack.Children.Add(roundLabel);

            var categoryPicker = new Picker
            {
                Title = "Choose Category",
                ItemsSource = viewModel.Categories
            };

            categoryPicker.SelectedItem = viewModel.SelectedCategory;
            stack.Children.Add(categoryPicker);

            var difficultyPicker = new Picker
            {
                Title = "Choose Difficulty",
                ItemsSource = viewModel.Difficulties
            };

            difficultyPicker.SelectedItem = viewModel.SelectedDifficulty;
            stack.Children.Add(difficultyPicker);

            roundPickers.Add((categoryPicker, difficultyPicker));
            RoundsStack.Children.Add(stack);
        }
    }

    private async void OnStartGameClicked(object sender, EventArgs e)
    {
        int numberOfROunds = (int)Preferences.Get("NumberOfRounds", 5.0);
        double timer = Preferences.Get("TimerDuration", 30.0);
        int questionsPerRound = (int)Preferences.Get("NumberOfQuestions", 5.0);
        int numberOfPlayers = playersName.Count;
        int totalRequiredQuestions = numberOfROunds * questionsPerRound * numberOfPlayers;

        switch (gameMode)
        {
            case "Race":
                timer = 10.0;
                break;
        }

        var questions = new List<TriviaQuestion>();
        var trivia = new Trivia();

        for (int roundIndex = 0; roundIndex < numberOfROunds; roundIndex++)
        {
            var (catPicker, diffPicker) = roundPickers[roundIndex];

            string chosenCategory = catPicker.SelectedItem as string;
            string chosenDifficulty = diffPicker.SelectedItem as string;

            int catId = ConvertCategoryToId(chosenCategory);

            var roundsQuest = await trivia.GetQuestions(
                amount: questionsPerRound,
                categoryId: catId,
                difficulty: chosenDifficulty.ToLower());
            questions.AddRange(roundsQuest);

            await Task.Delay(500);
        }

        if (questions.Count < totalRequiredQuestions)
        {
            await DisplayAlert("No Questions", "No questions found.", "OK");
            return;
        }
        await Navigation.PushAsync(new GamePage(questions, numberOfROunds, timer, gameMode, playersName));
    }
    private int ConvertCategoryToId(string categoryName)
    {
        switch (categoryName)
        {
            case "General Knowledge":
                return 9;
            case "Film":
                return 11;
            case "Music":
                return 12;
            case "Video Games":
                return 15;
            case "History":
                return 23;
            case "Mythology":
                return 20;
            default: return 9;
        }
    }
}