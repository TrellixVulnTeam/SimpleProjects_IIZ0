<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AR406500.aspx.cs" Inherits="Page_AR406500" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PageLoadBehavior="PopulateSavedValues" PrimaryView="Filter" TypeName="PX.Objects.AR.FailedCCPaymentEnq">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="ViewCustomer" DependOnGrid="grid" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ViewDocument" DependOnGrid="grid" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ViewCreditCard" DependOnGrid="grid" Visible="False" CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server"  Style="z-index: 100" Width="100%" DataMember="Filter" Caption="Selection" DefaultControlID="edBeginDate">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
			<px:PXDateTimeEdit CommitChanges="True" ID="edBeginDate" runat="server" DataField="BeginDate" />
			<px:PXDateTimeEdit CommitChanges="True" ID="edEndDate" runat="server" DataField="EndDate" />
			<px:PXDropDown CommitChanges="True" ID="edDisplayType" runat="server" DataField="DisplayType" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
			<px:PXSelector CommitChanges="True" ID="edProcessingCenterID" runat="server" DataField="ProcessingCenterID" />
			<px:PXSelector CommitChanges="True" ID="edCustomerClassID" runat="server" DataField="CustomerClassID" />
			<px:PXSegmentMask CommitChanges="True" ID="PXSegmentMask1" runat="server" DataField="CustomerID" /></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server"  Height="150px" Style="z-index: 100" Width="100%" Caption="Transaction List" AllowSearch="True" SkinID="Inquire" RestrictFields="True">
		<Levels>
			<px:PXGridLevel DataMember="PaymentTrans">
				<Columns>
					<px:PXGridColumn  DataField="Customer__AcctCD" Width="90px" />
					<px:PXGridColumn  DataField="Customer__AcctName" Width="150px" />
					<px:PXGridColumn  DataField="DocType" />
					<px:PXGridColumn  DataField="RefNbr" />
					<px:PXGridColumn  DataField="OrigDocType" />
					<px:PXGridColumn  DataField="OrigRefNbr" />
					<px:PXGridColumn  DataField="ProcessingCenterID" Width="90px" />
					<px:PXGridColumn DataField="TranNbr" TextAlign="Right" Visible="False" />
					<px:PXGridColumn DataField="TranType" Width="100px" />
					<px:PXGridColumn  DataField="Amount" TextAlign="Right" Width="90px" />
					<px:PXGridColumn  DataField="ProcStatus" />
					<px:PXGridColumn DataField="TranStatus" />
					<px:PXGridColumn DataField="PCTranNumber" Width="90px" />
					<px:PXGridColumn DataField="PCResponseReasonText" Width="200px" />
					<px:PXGridColumn DataField="StartTime" />
				</Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
		<ActionBar DefaultAction="cmdViewDocument">
			<Actions>
				<Save Enabled="False" />
				<AddNew Enabled="False" />
			</Actions>
			<CustomItems>
				<px:PXToolBarButton Text="View Document" Key="cmdViewDocument">
				    <AutoCallBack Command="ViewDocument" Target="ds" />
				</px:PXToolBarButton>
				<px:PXToolBarButton Text="View Customer" Key="cmdViewCustomer">
				    <AutoCallBack Command="ViewCustomer" Target="ds" />
				</px:PXToolBarButton>
				<px:PXToolBarButton Text="View Payment Method" Key="cmdViewCreditCard">
				    <AutoCallBack Command="ViewCreditCard" Target="ds" />
				</px:PXToolBarButton>
			</CustomItems>
		</ActionBar>
		<Mode AllowAddNew="False" AllowDelete="False"  />
	</px:PXGrid>
</asp:Content>
