using TriviaGame.TriviaDB;
namespace TriviaGame;

public partial class GamePage : ContentPage
{
    private List<TriviaQuestion> questions;
    private int rounds;
    private double timer;
    private string gameMode;
    private List<string> playerNames;

    private int currentRound = 1;
    private int currentPlayer = 0;
    public GamePage(List<TriviaQuestion> questions, int rounds, double timer, string gameMode, List<string> playerNames)
	{
		InitializeComponent();
        this.questions = questions;
        this.rounds = rounds;
        this.timer = timer;
        this.gameMode = gameMode;
        this.playerNames = playerNames;

        Display();
        LoadQuestions();
	}
    
    private void Display()
    {
        RLabel.Text = $"Round {currentRound} of {rounds}";
        PlayerLabel.Text = $"Current Player: {playerNames[currentPlayer]}";
    }
    
    private void LoadQuestions()
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}