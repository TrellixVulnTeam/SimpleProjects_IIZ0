<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FA506000.aspx.cs"
    Inherits="Page_FA506000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.FA.SplitProcess" PrimaryView="Filter">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="ViewAsset" DependOnGrid="grid" Visible="False" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Filter" NoteField=""
        Caption="Options">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
            <px:PXSelector CommitChanges="True" ID="edAssetID" runat="server" DataField="AssetID" DataMember="_FixedAsset_" />
            <px:PXNumberEdit ID="edCost" runat="server" DataField="Cost" Enabled="false" />
            <px:PXNumberEdit ID="edQty" runat="server" DataField="Qty" Enabled="false" />
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
            <px:PXDateTimeEdit CommitChanges="True" ID="edSplitDate" runat="server" DataField="SplitDate" />
            <px:PXSelector ID="edSplitPeriodID" runat="server" DataField="SplitPeriodID" />
            <px:PXCheckBox ID="chkDeprBeforeSplit" runat="server" DataField="DeprBeforeSplit" />
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Details" Caption="Split Assets"
        AllowFilter="False" CaptionVisible="True">
        <Levels>
            <px:PXGridLevel DataMember="Splits">
                <Columns>
                    <px:PXGridColumn AllowNull="False" DataField="Cost" Label="Cost" TextAlign="Right" Width="100px" AutoCallBack="True" />
                    <px:PXGridColumn AllowNull="False" DataField="Qty" Label="Quantity" TextAlign="Right" Width="100px" AutoCallBack="True" />
                    <px:PXGridColumn AllowNull="False" DataField="Ratio" Label="Ratio" TextAlign="Right" Width="100px" AutoCallBack="True" />
                    <px:PXGridColumn DataField="AssetCD" Label="Asset ID" Width="100px" LinkCommand="ViewAsset" />
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXGrid>
</asp:Content>
