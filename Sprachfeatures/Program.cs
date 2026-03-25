using IntList = System.Collections.Generic.List<int>;

namespace Sprachfeatures;

internal unsafe class Program
{
	static unsafe void Main(string[] args)
	{
		string zahl = "1";
		if (int.TryParse(zahl, out int x)) //Wird darüber normal erzeugt
		{

		}
		Console.WriteLine(x);

		//Typvergleiche

		//is
		//Beachtet Vererbungshierarchien
		StreamWriter sw = new StreamWriter(zahl);
		if (sw is TextWriter)
		{
			//true
		}

		//Genauer Typvergleich
		if (sw.GetType() == typeof(TextWriter))
		{
			//false
		}

		if (sw is IDisposable)
		{
			//Typvergleiche mit Interfaces müssen immer mit is durchgeführt werden
		}

		//class und struct

		//class
		//Referenztyp
		//Wenn ein Objekt eines Referenztypens auf eine Variable zugewiesen wird, wird eine Referenz erstellt
		//Wenn zwei Objekte von Referenztypen verglichen werden, werden die Speicheradressen verglichen
		RefTest r = new RefTest() { Zahl = 5 };
		RefTest r2 = r; //Hier wird ein Zeiger auf das Objekt unter r gelegt
		r2.Zahl = 10;

		Console.WriteLine(r == r2);
		Console.WriteLine(r.GetHashCode() == r2.GetHashCode());
		Console.WriteLine(r.GetHashCode());
		Console.WriteLine(r2.GetHashCode());

		//struct
		//Wertetyp
		//Wenn ein Objekt eines Wertetypens auf eine Variable zugewiesen wird, wird eine Kopie erstellt
		//Wenn zwei Objekte von Wertetypen verglichen werden, werden die Inhalte verglichen
		int z = 5;
		int z2 = z;
		z2 = 10;

		//ref
		//Beliebige Typen referenzierbar machen
		int a = 5;
		ref int b = ref a; //Hier wird ein Zeiger auf a gelegt
		b = 10;

		//Test(a); //Ohne ref wird a unverändert bleiben
		Test(ref a); //Mit ref wird a von hier verändert

		unsafe
		{

		}

		//switch Pattern
		string zahl2 = string.Empty;
		string input = "1";
		switch (input)
		{
			case "1":
				zahl2 = "Eins";
				break;
			case "2":
				zahl2 = "Zwei";
				break;
			default:
				zahl2 = "Andere Zahl";
				break;
		}

		string zahl3 = input switch
		{
			"1" => "Eins",
			"2" => "Zwei",
			_ => "Andere Zahl"
		};

		string vorname = "lUkAs";
		string v = char.ToUpper(vorname[0]) + vorname[1..].ToLower();

		//String Interpolation ($-String): Code in einen String einbetten
		Console.WriteLine("Die zahl ist: " + zahl + ", a ist: " + a + ", z ist: " + z);
		Console.WriteLine($"Die zahl ist: {zahl}, a ist: {a}, z ist: {z}");
		Console.WriteLine($"Die zahl2 ist: {(zahl2 == string.Empty ? "Leer" : zahl2)}");

		//Verbatim String (@-String): String, der Escape Sequenzen ignoriert
		Console.WriteLine(@"\n\r\t");
		Console.WriteLine(@"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\9.0.10\System.Console.dll"); //Hier werden die Escape-Sequenzen ignoriert

		RefTest t = null;
		//Console.WriteLine(t); //'t' may be null here
		if (t != null)
		{
			Console.WriteLine(t); //'t' is not null here
		}

		Person p = new Person(1, "Max");
		Console.WriteLine(p.ID);
		Console.WriteLine(p.Name);

		Console.WriteLine(p);

		(int id, string name) = p; //Hier wird die Deconstruct-Methode verwendet
		Console.WriteLine(id);
		Console.WriteLine(name);

		List<int> zahlen = [];
		IntList zahlen2 = [];

		int m = 1;
		double d = m; //Implicit operator
		m = (int) d; //Explicit operator

		Console.WriteLine(zahlen[0]);
	}

	public static void Test(ref int x)
	{
		x = 50;
	}
}

public class RefTest
{
	public int Zahl;
}

public record Person(int ID, string Name);