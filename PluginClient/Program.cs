using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		IPlugin calc = LoadPlugin(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2026_03_25\PluginCalculator\bin\Debug\net9.0\PluginCalculator.dll");

		Console.WriteLine($"Name: {calc.Name}");
		Console.WriteLine($"Description: {calc.Description}");
		Console.WriteLine($"Version: {calc.Version}");
		Console.WriteLine($"Autor: {calc.Author}");

		foreach (MethodInfo mi in calc.GetType().GetMethods())
		{
			if (mi.GetCustomAttribute<ReflectionVisible>() != null) //Hat die jetztige Methode das Attribut?
			{
				Console.WriteLine(mi.GetCustomAttribute<ReflectionVisible>().Name);
				Console.WriteLine($"{mi.Name}");
			}
		}
	}

	static IPlugin LoadPlugin(string path)
	{
		Assembly a = Assembly.LoadFrom(path);

		Type pluginType = a.GetTypes().First(e => e.GetInterface(nameof(IPlugin)) != null); //Suche die erste Klasse, die das Interface hat

		return (IPlugin) Activator.CreateInstance(pluginType);
	}
}
