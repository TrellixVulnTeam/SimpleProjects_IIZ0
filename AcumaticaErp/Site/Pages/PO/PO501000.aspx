<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="PO501000.aspx.cs"
    Inherits="Page_PO501000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" Width="100%" runat="server" Visible="True" PrimaryView="Orders" TypeName="PX.Objects.PO.POReleaseReceipt">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Cancel" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Process" StartNewGroup="True" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="ProcessAll" />
            <px:PXDSCallbackCommand DependOnGrid="grid" Name="viewDocument" Visible="False" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100; left: 0px; top: 0px;" AllowPaging="True"
        AllowSearch="true" Caption="PO Receipts" DataSourceID="ds" BatchUpdate="True" AdjustPageSize="Auto" SkinID="Inquire">
        <Levels>
            <px:PXGridLevel DataMember="Orders">
                <RowTemplate>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                    <px:PXDropDown ID="edReceiptType" runat="server" AllowNull="False" DataField="ReceiptType" Enabled="False" />
                    <px:PXSelector ID="edReceiptNbr" runat="server" AllowEdit="True" DataField="ReceiptNbr" Enabled="False" />
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn AllowNull="False" DataField="Selected" TextAlign="Center" Type="CheckBox" Width="60px" AllowCheckAll="True"
                        AllowMove="False" AllowSort="False" />
                    <px:PXGridColumn AllowUpdate="False" DataField="ReceiptNbr" />
                    <px:PXGridColumn AllowUpdate="False" DataField="ReceiptType" />
                    <px:PXGridColumn AllowUpdate="False" DataField="VendorID" DisplayFormat="&gt;AAAAAAAAAA" />
                    <px:PXGridColumn AllowUpdate="False" DataField="VendorID_Vendor_acctName" Width="240px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="VendorLocationID" DisplayFormat="&gt;AAAAAA" />
                    <px:PXGridColumn AllowUpdate="False" DataField="ReceiptDate" Width="90px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="CuryID" DisplayFormat="&gt;LLLLL" />
                    <px:PXGridColumn AllowNull="False" AllowUpdate="False" DataField="CuryOrderTotal" TextAlign="Right" Width="100px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="WorkgroupID" />
                    <px:PXGridColumn AllowUpdate="False" DataField="OwnerID" />
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="200" />
        <ActionBar>
            <CustomItems>
                <px:PXToolBarButton Text="View Document">
                    <AutoCallBack Command="viewDocument" Target="ds">
                        <Behavior CommitChanges="True"></Behavior>
                    </AutoCallBack>
                </px:PXToolBarButton>
            </CustomItems>
        </ActionBar>
        <AutoSize Enabled="True" Container="Window" MinHeight="200"></AutoSize>
    </px:PXGrid>
</asp:Content>
