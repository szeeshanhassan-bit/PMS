using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;


public partial class Admin_leadAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {           
            if (txtUsername.Value.ToLower() == "testadmin"  && txtPassword.Value == "test")
            {
                HttpContext.Current.Session["validuser"] = "valid";
                Response.Redirect("../Admin/Adminhome.aspx");
            }
            else
            {

                divLoginError.Visible = true;
            }
        }
    }  
}