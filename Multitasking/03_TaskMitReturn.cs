namespace Multitasking;

internal class _03_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Run);
		t.Start();

		//Diese Anweisung blockiert den Main Thread
		//Lösungen: ContinueWith, async/await
		//Console.WriteLine(t.Result);

		bool hasPrinted = false;
		for (int i = 0; i < 100; i++)
		{
			if (!hasPrinted && t.IsCompletedSuccessfully)
			{
				Console.WriteLine(t.Result);
				hasPrinted = true;
			}
			Console.WriteLine($"Main Thread: {i}");
			Thread.Sleep(25);
		}

		//Wenn der Main Thread vor dem Task fertig ist, wird das Ergebnis nie ausgegeben
		if (!hasPrinted && t.IsCompletedSuccessfully)
			Console.WriteLine(t.Result);

		Console.ReadKey();
	}

	static int Run()
	{
		Thread.Sleep(1000);
		return Random.Shared.Next();
	}
}
