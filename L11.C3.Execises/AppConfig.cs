using System.Collections.Specialized;
using Option;

namespace L11.C3.Execises;

// Book code

// 5.  Write implementations for the methods in the `AppConfig` class
// below. (For both methods, a reasonable one-line method body is possible.
// Assume settings are of type string, numeric or date.) Can this
// implementation help you to test code that relies on settings in a
// `.config` file?
public class AppConfig
{
    NameValueCollection source;

    //public AppConfig() : this(ConfigurationManager.AppSettings) { }

    public AppConfig(NameValueCollection source)
    {
        this.source = source;
    }

    public Option<T> Get<T>(string key)
        => source[key] is null
            ? new None()
            : new Some<T>((T)Convert.ChangeType(source[key], typeof(T)));

    public T Get<T>(string key, T defaultValue)
        => Get<T>(key).Match(
            () => defaultValue,
            (value) => value);
}