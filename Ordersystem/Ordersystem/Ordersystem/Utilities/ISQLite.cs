using SQLite;

namespace Ordersystem.Utilities
{
    /// <summary>
    /// The interface for the SQLite.
    /// </summary>
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
