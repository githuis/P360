using System;
using System.IO;
using Ordersystem.Utilities;

namespace Ordersystem.iOS.Utilities
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite.SQLiteConnection GetConnection(string filename)
        {
            var sqliteFilename = filename + ".db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}