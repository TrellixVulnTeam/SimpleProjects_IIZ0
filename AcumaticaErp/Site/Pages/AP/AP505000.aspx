<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AP505000.aspx.cs" Inherits="Page_AP505000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
		TypeName="PX.Objects.AP.APPrintChecks" PrimaryView="Filter" 
		PageLoadBehavior="GoLastRecord">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Process" CommitChanges="true" StartNewGroup="true" />
			<px:PXDSCallbackCommand Name="ProcessAll" CommitChanges="true" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="CurrencyView" Visible="False" />
			<px:PXDSCallbackCommand Name="viewDocument" Visible="false" DependOnGrid="grid" />
		</CallbackCommands>		
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Filter" Caption="Selection" DefaultControlID="edPayTypeID" >	
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
			<px:PXSelector CommitChanges="True" ID="edPayTypeID" runat="server" DataField="PayTypeID" AutoRefresh="True" />
			<px:PXSegmentMask CommitChanges="True" ID="edPayAccountID" runat="server" DataField="PayAccountID" />			
			<px:PXSelector ID="edCuryID" runat="server" DataField="CuryID" />
			<px:PXTextEdit CommitChanges="True" ID="edNextCheckNbr" runat="server" DataField="NextCheckNbr" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
			<px:PXNumberEdit ID="edGLBalance" runat="server" DataField="GLBalance" Enabled="False" />
			<px:PXNumberEdit ID="edCashBalance" runat="server" DataField="CashBalance" Enabled="False" />
			<px:PXNumberEdit ID="edCurySelTotal" runat="server" DataField="CurySelTotal" Enabled="False" />
			<px:PXNumberEdit ID="edSelCount" runat="server" DataField="SelCount" Enabled="False" />
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="288px" Style="z-index: 100" Width="100%" Caption="Payments" AllowPaging="true" AdjustPageSize="Auto" SkinID="Inquire">
		<Levels>
			<px:PXGridLevel DataMember="APPaymentList">
				<RowTemplate>
				</RowTemplate>
				<Columns>
					<px:PXGridColumn DataField="Selected" Width="20px" TextAlign="Center" Type="CheckBox" AllowCheckAll="True" AllowSort="False" AllowMove="False" />
					<px:PXGridColumn DataField="ExtRefNbr" Width="90px" />
					<px:PXGridColumn DataField="DocDate" Width="90px" />
					<px:PXGridColumn DataField="RefNbr" Width="100px" LinkCommand="viewDocument" />
					<px:PXGridColumn DataField="VendorID" Width="100px" />
					<px:PXGridColumn DataField="VendorID_Vendor_acctName" Width="250px" />
					<px:PXGridColumn DataField="CuryOrigDocAmt" TextAlign="Right" Width="81px" />
				</Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
		<ActionBar>
			<CustomItems>
				<px:PXToolBarButton Text="View Document" Tooltip="View Document" CommandName="viewDocument" CommandSourceID="ds" />
			</CustomItems>
		</ActionBar>
	</px:PXGrid>
</asp:Content>
