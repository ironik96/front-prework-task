<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Task1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <div class="row">
        <div class="col-12 search-row">
            <asp:TextBox CssClass="form-control" placeholder="search by civil id" ID="inputTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="submitButton" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="SearchButton_Click" />
        </div>
     
    </div>
    <div class="row">
        
        <div class="col-12">
            <div class="table-responsive"></div>
            <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered" runat="server" OnDataBound="DetailsButton_DataBound" AutoGenerateColumns="True"></asp:GridView>
        </div>
    </div>
    <div class="row text-center">
        <asp:LinkButton ID="DetailsLink" CssClass="btn btn-primary" runat="server" Visible="false" OnClick="DetailsLink_Click">Customer Details</asp:LinkButton>
    </div>

</asp:Content>
