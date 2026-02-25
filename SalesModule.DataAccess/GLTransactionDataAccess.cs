using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesModule.DataAccess
{
    public class GLTransactionDataAccess : IGLTransactionDataAccess
    {
        private readonly string _connectionString;

        public GLTransactionDataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void Insert(GLTransaction transaction, List<GLTransactionLine> lines)
        {
            using (var db = new SalesModuleDataContext(_connectionString))
            {
                // Insert transaction header
                db.GLTransactions.InsertOnSubmit(transaction);
                db.SubmitChanges(); // to generate Id

                // Insert lines with the new transaction Id
                foreach (var line in lines)
                {
                    line.GLTransactionId = transaction.Id;
                    db.GLTransactionLines.InsertOnSubmit(line);
                }
                db.SubmitChanges();
            }
        }
    }
}