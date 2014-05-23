<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CA401000.aspx.cs" Inherits="Page_CA401000"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
		TypeName="PX.Objects.CA.CashFlowEnq" PrimaryView="Filter" 
		PageLoadBehavior="PopulateSavedValues" >
        <CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="Cancel" StartNewGroup="true" />
			<px:PXDSCallbackCommand Name="viewReport" StartNewGroup="true" />			
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Width="100%" DataMember="Filter" Caption="Selection" TabIndex="2100">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="sm" ControlSize="100%" />
			<px:PXDateTimeEdit CommitChanges="True" ID="edStartDate" runat="server" Checked="True" DataField="StartDate" />
            <px:PXSelector CommitChanges="True" ID="edAccountID" runat="server" DataField="AccountID" Size="m" DataSourceID="ds" />			
			<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkIncludeUnassignedDocs" runat="server" DataField="IncludeUnassignedDocs" />
			<px:PXSelector CommitChanges="True" ID="edDefaultAccountID" runat="server" DataField="DefaultAccountID" Size="m" DataSourceID="ds" />
			<px:PXSelector CommitChanges="True" ID="edCuryID" runat="server" DataField="CuryID" Size="m" DataSourceID="ds" />
			<px:PXSelector CommitChanges="True" ID="edCuryRateTypeID" runat="server" DataField="CuryRateTypeID" Size="m" DataSourceID="ds" />
            <px:PXLayoutRule runat="server" StartColumn="True"/>            
			<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkincludeUnreleased" runat="server" DataField="IncludeUnreleased" />			
			<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkincludeUnapplied" runat="server" DataField="IncludeUnapplied"/>
			<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkincludeScheduled" runat="server" DataField="IncludeScheduled"/>
			<px:PXCheckBox CommitChanges="True" SuppressLabel="True" ID="chkSummaryOnly" runat="server" DataField="SummaryOnly"/>
			
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="150px" 
		Width="100%" ActionsPosition="Top" Caption="Forecast Details" 
        SkinID="Inquire"  RepaintColumns="True" TabIndex="300" RestrictFields="True">
        <Levels>
            <px:PXGridLevel DataMember="CashFlow">
                <RowTemplate></RowTemplate>
                <Columns>
					<px:PXGridColumn DataField="RecordType" Label="RecordType" Width="98px" TextAlign="Left" />
					<px:PXGridColumn DataField="CashAccountID" Label="Cash Account" Width="90px" />
					<px:PXGridColumn DataField="CashAccountID_CashAccount_Descr" Width="160px" />
                    <px:PXGridColumn DataField="BAccountID" Label="BAccount ID" Width="90px" />
					<px:PXGridColumn DataField="BAccountID_BAccountR_AcctName" Label="BAccount ID" Width="120px" />
					<px:PXGridColumn DataField="EntryID" Label="Entry ID" Width="90px" TextField="TranDesc" DisplayMode="Text"  />
					<%--<px:PXGridColumn DataField="StartingDate" Label="Starting Date" Width="90px" />--%>
                    <px:PXGridColumn DataField="CuryAmountDay0" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay1" Label="Beginning Amt" Width="90px" TextAlign="Right"/>
					<px:PXGridColumn DataField="CuryAmountDay2" Label="Beginning Amt" Width="90px" TextAlign="Right"/>
					<px:PXGridColumn DataField="CuryAmountDay3" Label="Beginning Amt" Width="90px" TextAlign="Right"/>
					<px:PXGridColumn DataField="CuryAmountDay4" Label="Beginning Amt" Width="90px" TextAlign="Right"/>
					<px:PXGridColumn DataField="CuryAmountDay5" Label="Beginning Amt" Width="90px" TextAlign="Right"/>
					<px:PXGridColumn DataField="CuryAmountDay6" Label="Beginning Amt" Width="90px" TextAlign="Right"/>
					<px:PXGridColumn DataField="CuryAmountDay7" Label="Beginning Amt" Width="90px" TextAlign="Right"/>
					<px:PXGridColumn DataField="CuryAmountDay8" Label="Beginning Amt" Width="90px" TextAlign="Right"/>
					<px:PXGridColumn DataField="CuryAmountDay9" Label="Beginning Amt" Width="90px" TextAlign="Right"/>
					<px:PXGridColumn DataField="CuryAmountDay10" Label="Beginning Amt" Width="90px" TextAlign="Right"/>
					<px:PXGridColumn DataField="CuryAmountDay11" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay12" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay13" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay14" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay15" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay16" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay17" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay18" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay19" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay20" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay21" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay22" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay23" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay24" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay25" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay26" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay27" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay28" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountDay29" Label="Beginning Amt" Width="90px" TextAlign="Right" />
					<px:PXGridColumn DataField="CuryAmountSummary" Label="Summary" Width="90px" TextAlign="Right" />
                </Columns>

<Layout FormViewHeight=""></Layout>
            </px:PXGridLevel>
        </Levels>        
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />

<AutoSize Enabled="True" Container="Window" MinHeight="150"></AutoSize>
    </px:PXGrid>
</asp:Content>
