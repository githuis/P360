using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Ordersystem.Utilities
{
    public class SQLItem<T>
    {
        public SQLItem(T item)
        {
            Item = item;
        }

        public T Item { get; private set; }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
