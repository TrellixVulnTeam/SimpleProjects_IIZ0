
<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AR512500.aspx.cs" Inherits="Page_AR512500" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter" PageLoadBehavior="PopulateSavedValues" TypeName="PX.Objects.AR.ARExpiredCardsProcess">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Process" CommitChanges="true" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="ViewCustomer" DependOnGrid="grid" Visible="False"
				CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ViewPaymentMethod" DependOnGrid="grid" Visible="False"
				CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" 
	Style="z-index: 100" Width="100%" DataMember="Filter" Caption="Selection" DefaultControlID="edBeginDate">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True"  LabelsWidth="M" ControlSize="S" />

			<px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Expiration Period" />

			<px:PXDateTimeEdit CommitChanges="True" ID="edBeginDate" runat="server" DataField="BeginDate"  />
			<px:PXNumberEdit CommitChanges="True" ID="edExpireXDays" runat="server" AllowNull="False" DataField="ExpireXDays" />
			<px:PXLayoutRule runat="server" StartColumn="True"  LabelsWidth="S" ControlSize="XM" />
            <px:PXLabel runat="server"></px:PXLabel>
			<px:PXSelector CommitChanges="True" ID="edCustomerClassID" runat="server" DataField="CustomerClassID"  />
			<px:PXCheckBox CommitChanges="True" ID="chkDefaultOnly" runat="server" DataField="DefaultOnly" />
			<px:PXCheckBox CommitChanges="True" ID="chkNotificationSendOnly" runat="server" DataField="NotificationSendOnly" /></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="150px" 
		Style="z-index: 100; left: 0px; top: 0px;" Width="100%" Caption="Card List" 
		AllowSearch="True" AllowPaging="True" SkinID="Inquire">
		<Levels>
			<px:PXGridLevel  DataMember="Cards">
				<Columns>
					<px:PXGridColumn AllowNull="False" DataField="Selected" TextAlign="Center" Type="CheckBox" Width="60px" AllowCheckAll = "true" />
					<px:PXGridColumn DataField="BAccountID" DisplayFormat="&gt;AAAAAAAAAA" Width="100px" />
					<px:PXGridColumn AllowUpdate="False" DataField="Customer__AcctName" Width="200px" />
					<px:PXGridColumn AllowUpdate="False" DataField="Customer__CustomerClassID" DisplayFormat="&gt;aaaaaaaaaa"  />
					<px:PXGridColumn DataField="PaymentMethodID" DisplayFormat="&gt;aaaaaaaaaa"  />
					<px:PXGridColumn AllowNull="False" AllowUpdate="False" DataField="Descr" Width="200px" />
					<px:PXGridColumn AllowNull="False" DataField="IsActive" TextAlign="Center" Type="CheckBox" Width="50px" />
					<px:PXGridColumn AllowUpdate="False" DataField="ExpirationDate" Width="90px" />
					<px:PXGridColumn AllowUpdate="False" DataField="LastNotificationDate" Width="90px" />
					<px:PXGridColumn AllowUpdate="False" DataField="Contact__EMail" Width="200px" />
					<px:PXGridColumn AllowUpdate="False" DataField="Contact__Phone1" DisplayFormat="CCCCCCCCCCCCCCCCCCCC" Width="100px" />
					<px:PXGridColumn AllowUpdate="False" DataField="Contact__Fax" DisplayFormat="CCCCCCCCCCCCCCCCCCCC" Width="100px" />
					<px:PXGridColumn AllowUpdate="False" DataField="Contact__WebSite" Width="200px" />
					
				</Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
		<ActionBar>
			<Actions>
				<Save Enabled="False" />
			</Actions>
			<CustomItems>
				<px:PXToolBarButton Text="View Customer" Key="cmdViewCustomer">
				    <AutoCallBack Command="ViewCustomer" Target="ds" />
				</px:PXToolBarButton>
				<px:PXToolBarButton Text="View Payment Method" Key="cmdViewPaymentMethod">
				    <AutoCallBack Command="ViewPaymentMethod" Target="ds" />
				</px:PXToolBarButton>
			</CustomItems>
		</ActionBar>
		<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
	</px:PXGrid>
</asp:Content>
