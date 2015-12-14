using System.IO;
using System.Collections.Generic;
using Ordersystem.Droid.Utilities;

[assembly: Xamarin.Forms.Dependency(typeof(LocalFile))]
namespace Ordersystem.Droid.Utilities
{
    public class LocalFile : ILocalFile
    {
        private string _filepath;

		/// <summary>
		/// Creates a path to the file wherein the database lies.
		/// </summary>
		/// <param name="filename">The name of the file.</param>
        public void UseFilePath(string filename)
        {
            string fileName = filename + ".txt";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            _filepath = Path.Combine(documentsPath, fileName);
            if (!File.Exists(_filepath)) File.Create(_filepath);
        }

		/// <summary>
		/// Writes several lines to the file.
		/// </summary>
		/// <param name="Strings">The lines to be written.</param>
		/// <param name="append">If set to <c>true</c> append, otherwise overwrite.</param>
        public void WriteToFile(List<string> Strings, bool append)
        {
            using (var streamWriter = new StreamWriter(_filepath, append))
            {
                foreach (string String in Strings)
                {
                    streamWriter.WriteLine(String);
                }
                streamWriter.Close();
            }
        }

		/// <summary>
		/// Reads the file.
		/// </summary>
		/// <returns>The lines from the file.</returns>
        public List<string> ReadFile()
        {
            List<string> fileLines = new List<string>();
            using (var streamReader = new StreamReader(_filepath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }
                streamReader.Close();
            }

            return fileLines;
        }
    }
}