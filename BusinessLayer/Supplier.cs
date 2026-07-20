using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace BusinessLayer
{
    public class Supplier
    {
        private DBManager _dbManager;
        public int SUPPLIER_ID { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public string SUPPLIER_ADDRESS { get; set; }
        public string SUPPLIER_ABN { get; set; }
        public string SUPPLIER_CONTACT { get; set; }
        public bool IS_ACTIVE { get; set; }


        public Supplier()
        {
            _dbManager = new DBManager();
        }
        public long GetMaxSupplierId()
        {
            string query = "SELECT ISNULL(MAX(SUPPLIER_ID),0)+1 AS sup FROM TBL_SUPPLIER";
            return _dbManager.getMaxID(query);
        }

        public int addSupplier()
        {
            string query = @"INSERT INTO TBL_SUPPLIER (SUPPLIER_ID,SUPPLIER_NAME,SUPPLIER_ADDRESS, SUPPLIER_ABN,SUPPLIER_CONTACT,IS_ACTIVE)
                                                VALUES('" + GetMaxSupplierId() + "','" + this.SUPPLIER_NAME + "','"
                                                          + this.SUPPLIER_ADDRESS + "','" + this.SUPPLIER_ABN
                                                          + "','" + this.SUPPLIER_CONTACT
                                                          + "','" + this.IS_ACTIVE + "')";
            int result = _dbManager.executeQuery(query);
            return result;            
        }
        public bool updateSupplier()
        {
            string query = "UPDATE TBL_SUPPLIER SET SUPPLIER_NAME ='" + this.SUPPLIER_NAME + "',SUPPLIER_ADDRESS='" + this.SUPPLIER_ADDRESS + "',SUPPLIER_ABN='" + this.SUPPLIER_ABN + "',SUPPLIER_CONTACT='" + this.SUPPLIER_CONTACT + "' WHERE SUPPLIER_ID=" + this.SUPPLIER_ID + "";
            int result = _dbManager.executeQuery(query);                                    
            return true;
        }
        public bool deleteSupplier()
        {
            string query = "UPDATE TBL_SUPPLIER SET IS_ACTIVE =0 WHERE SUPPLIER_ID ='" + this.SUPPLIER_ID + "'";
            _dbManager.executeQuery(query);

            return true;

        }
        public List<Supplier> GetAllSupplier()
        {
            string query = "SELECT * FROM TBL_SUPPLIER WHERE ISNULL(IS_ACTIVE,1) =1 ";

           return CommonFunctions.ConvertDataTable<Supplier>( _dbManager.GetRecords(query));

        }
        public List<Supplier> GetSpecificSupplier()
        {
            string query = "SELECT * FROM  TBL_SUPPLIER WHERE SUPPLIER_ID ='" + this.SUPPLIER_ID + "'";
            return CommonFunctions.ConvertDataTable<Supplier>(_dbManager.GetRecords(query));

        } 
    }
}
