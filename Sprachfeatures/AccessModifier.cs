namespace Sprachfeatures;

public class AccessModifier
{
	public int Alter { get; set; } //public: Kann von überall angegriffen werden (auch außerhalb vom Projekt)

	internal string Vorname { get; set; } //internal: Kann von überall angegriffen werden, aber nicht außerhalb vom Projekt

	private string Nachname { get; set; } //private: Kann nur innerhalb der Klasse selbst angegriffen werden

	//////////////////////////////////////////////////////
	
	protected string Adresse { get; set; } //protected: Wie private, aber auch in Unterklassen sichtbar (auch außerhalb)

	protected internal int Groesse { get; set; } //protected internal: Im gesamten Projekt sichtbar (wie internal), aber auch in Unterklassen außerhalb

	protected private string Haarfarbe { get; set; } //protected private: Wie protected, aber nur im Projekt verwendbar
}

public class Test : AccessModifier
{
	public void X()
	{
        //Console.WriteLine(Nachname);
        Console.WriteLine(Adresse);
    }
}

public partial class TestClass
{
	public void PartialTest()
	{

	}
}