using System.Reflection;

namespace ReflectionSample;

public delegate void LogDelegate(string message);


public abstract class CloneableObject : ICloneable
{
    public object Clone()
    {
        Type type = this.GetType();

        var clone = Activator.CreateInstance(type);

        foreach (var item in type.GetProperties())
        {
            var attribute = item.GetCustomAttribute<CloneRuleAttribute>();
            if (attribute != null)
            {
                if (!attribute.DontClone)
                {
                    if (item.PropertyType == typeof(string))
                    {
                        switch (attribute.Format)
                        {
                            case CasingFormat.None:
                                item.SetValue(clone, item.GetValue(this));
                                break;
                            case CasingFormat.UpperCase:
                                item.SetValue(clone, item.GetValue(this)?.ToString()?.ToUpper());
                                break;
                            case CasingFormat.LowerCase:
                                item.SetValue(clone, item.GetValue(this)?.ToString()?.ToLower());
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        item.SetValue(clone, item.GetValue(this));
                    }
                }
            }
            else
            {
                item.SetValue(clone, item.GetValue(this));
            }
        }

        LoggingDelegate($"Objekt vom Typ {type.Name} wurde geklont.");
        LoggingAction($"Objekt vom Typ {type.Name} wurde geklont.");

        // Möglichkeit 1 um NullReferenceException zu umgehen
        //LoggingDelegate?.Invoke($"Objekt vom Typ {type.Name} wurde geklont.");

        //// Möglichkeit 2 um NullReferenceException zu umgehen
        //if (LoggingDelegate != null)
        //{
        //    LoggingDelegate($"Objekt vom Typ {type.Name} wurde geklont.");
        //}

        return clone ?? new object();
    }

    public LogDelegate LoggingDelegate = delegate { };

    public Action<string> LoggingAction = delegate { };
}
