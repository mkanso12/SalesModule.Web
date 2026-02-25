using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesModule.DataAccess
{
    public interface IGLTransactionDataAccess
    {
        void Insert(GLTransaction transaction, List<GLTransactionLine> lines);
    }
}