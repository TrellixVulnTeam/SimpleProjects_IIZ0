<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FA501000.aspx.cs"
    Inherits="Page_FA501000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Years" TypeName="PX.Objects.FA.GenerationPeriods">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Process" CommitChanges="true" StartNewGroup="True" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Years" Caption="Parameters">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
            <px:PXTextEdit CommitChanges="True" runat="server" DataField="YearTo" ID="edYearTo" />
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Height="150px" SkinID="Inquire" Caption="Books"
        AdjustPageSize="Auto" AllowPaging="True">
        <Levels>
            <px:PXGridLevel DataMember="Books">
                <Columns>
                    <px:PXGridColumn AllowNull="False" DataField="Selected" Label="Selected" TextAlign="Center" Type="CheckBox" Width="60px"
                        AllowCheckAll="True" />
                    <px:PXGridColumn AllowUpdate="False" DataField="BookCode" DisplayFormat="&gt;CCCCCCCCCC" Label="Book Code" />
                    <px:PXGridColumn AllowUpdate="False" DataField="Description" Label="Description" Width="200px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="FirstCalendarYear" Label="Last Calendar Year" TextAlign="Right" Width="81px" />
                    <px:PXGridColumn AllowUpdate="False" DataField="LastCalendarYear" Label="Last Calendar Year" TextAlign="Right" Width="81px" />
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXGrid>
</asp:Content>
