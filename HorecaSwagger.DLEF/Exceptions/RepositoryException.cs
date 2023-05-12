using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Exceptions;

public class RepositoryException : Exception
{
    public RepositoryException()
    {
    }

    public RepositoryException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
