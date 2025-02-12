<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AP403000.aspx.cs" Inherits="Page_AP403000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter" TypeName="PX.Objects.AP.APPendingInvoicesEnq">
		<CallbackCommands>
			<px:PXDSCallbackCommand DependOnGrid="grid" Name="ProcessPayment" Visible="false" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Filter" Caption="Selection" DefaultControlID="edPayAccountID">
		<Activity HighlightColor="" SelectedColor="" Width="" Height=""></Activity>
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
			<px:PXSegmentMask CommitChanges="True" ID="edPayAccountID" runat="server" DataField="PayAccountID" />
			<px:PXSelector CommitChanges="True" ID="edPayTypeID" runat="server" DataField="PayTypeID" />
			<px:PXDateTimeEdit CommitChanges="True" ID="edPayDate" runat="server" DataField="PayDate" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
			<px:PXSelector ID="edCuryID" runat="server" DataField="CuryID" Enabled="False" />
			<px:PXNumberEdit ID="edBalance" runat="server" DataField="Balance" Enabled="False" />
			<px:PXNumberEdit ID="edCuryBalance" runat="server" DataField="CuryBalance" Enabled="False" />
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="150px" Style="z-index: 100" Width="100%" Caption="Pending Bills Summary" SkinID="Inquire"  TabIndex="8500" RestrictFields="True">
		<Levels>
			<px:PXGridLevel DataMember="Documents">
				<Columns>
					<px:PXGridColumn DataField="PayAccountID" LinkCommand = "ProcessPayment"/>
					<px:PXGridColumn DataField="PayAccountID_CashAccount_Descr" Width="200px" />
					<px:PXGridColumn DataField="PayTypeID" />
					<px:PXGridColumn DataField="CuryID" />
					<px:PXGridColumn DataField="DocCount" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryDocBal" TextAlign="Right" Width="100px" />
					<px:PXGridColumn DataField="DocBal" TextAlign="Right" Width="100px" AllowShowHide="Server" />
					<px:PXGridColumn DataField="CuryDiscBal" TextAlign="Right" Width="100px" AllowShowHide="Server" />
					<px:PXGridColumn DataField="DiscBal" TextAlign="Right" Width="100px" AllowShowHide="Server" />
					<px:PXGridColumn DataField="OverdueDocCount" TextAlign="Right" />
					<px:PXGridColumn DataField="OverdueDocBal" TextAlign="Right" Width="100px" AllowShowHide="Server" />
					<px:PXGridColumn DataField="OverdueCuryDocBal" TextAlign="Right" Width="100px"/>
					<px:PXGridColumn DataField="ValidDiscCount" TextAlign="Right" />
					<px:PXGridColumn DataField="ValidDiscBal" TextAlign="Right" Width="100px" AllowShowHide="Server" />
					<px:PXGridColumn DataField="ValidCuryDiscBal" TextAlign="Right" Width="100px" />
					<px:PXGridColumn DataField="LostDiscCount" TextAlign="Right" />
					<px:PXGridColumn DataField="LostDiscBal" TextAlign="Right" Width="100px" AllowShowHide="Server" />
					<px:PXGridColumn DataField="LostCuryDiscBal" TextAlign="Right" Width="100px" />
					<px:PXGridColumn DataField="MinPayDate" Width="90px" />
					<px:PXGridColumn DataField="MaxPayDate" Width="90px" />
				</Columns>

<Layout FormViewHeight=""></Layout>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
		<ActionBar DefaultAction="cmdViewDoc">
			<CustomItems>
				<px:PXToolBarButton Text="Process Payment" Key="cmdViewDoc">
				    <AutoCallBack Command="ProcessPayment" Target="ds" />
				</px:PXToolBarButton>
			</CustomItems>
		</ActionBar>
	</px:PXGrid>
</asp:Content>
