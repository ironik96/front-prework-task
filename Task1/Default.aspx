<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Task1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-12 search-row">
            <asp:TextBox CssClass="form-control" placeholder="search by civil id" ID="inputTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="submitButton" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="SearchButton_Click" />
        </div>

    </div>
    <div class="row">
        <div class="col-12 alert alert-success alert-dismissable" role="alert" style="display:<%=displayAlert%>">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">x</button>
            <asp:Label ID="SuccessMessage" Text="" runat="server" />
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <div class="table-responsive">
                <asp:GridView ID="CustomersGrid"
                    CssClass="table table-striped table-bordered"
                    runat="server"
                    AutoGenerateColumns="false"
                    DataKeyNames="civil_id"
                    OnRowEditing="CustomersGrid_RowEditing"
                    OnRowCancelingEdit="CustomersGrid_RowCancelingEdit"
                    OnRowDataBound="CustomersGrid_RowDataBound"
                    OnRowUpdating="CustomersGrid_RowUpdating"
                    OnRowDeleting="CustomersGrid_RowDeleting">
                    <Columns>
                        <%-- Controls --%>
                        <asp:CommandField
                            ShowEditButton="true"
                            EditText="Edit"
                            ShowDeleteButton="true"
                            DeleteText="Delete"
                            HeaderText="Modify" />
                        <%-- Civil Id --%>
                        <asp:TemplateField HeaderText="Civil Id">
                            <ItemTemplate>
                                <asp:Label ID="CivilIdLabel" Text='<%#Eval("civil_id")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- Customer Name --%>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="Label2" Text='<%#Eval("customer_name")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtName" Text='<%#Eval("customer_name")%>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%-- Gender --%>
                        <asp:TemplateField HeaderText="Gender">
                            <ItemTemplate>
                                <asp:Label ID="Label3" Text='<%#Eval("genderString")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="GenderList" runat="server">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%-- Phone Number --%>
                        <asp:TemplateField HeaderText="Phone Number">
                            <ItemTemplate>
                                <asp:Label ID="Label4" Text='<%#Eval("phone_number")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtPhone" Text='<%#Eval("phone_number")%>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%-- Area --%>
                        <asp:TemplateField HeaderText="Area">
                            <ItemTemplate>
                                <asp:Label ID="Label5" Text='<%#Eval("area")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtArea" Text='<%#Eval("area")%>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%-- Block --%>
                        <asp:TemplateField HeaderText="Block">
                            <ItemTemplate>
                                <asp:Label ID="Label6" Text='<%#Eval("block_number")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtBlockNumber" Text='<%#Eval("block_number")%>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%-- Street --%>
                        <asp:TemplateField HeaderText="Street">
                            <ItemTemplate>
                                <asp:Label ID="Label7" Text='<%#Eval("street")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtStreet" Text='<%#Eval("street")%>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%-- House --%>
                        <asp:TemplateField HeaderText="House">
                            <ItemTemplate>
                                <asp:Label ID="Label8" Text='<%#Eval("house")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtHouse" Text='<%#Eval("house")%>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%-- Segment --%>
                        <asp:TemplateField HeaderText="Segment">
                            <ItemTemplate>
                                <asp:Label ID="Label9" Text='<%#Eval("profile_type_name")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="SegmentList" runat="server">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row text-center">
        <asp:LinkButton ID="DetailsLink" CssClass="btn btn-primary" runat="server" Visible="false" OnClick="DetailsLink_Click">Customer Details</asp:LinkButton>
    </div>

</asp:Content>
