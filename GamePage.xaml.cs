using System.Net;
using System.Text.Json;
using TriviaGame.TriviaDB;
namespace TriviaGame;

public partial class GamePage : ContentPage
{
    private List<TriviaQuestion> questions;
    private int totalRounds;
    private double timer;
    private string gameMode;
    private List<string> playerNames;
    private int qPerRound;
    
    private int currentRound = 1;
    private int currentQuestion = 0;
    private int currentQuestionRound = 1;
    private int currentPlayer = 0;
    private int remainingTime;

    private bool timerON;
    private List<int> playerScores;
    private List<int> playerHearts;
    private CancellationTokenSource cts;
    private int[] playerStreaks;

    public GamePage(List<TriviaQuestion> questions, int rounds, double timer, string gameMode, List<string> playerNames, int questionsPerRound)
	{
		InitializeComponent();
        this.questions = questions;
        this.totalRounds = rounds;
        this.timer = timer;
        this.gameMode = gameMode;
        this.playerNames = playerNames;
        this.qPerRound = questionsPerRound;

        playerStreaks = new int[playerNames.Count];
        playerScores = new List<int>();
        for(int i = 0; i < playerNames.Count; i++)
        {
            playerScores.Add(0);
        }

        playerHearts = new List<int>();
        for(int i = 0; i < playerNames.Count; i++)
        {
            playerHearts.Add(3);
        }

        Display();
        LoadQuestions();
        StartTimer();
	}
    
    private void Display()
    {
        RLabel.Text = $"Round {currentRound} of {totalRounds}";
        QTLabel.Text = $"Question {currentQuestionRound} of {qPerRound}";
        PlayerLabel.Text = $"Current Player: {playerNames[currentPlayer]}";

        SurvivalLayout.Children.Clear();
        if(gameMode == "Survival")
        {
            int heartsPLayer = playerHearts[currentPlayer];
            for(int i = 0; i < heartsPLayer; i++)
            {
                var heart = new Label
                {
                    Text = "♥",
                    TextColor = Colors.Red
                };
                SurvivalLayout.Children.Add(heart);
            }
        }
    }

    private void LoadQuestions()
    {
        if (currentQuestion >= questions.Count)
        {
            EndGame();
            return;
        }
        var q = questions[currentQuestion];

        string questionText = WebUtility.HtmlDecode(q.question);
        QLabel.Text = questionText;

        var Answers = new List<string>(q.incorrect_answers);
        Answers.Add(q.correct_answer);

        Answers = Answers.OrderBy(_ => Guid.NewGuid()).ToList();
        
        AnswersLayout.Children.Clear();

        foreach (var ans in Answers)
        {
            string answer = WebUtility.HtmlDecode(ans);
            var button = new Button { Text = answer, HorizontalOptions = LayoutOptions.Center };
            button.Clicked += (s, e) => OnAnswerClicked(answer, q.correct_answer);
            AnswersLayout.Children.Add(button);
        }
    }

    private void StartTimer()
    {
        remainingTime = (int)timer;
        Timer.Text = $"Time: {remainingTime}";
        timerON = true;

        cts = new CancellationTokenSource();
        var token = cts.Token;

        Device.StartTimer(TimeSpan.FromSeconds(1),() =>
        {
            if (token.IsCancellationRequested)
            {
                return false;
            }
            remainingTime--;
            if(remainingTime < 0)
            {
                TimeOut();
                return false;
            }
            Timer.Text = $"Time: {remainingTime}";
            return true;
        });
    }
    
    private void StopTimer()
    {
        if(cts != null && !cts.IsCancellationRequested)
        {
            cts.Cancel();
        }
        timerON = false;
    }

    private async void TimeOut()
    {
        await DisplayAlert("Timer", "Out of time", "Ok");
        WrongAnswer();
    }
    private async void OnAnswerClicked(string answer, string correctAns)
    {
        if (timerON)
        {
            StopTimer();
        }

        var q = questions[currentQuestion];
        string diff = q.difficulty?.ToLower();
        int points = 5;

        switch (diff)
        {
            case "easy":
                points = 5;
                break;
            case "medium":
                points = 7; 
                break;
            case "hard":
                points = 10;
                break;
        }

        if (answer == correctAns)
        {
            await DisplayAlert("Correct", "Well done", "Ok");
            
            if(gameMode == "Streak")
            {
                int bonus = 2 * playerStreaks[currentPlayer];
                playerStreaks[currentPlayer]++;
                playerScores[currentPlayer] += bonus;
            }
            else
            {
                playerScores[currentPlayer] += points;
            }
        }
        else
        {
            await DisplayAlert("Wrong", $"Correct answer:{correctAns}", "Ok");
            if(gameMode == "Streak")
            {
                playerStreaks[currentPlayer] = 0;
            }
            WrongAnswer();
        }
        
        currentPlayer++;

        if(playerNames.Count == 1 && currentQuestion >= (totalRounds * qPerRound) - 1)
        {
            EndGame();
            return;
        }

        if(currentPlayer >= playerNames.Count)
        {
            currentPlayer = 0;
            currentQuestionRound++;
            currentQuestion++;
        }

        if(currentQuestionRound > qPerRound)
        {
            currentRound++;
            if(currentRound > totalRounds)
            {
                EndGame();
                return;
            }
            currentQuestionRound = 1;
        }

        if(currentQuestion >= questions.Count)
        {
            EndGame();
            return;
        }
        Display();
        LoadQuestions();
        StartTimer();
    }
    private void WrongAnswer()
    {
        if(gameMode == "Survival")
        {
            playerHearts[currentPlayer]--;
            if(playerHearts[currentPlayer] <= 0)
            {
                DisplayAlert("Game over", "You're out of hearts", "Ok");
                playerNames.RemoveAt(currentPlayer);
                playerScores.RemoveAt(currentPlayer);
                playerHearts.RemoveAt(currentPlayer);

                if(playerNames.Count == 0)
                {
                    EndGame();
                    return;
                }

                if(currentRound >= playerNames.Count)
                {
                    currentPlayer = 0;
                }
            }
        }else if(gameMode == "Elimination")
        {
            string eliminatedPplayer = playerNames[currentPlayer];

            DisplayAlert("Eliminated", $"{eliminatedPplayer} is out", "Ok");

            playerNames.RemoveAt(currentPlayer);
            playerScores.RemoveAt(currentPlayer);

            if(playerNames.Count == 1)
            {
                DisplayAlert("Game over", $"{playerNames[0]} wins!", "Ok");
                EndGame();
                return;
            }
            if (currentPlayer >= playerNames.Count)
            {
                currentPlayer = 0;
            }
        }
    }

    private async void EndGame()
    {
        StopTimer();

        var scoreBoard = new List<(string Name, int Score)>();
        for(int i = 0; i < playerNames.Count; i++)
        {
            scoreBoard.Add((playerNames[i], playerScores[i]));
        }
        scoreBoard.Sort((a,b) =>  b.Score.CompareTo(a.Score));

        string output = "";
        foreach(var entry in scoreBoard)
        {
            output += $"\n{entry.Name}: {entry.Score}";
        }

        Preferences.Set("Leaderboard", output);

        await DisplayAlert("Game Finished", $"Thank you for playing this game\n{output}", "Ok");
        await Navigation.PopToRootAsync();
    }
}