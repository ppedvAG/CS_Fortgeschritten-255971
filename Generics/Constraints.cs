namespace Generics;

public class Constraints
{
	static void Main(string[] args)
	{
		Person p = new Person();
		Test<Person>();
	}

	//Constraints verwenden
	static void Test<T>() where T : Person, new()
	{
		T obj = new T(); //new möglich dank new() Constraint
		Console.WriteLine(obj.Id); //Zugriff möglich dank Person Constraint
	}
}

public class Person
{
	public int Id { get; set; }

	public string Name { get; set; }

	//Hat einen Standardkonstruktor
	//Wenn ein Konstruktor erstellt wird, wird der Standardkonstruktor überschrieben
	public Person(int id, string name)
	{
		Id = id;
		Name = name;
	}

	public Person()
	{
		
	}
}

public class DataStore1<T> where T : Constraints; //Klasse selbst oder Unterklasse

public class DataStore2<T> where T : ICloneable; //Interface selbst oder Unterklasse

public class DataStore3<T> where T : class; //T muss ein Referenztyp sein

public class DataStore4<T> where T : struct; //T muss ein Wertetyp sein

public class DataStore5<T> where T : new(); //Der Typ muss einen Standardkonstruktor haben

public class DataStore6<T> where T : Enum; //Der Typ muss ein Enum sein

public class DataStore7<T> where T : Delegate; //Der Typ muss ein Delegate sein

public class DataStore8<T> where T : unmanaged; //Der Typ muss ein Basisdatentyp/Pointer/Enum sein

public class DataStore9<T> where T : class, new();

public class DataStore10<T1, T2>
	where T1 : class, new()
	where T2 : struct, allows ref struct;