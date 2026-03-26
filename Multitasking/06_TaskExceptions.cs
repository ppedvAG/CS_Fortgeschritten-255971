namespace Multitasking;

internal class _06_TaskExceptions
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Run);
		t.Start();

		try
		{
			//Bei Wait(), WaitAll() und Result wird die besagte Exception geworfen
			t.Wait();

			Task.WaitAll(t);

			Console.WriteLine(t.Result);
		}
		catch (AggregateException ex)
		{
			Console.WriteLine(ex.InnerException);
		}
	}

	static int Run()
	{
		throw new Exception("Hallo");
	}
}
