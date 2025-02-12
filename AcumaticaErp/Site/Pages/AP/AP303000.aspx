<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AP303000.aspx.cs" Inherits="Page_TabView" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.AP.VendorMaint" PrimaryView="BAccount">
		<CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand StartNewGroup="True" Name="First" />
			<px:PXDSCallbackCommand DependOnGrid="grdContacts" Name="ViewContact" Visible="False" />
			<px:PXDSCallbackCommand Name="NewContact" Visible="False" CommitChanges="true" />
			<px:PXDSCallbackCommand DependOnGrid="grdLocations" Name="ViewLocation" Visible="False" />
			<px:PXDSCallbackCommand Name="NewLocation" Visible="False" CommitChanges="true" />
			<px:PXDSCallbackCommand DependOnGrid="grdLocations" Name="SetDefault" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewCustomer" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewBusnessAccount" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewMainOnMap" Visible="false" />
			<px:PXDSCallbackCommand Name="ViewDefLocationOnMap" Visible="false" />
			<px:PXDSCallbackCommand Name="ViewRestrictionGroups" Visible="False" />
			<px:PXDSCallbackCommand Name="ExtendToCustomer" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewBalanceDetails" Visible="False" />
			<px:PXDSCallbackCommand Name="ViewRemitOnMap" Visible="false" />
			<px:PXDSCallbackCommand Name="NewBillAdjustment" Visible="False" />
			<px:PXDSCallbackCommand Name="NewManualCheck" Visible="False" />
			<px:PXDSCallbackCommand Name="VendorDetails" Visible="False" />
			<px:PXDSCallbackCommand Name="ApproveBillsForPayments" Visible="False" />
			<px:PXDSCallbackCommand Name="PayBills" Visible="False" />
			<px:PXDSCallbackCommand Name="BalanceByVendor" Visible="False" />
			<px:PXDSCallbackCommand Name="VendorHistory" Visible="False" />
			<px:PXDSCallbackCommand Name="APAgedPastDue" Visible="False" />
			<px:PXDSCallbackCommand Name="APAgedOutstanding" Visible="False" />
			<px:PXDSCallbackCommand Name="APDocumentRegister" Visible="False" />
			<px:PXDSCallbackCommand Name="RepVendorDetails" Visible="False" />
			<px:PXDSCallbackCommand Name="Action" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="Inquiry" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="Report" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ValidateAddresses" Visible="False" CommitChanges="True" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXSmartPanel ID="pnlChangeID" runat="server"  Caption="Specify New ID"
        CaptionVisible="true" DesignView="Hidden" LoadOnDemand="true" Key="ChangeIDDialog" CreateOnDemand="false" AutoCallBack-Enabled="true"
        AutoCallBack-Target="formChangeID" AutoCallBack-Command="Refresh" CallBackMode-CommitChanges="True" CallBackMode-PostData="Page"
        AcceptButtonID="btnOK">
            <px:PXFormView ID="formChangeID" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" CaptionVisible="False"
                DataMember="ChangeIDDialog">
                <ContentStyle BackColor="Transparent" BorderStyle="None" />
                <Template>
                    <px:PXLayoutRule ID="rlAcctCD" runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                    <px:PXSegmentMask ID="edAcctCD" runat="server" DataField="CD" />
                </Template>
            </px:PXFormView>
            <px:PXPanel ID="pnlChangeIDButton" runat="server" SkinID="Buttons">
                <px:PXButton ID="btnOK" runat="server" DialogResult="OK" Text="OK" >
                    <AutoCallBack Target="formChangeID" Command="Save" />
                </px:PXButton>
            </px:PXPanel>
    </px:PXSmartPanel>
	<px:PXFormView ID="BAccount" runat="server" Width="100%" Caption="Vendor Summary" DataMember="BAccount" NoteIndicator="True" FilesIndicator="True" NotifyIndicator="True" ActivityIndicator="True" LinkIndicator="True"
		DefaultControlID="edAcctCD" DataSourceID="ds">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
			<px:PXSegmentMask ID="edAcctCD" runat="server" DataField="AcctCD" DisplayMode="Value" DataSourceID="ds" FilterByAllFields="True" />
			<px:PXLayoutRule runat="server" ColumnSpan="2" />
			<px:PXTextEdit CommitChanges="True" ID="edAcctName" runat="server" DataField="AcctName" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="XS" ControlSize="S" />
			<px:PXDropDown ID="edStatus" runat="server" DataField="Status" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
			<px:PXFormView ID="VendorBalance" runat="server" DataMember="VendorBalance" DataSourceID="ds" RenderStyle="Simple">
				<Template>
					<px:PXLayoutRule runat="server" ControlSize="M" LabelsWidth="SM" StartColumn="True" />
					<px:PXNumberEdit ID="edBalance" runat="server" DataField="Balance" Enabled ="False">
					</px:PXNumberEdit>
					<px:PXNumberEdit ID="edDepositsBalance" runat="server" DataField="DepositsBalance" Enabled ="False">
					</px:PXNumberEdit>
				</Template>
			</px:PXFormView>
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXTab ID="tab" runat="server" Height="494px" Style="z-index: 100" Width="100%" DataMember="CurrentVendor">
		<Activity HighlightColor="" SelectedColor="" Width="" Height=""></Activity>
		<Items>
			<px:PXTabItem Text="General Info" RepaintOnDemand="false">
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
					<px:PXFormView ID="DefContact" runat="server" Caption="Main Contact" DataMember="DefContact" RenderStyle="Fieldset">
						<Template>
							<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
							<px:PXTextEdit ID="edFullName" runat="server" DataField="FullName" />
							<px:PXTextEdit ID="edSalutation" runat="server" DataField="Salutation" />
							<px:PXMailEdit ID="edEMail" runat="server" DataField="EMail" CommitChanges="True"/>
							<px:PXLinkEdit ID="edWebSite" runat="server" DataField="WebSite" CommitChanges="True"/>
							<px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1" />
							<px:PXMaskEdit ID="edPhone2" runat="server" DataField="Phone2" />
							<px:PXMaskEdit ID="edFax" runat="server" DataField="Fax" />
						</Template>
					</px:PXFormView>
					<px:PXTextEdit ID="edAcctReferenceNbr" runat="server" DataField="AcctReferenceNbr" />
					<px:PXSegmentMask ID="edParentBAccountID" runat="server" DataField="ParentBAccountID" AllowEdit="True" />
					<px:PXFormView ID="DefAddress" runat="server" Caption="Main Address" DataMember="DefAddress" SyncPosition="true" RenderStyle="Fieldset">
						<Template>
							<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
							<px:PXCheckBox ID="edIsValidated" runat="server" DataField="IsValidated" Enabled="False" />
							<px:PXTextEdit ID="edAddressLine1" runat="server" DataField="AddressLine1" />
							<px:PXTextEdit ID="edAddressLine2" runat="server" DataField="AddressLine2" />
							<px:PXTextEdit ID="edCity" runat="server" DataField="City" />
							<px:PXSelector ID="edCountryID" runat="server" AllowEdit="True" CommitChanges="True" DataField="CountryID" />
							<px:PXSelector ID="edState" runat="server" AllowEdit="True" AutoRefresh="True" DataField="State" CommitChanges="True" />
							<px:PXLayoutRule runat="server" Merge="True" />
							<px:PXMaskEdit ID="edPostalCode" runat="server" DataField="PostalCode" Size="s" />
							<px:PXButton ID="btnViewMainOnMap" runat="server" CommandName="ViewMainOnMap" CommandSourceID="ds" Text="View on Map" />
							<px:PXLayoutRule runat="server" />
						</Template>
					</px:PXFormView>
					<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" StartGroup="True" GroupCaption="Financial Settings" />
					<px:PXSelector CommitChanges="True" ID="edVendorClassID" runat="server" DataField="VendorClassID" AllowEdit="True" />
					<px:PXSelector ID="edTermsID" runat="server" DataField="TermsID" AllowEdit="True" />
					<px:PXLayoutRule ID="PXLayoutRule3" runat="server" Merge="True" />
					<px:PXSelector ID="edCuryID" runat="server" DataField="CuryID" AllowEdit="True" Size="S" />
					<px:PXCheckBox ID="chkAllowOverrideCury" runat="server" DataField="AllowOverrideCury" />
					<px:PXLayoutRule ID="PXLayoutRule4" runat="server" />
					<px:PXLayoutRule ID="PXLayoutRule5" runat="server" Merge="True" />
					<px:PXSelector ID="edCuryRateTypeID" runat="server" DataField="CuryRateTypeID" AllowEdit="True" Size="S" />
					<px:PXCheckBox ID="chkAllowOverrideRate" runat="server" DataField="AllowOverrideRate" />
					<px:PXLayoutRule ID="PXLayoutRule6" runat="server" />
					<px:PXLayoutRule ID="PXLayoutRule7" runat="server" StartGroup="True" GroupCaption="Vendor Properties" />
					<px:PXCheckBox ID="chkLandedCostVendor" runat="server" DataField="LandedCostVendor" />
					<px:PXCheckBox CommitChanges="True" ID="chkTaxAgency" runat="server" DataField="TaxAgency" />
					<px:PXCheckBox ID="chkVendor1099" runat="server" DataField="Vendor1099" />
					<px:PXDropDown ID="edBox1099" runat="server" DataField="Box1099" />
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Payment Settings">
				<Template>
					<px:PXFormView ID="DefLocationPayment" runat="server" DataMember="DefLocation" SkinID="Transparent">
						<Template>
							<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" StartGroup="True" GroupCaption="Remittance Contact" />
							<px:PXCheckBox CommitChanges="True" ID="chkIsRemitContactSameAsMain" runat="server" DataField="IsRemitContactSameAsMain" />
							<px:PXFormView ID="RemitContact" runat="server" DataMember="RemitContact" RenderStyle="Simple">
								<Template>
									<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
									<px:PXLinkEdit ID="edWebSite" runat="server" DataField="WebSite" CommitChanges="True"/>
									<px:PXMailEdit ID="edEMail" runat="server" DataField="EMail" CommitChanges="True"/>
									<px:PXMaskEdit ID="edFax" runat="server" DataField="Fax" />
									<px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1" />
									<px:PXMaskEdit ID="edPhone2" runat="server" DataField="Phone2" />
									<px:PXTextEdit ID="edSalutation" runat="server" DataField="Salutation" />
									<px:PXTextEdit ID="edFullName" runat="server" DataField="FullName" />
								</Template>
							</px:PXFormView>
							<px:PXLayoutRule runat="server" GroupCaption="Remittance Address" StartGroup="True" />
							<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkIsRemitAddressSameAsMain" runat="server" DataField="IsRemitAddressSameAsMain" />
							<px:PXFormView ID="RemitAddress" runat="server" DataMember="RemitAddress" SyncPosition="true" RenderStyle="Simple">
								<Template>
									<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
									<px:PXCheckBox ID="edIsValidated" runat="server" DataField="IsValidated" Enabled="False" />
									<px:PXTextEdit ID="edAddressLine1" runat="server" DataField="AddressLine1" />
									<px:PXTextEdit ID="edAddressLine2" runat="server" DataField="AddressLine2" />
									<px:PXTextEdit ID="edCity" runat="server" DataField="City" />
									<px:PXSelector ID="edCountryID" runat="server" AllowEdit="True" AutoRefresh="True" CommitChanges="True" DataField="CountryID" />
									<px:PXSelector ID="edState" runat="server" AllowEdit="True" AutoRefresh="True" DataField="State" />
									<px:PXLayoutRule runat="server" Merge="True" />
									<px:PXMaskEdit ID="edPostalCode" runat="server" DataField="PostalCode" Size="s" />
									<px:PXButton ID="btnViewRemitOnMap" runat="server" CommandName="ViewRemitOnMap" CommandSourceID="ds" Size="xs" Text="View on Map" />
									<px:PXLayoutRule runat="server" />
								</Template>
							</px:PXFormView>
							<px:PXLayoutRule runat="server" ControlSize="XM" GroupCaption="Default Payment Settings" LabelsWidth="SM" StartColumn="True" StartGroup="True" />
							<px:PXSelector CommitChanges="True" ID="edVPaymentMethodID" runat="server" DataField="VPaymentMethodID" AutoRefresh="True" AllowEdit="True" />
							<px:PXSegmentMask CommitChanges="True" ID="edVCashAccountID" runat="server" DataField="VCashAccountID" AllowEdit="True" AutoRefresh="True" />
							<px:PXDropDown ID="edVPaymentByType" runat="server" DataField="VPaymentByType" />
							<px:PXNumberEdit ID="edVPaymentLeadTime" runat="server" DataField="VPaymentLeadTime" />
							<px:PXCheckBox ID="chkVSeparateCheck" runat="server" DataField="VSeparateCheck" />
							<px:PXGrid ID="grdPaymentDetails" runat="server" Caption="Payment Instructions" SkinID="Attributes" MatrixMode="True" Height="160px" Width="400px">
								<Levels>
									<px:PXGridLevel DataMember="PaymentDetails" DataKeyNames="BAccountID,LocationID,PaymentMethodID,DetailID">
										<Columns>
											<px:PXGridColumn DataField="PaymentMethodDetail__descr" Width="150px" />
											<px:PXGridColumn DataField="DetailValue" Width="200px" />
										</Columns>
										<Layout FormViewHeight="" />
									</px:PXGridLevel>
								</Levels>
							</px:PXGrid>
						</Template>
					</px:PXFormView>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Purchase Settings" LoadOnDemand="True">
				<Template>
					<px:PXFormView ID="DefLocation" runat="server" CaptionVisible="False" DataMember="DefLocation" Width="100%" SkinID="Transparent">
						<Template>
							<px:PXLayoutRule ID="PXLayoutRule8" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
							<px:PXLayoutRule ID="PXLayoutRule9" runat="server" StartGroup="True" GroupCaption="Shipper's Contact" ControlSize="XM" LabelsWidth="SM" />
							<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkIsContactSameAsMain" runat="server" DataField="IsContactSameAsMain" />
							<px:PXFormView ID="DefLocationContact" runat="server" DataMember="DefLocationContact" RenderStyle="Simple">
								<Template>
									<px:PXLayoutRule ID="PXLayoutRule10" runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
									<px:PXTextEdit ID="edFullName" runat="server" DataField="FullName" />
									<px:PXTextEdit ID="edSalutation" runat="server" DataField="Salutation" />
									<px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1" />
									<px:PXMaskEdit ID="edPhone2" runat="server" DataField="Phone2" />
									<px:PXMaskEdit ID="edFax" runat="server" DataField="Fax" />
									<px:PXMailEdit ID="edEMail" runat="server" DataField="EMail" CommitChanges="True"/>
									<px:PXLinkEdit ID="edWebSite" runat="server" DataField="WebSite" CommitChanges="True"/>
								</Template>
							</px:PXFormView>
							<px:PXLayoutRule ID="PXLayoutRule11" runat="server" GroupCaption="Shipper's Address" StartGroup="True" ControlSize="XM" LabelsWidth="SM" />
							<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkIsMain" runat="server" DataField="IsAddressSameAsMain" />
							<px:PXCheckBox ID="edIsValidated" runat="server" DataField="IsValidated" Enabled="False" />
							<px:PXTextEdit ID="edAddressLine1" runat="server" DataField="AddressLine1" />
							<px:PXTextEdit ID="edAddressLine2" runat="server" DataField="AddressLine2" />
							<px:PXTextEdit ID="edCity" runat="server" DataField="City" />
							<px:PXSelector CommitChanges="True" ID="edCountryID" runat="server" DataField="CountryID" AllowEdit="True" />
							<px:PXSelector ID="edState" runat="server" DataField="State" AutoRefresh="True" AllowEdit="True" />
							<px:PXLayoutRule ID="PXLayoutRule12" runat="server" Merge="True" />
							<px:PXMaskEdit Size="s" ID="edPostalCode" runat="server" DataField="PostalCode" />
							<px:PXButton Size="xs" ID="btnViewDefLoactionOnMap" runat="server" CommandName="ViewDefLocationOnMap" CommandSourceID="ds" Text="View on Map" />
							<px:PXLayoutRule ID="PXLayoutRule13" runat="server" />
							<px:PXLayoutRule ID="PXLayoutRule14" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" StartGroup="True" GroupCaption="Default Location Settings" />
							<%--<px:PXSegmentMask ID="edLocationCD" runat="server" DataField="LocationCD" AllowEdit="True" />--%>
							<px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" />
							<px:PXSelector ID="edVBranchID" runat="server" DataField="VBranchID" AllowEdit="True" />
							<px:PXSelector ID="edTaxZoneID" runat="server" DataField="VTaxZoneID" AllowEdit="True" />
							<px:PXTextEdit ID="edTaxRegistrationID" runat="server" DataField="TaxRegistrationID" />
							<px:PXLayoutRule ID="PXLayoutRule5" runat="server" Merge="True" />
							<px:PXCheckBox ID="chkVPrintOrder" runat="server" DataField="VPrintOrder" />
							<px:PXCheckBox ID="chkVEmailOrder" runat="server" DataField="VEmailOrder" />
							<px:PXLayoutRule ID="PXLayoutRule15" runat="server" />
							<px:PXLayoutRule ID="PXLayoutRule7" runat="server" StartGroup="True" GroupCaption="Shipping Instructions" />
							<px:PXSegmentMask ID="edVSiteID" runat="server" DataField="VSiteID" AllowEdit="True" CommitChanges="True" AutoRefresh="True"/>
							<px:PXSelector ID="edShipTermsID" runat="server" DataField="VShipTermsID" AllowEdit="True" />
							<px:PXSelector ID="edVCarrierID" runat="server" DataField="VCarrierID" AllowEdit="True" />
							<px:PXSelector ID="edFOBPointID" runat="server" DataField="VFOBPointID" AllowEdit="True" />
							<px:PXNumberEdit ID="edLeadTime" runat="server" DataField="VLeadTime" />
							<px:PXLayoutRule ID="PXLayoutRule16" runat="server" StartGroup="True" GroupCaption="Receipt Actions" />
							<px:PXNumberEdit ID="edVRcptQtyMin" runat="server" DataField="VRcptQtyMin" />
							<px:PXNumberEdit ID="edVRcptQtyMax" runat="server" DataField="VRcptQtyMax" />
							<px:PXNumberEdit ID="edVRcptQtyThreshold" runat="server" DataField="VRcptQtyThreshold" />
							<px:PXDropDown ID="edVRcptQtyAction" runat="server" DataField="VRcptQtyAction" />
						</Template>
					</px:PXFormView>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Locations" LoadOnDemand="False">
				<Template>
					<px:PXGrid ID="grdLocations" runat="server" AllowSearch="True" Height="99%" SkinID="DetailsInTab" Style="z-index: 100;" Width="100%">
						<LevelStyles>
							<RowForm Height="400px" Width="800px">
							</RowForm>
						</LevelStyles>
						<Mode AllowAddNew="False" AllowColMoving="False" AllowDelete="False" />
						<ActionBar>
							<Actions>
								<Save Enabled="False" />
								<AddNew Enabled="False" />
								<Delete Enabled="False" />
								<EditRecord Enabled="False" />
							</Actions>
							<CustomItems>
								<px:PXToolBarButton Text="New Location">
									<AutoCallBack Command="NewLocation" Target="ds" />
								    <PopupCommand Command="Refresh" Target="grdLocations" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdViewLocation" Text="Location Details">
									<AutoCallBack Command="ViewLocation" Target="ds" />
								    <PopupCommand Command="Refresh" Target="grdLocations" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Set as Default">
									<AutoCallBack Command="SetDefault" Target="ds" />
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="Locations">
								<RowTemplate>
									<px:PXSelector ID="edLocationCD" runat="server" DataField="LocationCD" AllowEdit="True" />
								</RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="IsActive" TextAlign="Center" Type="CheckBox" Width="60px" />
									<px:PXGridColumn DataField="IsDefault" TextAlign="Center" Type="CheckBox" Width="60px" />
									<px:PXGridColumn AllowShowHide="False" DataField="LocationBAccountID" TextAlign="Right" Visible="False" />
									<px:PXGridColumn DataField="LocationID" TextAlign="Right" Visible="False" />
									<px:PXGridColumn DataField="LocationCD" />
									<px:PXGridColumn DataField="Descr" TextCase="Upper" Width="150px" />
									<px:PXGridColumn DataField="City" Width="130px" />
									<px:PXGridColumn AutoCallBack="True" DataField="CountryID" RenderEditorText="True" />
									<px:PXGridColumn DataField="State" RenderEditorText="True" Width="100px" />
									<px:PXGridColumn DataField="VTaxZoneID" Width="80px" />
									<px:PXGridColumn DataField="VExpenseAcctID" Width="108px" AutoCallBack="True" />
									<px:PXGridColumn DataField="VExpenseSubID" Width="180px" />
								</Columns>
								<Layout FormViewHeight="" />
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" />
						<Mode AllowUpdate="False" AllowAddNew="False" AllowDelete="False"></Mode>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Contacts" LoadOnDemand="True">
				<Template>
					<px:PXGrid ID="grdContacts" runat="server" Height="100%" Width="100%" AllowSearch="True" SkinID="DetailsInTab">
						<Levels>
							<px:PXGridLevel DataMember="ExtContacts">
								<RowTemplate>
								</RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="ContactBAccountID" TextAlign="Right" Visible="False" AllowShowHide="False" />
									<px:PXGridColumn DataField="ContactID" TextAlign="Right" Visible="False" />
									<px:PXGridColumn DataField="IsActive" TextAlign="Center" Type="CheckBox" Width="60px" />
									<px:PXGridColumn DataField="Salutation" Width="160px" />
									<px:PXGridColumn DataField="ContactDisplayName" Width="280px" LinkCommand = "ViewContact"/>
									<px:PXGridColumn DataField="City" TextCase="Upper" Width="180px" />
									<px:PXGridColumn DataField="EMail" Width="200px" />
									<px:PXGridColumn DataField="Phone1" Width="140px" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" />
						<Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="false" />
						<ActionBar DefaultAction="grdContacts">
							<Actions>
								<Save Enabled="False" />
								<AddNew Enabled="False" />
								<Delete Enabled="False" />
							</Actions>
							<CustomItems>
								<px:PXToolBarButton Text="New Contact">
								    <AutoCallBack Command="NewContact" Target="ds" />
								    <PopupCommand Command="Refresh" Target="grdContacts" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Contact Details">
								    <AutoCallBack Command="ViewContact" Target="ds" />
								    <PopupCommand Command="Refresh" Target="grdContacts" />
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="GL Accounts" LoadOnDemand="False">
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
					<px:PXFormView ID="DefLocationGLAccounts" runat="server" DataMember="DefLocation" RenderStyle="Simple">
						<Template>
							<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
							<px:PXSegmentMask ID="edVAPAccountID" runat="server" CommitChanges="True" DataField="VAPAccountID" />
							<px:PXSegmentMask ID="edVAPSubID" runat="server" DataField="VAPSubID" />
							<px:PXSegmentMask ID="edVExpenseAcctID" runat="server" CommitChanges="True" DataField="VExpenseAcctID" />
							<px:PXSegmentMask ID="edVExpenseSubID" runat="server" DataField="VExpenseSubID" />
						    <px:PXSegmentMask ID="edVDiscountAcctID" runat="server" CommitChanges="True" DataField="VDiscountAcctID" />
							<px:PXSegmentMask ID="edVDiscountSubID" runat="server" DataField="VDiscountSubID" />
							<px:PXSegmentMask ID="edVFreightAcctID" runat="server" CommitChanges="True" DataField="VFreightAcctID" />
							<px:PXSegmentMask ID="edVFreightSubID" runat="server" DataField="VFreightSubID" />
						</Template>
					</px:PXFormView>
					<px:PXSegmentMask CommitChanges="True" ID="edDiscTakenAcctID" runat="server" DataField="DiscTakenAcctID" />
					<px:PXSegmentMask ID="edDiscTakenSubID" runat="server" DataField="DiscTakenSubID" />
					<px:PXSegmentMask CommitChanges="True" ID="edPrepaymentAcctID" runat="server" DataField="PrepaymentAcctID" />
					<px:PXSegmentMask ID="edPrepaymentSubID" runat="server" DataField="PrepaymentSubID" />
					<px:PXSegmentMask CommitChanges="True" ID="edPOAccrualAcctID" runat="server" DataField="POAccrualAcctID" />
					<px:PXSegmentMask ID="edPOAccrualSubID" runat="server" DataField="POAccrualSubID" />
					<px:PXSegmentMask CommitChanges="True" ID="edPrebookAcctID" runat="server" DataField="PrebookAcctID" />
					<px:PXSegmentMask ID="edPrebookSubID" runat="server" DataField="PrebookSubID" />
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Tax Agency Settings" BindingContext="tab" VisibleExp="DataControls[&quot;chkTaxAgency&quot;].Value = 1">
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
					<px:PXDropDown ID="edTaxPeriodType" runat="server" DataField="TaxPeriodType" />
                    <px:PXCheckBox ID="chktaxReportFinPeriod" runat="server" DataField="TaxReportFinPeriod" />
					<px:PXSegmentMask CommitChanges="True" ID="edSalesTaxAcctID" runat="server" DataField="SalesTaxAcctID" />
					<px:PXSegmentMask CommitChanges="True" ID="edSalesTaxSubID" runat="server" DataField="SalesTaxSubID" />
					<px:PXSegmentMask CommitChanges="True" ID="edPurchTaxAcctID" runat="server" DataField="PurchTaxAcctID" />
					<px:PXSegmentMask CommitChanges="True" ID="edPurchTaxSubID" runat="server" DataField="PurchTaxSubID" />
					<px:PXSegmentMask CommitChanges="True" ID="edTaxExpenseAcctID" runat="server" DataField="TaxExpenseAcctID" />
					<px:PXSegmentMask CommitChanges="True" ID="edTaxExpenseSubID" runat="server" DataField="TaxExpenseSubID" />
					<px:PXCheckBox ID="chkUpdClosedTaxPeriods" runat="server" DataField="UpdClosedTaxPeriods" />
					<px:PXDropDown ID="edTaxReportRounding" runat="server" DataField="TaxReportRounding" />					
				    <px:PXLayoutRule runat="server" Merge="True"/>
					<px:PXNumberEdit ID="edTaxReportPrecision" runat="server" DataField="TaxReportPrecision" Size="xxs" />
                    <px:PXCheckBox ID="chkTaxUseVendorCurPrecision" runat="server" DataField="TaxUseVendorCurPrecision" CommitChanges="true" />
                    <px:PXLayoutRule runat="server" Merge="False"/>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Mailing Settings">
				<Template>
					<px:PXSplitContainer runat="server" ID="sp1" SplitterPosition="300" SkinID="Horizontal" Height="494px">
						<AutoSize Enabled="true" />
						<Template1>
							<px:PXGrid ID="gridNS" runat="server" SkinID="DetailsInTab" Width="100%" Height="150px" Caption="Mailings" AdjustPageSize="Auto" AllowPaging="True" DataSourceID="ds">
								<AutoSize Enabled="True" />
								<AutoCallBack Target="gridNR" Command="Refresh" />
								<Levels>
									<px:PXGridLevel DataMember="NotificationSources" DataKeyNames="SourceID,SetupID">
										<RowTemplate>
											<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
											<px:PXDropDown ID="edFormat" runat="server" DataField="Format" />
											<px:PXSegmentMask ID="edNBranchID" runat="server" DataField="NBranchID" />
											<px:PXCheckBox ID="chkActive" runat="server" Checked="True" DataField="Active" />
											<px:PXSelector ID="edSetupID" runat="server" DataField="SetupID" />
											<px:PXSelector ID="edReportID" runat="server" DataField="ReportID" ValueField="ScreenID" />
											<px:PXSelector ID="edNotificationID" runat="server" DataField="NotificationID" ValueField="Name" />
											<px:PXSelector ID="edEMailAccountID" runat="server" DataField="EMailAccountID" DisplayMode="Text"/>
										</RowTemplate>
										<Columns>
											<px:PXGridColumn DataField="SetupID" Width="108px" AutoCallBack="True" />
											<px:PXGridColumn DataField="NBranchID" AutoCallBack="True" Label="Branch" />
											<px:PXGridColumn DataField="EMailAccountID" Width="200px" DisplayMode="Text" />
											<px:PXGridColumn DataField="ReportID" Width="150px" AutoCallBack="True" />
											<px:PXGridColumn DataField="NotificationID" Width="150px" AutoCallBack="True" />
											<px:PXGridColumn DataField="Format" Width="54px" RenderEditorText="True" AutoCallBack="True" />
											<px:PXGridColumn DataField="Active" TextAlign="Center" Type="CheckBox" />
											<px:PXGridColumn DataField="OverrideSource" TextAlign="Center" Type="CheckBox" AutoCallBack="True" />
										</Columns>
										<Layout FormViewHeight="" />
									</px:PXGridLevel>
								</Levels>
							</px:PXGrid>
						</Template1>
						<Template2>
							<px:PXGrid ID="gridNR" runat="server" SkinID="DetailsInTab" Width="100%" Caption="Recipients" AdjustPageSize="Auto" AllowPaging="True" DataSourceID="ds">
								<AutoSize Enabled="True" />
								<Mode InitNewRow="True"></Mode>
								<Parameters>
									<px:PXSyncGridParam ControlID="gridNS" />
								</Parameters>
								<CallbackCommands>
									<Save RepaintControls="None" RepaintControlsIDs="ds" />
									<FetchRow RepaintControls="None" />
								</CallbackCommands>
								<Levels>
									<px:PXGridLevel DataMember="NotificationRecipients" DataKeyNames="NotificationID">
										<Mode InitNewRow="True"></Mode>
										<Columns>
											<px:PXGridColumn DataField="ContactType" RenderEditorText="True" Width="100px" AutoCallBack="True" />
											<px:PXGridColumn DataField="OriginalContactID" Visible="False" AllowShowHide="False" />
											<px:PXGridColumn DataField="ContactID" Width="200px">
												<NavigateParams>
													<px:PXControlParam Name="ContactID" ControlID="gridNR" PropertyName="DataValues[&quot;OriginalContactID&quot;]" />
												</NavigateParams>
											</px:PXGridColumn>
											<px:PXGridColumn DataField="Email" Width="200px" />
											<px:PXGridColumn DataField="Format" RenderEditorText="True" Width="60px" AutoCallBack="True" />
											<px:PXGridColumn DataField="Active" TextAlign="Center" Type="CheckBox" Width="60px" />
											<px:PXGridColumn DataField="Hidden" TextAlign="Center" Type="CheckBox" Width="60px" />
										</Columns>
										<RowTemplate>
											<px:PXLayoutRule ID="PXLayoutRule2" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
											<px:PXDropDown ID="edContactType" runat="server" DataField="ContactType" />
											<px:PXSelector ID="edContactID" runat="server" DataField="ContactID" AutoRefresh="True" ValueField="DisplayName" AllowEdit="True" />
										</RowTemplate>
										<Layout FormViewHeight="" />
									</px:PXGridLevel>
								</Levels>
							</px:PXGrid>
						</Template2>
					</px:PXSplitContainer>
				</Template>
			</px:PXTabItem>
		</Items>
		<AutoSize Container="Window" Enabled="True" MinWidth="300" />
	</px:PXTab>
</asp:Content>
