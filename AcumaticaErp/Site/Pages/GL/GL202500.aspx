<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true"
	ValidateRequest="false" CodeFile="GL202500.aspx.cs" Inherits="Page_GL202500"
	Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" Width="100%" runat="server" PrimaryView="AccountRecords" TypeName="PX.Objects.GL.AccountMaint"  Visible="True">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Save" CommitChanges="True" />
			<px:PXDSCallbackCommand DependOnGrid="grid" Name="ViewRestrictionGroups" />
		    <px:PXDSCallbackCommand Name="AccountByPeriodEnq" DependOnGrid="grid"/>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="350px" Width="100%" AdjustPageSize="Auto"
		SkinID="Primary" AllowPaging="True" AllowSearch="True" FastFilterFields="AccountCD,Description" SyncPosition="True">
		<Levels>
			<px:PXGridLevel DataMember="AccountRecords">
				<RowTemplate>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
					<px:PXSegmentMask ID="edAccountCD" runat="server" DataField="AccountCD" />
					<px:PXCheckBox ID="chkActive" runat="server" DataField="Active" />
					<px:PXSelector ID="edAccountClassID" runat="server" DataField="AccountClassID" AllowEdit="True" AutoCallback="True"/>
				    <px:PXDropDown ID="Type" runat="server" DataField="Type"/>
					<px:PXNumberEdit ID="edCOAOrder" runat="server" DataField="COAOrder" />
					<px:PXDropDown ID="PostOption" runat="server" DataField="PostOption">
					</px:PXDropDown>
					<px:PXCheckBox ID="chkRequireUnits" runat="server" DataField="RequireUnits" />
					<px:PXCheckBox ID="chkIsCashAccount" runat="server" DataField="IsCashAccount" />
					<px:PXCheckBox ID="chkSecured" runat="server" DataField="Secured" Enabled="False" />
					<px:PXLayoutRule runat="server" ColumnSpan="2" />
					<px:PXTextEdit ID="edDescription" runat="server" DataField="Description" />
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
					<px:PXSelector ID="edCuryID" runat="server" DataField="CuryID" AllowEdit="True" CommitChanges="true" />
					<px:PXSelector ID="edRevalCuryRateTypeId" runat="server" DataField="RevalCuryRateTypeId"
						AllowEdit="True" CommitChanges="true" />
					<px:PXSelector ID="edGLConsolAccountCD" runat="server" DataField="GLConsolAccountCD" />
					<px:PXSegmentMask ID="edAccountGroupID" runat="server" DataField="AccountGroupID"
						AutoRefresh="True" AllowEdit="True" />
					<px:PXNumberEdit ID="edAccountID" runat="server" DataField="AccountID" />
                    <px:PXSelector ID="edTaxCategory" runat="server" DataField="TaxCategoryID" />
				</RowTemplate>
				<Columns>
					<px:PXGridColumn DataField="AccountID" TextAlign="Right" Visible="False" />
					<px:PXGridColumn DataField="AccountCD" Width="100px" />
					<px:PXGridColumn DataField="AccountClassID" Width="144px" AutoCallBack="True"/>
					<px:PXGridColumn DataField="Type" Width="107px" Type="DropDownList" />
					<px:PXGridColumn DataField="Active" Width="61px" TextAlign="Center" Type="CheckBox" />
					<px:PXGridColumn DataField="Description" Width="324px" />
					<px:PXGridColumn DataField="RequireUnits" TextAlign="Center" Type="CheckBox" Width="61px" />
					<px:PXGridColumn DataField="NoSubDetail" Type="CheckBox" Width="61px" TextAlign="Center" />
					<px:PXGridColumn DataField="PostOption" Width="107px" Type="DropDownList" />
					<px:PXGridColumn DataField="COAOrder" Width="54px" TextAlign="Right" />
					<px:PXGridColumn DataField="GLConsolAccountCD" Width="100px" />
					<px:PXGridColumn DataField="CuryID" AutoCallBack="True" Width="106px" />
					<px:PXGridColumn DataField="IsCashAccount" TextAlign="Center" Type="CheckBox" Width="60px" />
					<px:PXGridColumn DataField="Secured" TextAlign="Center" Type="CheckBox" Width="50px" />
					<px:PXGridColumn DataField="RevalCuryRateTypeId" />
					<px:PXGridColumn DataField="AccountGroupID" Width="81px" />
                    <px:PXGridColumn DataField="TaxCategoryID" />
				</Columns>
				<Styles>
					<RowForm Height="250px">
					</RowForm>
				</Styles>
			</px:PXGridLevel>
		</Levels>
		<Layout FormViewHeight="250px" />
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
		<%--<ActionBar>
			<Actions>
				<NoteShow Enabled="False" />
			</Actions>
			<CustomItems>
				<px:PXToolBarButton Text="Cash Account" Tooltip="Cash Account Settings">
				    <AutoCallBack Command="CashAccountJump" Target="ds" />
					<PopupCommand Command="Refresh" Target="grid" />
				</px:PXToolBarButton>
				<px:PXToolBarButton Text="View Restriction Groups" CommandSourceID="ds" CommandName="ViewRestrictionGroups">
				</px:PXToolBarButton>
			    <px:PXToolBarButton Text="Account By Period" CommandSourceID="ds" CommandName="AccountByPeriodEnq"/>
			</CustomItems>
		</ActionBar>--%>
		<Mode AllowFormEdit="True" AllowUpload="True" />
		<CallbackCommands>
			<Save PostData="Content" />
		</CallbackCommands>
	</px:PXGrid>
	<px:PXSmartPanel ID="smpCashAccountED" runat="server" Key="CashAccountEditor" InnerPageUrl="~/Pages/CA/CA202000.aspx?PopupPanel=On"
		CaptionVisible="true" Caption="Cash Account" RenderIFrame="True">
	</px:PXSmartPanel>
</asp:Content>
