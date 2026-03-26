namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		Task t2 = Task.Factory.StartNew(Run); //ab .NET Framework 4.0

		Task t3 = Task.Run(Run); //ab .NET Framework 4.5

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		//Task wurde vor dem Ende abgebrochen, weil der Main Thread fertig war
		//-> Vorder- und Hintergrundthreads
		//Tasks werden immer als Hintergrundthreads angelegt (über Threadpool)

		Console.ReadKey(); //Main Thread aufhalten
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Task: {i}");
		}
	}
}
