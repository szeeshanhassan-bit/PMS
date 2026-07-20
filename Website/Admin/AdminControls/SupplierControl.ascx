<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplierControl.ascx.cs" Inherits="Admin_AdminControls_Supplier" %>

        <div id="Supplier" class="innerContainer container" style="display: none">
            <div class="lead" data-name="buttonsDiv-Suppliers" style="width: 100%; height: 60px;">
               <input  class="btn"    type="button" title="Add Supplier" value="Add Supplier" onclick="AddSupplier()" />
                <input class="btn"   type="button" title="Clear Fields" value="Clear Fields" onclick="clearContentSuppliers('#Supplier')" />
                <input class="btn"   type="button" title="Back" value="Back" style="display: none" onclick="manageSection('Add Supplier,Clear Fields,Show Suppliers', 'Suppliers-Add', 'content-Suppliers', this);" />
                <input class="btn"   type="button" title="ShowSuppliers" value="Show Suppliers" onclick="manageSection('Show Suppliers,Back', 'Suppliers-Show', 'content-Suppliers', this); ShowSuppliers()" />
                <input class="btn"   type="button"  value="Update" style="display: none" onclick="UpdateSupplier(); " />
                <input class="btn"   type="button"  value="Cancel" style="display: none" onclick="manageSection('Add Supplier,Clear Fields,Show Suppliers', 'Suppliers-Add', 'content-Suppliers', this); clearContentSuppliers('#Suppliers');" />
                <input class="btn"   type="button"  value="Edit" style="display: none" onclick="manageSection('Update,Cancel', 'Suppliers-Add', 'content-Suppliers', this);" />
                <hr style="margin-top: 10px; margin-bottom: 20px; float: left; width: 100%" />
            </div>
            <div id="content-Suppliers" >
                <div id="Suppliers-Add" > 
                    <div class="form-group">
                        <label for="txtCategoryID">Name</label>
                        <input type="text" class="form-control" id="txtSupplierName" style="width:50%" placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="txtCategoryID">Address</label>
                        <input type="text" class="form-control" id="txtSupplierAddress" style="width:50%" placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="txtCategoryID">Contact</label>
                        <input type="text" class="form-control" id="txtSupplierContact" style="width:50%" placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="txtCategoryID">ABN</label>
                        <input type="text" class="form-control" id="txtSupplierABN" style="width:50%" placeholder="">
                    </div>                                     
                    <br />
                    <div style="clear:both"></div>                    
                     <input class="btn"  type="button"  value="Save"  onclick="AddSupplier();" />
                    </div>                                        
                    <br />
                <div id="Suppliers-Show" style="display: none;">
                </div>
                </div>

                
            </div> 
        <script>

            //--------------------------------- Suppliers ---------------------------------

            function AddSupplier() {

                var supplier = {
                    supplierName: $('input[id$=txtSupplierName]').val(),
                    supplierAddress: $('input[id$=txtSupplierAddress]').val(),
                    supplierAbn: $('input[id$=txtSupplierABN]').val(),
                    supplierContact: $('input[id$=txtSupplierContact]').val()
                };
                
                $.ajax({
                    type: "POST",
                    url: "/ApiLayer/api/suppliers",
                    data: JSON.stringify(supplier),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        alert("Supplier Saved Successfully");
                        clearContent('#Supplier');
                    },
                    error: ErrorAddSupplier
                });                
            }

            function clearContent(div) {
                var ref_id = div + ' input[type="text"]:visible, textarea:visible, input[type="file"]:visible'
                $(ref_id).val('');
            }
            function clearContentSuppliers(div) {
                var ref_id = div + ' input[type="text"]:visible, textarea:visible, input[type="file"]:visible'
                $(ref_id).val('');
                $('#txtContent').val("");

            }
            function ErrorAddSupplier() {
                alert("Some Error occured in adding Supplier. Please contact Support team");
            }
            function ShowSuppliers() {

                $.ajax({
                    type: "GET",
                    url: "/ApiLayer/api/suppliers",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: successShowSuppliers,
                    error: ErrorShowSuppliers
                });
            }

            function successShowSuppliers(data) {

                $('#Suppliers-Show').html(generateTable(data));                
                SetDataTable('#tblDatatable');
            }
            function generateTable(data) {

                var table = "<table id='tblDatatable' cellspacing ='1px' style='width:100%'>";
                table += "<thead><tr>";

                table += "<th style='width:50px'>Sr.</th>";
                table += "<th>Name</th>";
                table += "<th>Address</th>";
                table += "<th>ABN</th>";
                table += "<th>Contact</th>";
                table += "<th>Edit</th>";
                table += "<th>Delete</th>";

                table += "</tr></thead><tbody>";

                var serial = 0;
                for (var i = 0; i < data.length; i++) {
                    serial++;
                    table += "<tr>";

                    table += "<td>" + serial + "</td>";
                    table += "<td>" + data[i].supplierName + "</td>";
                    table += "<td>" + data[i].supplierAddress + "</td>";
                    table += "<td>" + data[i].supplierAbn + "</td>";
                    table += "<td>" + data[i].supplierContact + "</td>";
                    table += "<td><a style='cursor:pointer;color:red' data-id='" + data[i].supplierId + "' onclick='editSupplier(this)'><img src='../img/img_edit.png' style='width:30px;height:30px;' />  </a> </td>";
                    if (data[i].isActive === true) {
                        table += "<td><a style='cursor:pointer;color:red' data-id='" + data[i].supplierId + "' onclick='deleteSupplier(this)'>Delete</a></td>";
                    }
                    else {
                        table += "<td>Deleted</td>";
                    }

                    table += "</tr>";
                }
                table + "</tbody>";             
                table += "</table>";
                return table;

            }
            function ErrorShowSuppliers() {

                alert("Some Error occured in showing Supplier. Please contact Support team");

            }

            function deleteSupplier(aTag) {
            
                var Supplierid = $(aTag).data('id');
                $.ajax({
                    type: "DELETE",
                    url: "/ApiLayer/api/suppliers/" + Supplierid,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: successdelete_Suppliers,
                    error: Errordelete_Suppliers
                });

            }
            function successdelete_Suppliers() {

                alert("Deleted successfully");
                ShowSuppliers();
            }
            function Errordelete_Suppliers() {

                alert("Error occured in deleting. Please Contact support team");
            }


            function editSupplier(aTag) {
                
                var Supplierid = $(aTag).data('id');
                $.ajax({
                    type: "GET",
                    url: "/ApiLayer/api/suppliers/" + Supplierid,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: successedit_Suppliers,
                    error: function () { alert("Error occured in editing. Please Contact support team"); }
                });

            }
            function successedit_Suppliers(data) {
                var supplier = data;
                if (Array.isArray(data) && data.length > 0) {
                    supplier = data[0];
                }

                if (supplier) {
                    $('div[id=Supplier] div:eq(0) input[type=button]').last().click();
                    $('div[id=Supplier] div:eq(0) input[type=button][value="Update"]').data('id', supplier.supplierId);
                                        
                    $('input[id$=txtSupplierName]').val(supplier.supplierName);
                    $('input[id$=txtSupplierAddress]').val(supplier.supplierAddress);
                    $('input[id$=txtSupplierABN]').val(supplier.supplierAbn);
                    $('input[id$=txtSupplierContact]').val(supplier.supplierContact);                                        
                }
            }
            
            function UpdateSupplier() {
                var Supplierid = $('div[id=Supplier] div:eq(0) input[type=button][value="Update"]').data('id');
                if (Supplierid != undefined) {                                        

                    var supplier = {
                        supplierName: $('input[id$=txtSupplierName]').val(),
                        supplierAddress: $('input[id$=txtSupplierAddress]').val(),
                        supplierAbn: $('input[id$=txtSupplierABN]').val(),
                        supplierContact: $('input[id$=txtSupplierContact]').val()
                    };

                    $.ajax({
                        type: "PUT",
                        url: "/ApiLayer/api/suppliers/" + Supplierid,
                        data: JSON.stringify(supplier),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            alert("Supplier Updated Successfully");
                            $('div[id=Supplier] div:eq(0) input[type=button][value="Cancel"]').click();
                            clearContentSuppliers('#Supplier');
                        },
                        error: function () { alert("Error occured in Updating. Please Contact support team"); }
                    });
                }
            }
   
            function SetDataTable(tbl) {
                var totalcolumns = $(tbl + " tr:nth-child(1) th").length;
                //if ($("#tblFellows tr").length < 2)
                //   return;
                var columns = [];
                columns.push({ "bSortable": false });
                for (var i = 1; i < totalcolumns; i++) {

                    columns.push({ "bSortable": true });
                }
                oTable = $(tbl).dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers",
                    "bSearchable": true,
                    "aoColumns": columns
                });

            }
            //--------------------------------- Suppliers end ---------------------------------
        </script>
        