<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
	ValidateRequest="false" CodeFile="CR406000.aspx.cs" Inherits="Page_CR406000"
	Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.CR.CampaignEnq"
		PrimaryView="Filter" PageLoadBehavior="PopulateSavedValues">
		<CallbackCommands>
			<px:PXDSCallbackCommand Visible="false" DependOnGrid="grid" Name="FilteredItems_ViewDetails" />
			<pxa:PXExtendedDSCallbackCommand Name="AddNewLead" ForDashboard="true" />
		</CallbackCommands>
		<DataTrees>
			<px:PXTreeDataMember TreeView="_EPCompanyTree_Tree_" TreeKeys="WorkgroupID" />
		</DataTrees>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Width="100%" DataMember="Filter" Caption="Selection">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True"/>
			<px:PXLayoutRule ID="PXLayoutRule3" runat="server" Merge="True" ControlSize="M"/>
			<px:PXSelector ID="edOwnerID" runat="server" DataField="OwnerID" AutoRefresh="True" CommitChanges="True"/>
			<px:PXCheckBox  ID="chkMyOwner" runat="server" Checked="True" DataField="MyOwner" CommitChanges="True"/>
			<px:PXLayoutRule ID="PXLayoutRule4" runat="server" Merge="False" />
			<px:PXLayoutRule ID="PXLayoutRule5" runat="server" Merge="True" ControlSize="M"/>
			<px:PXSelector ID="edWorkGroupID" runat="server" DataField="WorkGroupID" CommitChanges="True" AutoRefresh="True"/>
			<px:PXCheckBox ID="chkMyWorkGroup" runat="server" DataField="MyWorkGroup" CommitChanges="True"/>
			<px:PXLayoutRule ID="PXLayoutRule7"  runat="server" Merge="False" />
			<px:PXLayoutRule ID="PXLayoutRule2" runat="server" StartColumn="True" LabelsWidth="S"
				ControlSize="XM" SuppressLabel="False" />
			<px:PXSelector CommitChanges="True" ID="edCampaignID" runat="server" DataField="CampaignID"
				AllowEdit="true" DisplayMode="Hint" />
			<px:PXDropDown CommitChanges="True" ID="edStatus" runat="server" DataField="Status" Size="S" />
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="150px" Style="z-index: 100"
		Width="100%" ActionsPosition="Top" Caption="Campaign Members" AllowPaging="true"
		AdjustPageSize="auto" SkinID="Inquire" FastFilterFields="DisplayName,FullName" RestrictFields="True">
		<Levels>
			<px:PXGridLevel DataMember="FilteredItems">
				<Columns>
					<px:PXGridColumn DataField="CRCampaign__CampaignID" Width="80px" />
					<px:PXGridColumn DataField="CRCampaign__CampaignName" Width="120px" />
					<px:PXGridColumn DataField="CRCampaign__Status" Width="54px" />
					<px:PXGridColumn DataField="ContactType" Width="54px" RenderEditorText="True" />
					<px:PXGridColumn DataField="DisplayName" Width="130px" LinkCommand="FilteredItems_ViewDetails" />
					<px:PXGridColumn DataField="Title" Width="50px" />
					<px:PXGridColumn DataField="FirstName" Width="100px" />
					<px:PXGridColumn DataField="MidName" Width="100px" />
					<px:PXGridColumn DataField="LastName" Width="100px" />
					<px:PXGridColumn DataField="Salutation" Width="180px" />
					<px:PXGridColumn DataField="BAccount__AcctCD" Width="150px" LinkCommand="FilteredItems_BAccount_ViewDetails" />
					<px:PXGridColumn DataField="FullName" Width="200px" />
					<px:PXGridColumn DataField="IsActive" Width="60px" Type="CheckBox"/>
					<px:PXGridColumn DataField="Status" Width="90px" />
					<px:PXGridColumn DataField="Resolution" Width="90px" />
					<px:PXGridColumn DataField="ClassID" Width="90px" />
					<px:PXGridColumn DataField="Source" Width="90px" />
					<px:PXGridColumn DataField="State__name" Width="90px" />
					<px:PXGridColumn DataField="Address__City" Width="90px" />
					<px:PXGridColumn DataField="Address__AddressLine1" Width="300px" />
					<px:PXGridColumn DataField="Address__AddressLine2" Width="300px" />
					<px:PXGridColumn DataField="EMail" Width="190px" />
					<px:PXGridColumn DataField="Phone1" DisplayFormat="+#(###) ###-####" Width="130px" />
					<px:PXGridColumn DataField="Phone2" DisplayFormat="+#(###) ###-####" Width="130px" />
					<px:PXGridColumn DataField="Phone3" DisplayFormat="+#(###) ###-####" Width="130px" />
					<px:PXGridColumn DataField="Fax" DisplayFormat="+#(###) ###-####" Width="130px" />
					<px:PXGridColumn DataField="WorkgroupID" Width="90px" />
					<px:PXGridColumn DataField="OwnerID" Width="90px" DisplayMode="Text"/>
					<px:PXGridColumn DataField="CreatedByID_Creator_Username" Width="100px" SyncVisible="False" SyncVisibility="False" Visible="False" />
					<px:PXGridColumn DataField="CreatedDateTime" Width="90px" />
					<px:PXGridColumn DataField="LastModifiedByID_Modifier_Username" Width="100px" SyncVisible="False" SyncVisibility="False" Visible="False" />
					<px:PXGridColumn DataField="LastModifiedDateTime" Width="90px" />
				</Columns>
			</px:PXGridLevel>
		</Levels>
		<ActionBar DefaultAction="cmdItemDetails" PagerVisible="False">
			<PagerSettings Mode="NextPrevFirstLast" />
			<CustomItems>
				<px:PXToolBarButton Text="View Details" Tooltip="View Details" Key="cmdItemDetails"
					Visible="false">
					<Images Normal="main@DataEntry" />
					<AutoCallBack Command="FilteredItems_ViewDetails" Target="ds">
						<Behavior CommitChanges="True" />
					</AutoCallBack>	 
					<ActionBar GroupIndex="0" />
				</px:PXToolBarButton> 
			</CustomItems>
		</ActionBar>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
		<CallbackCommands>
			<Refresh CommitChanges="True" PostData="Page" />
		</CallbackCommands>
	</px:PXGrid>
</asp:Content>
