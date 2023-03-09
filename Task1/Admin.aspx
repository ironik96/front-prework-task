<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Task1.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container lookup-list">
        <div class="lookup-item-container">
            <h3 class="lookup-title">Segment</h3>
            <asp:DropDownList ID="ProfileTypeList" CssClass="type-dropdown" runat="server" OnSelectedIndexChanged="List_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <div class="spacer"></div>
            <button id="segment-add" type="button" class="btn btn-primary" data-toggle="modal" data-target="#addModal">Add</button>
            <button id="segment-edit" type="button" class="btn btn-default" data-toggle="modal" data-target="#editModal">Edit</button>
            <button id="segment-delete" type="button" class="btn btn-danger" data-toggle="modal" data-target="#deleteModal">Delete</button>
        </div>
    </div>


    <!-- Add Modal -->
    <div class="modal fade" id="addModal" role="dialog">
        <div class="modal-dialog modal-dialog-centered">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Segment</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox CssClass="form-control" Style="max-width: 100%" placeholder="Segment Name" ID="SegmentInputBox_Add" runat="server"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <asp:Button CssClass="btn btn-primary" runat="server" Text="Add" data-dismiss="modal" UseSubmitBehavior="false" OnClick="AddSegment_Click"/>
                </div>
            </div>

        </div>
    </div>

    <!-- Edit Modal -->
    <div class="modal fade" id="editModal" role="dialog">
        <div class="modal-dialog modal-dialog-centered">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Segment</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox CssClass="form-control" Style="max-width: 100%" placeholder="Segment Name" ID="SegmentInputBox_Edit" runat="server"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <asp:Button CssClass="btn btn-primary" runat="server" Text="Update" data-dismiss="modal" UseSubmitBehavior="false" OnClick="UpdateSegment_Click"/>
                </div>
            </div>

        </div>
    </div>

    <!-- Delete Modal -->
    <div class="modal fade" id="deleteModal" role="dialog">
        <div class="modal-dialog modal-dialog-centered">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Delete Segment?</h4>
                </div>
                
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <asp:Button CssClass="btn btn-danger" runat="server" Text="Delete" data-dismiss="modal" UseSubmitBehavior="false" OnClick="DeleteSegment_Click"/>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
