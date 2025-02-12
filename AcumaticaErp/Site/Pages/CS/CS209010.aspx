<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CS209010.aspx.cs" Inherits="Page_CS209010" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.CS.CSFilterMaint" PrimaryView="Filter">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
		    <px:PXDSCallbackCommand Name="MakeFilterNotShared" />
		</CallbackCommands>
		<DataTrees>
			<px:PXTreeDataMember TreeKeys="PageID" TreeView="Pages" />
			<px:PXTreeDataMember TreeView="SiteMap" TreeKeys="NodeID" />
		</DataTrees>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="frmHeader" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Filter" Caption="Filter Summary" TemplateContainer="" TabIndex="9700">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
			<px:PXSelector ID="edFilterID" runat="server" DataField="FilterID" ValueField="FilterID" TextField="FilterName" DataSourceID="ds" />
			<px:PXTextEdit ID="edName" runat="server" DataField="FilterName" />
			<px:PXTreeSelector CommitChanges="True" ID="edScreenID" runat="server" DataField="ScreenID" PopulateOnDemand="True" ShowRootNode="False" TreeDataSourceID="ds" TreeDataMember="SiteMap" AutoRefresh="True"
				MinDropWidth="357">
				<DataBindings>
					<px:PXTreeItemBinding DataMember="SiteMap" TextField="Title" ValueField="ScreenID" ImageUrlField="Icon" />
				</DataBindings>
			</px:PXTreeSelector>
			<px:PXSelector CommitChanges="True" ID="edViewName" runat="server" DataField="ViewName" ValueField="Name" TextField="DisplayName" AutoRefresh="True" DataSourceID="ds" />
			<px:PXLayoutRule runat="server" StartColumn="True" SuppressLabel="True" />
			<px:PXCheckBox ID="chkIsDefault" runat="server" DataField="IsDefault" />
			<px:PXCheckBox ID="chkIsShortcut" runat="server" DataField="IsShortcut" />
			<px:PXCheckBox ID="chkIsSystem" runat="server" DataField="IsSystem" />
		</Template>
		<CallbackCommands>
			<Save RepaintControlsIDs="grid" />
		</CallbackCommands>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Height="100%" Width="100%" ActionsPosition="Top" SkinID="Details" Caption="Details" RepaintColumns="true" MatrixMode="True" 
		TabIndex="10100">
		<Levels>
			<px:PXGridLevel DataMember="FilterDetails">
				<Mode InitNewRow="true" />
				<Columns>
					<px:PXGridColumn DataField="IsUsed" Type="CheckBox" Width="60px" TextAlign="Center" />
					<px:PXGridColumn DataField="OpenBrackets" Type="DropDownList" Width="50px" />
					<px:PXGridColumn DataField="DataField" Type="DropDownList" Width="200px" CommitChanges="True" />
					<px:PXGridColumn DataField="Condition" Type="DropDownList" Width="100px" CommitChanges="True" />
					<px:PXGridColumn DataField="ValueSt" Width="200px" />
					<px:PXGridColumn DataField="ValueSt2" Width="200px" />
					<px:PXGridColumn DataField="CloseBrackets" Type="DropDownList" Width="50px" />
					<px:PXGridColumn DataField="Operator" Type="DropDownList" Width="50px" />
				</Columns>
				<Layout FormViewHeight=""></Layout>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>
