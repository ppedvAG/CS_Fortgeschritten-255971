using System.Collections.Concurrent;

namespace Multitasking;

internal class _10_ConcurrentCollection
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> bag = [];
		//bag[0] //Kein Index
		bag.Add(1);
		bag.Add(2);
		bag.Add(3);

		ConcurrentDictionary<int, string> dict = [];
		dict.TryAdd(1, "Eins"); //Kann scheitern, wenn ein anderer Task das Element bereits hinzugefügt hat

		//dict[0] //Index wie bei Dictionary

		SynchronizedCollection<int> collection = [];
		//collection[0] //Alternative zum Bag, aber mit einem Index
	}
}
