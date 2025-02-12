<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="EP505020.aspx.cs"
    Inherits="Page_EP505020" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.EP.EquipmentTimeCardRelease" PrimaryView="FilteredItems">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Cancel" />
            <px:PXDSCallbackCommand Name="Process" CommitChanges="true" StartNewGroup="True" />
            <px:PXDSCallbackCommand Name="ProcessAll" CommitChanges="true" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="150px" Style="z-index: 100" Width="100%" ActionsPosition="Top"
        Caption="Time Cards" SkinID="Inquire">
        <Levels>
            <px:PXGridLevel DataMember="FilteredItems">
                <RowTemplate>
                    <px:PXTimeSpan runat="server" DataField="TimeSetupCalc" ID="RegularTime" Enabled="False" Size="XS" LabelWidth="55" InputMask="hh:mm" MaxHours="99" />
                    <px:PXTimeSpan runat="server" DataField="TimeRunCalc" ID="PXTimeSpan1" Enabled="False" Size="XS" LabelWidth="55" InputMask="hh:mm" MaxHours="99" />
                    <px:PXTimeSpan runat="server" DataField="TimeSuspendCalc" ID="PXTimeSpan2" Enabled="False" Size="XS" LabelWidth="55" InputMask="hh:mm" MaxHours="99" />
                    <px:PXTimeSpan runat="server" DataField="TimeTotalCalc" ID="PXTimeSpan3" Enabled="False" Size="XS" LabelWidth="55" InputMask="hh:mm" MaxHours="99" />
                    <px:PXTimeSpan runat="server" DataField="TimeBillableSetupCalc" ID="PXTimeSpan4" Enabled="False" Size="XS" LabelWidth="55" InputMask="hh:mm" MaxHours="99" />
                    <px:PXTimeSpan runat="server" DataField="TimeBillableRunCalc" ID="PXTimeSpan5" Enabled="False" Size="XS" LabelWidth="55" InputMask="hh:mm" MaxHours="99" />
                    <px:PXTimeSpan runat="server" DataField="TimeBillableSuspendCalc" ID="PXTimeSpan6" Enabled="False" Size="XS" LabelWidth="55" InputMask="hh:mm" MaxHours="99" />
                    <px:PXTimeSpan runat="server" DataField="TimeBillableTotalCalc" ID="PXTimeSpan7" Enabled="False" Size="XS" LabelWidth="55" InputMask="hh:mm" MaxHours="99" />
                </RowTemplate>
                <Columns>
                    <px:PXGridColumn AllowCheckAll="True" AllowNull="False" DataField="Selected" TextAlign="Center" Type="CheckBox" Width="20px" />
                    <px:PXGridColumn DataField="TimeCardCD" Width="108px" />
                    <px:PXGridColumn DataField="EquipmentID" Label="Employee ID" Width="108px" />
                    <px:PXGridColumn DataField="WeekID" Label="Week" Width="81px" DisplayMode="Text" />
                    
                    <px:PXGridColumn DataField="TimeSetupCalc" Label="TimeSetupCalc" Width="81px" RenderEditorText="True" />
                     <px:PXGridColumn DataField="TimeRunCalc" Label="TimeRunCalc" Width="81px" RenderEditorText="True" />
                     <px:PXGridColumn DataField="TimeSuspendCalc" Label="TimeSuspendCalc" Width="81px" RenderEditorText="True" />
                     <px:PXGridColumn DataField="TimeTotalCalc" Label="TimeTotalCalc" Width="81px" RenderEditorText="True" />
                     <px:PXGridColumn DataField="TimeBillableSetupCalc" Label="TimeBillableSetupCalc" Width="81px" RenderEditorText="True" />
                     <px:PXGridColumn DataField="TimeBillableRunCalc" Label="TimeBillableRunCalc" Width="81px" RenderEditorText="True" />
                     <px:PXGridColumn DataField="TimeBillableSuspendCalc" Label="TimeBillableSuspendCalc" Width="81px" RenderEditorText="True" />
                     <px:PXGridColumn DataField="TimeBillableTotalCalc" Label="TimeBillableTotalCalc" Width="81px" RenderEditorText="True" />
                   
                    <px:PXGridColumn DataField="EPApproval__ApprovedByID" Width="108px" />
                    <px:PXGridColumn DataField="EPApproval__ApproveDate" Width="90px" />
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXGrid>
</asp:Content>
