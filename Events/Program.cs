namespace Events;

/// <summary>
/// Events
/// </summary>
internal class Program
{
	static void Main(string[] args) => new Program().Run();

	//////////////////////////////////////////////////////////////////////////////

	public event EventHandler TestEvent; //Entwicklerseite

	public event EventHandler<int> IntEvent; //Generell sollte hier ein EventArgs Typ verwendet werden

	public event EventHandler<TestEventArgs> ArgsEvent;

	//////////////////////////////////////////////////////////////////////////////

	private event EventHandler accessorEvent;

	public event EventHandler AccessorEvent
	{
		add
		{
			accessorEvent += value;
			Console.WriteLine($"{value.Method.Name} angehängt");
		}
		remove => accessorEvent -= value;
	}

	public void Run()
	{
		TestEvent += Program_TestEvent; //Anwenderseite

		TestEvent?.Invoke(this, EventArgs.Empty); //Entwicklerseite

		///////////////////////////////////////

		IntEvent += Program_IntEvent;

		IntEvent?.Invoke(this, 10);

		///////////////////////////////////////

		ArgsEvent += Program_ArgsEvent;

		ArgsEvent?.Invoke(this, new TestEventArgs() { Status = "Verbindung hergestellt" });

		///////////////////////////////////////

		AccessorEvent += Program_AccessorEvent;

		accessorEvent?.Invoke(this, EventArgs.Empty); //Hier kann nicht das Event mit Accessoren ausgeführt werden
	}

	private void Program_TestEvent(object? sender, EventArgs e)
	{
		Console.WriteLine("TestEvent ausgeführt");
	}

	private void Program_IntEvent(object? sender, int e)
	{
		Console.WriteLine($"Die Zahl ist: {e}");
	}

	private void Program_ArgsEvent(object? sender, TestEventArgs e)
	{
		Console.WriteLine($"Status: {e.Status}");
	}

	private void Program_AccessorEvent(object? sender, EventArgs e)
	{
		Console.WriteLine("Private Event ausgeführt");
	}
}

public class TestEventArgs : EventArgs
{
	public string Status { get; set; }
}