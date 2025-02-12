<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AP503000.aspx.cs" Inherits="Page_AP503000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.AP.APPayBills" PrimaryView="Filter" PageLoadBehavior="PopulateSavedValues">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Process" CommitChanges="true" StartNewGroup="true" />
			<px:PXDSCallbackCommand Name="ProcessAll" CommitChanges="true" />
			<px:PXDSCallbackCommand Visible="False" Name="CurrencyView" />
			<px:PXDSCallbackCommand DependOnGrid="grid" Name="viewDocument" Visible="False" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" Style="z-index: 100" Width="100%" DataMember="Filter" Caption="Selection" DefaultControlID="edPayTypeID" TabIndex="6100">
		<Activity HighlightColor="" SelectedColor="" Width="" Height=""></Activity>
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
			<px:PXSelector CommitChanges="True" ID="edPayTypeID" runat="server" DataField="PayTypeID" AutoRefresh="True" />
			<px:PXSegmentMask CommitChanges="True" ID="edPayAccountID" runat="server" DataField="PayAccountID" />			
			<px:PXDateTimeEdit CommitChanges="True" ID="edSelectionDate" runat="server" DataField="PayDate" />
			<px:PXSelector CommitChanges="True" ID="edPayFinPeriodID" runat="server" DataField="PayFinPeriodID" />
			<pxa:PXCurrencyRate DataField="CuryID" ID="edCury" runat="server" RateTypeView="_PayBillsFilter_CurrencyInfo_" DataMember="_Currency_"></pxa:PXCurrencyRate>
			<px:PXSegmentMask CommitChanges="True" ID="edVendorID" runat="server" DataField="VendorID" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="M" />
			<px:PXLayoutRule runat="server" Merge="True" />
			<px:PXCheckBox AlignLeft="True" Size="M" CommitChanges="True" ID="chkOverDueIn" runat="server" DataField="ShowPayInLessThan" />
			<px:PXNumberEdit CommitChanges="True" Size="xxs" ID="edOverDueIn" runat="server" DataField="PayInLessThan" SuppressLabel="True" />
			<px:PXLabel ID="lblOverDueIn" runat="server">Days</px:PXLabel>
			<px:PXLayoutRule runat="server" />
			<px:PXLayoutRule runat="server" Merge="True" />
			<px:PXCheckBox AlignLeft="True" Size="M" CommitChanges="True" ID="chkDiscountExparedWithinLast" runat="server" DataField="ShowDueInLessThan" />
			<px:PXNumberEdit CommitChanges="True" Size="xxs" ID="edDiscountExparedWithinLast" runat="server" DataField="DueInLessThan" SuppressLabel="True" />
			<px:PXLabel ID="lblDiscountExparedWithinLast" runat="server">Days</px:PXLabel>
			<px:PXLayoutRule runat="server" />
			<px:PXLayoutRule runat="server" Merge="True" />
			<px:PXCheckBox AlignLeft="True" Size="M" CommitChanges="True" ID="chkDiscountExpiresInLessThan" runat="server" DataField="ShowDiscountExpiresInLessThan" />
			<px:PXNumberEdit CommitChanges="True" Size="xxs" ID="edDiscountExpiresInLessThan" runat="server" DataField="DiscountExpiresInLessThan" SuppressLabel="True" />
			<px:PXLabel ID="lblDiscountExpiresInLessThan" runat="server">Days</px:PXLabel>
			<px:PXLayoutRule runat="server" />
			<px:PXCheckBox AlignLeft="True" Size="M" CommitChanges="True" ID="chkTakeDiscAlways" runat="server" DataField="TakeDiscAlways" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
			<px:PXNumberEdit ID="edGLBalance" runat="server" DataField="GLBalance" Enabled="False" />
			<px:PXNumberEdit ID="edCashBalance" runat="server" DataField="CashBalance" Enabled="False" />
			<px:PXNumberEdit ID="edCurySelTotal" runat="server" DataField="CurySelTotal" Enabled="False" />
			<px:PXNumberEdit ID="edSelCount" runat="server" DataField="SelCount" Enabled="False" />
        </Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="288px" Style="z-index: 100" Width="100%" Caption="Payment Details" AllowPaging="True" AdjustPageSize="Auto" SkinID="Details" DataSourceID="ds" TabIndex="6300">
		<Mode InitNewRow="True"></Mode>
		<Levels>
			<px:PXGridLevel DataMember="APDocumentList">
				<RowTemplate>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
					<px:PXCheckBox ID="chkSelected" runat="server" DataField="Selected" />
					<px:PXDropDown ID="AdjdDocType" runat="server" DataField="AdjdDocType" 
                        CommitChanges="True" />
					<px:PXSelector ID="edAdjdRefNbr" runat="server" DataField="AdjdRefNbr" 
                        AutoRefresh="True" AllowEdit="True" edit="1" >
						<Parameters>
							<px:PXControlParam ControlID="grid" Name="APAdjust.adjdDocType" PropertyName="DataValues[&quot;AdjdDocType&quot;]" Type="String" />
						</Parameters>
					</px:PXSelector>
					<px:PXCheckBox ID="SeparateCheck" runat="server" DataField="SeparateCheck" Text="Pay Separately" />
					<px:PXNumberEdit ID="CuryAdjgDiscAmt" runat="server" DataField="CuryAdjgDiscAmt" />
					<px:PXNumberEdit ID="CuryAdjgAmt" runat="server" DataField="CuryAdjgAmt" />
				</RowTemplate>
				<Columns>
					<px:PXGridColumn DataField="Selected" Width="20px" TextAlign="Center" Type="CheckBox" AllowCheckAll="True" AllowSort="False" AllowMove="False" AutoCallBack="True" />
					<px:PXGridColumn DataField="AdjdDocType" Type="DropDownList" Width="81px" AutoCallBack = "true"  />
					<px:PXGridColumn DataField="AdjdRefNbr" Width="108px" />
					<px:PXGridColumn DataField="VendorID" Width="81px" /> 
					<px:PXGridColumn DataField="VendorID_Vendor_acctName" Width="140px" />
					<px:PXGridColumn DataField="SeparateCheck" TextAlign="Center" Type="CheckBox" Width="80px" />
					<px:PXGridColumn DataField="APInvoice__PayDate" Width="90px" />
					<px:PXGridColumn DataField="APInvoice__DueDate" Width="90px" />
					<px:PXGridColumn DataField="APInvoice__DiscDate" Width="90px" />
                    <px:PXGridColumn DataField="APInvoice__DocDate" Width="90px"/>
					<px:PXGridColumn DataField="CuryAdjgAmt" TextAlign="Right" Width="81px" AutoCallBack="True" />
					<px:PXGridColumn DataField="CuryAdjgDiscAmt" TextAlign="Right" Width="81px" AutoCallBack="True" />
					<px:PXGridColumn DataField="CuryDocBal" TextAlign="Right" Width="81px" />
					<px:PXGridColumn DataField="CuryDiscBal" TextAlign="Right" Width="81px" />
					<px:PXGridColumn DataField="APInvoice__CuryID" Width="54px" />
					<px:PXGridColumn DataField="APInvoice__InvoiceNbr" Width="90px" />
                    <px:PXGridColumn DataField="APInvoice__DocDesc" Width="160px" />
				</Columns> 
				<Layout FormViewHeight=""></Layout>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
		<Mode InitNewRow="True" AllowDelete="True" />
		<ActionBar>
			<CustomItems>
				<px:PXToolBarButton Text="View Document" Tooltip="View Document" CommandName="viewDocument" CommandSourceID="ds" />
			</CustomItems>
		</ActionBar>
		<AutoSize Enabled="True" Container="Window" MinHeight="150"></AutoSize>
	</px:PXGrid>
</asp:Content>
