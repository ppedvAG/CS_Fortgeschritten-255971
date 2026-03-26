namespace Multitasking;

internal class _09_Mutex
{
	static void Main(string[] args)
	{
		//Mutex: Prozessübergreifender Speicher
		Mutex m;
		bool laeuftBereits = Mutex.TryOpenExisting("Multitasking", out m);
		if (laeuftBereits)
		{
			Console.WriteLine("Anwendung läuft bereits");
		}
		else
		{
			Console.WriteLine("Neue Anwendung");
			m = new Mutex(true, "Multitasking");
		}
		Console.ReadKey();
		m.Close(); //WICHTIG: Mutex am Ende der Anwendung schließen
	}
}
