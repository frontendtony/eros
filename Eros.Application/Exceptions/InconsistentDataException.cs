using System.Globalization;

namespace Eros.Application.Exceptions;

public class InconsistentDataException : Exception
{
    public InconsistentDataException(string message) : base(message)
    {
    }

    public InconsistentDataException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public InconsistentDataException(string message, params object[] args)
       : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
