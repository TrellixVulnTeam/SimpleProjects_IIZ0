<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
	ValidateRequest="false" CodeFile="CR302000.aspx.cs" Inherits="Page_CR302000"
	Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Contact"
		TypeName="PX.Objects.CR.ContactMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Cancel" PopupVisible="true" />
			<px:PXDSCallbackCommand Name="Delete" PopupVisible="true" />
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand Name="SaveClose" CommitChanges="True" Visible="false" PopupVisible="true"
				ClosePopup="true" />
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" PopupVisible="true" />
			<px:PXDSCallbackCommand Name="First" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="Action" StartNewGroup="true" CommitChanges="true" />
			<px:PXDSCallbackCommand Name="Merge" Visible="False" />
			<px:PXDSCallbackCommand Name="AddOpportunity" CommitChanges="True" Visible="False" />
			<px:PXDSCallbackCommand Name="Opportunities_ViewDetails" Visible="False" DependOnGrid="gridOpportunities" />
			<px:PXDSCallbackCommand Name="Opportunities_Contact_ViewDetails" Visible="False"
				DependOnGrid="gridOpportunities" />
			<px:PXDSCallbackCommand Name="AddCase" CommitChanges="True" Visible="False" />
			<px:PXDSCallbackCommand Name="Cases_ViewDetails" Visible="False" DependOnGrid="gridCases" />
			<px:PXDSCallbackCommand Name="copyBAccountContactInfo" CommitChanges="True" Visible="False" />
			<px:PXDSCallbackCommand Name="NewTask" Visible="False" CommitChanges="True" PopupCommand="Cancel"
				PopupCommandTarget="ds" />
			<px:PXDSCallbackCommand Name="NewEvent" Visible="False" CommitChanges="True" PopupCommand="Cancel"
				PopupCommandTarget="ds" />
			<px:PXDSCallbackCommand Name="NewActivity" Visible="False" CommitChanges="True" PopupCommand="Cancel"
				PopupCommandTarget="ds" />
			<px:PXDSCallbackCommand Name="NewMailActivity" Visible="False" CommitChanges="True"
				PopupCommand="Cancel" PopupCommandTarget="ds" />
			<px:PXDSCallbackCommand Name="ViewActivity" Visible="False" CommitChanges="True"
				PopupCommand="Cancel" PopupCommandTarget="ds" DependOnGrid="gridActivities" />
			<px:PXDSCallbackCommand Name="OpenActivityOwner" Visible="False" CommitChanges="True"
				DependOnGrid="gridActivities" />
			<px:PXDSCallbackCommand Name="ViewOnMap" CommitChanges="true" Visible="false" />
			<px:PXDSCallbackCommand Name="ValidateAddress" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="Members_CRCampaign_ViewDetails" Visible="False" CommitChanges="True"
				DependOnGrid="grdCampaignHistory" />
			<px:PXDSCallbackCommand Name="Subscriptions_CRMarketingList_ViewDetails" Visible="False"
				CommitChanges="True" DependOnGrid="grdMarketingLists" />
			<px:PXDSCallbackCommand Name="Relations_EntityDetails" Visible="False" CommitChanges="True"
				DependOnGrid="grdRelations" />
			<px:PXDSCallbackCommand Name="Relations_ContactDetails" Visible="False" CommitChanges="True"
				DependOnGrid="grdRelations" />
			<px:PXDSCallbackCommand Name="ResetPassword" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ResetPasswordOK" Visible="False" CommitChanges="True" />

			<px:PXDSCallbackCommand Name="ActivateLogin" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="EnableLogin" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="DisableLogin" Visible="False" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="UnlockLogin" Visible="False" CommitChanges="True" />
            <px:PXDSCallbackCommand Name="CheckForDuplicates" CommitChanges="True" Visible="False" />
            <px:PXDSCallbackCommand Name="ConvertToBAccount" CommitChanges="True" Visible="False" />
            <px:PXDSCallbackCommand Visible="false" DependOnGrid="PXGridDuplicates" Name="Duplicates_Contact_ViewDetails" />
			<px:PXDSCallbackCommand Visible="false" DependOnGrid="PXGridDuplicates" Name="Duplicates_BAccount_ViewDetails" />	
            <px:PXDSCallbackCommand Name="AttachToAccount"  Visible="False" CommitChanges="True" DependOnGrid="PXGridDuplicates"/>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" Width="100%" Caption="Contact Summary"
		DataMember="Contact" DataSourceID="ds" NoteIndicator="True" FilesIndicator="True"
		LinkIndicator="True" NotifyIndicator="True" DefaultControlID="edContactID">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
			<px:PXSelector ID="edContactID" runat="server" DataField="ContactID" NullText="<NEW>"
				DisplayMode="Text" TextMode="Search" FilterByAllFields="True" AutoRefresh="True" />
			<px:PXDropDown ID="edContactType" runat="server" DataField="ContactType"/>
            <px:PXCheckBox ID="chkIsActive" runat="server" Checked="True" DataField="IsActive" CommitChanges="True">
                <AutoCallBack Enabled="true" ActiveBehavior="True">
                    <Behavior CommitChanges="True" RepaintControls="All" RepaintControlsIDs="form,tab,frmLogin"/>
                </AutoCallBack>
            </px:PXCheckBox>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
			<px:PXSelector ID="WorkgroupID" runat="server" DataField="WorkgroupID" CommitChanges="True"
				TextMode="Search" DisplayMode="Text" FilterByAllFields="True" />
			<px:PXSelector ID="OwnerID" runat="server" DataField="OwnerID" TextMode="Search"
				DisplayMode="Text" FilterByAllFields="True" />
            <px:PXDropDown ID="edDuplicateStatus" runat="server" DataField="DuplicateStatus" CommitChanges="True" Enabled="False"/>
            <px:PXCheckBox ID="edDuplicateFound" runat="server" DataField="DuplicateFound" Visible="False"/>
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" DataSourceID="ds" Height="500px" DataMember="ContactCurrent"
		Width="100%">
		<Items>
			<px:PXTabItem Text="Details" RepaintOnDemand="False">
				<Template>
					<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM"
						StartColumn="True" />
					<px:PXLayoutRule runat="server" GroupCaption="Summary" StartGroup="True" />
					<px:PXLayoutRule runat="server" Merge="True" />
					<px:PXLabel ID="LFirstName" runat="server" Size="SM" />
					<px:PXDropDown ID="Title" runat="server" DataField="Title" Size="XS" SuppressLabel="True" CommitChanges="True" />
					<px:PXTextEdit ID="FirstName" runat="server" DataField="FirstName" Size="Empty" Width="164px" 
					LabelID="LFirstName" CommitChanges="True" />
					<px:PXLayoutRule runat="server" />
					<px:PXTextEdit ID="edLastName" runat="server" DataField="LastName" SuppressLabel="False"
						CommitChanges="True" />
					<px:PXTextEdit ID="edSalutation" runat="server" DataField="Salutation" SuppressLabel="False" />
					<px:PXSegmentMask ID="edBAccountID" runat="server" AllowEdit="True" CommitChanges="True"
						DataField="BAccountID" FilterByAllFields="True" TextMode="Search" DataSourceID="ds" />
					<px:PXTextEdit ID="edCompanyName" runat="server" DataField="FullName" />
					<px:PXSegmentMask ID="edParentBAccountID" runat="server" AllowEdit="True" DataField="ParentBAccountID"
						FilterByAllFields="True" TextMode="Search" DataSourceID="ds" />
					<px:PXLayoutRule ID="PXLayoutRule12" runat="server" GroupCaption="Contact" StartGroup="True" />
					<px:PXMailEdit ID="EMail" runat="server" CommandName="NewMailActivity" CommandSourceID="ds"
						DataField="EMail" CommitChanges="True"/>
					<px:PXLinkEdit ID="WebSite" runat="server" DataField="WebSite" CommitChanges="True"/>
					<px:PXMaskEdit ID="Phone1" runat="server" DataField="Phone1" SuppressLabel="False" />
					<px:PXMaskEdit ID="Phone2" runat="server" DataField="Phone2" />
					<px:PXTextEdit ID="Phone3" runat="server" DataField="Phone3" />
					<px:PXMaskEdit ID="Fax" runat="server" DataField="Fax" />
					<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM"
						StartColumn="True" />
                    <px:PXLayoutRule ID="PXLayoutRule3" runat="server" GroupCaption="CRM" StartGroup="True" />
					<px:PXSelector ID="edClassID" runat="server" AllowEdit="True" DataField="ClassID" FilterByAllFields="True" TextMode="Search" CommitChanges="True"/>
					<px:PXDropDown ID="Method" runat="server" DataField="Method" />
					<px:PXLayoutRule ID="PXLayoutRule5" runat="server" Merge="True" />
					<px:PXCheckBox ID="edNoCall" runat="server" DataField="NoCall" Size="S" SuppressLabel="True" />
					<px:PXCheckBox ID="edNoFax" runat="server" DataField="NoFax" Size="S" SuppressLabel="True" />
					<px:PXLayoutRule ID="PXLayoutRule7" runat="server" />
					<px:PXLayoutRule ID="PXLayoutRule8" runat="server" Merge="True" />
					<px:PXCheckBox ID="edNoEMail" runat="server" DataField="NoEMail" Size="S" SuppressLabel="True" />
					<px:PXCheckBox ID="edNoMail" runat="server" DataField="NoMail" Size="S" SuppressLabel="True" />
					<px:PXLayoutRule ID="PXLayoutRule9" runat="server" />
					<px:PXLayoutRule ID="PXLayoutRule10" runat="server" Merge="True" />
					<px:PXCheckBox ID="edNoMassMail" runat="server" DataField="NoMassMail" Size="S" />
					<px:PXCheckBox ID="edNoMarketingMaterials" runat="server" DataField="NoMarketing"
						Size="S" />
					<px:PXLayoutRule ID="PXLayoutRule11" runat="server" />
						<px:PXDateTimeEdit ID="edBirthday" runat="server" DataField="Birthday" />
					<px:PXLayoutRule runat="server" GroupCaption="Address" StartGroup="True" />
					<px:PXFormView ID="formA" runat="server" DataMember="AddressCurrent" DataSourceID="ds"
						SkinID="Transparent" TabIndex="2500">
						<Template>
							<px:PXLayoutRule runat="server" Merge="True" />
							<px:PXFormView ID="panelfordatamember" runat="server" DataMember="ContactCurrent2"
								DataSourceID="ds" RenderStyle="Simple" TabIndex="3100">
								<Template>
									<px:PXLayoutRule runat="server" LabelsWidth="SM" />
									<px:PXCheckBox ID="chkIsSameAsMaint" runat="server" DataField="IsAddressSameAsMain"
										CommitChanges="True" />
								</Template>
								<ContentStyle BackColor="Transparent" BorderStyle="None" />
							</px:PXFormView>
							<px:PXCheckBox ID="chkIsValidated" runat="server" DataField="IsValidated" Enabled="False" />
							<px:PXLayoutRule runat="server" />
							<px:PXTextEdit ID="edAddressLine1" runat="server" DataField="AddressLine1" />
							<px:PXTextEdit ID="edAddressLine2" runat="server" DataField="AddressLine2" />
							<px:PXTextEdit ID="edCity" runat="server" DataField="City" />
							<px:PXSelector ID="edCountryID" runat="server" AllowEdit="True" DataField="CountryID"
								FilterByAllFields="True" TextMode="Search" CommitChanges="True" DataSourceID="ds" edit="1" />
							<px:PXSelector ID="edState" runat="server" AutoRefresh="True" DataField="State" CommitChanges="True"
								FilterByAllFields="True" TextMode="Search" DataSourceID="ds" />
							<px:PXLayoutRule runat="server" Merge="True" />
							<px:PXMaskEdit ID="edPostalCode" runat="server" DataField="PostalCode" Size="S" />
							<px:PXButton ID="btnViewOnMap" runat="server" CommandName="ViewOnMap" CommandSourceID="ds"
								Size="xs" Text="View On Map" Height="20px" />
							<px:PXLayoutRule runat="server" />
						</Template>
						<ContentLayout ControlSize="XM" LabelsWidth="SM" OuterSpacing="None" />
						<ContentStyle BackColor="Transparent" BorderStyle="None" />
					</px:PXFormView>
				</Template>
			</px:PXTabItem>
            <px:PXTabItem Text="Duplicates"  BindingContext="form" VisibleExp="DataControls[&quot;edDuplicateFound&quot;].Value == true" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="PXGridDuplicates" runat="server" DataSourceID="ds" SkinID="Inquire" Width="100%"
						Height="200px" MatrixMode="True">
					    <ActionBar>
							<CustomItems>
					            <px:PXToolBarButton Text="Merge" Key="cmdMerge">
                                        <AutoCallBack Command="Merge" Target="ds"></AutoCallBack>
                                        <PopupCommand Command="Cancel" Target="ds" />
                                </px:PXToolBarButton>
                                <px:PXToolBarButton Text="Attach to Account" Key="cmdAttachToAccount">
                                        <AutoCallBack Command="AttachToAccount" Target="ds"></AutoCallBack>
                                        <PopupCommand Command="Cancel" Target="ds" />
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="Duplicates">
								<Columns>
								    <px:PXGridColumn DataField="Selected" TextAlign="Center" Type="CheckBox" Width="80px" AllowCheckAll="True"/>
                                    <px:PXGridColumn DataField="Contact2__ContactType" TextAlign="Left" Width="100px" AllowShowHide="False" />
                                    <px:PXGridColumn DataField="BAccountR__Type" TextAlign="Left" Width="80px" />
                                    <px:PXGridColumn DataField="Contact2__DuplicateStatus" TextAlign="Left" Width="140px" />
									<px:PXGridColumn DataField="Contact2__LastModifiedDateTime" TextAlign="Left" Width="160px" />
									<px:PXGridColumn DataField="Contact2__DisplayName" TextAlign="Left" Width="180px" AllowShowHide="False"  LinkCommand="Duplicates_Contact_ViewDetails" />									
									<px:PXGridColumn DataField="Contact2__BAccountID" TextAlign="Left" Width="180px" LinkCommand="Duplicates_BAccount_ViewDetails"  />
                                    <px:PXGridColumn DataField="BAccountR__AcctName" TextAlign="Left" Width="180px" />
									<px:PXGridColumn DataField="Contact2__Email" TextAlign="Left" Width="180px" AllowShowHide="False" />
								</Columns>
								<Layout FormViewHeight="" />
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="200" />
						<ActionBar PagerVisible="False">
							<Actions>
								<Search Enabled="False" />
							</Actions>
						</ActionBar>
                        <Mode AllowAddNew="False" AllowColMoving="False" AllowDelete="False" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Additional Info" LoadOnDemand="True">
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM"
						ControlSize="XM" />
					<px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Common" />
					<px:PXPanel ID="PXPanel1" runat="server" RenderSimple="True" RenderStyle="Simple">
						<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM"
							StartColumn="True" />
						<px:PXDropDown ID="edGender" runat="server" DataField="Gender" />
						<px:PXDropDown ID="edMaritalStatus" runat="server" CommitChanges="True" DataField="MaritalStatus" />
						<px:PXTextEdit ID="edSpouse" runat="server" DataField="Spouse" />
					</px:PXPanel>
					<px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Lead History" />
					<px:PXPanel ID="PXPanel2" runat="server" RenderSimple="True" RenderStyle="Simple">
						<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM"
							ControlSize="XM" />
						<px:PXDropDown ID="edSource" runat="server" DataField="Source" SelectedIndex="2"
							Enabled="False" />
                        <px:PXSelector ID="CampaignID" runat="server" DataField="CampaignID" CommitChanges="True" Enabled="False"/>
						<px:PXDropDown ID="edStatus1" runat="server" DataField="Status" SelectedIndex="1"
							Enabled="False" />
						<px:PXDropDown ID="edResolution" runat="server" DataField="Resolution" SelectedIndex="1"
							Enabled="False" />
					</px:PXPanel>
					<px:PXLayoutRule runat="server" StartGroup="True" GroupCaption=" " />
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM"
						ControlSize="XM" />
					<px:PXPanel ID="PXPanel3" runat="server" RenderSimple="True" RenderStyle="Simple">
						<px:PXImageUploader ID="edImage" runat="server" DataField="Img" Height="189px" />
					</px:PXPanel>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Attributes">
				<Template>
					<px:PXGrid ID="PXGridAnswers" runat="server" DataSourceID="ds" SkinID="Inquire" Width="100%"
						Height="200px" MatrixMode="True">
						<Levels>
							<px:PXGridLevel DataMember="Answers" DataKeyNames="EntityType,EntityID,AttributeID">
								<Columns>
									<px:PXGridColumn DataField="AttributeID" TextAlign="Left" Width="250px" AllowShowHide="False"
										TextField="AttributeID_description" />
    								<px:PXGridColumn DataField="isRequired" TextAlign="Center" Type="CheckBox" Width="75px" />
									<px:PXGridColumn DataField="Value" Width="300px" AllowShowHide="False" AllowSort="False" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="200" />
						<ActionBar>
							<Actions>
								<Search Enabled="False" />
							</Actions>
						</ActionBar>
                        <Mode AllowAddNew="False" AllowColMoving="False" AllowDelete="False" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Activities" LoadOnDemand="True">
				<Template>
					<pxa:PXGridWithPreview ID="gridActivities" runat="server" DataSourceID="ds" Width="100%"
						AllowSearch="True" DataMember="Activities" AllowPaging="true" NoteField="NoteText"
						FilesField="NoteFiles" BorderWidth="0px" GridSkinID="Inquire" SplitterStyle="z-index: 100; border-top: solid 1px Gray;  border-bottom: solid 1px Gray"
						PreviewPanelStyle="z-index: 100; background-color: Window" PreviewPanelSkinID="Preview"
						BlankFilterHeader="All Activities" MatrixMode="true" PrimaryViewControlID="form">
						<ActionBar DefaultAction="cmdViewActivity" CustomItemsGroup="0" PagerVisible="False">
							<CustomItems>
								<px:PXToolBarButton Text="Add Task" Key="cmdAddTask">
									<AutoCallBack Command="NewTask" Target="ds" />
									<PopupCommand Command="Cancel" Target="ds" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Add Event" Key="cmdAddEvent">
									<AutoCallBack Command="NewEvent" Target="ds" />
									<PopupCommand Command="Cancel" Target="ds" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Add Email" Key="cmdAddEmail">
									<AutoCallBack Command="NewMailActivity" Target="ds" />
									<PopupCommand Command="Cancel" Target="ds" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Add Activity" Key="cmdAddActivity">
									<AutoCallBack Command="NewActivity" Target="ds" />
									<PopupCommand Command="Cancel" Target="ds" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdViewActivity" Visible="false">
									<ActionBar MenuVisible="false" />
									<AutoCallBack Command="ViewActivity" Target="ds" />
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="Activities">
								<Columns>
									<px:PXGridColumn DataField="IsCompleteIcon" Width="21px" AllowShowHide="False" AllowResize="False"
										ForceExport="True" />
									<px:PXGridColumn DataField="PriorityIcon" Width="21px" AllowShowHide="False" AllowResize="False"
										ForceExport="True" />
									<px:PXGridColumn DataField="ReminderIcon" Width="21px" AllowShowHide="False" AllowResize="False"
										ForceExport="True" />
									<px:PXGridColumn DataField="ClassIcon" Width="21px" AllowShowHide="False" AllowResize="False"
										ForceExport="True" />
									<px:PXGridColumn DataField="ClassInfo" Width="60px" />
									<px:PXGridColumn DataField="RefNoteID" Visible="false" AllowShowHide="False" />
									<px:PXGridColumn DataField="Subject" LinkCommand="ViewActivity" Width="297px" />
									<px:PXGridColumn DataField="UIStatus" />
                                    <px:PXGridColumn DataField="MPStatus" />
                                    <px:PXGridColumn DataField="Released" TextAlign="Center" Type="CheckBox" Width="80px" />
									<px:PXGridColumn DataField="StartDate" DisplayFormat="g" Width="120px" />
                                    <px:PXGridColumn DataField="CreatedDateTime" DisplayFormat="g" Width="120px" Visible="False" />
									<px:PXGridColumn DataField="TimeSpent" Width="80px" />
									<px:PXGridColumn DataField="CreatedByID" Visible="false" AllowShowHide="False" />
									<px:PXGridColumn DataField="CreatedByID_Creator_Username" Visible="false"
										SyncVisible="False" SyncVisibility="False" Width="108px">
										<NavigateParams>
											<px:PXControlParam Name="PKID" ControlID="gridActivities" PropertyName="DataValues[&quot;CreatedByID&quot;]" />
										</NavigateParams>
									</px:PXGridColumn>
									<px:PXGridColumn DataField="GroupID" Width="90px" />
									<px:PXGridColumn DataField="Owner" LinkCommand="OpenActivityOwner" Width="150px" DisplayMode="Text"/>
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<PreviewPanelTemplate>
							<pxa:PXHtmlView ID="edBody" runat="server" DataField="body" TextMode="MultiLine"
								MaxLength="50" Width="100%" Height="100%" SkinID="Label" />
						</PreviewPanelTemplate>
						<AutoSize Enabled="true" />
						<GridMode AllowAddNew="False" AllowDelete="False" AllowFormEdit="False" AllowUpdate="False" AllowUpload="False" />
					</pxa:PXGridWithPreview>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Relations" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="grdRelations" runat="server" Height="400px" Width="100%" AllowPaging="True"
						ActionsPosition="Top" AllowSearch="true" DataSourceID="ds" SkinID="Details">
						<Levels>
							<px:PXGridLevel DataMember="Relations">
								<Columns>
									<px:PXGridColumn DataField="Role" Width="120px" />
									<px:PXGridColumn DataField="EntityID" Width="160px" AutoCallBack="true" TextField="EntityCD"
										LinkCommand="Relations_EntityDetails" />
									<px:PXGridColumn DataField="Name" Width="200px" />
									<px:PXGridColumn DataField="ContactID" Width="160px" AutoCallBack="true" TextField="ContactName"
										DisplayMode="Text" LinkCommand="Relations_ContactDetails" />
									<px:PXGridColumn DataField="Email" Width="120px" />
									<px:PXGridColumn DataField="AddToCC" Width="70px" Type="CheckBox" TextAlign="Center" />
								</Columns>
								<RowTemplate>
									<px:PXSelector ID="edRelEntityID" runat="server" DataField="EntityID" FilterByAllFields="True" />
									<px:PXSelector ID="edRelContactID" runat="server" DataField="ContactID" FilterByAllFields="True" AutoRefresh="True" />
								</RowTemplate>
							</px:PXGridLevel>
						</Levels>
						<Mode InitNewRow="True" />
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Opportunities" LoadOnDemand="true">
				<Template>
					<px:PXGrid ID="gridOpportunities" runat="server" DataSourceID="ds" Height="423px"
						Width="100%" AllowSearch="True" ActionsPosition="Top" SkinID="Inquire">
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" />
						<Levels>
							<px:PXGridLevel DataMember="Opportunities">
								<Columns>
									<px:PXGridColumn DataField="OpportunityID" Width="90px" LinkCommand="Opportunities_ViewDetails" />
									<px:PXGridColumn DataField="OpportunityName" Width="151px" />
									<px:PXGridColumn DataField="StageID" Width="108px" />
									<px:PXGridColumn DataField="CROpportunityProbability__Probability" TextAlign="Right" Width="100px" />
									<px:PXGridColumn DataField="Status" Width="81px" RenderEditorText="True" />
									<px:PXGridColumn DataField="CuryAmount" TextAlign="Right" Width="81px" />
									<px:PXGridColumn DataField="CuryID" Width="54px" />
									<px:PXGridColumn DataField="CloseDate" Width="130px" />
									<px:PXGridColumn DataField="Contact__DisplayName" Width="120px" LinkCommand="Opportunities_Contact_ViewDetails" />
									<px:PXGridColumn DataField="WorkgroupID" Width="108px" />
									<px:PXGridColumn DataField="OwnerID" Width="108px" DisplayMode="Text" />
								</Columns>
								<Layout FormViewHeight="" />
							</px:PXGridLevel>
						</Levels>
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
						<ActionBar ActionsText="False" DefaultAction="cmdOpportunityDetails" PagerVisible="False">
							<CustomItems>
								<px:PXToolBarButton Key="cmdAddOpportunity" ImageKey="AddNew" Tooltip="Add New Opportunity" DisplayStyle="Image">
									<AutoCallBack Command="AddOpportunity" Target="ds" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Opportunity Details" Key="cmdOpportunityDetails">
									<AutoCallBack Command="Opportunities_ViewDetails" Target="ds" />
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Cases" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="gridCases" runat="server" DataSourceID="ds" Height="423px" Width="100%"
						AllowSearch="True" SkinID="Inquire" AllowPaging="true" AdjustPageSize="Auto"
						BorderWidth="0px">
						<ActionBar DefaultAction="cmdViewCaseDetails" PagerVisible="False">
							<CustomItems>
								<px:PXToolBarButton Key="cmdAddCase" ImageKey="AddNew" Tooltip="Add New Case" DisplayStyle="Image">
									<ActionBar GroupIndex="0" Order="2" />
									<AutoCallBack Command="AddCase" Target="ds" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdViewCaseDetails" Visible="false">
									<ActionBar GroupIndex="0" />
									<AutoCallBack Command="Cases_ViewDetails" Target="ds" />
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="Cases">
								<Columns>
									<px:PXGridColumn DataField="CaseCD" Width="80px" LinkCommand="Cases_ViewDetails" />
									<px:PXGridColumn DataField="Subject" Width="300px" />
									<px:PXGridColumn DataField="CaseClassID" Width="80px" />
									<px:PXGridColumn DataField="Severity" Width="54px" RenderEditorText="True" />
									<px:PXGridColumn DataField="Status" Width="72px" RenderEditorText="True" />
									<px:PXGridColumn DataField="Resolution" Width="72px" RenderEditorText="True" />
									<px:PXGridColumn DataField="CreatedDateTime" Width="90px" />
									<px:PXGridColumn DataField="InitResponse" Width="63px" DisplayFormat="###:##:##" />
									<px:PXGridColumn DataField="TimeEstimated" Width="63px" DisplayFormat="###:##:##" />
									<px:PXGridColumn DataField="ResolutionDate" Width="90px" />
									<px:PXGridColumn DataField="WorkgroupID" Width="108px" />
									<px:PXGridColumn DataField="OwnerID" Width="108px" DisplayMode="Text" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" />
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Campaigns" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="grdCampaignHistory" runat="server" Height="400px" Width="100%" Style="z-index: 100"
						AllowPaging="True" ActionsPosition="Top" AllowSearch="true" DataSourceID="ds"
						SkinID="Details">
						<Levels>
							<px:PXGridLevel DataMember="Members">
								<Columns>
									<px:PXGridColumn DataField="CampaignID" Width="200px" AutoCallBack="true" LinkCommand="Members_CRCampaign_ViewDetails" />
									<px:PXGridColumn DataField="CRCampaign__CampaignName" Width="200px" />
									<px:PXGridColumn DataField="Status" Width="72px" RenderEditorText="True" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Marketing Lists" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="grdMarketingLists" runat="server" Height="400px" Width="100%" Style="z-index: 100"
						AllowPaging="True" ActionsPosition="Top" AllowSearch="true" DataSourceID="ds"
						SkinID="Details">
						<Levels>
							<px:PXGridLevel DataMember="Subscriptions">
								<Columns>
									<px:PXGridColumn DataField="MarketingListID" Width="120px" AutoCallBack="true" TextField="CRMarketingList__MailListCode"
										LinkCommand="Subscriptions_CRMarketingList_ViewDetails" />
									<px:PXGridColumn DataField="CRMarketingList__Name" Width="200px" />
									<px:PXGridColumn DataField="Activated" Width="60px" Type="CheckBox" />
									<px:PXGridColumn DataField="Format" Width="80px" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Notifications" LoadOnDemand="True">
				<Template>
					<px:PXGrid runat="server" ID="gridNC" SkinID="Details" DataSourceID="ds" BorderWidth="0px"
						Width="100%" AdjustPageSize="Auto">
					    <Mode AllowAddNew="false"></Mode>
						<Levels>
							<px:PXGridLevel DataMember="NWatchers">
								<Columns>
									<px:PXGridColumn AllowUpdate="False" DataField="NotificationSetup__Module" />
									<px:PXGridColumn AllowUpdate="False" DataField="NotificationSetup__SourceCD" />
									<px:PXGridColumn DataField="NotificationSetup__NotificationCD" Width="120px" />
									<px:PXGridColumn AllowUpdate="False" DataField="ClassID" Width="100px" />
									<px:PXGridColumn AllowUpdate="False" DataField="EntityDescription_Description" Width="200px" />
									<px:PXGridColumn DataField="ReportID" DisplayFormat="CC.CC.CC.CC" Width="100px" />
									<px:PXGridColumn DataField="NotificationID" Width="140px" />
									<px:PXGridColumn AllowNull="False" DataField="Format" RenderEditorText="True" Width="80px" />
									<px:PXGridColumn AllowNull="False" DataField="Hidden" TextAlign="Center" Type="CheckBox"
										Width="60px" />
									<px:PXGridColumn DataField="Active" TextAlign="Center" Type="CheckBox" Width="60px" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="true" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
            <px:PXTabItem LoadOnDemand="True" Text="User Info" BindingContext="form" VisibleExp="DataControls[&quot;edContactType&quot;].Value == PN">
                <Template>
                    <px:PXFormView ID="frmLogin" runat="server" DataMember="User" SkinID="Transparent">
                        <Template>
                            <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True"/>
						    <px:PXDropDown ID="edState" runat="server" DataField="State" Enabled="False"/>
		                    <px:PXSelector ID="edLoginType" runat="server" DataField="LoginTypeID" CommitChanges="True" AllowEdit="True" AutoRefresh="True"/>
                            <px:PXMaskEdit ID="edUsername" runat="server" DataField="Username" CommitChanges="True"/>
                			<px:PXTextEdit ID="edPassword" runat="server" DataField="Password" TextMode ="Password"/>
                            <px:PXCheckBox ID="edGenerate" runat="server" DataField="GeneratePassword" CommitChanges="True"/>
                            <px:PXButton ID="btnResetPassword" runat="server" Text="Reset Password" CommandName="ResetPassword" CommandSourceID="ds" Width="150" Height="20"/>
                            <px:PXLayoutRule ID="PXLayoutRule2" runat="server" ControlSize="SM" StartColumn="True" SuppressLabel="True"/>
                            <px:PXButton ID="btnActivateLogin" runat="server" CommandName="ActivateLogin" CommandSourceID="ds" Width="150" Height="20"/>
                            <px:PXButton ID="btnEnableLogin" runat="server" CommandName="EnableLogin" CommandSourceID="ds" Width="150" Height="20"/>
                            <px:PXButton ID="btnDisableLogin" runat="server" CommandName="DisableLogin" CommandSourceID="ds" Width="150" Height="20"/>
                            <px:PXButton ID="btnUnlockLogin" runat="server" CommandName="UnlockLogin" CommandSourceID="ds" Width="150" Height="20"/>
                        </Template>
                    </px:PXFormView>
					<px:PXGrid ID="gridRoles" runat="server" DataSourceID="ds" Width="100%" ActionsPosition="Top" SkinID="DetailsInTab" Caption=" ">
					    <ActionBar>
                            <Actions>
                                <Save Enabled="False" />
                                <AddNew Enabled="False" />
                                <Delete Enabled="False" />
                            </Actions>
                        </ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="Roles">
								<Columns>
						            <px:PXGridColumn AllowMove="False" AllowSort="False" DataField="Selected" TextAlign="Center" Type="CheckBox" Width="20px" AutoCallBack="True"/>
									<px:PXGridColumn DataField="Rolename" Width="200px" AllowUpdate="False"/>
									<px:PXGridColumn AllowUpdate="False" DataField="Rolename_Roles_descr" Width="300px" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="250" MinWidth="300" />
					</px:PXGrid>
                    <px:PXSmartPanel ID="pnlResetPassword" runat="server" Caption="Change password"
                        LoadOnDemand="True" Width="400px" Key="User" CommandName="ResetPasswordOK" CommandSourceID="ds" AcceptButtonID="btnOk" CancelButtonID="btnCancel" AutoCallBack-Command="Refresh" AutoCallBack-Target="frmResetParams" AutoCallBack-Enabled="true">
                        <px:PXFormView ID="frmResetParams" runat="server" DataSourceID="ds" Width="100%" DataMember="User"
                            Caption="Reset Password" SkinID="Transparent">
                            <Template>
                                <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" ControlSize="M" LabelsWidth="SM" />
                                <px:PXTextEdit ID="edNewPassword" runat="server" DataField="NewPassword" TextMode="Password" Required="True" />
                                <px:PXTextEdit ID="edConfirmPassword" runat="server" DataField="ConfirmPassword" TextMode="Password" Required="True" />
                            </Template>
                        </px:PXFormView>
                        <px:PXPanel ID="PXPanel1" runat="server" SkinID="Buttons">
                            <px:PXButton ID="btnOk" runat="server" DialogResult="OK" Text="OK"/>
                            <px:PXButton ID="btnCancel" runat="server" DialogResult="Cancel" Text="Cancel" />
                        </px:PXPanel>
                    </px:PXSmartPanel>
                </Template>
            </px:PXTabItem>
        </Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="250" MinWidth="300" />
	</px:PXTab>
     <px:PXSmartPanel ID="PanelCreateAccount" runat="server" Style="z-index: 108; position: absolute; left: 27px; top: 99px;" Caption="New Account"
        CaptionVisible="True" LoadOnDemand="true" ShowAfterLoad="true" Key="AccountInfo" AutoCallBack-Enabled="true" AutoCallBack-Target="formCreateAccount" AutoCallBack-Command="Refresh"
        CallBackMode-CommitChanges="True" CallBackMode-PostData="Page" AcceptButtonID="PXButtonOK" CancelButtonID="PXButtonCancel">
        <px:PXFormView ID="formCreateAccount" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Caption="Services Settings" CaptionVisible="False" SkinID="Transparent"
            DataMember="AccountInfo">
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule6" runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                <px:PXMaskEdit ID="edBAccountID" runat="server" DataField="BAccountID" CommitChanges="True"/>
                <px:PXTextEdit ID="edAccountName" runat="server" DataField="AccountName" CommitChanges="True"/>
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="PXPanel4" runat="server" SkinID="Buttons">
            <px:PXButton ID="PXButtonOK" runat="server" Text="Create" DialogResult="OK" Width="63px" Height="20px"></px:PXButton>
            <px:PXButton ID="PXButtonCancel" runat="server" DialogResult="Cancel" Text="Cancel" Width="63px" Height="20px" Style="margin-left: 5px" />
        </px:PXPanel>
    </px:PXSmartPanel>
        <px:PXSmartPanel ID="spMergeParamsDlg" runat="server"
        Width="600" Caption="Please resolve the conflicts" CaptionVisible="True" Key="Duplicates" LoadOnDemand="True" ShowAfterLoad="true"
        AutoCallBack-Enabled="true" AutoCallBack-Target="formMerge" AutoCallBack-Command="Refresh"
        CallBackMode-CommitChanges="True" CallBackMode-PostData="Page"
        AcceptButtonID="btnOk" CancelButtonID="btnCancel">
        <px:PXFormView ID="formMerge" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" SkinID="Transparent"
            DataMember="mergeParams">
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule4" runat="server" StartColumn="True" />
                <px:PXSelector CommitChanges="True" ID="edContactID" runat="server" DataField="ContactID" FilterByAllFields="True" DisplayMode="Text" TextMode="Search" AutoRefresh="True"/>
                <px:PXGrid ID="grdFields" runat="server" Height="250px" DataSourceID="ds" MatrixMode="True">
                    <Levels>
                        <px:PXGridLevel DataMember="ValueConflicts">
                            <Columns>
                                <px:PXGridColumn DataField="DisplayName" Width="108px" />
                                <px:PXGridColumn DataField="Value" Width="190px" RenderEditorText="true" AutoCallBack="True" />
                            </Columns>
                            <Layout ColumnsMenu="False" />
                            <Mode AllowAddNew="false" AllowDelete="false" />
                        </px:PXGridLevel>
                    </Levels>
                    <ActionBar>
                        <Actions>
                            <ExportExcel Enabled="False" />
                            <AddNew Enabled="False" />
                            <FilterShow Enabled="False" />
                            <FilterSet Enabled="False" />
                            <Save Enabled="False" />
                            <Delete Enabled="False" />
                            <NoteShow Enabled="False" />
                            <Search Enabled="False" />
                            <AdjustColumns Enabled="False" />
                        </Actions>
                    </ActionBar>
                    <CallbackCommands>
                        <Save PostData="Page" />
                    </CallbackCommands>
                </px:PXGrid>
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="PXPanel2" runat="server" SkinID="Buttons">
            <px:PXButton ID="btnOK" runat="server" Text="OK" DialogResult="OK" />
            <px:PXButton ID="btnCancel" runat="server" Text="Cancel" DialogResult="Cancel" />
        </px:PXPanel>
    </px:PXSmartPanel>
</asp:Content>
