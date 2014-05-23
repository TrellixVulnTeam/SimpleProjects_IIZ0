<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="SO509000.aspx.cs"
    Inherits="Page_SO509000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.SO.SOCreate" PrimaryView="Filter">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Process" CommitChanges="true" StartNewGroup="True" />			
            <px:PXDSCallbackCommand DependOnGrid="grid" Name="InventorySummary" Visible="false" CommitChanges="true" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" DataMember="Filter" Width="100%" Caption="Selection"
        DefaultControlID="edPurchDate">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
            <px:PXDateTimeEdit CommitChanges="True" runat="server" DataField="PurchDate" ID="edPurchDate" />
            <px:PXLayoutRule runat="server" Merge="True" />
            <px:PXSelector CommitChanges="True" ID="edOwnerID" runat="server" DataField="OwnerID" />
            <px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkMyOwner" runat="server" Checked="True" DataField="MyOwner" />
            <px:PXLayoutRule runat="server" Merge="False" />
            <px:PXLayoutRule runat="server" Merge="True" />
            <px:PXSelector CommitChanges="True" ID="edWorkGroupID" runat="server" DataField="WorkGroupID" />
            <px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkMyWorkGroup" runat="server" DataField="MyWorkGroup" />
            <px:PXLayoutRule runat="server" Merge="False" />
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
            <px:PXSelector CommitChanges="True" ID="edItemClassID" runat="server" DataField="ItemClassID" />
            <px:PXSegmentMask CommitChanges="True" runat="server" DataField="SiteID" ID="edSiteID" />
            <px:PXSegmentMask CommitChanges="True" ID="edInventoryID" runat="server" DataField="InventoryID" AllowEdit="True">
                <GridProperties>
                    <PagerSettings Mode="NextPrevFirstLast" />
                </GridProperties>
            </px:PXSegmentMask>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Height="150px" SkinID="Details" Caption="Details">
        <Levels>
            <px:PXGridLevel DataMember="FixedDemand">
                <RowTemplate>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                    <px:PXSegmentMask ID="edReplenishmentSourceSiteID" runat="server" DataField="ReplenishmentSourceSiteID" />
                    <px:PXSegmentMask ID="edVendorLocationID" runat="server" AutoRefresh="True" DataField="VendorLocationID">
                        <Parameters>
                            <px:PXControlParam ControlID="grid" Name="SOFixedDemand.vendorID" PropertyName="DataValues[&quot;VendorID&quot;]" Type="String" />
                        </Parameters>
                    </px:PXSegmentMask>
                    <px:PXDateTimeEdit ID="edPlanDate" runat="server" DataField="PlanDate" Enabled="False" />
                    <px:PXSegmentMask ID="edVendorID" runat="server" DataField="VendorID" Enabled="False" />
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn AllowNull="False" DataField="Selected" TextAlign="Center" Type="CheckBox" AllowCheckAll="true" />
                    <px:PXGridColumn AllowUpdate="False" DataField="PlanType_INPlanType_descr" Width="156px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="InventoryID" DisplayFormat="&gt;AAA-&gt;CCC-&gt;AA" Width="81px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="InventoryID_InventoryItem_descr" Width="108px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="SubItemID" DisplayFormat="&gt;AA-A" Width="65px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="SiteID" DisplayFormat="&gt;AAAAAAAAAA" Width="81px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="ReplenishmentSourceSiteID" DisplayFormat="&gt;AAAAAAAAAA" Label="Replenishment Warehouse"
                        Width="120px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="UOM" Width="81px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="OrderQty" TextAlign="Right" Width="81px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="PlanDate" Width="90px" />
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
		<ActionBar>
            <CustomItems>                             
                <px:PXToolBarButton Text="Inventory Summary">
                    <AutoCallBack Command="InventorySummary" Target="ds" />
                </px:PXToolBarButton>
            </CustomItems>
        </ActionBar>
    </px:PXGrid>
</asp:Content>
