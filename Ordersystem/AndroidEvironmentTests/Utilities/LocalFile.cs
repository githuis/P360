using System.IO;
using System.Collections.Generic;
using Ordersystem.Droid.Utilities;

[assembly: Xamarin.Forms.Dependency(typeof(LocalFile))]
namespace Ordersystem.Droid.Utilities
{
    public class LocalFile : ILocalFile
    {
        public bool FilePathIsSet { get { return _filepath != null; } }

        private string _filepath = null;

        /// <summary>
        /// Creates a path to the file wherein the database lies.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        public void UseFilePath(string filename)
        {
            string fileName = filename + ".txt";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            _filepath = Path.Combine(documentsPath, fileName);
            if (!File.Exists(_filepath)) File.Create(_filepath).Dispose();
        }

        /// <summary>
        /// Writes several lines to the file.
        /// </summary>
        /// <param name="Strings">The lines to be written.</param>
        public void WriteToFile(List<string> strings)
        {
            File.WriteAllLines(_filepath, strings);
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <returns>The lines from the file.</returns>
        public List<string> ReadFile()
        {
            return new List<string>(File.ReadAllLines(_filepath));
        }

        /// <summary>
        /// Removes all contents of the file.
        /// </summary>
        public void CleanFile()
        {
            File.Create(_filepath).Dispose();
        }
    }
}