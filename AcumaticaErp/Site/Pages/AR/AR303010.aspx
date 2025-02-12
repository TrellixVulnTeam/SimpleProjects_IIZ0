<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AR303010.aspx.cs" Inherits="Page_AR303010" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="CustomerPaymentMethod" TypeName="PX.Objects.AR.CustomerPaymentMethodMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand Name="ViewBillAddressOnMap" Visible="false" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" PopupVisible="true" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="Delete" PopupVisible="true" ClosePopup="true" />
			<px:PXDSCallbackCommand Name="Cancel" PopupVisible="true" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
			<px:PXDSCallbackCommand StartNewGroup="True" Name="ValidateAddresses" Visible="True" CommitChanges="True" PopupVisible="true" />
			<px:PXDSCallbackCommand Name="CreateCCPaymentMethodHF" PopupCommand="SyncCCPaymentMethods" PopupCommandTarget="ds" DependOnGrid="grid" Visible="False"/>
			<px:PXDSCallbackCommand Name="SyncCCPaymentMethods" CommitChanges="true" Visible="False" />
			<px:PXDSCallbackCommand Name="ManageCCPaymentMethodHF" DependOnGrid="grid" Visible="False"/>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="CustomerPaymentMethod" Caption="Payment Method Selection" NoteIndicator="True" FilesIndicator="True"
		ActivityIndicator="true" LinkIndicator="true" NotifyIndicator="true"  DefaultControlID="edBAccountID">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
			<px:PXLayoutRule runat="server" Merge="True" />
			<px:PXSegmentMask ID="edBAccountID" runat="server" DataField="BAccountID" AllowEdit="True" />
			<px:PXCheckBox ID="chkIsActive" runat="server" DataField="IsActive" />
			<px:PXLayoutRule runat="server" Merge="False" />
			<px:PXLayoutRule runat="server" Merge="True" />
			<px:PXSelector CommitChanges="True" ID="edPaymentMethodID" runat="server" DataField="PaymentMethodID" />
			<px:PXCheckBox ID="chkHasBillingInfo" runat="server" DataField="HasBillingInfo" />
			<px:PXLayoutRule runat="server" Merge="False" />
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" Merge="True" />
			<px:PXSelector ID="edCCProcessingCenterID" runat="server" DataField="CCProcessingCenterID" CommitChanges="True" AutoRefresh="True"/>
			<px:PXCheckBox ID="edSyncDeletion" runat="server" DataField="SyncronizeDeletion" />
			<px:PXLayoutRule ID="PXLayoutRule2" runat="server" Merge="False" />
			<px:PXSelector ID="edCustomerCCPID" runat="server" DataField="CustomerCCPID" CommitChanges="True" AutoRefresh="True"/>
			<px:PXSegmentMask CommitChanges="True" ID="edCashAccountID" runat="server" DataField="CashAccountID" AllowEdit="True" AutoRefresh="True"/>
			<px:PXTextEdit ID="edDescr" runat="server" AllowNull="False" DataField="Descr" /></Template>
		<Parameters>
			<px:PXControlParam ControlID="form" Name="BAccountID" PropertyName="NewDataKey[&quot;BAccountID&quot;]" Type="String" />
		</Parameters>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXTab ID="tab" runat="server" DataSourceID="ds" Height="580px" Style="z-index: 100" Width="100%" DataMember="CurrentCPM">
		<AutoSize Enabled="True" Container="Window" MinWidth="300" MinHeight="250"></AutoSize>
		<Activity HighlightColor="" SelectedColor="" Width="" Height=""></Activity>
		<Items>
			<px:PXTabItem Text="Payment Method Details">
				<Template>
					<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" MatrixMode="True" SkinID="DetailsInTab" TabIndex="900">
						<ActionBar>
							<CustomItems>
								<px:PXToolBarButton Text="Create CC Payment Method HF" CommandSourceID="ds" CommandName="CreateCCPaymentMethodHF">
									<PopupCommand Target="ds" Command="SyncCCPaymentMethods" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Manage CC Payment Method HF" CommandSourceID="ds" CommandName="ManageCCPaymentMethodHF"/>
							</CustomItems>
						</ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="Details">
								<Columns>
									<px:PXGridColumn DataField="DetailID_PaymentMethodDetail_descr" Width="200px" />
									<px:PXGridColumn DataField="Value" Width="300px" CommitChanges="True"/>
								</Columns>
								<Layout FormViewHeight="" />
							</px:PXGridLevel>
						</Levels>
						<AutoSize Container="Parent" Enabled="True" MinHeight="150" />
						<Mode AllowDelete="False" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Billing Info" BindingContext="form" VisibleExp="DataControls[&quot;chkHasBillingInfo&quot;].Value = 1">
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
					<px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Contact Information" />
					<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkIsBillContSameAsMain" runat="server" DataField="IsBillContactSameAsMain" />
					<px:PXFormView ID="BillContact" runat="server" DataMember="BillContact" RenderStyle="Simple" DataSourceID="ds">
						<Template>
							<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
							<px:PXTextEdit ID="edFirstName" runat="server" DataField="FirstName" />
							<px:PXTextEdit ID="edLastName" runat="server" DataField="LastName" />
							<px:PXMaskEdit ID="edPhone1" runat="server" DataField="Phone1" />
							<px:PXMaskEdit ID="edPhone2" runat="server" DataField="Phone2" />
							<px:PXMaskEdit ID="edFax" runat="server" DataField="Fax" />
							<px:PXMailEdit ID="edEMail" runat="server" DataField="EMail" CommitChanges="True"/>
							<px:PXLinkEdit ID="edWebSite" runat="server" DataField="WebSite" CommitChanges="True"/>
						</Template>
					</px:PXFormView>
					<px:PXLayoutRule runat="server" LabelsWidth="SM" ControlSize="XM" />
					<px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Address" />
					<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkIsBillAddressSameAsMain" runat="server" DataField="IsBillAddressSameAsMain" />
					<px:PXFormView ID="BillAddress" runat="server" DataMember="BillAddress" RenderStyle="Simple" DataSourceID="ds">
						<Template>
							<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
							<px:PXCheckBox ID="edIsValidated" runat="server" DataField="IsValidated" Enabled="False"/>
							<px:PXTextEdit ID="edAddressLine1" runat="server" DataField="AddressLine1" />
							<px:PXTextEdit ID="edAddressLine2" runat="server" DataField="AddressLine2" />
							<px:PXTextEdit ID="edCity" runat="server" DataField="City" />
							<px:PXSelector CommitChanges="True" ID="edCountryID" runat="server" DataField="CountryID" AutoRefresh="True" AllowEdit="True" />
							<px:PXSelector ID="edState" runat="server" DataField="State" AutoRefresh="true" AllowEdit="True"/>
							<px:PXLayoutRule runat="server" Merge="True" />
							<px:PXMaskEdit Size="s" ID="edPostalCode" runat="server" DataField="PostalCode" />
							<px:PXButton Size="xs" ID="btnViewBillAddressOnMap" runat="server" CommandName="ViewBillAddressOnMap" CommandSourceID="ds" Text="View on Map"/>
							<px:PXLayoutRule runat="server" Merge="False" />
						</Template>
					</px:PXFormView>
				</Template>
			</px:PXTabItem>
		</Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="300" MinWidth="300" />
	</px:PXTab>
</asp:Content>
