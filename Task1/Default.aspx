<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Task1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-12 search-row">
            <asp:TextBox CssClass="form-control" placeholder="search by civil id" ID="inputTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="submitButton" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="SearchButton_Click" />
        </div>

    </div>
    <div class="row">
        <div class="col-12 alert alert-success alert-dismissable" role="alert" style="display: <%=displayAlert%>">
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
                    ShowFooter="true"
                    OnRowEditing="CustomersGrid_RowEditing"
                    OnRowCancelingEdit="CustomersGrid_RowCancelingEdit"
                    OnRowDataBound="CustomersGrid_RowDataBound"
                    OnRowUpdating="CustomersGrid_RowUpdating"
                    OnRowDeleting="CustomersGrid_RowDeleting"
                    OnRowCommand="CustomersGrid_RowCommand"
                    OnSelectedIndexChanged="CustomersGrid_SelectedIndexChanged"
                    >
                    <Columns>
                        <%-- Edit Controls --%>
                        <asp:CommandField
                            ShowEditButton="true"
                            EditText="Edit"
                            ControlStyle-CssClass="btn btn-default"
                            ItemStyle-Wrap="false"
                            ItemStyle-CssClass="text-center" />
                        <%-- Civil Id --%>
                        <asp:TemplateField HeaderText="Civil Id" HeaderStyle-Width="100" ItemStyle-Width="100">
                            <ItemTemplate>
                                <asp:Label ID="CivilIdLabel" Text='<%#Eval("civil_id")%>' runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtCivilId" CssClass="form-control" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%-- Customer Name --%>
                        <asp:TemplateField HeaderText="Name" HeaderStyle-Width="120" ItemStyle-Width="120">
                            <ItemTemplate>
                                <asp:Label ID="Label2" Text='<%#Eval("customer_name")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtName" CssClass="form-control" Text='<%#Eval("customer_name")%>' runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtNameInsert" CssClass="form-control" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%-- Gender --%>
                        <asp:TemplateField HeaderText="Gender" HeaderStyle-Width="80" ItemStyle-Width="80">
                            <ItemTemplate>
                                <asp:Label ID="Label3" Text='<%#Eval("genderString")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList CssClass="gridview-dropdown" ID="GenderList" runat="server">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList CssClass="gridview-dropdown" ID="GenderListInsert" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%-- Phone Number --%>
                        <asp:TemplateField HeaderText="Phone Number" HeaderStyle-Width="140" ItemStyle-Width="140">
                            <ItemTemplate>
                                <asp:Label ID="Label4" Text='<%#Eval("phone_number")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtPhone" CssClass="form-control" Text='<%#Eval("phone_number")%>' runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtPhoneInsert" CssClass="form-control" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%-- Area --%>
                        <asp:TemplateField HeaderText="Area" HeaderStyle-Width="60" ItemStyle-Width="60">
                            <ItemTemplate>
                                <asp:Label ID="Label5" Text='<%#Eval("area")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtArea" CssClass="form-control" Text='<%#Eval("area")%>' runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtAreaInsert" CssClass="form-control" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%-- Block --%>
                        <asp:TemplateField HeaderText="Block" HeaderStyle-Width="60" ItemStyle-Width="60">
                            <ItemTemplate>
                                <asp:Label ID="Label6" Text='<%#Eval("block_number")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtBlockNumber" CssClass="form-control" Text='<%#Eval("block_number")%>' runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtBlockNumberInsert" CssClass="form-control" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%-- Street --%>
                        <asp:TemplateField HeaderText="Street" HeaderStyle-Width="100" ItemStyle-Width="100">
                            <ItemTemplate>
                                <asp:Label ID="Label7" Text='<%#Eval("street")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtStreet" CssClass="form-control" Text='<%#Eval("street")%>' runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtStreetInsert" CssClass="form-control" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%-- House --%>
                        <asp:TemplateField HeaderText="House" HeaderStyle-Width="60" ItemStyle-Width="60">
                            <ItemTemplate>
                                <asp:Label ID="Label8" Text='<%#Eval("house")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtHouse" CssClass="form-control" Text='<%#Eval("house")%>' runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TxtHouseInsert" CssClass="form-control" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%-- Segment --%>
                        <asp:TemplateField HeaderText="Segment" HeaderStyle-Width="120" ItemStyle-Width="120">
                            <ItemTemplate>
                                <asp:Label ID="Label9" Text='<%#Eval("profile_type_name")%>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList CssClass="gridview-dropdown" ID="SegmentList" runat="server">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList CssClass="gridview-dropdown" ID="SegmentListInsert" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%-- Controls --%>
                        <asp:TemplateField ItemStyle-CssClass="text-center" FooterStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton CssClass="btn btn-primary" runat="server" CommandName="Select">Details</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton CssClass="btn btn-danger"
                                    Text="Delete"
                                    CommandName="Delete"
                                    runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton CssClass="btn btn-primary" runat="server" CommandName="Insert">Add</asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
