using System;

namespace Ordersystem.Exceptions
{
    /// <summary>
    /// Thrown when an item was not found in the SQLite database.
    /// </summary>
    public class ItemNotFoundException : ArgumentNullException
    {
        public ItemNotFoundException()
        {
        }

        public ItemNotFoundException(string paramName) : base(paramName)
        {
        }

        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ItemNotFoundException(string paramName, string message) : base(paramName, message)
        {
        }
    }
}
