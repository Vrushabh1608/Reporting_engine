﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Reports.Master" AutoEventWireup="true"
    CodeFile="Report_Campaign_Agent_Performance.aspx.cs" Inherits="Report_Campaign_Agent_Performance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

        function CharacterOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 33 && AsciiValue <= 64) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue >= 91 && AsciiValue <= 96) || (AsciiValue >= 123 && AsciiValue <= 126))
                event.returnValue = false;
            else
                event.returnValue = true;
        }

        function openModalDivOverride() {
            $('#DivOverrid').modal({
                backdrop: 'static'
            })

            $('#DivOverrid').modal('show');
        }

        function openModalDivConfirmation() {
            $('#DivConfirmation').modal({
                backdrop: 'static'
            })

            $('#DivConfirmation').modal('show');
        }

        function ValidateEnterKey(evt) {
            if (evt.keyCode == 13) //detect Enter key
            {
                return false;
            }
            else {
                return true;
            }
        }

        function autoTab(input, len, e) {
            var keyCode = (isNN) ? e.which : e.keyCode;
            var filter = (isNN) ? [0, 8, 9] : [0, 8, 9, 16, 17, 18, 37, 38, 39, 40, 46];
            if (input.value.length >= len) {
                input.value = input.value.slice(0, len);
                input.form[(getIndex(input) + 1)].focus();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="Report_Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Campaign Agent Performance Report<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="true" runat="server"
                ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
        </div>
        <!--#nav-search-->
    </div>
    <div id="page-content" class="clearfix">
        <!--/page-header-->
        <div class="row-fluid">
            <!-- -->
            <!-- PAGE CONTENT BEGINS HERE -->
            <asp:UpdatePanel ID="UpdatePanelMsgBox" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="alert alert-block alert-success" id="Msg_Success" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-ok"></i></strong>
                            <asp:Label ID="lblSuccess" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                    <div class="alert alert-error" id="Msg_Error" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-remove"></i>Error!</strong>
                            <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="DivSearchPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>
                            Search Options
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                         <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label14" CssClass="red">Campaign Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCampaignNameSearch" Width="215px" data-placeholder="Select Campaign NAme"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <%--<asp:Label runat="server" ID="Label3">Campaign Name</asp:Label>--%>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <%--<asp:TextBox ID="txtCampaignNameSearch" runat="server" Width="205px"></asp:TextBox>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" ValidationGroup="UcValidateSearch" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>                     

                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" >
                    <HeaderTemplate>
                        <b> Campaign Name</b> </th>
                        <th style="width: 20%; text-align: left;">
                            Agent Name
                        </th> 
                        <th style="width: 15%; text-align: center;">
                            Assigned Contacts
                        </th>
                         <th style="width: 15%; text-align: center;">
                            Contacts Converted To Lead                       
                         </th>
                         <th style="width: 15%; text-align: center;">
                            Contacts Converted To Opportunity
                          </th>
                         <th style="width: 15%; text-align: center;">
                            Closed Deals  
                        
                    </HeaderTemplate>
                    <ItemTemplate>
                                <asp:Label ID="lblCampaignName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Campaign_Name")%>' />
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lblCampaignAgent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Campaign_Agent")%>' />
                            </td>                            
                             <td style="text-align: center;">
                                <asp:Label ID="lblTotalAssignedContacts" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TotalAssignedContacts")%>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTotalConvertedLeadContacts" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ConvertedLeadContacts")%>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTotalConvertedOpportunityContacts" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ConvertedOpportunityContacts")%>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblTotalClosedDeals" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ClosedDeals")%>' />
                            </td>
                    </ItemTemplate>
                </asp:DataList>
<%--
                <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="Label19" Text="" ForeColor="Red" />
                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" 
                        ID="btnAllAuthorise" runat="server"
                        Text="Authorise" ValidationGroup="UcValidate" width="80px" 
                        onclick="btnAllAuthorise_Click"  />                    
                    <asp:ValidationSummary ID="ValidationSummary4" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                </div>--%>
            </div>            
        </div>
    </div>
</asp:Content>
