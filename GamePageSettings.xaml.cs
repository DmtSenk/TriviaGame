using TriviaGame.ViewModel;
namespace TriviaGame;

public partial class GamePageSettings : ContentPage
{
    private GameSettingsViewModel viewModel;
    public GamePageSettings()
	{
		InitializeComponent();
        viewModel = new GameSettingsViewModel();
        BindingContext = viewModel;
    }

    private async void OnStartGameClicked(object sender, EventArgs e)
    {
        string category = viewModel.SelectedCategory;
        string difficulty = viewModel.SelectedDifficulty;
        string gameMode = viewModel.SelectedGameMode;

        
        await DisplayAlert("Game Started",
            $"Category: {category}\nDifficulty: {difficulty}\nGame Mode: {gameMode}","OK");

        
        // await Navigation.PushAsync(new GamePage(category, difficulty, gameMode));

    }
}