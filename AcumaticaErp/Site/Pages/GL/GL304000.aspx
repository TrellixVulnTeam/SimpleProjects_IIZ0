<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
	ValidateRequest="false" CodeFile="GL304000.aspx.cs" Inherits="Page_GL304000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" TypeName="PX.Objects.GL.JournalWithSubEntry"
		PrimaryView="BatchModule" style="float:left">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Insert" PostData="Self" />
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="Last" PostData="Self" />
			<px:PXDSCallbackCommand Name="CurrencyView" CommitChanges="True"  Visible="False" />
			<px:PXDSCallbackCommand Name="Release" CommitChanges="True" StartNewGroup="True" />												
			<px:PXDSCallbackCommand Name="ViewDocument" DependOnGrid="grid"  Visible="false" />
			<px:PXDSCallbackCommand Name="ShowTaxes" CommitChanges="True" Visible="False" DependOnGrid="grid" />
		</CallbackCommands>
	</px:PXDataSource>

	<px:PXToolBar ID="toolbar1" runat="server" SkinID="Navigation" CommandSourceID="ds" CallbackUpdatable="True">
		<Items>							
		</Items>
		<Layout ItemsAlign="Left" />
	</px:PXToolBar>
	<div style="clear: left" />

</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100"
		Width="100%" DataKeyNames="Module,BatchNbr" DataMember="BatchModule" Caption="Batch Summary" NoteIndicator="True" FilesIndicator="True" 
		ActivityIndicator="true" ActivityField="NoteActivity" LinkIndicator="true" NotifyIndicator="true" EmailingGraph="PX.Objects.CR.CREmailActivityMaint,PX.Objects"
			DefaultControlID="edModule" >
		<Parameters>
			<px:PXQueryStringParam Name="GLDocBatch.module" QueryStringField="Module" Type="String"
				OnLoadOnly="True" />
		</Parameters>

        <Activity HighlightColor="" SelectedColor="" Width="" Height=""></Activity>
		<Searches>
			<px:PXQueryStringParam Name="Module" QueryStringField="Module" Type="String" OnLoadOnly="True" />
			<px:PXQueryStringParam Name="BatchNbr" QueryStringField="BatchNbr" Type="String"
				OnLoadOnly="True" />
			<px:PXControlParam Name="Module" ControlID="form" PropertyName="NewDataKey[&quot;Module&quot;]"
				Type="String" />
			<px:PXControlParam Name="BatchNbr" ControlID="form" PropertyName="NewDataKey[&quot;BatchNbr&quot;]"
				Type="String" />
		</Searches>
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
			<%--<px:PXDropDown ID="edModule" runat="server" DataField="Module" SelectedIndex="-1" Enabled ="false" />--%>
			<px:PXSelector ID="edBatchNbr" runat="server" DataField="BatchNbr" DataKeyNames="Module,BatchNbr" AutoRefresh="true"/>				
			<px:PXDropDown ID="edStatus" runat="server" DataField="Status" SelectedIndex="-1" Size="s"  Enabled="False"/>			
			<px:PXCheckBox ID="chkHold" runat="server" DataField="Hold" CommitChanges="True" Height="16px" />
			<px:PXDateTimeEdit ID="edDateEntered" runat="server" DataField="DateEntered" CommitChanges="True" DisplayFormat="d" />
			<px:PXSelector ID="edFinPeriodID" runat="server" DataField="FinPeriodID" CommitChanges="True" MaxLength="6" DataKeyNames="FinPeriodID" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
			<px:PXSegmentMask ID="edBranchID" runat="server" DataField="BranchID"  CommitChanges="True" DataKeyNames="BranchCD"  DataSourceID="ds"  />
			<px:PXSelector ID="edLedgerID" runat="server" DataField="LedgerID" CommitChanges="True" DataKeyNames="LedgerCD"  DataSourceID="ds" Enabled="false"/>
			<pxa:PXCurrencyRate ID="edCury" runat="server"  DataMember="_Currency_" DataField="CuryID" RateTypeView="_GLDocBatch_CurrencyInfo_"/>			
			<px:PXLayoutRule runat="server" ColumnSpan="2" />
			<px:PXTextEdit ID="edDescription" runat="server" DataField="Description" MaxLength="255"  />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
			<px:PXNumberEdit ID="edCuryDebitTotal" runat="server" DataField="CuryDebitTotal" Enabled="False" />
			<px:PXNumberEdit ID="edCuryCreditTotal" runat="server" DataField="CuryCreditTotal" Enabled="False" />
			<px:PXNumberEdit ID="edCuryControlTotal" runat="server" DataField="CuryControlTotal"  /></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXTab ID="tab" runat="server" Height="126px" Style="z-index: 100;" 
		Width="100%">

<AutoSize Enabled="True" Container="Window" MinHeight="180"></AutoSize>

		<Activity HighlightColor="" SelectedColor="" Width="" Height=""></Activity>
		<Items>
			<px:PXTabItem Text="Transactions">
				<Template>
					<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100;" Width="100%" FilesField="NoteFiles" SkinID="DetailsInTab" SyncPosition="true"  >
						<Levels>
							<px:PXGridLevel DataMember="GLTranModuleBatNbr" DataKeyNames="Module,BatchNbr,LineNbr"
								SortOrder="GroupTranID, LineNbr">
								<RowTemplate>
									<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
									<px:PXSelector ID="edTranCode" runat="server" DataField="TranCode" DataKeyNames="TranCode" DataSourceID="ds" ValueField="TranCode" CommitChanges="true"  />
									<px:PXDateTimeEdit ID="edTranDate" runat="server" DataField="TranDate"/>
									<px:PXSelector ID="edBAccountID" runat="server" DataField="BAccountID" DataKeyNames="AcctCD" DataSourceID="ds" AutoRefresh="true" AllowAddNew="true" CommitChanges="true">
										<Parameters>
											<px:PXSyncGridParam ControlID="grid" />
										</Parameters>
									</px:PXSelector>
									<px:PXSegmentMask ID="edLocationID" runat="server" DataField="LocationID" DataKeyNames="BAccountID,LocationCD" DataMember="_Location_GLTranDoc.bAccountID_" DataSourceID="ds"  AutoRefresh="true" >
										<Parameters>
											<px:PXSyncGridParam ControlID="grid" />
										</Parameters>
									</px:PXSegmentMask>									
									<px:PXSelector ID="edEntryTypeID" runat="server" DataField="EntryTypeID"  CommitChanges="true" />
									<px:PXSegmentMask ID="edDebitAccountID" runat="server" DataField="DebitAccountID" DataKeyNames="AccountCD"  DataSourceID="ds" OnValueChange="Commit" AutoRefresh="True">
									</px:PXSegmentMask>
									<px:PXSegmentMask ID="edDebitSubID" runat="server" DataField="DebitSubID" DataKeyNames="Value"  DataSourceID="ds" OnValueChange="Commit">
										<Parameters>
											<px:PXControlParam ControlID="grid" Name="GLTranDoc.debitAccountID" PropertyName="DataValues[&quot;DebitAccountID&quot;]" />
										</Parameters>
									</px:PXSegmentMask>
									<px:PXSegmentMask ID="edCreditAccountID" runat="server" DataField="CreditAccountID" DataKeyNames="AccountCD"  DataSourceID="ds" OnValueChange="Commit" AutoRefresh="True">									
									</px:PXSegmentMask>
									<px:PXSegmentMask ID="edCreditSubID" runat="server" DataField="CreditSubID" DataKeyNames="Value" DataSourceID="ds"  OnValueChange="Commit">
										<Parameters>
											<px:PXControlParam ControlID="grid" Name="GLTranDoc.creditAccountID" PropertyName="DataValues[&quot;CreditAccountID&quot;]" />
										</Parameters>
									</px:PXSegmentMask>
									<px:PXTextEdit ID="edExtRefNbr" runat="server" AllowNull="False" DataField="ExtRefNbr"  />
									<px:PXNumberEdit ID="edCuryTranTotal" runat="server" AllowNull="False" DataField="CuryTranTotal"  />
									<px:PXNumberEdit ID="edCuryTranAmt" runat="server" AllowNull="False" DataField="CuryTranAmt"  />
									<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
									<px:PXCheckBox ID="chkSplit" runat="server" DataField="Split" />
									<px:PXTextEdit ID="edTranDesc" runat="server" DataField="TranDesc" MaxLength="60"  />									
									<px:PXSegmentMask ID="edProjectID" runat="server" DataField="ProjectID" AutoRefresh = "true"/>
									<px:PXSegmentMask ID="edTaskID" runat="server" DataField="TaskID" AutoRefresh = "true"/>
									<px:PXSelector ID="edPaymentMethodID" runat="server" DataField="PaymentMethodID" DataKeyNames="PaymentMethodID" DataSourceID="ds" CommitChanges="true">
										<Parameters>
											<px:PXSyncGridParam ControlID="grid" />
										</Parameters>
									</px:PXSelector>									
									<px:PXSelector ID="edPMInstanceID" runat="server" DataField="PMInstanceID" DataKeyNames="BAccountID,PMInstanceID" DataSourceID="ds" TextField="Descr"  AutoRefresh="true" CommitChanges="true">
										<Parameters>
											<px:PXSyncGridParam ControlID="grid" />
										</Parameters>
									</px:PXSelector>
									<px:PXSelector ID="edTermsID" runat="server" DataField="TermsID" DataKeyNames="TermsID" DataMember="_Terms_" DataSourceID="ds"/>
									<px:PXDateTimeEdit ID="edDueDate" runat="server" DataField="DueDate"  />
									<px:PXDateTimeEdit ID="edDiscDate" runat="server" DataField="DiscDate" />
									<px:PXNumberEdit ID="edCuryDiscAmt" runat="server" AllowNull="False" DataField="CuryDiscAmt"  />
									<px:PXSelector ID="edTaxZoneID" runat="server" DataField="TaxZoneID"  DataSourceID="ds" />
									<px:PXSelector ID="edTaxCategoryID" runat="server" DataField="TaxCategoryID" DataKeyNames="TaxCategoryID" DataMember="_TaxCategory_" DataSourceID="ds"  AutoRefresh="True"/>									
									</RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="RefNbr" Label="RefNbr" Width="48px" LinkCommand="ViewDocument"/>
									<px:PXGridColumn DataField="TranCode" Label="Tran Code" AutoCallBack="True" RenderEditorText="True" Width="54px" />
									<px:PXGridColumn DataField="TranType" Label="Type" MaxLength="3" AutoCallBack="True" Width="81px" />
									<px:PXGridColumn DataField="TranDate" Label="Transaction Date" AllowUpdate="False" Width="90px" />
									<px:PXGridColumn DataField="Module" Label="Module" MaxLength="2" Visible="False" />
									<px:PXGridColumn DataField="BatchNbr" Label="Batch Number" Visible="False" MaxLength="15" />
									<px:PXGridColumn DataField="LineNbr" Label="Line Nbr." AllowUpdate="False" TextAlign="Right" Visible="False" />
									<px:PXGridColumn DataField="BAccountID" Label="Customer/Vendor" Width="108px" AllowUpdate="False" AutoCallBack="True" />
									<px:PXGridColumn DataField="LocationID" Label="Location" Width="54px" AutoCallBack="True" />
									<px:PXGridColumn DataField="EntryTypeID" AutoCallBack ="true" />
									<px:PXGridColumn DataField="DebitAccountID" Label="Account" Width="54px" AutoCallBack="True" />
									<px:PXGridColumn DataField="DebitSubID"  Label="Subaccount" Width="98px" />
									<px:PXGridColumn DataField="CreditAccountID"  Label="Account" Width="54px" AutoCallBack="True" />
									<px:PXGridColumn DataField="CreditSubID" Label="Subaccount" Width="98px" />
                                    <px:PXGridColumn DataField="ExtRefNbr" Label="Ref. Number" MaxLength="15" Width="90px" AllowNull="False" />
									<px:PXGridColumn DataField="CuryTranTotal" Label="Tran Total" Width="81px" AllowNull="False" TextAlign="Right"  AutoCallBack="true" />
									<px:PXGridColumn DataField="CuryTranAmt" Label="Tran Amount" Width="81px" AllowNull="False" TextAlign="Right"  AutoCallBack="true" />
									<px:PXGridColumn DataField="TaxZoneID" Label="Tax Zone" Width="81px" AutoCallBack="True"/>
									<px:PXGridColumn DataField="TaxCategoryID" Label="Tax Category" Width="81px" AutoCallBack="True" />
									<px:PXGridColumn DataField="CuryTaxAmt" Label="Tran Amount" Width="81px" AllowNull="true"  TextAlign="Right" LinkCommand="ShowTaxes"/>									
									<px:PXGridColumn DataField="Split" Label="Split" AllowUpdate="False" Width="30px" AutoCallBack="True" Type="CheckBox" />
									<px:PXGridColumn DataField="TranDesc" Label="Transaction Description" Width="150px" MaxLength="60" />
									<px:PXGridColumn DataField="ProjectID" Label="Project" Width="108px" AutoCallBack="True" />
                                    <px:PXGridColumn DataField="TaskID" Label="Task" Width="108px" AutoCallBack="True" />									
									<px:PXGridColumn DataField="PaymentMethodID" Label="Payment Method" Width="81px" AutoCallBack="True" MaxLength="10" />
									<px:PXGridColumn DataField="PMInstanceID" Label="Payment Method" Width="120px" AutoCallBack="True" DisplayMode="Text" />
									<px:PXGridColumn DataField="TermsID" Label="Terms" Width="81px" AutoCallBack="True" />
									<px:PXGridColumn DataField="DueDate" Label="Due Date" Width="90px" />
									<px:PXGridColumn DataField="DiscDate" Label="Cash Discount Date" Width="90px" />
									<px:PXGridColumn DataField="CuryDiscAmt" Label="Cash Discount" AllowNull="True" TextAlign="Right" Width="81px" />
									<px:PXGridColumn DataField="CuryInclTaxAmt" Label="Tran Amount" Width="81px" AllowNull="True" TextAlign="Right"/>
									<px:PXGridColumn DataField="CuryDocTotal" Label="Tran Amount" Width="81px" AllowNull="False" TextAlign="Right"/>									
									<px:PXGridColumn DataField="GroupTranID" Label="GroupTranID" TextAlign="Right" Width="81px" Visible="false" />
									<px:PXGridColumn DataField="TranModule" Label="Module" Width="18px" />
									<px:PXGridColumn DataField="DocCreated" Label="Doc. Created" Width="54px" Type="CheckBox" />
									<px:PXGridColumn DataField="Released" Label="Released" Width="54px" Type="CheckBox" Visible="false"/>									
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" />
						<Mode InitNewRow="True" AllowFormEdit="False" AllowUpload="True" AllowSort="False" />
						<LevelStyles>
							<RowForm Height="159px" />
						</LevelStyles>
						<ActionBar>
							<CustomItems>
								<px:PXToolBarButton Text="View Source Document">
								    <AutoCallBack Command="ViewDocument" Target="ds" />
								</px:PXToolBarButton>
								<px:PXToolBarButton Text="Taxes" Key="cmdLS" CommandName="ShowTaxes" CommandSourceID="ds"
									DependOnGrid="grid" />
							</CustomItems>
						</ActionBar>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="AP Payment Applications" Visible="True">
                <Template>
                    <px:PXSplitContainer runat="server" ID="sp1" SplitterPosition="300" SkinID="Horizontal" Height="500px">
                        <AutoSize Enabled="True" />                        
                        <Template1>
                            <px:PXGrid ID="gridAPPayments" runat="server" DataSourceID="ds" Style="z-index: 100; top: 0px; left: 0px; height: 180px;"
                                Width="100%" FilesField="NoteFiles" SkinID="DetailsInTab" Height="180px" SyncPosition="True"
								TabIndex="8100">
                                <AutoCallBack Target="gridAPAppDocuments" Command="Refresh" />
                                <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
                                <Levels>
                                    <px:PXGridLevel DataMember="APPayments" DataKeyNames="Module,BatchNbr,LineNbr">
                                        <Columns>
                                            <px:PXGridColumn DataField="TranCode" />
											<px:PXGridColumn DataField="RefNbr" />
                                            <px:PXGridColumn DataField="TranDate" Width="90px" />
                                            <px:PXGridColumn DataField="BAccountID" />
                                            <px:PXGridColumn DataField="LocationID" />
                                            <px:PXGridColumn DataField="PaymentMethodID" />
                                            <px:PXGridColumn DataField="DebitAccountID" />
                                            <px:PXGridColumn DataField="DebitSubID" />
                                            <px:PXGridColumn DataField="CreditAccountID" />
                                            <px:PXGridColumn DataField="CreditSubID" />
                                            <px:PXGridColumn DataField="ExtRefNbr" />
                                            <px:PXGridColumn DataField="CuryID" />
											<px:PXGridColumn DataField="CuryApplAmt" />
                                            <px:PXGridColumn DataField="curyUnappliedBal" />
                                            <px:PXGridColumn AllowNull="False" DataField="CuryTranAmt" TextAlign="Right" Width="100px" />
                                            <px:PXGridColumn DataField="TranDesc" Width="200px" />                                            
                                        </Columns>
                                    </px:PXGridLevel>
                                </Levels>
                                <AutoSize Enabled="True" />                            	
								<actionbar>
									<actions>
										<addnew enabled="False" menuvisible="False" />
										<delete enabled="False" menuvisible="False" />
										<editrecord enabled="False" menuvisible="False" />
									</actions>
								</actionbar>																
                            </px:PXGrid>
                        </Template1>
                        <Template2>
                            <px:PXGrid ID="gridAPAppDocuments" runat="server" Width="100%" Height="120px" 
								DataSourceID="ds" SkinID="DetailsInTab" AdjustPageSize="Auto"
                                Caption="Documents to Apply" TabIndex="8300">                                
                                <CallbackCommands>
                                    <Refresh SelectControlsIDs="gridAPPayments" />
                                </CallbackCommands>
                                <Levels>
                                    <px:PXGridLevel DataMember="APAdjustments">
										<RowTemplate>
											<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
											<px:PXDropDown ID="edAdjdDocType" runat="server" DataField="AdjdDocType" CommitChanges="True" />
											<px:PXSelector ID="edAdjdRefNbr" runat="server" AutoRefresh="True" CommitChanges="True" DataField="AdjdRefNbr" AllowEdit="True">
                                                <Parameters>											        
											        <px:PXControlParam ControlID="gridAPAppDocuments" Name="aAdjdDocType" PropertyName="DataValues[&quot;AdjdDocType&quot;]" />
										        </Parameters>                                                
                                            </px:PXSelector>                                             
											<px:PXNumberEdit ID="edCuryAdjgAmt" runat="server" CommitChanges="True" DataField="CuryAdjgAmt" />
											<px:PXNumberEdit ID="edCuryAdjgDiscAmt" runat="server" CommitChanges="True" DataField="CuryAdjgDiscAmt" />
											<px:PXNumberEdit ID="edCuryAdjgWhTaxAmt" runat="server" CommitChanges="True" DataField="CuryAdjgWhTaxAmt" />
											<px:PXDateTimeEdit ID="edAdjdDocDate" runat="server" DataField="AdjdDocDate" Enabled="False" />
											<px:PXDateTimeEdit ID="edAPInvoice__DueDate" runat="server" DataField="APInvoice__DueDate" />
											<px:PXDateTimeEdit ID="edAPInvoice__DiscDate" runat="server" DataField="APInvoice__DiscDate" />
											<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
											<px:PXNumberEdit ID="edAdjdCuryRate" runat="server" CommitChanges="True" DataField="AdjdCuryRate" />
											<px:PXTextEdit ID="edAdjgDocType" runat="server" DataField="AdjgDocType" />
											<px:PXNumberEdit ID="edCuryDocBal" runat="server" DataField="CuryDocBal" Enabled="False" />
											<px:PXTextEdit ID="edAdjgRefNbr" runat="server" DataField="AdjgRefNbr" />
											<px:PXNumberEdit ID="edCuryDiscBal" runat="server" DataField="CuryDiscBal" Enabled="False" />
											<px:PXNumberEdit ID="edAdjNbr" runat="server" DataField="AdjNbr" />
											<px:PXNumberEdit ID="edCuryWhTaxBal" runat="server" DataField="CuryWhTaxBal" Enabled="False" />
										</RowTemplate>
										<Columns>
											<px:PXGridColumn DataField="AdjdDocType" Width="100px" Type="DropDownList" AutoCallBack="True" />
											<px:PXGridColumn AutoCallBack="True" DataField="AdjdRefNbr" Width="100px"/>
											<px:PXGridColumn AutoCallBack="True" DataField="CuryAdjgAmt" TextAlign="Right" Width="100px" />
											<px:PXGridColumn AutoCallBack="True" DataField="CuryAdjgDiscAmt" TextAlign="Right" Width="100px" />
											<px:PXGridColumn AutoCallBack="True" DataField="CuryAdjgWhTaxAmt" TextAlign="Right" Width="100px" />
											<px:PXGridColumn DataField="AdjdDocDate" Width="100px" />
											<px:PXGridColumn DataField="APInvoice__DueDate" Width="100px" />
											<px:PXGridColumn DataField="APInvoice__DiscDate" Width="100px" />
											<px:PXGridColumn AutoCallBack="True" DataField="AdjdCuryRate" TextAlign="Right" Width="100px" />
											<px:PXGridColumn DataField="CuryDocBal" TextAlign="Right" Width="100px" />
											<px:PXGridColumn DataField="CuryDiscBal" TextAlign="Right" Width="100px" />
											<px:PXGridColumn DataField="CuryWhTaxBal" TextAlign="Right" Width="100px" />
											<px:PXGridColumn DataField="APInvoice__DocDesc" Width="200px" />
											<px:PXGridColumn DataField="AdjdCuryID" Width="50px" />
											<px:PXGridColumn DataField="AdjdFinPeriodID" />
											<px:PXGridColumn DataField="APInvoice__InvoiceNbr" Width="90px" />
											<px:PXGridColumn DataField="VendorID" TextAlign="Right" Width="50px" />
											<px:PXGridColumn DataField="AdjgDocType" Width="50px" />
											<px:PXGridColumn DataField="AdjgRefNbr" Width="100px" />
											<px:PXGridColumn DataField="AdjNbr" TextAlign="Right" Width="54px" />
										</Columns>
                                    </px:PXGridLevel>
                                </Levels>
                                <AutoSize Enabled="True" />
                                <ActionBar DefaultAction="ViewDoc">
                                    <PagerSettings Mode="NextPrevFirstLast" />
                                </ActionBar>                                
                            </px:PXGrid>
                        </Template2>
                    </px:PXSplitContainer>
                </Template>
			</px:PXTabItem>
			<px:PXTabItem Text="AR Payment Applications" Visible="True">
                <Template>
                    <px:PXSplitContainer runat="server" ID="sp1" SplitterPosition="300" SkinID="Horizontal" Height="500px">
                        <AutoSize Enabled="True" />                        
                        <Template1>
                            <px:PXGrid ID="gridARPayments" runat="server" DataSourceID="ds" Style="z-index: 100; top: 0px; left: 0px; height: 180px;"
                                Width="100%" FilesField="NoteFiles" SkinID="DetailsInTab" Height="180px" SyncPosition="True">
                                <AutoCallBack Target="gridARAppDocuments" Command="Refresh" />
                                <Mode AllowAddNew="False" AllowDelete="False" AllowUpdate="False" />
                                <Levels>
                                    <px:PXGridLevel DataMember="ARPayments" DataKeyNames="Module,BatchNbr,LineNbr">
                                        <Columns>
                                            <px:PXGridColumn DataField="TranCode" />
											<px:PXGridColumn DataField="RefNbr" />
                                            <px:PXGridColumn DataField="TranDate" Width="90px" />
                                            <px:PXGridColumn DataField="BAccountID" />
                                            <px:PXGridColumn DataField="LocationID" />
                                            <px:PXGridColumn DataField="PaymentMethodID" />
                                            <px:PXGridColumn DataField="DebitAccountID" />
                                            <px:PXGridColumn DataField="DebitSubID" />
                                            <px:PXGridColumn DataField="CreditAccountID" />
                                            <px:PXGridColumn DataField="CreditSubID" />
                                            <px:PXGridColumn DataField="ExtRefNbr" />
                                            <px:PXGridColumn DataField="CuryID" />
											<px:PXGridColumn DataField="CuryApplAmt" />
                                            <px:PXGridColumn DataField="curyUnappliedBal" />
                                            <px:PXGridColumn AllowNull="False" DataField="CuryTranAmt" TextAlign="Right" Width="100px" />
                                            <px:PXGridColumn DataField="TranDesc" Width="200px" />                                            
                                        </Columns>
                                    </px:PXGridLevel>
                                </Levels>
                                <AutoSize Enabled="True" />                            	
								<actionbar>
									<actions>
										<addnew enabled="False" menuvisible="False" />
										<delete enabled="False" menuvisible="False" />
										<editrecord enabled="False" menuvisible="False" />
									</actions>
								</actionbar>																
                            </px:PXGrid>
                        </Template1>
                        <Template2>
                            <px:PXGrid ID="gridARAppDocuments" runat="server" Width="100%" Height="120px" 
								DataSourceID="ds" SkinID="DetailsInTab" AdjustPageSize="Auto"
                                Caption="Documents to Apply">                                
                                <CallbackCommands>
                                    <Refresh SelectControlsIDs="gridARPayments" />
                                </CallbackCommands>
                                <Levels>
                                    <px:PXGridLevel DataMember="ARAdjustments">
										<RowTemplate>
											<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />
											<px:PXDropDown ID="edAdjdDocTypeAR" runat="server" DataField="AdjdDocType" CommitChanges="True" />
											<px:PXSelector ID="edAdjdRefNbrAR" runat="server" AutoRefresh="True" CommitChanges="True" DataField="AdjdRefNbr" AllowEdit="True">
                                                <Parameters>
											        <px:PXControlParam ControlID="gridARAppDocuments" Name="aAdjdDocType" PropertyName="DataValues[&quot;AdjdDocType&quot;]" />
										        </Parameters>                                                
                                             </px:PXSelector>
											<px:PXNumberEdit ID="edCuryAdjgAmtAR" runat="server" CommitChanges="True" DataField="CuryAdjgAmt" />
											<px:PXNumberEdit ID="edCuryAdjgDiscAmtAR" runat="server" CommitChanges="True" DataField="CuryAdjgDiscAmt" />
											<px:PXNumberEdit ID="edCuryAdjgWOAmtAR" runat="server" CommitChanges="True" DataField="CuryAdjgWOAmt" />
											<px:PXDateTimeEdit ID="edAdjdDocDateAR" runat="server" DataField="AdjdDocDate" Enabled="False" />
											<px:PXDateTimeEdit ID="edARInvoice__DueDate" runat="server" DataField="ARInvoice__DueDate" />
											<px:PXDateTimeEdit ID="edARInvoice__DiscDate" runat="server" DataField="ARInvoice__DiscDate" />
											<px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="SM" StartColumn="True" />											
											<px:PXTextEdit ID="edAdjgDocTypeAR" runat="server" DataField="AdjgDocType" />
											<px:PXNumberEdit ID="edCuryDocBalAR" runat="server" DataField="CuryDocBal" Enabled="False" />
											<px:PXTextEdit ID="edAdjgRefNbrAR" runat="server" DataField="AdjgRefNbr" />
											<px:PXNumberEdit ID="edCuryDiscBalAR" runat="server" DataField="CuryDiscBal" Enabled="False" />
											<px:PXNumberEdit ID="edAdjNbrAR" runat="server" DataField="AdjNbr" />
											<px:PXNumberEdit ID="edCuryWhTaxBalAR" runat="server" DataField="CuryWhTaxBal" Enabled="False" />
										</RowTemplate>
										<Columns>
											<px:PXGridColumn DataField="AdjdDocType" Width="100px" Type="DropDownList" AutoCallBack="True" />
											<px:PXGridColumn AutoCallBack="True" DataField="AdjdRefNbr" Width="100px"/>
											<px:PXGridColumn AllowNull="False" DataField="CuryAdjgAmt" AutoCallBack="True" TextAlign="Right" Width="81px" />
											<px:PXGridColumn AllowNull="False" DataField="CuryAdjgDiscAmt" AutoCallBack="True" TextAlign="Right" Width="81px" />
											<px:PXGridColumn AllowNull="False" DataField="CuryAdjgWOAmt" AutoCallBack="True" TextAlign="Right" Width="81px" />
											<px:PXGridColumn AllowUpdate="False" DataField="AdjdDocDate" Width="90px" />											
											<px:PXGridColumn DataField="ARInvoice__DueDate" Width="100px" />
											<px:PXGridColumn DataField="ARInvoice__DiscDate" Width="100px" />											
											<px:PXGridColumn DataField="CuryDocBal" TextAlign="Right" Width="100px" />
											<px:PXGridColumn DataField="CuryDiscBal" TextAlign="Right" Width="100px" />
											<px:PXGridColumn DataField="CuryWOBal" TextAlign="Right" Width="100px" />
											<px:PXGridColumn DataField="ARInvoice__DocDesc" Width="200px" />
											<px:PXGridColumn DataField="AdjdCuryID" Width="50px" />
											<px:PXGridColumn DataField="AdjdFinPeriodID" />
											<px:PXGridColumn DataField="ARInvoice__InvoiceNbr" Width="90px" />
											<px:PXGridColumn DataField="CustomerID" TextAlign="Right" Width="50px" />
											<px:PXGridColumn DataField="AdjgDocType" Width="50px" />
											<px:PXGridColumn DataField="AdjgRefNbr" Width="100px" />
											<px:PXGridColumn DataField="AdjNbr" TextAlign="Right" Width="54px" />
										</Columns>
                                    </px:PXGridLevel>
                                </Levels>
                                <AutoSize Enabled="True" />
                                <ActionBar DefaultAction="ViewDoc">
                                    <PagerSettings Mode="NextPrevFirstLast" />
                                </ActionBar>                                
                            </px:PXGrid>
                        </Template2>
                    </px:PXSplitContainer>
                </Template>
			</px:PXTabItem>		
			<px:PXTabItem Text="GL Transactions">
				<Template>
				<px:PXGrid ID="gridGLTrans" runat="server" DataSourceID="ds" Style="z-index: 100;" Width="100%" AllowSearch="True"
					 AdjustPageSize="Auto"   AllowPaging="True" FilesField="NoteFiles" SkinID="Inquire">
					<AutoSize Enabled="True"/>
				<Levels>
				<px:PXGridLevel DataMember="GLTransactions" DataKeyNames="Module,BatchNbr,LineNbr">				                
						<Columns>
							<px:PXGridColumn DataField="RefNbr" AllowNull="False" Width="72px"/>
							<px:PXGridColumn DataField="Module" RenderEditorText="True" Width="48px" />
							<px:PXGridColumn DataField="BatchNbr" Width="72px" />
							<px:PXGridColumn DataField="TranDate" Width="90px" />							
							<px:PXGridColumn DataField="AccountID" Width="72px"/>
							<px:PXGridColumn DataField="SubID" Width="100px"/>														
							<px:PXGridColumn DataField="InventoryID" />
							<px:PXGridColumn DataField="Qty" AllowNull="False" TextAlign="Right" Width="100px" />
							<px:PXGridColumn DataField="CuryID" />
							<px:PXGridColumn DataField="CuryDebitAmt" AllowNull="False" TextAlign="Right" Width="100px" />
							<px:PXGridColumn DataField="CuryCreditAmt" AllowNull="False" TextAlign="Right" Width="100px" />
							<px:PXGridColumn DataField="TranDesc" Width="200px" />							
						</Columns>
						<Layout FormViewHeight="" />
				</px:PXGridLevel>
				</Levels>				
				</px:PXGrid>
				</Template>
			</px:PXTabItem>
		</Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="180" />
	</px:PXTab>
	
	<px:PXSmartPanel ID="PanelLS" runat="server" Style="z-index: 108;" Width="764px" Height="240px"
		Caption="Document Taxes" CaptionVisible="True" Key="CurrentDocTaxes"
		AutoCallBack-Command="Refresh" AutoCallBack-Enabled="True" AutoCallBack-Target="gridDetailTaxes1">
		<px:PXGrid ID="gridDetailTaxes1" runat="server" Width="100%" 
			DataSourceID="ds" SkinID="Details" AdjustPageSize="Auto">
			<Mode InitNewRow="True" />
			<Parameters>
				<px:PXSyncGridParam ControlID="grid" />
			</Parameters>
			<Levels>
				<px:PXGridLevel DataMember="CurrentDocTaxes" DataKeyNames="Module,BatchNbr,LineNbr,TaxID,DetailType">
					<Columns>
						<px:PXGridColumn DataField="TaxID" />
						<px:PXGridColumn AllowNull="False" DataField="CuryTaxableAmt" TextAlign="Right" Width="100px" AutoCallBack="true" />
						<px:PXGridColumn AllowNull="False" DataField="TaxRate" TextAlign="Right" Width="100px" />
						<px:PXGridColumn AllowNull="False" DataField="CuryTaxAmt" TextAlign="Right" Width="100px" AutoCallBack="true" />                        
                        <px:PXGridColumn DataField="NonDeductibleTaxRate" TextAlign="Right" Width="100px" />
                        <px:PXGridColumn DataField="CuryExpenseAmt" TextAlign="Right" Width="100px" />
					</Columns>
				</px:PXGridLevel>
			</Levels>
			<ActionBar DefaultAction="ViewDoc">
				<Actions>
					<AddNew Enabled="False" MenuVisible="False" />
					<Delete Enabled="False" MenuVisible="False" />
				</Actions>			
			</ActionBar>
			<AutoSize Enabled="true" />
		</px:PXGrid>
		<px:PXPanel ID="PXPanel1" runat="server" SkinID="Buttons">
			<px:PXButton ID="btnSave" runat="server" DialogResult="OK" Text="OK" />
		</px:PXPanel>
	</px:PXSmartPanel>
</asp:Content>
