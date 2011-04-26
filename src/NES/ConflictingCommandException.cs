using System;

namespace NES
{
    public class ConflictingCommandException : Exception
    {
        public ConflictingCommandException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}