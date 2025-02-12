<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FA507000.aspx.cs"
    Inherits="Page_FA507000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter" TypeName="PX.Objects.FA.TransferProcess">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="ViewAsset" DependOnGrid="grid" Visible="False" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Filter" DefaultControlID="edPeriodID"
        NoteField="" Caption="Options" TabIndex="3100">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
            <px:PXDateTimeEdit CommitChanges="True" ID="edTransferDate" runat="server" DataField="TransferDate" />
            <px:PXMaskEdit CommitChanges="True" ID="edPeriodID" runat="server" DataField="PeriodID" />
            <px:PXTextEdit ID="edReason" runat="server" DataField="Reason" />
            <px:PXPanel ID="PXPanel1" runat="server" RenderSimple="True" RenderStyle="Simple">
                <px:PXLayoutRule runat="server" GroupCaption="Asset Transfer From" StartGroup="True" LabelsWidth="S" ControlSize="XM" />
                <px:PXSegmentMask ID="edBranchFrom" runat="server" CommitChanges="True" DataField="BranchFrom" DataMember="_Branch_" DataSourceID="ds" />
                <px:PXSelector ID="edDepartmentFrom" runat="server" CommitChanges="True" DataField="DepartmentFrom" DataMember="_EPDepartment_"
                    DataSourceID="ds" />
                <px:PXSelector ID="edClassFrom" runat="server" CommitChanges="True" DataField="ClassFrom" DataSourceID="ds" />
                <px:PXLayoutRule runat="server" GroupCaption="To" StartColumn="True" StartGroup="True" LabelsWidth="S" ControlSize="XM" />
                <px:PXSegmentMask ID="edBranchTo" runat="server" CommitChanges="True" DataField="BranchTo" DataMember="_Branch_" DataSourceID="ds" />
                <px:PXSelector ID="edDepartmentTo" runat="server" CommitChanges="True" DataField="DepartmentTo" DataMember="_EPDepartment_"
                    DataSourceID="ds" />
                <px:PXSelector ID="edClassTo" runat="server" CommitChanges="True" DataField="ClassTo" DataSourceID="ds" />
            </px:PXPanel>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Height="150px" SkinID="Inquire" AdjustPageSize="Auto"
        AllowPaging="True" Caption="Fixed Assets" AllowSearch="True" FastFilterFields="AssetCD">
        <Levels>
            <px:PXGridLevel DataKeyNames="AssetCD" DataMember="Assets">
                <Columns>
                    <px:PXGridColumn AllowCheckAll="True" DataField="Selected" Label="Selected" TextAlign="Center" Type="CheckBox" Width="60px" />
                    <px:PXGridColumn DataField="BranchID" Label="Branch" />
                    <px:PXGridColumn DataField="ClassID" Label="Asset Class" />
                    <px:PXGridColumn DataField="AssetCD" Label="Asset ID" LinkCommand="ViewAsset" />
                    <px:PXGridColumn DataField="Description" Label="Description" Width="200px" />
                    <px:PXGridColumn DataField="ParentAssetID" Label="Parent Asset" />
                    <px:PXGridColumn AllowNull="False" DataField="FADetails__CurrentCost" Label="Basis" TextAlign="Right" Width="100px" />
                    <px:PXGridColumn DataField="FADetails__ReceiptDate" Label="Receipt Date" Width="90px" />
                    <px:PXGridColumn DataField="UsefulLife" Label="Useful Life, Years" TextAlign="Right" Width="100px" />
                    <px:PXGridColumn DataField="FADetails__TransferPeriod" />
                    <px:PXGridColumn DataField="FAAccountID" Label="Fixed Assets Account" />
                    <px:PXGridColumn DataField="FASubID" Label="Fixed Assets Sub." />
                    <px:PXGridColumn DataField="FADetails__TagNbr" Label="Tag Number" Width="80px" />
                    <px:PXGridColumn DataField="Account__AccountClassID" Label="Account Class" Width="80px" />
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
        <ActionBar PagerVisible="False"/>
    </px:PXGrid>
</asp:Content>
