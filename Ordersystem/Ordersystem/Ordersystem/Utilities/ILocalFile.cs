using System;
using System.Collections.Generic;

namespace Ordersystem
{
	public interface ILocalFile
	{
		/// <summary>
		/// Creates a path to the file wherein the database lies.
		/// </summary>
		/// <param name="filename">The name of the file.</param>
		void UseFilePath (string filename);

		/// <summary>
		/// Writes several lines to the file.
		/// </summary>
		/// <param name="Strings">The lines to be written.</param>
		/// <param name="append">If set to <c>true</c> append, otherwise overwrite.</param>
		void WriteToFile (List<string> Strings, bool append);

		/// <summary>
		/// Reads the file.
		/// </summary>
		/// <returns>The lines from the file.</returns>
		List<string> ReadFile();
	}
}

