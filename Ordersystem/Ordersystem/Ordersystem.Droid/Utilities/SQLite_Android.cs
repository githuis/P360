using System.IO;
using Ordersystem.Utilities;
using SQLite;
using Ordersystem.Droid.Utilities;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace Ordersystem.Droid.Utilities
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android() {}

        public SQLiteConnection GetConnection(string filename)
        {
            string sqliteFilename = filename + ".db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}