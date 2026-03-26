using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();

		//Synchron
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		///////////////////////////////////////////////////////

		//Tasks
		//Task t1 = new Task(Toast);
		//Task t2 = new Task(Tasse);
		//Task t3 = t2.ContinueWith(v => Kaffee());
		//t1.ContinueWith(v =>
		//{
		//	if (t1.IsCompleted && t3.IsCompleted)
		//		Console.WriteLine(sw.ElapsedMilliseconds);
		//}); //Was passiert, wenn der Toast zuerst fertig wird?
		//t1.Start();
		//t3.ContinueWith(v =>
		//{
		//	if (t1.IsCompleted && t3.IsCompleted)
		//		Console.WriteLine(sw.ElapsedMilliseconds); 
		//});
		//t2.Start();
		////Console.WriteLine(sw.ElapsedMilliseconds); //Die korrekte Zeit wird hier nie ausgegeben, ohne WaitAll zu benutzen

		//Console.ReadKey();
		////Anstrengend, skaliert nicht, unbrauchbar

		///////////////////////////////////////////////////////

		//async/await
		//Task t1 = ToastAsync(); //Wenn eine Async-Methode gestartet wird, wird diese direkt als Task gestartet (kein new Task notwendig)
		//						//Task t1 = Task.Run(ToastAsync); //Nicht mehr notwendig
		//Task t2 = TasseAsync();
		//await t2; //Funktioniert wie Wait(), lässt aber den Main Thread weiterlaufen
		//Task t3 = KaffeeAsync();
		//await t3;
		//await t1;
		//Console.WriteLine(sw.ElapsedMilliseconds);

		//Verbesserung
		//Task t1 = ToastAsync(); //Wenn eine Async-Methode gestartet wird, wird diese direkt als Task gestartet (kein new Task notwendig)
		//Task t2 = TasseAsync().ContinueWith(v => KaffeeAsync().Wait()); //Der innere Task muss hier warten, damit er nicht abgebrochen wird
		//await Task.WhenAll(t1, t2); //Selbiger Effekt wie WaitAll(), kann aber selbst awaited werden
		//Console.WriteLine(sw.ElapsedMilliseconds);

		///////////////////////////////////////////////////////

		//async/await mit Objekten
		//Task<Toast> t1 = ToastObjectAsync();
		//Task<Tasse> t2 = TasseObjectAsync();
		//Tasse t = await t2; //Das await Keyword kann auch ein Result zurückgeben
		//Task<Kaffee> t3 = KaffeeObjectAsync(t);
		//Kaffee k = await t3;
		//Toast b = await t1;
		//Fruehstueck f = new Fruehstueck(b, k);
		//Console.WriteLine(sw.ElapsedMilliseconds);

		//Verbesserung
		Task<Toast> t1 = ToastObjectAsync();
		Task<Kaffee> t3 = KaffeeObjectAsync(await TasseObjectAsync());
		Fruehstueck f = new Fruehstueck(await t1, await t3);
		Console.WriteLine(sw.ElapsedMilliseconds);

		///////////////////////////////////////////////////////

		//Beliebige langandauernde Operation awaiten mithilfe von der Task-Klasse
		//Auch Code, der nicht async definiert ist, kann damit awaited werden

		List<int> zahlen = Enumerable.Range(0, 100_000_000).ToList();
		List<int> sortiert = zahlen.Order().ToList(); //Dieses Statement dauert ein bisschen (1.2s), dieses Statement wird auf dem Main Thread ausgeführt

		//Besser: 
		Task<List<int>> t = Task.Run(() => zahlen.Order().ToList()); //Dieses Statement wird auf einem Task ausgeführt -> für den User transparent
		List<int> sortiertAsync = await t; //Keine Blockade vom Main Thread
	}

	#region	Synchron
	static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron
	public static async Task ToastAsync()
	{
		await Task.Delay(2000); //await: Warte darauf, das dieser Task fertig werden
		Console.WriteLine("Toast fertig");
		//Kein return notwendig
	}

	public static async Task TasseAsync()
	{
		await Task.Delay(1500); //await: Warte darauf, das dieser Task fertig werden
		Console.WriteLine("Tasse fertig");
	}

	public static async Task KaffeeAsync()
	{
		await Task.Delay(1500); //await: Warte darauf, das dieser Task fertig werden
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	public static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000); //await: Warte darauf, das dieser Task fertig werden
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	public static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500); //await: Warte darauf, das dieser Task fertig werden
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	public static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500); //await: Warte darauf, das dieser Task fertig werden
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
}

public record Toast;

public record Tasse;

public record Kaffee(Tasse t);

public record Fruehstueck(Toast toast, Kaffee kaffee);