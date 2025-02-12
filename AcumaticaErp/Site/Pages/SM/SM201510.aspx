<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FormDetail.master"
	AutoEventWireup="true" CodeFile="SM201510.aspx.cs" Inherits="Pages_SM_SM201510"
	EnableViewState="False" EnableViewStateMac="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="License"
		TypeName="PX.SM.LicensingSetup">
		<CallbackCommands>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXUploadDialog ID="dlgUploadFile" runat="server" Key="UploadDialogPanel" Height="60px" Style="position: static" Width="400px"
		Caption="Upload New License File" AutoSaveFile="false" SessionKey="UploadedLicenseKey" AllowedTypes=".als"
		RenderCheckIn="false" RenderLinkTextBox="false" RenderLink="false" RenderComment="false" />
	<px:PXSmartPanel ID="pnlNewLicense" runat="server" CaptionVisible="True" Caption="Activate New License"
		ForeColor="Black" Style="position: static" LoadOnDemand="true" Width="400px"
		Key="LicenseKeyPanel" AutoCallBack-Enabled="True" AutoCallBack-Target="frmInstall"
		AutoCallBack-Command="Refresh" AutoCallBack-ActiveBehavior="true" AutoCallBack-Behavior-RepaintControlsIDs="frmInstall">
		
		<px:PXFormView runat="server" ID="frmInstall" Width="100%" DataSourceID="ds" DataMember="LicenseKeyPanel" TemplateContainer="">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule2" runat="server" StartColumn="True" StartRow="True"/>
			<px:PXLabel ID="lblDisclamer" runat="server" Size="L" CssClass="LicenseDisclamer" />

			<px:PXLayoutRule ID="PXLayoutRule3" runat="server" StartColumn="True" StartRow="True" ControlSize="XM" LabelsWidth="XM" />
			<px:PXMaskEdit ID="edLicensing" runat="server" DataField="LicensingKey" />
            <%--<px:PXCheckBox ID="edValidateInsNumb" runat="server" DataField="DeactivateOldInstance" AlignLeft="true" />--%>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" />
		
			<px:PXPanel ID="PXPanel3" runat="server" SkinID="Buttons" ContentLayout-ContentAlign="Right" Width="100%" >
				<px:PXLayoutRule ID="PXLayoutRule5" runat="server" StartColumn="True" LabelsWidth="L" Merge="True" />
				<px:PXLabel runat="server" ID="Hole" Width="40px" />
				<px:PXLayoutRule ID="PXLayoutRule4" runat="server" StartColumn="True" LabelsWidth="L" Merge="True" />
				<px:PXButton ID="btnInstallOK" runat="server" DialogResult="OK" Text="OK" />
				<px:PXButton ID="btnInstallCancel" runat="server" DialogResult="Cancel" Text="Cancel" />
			</px:PXPanel>
		</Template>
		<AutoSize Enabled="True" Container="Window" />
	</px:PXFormView>
	</px:PXSmartPanel>
    <px:PXSmartPanel ID="pnlWarning" runat="server" Caption="Warning" CaptionVisible="true" Width="300px" Key="LicenseWarning"
        AutoCallBack-Enabled="true" AutoCallBack-Target="frmWarning" ForeColor="Black" Style="position: static" LoadOnDemand="true"
        AutoCallBack-Command="Refresh" AutoCallBack-ActiveBehavior="true" AutoCallBack-Behavior-RepaintControlsIDs="frmWarning">
        <px:PXFormView ID="frmWarning" runat="server" Width="100%" DataSourceID="ds" DataMember="LicenseWarning" TemplateContainer="">
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule6" runat="server" StartColumn="True" StartRow="True"/>
                <px:PXLabel ID="edWarningText" runat="server" Size="L" CssClass="LicenseDisclamer" />

                <px:PXPanel ID="PXPanel4" runat="server" SkinID="Buttons" ContentLayout-ContentAlign="Right" Width="100%">
                    <px:PXButton ID="btnDeactivateOK" runat="server" DialogResult="Yes" Text="Yes" />
                    <px:PXButton ID="btnDeactivateCancel" runat="server" DialogResult="No" Text="No" />
                </px:PXPanel>
            </Template>
            <AutoSize Enabled="True" Container="Window" />
        </px:PXFormView>
    </px:PXSmartPanel>
	<px:PXFormView runat="server" ID="form" DataSourceID="ds" DataMember="License" Width="100%">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" ControlSize="M" LabelsWidth="M" />
			<px:PXLayoutRule ID="PXLayoutRule7" runat="server" Merge="True" ControlSize="M" LabelsWidth="M" />
			<px:PXDropDown ID="edStatus" runat="server" DataField="Status" Enabled="False" />
			<px:PXCheckBox ID="chkPreview" runat="server" RenderStyle="Button" Enabled="False"
				CallbackUpdatable="True" AlignLeft="True" SuppressLabel="True" DataField="Preview" ToolTip="License preview mode.">
				<UncheckImages Normal="main@InquiryF" />
				<CheckImages Normal="main@InquiryF" />
			</px:PXCheckBox>
			<px:PXLayoutRule ID="PXLayoutRule8" runat="server"/>
			<px:PXTextEdit ID="edValidFrom" runat="server" DataField="ValidFrom" Enabled="False" />	
			<px:PXNumberEdit ID="edProcessors" runat="server" DataField="Processors" Enabled="False" />
			<px:PXNumberEdit ID="edUsers" runat="server" DataField="Users" Enabled="False" />			

			<px:PXLayoutRule ID="PXLayoutRule6" runat="server" StartColumn="True" ControlSize="M" LabelsWidth="M" />
			<px:PXCheckBox ID="chkIsValid" runat="server" RenderStyle="Button" Enabled="False"
				CallbackUpdatable="True" AlignLeft="True" SuppressLabel="True" DataField="Valid" ToolTip="The indicator is green when the license is valid.">
				<UncheckImages Normal="main@Fail" />
				<CheckImages Normal="main@Success" />
			</px:PXCheckBox>
			<px:PXTextEdit ID="edValidTo" runat="server" DataField="ValidTo" Enabled="False" />
			<px:PXTextEdit ID="edVersion" runat="server" DataField="Version" Enabled="False" />
			<px:PXNumberEdit ID="edCompanies" runat="server" DataField="Companies" Enabled="False" />			
		</Template>
		<AutoSize Enabled="False" Container="Parent" />
	</px:PXFormView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid runat="server" ID="grid" Width="100%" Height="100%" DataSourceID="ds" SkinID="Details" Caption="Operations">
		<AutoSize Enabled="true" Container="Window" />
		<Levels>
			<px:PXGridLevel DataMember="Features">
				<Columns>
					<px:PXGridColumn DataField="Enabled" TextAlign="Center" Type="CheckBox" Width="100px" />
					<px:PXGridColumn DataField="Name" Width="300px" />
				</Columns>
				<Layout FormViewHeight="" />
			</px:PXGridLevel>
		</Levels>
	</px:PXGrid>
</asp:Content>