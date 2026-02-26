using System.Collections.Generic;
using SalesModule.DataAccess;

namespace SalesModule.BusinessLogic
{
    public interface IGLTransactionService
    {
        List<GLTransaction> GetAllTransactions();
    }
}