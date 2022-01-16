<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InvoiceUpload._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <div class="row">
        <div class="col-md-4">
            <asp:FileUpload ID="FileUpload1" runat="server" Style="margin: 20px 0 0 10px" />
            <asp:Label ID="lbl_allow_format" runat="server" Text="Allowed file type(csv,xml)" />          
           
        </div>
        <div class="col-md-4">
             <asp:Button ID="button_fileUpload" Text="Upload" runat="server" OnClick="Upload" Style="margin-top: 20px" />
        </div>

    </div>

</asp:Content>
