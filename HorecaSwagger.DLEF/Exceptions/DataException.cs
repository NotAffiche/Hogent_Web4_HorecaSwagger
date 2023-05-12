using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Exceptions;

public class DataException : Exception
{
    public DataException()
    {
    }

    public DataException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
