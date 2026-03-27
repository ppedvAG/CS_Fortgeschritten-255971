using System.Text.Json;

namespace Reflection;

internal class CodeGenerator
{
	static void Main(string[] args)
	{
		Type jeElement = typeof(JsonElement);
		List<string> lines = jeElement
			.GetMethods()
			.OrderBy(e => e.Name)
			.Select(e => $"{e.ReturnType.Name} => element.{e.Name}(),\n")
			.ToList();
		string full = string.Join("", lines);

		////////////////////////////////////////////

		string readJson2 = File.ReadAllText("");

		JsonDocument doc = JsonDocument.Parse(readJson2);
		JsonElement.ArrayEnumerator ae = doc.RootElement.EnumerateArray();
		foreach (JsonElement element in ae)
		{
			int v1 = element.GetProperty("MaxV").GetInt32();
			int v2 = element.GetProperty<int>("MaxV");
		}
	}

	public T GetProperty<T>(JsonElement e, string prop) where T : struct
	{
		//return T switch funktioniert nicht, weil für einen Typswitch ein Objekt benötigt wird
		//default(T) ist ein Trick, um ein Objekt zu erzeugen, dass genau den richtigen Typen hat
		JsonElement element = e.GetProperty(prop);
		object o = default(T) switch
		{
			bool => element.GetBoolean(),
			byte => element.GetByte(),
			byte[] => element.GetBytesFromBase64(),
			DateTime => element.GetDateTime(),
			DateTimeOffset => element.GetDateTimeOffset(),
			decimal => element.GetDecimal(),
			double => element.GetDouble(),
			Guid => element.GetGuid(),
			short => element.GetInt16(),
			int => element.GetInt32(),
			long => element.GetInt64(),
			sbyte => element.GetSByte(),
			float => element.GetSingle(),
			ushort => element.GetUInt16(),
			uint => element.GetUInt32(),
			ulong => element.GetUInt64(),
			_ => default(T)
		};
		return (T) o;
	}
}

public static class JsonExtensions
{
	public static T GetProperty<T>(this JsonElement e, string prop) where T : struct
	{
		//return T switch funktioniert nicht, weil für einen Typswitch ein Objekt benötigt wird
		//default(T) ist ein Trick, um ein Objekt zu erzeugen, dass genau den richtigen Typen hat
		JsonElement element = e.GetProperty(prop);
		object o = default(T) switch
		{
			bool => element.GetBoolean(),
			byte => element.GetByte(),
			byte[] => element.GetBytesFromBase64(),
			DateTime => element.GetDateTime(),
			DateTimeOffset => element.GetDateTimeOffset(),
			decimal => element.GetDecimal(),
			double => element.GetDouble(),
			Guid => element.GetGuid(),
			short => element.GetInt16(),
			int => element.GetInt32(),
			long => element.GetInt64(),
			sbyte => element.GetSByte(),
			float => element.GetSingle(),
			ushort => element.GetUInt16(),
			uint => element.GetUInt32(),
			ulong => element.GetUInt64(),
			_ => default(T)
		};
		return (T) o;
	}
}