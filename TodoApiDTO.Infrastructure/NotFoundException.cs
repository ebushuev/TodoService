using System;

namespace TodoApiDTO.Infrastructure
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
