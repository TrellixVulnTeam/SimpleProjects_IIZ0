﻿<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/FormDetail.master" CodeFile="SM205080.aspx.cs" Inherits="Pages_SM_SM205080" ValidateRequest="False" %>


<asp:Content ID="Content1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource runat="server"
	ID="ds"
	Visible="True"
	TypeName="PX.SM.SessionListMaint"
	PrimaryView="Filter"
	Width="100%">
<%--		<CallbackCommands>
		    <px:PXDSCallbackCommand Name="actionFlushSamples" RepaintControls="Bound"/>
		    <px:PXDSCallbackCommand Name="actionStopMonitor" RepaintControls="Bound"/>
		    <px:PXDSCallbackCommand Name="actionClearSamples" RepaintControls="Bound"/>
		    <px:PXDSCallbackCommand Name="actionViewScreen" DependOnGrid="grid" Visible="False"/>
			
		</CallbackCommands>--%>
	</px:PXDataSource>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView runat="server" 
	ID="form"
	Width="100%"
	
	DataMember="Filter" CaptionVisible="False">
		<Template>
		    


	
		</Template>
		
	</px:PXFormView>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid runat="server" ID="grid" SkinID="Details" Width="100%" Height="400px"
         SyncPosition="True"
        AutoAdjustColumns="True" AllowPaging="True" AdjustPageSize="Auto">
		<Levels>
			<px:PXGridLevel DataMember="List">
				<Columns>
					<px:PXGridColumn DataField="User" />
					<px:PXGridColumn DataField="Created" DisplayFormat="dd MMM HH:mm" />
                    <px:PXGridColumn DataField="ID"  />
					
					<px:PXGridColumn DataField="Size" DisplayFormat="0,0" />
					
				</Columns>
				
			</px:PXGridLevel>
		</Levels>
<%--        <CallbackCommands>
            <Refresh RepaintControls="Bound" />
        </CallbackCommands>--%>
	    <AutoSize Enabled="True" Container="Window"/>
        <ActionBar PagerVisible="False" DefaultAction="buttonSql">
            <CustomItems>
                <px:PXToolBarButton Text="Details" PopupPanel="PanelDetails" Key="Details"/>
       

            </CustomItems>
        </ActionBar>
		

	</px:PXGrid>
    
    
    
     <px:PXSmartPanel runat="server" ID="PanelDetails" Width="100%" Height="650px"
        ShowMaximizeButton="True"
        CaptionVisible="True"
		Caption="Details"
        AutoSize-Enabled="True"
         AutoRepaint="True" Key="Details">
       

        <px:PXGrid runat="server" ID="GridDetails"
            Width="100%"
            SkinID="Details"
           PageSize="25"
            AutoGenerateColumns="Recreate"
           AllowPaging="True">
            <Mode AllowFormEdit="True"></Mode>
            <Levels>
                
			<px:PXGridLevel DataMember="Details" >
<%--				<Columns>



				</Columns>--%>
				
			</px:PXGridLevel>
		</Levels>
	    <AutoSize Enabled="True" Container="Parent"/>
        <ActionBar PagerVisible="False"/>    

        </px:PXGrid>
    </px:PXSmartPanel>
    


</asp:Content>

