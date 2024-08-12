using MathGame.Models;
using Microsoft.Maui.Graphics.Text;
using System.Reflection.Emit;

namespace MathGame.Maui;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;
	const int numberOfGames = 3;
	int gamesLeft = numberOfGames;

	public GamePage(string gameType)
	{
		InitializeComponent();
		GameType = gameType;
		BindingContext = this;

		CreateNewQuestion();

	}

	private void CreateNewQuestion()
	{
		var gameOperand = GameType switch
		{
			"Addition" => "+",
			"Subtraction" => "-",
			"Multiplication" => "*",
			"Division" => "/",
			_ => ""
		};

		var random = new Random();

		if (GameType == "Division")
		{
			int[] divNumbers = Utility.GetDivisionNumbers();
			firstNumber = divNumbers[0];
			secondNumber = divNumbers[1];
		}
		else
		{
			firstNumber = random.Next(1, 9);
			secondNumber = random.Next(1, 9);
		}

		LBLquestion.Text = $"{firstNumber} {gameOperand} {secondNumber}";

	}

	private void OnAnswerSubmitted(object sender, EventArgs e)
	{
		var answer = int.Parse(ENTanswer.Text);
		var isCorrect = false;

		switch (GameType)
		{
			case "Addition":
				isCorrect = answer == firstNumber + secondNumber;
				break;
			case "Subtraction":
				isCorrect = answer == firstNumber - secondNumber;
				break;
			case "Multiplication":
				isCorrect = answer == firstNumber * secondNumber;
				break;
			case "Division":
				isCorrect = answer == firstNumber / secondNumber;
				break;
		}

		ProcessAnswer(isCorrect);
		LBLanswer.TextColor = Colors.White;
		gamesLeft--;
		ENTanswer.Text = "";

		if (gamesLeft == 0)
			GameOverPrompt();
		else
			CreateNewQuestion();

	}

	private void OnBackToMenu(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage());
	}

    private void ProcessAnswer(bool isCorrect)
	{
		if (isCorrect)
		{
			score += 1;
			LBLanswer.TextColor = Colors.Green;
			LBLanswer.Text = "Correct Answer";
		}
		else
		{
            LBLanswer.TextColor = Colors.Red;
            LBLanswer.Text = "Incorrect Answer";
        }
	}

	private void GameOverPrompt()
	{

		GameOperation gameOperation = GameType switch
		{
			"Addition" => GameOperation.Addition,
            "Subtraction" => GameOperation.Subtraction,
            "Multiplication" => GameOperation.Multiplication,
            "Division" => GameOperation.Division,
        };

		QuestionArea.IsVisible = false;
		BTNbackToMenu.IsVisible = true;
		LBLgameOver.Text = $"Game Over. Final Score is {score} out of {numberOfGames} point(s).";

		App.GameRepository.Add(new Game
		{
			Date = DateTime.Now,
			Type = gameOperation,
			Score = score
		});

	}

}