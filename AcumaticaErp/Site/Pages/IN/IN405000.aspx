<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="IN405000.aspx.cs"
    Inherits="Page_IN405000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PageLoadBehavior="PopulateSavedValues" PrimaryView="Filter"
        TypeName="PX.Objects.IN.InventoryTranHistEnq">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="ViewItem" DependOnGrid="grid" Visible="false" />
            <px:PXDSCallbackCommand Name="ViewSummary" DependOnGrid="grid" Visible="false" />
            <px:PXDSCallbackCommand Name="ViewAllocDet" DependOnGrid="grid" Visible="false" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Caption="Selection" CaptionAlign="Justify"
        DataMember="Filter" DefaultControlID="edInventoryID">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
            <px:PXSelector CommitChanges="True" ID="edInventoryID" runat="server" DataField="InventoryID" AllowEdit="true">
                <GridProperties>
                    <PagerSettings Mode="NextPrevFirstLast" />
                </GridProperties>
            </px:PXSelector>
            <px:PXSegmentMask CommitChanges="True" ID="edSiteID" runat="server" DataField="SiteID" />
            <px:PXSegmentMask CommitChanges="True" ID="edLocationID" runat="server" AutoRefresh="True" DataField="LocationID" />
            <px:PXTextEdit CommitChanges="True" ID="edLotSerialNbr" runat="server" AllowNull="False" DataField="LotSerialNbr" />
            <px:PXSegmentMask CommitChanges="True" ID="edSubItemCD" runat="server" DataField="SubItemCD" AutoRefresh="true" />
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
            <px:PXDateTimeEdit CommitChanges="True" ID="edStartDate" runat="server" DataField="StartDate" DisplayFormat="d" />
            <px:PXDateTimeEdit CommitChanges="True" ID="edEndDate" runat="server" DataField="EndDate" DisplayFormat="d" />
            <px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkSummaryByDay" runat="server" DataField="SummaryByDay" />
            <px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkIncludeUnreleased" runat="server" DataField="IncludeUnreleased" />
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="144px" Style="z-index: 100" Width="100%" AdjustPageSize="Auto"
        AllowPaging="True" AllowSearch="True" BatchUpdate="True" Caption="Transaction Details" SkinID="Inquire" RestrictFields="True">
        <Levels>
            <px:PXGridLevel DataMember="ResultRecords">
                <RowTemplate>
                    <px:PXLayoutRule ID="PXLayoutRule3" runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                    <px:PXSelector ID="edDocRefNbr" runat="server" AllowEdit="True" DataField="DocRefNbr" />
                    <px:PXSelector ID="edINTran__SOOrderType" runat="server" DataField="INTran__SOOrderType" />
                    <px:PXSelector ID="edINTran__SOOrderNbr" runat="server" DataField="INTran__SOOrderNbr" AllowEdit="true" />
                    <px:PXSelector ID="edINTran__POReceiptNbr" runat="server" DataField="INTran__POReceiptNbr" AllowEdit="true" />
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn DataField="GridLineNbr" />
                    <px:PXGridColumn DataField="InventoryID" DisplayFormat="&gt;CCCCC-CCCCCCCCCCCCCCC" />
                    <px:PXGridColumn DataField="TranDate" Width="90px" />
                    <px:PXGridColumn DataField="TranType" />
                    <px:PXGridColumn DataField="DocRefNbr">
                        <NavigateParams>
                            <px:PXControlParam ControlID="grid" Direction="Output" Name="DocType" PropertyName="DataValues[&quot;DocType&quot;]" Type="String" />
                            <px:PXControlParam ControlID="grid" Direction="Output" Name="RefNbr" PropertyName="DataValues[&quot;DocRefNbr&quot;]" Type="String" />
                        </NavigateParams>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="DocType" Visible="false" />
                    <px:PXGridColumn DataField="SubItemID" DisplayFormat="&gt;AA-A" />
                    <px:PXGridColumn DataField="SiteID" DisplayFormat="&gt;AAAAAAAAAA" />
                    <px:PXGridColumn DataField="LocationID" AllowShowHide="Server" DisplayFormat="&gt;AAAAAAAAAA" />
                    <px:PXGridColumn DataField="LotSerialNbr" AllowShowHide="Server" DisplayFormat="&gt;aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" Width="120px" />
                    <px:PXGridColumn DataField="FinPerNbr" DisplayFormat="##-####" />
                    <px:PXGridColumn DataField="TranPerNbr" DisplayFormat="##-####" />
                    <px:PXGridColumn DataField="Released" TextAlign="Center" Type="CheckBox" />
                    <px:PXGridColumn AllowNull="False" DataField="BegQty" TextAlign="Right" Width="100px" />
                    <px:PXGridColumn AllowNull="False" DataField="QtyIn" TextAlign="Right" Width="100px" />
                    <px:PXGridColumn AllowNull="False" DataField="QtyOut" TextAlign="Right" Width="100px" />
                    <px:PXGridColumn AllowNull="False" DataField="EndQty" TextAlign="Right" Width="100px" />
                    <px:PXGridColumn AllowNull="False" DataField="INTran__UnitCost" TextAlign="Right" Width="100px" />
                    <px:PXGridColumn DataField="INTran__SOOrderType" DisplayFormat="&gt;aa" Label="SO Order Type" />
                    <px:PXGridColumn DataField="INTran__SOOrderNbr" DisplayFormat="&gt;CCCCCCCCCCCCCCC" Label="SO Order Nbr.">
                        <NavigateParams>
                            <px:PXControlParam Name="OrderType" ControlID="grid" PropertyName="DataValues[&quot;INTran__SOOrderType&quot;]" />
                            <px:PXControlParam Name="OrderNbr" ControlID="grid" PropertyName="DataValues[&quot;INTran__SOOrderNbr&quot;]" />
                        </NavigateParams>
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="INTran__POReceiptNbr" DisplayFormat="&gt;CCCCCCCCCCCCCCC" Label="PO Receipt Nbr." />
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <ActionBar DefaultAction="allocDet">
            <CustomItems>
                <px:PXToolBarButton Key="item" Text="View Inventory Item">
                    <AutoCallBack Target="ds" Command="ViewItem" />
                </px:PXToolBarButton>
                <px:PXToolBarButton Key="summary" Text="View Summary">
                    <AutoCallBack Target="ds" Command="ViewSummary" />
                </px:PXToolBarButton>
                <px:PXToolBarButton Key="allocDet" Text="View Allocation Details">
                    <AutoCallBack Target="ds" Command="ViewAllocDet" />
                </px:PXToolBarButton>
            </CustomItems>
        </ActionBar>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXGrid>
</asp:Content>
