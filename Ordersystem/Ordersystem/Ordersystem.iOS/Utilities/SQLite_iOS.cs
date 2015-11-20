using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Ordersystem.iOS.Utilities;
using Ordersystem.Utilities;

namespace Ordersystem.iOS.Utilities
{
    
    public class SQLite_iOS : ISQLite 
    {
        public SQLite.SQLiteConnection GetConnection()
        {
            const string sqliteFilename = "LocalDatabase.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}
