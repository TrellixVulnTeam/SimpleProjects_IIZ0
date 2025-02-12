<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AP305000.aspx.cs" Inherits="Page_AP305000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Document" TypeName="PX.Objects.CA.CABatchEntry" PageLoadBehavior="GoLastRecord">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" Visible="false" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="true" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
			<px:PXDSCallbackCommand StartNewGroup="True" DependOnGrid="grid" Name="ViewAPDocument" Visible="false" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" Style="z-index: 100" Width="100%" Caption="Batch Summary" DataMember="Document" DefaultControlID="edBatchNbr" FilesIndicator="True" NoteIndicator="True" LinkIndicator="True"
		NotifyIndicator="True">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
			<px:PXSelector runat="server" DataField="BatchNbr" ID="edBatchNbr" />
			<px:PXDropDown runat="server" DataField="Status" ID="edStatus" Enabled="False" />
			<px:PXCheckBox CommitChanges="True" Size="" runat="server" DataField="Hold" ID="chkHold" />
			<px:PXDateTimeEdit CommitChanges="True" runat="server" DataField="TranDate" ID="edTranDate" />
			<px:PXTextEdit runat="server" DataField="ExtRefNbr" ID="edExtRefNbr" />
			<px:PXDateTimeEdit runat="server" DataField="ExportTime" ID="edExportTime" Enabled="False" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
			<px:PXSegmentMask CommitChanges="True" Height="18px" runat="server" DataField="CashAccountID" ID="edCashAccountID" Enabled="False" />
			<px:PXSelector CommitChanges="True" runat="server" DataField="PaymentMethodID" ID="edPaymentMethodID" Enabled="False" />
			<px:PXSelector runat="server" DataField="ReferenceID" ID="edReferenceID" Enabled="False" />
			<px:PXTextEdit runat="server" DataField="BatchSeqNbr" ID="edBatchSeqNbr" />
			<px:PXNumberEdit runat="server" DataField="DateSeqNbr" ID="edDateSeqNbr" Enabled="False" />
			<px:PXLayoutRule runat="server" ColumnSpan="2" />
			<px:PXTextEdit runat="server" DataField="TranDesc" ID="edTranDesc" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
			<px:PXNumberEdit runat="server" Enabled="False" DataField="CuryDetailTotal" ID="edCuryDetailTotal" />
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="180px" AllowAutoHide="false" Width="100%" Caption="Payments" SkinID="Details">
		<%--<AutoCallBack Target="gridPaymentApplications" Command="Refresh" />--%>
		<Levels>
			<px:PXGridLevel DataMember="BatchPayments">
				<RowTemplate>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
					<px:PXDropDown ID="edAPPayment__DocType" runat="server" DataField="APPayment__DocType" />
					<px:PXSelector ID="edAPPayment__RefNbr" runat="server" DataField="APPayment__RefNbr" />
					<px:PXSegmentMask ID="edAPPayment__VendorID" runat="server" DataField="APPayment__VendorID" />
					<px:PXSegmentMask ID="edAPPayment__VendorLocationID" runat="server" DataField="APPayment__VendorLocationID" />
					<px:PXSelector ID="edAPPayment__CuryID" runat="server" DataField="APPayment__CuryID" />
					<px:PXTextEdit ID="edAPPayment__DocDesc" runat="server" DataField="APPayment__DocDesc" />
					<px:PXSelector ID="edAPPayment__PaymentMethodID" runat="server" DataField="APPayment__PaymentMethodID" />
					<px:PXTextEdit ID="edAPPayment__ExtRefNbr" runat="server" DataField="APPayment__ExtRefNbr" />
					<px:PXDateTimeEdit ID="edAPPayment__DocDate" runat="server" DataField="APPayment__DocDate" Enabled="False" />
					<px:PXNumberEdit ID="edAPPayment__CuryOrigDocAmt" runat="server" DataField="APPayment__CuryOrigDocAmt" /></RowTemplate>
				<Columns>
					<px:PXGridColumn DataField="APPayment__DocType" Width="65px" Type="DropDownList" />
					<px:PXGridColumn DataField="APPayment__RefNbr" Width="87px" LinkCommand="ViewAPDocument" />
					<px:PXGridColumn DataField="APPayment__VendorID" Width="90px" />
					<px:PXGridColumn DataField="APPayment__VendorLocationID" Width="90px" />
					<px:PXGridColumn DataField="APPayment__DocDate" Width="80px" />
					<px:PXGridColumn DataField="APPayment__Status" />
					<px:PXGridColumn DataField="APPayment__CuryID" Width="54px" />
					<px:PXGridColumn DataField="APPayment__DocDesc" Width="150px" />
					<px:PXGridColumn DataField="APPayment__PaymentMethodID" Width="81px" />
					<px:PXGridColumn DataField="APPayment__ExtRefNbr" />
					<px:PXGridColumn DataField="APPayment__CuryOrigDocAmt" TextAlign="Right" Width="81px" />
				</Columns>
			</px:PXGridLevel>
		</Levels>
		<ActionBar>
			<Actions>
				<AddNew Enabled="False" />
			</Actions>
			<CustomItems>
				<px:PXToolBarButton Text="View Payment">
				    <AutoCallBack Command="ViewAPDocument" Target="ds" />
				</px:PXToolBarButton>
			</CustomItems>
		</ActionBar>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>
