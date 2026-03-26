namespace Multitasking;

internal class _08_Lock
{
	public static int Counter { get; set; }

	public static object Lock { get; set; } = new();

	static void Main(string[] args)
	{
		List<Task> tasks = [];
		for (int i = 0; i < 100; i++)
			tasks.Add(Task.Run(Run));
		Console.ReadKey();
	}

	static void Run()
	{
		//Problem: Irreguläre Outputs -> Manche Zahlen werden viel zu spät/an der falschen Stelle ausgegeben
		for (int i = 0; i < 100; i++)
		{
			lock (Lock)
			{
				Counter++;
				Console.WriteLine(Counter);
			}

			//Montior: Selber Code wie Lock, aber als Methoden
			//Achtung: Exit nicht vergessen
			Monitor.Enter(Lock);
			Counter++;
			Console.WriteLine(Counter);
			Monitor.Exit(Lock);

			//Interlocked.Add(ref Counter, 1); //Automatisch gelocktes ++
		}
	}
}
