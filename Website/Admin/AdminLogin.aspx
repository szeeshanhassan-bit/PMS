<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="Admin_leadAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.7.2.min.js"></script>
    <link href="../Styles/common/bootstrap.css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top:50px">
    <div class="container">
        <div class="row">
            <div class="col-lg-4 col-md-2 col-sm-2"></div>
            <div class="col-lg-4 col-md-8 col-sm-8">
      <div class="modal-content">
                    <div id="divLoginHeader"  class="modal-header">
                      Login To Admin Panel                      
                    </div>
                    <div class="modal-body">
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                            <input type="text" id="txtUsername" runat="server" style="width:88%" class="form-control" placeholder="Username" />
                        </div>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-cog"></span></span>
                            <input type="password" id="txtPassword" runat="server" style="width:88%" class="form-control" placeholder="Password" />
                        </div>

                        <div id="divLoginError" runat="server" visible="false" style="color: red; padding-top: 5px; margin-left: 10px;">User name and password does not match</div>
                    </div>
                    <div class="modal-footer" style="margin-top: 0px">
                        <button type="submit" class="btn" id="btnLogin" onclick="ValidateUser()">Go</button>
                    </div>

    </div>
       </div>
            <div class="col-lg-4 col-md-2 col-sm-2"></div>
            </div>
                 </div>
        </div>
    </form>
</body>
</html>
<script>    
    $(document).ready(function () {

        //alert()

        
        //$('#firstDiv').hide();
        //$('#secDiv').show();
    });


    function ValidateUser() {

        //server side function is used now

        var request = "{'username':'" + $("input[id$=txtUsername]").val() + "','password':'" + $("input[id$=txtPassword]").val() + "'}";
        $.ajax({
            type: "POST",
            url: "leadAdmin.aspx/ValidateUser",
            data: request,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successValidateUser,
            error: ErrorValidateUser
        });
    }
    function successValidateUser(data) {
        data = data.d;
        if (data == "1") {
            $('div[id$=divLoginError]').css('display', 'none');
            window.location = "../admin/adminhome.aspx";
            //call server side function to create session.
            // __doPostBack('Button2_Click', '');
        }
        else {
            $('div[id$=divLoginError]').css('display', 'block');
        }
    }

    function ErrorValidateUser() {

    }

   
</script>

