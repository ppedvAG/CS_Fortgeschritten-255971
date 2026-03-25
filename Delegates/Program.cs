namespace Delegates;

internal class Program
{
	public delegate void Vorstellung(string name); //Definition von dem Delegate

	/// <summary>
	/// Delegates
	/// 
	/// Eigener Typ, der erzeugt werden kann, um Methodenzeiger zu speichern
	/// </summary>
	static void Main(string[] args)
	{
		Vorstellung v = new Vorstellung(VorstellungDE); //Erstellung mit Initialmethode
		v("Max"); //Ausführung des Delegates

		Console.WriteLine("---------------------------------------");

		v += new Vorstellung(VorstellungEN); //Weitere Methode anhängen
		v += VorstellungEN; //Selber Effekt, aber kürzer
		v("Udo");

		Console.WriteLine("---------------------------------------");

		v -= VorstellungDE; //Entfernt die DE-Methode
		v("Tim");

		Console.WriteLine("---------------------------------------");

		v -= VorstellungEN;
		v -= VorstellungEN;
		//v("Max"); //Hier ist das gesamte Delegate null -> Null Check

		Console.WriteLine("---------------------------------------");

		if (v is not null)
			v.Invoke("Udo");

		v?.Invoke("Tim"); //Null propagation: Führt den Code nach dem Fragezeichen nur aus, wenn das Objekt nicht null ist

		foreach (Delegate d in v.GetInvocationList()) //Delegate anschauen
		{
			Console.WriteLine(d.Method.Name);
		}
	}

	static void VorstellungDE(string name)
	{
		Console.WriteLine($"Hallo mein Name ist {name}");
	}

	static void VorstellungEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}
