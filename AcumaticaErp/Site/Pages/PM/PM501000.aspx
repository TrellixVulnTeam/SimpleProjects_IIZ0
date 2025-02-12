<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="PM501000.aspx.cs"
    Inherits="Page_PM501000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" Width="100%" runat="server" Visible="true" PrimaryView="Items" TypeName="PX.Objects.PM.RegisterRelease">
        <CallbackCommands>
            <px:PXDSCallbackCommand CommitChanges="true" Name="Process" StartNewGroup="true" />
            <px:PXDSCallbackCommand CommitChanges="true" Name="ProcessAll" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100" AllowPaging="True" AllowSearch="true"
        AdjustPageSize="Auto" DataSourceID="ds" SkinID="Inquire" Caption="Transactions">
        <Levels>
            <px:PXGridLevel DataMember="Items">
                <RowTemplate>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
                    <px:PXCheckBox ID="chkSelected" runat="server" DataField="Selected" />
                    <px:PXDropDown ID="edModule" runat="server" DataField="Module" Enabled="False" SelectedIndex="4" />
                    <px:PXSelector ID="edRefNbr" runat="server" DataField="RefNbr" Enabled="False" AllowEdit="true" Width="128px"/>
                    <px:PXDateTimeEdit ID="edDate" runat="server" DataField="Date" Enabled="False" Width="108px"/>
                    <px:PXTextEdit ID="edDescription" runat="server" DataField="Description" Enabled="False" Width="308px"/>
                    <px:PXCheckBox ID="chkReleased" runat="server" DataField="Released" Enabled="False" />
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="Selected" Label="Selected" TextAlign="Center" Type="CheckBox" AllowCheckAll="true" Width="20px" />
                    <px:PXGridColumn DataField="RefNbr" Label="Ref Number" Width="108px" />
                    <px:PXGridColumn DataField="Date" Label="Transaction Date" Width="108px" />
                    <px:PXGridColumn DataField="Description" Label="Description" Width="500px" />
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="200" />
    </px:PXGrid>
</asp:Content>
