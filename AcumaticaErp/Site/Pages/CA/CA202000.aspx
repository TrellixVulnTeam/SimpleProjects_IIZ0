<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CA202000.aspx.cs" Inherits="Page_CA202000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <table style="width: 100%;" cellspacing="0" border="0" cellpadding="0">
	    <tr>
		    <td class="ToolBarDS" style="width: 431px; height: 26px">
			    <px:PXDataSource ID="ds" runat="server" SkinID="Transparent" BorderStyle="NotSet" Visible="True" Width="100%" PrimaryView="CashAccount" TypeName="PX.Objects.CA.CashAccountMaint">
				    <CallbackCommands>
					    <px:PXDSCallbackCommand Visible="false" Name="ViewPTInstance" DependOnGrid="grdPTInstances" StartNewGroup="true" />
					    <px:PXDSCallbackCommand Visible="false" Name="AddPTInstance" />
				    </CallbackCommands>
			    </px:PXDataSource>
		    </td>
	    </tr>
</table>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXSmartPanel ID="smpPTInstance" runat="server" Key="PTInstanceEditor" InnerPageUrl="~/Pages/CA/CA206000.aspx?PopupPanel=On" CaptionVisible="True" Caption="Card Definition" RenderIFrame="True" Visible="False">
	</px:PXSmartPanel>
	<px:PXFormView ID="form" runat="server" Width="100%" 
        Caption="Cash Account Summary" DataMember="CashAccount" NoteIndicator="True" 
        FilesIndicator="True" ActivityIndicator="True" ActivityField="NoteActivity"
		TemplateContainer="" TabIndex="1100" DataSourceID="ds">
		<%--<Parameters>
			<px:PXQueryStringParam Name="CashAccountCD" QueryStringField="CashAccountID" Type="String" OnLoadOnly="True" />
		</Parameters>--%>
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" ControlSize="M" LabelsWidth="M" />
			<px:PXSegmentMask ID="CashAccountCD" runat="server" DataField="CashAccountCD" DisplayMode="Hint"
                DataSourceID="ds"/>            
			<px:PXSegmentMask ID="edAccountID" runat="server" DataField="AccountID" 
                AutoRefresh="True" DisplayMode="Hint" DataSourceID="ds"  CommitChanges="True"/>
			<px:PXSegmentMask CommitChanges="True" ID="edSubID" runat="server" 
                DataField="SubID" AutoRefresh="True" DataSourceID="ds" />
			<px:PXSegmentMask CommitChanges="True" ID="edBranchID" runat="server" 
                DataField="BranchID" DataSourceID="ds" />
			<px:PXSelector ID="edCuryID" runat="server" DataField="CuryID" Enabled="False" 
                DataSourceID="ds" />
			<px:PXSelector ID="edCuryRateTypeID" runat="server" DataField="CuryRateTypeID" 
                AllowEdit="True" DataSourceID="ds" edit="1" />
			<px:PXTextEdit ID="edExtRefNbr" runat="server" DataField="ExtRefNbr">
            </px:PXTextEdit>
			<px:PXLayoutRule runat="server" ColumnSpan="2" />
			<px:PXTextEdit ID="edDescr" runat="server" DataField="Descr">
			</px:PXTextEdit>
			<px:PXLayoutRule runat="server" StartColumn="True" ControlSize="M" LabelsWidth="M" />
			<px:PXCheckBox CommitChanges="True" ID="chkClearingAccount" runat="server" Checked="True" DataField="ClearingAccount" />
			<px:PXCheckBox ID="chkReconcile" runat="server" Checked="True" DataField="Reconcile" />
            <px:PXCheckBox ID="chkRestrictVisibility" runat="server" Checked="True" DataField="RestrictVisibilityWithBranch" />
			<px:PXSelector ID="edReconNumberingID" runat="server" 
                DataField="ReconNumberingID" AllowEdit="True" DataSourceID="ds" edit="1" />
			<px:PXSelector ID="edReferenceID" runat="server" DataField="ReferenceID" 
                AllowEdit="True" DataSourceID="ds" edit="1" />
			<px:PXSelector ID="edStatementImportTypeName" runat="server" 
                DataField="StatementImportTypeName" DataSourceID="ds"/>
			<px:PXCheckBox ID="chkAcctSettingsAllowed" runat="server" DataField="AcctSettingsAllowed" Enabled="False" />
			<px:PXCheckBox ID="chkPTInstancesAllowed" runat="server" DataField="PTInstancesAllowed" Enabled="False" />
			
		</Template>
		<AutoSize MinWidth="200" />
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXTab ID="tab" runat="server" Height="300px" Width="100%" LoadOnDemand="True" DataMember="CurrentCashAccount" BorderStyle="None" DataSourceID="ds">
		<Items>
			<px:PXTabItem Text="Payment Methods">
				<Template>
					<px:PXGrid ID="grid" runat="server" Height="350px" Width="100%" AllowFilter="False" SkinID="DetailsInTab" DataSourceID="ds">
						<Levels>
							<px:PXGridLevel DataMember="Details" DataKeyNames="PaymentMethodID,CashAccountID">
								<RowTemplate>
									<px:PXLayoutRule runat="server" StartColumn="True" />
									<px:PXSelector ID="edPaymentMethodID" runat="server" DataField="PaymentMethodID" />
								</RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="PaymentMethodID" Width="90px" AutoCallBack="True" />
									<px:PXGridColumn AutoCallBack="True" DataField="UseForAP" TextAlign="Center" Type="CheckBox" Width="60px" />
									<px:PXGridColumn DataField="APIsDefault" TextAlign="Center" Type="CheckBox" Width="60px" AutoCallBack="True" />
									<px:PXGridColumn DataField="APAutoNextNbr" TextAlign="Center" Type="CheckBox" Width="90px" />
									<px:PXGridColumn DataField="APLastRefNbr" Width="120px" />
									<px:PXGridColumn DataField="APBatchLastRefNbr" Width="120px" />
									<px:PXGridColumn AutoCallBack="True" DataField="UseForAR" TextAlign="Center" Type="CheckBox" Width="60px" />
									<px:PXGridColumn DataField="ARIsDefault" TextAlign="Center" Type="CheckBox" Width="70px"/>
									<px:PXGridColumn DataField="ARIsDefaultForRefund" TextAlign="Center" Type="CheckBox" Width="120px"/>
									<px:PXGridColumn DataField="ARAutoNextNbr" TextAlign="Center" Type="CheckBox" Width="90px" />
									<px:PXGridColumn DataField="ARLastRefNbr" Width="120px" />
								</Columns>
								<Layout FormViewHeight="" />
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Clearing Accounts" BindingContext="form" VisibleExp="DataControls[&quot;chkClearingAccount&quot;].Value == 0">
				<Template>
					<px:PXGrid ID="gridDepositAccount" runat="server" Height="300px" Width="100%" ActionsPosition="Top" AllowFilter="False" SkinID="DetailsInTab" DataSourceID="ds" TabIndex="14900">
						<Levels>
							<px:PXGridLevel DataMember="Deposits" DataKeyNames="AccountID,DepositAcctID,PaymentMethodID">
								<RowTemplate>
									<px:PXLayoutRule runat="server" StartColumn="True" />
									<px:PXSegmentMask ID="edDepositAcctID" runat="server" DataField="DepositAcctID" AutoRefresh="True" />
									<px:PXSelector ID="edPaymentMethodID1" runat="server" DataField="PaymentMethodID" />
									<px:PXSelector ID="edChargeEntryTypeID" runat="server" DataField="ChargeEntryTypeID" />
								</RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="AccountID" TextAlign="Right" Width="154px" />
									<px:PXGridColumn DataField="DepositAcctID" Width="154px" />
									<px:PXGridColumn DataField="PaymentMethodID" Width="154px" />
									<px:PXGridColumn DataField="ChargeEntryTypeID" Width="154px" />
									<px:PXGridColumn DataField="ChargeRate" Width="154px" />
								</Columns>
								<Layout FormViewHeight="" />
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Entry Types">
				<Template>
					<px:PXGrid ID="grid2" runat="server" Height="150px" Style="z-index: 100; left: 0px; top: 0px;" Width="100%" ActionsPosition="Top" AllowFilter="False" BorderStyle="None" BorderWidth="0px" 
						SkinID="Details" DataSourceID="ds">
						<Levels>
							<px:PXGridLevel DataMember="ETDetails" DataKeyNames="AccountID,EntryTypeID">
								<RowTemplate>
									<px:PXLayoutRule runat="server" StartColumn="True" />
									<px:PXSelector ID="edEntryTypeID" runat="server" DataField="EntryTypeID" AllowEdit="True" />
									<px:PXSegmentMask ID="edOffsetAccountID" runat="server" DataField="OffsetAccountID" />
									<px:PXSelector ID="edTaxZoneID" runat="server" DataField="TaxZoneID" />
									<px:PXDropDown ID="edCAEntryType__Module" runat="server" DataField="CAEntryType__Module" Enabled="False" />
									<px:PXSegmentMask ID="edOffsetSubID" runat="server" DataField="OffsetSubID" />
									<px:PXSegmentMask ID="edCAEntryType__AccountID" runat="server" DataField="CAEntryType__AccountID" Enabled="False" AllowEdit="False"/>
									<px:PXSegmentMask ID="edCAEntryType__SubID" runat="server" DataField="CAEntryType__SubID" Enabled="False"  AllowEdit="False"/>
									<px:PXSelector ID="edCAEntryType__ReferenceID" runat="server" DataField="CAEntryType__ReferenceID" Enabled="False" AllowEdit="False" />
									<px:PXDropDown ID="edCAEntryType__DrCr" runat="server" DataField="CAEntryType__DrCr" Enabled="False" AllowEdit="False"/>
									<px:PXTextEdit Size="xl" ID="edCAEntryType__Descr" runat="server" DataField="CAEntryType__Descr" Enabled="False" />
								</RowTemplate>
								<Columns>
									<px:PXGridColumn AllowShowHide="False" DataField="AccountID" TextAlign="Right" Visible="False" />
									<px:PXGridColumn DataField="EntryTypeID" Width="90px" AutoCallBack="True" />
									<px:PXGridColumn DataField="CAEntryType__DrCr" Width="90px" Type="DropDownList" />
									<px:PXGridColumn DataField="CAEntryType__Module" Width="60px" />
									<px:PXGridColumn DataField="CAEntryType__AccountID" Width="120px" />
									<px:PXGridColumn DataField="CAEntryType__SubID" Width="140px" />
									<px:PXGridColumn DataField="CAEntryType__ReferenceID" Width="100px" />
									<px:PXGridColumn DataField="CAEntryType__Descr" Width="200px" />
									<px:PXGridColumn DataField="CAEntryType__UseToReclassifyPayments" Width="200px" Type="CheckBox" TextAlign="Center"/>
                                    <px:PXGridColumn AutoCallBack="True" DataField="OffsetCashAccountID" Width="120px" />
									<px:PXGridColumn AutoCallBack="True" DataField="OffsetAccountID" Width="120px" />
									<px:PXGridColumn AutoCallBack="True" DataField="OffsetSubID" Width="108px" />
									<px:PXGridColumn AutoCallBack="True" DataField="TaxZoneID" Width="108px" />
								</Columns>
								<Layout FormViewHeight="" />
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Corporate Cards" BindingContext="form" VisibleExp="DataControls[&quot;chkPTInstancesAllowed&quot;].Value = 1">
				<Template>
					<px:PXGrid ID="grdPTInstances" runat="server" Height="150px" Width="100%" ActionsPosition="Top" NoteField="NoteID" BorderStyle="None" BorderWidth="0px" SkinID="Details" DataSourceID="ds" 
						FilesField="NoteFiles">
						<Levels>
							<px:PXGridLevel DataMember="PTInstances" DataKeyNames="CashAccountID,PTInstanceID">
								<Columns>
									<px:PXGridColumn DataField="PaymentMethodID" />
									<px:PXGridColumn DataField="BAccountID" />
									<px:PXGridColumn DataField="BAccount__AcctName" Width="200px" />
									<px:PXGridColumn DataField="Descr" Width="200px" />
									<px:PXGridColumn DataField="IsActive" TextAlign="Center" Type="CheckBox" Width="60px" />
								</Columns>
								<Layout FormViewHeight="" />
							</px:PXGridLevel>
						</Levels>
						<ActionBar DefaultAction="cmdViewPTInstance">
							<CustomItems>
								<px:PXToolBarButton Text="Add Card">
								    <AutoCallBack Command="AddPTInstance" Target="ds" />
								    <PopupCommand Command="Refresh" Target="grdPTInstances" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdViewPTInstance" Text="View Cards">
								    <AutoCallBack Command="ViewPTInstance" Target="ds" />
								    <PopupCommand Command="Refresh" Target="grdPTInstances" />
								</px:PXToolBarButton>
							</CustomItems>
						</ActionBar>
						<AutoSize Enabled="True" MinHeight="150" MinWidth="200" />
						<Mode AllowAddNew="False" AllowDelete="False" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Remittance Settings" BindingContext="form" VisibleExp="DataControls[&quot;chkAcctSettingsAllowed&quot;].Value = 1">
				<Template>
                    <px:PXSplitContainer runat="server" ID="sp1" SplitterPosition="300" SkinID="Verical" Height="500px">
                        <AutoSize Enabled="True" />                        
                        <Template1>
                            <px:PXGrid ID="gridPaymentMethodForRemittance" runat="server" DataSourceID="ds" Width="100%" Caption="Payment Method"  SkinID="DetailsWithFilter" Height="180px" SyncPosition="True" AutoAdjustColumns="true">
                                <AutoCallBack Target="grdPaymentDetails" Command="Refresh" />
                                <Levels>
							        <px:PXGridLevel DataMember="PaymentMethodForRemittance" DataKeyNames="AccountID,PaymentMethodID">
								        <Columns>
                                            <px:PXGridColumn DataField="PaymentMethodID" TextAlign="Left" Width="140px" />
                                        </Columns>
                                        <Layout FormViewHeight="" />
                                    </px:PXGridLevel>
                                 </Levels>
                                 <AutoSize Enabled="True" MinWidth="120" />
						         <ActionBar ActionsVisible="False">
							        <Actions>
								        <AddNew Enabled="False" />
								        <Delete Enabled="False" />                                        
							        </Actions>
						        </ActionBar>   							
                            </px:PXGrid>                            
                        </Template1>
                        <Template2>                      
					        <px:PXGrid ID="grdPaymentDetails" runat="server" Caption="Remittance Details" MatrixMode="True" SkinID ="DetailsWithFilter" DataSourceID="ds" Width="100%" >
                                <CallbackCommands>
                                    <Refresh SelectControlsIDs="gridPaymentMethodForRemittance" />
                                </CallbackCommands>
						    <Levels>
							<px:PXGridLevel DataMember="PaymentDetails" DataKeyNames="AccountID,PaymentMethodID,DetailID">
								<Columns>
									<px:PXGridColumn DataField="AccountID" TextAlign="Right" Width="120px" />
									<px:PXGridColumn DataField="PaymentMethodID" />
									<px:PXGridColumn DataField="DetailID"/>
									<px:PXGridColumn DataField="PaymentMethodDetail__descr" Width="190px" />
									<px:PXGridColumn AllowShowHide="False" DataField="DetailValue" Width="280px" />
								</Columns>
								<Layout FormViewHeight="" />
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" MinHeight="100" />
						<ActionBar ActionsVisible="False">
							<Actions>
								<AddNew Enabled="False" />
								<Delete Enabled="False" />
							</Actions>
						</ActionBar>
					    </px:PXGrid>
                        </Template2>
                    </px:PXSplitContainer>
				</Template>
			</px:PXTabItem>
		    <px:PXTabItem BindingContext="form" Text="Signature">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" ControlSize="XM" LabelsWidth="XS"/>
                    <px:PXTextEdit ID="edSignatureDescr" runat="server" DataField="SignatureDescr"/>
                    <px:PXImageUploader ID="edSignature" runat="server" DataField="Signature" Height="300px" Width="300px"/>
                </Template>
            </px:PXTabItem>
		</Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXTab>
</asp:Content>
