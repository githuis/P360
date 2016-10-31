using System;
using System.IO;
using Ordersystem.Utilities;

namespace Ordersystem.Droid.Utilities
{
    internal class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection(string filename)
        {
            var sqliteFilename = filename + ".db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}