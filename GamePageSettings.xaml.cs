using TriviaGame.ViewModel;
using TriviaGame.TriviaDB;
namespace TriviaGame;

public partial class GamePageSettings : ContentPage
{
    private GameSettingsViewModel viewModel;
    private List<string> playersName;
    private string gameMode;
    public GamePageSettings(string gameMode, List<string> playersName)
	{
		InitializeComponent();
        this.gameMode = gameMode;
        this.playersName = playersName;
        viewModel = new GameSettingsViewModel();
        BindingContext = viewModel;
    }

    private async void OnStartGameClicked(object sender, EventArgs e)
    {
        string category = viewModel.SelectedCategory;
        string difficulty = viewModel.SelectedDifficulty;

        int rounds = 6;
        double timer = 60;

        switch (gameMode)
        {
            case "Classic":
            case "Survival":
            case "Streak":
            case "Elimination":
                rounds = Preferences.Get("NumberOfRounds", 6);
                timer = Preferences.Get("TimerDuration", 30.0);
                break;
            case "Race":
                rounds = Preferences.Get("NumberOfRounds", 6);
                timer = 10.0;
                break;
            default:
                rounds = Preferences.Get("NumberOfRounds", 6);
                timer = Preferences.Get("TimerDuration", 30.0);
                break;
        }

        int caregoryId = ConvertCategoryToId(category);

        var trivia = new Trivia();
        var questions = await trivia.GetQuestions(amount: rounds, categoryId: caregoryId, difficulty: difficulty);
        
        await Navigation.PushAsync(new GamePage(questions, rounds, timer, gameMode, playersName));

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