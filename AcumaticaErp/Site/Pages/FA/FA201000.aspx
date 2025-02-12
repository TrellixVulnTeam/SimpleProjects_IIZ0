<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FA201000.aspx.cs"
    Inherits="Page_FA201000" Title="Asset Class" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.FA.AssetClassMaint" PrimaryView="AssetClass">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Insert" PostData="Self" />
            <px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
            <px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
            <px:PXDSCallbackCommand Name="Last" PostData="Self" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="AssetClass" Caption="Asset Class Summary"
        NoteIndicator="True" FilesIndicator="True" ActivityIndicator="True" ActivityField="NoteActivity" TabIndex="29100">
        <Template>
            <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="S" StartColumn="True" />
            <px:PXSelector Size="S" ID="edAssetCD" runat="server" DataField="AssetCD" AutoRefresh="True" DataSourceID="ds" />
            <px:PXSelector CommitChanges="True" ID="edParentAssetID" runat="server" DataField="ParentAssetID" AutoRefresh="True" 
                DataSourceID="ds" />
            <px:PXTextEdit ID="edDescription" runat="server" DataField="Description" />
            <px:PXLayoutRule runat="server" StartColumn="True" />
            <px:PXCheckBox ID="chkActive" runat="server" Checked="True" DataField="Active" AlignLeft="True" />
            <px:PXCheckBox ID="chkHoldEntry" runat="server" DataField="HoldEntry" AlignLeft="True" />
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Height="579px" Style="z-index: 100" Width="100%" DataMember="CurrentAssetClass" DataSourceID="ds">
        <Items>
            <px:PXTabItem Text="General Settings">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
                    <px:PXCheckBox CommitChanges="True" ID="chkIsTangible" runat="server" Checked="True" DataField="IsTangible" />
                    <px:PXDropDown ID="edAssetType" runat="server" DataField="AssetType" Size="S" />
                    <px:PXNumberEdit CommitChanges="True" ID="edUsefulLife" runat="server" DataField="UsefulLife" />
                    <px:PXSelector ID="edUsageSchedule" runat="server" DataField="UsageScheduleID" AllowEdit="True" AutoRefresh="True" />
                    <px:PXSelector ID="edServiceSchedule" runat="server" DataField="ServiceScheduleID" AllowEdit="True" AutoRefresh="True" />
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Depreciation Settings">
                <Template>
                    <px:PXGrid SkinID="Details" ID="gridDepriciationSettings" BorderWidth="0px" runat="server" DataSourceID="ds" Style="z-index: 100;
                        left: 0px; top: 0px; height: 409px;" Width="100%" AllowPaging="True" AdjustPageSize="Auto" ActionsPosition="Top" 
                        AllowSearch="True" Height="409px">
                        <Levels>
                            <px:PXGridLevel DataMember="DepreciationSettings" DataKeyNames="BookID,AssetID">
                                <Mode AllowAddNew="True" AllowFormEdit="True" />
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                                    <px:PXSelector ID="edBookID" runat="server" DataField="BookID" AutoRefresh="True" AllowEdit="True" />
                                    <px:PXCheckBox SuppressLabel="True" ID="chkDepreciate" runat="server" DataField="Depreciate" />
                                    <px:PXDropDown CommitChanges="True" ID="edAveragingConvention" runat="server" DataField="AveragingConvention" />
                                    <px:PXSelector CommitChanges="True" ID="edDepreciationMethodID" runat="server" AllowEdit="True" AutoRefresh="True" 
                                        DataField="DepreciationMethodID" />
                                    <px:PXCheckBox SuppressLabel="True" ID="chkBonus" runat="server" DataField="Bonus" />
                                    <px:PXCheckBox SuppressLabel="True" ID="chkSect179" runat="server" DataField="Sect179" />
                                    <px:PXNumberEdit CommitChanges="True" ID="edUsefulLife" runat="server" DataField="UsefulLife" AllowNull="True" />
                                    <px:PXNumberEdit ID="edRecoveryPeriod" runat="server" DataField="RecoveryPeriod" />
                                    <px:PXDropDown ID="edMidMonthType" runat="server" DataField="MidMonthType" />
                                    <px:PXNumberEdit ID="edMidMonthDay" runat="server" DataField="MidMonthDay" />
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn DataField="BookID" Width="80px" AutoCallBack="True" />
                                    <px:PXGridColumn DataField="UsefulLife" Label="Useful Life, years" TextAlign="Right" Width="81px" AutoCallBack="True" />
                                    <px:PXGridColumn DataField="UpdateGL" TextAlign="Center" AllowNull="False" Type="CheckBox" AutoCallBack="False"/>
                                    <px:PXGridColumn DataField="Depreciate" TextAlign="Center" AllowNull="False" Type="CheckBox" AutoCallBack="True" />
                                    <px:PXGridColumn AutoCallBack="True" DataField="DepreciationMethodID" Label="Depreciation Method" Width="108px" />
                                    <px:PXGridColumn DataField="AveragingConvention" Label="Averaging Convention" RenderEditorText="True" Width="90px" AutoCallBack="True" />
                                    <px:PXGridColumn DataField="Bonus" AllowNull="False" TextAlign="Center" Type="CheckBox" />
                                    <px:PXGridColumn DataField="Sect179" AllowNull="False" TextAlign="Center" Type="CheckBox" />
                                    <px:PXGridColumn DataField="RecoveryPeriod" Label="Useful Life, book periods" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn DataField="MidMonthType" Label="Mid-Month Type" RenderEditorText="True" />
                                    <px:PXGridColumn DataField="MidMonthDay" Label="Mid-Month Day" TextAlign="Right" />
                                </Columns>
                                <Layout FormViewHeight="" />
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="GL Accounts">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="XM" ControlSize="XM" />
                    <px:PXSegmentMask ID="edConstructionAccountID" runat="server" DataField="ConstructionAccountID" />
                    <px:PXSegmentMask ID="edConstructionSubID" runat="server" DataField="ConstructionSubID">
                        <Parameters>
                            <px:PXControlParam ControlID="tab" Name="FixedAsset.constructionAccountID" PropertyName="DataControls[&quot;edConstructionAccountID&quot;].Value" />
                        </Parameters>
                    </px:PXSegmentMask>
                    <px:PXSegmentMask ID="edFAAccountID" runat="server" DataField="FAAccountID" />
                    <px:PXSegmentMask ID="edFASubID" runat="server" DataField="FASubID">
                        <Parameters>
                            <px:PXControlParam ControlID="tab" Name="FixedAsset.fAAccountID" PropertyName="DataControls[&quot;edFAAccountID&quot;].Value" />
                        </Parameters>
                    </px:PXSegmentMask>
                    <px:PXSegmentMask CommitChanges="True" ID="edFASubMask" runat="server" DataField="FASubMask" />
                    <px:PXSegmentMask ID="edAccumulatedDepreciationAccountID" runat="server" DataField="AccumulatedDepreciationAccountID" />
                    <px:PXSegmentMask ID="edAccumulatedDepreciationSubID" runat="server" DataField="AccumulatedDepreciationSubID">
                        <Parameters>
                            <px:PXControlParam ControlID="tab" Name="FixedAsset.accumulatedDepreciationAccountID" PropertyName="DataControls[&quot;edAccumulatedDepreciationAccountID&quot;].Value" />
                        </Parameters>
                    </px:PXSegmentMask>
                    <px:PXCheckBox CommitChanges="True" ID="chkUseFASubMask" runat="server" Checked="True" DataField="UseFASubMask" />
                    <px:PXSegmentMask ID="edAccumDeprSubMask" runat="server" DataField="AccumDeprSubMask" />
                    <px:PXSegmentMask ID="edDepreciatedExpenseAccountID" runat="server" DataField="DepreciatedExpenseAccountID" />
                    <px:PXSegmentMask ID="edDepreciatedExpenseSubID" runat="server" DataField="DepreciatedExpenseSubID">
                        <Parameters>
                            <px:PXControlParam ControlID="tab" Name="FixedAsset.depreciatedExpenseAccountID" PropertyName="DataControls[&quot;edDepreciatedExpenseAccountID&quot;].Value" />
                        </Parameters>
                    </px:PXSegmentMask>
                    <px:PXSegmentMask ID="edDeprExpenceSubMask" runat="server" DataField="DeprExpenceSubMask" />
                    <px:PXSegmentMask ID="edDisposalAccountID" runat="server" DataField="DisposalAccountID" />
                    <px:PXSegmentMask ID="edDisposalSubID" runat="server" DataField="DisposalSubID">
                        <Parameters>
                            <px:PXControlParam ControlID="tab" Name="FixedAsset.disposalAccountID" PropertyName="DataControls[&quot;edDisposalAccountID&quot;].Value" />
                        </Parameters>
                    </px:PXSegmentMask>
                    <px:PXSegmentMask ID="edProceedsSubMask" runat="server" DataField="ProceedsSubMask" />
                    <px:PXSegmentMask ID="edGainAccountID" runat="server" DataField="GainAcctID" />
                    <px:PXSegmentMask ID="edGainSubID" runat="server" DataField="GainSubID">
                        <Parameters>
                            <px:PXControlParam ControlID="tab" Name="FixedAsset.gainAcctID" PropertyName="DataControls[&quot;edGainAccountID&quot;].Value" />
                        </Parameters>
                    </px:PXSegmentMask>
                    <px:PXSegmentMask ID="edLossAccountID" runat="server" DataField="LossAcctID" />
                    <px:PXSegmentMask ID="edLossSubID" runat="server" DataField="LossSubID">
                        <Parameters>
                            <px:PXControlParam ControlID="tab" Name="FixedAsset.gainAcctID" PropertyName="DataControls[&quot;edLossAccountID&quot;].Value" />
                        </Parameters>
                    </px:PXSegmentMask>
                    <px:PXSegmentMask ID="edGainLossSubMask" runat="server" DataField="GainLossSubMask" />
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                    <px:PXSegmentMask ID="edRentAccountID" runat="server" DataField="RentAccountID" />
                    <px:PXSegmentMask ID="edRentSubID" runat="server" DataField="RentSubID">
                        <Parameters>
                            <px:PXControlParam ControlID="tab" Name="FixedAsset.rentAccountID" PropertyName="DataControls[&quot;edRentAccountID&quot;].Value" />
                        </Parameters>
                    </px:PXSegmentMask>
                    <px:PXSegmentMask ID="edLeaseAccountID" runat="server" DataField="LeaseAccountID" />
                    <px:PXSegmentMask ID="edLeaseSubID" runat="server" DataField="LeaseSubID">
                        <Parameters>
                            <px:PXControlParam ControlID="tab" Name="FixedAsset.leaseAccountID" PropertyName="DataControls[&quot;edLeaseAccountID&quot;].Value" />
                        </Parameters>
                    </px:PXSegmentMask>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="250" MinWidth="300" />
    </px:PXTab>
</asp:Content>
