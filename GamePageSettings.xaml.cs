using TriviaGame.ViewModel;
namespace TriviaGame;

public partial class GamePageSettings : ContentPage
{
    private GameSettingsViewModel viewModel;
    public GamePageSettings(string gameMode, List<string> playerNames)
	{
		InitializeComponent();
        viewModel = new GameSettingsViewModel();
        BindingContext = viewModel;
    }

    private async void OnStartGameClicked(object sender, EventArgs e)
    {
        string category = viewModel.SelectedCategory;
        string difficulty = viewModel.SelectedDifficulty;
        

        
        await DisplayAlert("Game Started",$"Category: {category}\nDifficulty: {difficulty}","OK");

        
        //await Navigation.PushAsync(new (category, difficulty, gameMode));

    }
}