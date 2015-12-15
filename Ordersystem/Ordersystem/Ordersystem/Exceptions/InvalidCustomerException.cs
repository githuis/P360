using System;

namespace Ordersystem
{
	/// <summary>
	/// Thrown when the customer recieved from the database is invalid.
	/// </summary>
	public class InvalidCustomerException : NullReferenceException
	{
		/// <summary>
		/// Thrown when the customer recieved from the database is invalid.
		/// </summary>
		public InvalidCustomerException () : base()
		{
		}

		/// <summary>
		/// Thrown when the customer recieved from the database is invalid.
		/// </summary>
		/// <param name="message">Exception message.</param>
		public InvalidCustomerException (string message) : base(message)
		{
		}

		/// <summary>
		/// Thrown when the customer recieved from the database is invalid.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="innerException">Inner exception.</param>
		public InvalidCustomerException (string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

