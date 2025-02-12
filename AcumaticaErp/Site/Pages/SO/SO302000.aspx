<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="SO302000.aspx.cs" Inherits="Page_SO302000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.SO.SOShipmentEntry" PrimaryView="Document">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
            <px:PXDSCallbackCommand Name="SelectSO" Visible="False" RepaintControls="All" />
            <px:PXDSCallbackCommand Name="AddSO" Visible="False" CommitChanges="true" />
            <px:PXDSCallbackCommand Name="AddSOCancel" Visible="False" CommitChanges="true" />
            <px:PXDSCallbackCommand Name="Action" Visible="True" CommitChanges="true" StartNewGroup="true" />
            <px:PXDSCallbackCommand Name="Report" Visible="True" CommitChanges="true" />
            <px:PXDSCallbackCommand Name="Inquiry" Visible="True" CommitChanges="true" />
            <px:PXDSCallbackCommand Name="Hold" Visible="false" CommitChanges="true" />
            <px:PXDSCallbackCommand Name="Flow" Visible="false" CommitChanges="true" />
            <px:PXDSCallbackCommand Name="InventorySummary" Visible="false" CommitChanges="true" DependOnGrid="grid" />
            <px:PXDSCallbackCommand Visible="False" Name="CurrencyView" />
            <px:PXDSCallbackCommand CommitChanges="True" Visible="False" Name="RecalculatePackages" />
			<px:PXDSCallbackCommand CommitChanges="True" Visible="False" Name="LSSOShipLine_generateLotSerial" />
            <px:PXDSCallbackCommand CommitChanges="True" Visible="False" Name="LSSOShipLine_binLotSerial" DependOnGrid="grid" />
            <px:PXDSCallbackCommand Name="NewTask" Visible="False" CommitChanges="True" />
            <px:PXDSCallbackCommand Name="NewEvent" Visible="False" CommitChanges="True" />
            <px:PXDSCallbackCommand Name="NewActivity" Visible="False" CommitChanges="True" />
            <px:PXDSCallbackCommand Name="NewMailActivity" Visible="False" CommitChanges="True" PopupCommand="Cancel" PopupCommandTarget="ds" />
            <px:PXDSCallbackCommand StartNewGroup="True" Name="ValidateAddresses" Visible="False" CommitChanges="True" />
        </CallbackCommands>
        <DataTrees>
            <px:PXTreeDataMember TreeView="_EPCompanyTree_Tree_" TreeKeys="WorkgroupID" />
        </DataTrees>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Document" Caption="Shipment Summary" NoteIndicator="True" FilesIndicator="True" LinkIndicator="true"
        NotifyIndicator="true" EmailingGraph="PX.Objects.CR.CREmailActivityMaint,PX.Objects" ActivityIndicator="true" ActivityField="NoteActivity" DefaultControlID="edShipmentNbr">
        <CallbackCommands>
            <Save PostData="Self" />
        </CallbackCommands>
        <Activity HighlightColor="" SelectedColor="" Width="" Height=""></Activity>
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
            <px:PXSelector ID="edShipmentNbr" runat="server" DataField="ShipmentNbr" AutoRefresh="true" />
            <px:PXDropDown ID="edShipmentType" runat="server" DataField="ShipmentType" />
            <px:PXDropDown ID="edStatus" runat="server" DataField="Status" Enabled="False" />
            <px:PXCheckBox ID="chkHold" runat="server" DataField="Hold">
                <AutoCallBack Command="Hold" Target="ds">
                    <Behavior CommitChanges="true" />
                </AutoCallBack>
            </px:PXCheckBox>
            <px:PXLayoutRule runat="server" Merge="False" />
            <px:PXDropDown ID="edOperation" runat="server" DataField="Operation" />
            <px:PXDateTimeEdit CommitChanges="True" ID="edShipDate" runat="server" DataField="ShipDate" />
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
            <px:PXSegmentMask CommitChanges="True" ID="edCustomerID" runat="server" DataField="CustomerID" AllowAddNew="True" AllowEdit="True" />
            <px:PXSegmentMask CommitChanges="True" ID="edCustomerLocationID" runat="server" AutoRefresh="True" DataField="CustomerLocationID" />
            <px:PXSegmentMask CommitChanges="True" ID="edSiteID" runat="server" DataField="SiteID" />
            <pxa:PXCurrencyRate DataField="CuryID" ID="edCury" runat="server" DataSourceID="ds" RateTypeView="_SOShipment_CurrencyInfo_" DataMember="_Currency_" />
            <px:PXTreeSelector CommitChanges="True" ID="PXTreeSelector1" runat="server" DataField="WorkgroupID" TreeDataMember="_EPCompanyTree_Tree_" TreeDataSourceID="ds" PopulateOnDemand="true" InitialExpandLevel="0"
                ShowRootNode="false">
                <DataBindings>
                    <px:PXTreeItemBinding TextField="Description" ValueField="Description" />
                </DataBindings>
            </px:PXTreeSelector>
            <px:PXSelector ID="edOwnerID" runat="server" DataField="OwnerID" ValueField="EPEmployee__acctCD" />
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="M" />
            <px:PXNumberEdit ID="edShipmentQty" runat="server" DataField="ShipmentQty" Enabled="False" />
            <px:PXNumberEdit CommitChanges="True" ID="edControlQty" runat="server" DataField="ControlQty" />
			<px:PXNumberEdit ID="PXNumberEdit1" runat="server" DataField="ShipmentWeight" Enabled="False" />
			<px:PXNumberEdit ID="PXNumberEdit4" runat="server" DataField="ShipmentVolume" Enabled="False" />
			<px:PXNumberEdit ID="PXNumberEdit3" runat="server" DataField="PackageCount" Enabled="False" />
			<px:PXNumberEdit ID="PXNumberEdit2" runat="server" DataField="PackageWeight" Enabled="False" />
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Height="423px" Style="z-index: 100;" Width="100%" TabIndex="23">
        <Items>
            <px:PXTabItem Text="Document Details">
                <Template>
                    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100; left: 0px; top: 0px; height: 372px;" Width="100%" SkinID="DetailsInTab" StatusField="Availability" Height="372px" TabIndex="-7372">
                        <Levels>
                            <px:PXGridLevel DataMember="Transactions" DataKeyNames="ShipmentNbr,LineNbr">
                                <Columns>
                                    <px:PXGridColumn DataField="Availability" Width="1px" />
                                    <px:PXGridColumn DataField="ShipmentNbr" Width="90px" />
                                    <px:PXGridColumn DataField="LineNbr" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="OrigOrderType" Width="36px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="OrigOrderNbr" Width="90px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="OrigLineNbr" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn DataField="InventoryID" DisplayFormat="&gt;AAAAAAAAAA" Width="81px" AutoCallBack="True" RenderEditorText="True" />
                                    <px:PXGridColumn DataField="SubItemID" DisplayFormat="&gt;AA-A" Width="45px" NullText="<SPLIT>" AutoCallBack="True" />
                                    <px:PXGridColumn AllowNull="False" DataField="IsFree" TextAlign="Center" Type="CheckBox" />
                                    <px:PXGridColumn DataField="SiteID" DisplayFormat="&gt;AAAAAAAAAA" Width="81px" AutoCallBack="True" />
                                    <px:PXGridColumn DataField="LocationID" DisplayFormat="&gt;AAAAAAAAAA" Width="81px" NullText="<SPLIT>" />
                                    <px:PXGridColumn DataField="UOM" Width="54px" AutoCallBack="True" />
                                    <px:PXGridColumn AllowNull="False" AutoCallBack="True" DataField="ShippedQty" TextAlign="Right" Width="81px" />
                                    <px:PXGridColumn AllowNull="False" DataField="OriginalShippedQty" TextAlign="Right" Width="81px" />
                                    <px:PXGridColumn AllowNull="False" DataField="OrigOrderQty" TextAlign="Right" Width="81px" />
                                    <px:PXGridColumn AllowNull="False" DataField="OpenOrderQty" TextAlign="Right" Width="81px" />
                                    <px:PXGridColumn DataField="LotSerialNbr" Width="180px" NullText="<SPLIT>" />
                                    <px:PXGridColumn DataField="ExpireDate" Width="90px" />
                                    <px:PXGridColumn DataField="ReasonCode" DisplayFormat="&gt;AAAAAAAAAA" Width="81px" />
                                    <px:PXGridColumn DataField="TranDesc" Width="180px" />
                                </Columns>
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                                    <px:PXTextEdit ID="edOrigOrderType" runat="server" DataField="OrigOrderType" Enabled="False" />
                                    <px:PXTextEdit ID="edOrigOrderNbr" runat="server" DataField="OrigOrderNbr" Enabled="False" />
                                    <px:PXSegmentMask CommitChanges="True" ID="edInventoryID" runat="server" DataField="InventoryID" AllowEdit="True" />
                                    <px:PXSegmentMask CommitChanges="True" ID="edSubItemID" runat="server" DataField="SubItemID" AutoRefresh="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                        </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXCheckBox ID="chkIsFree" runat="server" DataField="IsFree" Enabled="False" />
                                    <px:PXSegmentMask CommitChanges="True" ID="edSiteID" runat="server" DataField="SiteID" AutoRefresh="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" />
                                        </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edLocationID" runat="server" DataField="LocationID" AutoRefresh="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.siteID" PropertyName="DataValues[&quot;SiteID&quot;]" Type="String" />
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" />
                                        </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXSelector CommitChanges="True" ID="edUOM" runat="server" DataField="UOM">
                                        <Parameters>
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                        </Parameters>
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="edShippedQty" runat="server" DataField="ShippedQty" />
                                    <px:PXNumberEdit ID="edOrigOrderQty" runat="server" DataField="OrigOrderQty" Enabled="False" />
                                    <px:PXLayoutRule runat="server" ColumnSpan="2" />
                                    <px:PXTextEdit ID="edTranDesc" runat="server" DataField="TranDesc" />
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                                    <px:PXTextEdit ID="edShipmentNbr" runat="server" DataField="ShipmentNbr" />
                                    <px:PXNumberEdit ID="edLineNbr" runat="server" DataField="LineNbr" />
                                    <px:PXSelector ID="edLotSerialNbr" runat="server" DataField="LotSerialNbr" AutoRefresh="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" />
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.locationID" PropertyName="DataValues[&quot;LocationID&quot;]" Type="String" />
                                        </Parameters>
                                    </px:PXSelector>
                                    <px:PXDateTimeEdit ID="edExpireDate" runat="server" DataField="ExpireDate" DisplayFormat="d" />
                                    <px:PXSelector ID="edReasonCode" runat="server" DataField="ReasonCode">
                                        <Parameters>
                                            <px:PXControlParam ControlID="form" Name="SOShipLine.orderType" PropertyName="NewDataKey[&quot;OrderType&quot;]" Type="String" />
                                        </Parameters>
                                    </px:PXSelector>
                                </RowTemplate>
                                <Layout FormViewHeight="" />
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <Mode AllowAddNew="False" AllowFormEdit="True" />
                        <ActionBar PagerGroup="3" PagerOrder="2">
                            <CustomItems>
                                <px:PXToolBarButton Text="Bin/Lot/Serial" Key="cmdLS" CommandName="LSSOShipLine_binLotSerial" CommandSourceID="ds" DependOnGrid="grid" />
                                <px:PXToolBarButton Text="Add Order">
                                    <AutoCallBack Command="SelectSO" Target="ds">
                                        <Behavior PostData="Page" CommitChanges="True" />
                                    </AutoCallBack>
                                </px:PXToolBarButton>
                                <px:PXToolBarButton Text="Inventory Summary">
                                    <AutoCallBack Command="InventorySummary" Target="ds" />
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Orders">
                <Template>
                    <px:PXGrid ID="grid5" runat="server" DataSourceID="ds" Height="150px" Style="z-index: 100" Width="100%" SkinID="Details" BorderWidth="0px">
                        <Levels>
                            <px:PXGridLevel DataMember="OrderList" DataKeyNames="OrderType,OrderNbr,ShipmentType,ShipmentNbr">
                                <Columns>
                                    <px:PXGridColumn DataField="ShipmentNbr" Width="90px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="OrderType" Width="72px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="OrderNbr" Width="90px" />
                                    <px:PXGridColumn AllowNull="False" DataField="ShipmentQty" TextAlign="Right" Width="108px" />
                                    <px:PXGridColumn AllowNull="False" DataField="ShipmentWeight" TextAlign="Right" Width="108px" />
                                    <px:PXGridColumn AllowNull="False" DataField="ShipmentVolume" TextAlign="Right" Width="108px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="InvoiceType" Width="72px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="InvoiceNbr" Width="90px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="InvtDocType" Width="72px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="InvtRefNbr" Width="90px" />
                                </Columns>
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                                    <px:PXTextEdit ID="edOrderType3" runat="server" DataField="OrderType" Enabled="False" />
                                    <px:PXSelector ID="edOrderNbr3" runat="server" DataField="OrderNbr" AutoRefresh="True" AllowEdit="True" />
                                    <px:PXTextEdit ID="edShipmentNbr3" runat="server" DataField="ShipmentNbr" />
                                    <px:PXSelector SuppressLabel="True" ID="edInvoiceNbr3" runat="server" DataField="InvoiceNbr" AutoRefresh="True" AllowEdit="True" />
                                    <px:PXSelector SuppressLabel="True" ID="edInvtRefNbr3" runat="server" DataField="InvtRefNbr" AutoRefresh="True" AllowEdit="True" />
                                    <px:PXNumberEdit ID="edShipmentQty3" runat="server" DataField="ShipmentQty" />
                                </RowTemplate>
                                <Layout FormViewHeight="" />
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                        <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
                        <ActionBar PagerGroup="3" PagerOrder="2" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Shipping Settings">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" SuppressLabel="True" />
                    <px:PXPanel RenderStyle="Fieldset" ID="panelD" runat="server" Caption="Ship-To Info">
                        <px:PXFormView ID="formD" runat="server" CaptionVisible="False" DataMember="Shipping_Contact" DataSourceID="ds" AllowCollapse="false">
                            <Template>
                                <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
                                <px:PXCheckBox ID="chkOverrideContact" runat="server" CommitChanges="True" DataField="OverrideContact" />
                                <px:PXTextEdit ID="edFullName" runat="server" DataField="FullName" />
                                <px:PXTextEdit ID="edSalutation" runat="server" DataField="Salutation" />
                                <px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1" />
                                <px:PXMailEdit ID="edEmail" runat="server" DataField="Email" CommitChanges="True"/>
                            </Template>
                            <ContentStyle BackColor="Transparent" BorderStyle="None" />
                        </px:PXFormView>
                        <px:PXFormView ID="formB" runat="server" CaptionVisible="False" DataMember="Shipping_Address" DataSourceID="ds" AllowCollapse="false">
                            <Template>
                                <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
                                <px:PXLayoutRule runat="server" Merge="True" />
                                <px:PXCheckBox ID="chkOverrideAddress" runat="server" CommitChanges="True" DataField="OverrideAddress" Height="18px" />
                                <px:PXCheckBox ID="chkIsValidated" runat="server" DataField="IsValidated" Enabled="False" />
                                <px:PXLayoutRule runat="server" />
                                <px:PXTextEdit ID="edAddressLine1" runat="server" DataField="AddressLine1" />
                                <px:PXTextEdit ID="edAddressLine2" runat="server" DataField="AddressLine2" />
                                <px:PXTextEdit ID="edCity" runat="server" DataField="City" />
                                <px:PXSelector ID="edCountryID" runat="server" AutoRefresh="True" DataField="CountryID" DataSourceID="ds" />
                                <px:PXSelector ID="edState" runat="server" AutoRefresh="True" DataField="State" DataSourceID="ds">
                                    <CallBackMode PostData="Container" />
                                    <Parameters>
                                        <px:PXControlParam ControlID="formB" Name="SOShippingAddress.countryID" PropertyName="DataControls[&quot;edCountryID&quot;].Value" Type="String" />
                                    </Parameters>
                                </px:PXSelector>
                                <px:PXMaskEdit ID="edPostalCode" runat="server" DataField="PostalCode" />
                            </Template>
                            <ContentStyle BackColor="Transparent" BorderStyle="None" />
                        </px:PXFormView>
                    </px:PXPanel>
                    <px:PXLayoutRule runat="server" StartColumn="True" SuppressLabel="True" />
                    <px:PXPanel RenderStyle="Fieldset" ID="PXPanel2" runat="server" Caption="Shipping Information">
                        <px:PXFormView ID="formF" runat="server" CaptionVisible="False" DataMember="CurrentDocument" DataSourceID="ds" AllowCollapse="false">
                            <Template>
                                <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
                                <px:PXSelector ID="edShipVia" runat="server" CommitChanges="True" DataField="ShipVia" DataSourceID="ds" Size="s" />
                                <px:PXSelector ID="edFOBPoint" runat="server" DataField="FOBPoint" DataSourceID="ds" />
                                <px:PXSelector ID="edShipTermsID" runat="server" CommitChanges="True" DataField="ShipTermsID" DataSourceID="ds" />
								<px:PXSelector ID="edShipZoneID" runat="server" CommitChanges="True" DataField="ShipZoneID" DataSourceID="ds" />
                                <px:PXCheckBox ID="chkResedential" runat="server" DataField="Resedential" />
								<px:PXCheckBox ID="chkSaturdayDelivery" runat="server" DataField="SaturdayDelivery" />
								<px:PXCheckBox ID="chkUseCustomerAccount" runat="server" CommitChanges="True" DataField="UseCustomerAccount" />
								<px:PXCheckBox ID="PXCheckBox1" runat="server" DataField="Insurance" />
                                <px:PXCheckBox ID="chkGroundCollect" runat="server" DataField="GroundCollect" />
                                <px:PXNumberEdit ID="edCuryFreightCost" runat="server" DataField="CuryFreightCost" Enabled="False" />
                                <px:PXNumberEdit ID="edCuryFreightAmt" runat="server" CommitChanges="True" DataField="CuryFreightAmt" />
                                <px:PXSegmentMask ID="edDestinationSiteID" runat="server" CommitChanges="True" DataField="DestinationSiteID" DataSourceID="ds" />
                            </Template>
                            <ContentStyle BackColor="Transparent" BorderStyle="None" />
                        </px:PXFormView>
                    </px:PXPanel>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Packages">
                <Template>
                    <px:PXGrid ID="gridPackages" runat="server" DataSourceID="ds" Style="z-index: 100; left: 0px; top: 0px; height: 372px;" Width="100%" SkinID="Details" BorderWidth="0px">
	                    <ActionBar Position="TopAndBottom">
							<CustomItems>
								<px:PXToolBarButton Text="Recalculate Packages">
									<AutoCallBack Command="RecalculatePackages" Target="ds"/>
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
                        <Levels>
                            <px:PXGridLevel DataMember="Packages">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                                    <px:PXMaskEdit ID="edShipmentNbr_Pkg" runat="server" DataField="ShipmentNbr" InputMask="&gt;CCCCCCCCCCCCCCC" />
                                    <px:PXSelector ID="edBoxID" runat="server" DataField="BoxID" />
                                    <px:PXNumberEdit ID="edLineNbr_Pkg" runat="server" DataField="LineNbr" />
                                    <px:PXNumberEdit ID="edWeight" runat="server" DataField="Weight" />
									<px:PXNumberEdit ID="PXNumberEdit5" runat="server" DataField="COD" />
									<px:PXNumberEdit ID="PXNumberEdit6" runat="server" DataField="DeclaredValue" />
                                    <px:PXTextEdit ID="edTrackNumber" runat="server" DataField="TrackNumber" />
									<px:PXTextEdit ID="PXTextEdit1" runat="server" DataField="CustomRefNbr1" />
									<px:PXTextEdit ID="PXTextEdit2" runat="server" DataField="CustomRefNbr2" />
									<px:PXTextEdit ID="PXTextEdit3" runat="server" DataField="Description" />
									<px:PXDropDown runat="server" ID="edType" DataField="PackageType"></px:PXDropDown>
								</RowTemplate>
                                <Columns>
                                    <px:PXGridColumn AllowNull="False" DataField="Confirmed" Label="Confired" TextAlign="Center" Type="CheckBox" Width="91px" />
                                    <px:PXGridColumn DataField="BoxID" DisplayFormat="&gt;aaaaaaaaaaaaaaa" Label="Box ID" Width="117px" />
									<px:PXGridColumn DataField="PackageType" Width="63px" Type="DropDownList"/>
                                    <px:PXGridColumn AutoGenerateOption="NotSet" DataField="Description" MaxLength="30" Width="200px" />
									<px:PXGridColumn AllowNull="False" DataField="Weight" TextAlign="Right" Width="91px" />
                                    <px:PXGridColumn AllowNull="False" DataField="WeightUOM" Width="91px" />
                                    <px:PXGridColumn AllowNull="False" DataField="DeclaredValue" TextAlign="Right" Width="91px" />
                                    <px:PXGridColumn AllowNull="False" DataField="COD" Label="C.O.D. Amount" TextAlign="Right" Width="91px" />
                                    <px:PXGridColumn DataField="TrackNumber" Width="300px" />
                                    <px:PXGridColumn DataField="CustomRefNbr1" MaxLength="60" Width="200px" />
                                    <px:PXGridColumn DataField="CustomRefNbr2" MaxLength="60" Width="200px" />
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" MinHeight="150" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <CallbackCommands>
            <Search CommitChanges="True" PostData="Page" />
            <Refresh CommitChanges="True" PostData="Page" />
        </CallbackCommands>
        <AutoSize Container="Window" Enabled="True" MinHeight="180" />
    </px:PXTab>
    <%-- Bin/Lot/Serial Numbers --%>
    <px:PXSmartPanel ID="PanelLS" runat="server" Style="z-index: 108; left: 252px; position: absolute; top: 531px; height: 360px;" Width="764px" Caption="Bin/Lot/Serial Numbers" CaptionVisible="True"
        Key="lsselect" AutoCallBack-Command="Refresh" AutoCallBack-Enabled="True" AutoCallBack-Target="optform">
        <px:PXFormView ID="optform" runat="server" Width="100%" CaptionVisible="False" DataMember="LSSOShipLine_lotseropts" DataSourceID="ds" SkinID="Transparent">
            <Parameters>
                <px:PXSyncGridParam ControlID="grid" />
            </Parameters>
            <Template>
                <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
                <px:PXNumberEdit ID="edUnassignedQty" runat="server" DataField="UnassignedQty" Enabled="False" />
                <px:PXNumberEdit ID="edQty" runat="server" DataField="Qty">
                    <AutoCallBack>
                        <Behavior CommitChanges="True" />
                    </AutoCallBack>
                </px:PXNumberEdit>
                <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
                <px:PXMaskEdit ID="edStartNumVal" runat="server" DataField="StartNumVal" />
                <px:PXButton ID="btnGenerate" runat="server" Text="Generate" Height="20px" CommandName="LSSOShipLine_generateLotSerial" CommandSourceID="ds"></px:PXButton>
            </Template>
        </px:PXFormView>
        <px:PXGrid ID="grid2" runat="server" Width="100%" AutoAdjustColumns="True" DataSourceID="ds" Style="border-width: 1px 0px; left: 0px; top: 0px; height: 192px;">
            <AutoSize Enabled="true" />
            <Mode InitNewRow="True" />
            <Parameters>
                <px:PXSyncGridParam ControlID="grid" />
            </Parameters>
            <Levels>
                <px:PXGridLevel DataMember="splits">
                    <Columns>
                        <px:PXGridColumn DataField="InventoryID" Width="108px" />
                        <px:PXGridColumn DataField="SubItemID" Width="108px" />
                        <px:PXGridColumn DataField="LocationID" AllowShowHide="Server" Width="108px" />
                        <px:PXGridColumn DataField="LotSerialNbr" AllowShowHide="Server" Width="108px" />
                        <px:PXGridColumn DataField="Qty" Width="108px" TextAlign="Right" />
                        <px:PXGridColumn DataField="UOM" Width="108px" />
                        <px:PXGridColumn DataField="ExpireDate" AllowShowHide="Server" Width="90px" />
                        <px:PXGridColumn AllowUpdate="False" DataField="InventoryID_InventoryItem_descr" Width="108px" />
                    </Columns>
                    <RowTemplate>
                        <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                        <px:PXSegmentMask ID="edSubItemID2" runat="server" DataField="SubItemID" AutoRefresh="true" />
                        <px:PXSegmentMask ID="edLocationID2" runat="server" DataField="LocationID" AutoRefresh="true">
                            <Parameters>
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.siteID" PropertyName="DataValues[&quot;SiteID&quot;]" Type="String" />
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" />
                            </Parameters>
                        </px:PXSegmentMask>
                        <px:PXNumberEdit ID="edQty2" runat="server" DataField="Qty" />
                        <px:PXSelector ID="edUOM2" runat="server" DataField="UOM" AutoRefresh="true">
                            <Parameters>
                                <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                            </Parameters>
                        </px:PXSelector>
                        <px:PXSelector ID="edLotSerialNbr2" runat="server" DataField="LotSerialNbr" AutoRefresh="true">
                            <Parameters>
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" />
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.locationID" PropertyName="DataValues[&quot;LocationID&quot;]" Type="String" />
                            </Parameters>
                        </px:PXSelector>
                        <px:PXDateTimeEdit ID="edExpireDate2" runat="server" DataField="ExpireDate" />
                    </RowTemplate>
                    <Layout ColumnsMenu="False" />
                </px:PXGridLevel>
            </Levels>
        </px:PXGrid>
        <px:PXPanel ID="PXPanel1" runat="server" SkinID="Buttons">
            <px:PXButton ID="btnSave" runat="server" DialogResult="OK" Text="OK" />
        </px:PXPanel>
    </px:PXSmartPanel>
    <%-- Add Sales Order --%>
    <px:PXSmartPanel ID="PanelAddSO" runat="server" Height="396px" Style="z-index: 108; left: 216px; position: absolute; top: 171px" Width="873px" CommandName="AddSO" CommandSourceID="ds" Caption="Add Sales Order"
        CaptionVisible="True" LoadOnDemand="true" CallBackMode-CommitChanges="True" CallBackMode-PostData="Page" AutoRepaint="true" Key="addsofilter">
        <px:PXFormView ID="form4" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="addsofilter" CaptionVisible="False">
            <ContentStyle BackColor="Transparent" BorderStyle="None" />
            <Template>
                <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                <px:PXDropDown CommitChanges="True" ID="edOperation" runat="server" DataField="Operation" />
                <px:PXSelector CommitChanges="True" ID="edOrderType4" runat="server" DataField="OrderType" AutoRefresh="true" />
                <px:PXSelector ID="edOrderNbr4" runat="server" DataField="OrderNbr" AutoRefresh="true">
                    <AutoCallBack Target="grid4" Command="Refresh" />
                    <AutoCallBack Command="Save" Target="form4" />
                    <Parameters>
                        <px:PXControlParam ControlID="form4" Name="AddSOFilter.orderType" PropertyName="DataControls[&quot;edOrderType4&quot;].Value" Type="String" />
                    </Parameters>
                </px:PXSelector>
            </Template>
        </px:PXFormView>
        <px:PXGrid ID="grid4" runat="server" Height="240px" Width="100%" DataSourceID="ds" BatchUpdate="true" Style="border-width: 1px 0px" SkinID="Inquire">
            <AutoSize Enabled="true" />
            <Parameters>
                <px:PXControlParam ControlID="form4" Name="AddSOFilter_orderType" PropertyName="DataControls[&quot;edOrderType4&quot;].Value" Type="String" />
                <px:PXControlParam ControlID="form4" Name="AddSOFilter_orderNbr" PropertyName="DataControls[&quot;edOrderNbr4&quot;].Value" Type="String" />
                <px:PXControlParam ControlID="form4" Name="AddSOFilter_operation" PropertyName="DataControls[&quot;edOperation&quot;].Value" Type="String" />
            </Parameters>
            <Levels>
                <px:PXGridLevel DataMember="soshipmentplan">
                    <Columns>
                        <px:PXGridColumn DataField="Selected" Width="60px" Type="CheckBox" AllowCheckAll="True" TextAlign="Center" />
                        <px:PXGridColumn DataField="SOLine__LineNbr" Visible="false" />
                        <px:PXGridColumn DataField="SOLine__InventoryID" DisplayFormat="&gt;AAAAAAAAAA" />
                        <px:PXGridColumn DataField="SOLine__SubItemID" DisplayFormat="&gt;AA-A-A" Width="60px" />
                        <px:PXGridColumn DataField="SOLine__UOM" Width="63px" DisplayFormat="&gt;aaaaaa" />
                        <px:PXGridColumn DataField="SOLine__LocationID" Width="100px" />
                        <px:PXGridColumn DataField="SOLine__LotSerialNbr" Width="100px" />
                        <px:PXGridColumn DataField="PlanDate" Label="Plan Date" Width="90px" />
                        <px:PXGridColumn DataField="SOLine__OrderQty" Width="100px" TextAlign="Right" />
                        <px:PXGridColumn DataField="SOLine__OpenQty" Width="108px" TextAlign="Right" />
                        <px:PXGridColumn DataField="SOLine__TranDesc" Width="200px" />
                    </Columns>
                    <Layout ColumnsMenu="False" />
                </px:PXGridLevel>
            </Levels>
        </px:PXGrid>
        <px:PXPanel ID="PXPanel3" runat="server" SkinID="Buttons">
            <px:PXButton ID="PXButton1" runat="server" CommandName="AddSO" CommandSourceID="ds" Text="Add" />
            <px:PXButton ID="PXButton2" runat="server" DialogResult="OK" Text="Add &amp; Close" />
            <px:PXButton ID="PXButton3" runat="server" CommandName="AddSOCancel" CommandSourceID="ds" DialogResult="Cancel" Text="Cancel" />
        </px:PXPanel>
    </px:PXSmartPanel>
</asp:Content>
