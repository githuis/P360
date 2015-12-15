using System;

namespace Ordersystem
{
	/// <summary>
	/// Thrown when the orderlist recieved from the database is invalid.
	/// </summary>
	public class InvalidOrderlistException : NullReferenceException
	{
		/// <summary>
		/// Thrown when the orderlist recieved from the database is invalid.
		/// </summary>
		public InvalidOrderlistException () : base()
		{
		}

		/// <summary>
		/// Thrown when the orderlist recieved from the database is invalid.
		/// </summary>
		/// <param name="message">Exception message.</param>
		public InvalidOrderlistException (string message) : base(message)
		{
		}

		/// <summary>
		/// Thrown when the orderlist recieved from the database is invalid.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="innerException">Inner exception.</param>
		public InvalidOrderlistException (string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

