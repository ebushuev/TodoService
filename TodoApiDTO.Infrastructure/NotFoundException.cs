using System;

namespace TodoApiDTO.Infrastructure
{
    /// <summary>
    /// Исключение, возникающее, когда не удалось найти задачу
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
