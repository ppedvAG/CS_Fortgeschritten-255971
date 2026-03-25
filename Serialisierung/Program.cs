using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	public static List<Fahrzeug> Fahrzeuge =
	[
		new PKW(251, FahrzeugMarke.BMW),
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

	static void Main(string[] args)
	{
		//XML
		string xmlPfad = "Fahrzeuge.xml";

		//1. Serialisieren/Deserialisieren
		XmlSerializer xml = new XmlSerializer(Fahrzeuge.GetType());
		xml.Serialize(xmlPfad, Fahrzeuge);

		//using (FileStream fs = new FileStream(xmlPfad, FileMode.Create))
		//{
		//	xml.Serialize(fs, Fahrzeuge);
		//}

		List<Fahrzeug> fzg = xml.Deserialize<List<Fahrzeug>>(xmlPfad);
		//using (FileStream fs = new FileStream(xmlPfad, FileMode.Open))
		//{
		//	List<Fahrzeug> fzg = (List<Fahrzeug>) xml.Deserialize(fs);
		//}

		//2. Attribute
		
		//3. XML per Hand
		XmlDocument doc = new XmlDocument();
		doc.Load(xmlPfad);

		foreach (XmlNode node in doc.DocumentElement)
		{
			int v = int.Parse(node.Attributes["MaxV"].InnerText);
			FahrzeugMarke m = Enum.Parse<FahrzeugMarke>(node.Attributes["Marke"].InnerText);

			Console.WriteLine("-----------------------------");
			Console.WriteLine($"{v}, {m}");
		}
	}

	static void SystemJson()
	{
		//System.Text.Json
		string jsonPfad = "Fahrzeuge.json";

		//1. Serialisieren/Deserialisieren
		//string json = JsonSerializer.Serialize(Fahrzeuge);
		//File.WriteAllText(jsonPfad, json);

		//string readJson = File.ReadAllText(jsonPfad);
		//Fahrzeug[] fzg = JsonSerializer.Deserialize<Fahrzeug[]>(readJson);

		////2. Settings/Options
		//JsonSerializerOptions options = new JsonSerializerOptions();
		//options.WriteIndented = true;
		//options.Converters.Add(new JsonStringEnumConverter());

		//File.WriteAllText(jsonPfad, JsonSerializer.Serialize(Fahrzeuge, options)); //WICHTIG: Hier müssen die Options mitgegeben werden

		////3. Attribute
		////JsonExtensionData, JsonDerivedType
		//string readJson2 = File.ReadAllText(jsonPfad);
		//Fahrzeug[] fzg2 = JsonSerializer.Deserialize<Fahrzeug[]>(readJson2, options);

		////4. Json per Hand
		//JsonDocument doc = JsonDocument.Parse(readJson2);
		//JsonElement.ArrayEnumerator ae = doc.RootElement.EnumerateArray();
		//foreach (JsonElement element in ae)
		//{
		//	int v = element.GetProperty("MaxV").GetInt32();
		//	FahrzeugMarke m = Enum.Parse<FahrzeugMarke>(element.GetProperty("Marke").GetString());

		//	Console.WriteLine("------------------");
		//	Console.WriteLine($"{v}, {m}");
		//}
	}

	static void NewtonsoftJson()
	{
		//Newtonsoft.Json
		string jsonPfad = "Fahrzeuge.json";

		//1. Serialisieren/Deserialisieren
		string json = JsonConvert.SerializeObject(Fahrzeuge);
		File.WriteAllText(jsonPfad, json);

		string readJson = File.ReadAllText(jsonPfad);
		Fahrzeug[] fzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson);

		//2. Settings/Options
		JsonSerializerSettings settings = new JsonSerializerSettings();
		//settings.Formatting = Formatting.Indented;
		settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung aktivieren

		File.WriteAllText(jsonPfad, JsonConvert.SerializeObject(Fahrzeuge, settings)); //WICHTIG: Hier müssen die Options mitgegeben werden

		//3. Attribute
		//JsonExtensionData, JsonDerivedType
		string readJson2 = File.ReadAllText(jsonPfad);
		Fahrzeug[] fzg2 = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson);

		//4. Json per Hand
		JToken doc = JToken.Parse(readJson2); //JToken als Ersatz für JsonDocument und JsonElement
		foreach (JToken element in doc)
		{
			int v = element["Maximalgeschwindigkeit"].Value<int>();
			FahrzeugMarke m = Enum.Parse<FahrzeugMarke>(element["Marke"].Value<string>());

			Console.WriteLine("------------------");
			Console.WriteLine($"{v}, {m}");
		}
	}
}

[DebuggerDisplay("Marke: {Marke}, MaxV: {MaxV}")]
//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]
public class Fahrzeug
{
	[JsonProperty(PropertyName = "Maximalgeschwindigkeit", Order = 2)]
	[XmlAttribute]
	public int MaxV { get; set; }

	[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	//[JsonExtensionData]
	//public Dictionary<string, object> OtherData { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

	public Fahrzeug()
	{
		
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }

public class PKW : Fahrzeug
{
	public PKW(int maxV, FahrzeugMarke marke) : base(maxV, marke) { }

	public PKW()
	{
		
	}
}

/// <summary>
/// Erweiterungsmethoden
/// </summary>
public static class XmlExtensions
{
	public static void Serialize(this XmlSerializer xml, string pfad, object o) //Mit this den Typen beschreiben, der erweitert werden soll
	{
		using FileStream fs = new(pfad, FileMode.Create);
		xml.Serialize(fs, o);
	}

	public static T Deserialize<T>(this XmlSerializer xml, string pfad)
	{
		using FileStream fs = new FileStream(pfad, FileMode.Open);
		return (T) xml.Deserialize(fs);
	}
}