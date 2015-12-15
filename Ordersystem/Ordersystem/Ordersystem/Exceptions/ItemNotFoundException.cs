using System;

namespace Ordersystem.Exceptions
{
    /// <summary>
    /// Thrown when an item was not found in the local database.
    /// </summary>
	public class ItemNotFoundException : NullReferenceException
    {
		/// <summary>
		/// Thrown when an item was not found in the local database.
		/// </summary>
        public ItemNotFoundException()
        {
        }

		/// <summary>
		/// Thrown when an item was not found in the local database.
		/// </summary>
		/// <param name="message">Exception message.</param>
		public ItemNotFoundException(string message) : base(Message)
        {
        }

		/// <summary>
		/// Thrown when an item was not found in the local database.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="innerException">Inner exception.</param>
        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
