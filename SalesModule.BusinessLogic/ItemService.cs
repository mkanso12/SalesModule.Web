using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.BusinessLogic
{
    public class ItemService : IItemService
    {
        private readonly IItemDataAccess _itemDataAccess;

        public ItemService(IItemDataAccess itemDataAccess)
        {
            _itemDataAccess = itemDataAccess;
        }

        public List<Item> GetAllItems()
        {
            return _itemDataAccess.GetAll();
        }

        public Item GetItem(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Item ID must be a positive integer.", nameof(id));
            return _itemDataAccess.GetById(id);
        }

        public void CreateItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Item name is required.");
            if (item.UnitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.");
            if (item.OnHandQuantity < 0)
                throw new ArgumentException("On-hand quantity cannot be negative.");

            _itemDataAccess.Insert(item);
        }

        public void UpdateItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (item.Id <= 0)
                throw new ArgumentException("Invalid item ID.");
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Item name is required.");
            if (item.UnitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.");
            if (item.OnHandQuantity < 0)
                throw new ArgumentException("On-hand quantity cannot be negative.");

            _itemDataAccess.Update(item);
        }

        public void DeleteItem(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Item ID must be a positive integer.", nameof(id));
            _itemDataAccess.Delete(id);
        }
    }
}
