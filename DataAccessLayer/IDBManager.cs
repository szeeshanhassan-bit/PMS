using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Interface for database operations
    /// </summary>
    public interface IDBManager
    {
        void CreateConnection();
        void CloseConnection();
        DataTable GetRecords(string query);
        int ExecuteQuery(string query);
        long GetMaxID(string query);
    }
}
