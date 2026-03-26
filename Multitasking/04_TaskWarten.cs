namespace Multitasking;

internal class _04_TaskWarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		t.Wait(); //Warte auf den Task

		Task t2 = Task.Run(Run);
		Task t3 = Task.Run(Run);

		Task.WaitAll(t, t2, t3); //Warte auf mehrere Tasks

		Task.WaitAny(t, t2, t3); //Warte bis einer der drei Tasks fertig ist

		//Problem: Blockieren des Main Threads -> await
	}

	static void Run()
	{
		Thread.Sleep(1000);
	}
}