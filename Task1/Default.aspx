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
            <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered" runat="server" OnDataBound="DetailsButton_DataBound" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="civil_id" HeaderText="Civil Id" />
                    <asp:BoundField DataField="customer_name" HeaderText="Name" />
                    <asp:BoundField DataField="genderString" HeaderText="Gender" />
                    <asp:BoundField DataField="phone_number" HeaderText="Phone Number" />
                    <asp:BoundField DataField="area" HeaderText="Area" />
                    <asp:BoundField DataField="block_number" HeaderText="Block" />
                    <asp:BoundField DataField="street" HeaderText="Street" />
                    <asp:BoundField DataField="house" HeaderText="House" />
                    <asp:BoundField DataField="profile_type_name" HeaderText="Segment" />
                </Columns>

            </asp:GridView>
        </div>
    </div>
    <div class="row text-center">
        <asp:LinkButton ID="DetailsLink" CssClass="btn btn-primary" runat="server" Visible="false" OnClick="DetailsLink_Click">Customer Details</asp:LinkButton>
    </div>

</asp:Content>
