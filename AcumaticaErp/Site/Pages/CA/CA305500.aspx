﻿<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CA305500.aspx.cs" Inherits="Page_CA305500" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="filter" TypeName="PX.Objects.CA.CashForecastEntry">
		<CallbackCommands>			
			<px:PXDSCallbackCommand Name="Cancel" Visible="True" />
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />			
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server"  Style="z-index: 100" Width="100%" DataMember="filter" Caption="Selection" TemplateContainer="">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="M" />
			<px:PXSelector CommitChanges="True" ID="edAccountID" runat="server" DataField="CashAccountID" />
			<px:PXDateTimeEdit CommitChanges="True" ID="edStartDate" runat="server" DataField="StartDate" />
		</Template>
	</px:PXFormView>
</asp:Content>

<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" 
		AllowPaging="True" ActionsPosition="Top" DataSourceID="ds" 
		SkinID="Details" TabIndex="300" Caption="Cash Transactions">
		<Levels>
			<px:PXGridLevel DataMember="cashForecastTrans">
				<Columns>
					<px:PXGridColumn DataField="CashAccountID" AutoCallBack="True">	</px:PXGridColumn>					
					<px:PXGridColumn DataField="TranDate" DataType="DateTime" Width="90px"></px:PXGridColumn>
					<px:PXGridColumn AllowNull="False" DataField="DrCr" RenderEditorText="True"></px:PXGridColumn>
					<px:PXGridColumn DataField="TranDesc" Width="200px"></px:PXGridColumn>
					<px:PXGridColumn AllowShowHide="False" DataField="CuryID"></px:PXGridColumn>
					<px:PXGridColumn AllowNull="False" DataField="CuryTranAmt" DataType="Decimal" TextAlign="Right" Width="100px"></px:PXGridColumn>					
				</Columns>
				<Layout FormViewHeight=""></Layout>
			</px:PXGridLevel>
		</Levels>		
		<ActionBar></ActionBar>
		<AutoSize Enabled="True" Container="Window" MinHeight="200"></AutoSize>		
		<Mode InitNewRow="True" AllowUpload="True"></Mode>
	</px:PXGrid>
</asp:Content>
