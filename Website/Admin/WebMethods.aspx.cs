using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Admin_WebMethods : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Suppliers

    [WebMethod]
    public static int AddSupplier(Supplier objSupplier)
    {
        // call business manager
        try
        {
            BusinessManager businessManager = new BusinessManager();
            businessManager.ManageSupplier(objSupplier, OperationTypes.Add);
            return 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }                
       
    }
    [WebMethod]
    public static List<Supplier> Show_Suppliers()
    {
        List<Supplier> result = new List<Supplier>();

        Supplier objSupplier = new Supplier();
        // call business manager
        try
        {
            BusinessManager businessManager = new BusinessManager();
           result = businessManager.ManageSupplier(objSupplier, OperationTypes.GetAll);
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }         
    }
    [WebMethod]
    public static List<Supplier> Edit_Supplier(string id)
    {
        List<Supplier> result = new List<Supplier>();

        Supplier objSupplier = new Supplier();
        objSupplier.SUPPLIER_ID = Convert.ToInt32(id);
        // call business manager
        try
        {
            BusinessManager businessManager = new BusinessManager();
            result = businessManager.ManageSupplier(objSupplier, OperationTypes.GetSpecific);
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }         

    }
    [WebMethod]
    public static int Update_Supplier(Supplier objSupplier)
    {                
        // call business manager
        try
        {
            BusinessManager businessManager = new BusinessManager();
            businessManager.ManageSupplier(objSupplier, OperationTypes.Update);
            return 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    [WebMethod]
    public static int Delete_Supplier(string id)
    {       
        Supplier objSupplier = new Supplier();
        objSupplier.SUPPLIER_ID = Convert.ToInt32(id);
        // call business manager
        try
        {
            BusinessManager businessManager = new BusinessManager();
            businessManager.ManageSupplier(objSupplier, OperationTypes.Delete);
            return 1;
        }
        catch (Exception ex)
        {
            throw ex;
        } 
    }

    #endregion

}