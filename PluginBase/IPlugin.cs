namespace PluginBase;

/// <summary>
/// IPlugin
/// 
/// Stellt einen konkreten Typen (anstatt object) bereit
/// Optional
/// </summary>
public interface IPlugin
{
	string Name { get; }

	string Description { get; }

	string Version { get; }

	string Author { get; }
}
