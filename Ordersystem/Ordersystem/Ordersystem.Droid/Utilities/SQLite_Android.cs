using System.IO;
using Ordersystem.Utilities;
using SQLite;

namespace Ordersystem.Droid.Utilities
{
    class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            const string sqliteFilename = "LocalDatabase.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}