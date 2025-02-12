<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
	ValidateRequest="false" CodeFile="SM205010.aspx.cs" Inherits="Page_SM205010"
	Title="Automation Definition Maintenance" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<script language="javascript" type="text/javascript">
		PXFormView.prototype.onAfterResize = function ()
		{
			var editor = px_all[txtID]; // txtID is registered by server
			var form = px_all[frmID]; // tabId is registered by server
			if (editor != null)
				editor.element.style.width = form.element.clientWidth - 5 + "px";
		}

		PXTextEdit.prototype.onResize = function ()
		{
			var editor = px_all[txtID].element; // txtID is registered by server
			var form = px_all[frmID].element; // frmID is registered by server
			if (form.clientHeight >= 5)
				editor.style.height = form.clientHeight - 5 + "px";
			return true;
		}
	</script>
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.SM.AUDefinitionMaint"
		PrimaryView="Definition">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
			<px:PXDSCallbackCommand Name="ViewScreen" DependOnGrid="grid" Visible="false" />
			<px:PXDSCallbackCommand Name="PreloadDetails" StartNewGroup="true" CommitChanges="true" />
			<px:PXDSCallbackCommand Name="CopyDetails" CommitChanges="true" />
			<px:PXDSCallbackCommand Name="StoreDefinition" CommitChanges="true" />
			<px:PXDSCallbackCommand Name="ActivateDefinition2" StartNewGroup="true" CommitChanges="true" />
			<px:PXDSCallbackCommand Name="ShowPopulated" Visible="false" />
		</CallbackCommands>
		<DataTrees>
			<px:PXTreeDataMember TreeView="SiteMap" TreeKeys="NodeID" />
		</DataTrees>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
		Caption="Automation Definition" DataMember="Definition" NoteIndicator="True"
		FilesIndicator="True" TemplateContainer="">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" ControlSize="M" 
				LabelsWidth="SM" />
			<px:PXSelector ID="edDefinitionID" runat="server" DataField="DefinitionID"
				AutoRefresh="True" DataSourceID="ds" />
			<px:PXTextEdit ID="edDescription" runat="server" DataField="Description" />
			<px:PXLayoutRule runat="server" StartColumn="True" ControlSize="M" 
				LabelsWidth="SM" />
			<px:PXButton Size="s" ID="btnShowXml" runat="server" Text="Show Populated" CommandSourceID="ds"
				CommandName="ShowPopulated" ToolTip="Shows populated definition in XML form."
				Height="20px" AlignLeft="True" />
		</Template>
	</px:PXFormView>
	<px:PXSmartPanel ID="pnlViewXml" runat="server" Height="600px" Width="830px" Caption="Populated Definition XML"
		CaptionVisible="true" Key="PopupDefinition" AutoCallBack-Enabled="true"
		AutoCallBack-Command="Refresh" AutoCallBack-Target="frmViewXml">
		<px:PXFormView ID="frmViewXml" runat="server" DataSourceID="ds" Height="600px" Width="100%"
			CaptionVisible="False" DataMember="PopupDefinition">
			<ContentStyle BackColor="Transparent" BorderStyle="None">
			</ContentStyle>
			<Template>
				<px:PXTextEdit ID="edDetailsXml" runat="server" DataField="DetailsXml" Height="595px"
					LabelID="lblDetailsXml" Style="z-index: 101; border-style: none;" TextMode="MultiLine"
					Width="795px" SelectOnFocus="false">
					<AutoSize Enabled="true" />
				</px:PXTextEdit>
			</Template>
			<AutoSize Enabled="true" />
		</px:PXFormView>
	</px:PXSmartPanel>
	<px:PXSmartPanel ID="pnlCopyDetails" runat="server" Style="z-index: 108;
		left: 351px; position: absolute; top: 99px" Width="423px" Caption="Specify Source Definition"
		CaptionVisible="true" DesignView="Hidden" AutoCallBack-Command="Refresh" AutoCallBack-Enabled="True"
		AutoCallBack-Target="formCopyDetails" CallBackMode-CommitChanges="True" CallBackMode-PostData="Page"
		LoadOnDemand="true" Key="Filter">
		<div style="padding: 5px">
			<px:PXFormView ID="formCopyDetails" runat="server" DataSourceID="ds" Style="z-index: 100"
				Width="100%" CaptionVisible="False" DataMember="Filter">
				<ContentStyle BackColor="Transparent" BorderStyle="None" />
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
					<px:PXLabel ID="lblCopyCaution" runat="server">Caution : All current definition details will be deleted.</px:PXLabel>
					<px:PXSelector ID="edSourceDefinitionID" runat="server" DataField="SourceDefinitionID"
						AutoRefresh="true" />
				</Template>
			</px:PXFormView>
		</div>
		<px:PXPanel ID="PXPanel1" runat="server" SkinID="Buttons">
			<px:PXButton ID="btnOK" runat="server" DialogResult="OK" Text="OK" Width="63px" Height="20px">
				<AutoCallBack Target="formCopyDetails" Command="Save" />
			</px:PXButton>
		</px:PXPanel>
	</px:PXSmartPanel>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="150px" Style="z-index: 100"
		Width="100%" AdjustPageSize="Auto" AllowSearch="True" SkinID="Details" Caption="Screens">
		<ActionBar>
		</ActionBar>
		<Levels>
			<px:PXGridLevel DataMember="Details">
				<RowTemplate>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
					<px:PXTreeSelector SuppressLabel="True" ID="edScreenID" runat="server" DataField="ScreenID"
						PopulateOnDemand="True" ShowRootNode="False" TreeDataSourceID="ds" TreeDataMember="SiteMap"
						MinDropWidth="413">
						<DataBindings>
							<px:PXTreeItemBinding DataMember="SiteMap" TextField="Title" ValueField="ScreenID"
								ImageUrlField="Icon" />
						</DataBindings>
					</px:PXTreeSelector>
				</RowTemplate>
				<Columns>
					<px:PXGridColumn DataField="ScreenID" DisplayFormat="CC.CC.CC.CC" Width="208px" RenderEditorText="true"
						TextField="ScreenDescription" />
					<px:PXGridColumn AllowUpdate="False" DataField="Steps" Width="508px" />
				</Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Enabled="True" Container="Window" MinHeight="150" />
		<ActionBar>
			<CustomItems>
				<px:PXToolBarButton Text="View Screen" Key="cmdViewScreen">
					<AutoCallBack Enabled="True" Target="ds" Command="ViewScreen" />
				</px:PXToolBarButton>
			</CustomItems>
		</ActionBar>
	</px:PXGrid>
</asp:Content>
