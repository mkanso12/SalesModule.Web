using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.DataAccess
{
    public interface IItemDataAccess
    {
        List<Item> GetAll();
        Item GetById(int id);
        void Insert(Item item);
        void Update(Item item);
        void Delete(int id);
    }
}