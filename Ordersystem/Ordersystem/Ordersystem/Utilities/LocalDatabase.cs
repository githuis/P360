using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using Ordersystem.Model;

namespace Ordersystem.Utilities
{
    public class LocalDatabase
    {
        private SQLiteConnection _database;

        public LocalDatabase()
        {
            _database = DependencyService.Get<ISQLite>().GetConnection();
            _database.CreateTable<SQLItem<Order>>();
            _database.CreateTable<SQLItem<Orderlist>>();
            _database.CreateTable<SQLItem<int>>();
        }

        public Order GetOrder(int value)
        {
            int id = _database.Table<SQLItem<int>>().FirstOrDefault(x => x.Item == value).ID;
            return _database.Table<Order>().First(x => x. == id);
        }
    }
}
