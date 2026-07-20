using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    /// <summary>
    /// Interface for business operations
    /// </summary>
    public interface IBusinessManager
    {
        List<SupplierModel> GetAllSuppliers();
        SupplierModel GetSupplierById(int id);
        int AddSupplier(dynamic request);
        bool UpdateSupplier(int id, dynamic request);
        bool DeleteSupplier(int id);
    }

    /// <summary>
    /// Supplier data model
    /// </summary>
    public class SupplierModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierAbn { get; set; }
        public string SupplierContact { get; set; }
        public bool IsActive { get; set; }
    }

}

