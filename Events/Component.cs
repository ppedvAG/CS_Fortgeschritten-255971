namespace Events;

/// <summary>
/// Entwicklerseite
/// </summary>
public class Component
{
	public event Action Start; //Events können beliebige Delegates halten

	public event Action Stop;

	public event Action<int> Progress;

	/// <summary>
	/// Simuliert einen länger andauernden Prozess
	/// </summary>
	public void Run()
	{
		Start?.Invoke();

		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(200);
			Progress?.Invoke(i);
		}

		Stop?.Invoke();
	}
}
