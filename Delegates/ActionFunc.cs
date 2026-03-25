using System.Reflection.Metadata.Ecma335;

namespace Delegates;

internal class ActionFunc
{
	static void Main(string[] args)
	{
		//Action und Func
		//Platzhalter für viele verschiedene Delegates (wegen Generics)
		//Werden in der fortgeschrittene Programmierung überall verwendet
		//z.B.: Events, Multithreading, TPL, Linq, Reflection, ...

		//Action
		//Delegate welches bis zu 16 Parameter hat und keinen Wert zurückgibt (void)
		Action<int, int> add = Addiere; //Alles vom vorherigen Kapitel möglich
		add(3, 4);
		add?.Invoke(3, 4);

		//Praktisches Beispiel
		List<int> zahlen = Enumerable.Range(0, 20).ToList();
		zahlen.ForEach(PrintZahl);

		/////////////////////////////////////////////////////////////

		//Func
		//Delegate welches bis zu 16 Parameter hat und einen Wert zurückgibt (T)
		//WICHTIG: Der letzte generische Typparameter ist der Rückgabewert
		Func<int, int, double> div = Dividiere;
		double a = div(3, 5); //Im Gegensatz zur Action kann hier eine Variable angelegt werden
		
		double? b = div?.Invoke(3, 5); //Wenn div null ist, kommt hier null zurück
		double c = div?.Invoke(3, 5) ?? double.NaN; //Hier kann mit dem Null-Coalescing Operator der nullable-double (double?) umgangen werden

		//Praktisches Beispiel
		zahlen.Where(TeilbarDurch2);

		/////////////////////////////////////////////////////////////

		//Anonyme Funktionen
		//Delegate, welches nur einmal verwendet wird
		//Wird vorallem bei Linq verwendet
		div += delegate (int x, int y)
		{
			return x + y;
		};

		div += (int x, int y) =>
		{
			return x + y;
		};

		div += (x, y) =>
		{
			return x + y;
		};

		div += (int x, int y) => x + y;

		div += (x, y) => x + y; //Evolution der Kurzschreibweisen

		//Anwendung
		zahlen.ForEach((x) => { Console.WriteLine($"Die Zahl: {x}"); });
		zahlen.ForEach(x => Console.WriteLine($"Die Zahl: {x}"));
		zahlen.Where(x => { return x % 2 == 0; });
		zahlen.Where(x => x % 2 == 0);
	}

	static void Addiere(int a, int b)
	{
		Console.WriteLine($"{a} + {b} = {a + b}");
	}

	static void PrintZahl(int x)
	{
		Console.WriteLine($"Die Zahl: {x}");
	}

	static double Dividiere(int a, int b)
	{
		return (double) a / b;
	}

	static bool TeilbarDurch2(int x)
	{
		return x % 2 == 0;
	}
}
