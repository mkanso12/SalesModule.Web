using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.DataAccess
{
    public class ItemDataAccess : IItemDataAccess
    {
        private readonly string _connectionString;

        public ItemDataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<Item> GetAll()
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                return db.Items.ToList();
            }
        }

        public Item GetById(int id)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                return db.Items.FirstOrDefault(i => i.Id == id);
            }
        }

        public void Insert(Item item)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                db.Items.InsertOnSubmit(item);
                db.SubmitChanges();
            }
        }

        public void Update(Item item)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var existing = db.Items.FirstOrDefault(i => i.Id == item.Id);
                if (existing != null)
                {
                    existing.Name = item.Name;
                    existing.UnitPrice = item.UnitPrice;
                    existing.OnHandQuantity = item.OnHandQuantity;
                    db.SubmitChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                var item = db.Items.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    db.Items.DeleteOnSubmit(item);
                    db.SubmitChanges();
                }
            }
        }
    }
}