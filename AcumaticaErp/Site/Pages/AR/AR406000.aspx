<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AR406000.aspx.cs" Inherits="Page_AR406000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter" TypeName="PX.Objects.AR.CCTransactionsHistoryEnq">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="ViewPayment" DependOnGrid="grid" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ViewCustomer" DependOnGrid="grid" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ViewPaymentMethod" DependOnGrid="grid" Visible="False" CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" Style="z-index: 100" Width="100%" DataMember="Filter" Caption="Selection" DefaultControlID="edPaymentMethodID" TabIndex="2100">
		<Activity HighlightColor="" SelectedColor="" Width="" Height=""></Activity>
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
			<px:PXSelector CommitChanges="True" ID="edPaymentMethodID" runat="server" DataField="PaymentMethodID" />
			<px:PXMaskEdit CommitChanges="True" ID="emPartialCardNumber" runat="server" DataField="PartialCardNumber" />
			<px:PXTextEdit CommitChanges="True" ID="edNameOnCard" runat="server" DataField="NameOnCard" />
			<px:PXNumberEdit ID="edNumberOfCards" runat="server"  DataField="NumberOfCards" />
			<px:PXSelector CommitChanges="True" ID="edPMInstanceID" runat="server" AutoRefresh="True" DataField="PMInstanceID"/>
			<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
			<px:PXDateTimeEdit CommitChanges="True" ID="edStartDate" runat="server" DataField="StartDate" />
			<px:PXDateTimeEdit CommitChanges="True" ID="edEndDate" runat="server" DataField="EndDate" />
			<px:PXSelector CommitChanges="True" ID="edProcessingCenterID" runat="server" DataField="ProcessingCenterID" />
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="150px" Style="z-index: 100" Width="100%" Caption="Transaction List" AllowSearch="True" AllowPaging="True" SkinID="Inquire" RestrictFields="True">
		<Levels>
			<px:PXGridLevel DataMember="CCTrans">
				<RowTemplate>
					
				</RowTemplate>
				<Columns>
					<px:PXGridColumn DataField="Customer__AcctCD" Width="90px" />
					<px:PXGridColumn DataField="Customer__AcctName" Width="150px" />
					<px:PXGridColumn DataField="CCProcTran__DocType" Width="80px" />
					<px:PXGridColumn DataField="CCProcTran__RefNbr" Width="90px" />
					<px:PXGridColumn DataField="CCProcTran__OrigDocType" Width="80px" />
					<px:PXGridColumn DataField="CCProcTran__OrigRefNbr" Width="90px" />
					<px:PXGridColumn DataField="CustomerPaymentMethod__Descr" Width="150px" />
					<px:PXGridColumn DataField="TranNbr" TextAlign="Right" Width="54px" />
					<px:PXGridColumn  DataField="ProcessingCenterID" Width="85px" />
					<px:PXGridColumn DataField="TranType" RenderEditorText="True" Width="140px" />
					<px:PXGridColumn  DataField="TranStatus" RenderEditorText="True" Width="75px" />
					<px:PXGridColumn  DataField="Amount" TextAlign="Right" Width="80px" />
					<px:PXGridColumn DataField="RefTranNbr" TextAlign="Right" Width="54px" />
					<px:PXGridColumn DataField="PCTranNumber" Width="90px" />
					<px:PXGridColumn DataField="AuthNumber" Width="75px" />
					<px:PXGridColumn DataField="PCResponseReasonText" Width="240px" />
					<px:PXGridColumn DataField="StartTime" />
					<px:PXGridColumn  DataField="ProcStatus" RenderEditorText="True" Width="72px" />
					<px:PXGridColumn DataField="CVVVerificationStatus" RenderEditorText="True" Width="171px" />
				</Columns>
			</px:PXGridLevel>
		</Levels>
		<ActionBar>
			<CustomItems>
				<px:PXToolBarButton Text="View Payment" Key="cmdViewPayment">
				    <AutoCallBack Command="ViewPayment" Target="ds" />
				</px:PXToolBarButton>
				<px:PXToolBarButton Text="View Customer" Key="cmdViewCustomer">
				    <AutoCallBack Command="ViewCustomer" Target="ds" />
				</px:PXToolBarButton>
				<px:PXToolBarButton Text="View Payment Method" Key="cmdViewPaymentMethod">
				    <AutoCallBack Command="ViewPaymentMethod" Target="ds" />
				</px:PXToolBarButton>
			</CustomItems>
		</ActionBar>
		<AutoSize Container="Window" Enabled="True" MinHeight="50" MinWidth="50" />
	</px:PXGrid>
</asp:Content>
