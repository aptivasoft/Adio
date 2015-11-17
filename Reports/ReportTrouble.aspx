<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportTrouble.aspx.cs" Inherits="Reports_ReportTrouble" %>--%>


<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="ReportTrouble.aspx.cs" Inherits="Reports_ReportTrouble" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
 
    <table  align="center"  width="800px" class="patient_info">
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="4" style="height:25px;vertical-align:middle;"  > <asp:Label ID="lblHeading" runat="server" Text="Trouble Patient List"></asp:Label>
                    </td>
                   
            </tr>
             <tr class="medication_info_tr-odd">
                <td align="left" style="width:300px">
                    <asp:Label ID="lblOrganization" runat="server" Text="Organization :"></asp:Label>
                    &nbsp;&nbsp;<asp:DropDownList
                        ID="ddlOrganization" runat="server" 
                        OnDataBound="ddlOrganization_DataBound" AutoPostBack="True" 
                        onselectedindexchanged="ddlOrganization_SelectedIndexChanged" style="width:200px"></asp:DropDownList>
                        </td>
                <td align="left" style="width:225px"  >&nbsp;
                        <asp:Label ID="lblLocation" runat="server" Text="Location :"></asp:Label>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlLocation" runat="server" OnDataBound="ddlLocation_DataBound" 
                        onselectedindexchanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True" style="width:150px"></asp:DropDownList>
                    
                </td>
                
                <td align="left" width ="50px" colspan="2">
                    &nbsp;
                   
                </td>
          
            </tr>   
            <tr class="medication_info_tr-odd">
            <td align="right" colspan="4">
                <asp:label id="SortInformationLabel" forecolor="Navy" runat="server"/>

             </td>
            
            </tr>
            <tr class="medication_info_tr-even">
            <td align="center" colspan="4" >
                <asp:GridView ID="gridRxQueue" runat="server" 
                     AutoGenerateColumns="False" 
                     allowsorting="true"
                     onsorting="gridRxQueue_Sorting"
                     onsorted="gridRxQueue_Sorted" 
                     Width="800px"  
                     OnRowCommand="gridRxQueue_RowCommand" EmptyDataText="No Records Found..."  >
                       <Columns>
                       <asp:TemplateField HeaderText="Survey#" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbldrugID" class="labelID" runat="server" Text='<%#Eval("Survey_ID")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="Patient" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("PatientName")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>

                           <asp:TemplateField HeaderText="Survey Date/Time" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("Survey_Time")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="Enough Medicine?" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblQID1RES" runat="server" Text='<%# Eval("QID1RES")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>

                           <asp:templatefield headertext="Medicine working well?" itemstyle-horizontalalign="left" itemstyle-width="50px">
                            <itemtemplate>
                                &nbsp;&nbsp;&nbsp;<asp:label id="lblQID2RES" runat="server" text='<%#Eval("QID2RES")%>' font-bold="false"></asp:label>
                            </itemtemplate>                        
                        </asp:templatefield>

                           <asp:templatefield headertext="Missed Appt." itemstyle-horizontalalign="left" itemstyle-width="50px">
                            <itemtemplate>
                                &nbsp;&nbsp;&nbsp;<asp:label id="lblQID3RES" runat="server" text='<%#Eval("QID3RES")%>' font-bold="false"></asp:label>
                            </itemtemplate>                        
                        </asp:templatefield>

                           <asp:templatefield headertext="Transportation Difficulty" itemstyle-horizontalalign="left" itemstyle-width="50px">
                            <itemtemplate>
                                &nbsp;&nbsp;&nbsp;<asp:label id="lblQID4RES" runat="server" text='<%#Eval("QID4RES")%>' font-bold="false"></asp:label>
                            </itemtemplate>                        
                        </asp:templatefield>


                           <asp:templatefield headertext="Enough Medicine?" itemstyle-horizontalalign="left" itemstyle-width="50px">
                            <itemtemplate>
                                &nbsp;&nbsp;&nbsp;<asp:label id="lblQID5RES" runat="server" text='<%#Eval("QID5RES")%>' font-bold="false"></asp:label>
                            </itemtemplate>                        
                        </asp:templatefield>

                           <asp:templatefield headertext="More bad days?" itemstyle-horizontalalign="left" itemstyle-width="50px">
                            <itemtemplate>
                                &nbsp;&nbsp;&nbsp;<asp:label id="lblQID5RES" runat="server" text='<%#Eval("QID5RES")%>' font-bold="false"></asp:label>
                            </itemtemplate>                        
                        </asp:templatefield>

                           <asp:templatefield headertext="" itemstyle-horizontalalign="left" itemstyle-width="50px">
                            <itemtemplate>
                                &nbsp;&nbsp;&nbsp;<asp:LinkButton reportId='<%# Eval("Survey_ID")%>' followupStatus='<%# Eval("Followup_Status")%>' id="lblClose" runat="server" text='Close' font-bold="false" href="#myModal" data-toggle="modal" OnClientClick="openPouup(this);"></asp:LinkButton>
                            </itemtemplate>                        
                        </asp:templatefield>
                       
                       
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                 </asp:GridView>
                 </td>
            </tr>
            </table>

    <!-- this is bootstrp modal popup -->  
        <div id="myModal" class="modal fade">  
            <div class="modal-dialog" style="width: 373px;margin-top: 250px;">  
                <div class="modal-content" style="background-color: #B5D1E8;">  
                   <%-- <div class="modal-header">  
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>  
                          <h4 class="modal-title">Survey Followup update: Closed</h4>
                    </div>  --%>
                    <div class="modal-body" style="font-family: Verdana, Arial, Helvetica, sans-serif; ">  
                        <h6 class="modal-title" style="padding-bottom: 8px;">Survey Followup update: <span style="color:red;">Closed</span></h6>
                         <textarea id="txtArea" runat="server" rows="5" cols="45" placeholder="Enter Comments" style="font-size:11px;font-family: Verdana, Arial, Helvetica, sans-serif;"></textarea>
                        <div style="width:42%; margin:0 auto;padding-top: 10px;">
                            <button id="btnSaveComment" class="btn btn-default" runat="server" style="padding-left: 12px;padding-right: 10px;font-size: 12px;" onserverclick="save_OnClick">Save</button>  
                            <button type="button" class="btn btn-default" data-dismiss="modal" style="padding-left: 12px;padding-right: 10px;font-size: 12px;">Close</button> 
                            <asp:HiddenField ID="HiddenField1" runat="Server" Value="This is the Value of Hidden field" />
                        </div>
                    </div>  
                    <%--<div class="modal-footer">
                      <div style="width:40%; margin:0 auto;">
                            <button id="Button1" class="btn btn-default" runat="server"   onserverclick="save_OnClick">Save</button>  
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button> 
                            <asp:HiddenField ID="HiddenField2" runat="Server" Value="This is the Value of Hidden field" />
                        </div>  
                    </div>  --%>
                </div>  
            </div>  
        </div>  
         
        <!-- end -->  
    <script type="text/javascript">
        function openPouup(value) {
            var reportId = $(value).attr("reportId");
            var followupStatus = $(value).attr("followupStatus");
            $("#<%= HiddenField1.ClientID %>").attr("Value", reportId + "," +followupStatus);
        }
    </script>
    
</asp:Content>

