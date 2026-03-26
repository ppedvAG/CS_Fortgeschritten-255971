namespace Multitasking;

internal class _05_CancellationToken
{
	static void Main(string[] args)
	{
		//CancellationToken
		//Sender, Empfänger
		//Sender == Source; diese Source produziert CancellationTokens, und gibt diese an die Tasks weiter
		CancellationTokenSource cts = new CancellationTokenSource();
		CancellationToken ct = cts.Token; //CT ist ein struct, d.h., dass bei jedem Zugriff dieses Tokens eine Kopie erstellt wird

		Task t = new Task(Run, ct);
		t.Start();

		Thread.Sleep(500);
		cts.Cancel(); //Sende ein Abbruch-Signal an alle Tokens

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is not CancellationToken ct)
			return;

		for (int i = 0; i < 100; i++)
		{
			if (ct.IsCancellationRequested)
			{
				//Kann mit throw kombiniert werden, oder mit einem einfachen return/break
				ct.ThrowIfCancellationRequested();

				//Wenn ein Task mit einer Exception abstürzt, kann man dies nicht sehen
				//Hier wird auch ContinueWith benötigt
			}

			Console.WriteLine($"Task: {i}");
			Thread.Sleep(25);
		}
	}
}