using System;

namespace Ordersystem
{
	/// <summary>
	/// Thrown when a customer was not found in the database.
	/// </summary>
	public class CustomerNotFoundException : NullReferenceException
	{
		/// <summary>
		/// Thrown when a customer was not found in the database.
		/// </summary>
		public CustomerNotFoundException () : base()
		{
		}

		/// <summary>
		/// Thrown when a customer was not found in the database.
		/// </summary>
		/// <param name="message">Exception message.</param>
		public CustomerNotFoundException (string message) : base(message)
		{
		}

		/// <summary>
		/// Thrown when a customer was not found in the database.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="innerException">Inner exception.</param>
		public CustomerNotFoundException (string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

