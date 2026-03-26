namespace AsyncAwaitWPF;

public class AsyncDataSource
{
	/// <summary>
	/// Beispiel: Livestream, sehr große Datenmengen bei einer Datenbank
	/// -> nicht alle Daten gleichzeitig laden, sondern Teile laden und diese sofort verarbeiten, und währenddessen Daten weiterladen
	/// </summary>
	public async IAsyncEnumerable<int> Generate()
	{
		while (true)
		{
			await Task.Delay(Random.Shared.Next(100, 1000));
			yield return Random.Shared.Next(); //yield return: Gib den jetztigen Wert zurück, und bewege den Zeiger auf das nächste Element
		}
	}
}