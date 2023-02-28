<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerDetails.aspx.cs" Inherits="Task1.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row jumbotron">
    <h1><%= Name %> <span class="badge badge-light"><%= Gender %></span></h1>
    <div class="cus-info-row">
        <div class="cus-info-title">Civil ID: </div>
        <div><%= CivilId %></div>
    </div>
        <div class="cus-info-row">
        <div class="cus-info-title">Phone Number: </div>
        <div><%= PhoneNumber %></div>
    </div>
        <div class="cus-info-row">
        <div class="cus-info-title">Address: </div>
        <div><%= Address %></div>
    </div>
    </div>
    

    <div class="row">
        <div class="col-12">
            <div class="table-responsive"></div>
            <asp:GridView ID="CustomerDetail" Width="100%" CssClass="table table-striped table-bordered" runat="server" AutoGenerateColumns="false">
                <Columns>							
                <asp:BoundField DataField="account_number" HeaderText="Account Number" />
                <asp:BoundField DataField="account_type" HeaderText="Account Type" />
                <asp:BoundField DataField="card_number" HeaderText="Card Number" />
                <asp:BoundField DataField="card_type" HeaderText="Card Type" />
                <asp:BoundField DataField="issue_date" HeaderText="Card Issue Date" />
                <asp:BoundField DataField="exp_date" HeaderText="Card Expiry Date" />
                <asp:BoundField DataField="balance" HeaderText="Balance" />
                <asp:BoundField DataField="cvv" HeaderText="CVV" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
