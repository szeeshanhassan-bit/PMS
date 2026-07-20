<%@ Page Title="Admin" Language="C#" MasterPageFile="AdminMaster.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="Admin_AdminHome" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
         
             
    <script>
        $(document).ajaxStart(function () {
            $.blockUI();
        }).ajaxStop(function () {
            $.unblockUI();            
        });
    </script>


    <%--dashboard css--%>
    <style>
        .column {
            width: 300px;
            float: left;
            padding-bottom: 100px;
            border: 1px;
            padding-left:20px;
        }

        .portlet {
            margin: 0 1em 1em 0;
            min-height: 200px;
        }

        .portlet-header {
            margin: 0.3em;
            background-color: #444444;
            color: white;
            padding-bottom: 4px;
            padding-left: 0.2em;
        }

            .portlet-header .ui-icon {
                float: right;
            }

        .portlet-content {
            padding: 0.4em;
        }

        .ui-sortable-placeholder {
            border: 1px dotted black;
            visibility: visible !important;
            height: 50px !important;
        }

            .ui-sortable-placeholder * {
                visibility: hidden;
            }
        .specialStyle {
             
    
    float: left;
    height: 25px;
    margin-bottom: 10px;
    padding-left: 5px;
    width: 170px;
        }
    </style>   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="firstDiv" style="display:none;">
     <div style="width:100%;height:100%;"  id="loginForm">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div id="divLoginHeader"  class="modal-header">
                      Login
                        <button class="close" data-dismiss="modal" onclick="javascript:$.unblockUI();" >&times</button>
                    </div>
                    <div class="modal-body">
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                            <input type="text" id="txtUsername"  style="width:88%" class="form-control" placeholder="Username" />
                        </div>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-cog"></span></span>
                            <input type="text" id="txtPassword" style="width:88%" class="form-control" placeholder="Password" />
                        </div>

                        <div id="divLoginError" runat="server" style="color: red; display: none; padding-top: 5px; margin-left: 10px;">User name and password does not match</div>
                    </div>
                    <div class="modal-footer" style="margin-top: 0px">
                        <button type="button" class="btn" id="btnLogin" onclick="ValidateUser()">Go</button>
                    </div>

                </div>
            </div>
        </div>   
        </div>
    <h2 id="partial-title">...</h2>

    <div style="display:block" id="secDiv">
    <ul id="partial-navigation">
        <li><a id="partial-desc" href="javascript:void(0)">Shows the overview of all sections</a></li>
    </ul>
    <div id="partial-content">
       <%--control goes here--%>  
           
      </div>       
    </div> 
    
       
    <script type="text/javascript">
        /////////////////////////////////////////////////////// Load Control ////////////////////////////////////
        $(document).ready(function () {

            $('#main-navigation ul li:eq(0) a').click();
        });
        window.onload(function () {

            $('#main-navigation ul li:eq(0) a').click();
        });

        function LoadControl(ctrlName) {
            
            $.blockUI();
            ctrlPath = "~/Admin/AdminControls/"+ctrlName+".ascx";
            divID = "partial-content";
            serviceURL = "AdminHome.aspx/GetControlHtml";
            loadControlDynamically(serviceURL, ctrlPath, divID);
        }
            function loadControlDynamically(serviceURL, controlLocation, divID) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    url: serviceURL,
                    async: false,
                    data: "{'controlLocation':'" + controlLocation + "'}",
                    success: function (msg) {
                    
                        $('#' + divID).html(msg.d);
                    
                        $.unblockUI();

                    },
                    error: function () {
                        alert("Load control error! Please contact support team");
                    
                        $.unblockUI();
                    }
                });
            }



            //////////////////////////////////////////////////////////////////////////////////
            //--------------------------- dashboard ------------------------------------
            $(function () {
                $(".column").sortable({
                    connectWith: ".column"
                });
            
                $(".portlet").addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
                .find(".portlet-header")
                .addClass("ui-widget-header ui-corner-all")
                .prepend("<span class='ui-icon ui-icon-minusthick'></span>")
                .end()
                .find(".portlet-content");

                $(".portlet-header .ui-icon").click(function () {
                    $(this).toggleClass("ui-icon-minusthick").toggleClass("ui-icon-plusthick");
                    $(this).parents(".portlet:first").find(".portlet-content").toggle();
                });

                $(".column").disableSelection();
            });

            function setContent(link) {
            
                $('#partial-content > div').siblings().removeClass('tempCurrent');
                $('#partial-content > div[id="' + $(link).text() + '"]').addClass('tempCurrent');
                $('#partial-content > div[class~="tempCurrent"]').siblings().hide();
                $('#partial-content > div[class~="tempCurrent"]').fadeIn();
                $('#partial-desc').text($(link).data("desc")).fadeIn();
                $('#partial-title').text($(link).text()).fadeIn();

                //set active class
                $('.sf-vertical > li > a').removeClass('active-tab');
                $('.sf-vertical > li > a').filter(function () {
                    return $(this).text() == $(link).text()
                }).addClass('active-tab');
                //set active class end here   
            
                if ($(link).text() == "Project-Locations") {
                
                    var script = document.createElement('script');
                    script.src = "https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&callback=initialize";
                    document.body.appendChild(script);
                }

            }
            function manageSection(buttonValuesToShow, divIDToShow, containerID, caller) {

                var buttonValuesArray = buttonValuesToShow.split(',');
                $(caller).siblings('input[type="button"]').hide();
                $(caller).css('display', 'none');
                for (var i = 0; i < buttonValuesArray.length; i++) {

                    $(caller).siblings('input[type="button"][value="' + buttonValuesArray[i] + '"]').fadeIn();
                }

                $('div[id="' + containerID + '"] > div').siblings().hide();
                $('div[id="' + containerID + '"] > div[id="' + divIDToShow + '"]').fadeIn();
            }

            //--------------------------------- dashboard end ---------------------------------
      
                     
    </script>
   
    <script>
     

        function logout() {
            $.ajax({
                type: "POST",
                url: "adminhome.aspx/logout",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    data = data.d;
                    if (data == "1") {                        
                        window.location = "AdminLogin.aspx";
                        
                    }
                    else {
                        
                    }
                },
                error: function () {

                }
            });

        }

</script>    


    


</asp:Content>

