using System.IO;
using System.Collections.Generic;
using Ordersystem.Droid.Utilities;

[assembly: Xamarin.Forms.Dependency(typeof(LocalFile))]
namespace Ordersystem.Droid.Utilities
{
    public class LocalFile : ILocalFile
    {
        private string _filepath;

        public void UseFilePath(string filename)
        {
            string fileName = filename + ".txt";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            _filepath = Path.Combine(documentsPath, fileName);
            if (!File.Exists(_filepath)) File.Create(_filepath);
        }

        public void WriteSingleLineToFile(string String, bool append)
        {
            using (var streamWriter = new StreamWriter(_filepath, append))
            {
                streamWriter.WriteLine(String);
                streamWriter.Close();
            }
        }

        public void WriteSeveralLinesToFile(List<string> Strings, bool append)
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

        public string ReadLineFromFile()
        {
            using (var streamReader = new StreamReader(_filepath))
            {
                string lineRead = streamReader.ReadLine();
                streamReader.Close();
                return lineRead;
            }
        }

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

        public bool Exists()
        {
            return File.Exists(_filepath);
        }
    }
}