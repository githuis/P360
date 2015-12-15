using System;

namespace Ordersystem
{
	/// <summary>
	/// Thrown when an orderlist was not found in the database.
	/// </summary>
	public class OrderlistNotFoundException : NullReferenceException
	{
		/// <summary>
		/// Thrown when an orderlist was not found in the database.
		/// </summary>
		public OrderlistNotFoundException () : base()
		{
		}

		/// <summary>
		/// Thrown when an orderlist was not found in the database.
		/// </summary>
		/// <param name="message">Exception message.</param>
		public OrderlistNotFoundException (string message) : base(message)
		{
		}

		/// <summary>
		/// Thrown when an orderlist was not found in the database.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="innerException">Inner exception.</param>
		public OrderlistNotFoundException (string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

