namespace MathGame.Maui;

public partial class GameHistoryPage : ContentPage
{
	public GameHistoryPage()
	{
		InitializeComponent();
		gamesList.ItemsSource = App.GameRepository.GetAllGames();
	}

	private void OnDelete(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		App.GameRepository.Delete((int)button.BindingContext);
		//Navigation.PushAsync(new GameHistoryPage());
	}

}