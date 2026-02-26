using System.Collections.Generic;
using SalesModule.DataAccess;

namespace SalesModule.BusinessLogic
{
    public class GLTransactionService : IGLTransactionService
    {
        private readonly IGLTransactionDataAccess _glDataAccess;

        public GLTransactionService(IGLTransactionDataAccess glDataAccess)
        {
            _glDataAccess = glDataAccess;
        }

        public List<GLTransaction> GetAllTransactions()
        {
            return _glDataAccess.GetAll();
        }
    }
}