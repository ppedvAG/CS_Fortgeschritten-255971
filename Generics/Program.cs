using System.Collections;

namespace Generics;

internal class Program
{
	private static void Main(string[] args)
	{
		List<int> zahlen = [];
		zahlen.Add(5); //T wird hier durch int ersetzt

		Test<int>(); //Innerhalb der Methode wird T durch int ersetzt

		/////////////////////////////////////////

		DataStore<int> x = new DataStore<int>();
		Console.WriteLine(x[1]); //Ist jetzt möglich wegen Indexer
								 //x[0] = 5;

		foreach (int i in zahlen) //Ist jetzt möglich wegen IEnumerable
		{
			Console.WriteLine(i);
		}
	}

	public static void Test<T>()
	{
		Console.WriteLine(typeof(T)); //Der Typ des Generics
		Console.WriteLine(nameof(T)); //Der Name des Generics
		Console.WriteLine(default(T)); //Der Standardwert des Generics
	}
}

public class DataStore<T> : IEnumerable<T>
{
	private T[] _items;

	public List<T> Items => _items.ToList();

	public void Add(T item, int index)
	{
		_items[index] = item;
	}

	public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public T this[int index]
	{
		get => _items[index];
	}
}