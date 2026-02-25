using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.BusinessLogic
{
    public interface IItemService
    {
        List<Item> GetAllItems();
        Item GetItem(int id);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(int id);
    }
}