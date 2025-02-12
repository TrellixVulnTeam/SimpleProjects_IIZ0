<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true"
	ValidateRequest="false" CodeFile="GL204500.aspx.cs" Inherits="Page_GL204500"
	Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.GL.AllocationMaint"
		PrimaryView="AllocationHeader">
		<CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />
			<px:PXDSCallbackCommand StartNewGroup="true" Name="First" />
			<px:PXDSCallbackCommand DependOnGrid="grBatches" Name="ViewBatch" Visible="False">
			</px:PXDSCallbackCommand>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="Cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="frmHeader" runat="server" Width="100%" Caption="Allocation Info"
		DataMember="AllocationHeader"  NoteIndicator="True" FilesIndicator="True"
		ActivityIndicator="True" ActivityField="NoteActivity" DefaultControlID="edGLAllocationID"
		TabIndex="100">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
			<px:PXSelector ID="edGLAllocationID" runat="server" DataField="GLAllocationID" >
				<GridProperties FastFilterFields="Descr">
				</GridProperties>
			</px:PXSelector>
			<px:PXTextEdit ID="edDescr" runat="server" DataField="Descr" />
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
			<px:PXCheckBox ID="chkActive" runat="server" Checked="True" DataField="Active" />
			<px:PXSegmentMask ID="edBranchID" runat="server" DataField="BranchID" DataMember="_Branch_"
				 /></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXTab ID="tab" runat="server"   DataMember="Allocation"
		Height="300px" Width="100%">
		<AutoSize Enabled="True" MinHeight="300" Container="Window" />
		<Items>
			<px:PXTabItem Text="Allocation">
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="M" />
					<px:PXSelector ID="edStartFinPeriodID" runat="server" DataField="StartFinPeriodID" />					
					<px:PXSelector ID="edEndFinPeriodID" runat="server" DataField="EndFinPeriodID" />
					<px:PXCheckBox ID="chkRecurring" runat="server" Checked="True" DataField="Recurring" />
					<px:PXDropDown ID="edAllocCollectMethod" runat="server" DataField="AllocCollectMethod" />
					<px:PXDropDown CommitChanges="True" ID="edAllocMethod" runat="server" DataField="AllocMethod" />
					<px:PXSelector CommitChanges="True" ID="edAllocLedgerID" runat="server" DataField="AllocLedgerID" />
					<px:PXSelector ID="edSourceLedgerID" runat="server" DataField="SourceLedgerID" AutoRefresh="True">
						<Parameters>
							<px:PXControlParam ControlID="tab" Name="GLAllocation.allocLedgerID" PropertyName="DataControls[&quot;edAllocLedgerID&quot;].Value" />
						</Parameters>
						<CallBackMode ContainerID="tab" PostData="Container" />
					</px:PXSelector>
					<px:PXSelector ID="edBasisLederID" runat="server" DataField="BasisLederID" />
					<px:PXNumberEdit ID="edSortOrder" runat="server" DataField="SortOrder" Size="S" />
					<px:PXDateTimeEdit ID="edLastRevisionOn" runat="server" DataField="LastRevisionOn" />
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Source Accounts">
				<Template>
					<px:PXGrid ID="grSource" runat="server"  Height="300px" Width="100%"
						SkinID="DetailsInTab" >
						<Levels>
							<px:PXGridLevel DataMember="Source">
								<RowTemplate>
									<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
									<px:PXSegmentMask ID="edAccountCD" runat="server" DataField="AccountCD" SelectMode="Segment"
										Wildcard="?" />
									<px:PXSegmentMask ID="edSubCD" runat="server" DataField="SubCD" SelectMode="Segment"
										Wildcard="?" />
									<px:PXSegmentMask ID="edContrAccountID" runat="server" DataField="ContrAccountID" />
									<px:PXSegmentMask ID="edContrSubID" runat="server" DataField="ContrSubID" />
									<px:PXNumberEdit ID="edLimitAmount" runat="server" DataField="LimitAmount" />
									<px:PXNumberEdit ID="edLimitPercent" runat="server" DataField="LimitPercent" />
									<px:PXDropDown ID="edPercentLimitType" runat="server" DataField="PercentLimitType" />
									<px:PXSegmentMask CommitChanges="True" ID="edBranchID" runat="server" DataField="BranchID" /></RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="GLAllocationID" Width="100px" />
									<px:PXGridColumn DataField="LineID" TextAlign="Right" Width="50px" />
									<px:PXGridColumn DataField="BranchID" Width="120px" AutoCallBack="True" />
									<px:PXGridColumn DataField="AccountCD" Width="120px" AutoCallBack="True" />
									<px:PXGridColumn DataField="SubCD" Width="120px" AutoCallBack="True" />
									<px:PXGridColumn DataField="ContrAccountID" Width="120px" AutoCallBack="True" />
									<px:PXGridColumn DataField="ContrSubID" Width="120px" />
									<px:PXGridColumn DataField="LimitAmount" TextAlign="Right" Width="100px" AutoCallBack="True" />
									<px:PXGridColumn DataField="LimitPercent" TextAlign="Right" Width="100px" AutoCallBack="True" />
								</Columns>
								<Layout FormViewHeight="" />
							</px:PXGridLevel>
						</Levels>
						<AutoSize Enabled="True" />
						<ActionBar>
							<Actions>
								<NoteShow Enabled="False" />
							</Actions>
						</ActionBar>
						<Mode AllowUpload="True" InitNewRow="True" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Destination Accounts">
				<Template>
					<px:PXGrid ID="grDest" runat="server"  Height="300px" Width="100%"
						SkinID="DetailsInTab">
						<Levels>
							<px:PXGridLevel DataMember="Destination">
								<RowTemplate>
									<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
									<px:PXSegmentMask ID="edAccountID" runat="server" DataField="AccountID" />
									<px:PXSegmentMask ID="edSubID" runat="server" DataField="SubID" />									
									<px:PXSegmentMask ID="edBasisAccountCD" runat="server" DataField="BasisAccountCD" SelectMode="Segment"
										Wildcard="?" />
									<px:PXSegmentMask ID="edBasisSubCD" runat="server" DataField="BasisSubCD" SelectMode="Segment"
										Wildcard="?" />
                                    <px:PXNumberEdit ID="edWeight" runat="server" DataField="Weight" />
									<px:PXSegmentMask CommitChanges="True" ID="edBranchID1" runat="server" DataField="BranchID" />
								</RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="GLAllocationID" AllowShowHide="Server" />
									<px:PXGridColumn DataField="LineID" TextAlign="Right" AllowShowHide="Server" />
									<px:PXGridColumn DataField="BranchID" Width="100px" AutoCallBack="True" />
									<px:PXGridColumn DataField="AccountID" Width="120px" />
									<px:PXGridColumn DataField="SubID" Width="120px" />                                    
									<px:PXGridColumn DataField="BasisBranchID" Width="100px" AutoCallBack="True" />									
                                    <px:PXGridColumn DataField="BasisAccountCD" Width="120px" AutoCallBack="True" />
									<px:PXGridColumn DataField="BasisSubCD" Width="120px" AutoCallBack="True" />
									<px:PXGridColumn DataField="Weight" TextAlign="Right" Width="100px" RenderEditorText="True" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<Mode InitNewRow="True" AllowUpload="True" />
						<AutoSize Enabled="True" />
						<ActionBar>
							<Actions>
								<NoteShow Enabled="False" />
							</Actions>
						</ActionBar>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Allocation History">
				<Template>
					<px:PXGrid ID="grBatches" runat="server"  Height="100%" Width="100%"
						AdjustPageSize="Auto" SkinID="DetailsInTab" AllowSearch="True">
						<Levels>
							<px:PXGridLevel DataMember="Batches">
								<RowTemplate>
									<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="S" />
									<px:PXSelector ID="edBatchNbr" runat="server" DataField="BatchNbr" AllowEdit="True" />
									<px:PXSelector ID="edLedgerID" runat="server" DataField="LedgerID" />
									<px:PXDateTimeEdit ID="edDateEntered" runat="server" DataField="DateEntered" />
									<px:PXSelector ID="edFinPeriodID" runat="server" DataField="FinPeriodID" />
									<px:PXDropDown ID="edStatus" runat="server" DataField="Status" Enabled="False" />
									<px:PXNumberEdit ID="edCuryControlTotal" runat="server" DataField="CuryControlTotal" /></RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="Module" Visible="False"  />
									<px:PXGridColumn DataField="BatchNbr" Width="70px"  />
									<px:PXGridColumn DataField="LedgerID" Width="100px"  />
									<px:PXGridColumn DataField="DateEntered" Width="70px"  />
									<px:PXGridColumn DataField="FinPeriodID" Width="70px"  />
									<px:PXGridColumn DataField="Status" RenderEditorText="True" Width="100px" />
									<px:PXGridColumn DataField="CuryControlTotal" TextAlign="Right" Width="100px" MatrixMode="True" />
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<ActionBar>
							<Actions>
								<AddNew Enabled="False" />
								<Delete Enabled="False" />
								<NoteShow Enabled="False" />
							</Actions>
							<CustomItems>
								<px:PXToolBarButton Text="Batch Details" CommandSourceID="ds" CommandName="ViewBatch" />
							</CustomItems>
						</ActionBar>
						<AutoSize Enabled="True" MinHeight="100" MinWidth="100" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
		</Items>
	</px:PXTab>
</asp:Content>
