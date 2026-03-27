using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		//Reflection
		//Ermittlung von Metainformationen über Objekte zur Laufzeit
		//z.B.: Methoden, Properties, Events, ...

		//Beginnt immer bei einem Type Objekt

		//Zwei Optionen:
		//- GetType()
		//- typeof(...)

		Program p = new Program();
		Type pt1 = p.GetType();

		Type pt2 = typeof(Program); //typeof kann nur mit konkreten Typnamen verwendet werden (nicht mit Objekten)

		Console.WriteLine(pt2);

		////////////////////////////////////////////

		//Dynamische Analyse/Verwendung von Objekten
		MethodInfo mi = pt2.GetMethod("Test");
		mi.Invoke(null, null);

		MethodInfo mi2 = pt2.GetMethod("Test2", BindingFlags.NonPublic | BindingFlags.Static); //private Methoden/Properties können hier angesprochen werden
		mi2.Invoke(null, null);

		Program p2 = new Program();
		PropertyInfo pi = pt2.GetProperty("Name"); //Property per Reflection ansprechen
		pi.SetValue(p2, "Max");

		////////////////////////////////////////////

		//Activator
		//Ermöglicht das Erzeugen von Objekten über ein Type-Objekt (oder über einen String)
		object o = Activator.CreateInstance(pt2);
		o.GetType().GetMethod("Test").Invoke(null, null);

		//Assembly
		//Codeblock, welcher sich in einer DLL befinden (oder alternativ: Projekt)
		Assembly a = Assembly.GetExecutingAssembly(); //Das derzeitige Projekt
													  //Assembly.LoadFrom("Pfad"); //Externe DLL laden

		////////////////////////////////////////////

		//Aufgabe: Component laden und verwenden
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2026_03_25\Events\bin\Debug\net9.0\Events.dll";
		Assembly loaded = Assembly.LoadFrom(pfad);

		Type compType = loaded.GetType("Events.Component");

		object component = Activator.CreateInstance(compType);

		compType.GetEvent("Start").AddEventHandler(component, () => Console.WriteLine("Reflection Start"));
		compType.GetEvent("Stop").AddEventHandler(component, () => Console.WriteLine("Reflection Stop"));
		compType.GetEvent("Progress").AddEventHandler(component, (int x) => Console.WriteLine($"Reflection Fortschritt: {x}"));

		compType.GetMethod("Run").Invoke(component, null);
	}

	public string Name { get; set; }

	public static void Test()
	{
		Console.WriteLine("Hallo Welt");
	}

	private static void Test2()
	{
		Console.WriteLine("Private Test");
	}
}
