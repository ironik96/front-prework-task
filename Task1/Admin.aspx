<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Task1.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container lookup-list">
        <div class="lookup-item-container">
            <h3 class="lookup-title">Segment</h3>
            <asp:DropDownList ID="ProfileTypeList" CssClass="type-dropdown" runat="server"></asp:DropDownList>
            <div class="spacer"></div>
            <button id="segment-edit" type="button" class="btn btn-default" data-toggle="modal" data-target="#myModal">Edit</button>
            <asp:Button ID="Delete" CssClass="btn btn-danger" runat="server" Text="Delete" />
        </div>
    </div>



    <!-- Modal -->
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-dialog-centered">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Segment</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox CssClass="form-control" Style="max-width: 100%" placeholder="New Segment Name" ID="SegmentInputBox" runat="server"></asp:TextBox>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="UpdateSegment" CssClass="btn btn-primary" runat="server" Text="Update" data-dismiss="modal" UseSubmitBehavior="false" OnClick="UpdateSegment_Click"/>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
