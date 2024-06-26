using System.Globalization;

namespace Eros.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public NotFoundException(string message, params object[] args)
       : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
