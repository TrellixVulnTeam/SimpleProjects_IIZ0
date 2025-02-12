<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true"
	ValidateRequest="false" CodeFile="CR308000.aspx.cs" Inherits="Page_CR308000"
	Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="MassMails"
		TypeName="PX.Objects.CR.CRMassMailMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" PostData="Page" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
			<px:PXDSCallbackCommand Name="previewMail" CommitChanges="true" PostData="Page" />
			<px:PXDSCallbackCommand Name="send" CommitChanges="true" SelectControlsIDs="tab" />
			<px:PXDSCallbackCommand Name="messageDetails" Visible="false" DependOnGrid="mailHistory" />
		</CallbackCommands>
		<DataTrees>
			<px:PXTreeDataMember TreeKeys="Key" TreeView="EntityItems" />
		</DataTrees>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXTab ID="tab" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%"
		DataMember="MassMails" NoteIndicator="True" FilesIndicator="True" ActivityIndicator="true"
		ActivityField="NoteActivity" LinkIndicator="true" NotifyIndicator="true" 
		DefaultControlID="edMassMailCD">
		<AutoSize Container="Window" Enabled="True" />
		<Items>
			<px:PXTabItem Text="Summary">
				<Template>
					<px:PXPanel ID="PXPanel1" runat="server">
						<px:PXLayoutRule ID="PXLayoutRule4" runat="server" StartColumn="True" LabelsWidth="S"
							ControlSize="M" />
						<px:PXSelector ID="edMassMailCD" runat="server" DataField="MassMailCD" />
						<px:PXSelector ID="edMailFrom" runat="server" DataField="MailAccountID" DisplayMode="Text" />
						<px:PXTreeSelector ID="edMailSubject" runat="server" DataField="MailSubject" TreeDataSourceID="ds"
							TreeDataMember="EntityItems" PopulateOnDemand="True" InitialExpandLevel="0" ShowRootNode="false"
							MinDropWidth="468" MaxDropWidth="600" AllowEditValue="true" AppendSelectedValue="true"
							AutoRefresh="true">
							<DataBindings>
								<px:PXTreeItemBinding TextField="Name" ValueField="Path" ImageUrlField="Icon" ToolTipField="Path" />
							</DataBindings>
						</px:PXTreeSelector>
						<px:PXTreeSelector ID="edMailTo" runat="server" DataField="MailTo" TreeDataSourceID="ds"
							TreeDataMember="EntityItems" PopulateOnDemand="True" InitialExpandLevel="0" ShowRootNode="false"
							MinDropWidth="468" MaxDropWidth="600" AllowEditValue="true" AppendSelectedValue="true"
							AutoRefresh="true">
							<DataBindings>
								<px:PXTreeItemBinding TextField="Name" ValueField="Path" ImageUrlField="Icon" ToolTipField="Path" />
							</DataBindings>
						</px:PXTreeSelector>
						<px:PXTreeSelector ID="edMailCc" runat="server" DataField="MailCc" TreeDataSourceID="ds"
							TreeDataMember="EntityItems" PopulateOnDemand="True" InitialExpandLevel="0" ShowRootNode="false"
							MinDropWidth="468" MaxDropWidth="600" AllowEditValue="true" AppendSelectedValue="true"
							AutoRefresh="true">
							<DataBindings>
								<px:PXTreeItemBinding TextField="Name" ValueField="Path" ImageUrlField="Icon" ToolTipField="Path" />
							</DataBindings>
						</px:PXTreeSelector>
						<px:PXTreeSelector ID="edMailBcc" runat="server" DataField="MailBcc" TreeDataSourceID="ds"
							TreeDataMember="EntityItems" PopulateOnDemand="True" InitialExpandLevel="0" ShowRootNode="false"
							MinDropWidth="468" MaxDropWidth="600" AllowEditValue="true" AppendSelectedValue="true"
							AutoRefresh="true">
							<DataBindings>
								<px:PXTreeItemBinding TextField="Name" ValueField="Path" ImageUrlField="Icon" ToolTipField="Path" />
							</DataBindings>
						</px:PXTreeSelector>
						<px:PXLayoutRule ID="PXLayoutRule25" runat="server" StartColumn="True" LabelsWidth="S"
							ControlSize="M" />
						<px:PXDropDown CommitChanges="True" ID="edSource" runat="server" AllowNull="False"
							DataField="Source" />
						<px:PXDateTimeEdit ID="edPlannedDate" runat="server" DataField="PlannedDate" />
						<px:PXDropDown ID="edStatus" runat="server" AllowNull="False" DataField="Status"
							Size="S" />
						<px:PXDateTimeEdit ID="edSentDateTime" runat="server" DataField="SentDateTime" DisplayFormat="g"
							EditFormat="g" Enabled="False" />
					</px:PXPanel>
					<pxa:PXRichTextEdit ID="wikiEdit" runat="server" Style="border-width: 0px; width: 100%;
						height: 100%;" DataField="MailContent" FilesContainer="message" AllowImageEditor="true"
						AllowLinkEditor="true" AllowLoadTemplate="true" AllowInsertParameter="true">
						<AutoSize Enabled="True" />
						<LoadTemplate TypeName="PX.SM.SMNotificationMaint" DataMember="Notifications" ViewName="NotificationTemplate" ValueField="notificationID" TextField="Name" DataSourceID="ds" Size="M"/>
						<InsertParameter DataSourceID="ds" DataMember="EntityItems" TextField="Name" ValueField="Path"
							ImageField="Icon" />
						<ImageEditor DataSourceID="ds" DataMember="$NoteImages$CRMassMail" ValueField="FileID"
							TextField="Name">
							<Columns>
								<px:PXGridColumn DataField="FileID" Visible="false" AllowShowHide="False" />
								<px:PXGridColumn DataField="Name" Width="400px" AllowShowHide="False" />
							</Columns>
						</ImageEditor>
						<LinkEditor TypeName="PX.SM.WikiArticlesTree" DataMember="Articles" ValueField="pageID"
							TextField="Title" />
					</pxa:PXRichTextEdit>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Leads/Contacts/Employees" VisibleExp="DataControls[&quot;edSource&quot;].Value == 2"
				LoadOnDemand="true">
				<AutoCallBack Command="Refresh" Target="leads" />
				<Template>
					<px:PXFilterEditor ID="mainFilter" runat="server" SkinID="External" FilterView="Leads$FilterHeader" FilterRowsView="Leads$FilterRow"
						DataSourceID="ds" Width="600px" Style="position: relative" LinkedGridID="leads" ShowDefaultFilter="true">
					</px:PXFilterEditor>
					<px:PXGrid ID="leads" runat="server" DataSourceID="ds" Height="150px" Style="z-index: 100;
						position: relative; border-bottom: 0px; border-right: 0px; border-left: 0px;"
						Width="100%" ActionsPosition="Top" AllowPaging="True" SkinID="Inquire" ExternalFilter="true">
						<Levels>
							<px:PXGridLevel DataMember="Leads">
								<Columns>
									<px:PXGridColumn AllowCheckAll="True" AllowNull="False" AllowShowHide="False" DataField="Selected"
										TextAlign="Center" Type="CheckBox" Width="40px" />
									<px:PXGridColumn DataField="ContactType" Width="54px" />
									<px:PXGridColumn AllowUpdate="False" DataField="DisplayName" Width="280px">
										<NavigateParams>
											<px:PXControlParam Name="ContactID" ControlID="leads" PropertyName="DataValues[&quot;ContactID&quot;]" />
										</NavigateParams>
									</px:PXGridColumn>
									<px:PXGridColumn DataField="FullName" Width="100px" />
									<px:PXGridColumn DataField="BAccount__ClassID" Width="60px" Visible="False" />
									<px:PXGridColumn AllowNull="False" DataField="IsActive" TextAlign="Center" Type="CheckBox"
										Width="60px" Visible="False" />
									<px:PXGridColumn AllowNull="False" DataField="ClassID" RenderEditorText="True" TextAlign="Center"
										Width="60px" Visible="False" />
									<px:PXGridColumn DataField="Source" Width="54px" Visible="False" />
									<px:PXGridColumn DataField="Status" Width="90px" />
									<px:PXGridColumn DataField="Title" Width="54px" Visible="False" />
									<px:PXGridColumn DataField="Salutation" Width="160px" />
									<px:PXGridColumn AllowUpdate="False" DataField="ContactID" Visible="false" AllowShowHide="False" />
									<px:PXGridColumn DataField="EMail" Width="200px" />
									<px:PXGridColumn DataField="Address__AddressLine1" Width="90px" Visible="False" />
									<px:PXGridColumn DataField="Address__AddressLine2" Width="90px" Visible="False" />
									<px:PXGridColumn DataField="Phone1" DisplayFormat="+#(###) ###-####" Width="140px" />
									<px:PXGridColumn DataField="Phone2" DisplayFormat="+#(###) ###-####" Width="140px" />
									<px:PXGridColumn DataField="Phone3" DisplayFormat="+#(###) ###-####" Width="140px" />
									<px:PXGridColumn DataField="Fax" DisplayFormat="+#(###) ###-####" Width="140px" />
									<px:PXGridColumn DataField="WebSite" Width="140px" />
									<px:PXGridColumn DataField="DateOfBirth" Width="90px" />
									<px:PXGridColumn DataField="CreatedByID_Creator_Username" Width="108px" />
									<px:PXGridColumn DataField="LastModifiedByID_Modifier_Username" Width="108px" />
									<px:PXGridColumn DataField="CreatedDateTime" Width="90px" />
									<px:PXGridColumn DataField="LastModifiedDateTime" Width="90px" />
									<px:PXGridColumn DataField="WorkgroupID" Width="90px" />
									<px:PXGridColumn DataField="OwnerID" Width="90px" DisplayMode="Text" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="150" />
						<ActionBar PagerVisible="False">
							<Actions>
								<Save Enabled="False" />
								<AddNew Enabled="False" />
								<Delete Enabled="False" />
								<EditRecord Enabled="False" />
								<FilterShow Enabled="False" />
								<FilterSet Enabled="False" />
							</Actions>
						</ActionBar>
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Campaigns" VisibleExp="DataControls[&quot;edSource&quot;].Value == 1"
				LoadOnDemand="true">
				<AutoCallBack Command="Refresh" Target="campaigns" />
				<Template>
					<px:PXGrid ID="campaigns" runat="server" DataSourceID="ds" Style="z-index: 105;"
						Width="100%" BorderStyle="None" AdjustPageSize="Auto" BorderWidth="0px" SkinID="Inquire">
						<AutoSize Enabled="True" />
						<Levels>
							<px:PXGridLevel DataMember="Campaigns">
								<Columns>
									<px:PXGridColumn AllowNull="False" DataField="Selected" TextAlign="Center" Type="CheckBox"
										Width="60px" AllowCheckAll="true" />
									<px:PXGridColumn AllowUpdate="False" DataField="CampaignID" Width="200px" />
									<px:PXGridColumn AllowUpdate="False" DataField="CampaignName" Width="200px" />
									<px:PXGridColumn AllowNull="False" AllowUpdate="False" DataField="CampaignType" Width="300px" />
									<px:PXGridColumn DataField="TargetStatus" Width="100px" Type="DropDownList" />
									<px:PXGridColumn DataField="DestinationStatus" Width="100px" Type="DropDownList" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="True" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Marketing List" VisibleExp="DataControls[&quot;edSource&quot;].Value == 0"
				LoadOnDemand="true">
				<AutoCallBack Command="Refresh" Target="mailList" />
				<Template>
					<px:PXGrid ID="mailList" runat="server" DataSourceID="ds" Style="z-index: 105;" Width="100%"
						BorderStyle="None" AdjustPageSize="Auto" BorderWidth="0px" SkinID="Inquire">
						<AutoSize Enabled="True" />
						<Levels>
							<px:PXGridLevel DataMember="MailLists">
								<Columns>
									<px:PXGridColumn AllowNull="False" DataField="Selected" TextAlign="Center" Type="CheckBox"
										Width="60px" AllowCheckAll="True" />
									<px:PXGridColumn AllowUpdate="False" DataField="MailListCode" DisplayFormat="&gt;aaaaaaaaaa"
										Width="200px" />
									<px:PXGridColumn AllowNull="False" AllowUpdate="False" DataField="Name" Width="400px" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Messages" LoadOnDemand="true">
				<Template>
					<px:PXGrid ID="mailHistory" runat="server" DataSourceID="ds" Style="z-index: 105;"
						Width="100%" BorderStyle="None" AdjustPageSize="Auto" ActionsPosition="Top" BorderWidth="0px"
						SkinID="Details">
						<AutoSize Enabled="True" />
						<ActionBar DefaultAction="cmdMessageDetails">
							<CustomItems>
								<px:PXToolBarButton Key="cmdMessageDetails" Visible="false">
									<ActionBar GroupIndex="0" />
									<AutoCallBack Command="messageDetails" Target="ds" />
								</px:PXToolBarButton>
							</CustomItems>
							<Actions>
								<AddNew Enabled="false" />
								<EditRecord Enabled="false" />
								<Delete Enabled="false" />
							</Actions>
						</ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="History">
								<Columns>
                                <px:PXGridColumn DataField="Subject" Width="450px" LinkCommand="Outbox_ViewDetails" />
                                <px:PXGridColumn DataField="MailTo" Width="250px"/>	
					            <px:PXGridColumn DataField="ProcessDate" DisplayFormat="g" Width="120px" />	
					            <px:PXGridColumn DataField="MPStatus" Width="70px" />
					            <px:PXGridColumn DataField="Source" Width="100px" SyncVisible="False" SyncVisibility="False" Visible="False"/>
					            <px:PXGridColumn DataField="Source_Description" Width="150px" LinkCommand="viewEntity" SyncVisible="False" SyncVisibility="False" Visible="True"/>				
					            </Columns> 
				            </px:PXGridLevel>							
						</Levels>
						<Mode AllowDelete="false" AllowAddNew="false" AllowUpdate="false" />
						<AutoSize Enabled="true" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
		</Items>
	</px:PXTab>
	<px:PXSmartPanel ID="pnlEmailPreview" runat="server" Height="117px" Width="378px" Style="z-index: 108; left: 315px; position: absolute; top: 399px" Caption="Preview Message"
		CaptionVisible="true" DesignView="Content" LoadOnDemand="true" Key="Preview"
		AutoCallBack-Enabled="true" AutoCallBack-Command="Refresh" AutoCallBack-Target="frmEmailPreview">
		<px:PXFormView ID="frmEmailPreview" runat="server" DataSourceID="ds" Style="z-index: 100;"
			Width="100%" SkinID="Transparent" DataMember="Preview">
			<Template>
				<px:PXLayoutRule ID="smLayoutRule" runat="server" StartColumn="True" LabelsWidth="XS" ControlSize="XM"/>
				<px:PXSelector ID="edMailFrom" runat="server" DataField="MailAccountID" DisplayMode="Text" />
				<px:PXTextEdit ID="edMailTo" runat="server" DataField="MailTo" />
			</Template>
		</px:PXFormView>
        <px:PXPanel ID="PXPanel1" runat="server" SkinID="Buttons" Style="margin-right: 25px;">
			<px:PXButton ID="btnSave" runat="server" DialogResult="OK" Text="OK"  Width="63px" Height="20px"/>
			<px:PXButton ID="btnCancel" runat="server" DialogResult="Cancel" Text="Cancel"  Width="63px" Height="20px" Style="margin-left: 5px"/>
        </px:PXPanel>
	</px:PXSmartPanel>
</asp:Content>
