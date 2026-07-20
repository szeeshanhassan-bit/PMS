using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace BusinessLayer
{
    /// <summary>
    /// Business logic manager for suppliers
    /// Implements IBusinessManager interface
    /// </summary>
    public class BusinessManager : IBusinessManager
    {
        private readonly IDBManager _dbManager;

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        public BusinessManager(IDBManager dbManager)
        {
            _dbManager = dbManager ?? throw new ArgumentNullException(nameof(dbManager));
        }

        /// <summary>
        /// Get all active suppliers
        /// </summary>
        public List<SupplierModel> GetAllSuppliers()
        {
            try
            {
                string query = "SELECT SUPPLIER_ID as SupplierId, SUPPLIER_NAME as SupplierName, " +
                               "SUPPLIER_ADDRESS as SupplierAddress, SUPPLIER_ABN as SupplierAbn, " +
                               "SUPPLIER_CONTACT as SupplierContact, IS_ACTIVE as IsActive " +
                               "FROM TBL_SUPPLIER WHERE ISNULL(IS_ACTIVE, 1) = 1 ORDER BY SUPPLIER_NAME";

                var dataTable = _dbManager.GetRecords(query);
                return CommonFunctions.ConvertDataTable<SupplierModel>(dataTable);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving suppliers", ex);
            }
        }

        /// <summary>
        /// Get a specific supplier by ID
        /// </summary>
        public SupplierModel GetSupplierById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid supplier ID", nameof(id));

                string query = "SELECT SUPPLIER_ID as SupplierId, SUPPLIER_NAME as SupplierName, " +
                               "SUPPLIER_ADDRESS as SupplierAddress, SUPPLIER_ABN as SupplierAbn, " +
                               "SUPPLIER_CONTACT as SupplierContact, IS_ACTIVE as IsActive " +
                               "FROM TBL_SUPPLIER WHERE SUPPLIER_ID = " + id;

                var dataTable = _dbManager.GetRecords(query);
                var suppliers = CommonFunctions.ConvertDataTable<SupplierModel>(dataTable);

                return suppliers.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving supplier with ID {id}", ex);
            }
        }

        /// <summary>
        /// Add a new supplier
        /// </summary>
        public int AddSupplier(dynamic request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                if (string.IsNullOrWhiteSpace(request.SupplierName))
                    throw new ArgumentException("Supplier name is required");

                long supplierId = GetMaxSupplierId();

                string query = $@"INSERT INTO TBL_SUPPLIER 
                                 (SUPPLIER_ID, SUPPLIER_NAME, SUPPLIER_ADDRESS, SUPPLIER_ABN, SUPPLIER_CONTACT, IS_ACTIVE)
                                 VALUES({supplierId}, '{EscapeSql(request.SupplierName)}', 
                                        '{EscapeSql(request.SupplierAddress ?? "")}', 
                                        '{EscapeSql(request.SupplierAbn ?? "")}', 
                                        '{EscapeSql(request.SupplierContact ?? "")}', 1)";

                _dbManager.ExecuteQuery(query);
                return (int)supplierId;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating supplier", ex);
            }
        }

        /// <summary>
        /// Update an existing supplier
        /// </summary>
        public bool UpdateSupplier(int id, dynamic request)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid supplier ID", nameof(id));

                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                if (string.IsNullOrWhiteSpace(request.SupplierName))
                    throw new ArgumentException("Supplier name is required");

                string query = $@"UPDATE TBL_SUPPLIER 
                                 SET SUPPLIER_NAME = '{EscapeSql(request.SupplierName)}', 
                                     SUPPLIER_ADDRESS = '{EscapeSql(request.SupplierAddress ?? "")}', 
                                     SUPPLIER_ABN = '{EscapeSql(request.SupplierAbn ?? "")}', 
                                     SUPPLIER_CONTACT = '{EscapeSql(request.SupplierContact ?? "")}' 
                                 WHERE SUPPLIER_ID = {id}";

                int result = _dbManager.ExecuteQuery(query);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error updating supplier with ID {id}", ex);
            }
        }

        /// <summary>
        /// Delete (soft delete) a supplier
        /// </summary>
        public bool DeleteSupplier(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid supplier ID", nameof(id));

                string query = $"UPDATE TBL_SUPPLIER SET IS_ACTIVE = 0 WHERE SUPPLIER_ID = {id}";
                int result = _dbManager.ExecuteQuery(query);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting supplier with ID {id}", ex);
            }
        }

        /// <summary>
        /// Get the next supplier ID
        /// </summary>
        private long GetMaxSupplierId()
        {
            try
            {
                string query = "SELECT ISNULL(MAX(SUPPLIER_ID), 0) + 1 AS NextId FROM TBL_SUPPLIER";
                return _dbManager.GetMaxID(query);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error getting next supplier ID", ex);
            }
        }

        /// <summary>
        /// Escape SQL string to prevent SQL injection
        /// </summary>
        private string EscapeSql(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return input.Replace("'", "''");
        }
    }
}
