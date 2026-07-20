using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;
using System.Net.Mail;
using System.Web.UI.HtmlControls;



public partial class Admin_AdminHome : System.Web.UI.Page
{

    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(Session["validuser"].ToString());
        //Response.End();
        if ((Session["validuser"] == null))
        {
            Response.Redirect("AdminLogin.aspx");
        }
       
    }



    #region administration
    [WebMethod]
    public static int logout()
    {
        HttpContext.Current.Session["validuser"] = null;        
        return 1;
    }   
    #endregion

    [WebMethod]
    public static string GetControlHtml(string controlLocation)
    {
        // Create instance of the page control
        System.Web.UI.Page page = new System.Web.UI.Page();

        // Create instance of the user control
        UserControl userControl = (UserControl)page.LoadControl(controlLocation);

        //Form control is mandatory on page control to process User Controls
        HtmlForm form = new HtmlForm();

        //Add user control to the form
        form.Controls.Add(userControl);

        //Add form to the page
        page.Controls.Add(form);

        //Write the control Html to text writer
        StringWriter textWriter = new StringWriter();

        //execute page on server
        HttpContext.Current.Server.Execute(page, textWriter, false);

        // Clean up code and return html
        return CleanHtml(textWriter.ToString());
    }

    private static string CleanHtml(string html)
    {
        return Regex.Replace(html, @"<[/]?(form)[^>]*?>", "", RegexOptions.IgnoreCase);
    }

   

  
}   