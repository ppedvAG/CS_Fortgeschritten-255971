namespace Multitasking;

internal class _07_ContinueWith
{
	static void Main(string[] args)
	{
		//ContinueWith
		//Taskketten erzeugen
		//Wenn Task 1 fertig ist wird Task 2 gestartet

		Task<int> t = new Task<int>(Run);
		t.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result));
		t.Start(); //WICHTIG: Vor dem Start() die ContinueWith() Statements anlegen

		/////////////////////////////////////////////////////////////////////////////////////
		
		//Beispiel: 03_TaskMitReturn
		Task<int> t2 = new Task<int>(Run);
		t2.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result));
		t2.Start();

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
			Thread.Sleep(25);
		}

		/////////////////////////////////////////////////////////////////////////////////////

		//Beispiel: 05_CancellationToken
		Task t3 = new Task(Run2);
		Task b = t3.ContinueWith(v => Console.WriteLine("Fertig")); //Wird bei Erfolg und bei Misserfolg gestartet
		Task f = t3.ContinueWith(v => Console.WriteLine("Fertig"), TaskContinuationOptions.OnlyOnRanToCompletion); //TaskContinuationOptions: Folgetask nur starten, unter bestimmten Bedingungen
		Task ex = t3.ContinueWith(v => Console.WriteLine(v.Exception), TaskContinuationOptions.OnlyOnFaulted); //TaskContinuationOptions: Folgetask nur starten, unter bestimmten Bedingungen
		t3.Start(); //ContinueWith-Tasks können auch in Variablen gespeichert werden

		Console.ReadKey();
	}

	static int Run()
	{
		//Thread.Sleep(1000);
		return Random.Shared.Next();
	}

	static void Run2()
	{
		throw new Exception("Hallo");
	}
}
