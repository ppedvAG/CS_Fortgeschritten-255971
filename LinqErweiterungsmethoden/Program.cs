using System.Diagnostics;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		//Listentheorie
		IEnumerable<int> zahlen = Enumerable.Range(0, 10); //<1ms, <1KB Speicher: Nur eine Anleitung

		List<int> daten = zahlen.ToList(); //Hier werden die Daten erzeugt: 1.2s, 4GB Speicher

		//WICHTIG: Immer mit IEnumerable arbeiten, wenn möglich
		//- Solange ein IEnumerable in einer Variable exisitiert, sollte es auch ein IEnumerable bleiben
		//- Wenn eine Funktionen Daten in Form einer Listen empfangen soll, sollte dieser Parameter den IEnumerable haben (anstatt List oder [])

		//Warum hier IEnumerable?
		//Weil bei einem IEnumerable (nur eine Anleitung) keine doppelte Iteration stattfindet
		//D.h., die Anleitung wird nur innerhalb der AddRange ausgeführt (und nicht hier)
		daten.AddRange(daten);

		//IEnumerator
		//Basiskomponente von allen Listen
		//Drei Bestandteile:
		//- object Current
		//- bool MoveNext()
		//- void Reset()
		//Zeigt immer auf ein Element, dieses Element kann ausgelesen werden, und mithilfe MoveNext() wird der Zeiger um ein Element weiterbewegt

		foreach (int i in daten) //MoveNext()
		{
			Console.WriteLine(i); //Current
		}
		//Reset()

		//Schleife iterieren ohne Schleife
		IEnumerator<int> enumerator = daten.GetEnumerator();
		enumerator.MoveNext(); //Bewegt den Enumerator in die Liste hinein
	start:
		Console.WriteLine($"Goto: {enumerator.Current}");
		if (enumerator.MoveNext())
			goto start;
		enumerator.Reset();

		//////////////////////////////////////////////////////////////////////

		//Linq
		List<int> z = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

		z.Sum();
		z.Min();
		z.Max();
		z.Average();

		z.First(); //Gibt den ersten Wert zurück, Exception wenn kein Wert gefunden wurde
		z.FirstOrDefault();  //Gibt den ersten Wert zurück, default(T) wenn kein Wert gefunden wurde (0)

		//z.First(e => e % 20 == 0); //Exception
		z.FirstOrDefault(e => e % 20 == 0); //0

		//Linq mit Objektliste
		List<Fahrzeug> fahrzeuge =
		[
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		];

		//Finde alle VWs
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW);

		//Finde alle VWs die mind. 250km/h fahren können
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW && e.MaxV > 250);
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW).Where(e => e.MaxV > 250); //Predicate: Bedingung

		//Nach Marke sortieren
		fahrzeuge.OrderBy(e => e.Marke); //Selector: Ein einzelnes Feld
		fahrzeuge.OrderByDescending(e => e.Marke);

		//Subsequente Sortierung
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV);
		fahrzeuge.OrderByDescending(e => e.Marke).ThenByDescending(e => e.MaxV);

		//All & Any
		if (fahrzeuge.All(e => e.MaxV > 250)) //Fahren alle Fahrzeuge über 250km/h?
		{
			//false
		}

		if (fahrzeuge.Any(e => e.MaxV > 300)) //Gibt es ein Fahrzeug, dass schneller als 300km/h fährt?
		{
			//true
		}

		//Prüfen ob alle Zeichen Buchstaben sind
		string text = "Hallo Welt";
		if (text.All(char.IsLetter))
		{

		}

		//Prüfen, ob ein Text eine E-Mail Adresse ist
		string mail = "Test@example.com";
		if (mail.Any(e => e == '@') && mail.Any(e => e == '.'))
		{

		}

		fahrzeuge.Any(); //Prüfen, ob die Liste Elemente enthält

		//Wieviele BMWs haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.BMW); //4
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).Count(); //Doppelte Iteration

		//Sum, Average, Min, MinBy, Max, MaxBy
		fahrzeuge.Min(e => e.MaxV); //Die Geschwindigkeit (int)
		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug, mit der kleinsten Geschwindigkeit (Fahrzeug)

		fahrzeuge.Average(e => e.MaxV); //Bilde den Durchschnitt, von allen Geschwindigkeiten (208.416666666)

		//Skip & Take
		//Webshop
		int page = 1;
		fahrzeuge.Skip(page * 10).Take(10); //0-9, 10-19

		//Finde die 3 schnellsten Fahrzeuge
		fahrzeuge.OrderByDescending(e => e.MaxV).Take(3);

		//Select
		//Transformiert die Liste

		//Zwei Anwendungsfälle für Select:
		//- Extrahieren von einem einzigen Feld (80%)
		//- Transformation (20%)

		//1.
		fahrzeuge.Select(e => e.Marke); //Nur die Marken, ohne Fahrzeuge

		fahrzeuge.Select(e => e.Marke).Distinct(); //Welche verschiedenen Marken haben wir?

		fahrzeuge.Select(e => e.MaxV).Sum();

		//2.

		//Beispiel: Eine Liste erzeugen von 0-10 mit 0.25er Schritten
		Enumerable.Range(0, 40).Select(e => e / 4.0); //Erzeuge eine Liste von 0-39, und konvertiere jedes Element in die Form x/4

		//Beispiel: Gesamte Listen casten
		Enumerable
			.Range(0, 40)
			.Select(e => e / 4.0)
			.Select(e => (int) e);

		//Beispiel: 08_Lock mit Linq
		List<Task> tasks = [];
		for (int i = 0; i < 100; i++)
			tasks.Add(Task.Run(Run));

		List<Task> aufgaben = Enumerable.Range(0, 100).Select(e => Task.Run(Run)).ToList();

		void Run() { }

		//Beispiel: Von einer Dateiliste die Endungen und Pfade abschneiden
		string[] files = Directory.GetFiles(@"C:\Windows");
		List<string> pfadeOhneEndung = [];
		foreach (string file in files)
			pfadeOhneEndung.Add(Path.GetFileNameWithoutExtension(file));

		List<string> pfade = Directory.GetFiles(@"C:\Windows").Select(Path.GetFileNameWithoutExtension).ToList();

		//SelectMany
		//Glättung
		List<int[]> zweiD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
		zweiD.SelectMany(e => e); //1D-Liste

		//GroupBy
		//Gruppen erzeugen anhand eines Kriteriums
		//Jedes Element kommt in seine Gruppe hinein
		fahrzeuge.GroupBy(e => e.Marke); //Mit einer Gruppierungsanleitung kann man nur schwer arbeiten -> ToDictionary

		Dictionary<FahrzeugMarke, List<Fahrzeug>> dict = fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(k => k.Key, v => v.ToList()); //IGrouping<FahrzeugMarke, Fahrzeug> -> List<Fahrzeug> über den ValueSelector

		//Beispiel: Durchschnittsgeschwindigkeit pro Marke
		Dictionary<FahrzeugMarke, double> dict2 = fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(k => k.Key, v => v.Average(e => e.MaxV));
	}
}

[DebuggerDisplay("Marke: {Marke}, MaxV: {MaxV}")]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }


	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }