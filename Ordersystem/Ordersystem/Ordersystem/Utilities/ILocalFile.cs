using System;
using System.Collections.Generic;

namespace Ordersystem
{
	public interface ILocalFile
	{
		void UseFilePath (string filename);

		void WriteSingleLineToFile (string String, bool append);

		void WriteSeveralLinesToFile (List<string> Strings, bool append);

		List<string> ReadFile();

		string ReadLineFromFile();

		bool Exists();
	}
}

