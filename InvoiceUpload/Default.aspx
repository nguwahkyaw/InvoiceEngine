<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InvoiceUpload._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function checkFileSize() {

            var maxFileSize = 1024; 
            const fi = document.getElementById("MainContent_FileUpload1");

            if (fi.val == '') {
                return false;
            }
            // Check if any file is selected.
            if (fi.files.length > 0) {
                {
                    const fsize = fi.files.item(0).size;
                    const file = Math.round(fsize / 1024);
                    if (file > maxFileSize) {
                        alert(
                            "File too Big, please select a file not larger than 1MB");
                    }
                    else {                        
                        document.getElementById("MainContent_button_fileUpload").disabled = false;
                        return true;
                    }

                }
            }

            else {
                return false;
            }
        }
    </script>
    <div class="row">
        <div class="col-md-4">
            <asp:FileUpload ID="FileUpload1" runat="server" Style="margin: 20px 0 0 10px" />
            <asp:Label ID="lbl_allow_format" runat="server" Text="Allowed file type(csv,xml)" />  
            <asp:CustomValidator ID="customValidatorUpload" runat="server" ErrorMessage="" ControlToValidate="FileUpload1" ClientValidationFunction="checkFileSize();" />
           
        </div>
        <div class="col-md-4">
             <asp:Button ID="button_fileUpload" Text="Upload" runat="server" OnClick="Upload" Style="margin-top: 20px" Enabled="false" />
        </div>

    </div>

</asp:Content>
