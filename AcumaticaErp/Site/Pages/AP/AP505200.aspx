<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AP505200.aspx.cs" Inherits="Page_AP505200" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.AP.APReleaseChecks" PrimaryView="Filter" PageLoadBehavior="PopulateSavedValues">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Process" CommitChanges="true" StartNewGroup="true" />
			<px:PXDSCallbackCommand Name="ProcessAll" CommitChanges="true" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="CurrencyView" Visible="False" />
			<px:PXDSCallbackCommand Name="viewDocument" Visible="false"	DependOnGrid="grid" />
			<px:PXDSCallbackCommand Name="release" Visible="false"	/>
			<px:PXDSCallbackCommand Name="reprint" Visible="false"	/>
			<px:PXDSCallbackCommand Name="voidReprint" Visible="false" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Filter" Caption="Selection" DefaultControlID="edPayTypeID">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />			
			<px:PXSelector CommitChanges="True" ID="edPayTypeID" runat="server" DataField="PayTypeID" />
			<px:PXSegmentMask CommitChanges="True" ID="edPayAccountID" runat="server" DataField="PayAccountID" />
			<px:PXDropDown ID="edAction" runat="server"  DataField="Action" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="S" />
			<px:PXSelector ID="edCuryID" runat="server" DataField="CuryID"  Enabled="False"/>
			<px:PXNumberEdit ID="edGLBalance" runat="server" DataField="GLBalance" Enabled="False" />
			<px:PXNumberEdit ID="edCashBalance" runat="server"  DataField="CashBalance" Enabled="False" /></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="288px" Style="z-index: 100" Width="100%" Caption="Payments" AllowPaging="true" AdjustPageSize="Auto" SkinID="Inquire">
		<Levels>
			<px:PXGridLevel DataMember="APPaymentList" >
				<RowTemplate>
							
				</RowTemplate>
				<Columns>
					<px:PXGridColumn  DataField="Selected" Width="20px" TextAlign="Center" Type="CheckBox" AllowCheckAll="True" AllowSort="False" AllowMove="False" />
					<px:PXGridColumn  DataField="PrintCheck" Width="45px" TextAlign="Center" Type="CheckBox" AllowSort="False" AllowMove="False" AutoCallBack="True" />
					<px:PXGridColumn  DataField="ExtRefNbr" Width="90px" />
					<px:PXGridColumn  DataField="DocDate" Width="90px" />
					<px:PXGridColumn DataField="RefNbr" Width="108px" LinkCommand = "viewDocument"/>
					<px:PXGridColumn DataField="VendorID" Width="81px" />
					<px:PXGridColumn DataField="VendorID_Vendor_acctName" Width="140px" />
					<px:PXGridColumn  DataField="CuryOrigDocAmt" TextAlign="Right" Width="81px"/>
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
