namespace Events;

/// <summary>
/// Anwenderseite
/// </summary>
internal class User
{
	static void Main(string[] args)
	{
		Component c = new Component();
		c.Start += C_Start;
		c.Stop += C_Stop;
		c.Progress += C_Progress;
		c.Run();
	}

	private static void C_Progress(int obj)
	{
		Console.WriteLine($"Fortschritt: {obj}");
	}

	private static void C_Stop()
	{
		Console.WriteLine("Prozess gestartet");
	}

	private static void C_Start()
	{
		Console.WriteLine("Prozess beendet");
	}
}
