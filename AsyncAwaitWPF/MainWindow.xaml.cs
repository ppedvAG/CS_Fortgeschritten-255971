using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Output.Text += i + "\n";
			Scroll.ScrollToEnd();
		}
	}

	private void Button_Click_TaskRun(object sender, RoutedEventArgs e)
	{
		Task.Run(() =>
		{
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Dispatcher.Invoke(() => Output.Text += i + "\n");
				Dispatcher.Invoke(() => Scroll.ScrollToEnd());
			}
		});
	}

	private async void Button_Click_Async(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 100; i++)
		{
			await Task.Delay(25);
			Output.Text += i + "\n";
			Scroll.ScrollToEnd();
		}
	}

	private async void Request(object sender, RoutedEventArgs e)
	{
		string url = "http://www.gutenberg.org/files/54700/54700-0.txt";

		//Starten
		using HttpClient client = new HttpClient();
		Task<HttpResponseMessage> x = client.GetAsync(url);

		//Zwischenschritte
		Output.Text = "Text wird heruntergeladen...";
		ReqButton.IsEnabled = false;

		//Warten
		HttpResponseMessage response = await x;

		if (response.IsSuccessStatusCode)
		{
			//Starten
			Task<string> text = response.Content.ReadAsStringAsync();

			//Zwischenschritte
			Output.Text = "Text wird ausgelesen...";

			//Warten
			string content = await text;

			Output.Text = content;
		}

		ReqButton.IsEnabled = true;
	}

	private async void Button_Click_AsyncDataSource(object sender, RoutedEventArgs e)
	{
		AsyncDataSource ads = new();
		//await foreach: Wartet auf den nächsten Datenpunkt, und führt die Schleife im Anschluss aus
		await foreach (int x in ads.Generate())
		{
			//await x;
			Output.Text += x + "\n";
		}
	}
}