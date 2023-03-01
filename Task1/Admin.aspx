<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Task1.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container lookup-grid">
        <div class="lookup-container">
            <h3 class="lookup-title">Segment</h3>
            <asp:DropDownList ID="ProfileTypeList" CssClass="type-dropdown" runat="server"></asp:DropDownList>
            <asp:Button ID="Edit" CssClass="btn btn-primary edit-btn" runat="server" Text="Edit" />
            <asp:Button ID="Delete" CssClass="btn btn-danger" runat="server" Text="Delete" />
        </div>
    </div>
</asp:Content>
 