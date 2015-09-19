<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="ActivitySummary.aspx.cs" Inherits="ActivitySummary" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <script type="text/javascript" language="javascript">
  // not animated collapse/expand
//  function togglePannelStatus(content)
//  {
//      var expand = (content.style.display=="none");
//      content.style.display = (expand ? "block" : "none");
//      toggleChevronIcon(content);
//  }
       function togglePannelStatus(content,val) {

           
           var id = document.getElementById(content).style.display

           if (id == "none") {
               document.getElementById(content).style.display = "inline";
               val.innerHTML = "<B>-</B>";

           }
           else {
               document.getElementById(content).style.display = "none";
               val.innerHTML = "<B>+</B>";
           }
       }
  
    </script>

 <asp:UpdatePanel ID="updateActivitySummary" runat="server">
 

 <ContentTemplate>

    <table  align="center"  width="800px" class="patient_info">
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="4" style="height:25px;vertical-align:middle;"  > <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
                    </td>
                   
            </tr>
              <tr class="medication_info_tr-odd">
              <td align="left" colspan="3" width="90%">
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
               <table  align="center"  width="100%" class="patient_info">
                    <tr >
                    <td align="left" colspan="2" >
                        &nbsp;&nbsp;Select Year &nbsp; 
                        <asp:DropDownList ID="ddlYear" runat="server" 
                            onselectedindexchanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>&nbsp; &nbsp; 
                      
                    </td>
                    <td align="right" colspan="2" >
                        &nbsp;&nbsp;Select Month &nbsp; 
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="100px">
                        </asp:DropDownList>&nbsp; &nbsp; 
                        
                    </td>
                    </tr>
                 </table>
                 </ContentTemplate>
          </asp:UpdatePanel>
                 </td>
                 <td align="right" width="10%" >
                 <asp:LinkButton ID="btnView" runat="server" onclick="btnView_Click">View</asp:LinkButton>
                       
                            &nbsp;&nbsp;
                 </td>
            </tr>
            <tr class="medication_info_tr-even">
            <td align="center" colspan="4" >
                <asp:GridView ID="gridRxQueue" runat="server" AutoGenerateColumns="False"  DataKeyNames="Clinic_ID"
                     Width="800px" onrowdatabound="gridRxQueue_RowDataBound"   OnRowCommand="gridRxQueue_RowCommand" EmptyDataText="No Records Found..."  >
                       <Columns>
                       <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="12px">
                            <ItemTemplate>
                                       <asp:Label ID="lblClinic_ID" runat="server" Text='<%#Eval("Clinic_ID")%>'></asp:Label>

                            </ItemTemplate>                        
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="264px">
                            <ItemTemplate>
                                       <asp:Label ID="lblClinic_Name" runat="server" Text='<%#Eval("Clinic_Name")%>' ></asp:Label>

                            </ItemTemplate>                        
                        </asp:TemplateField>
                                              
                       <asp:TemplateField HeaderText="New Patients" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblNewPatients" runat="server" Text='<%# Eval("NewPatients")%>' ></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="New Rx"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblNewRx" runat="server" Text='<%# Eval("NewRx")%>' ></asp:Label>                            
                            </ItemTemplate>
                       </asp:TemplateField>
                      
                       <asp:TemplateField HeaderText="Refills"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRefills" runat="server" Text='<%# Eval("Refills")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Sample"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblsample" runat="server" Text='<%# Eval("Sample")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="PAP"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblPAP" runat="server" Text='<%# Eval("PAP")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Payments"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblPayments" runat="server" Text='<%# Eval("Payments")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="S/P Billing"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblBilling" runat="server" Text='<%# Eval("SPBilling")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Med Reqs"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblMedReq" runat="server" Text='<%# Eval("MedRequests")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="other"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblother" runat="server" Text='<%# Eval("other")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="1px">
                           
                            <ItemTemplate>
                                &nbsp;                           
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       
                       
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                 </asp:GridView>
                 
                 </td>
            </tr>
            <tr><td  colspan="4">&nbsp;</td></tr>
            <tr class="medication_info_th1">
               
                    <td align="center"  colspan="4" style="height:25px;vertical-align:middle;"  > <asp:Label ID="lblRX30Heading" runat="server" Text=""></asp:Label>
                    </td>
                   
            </tr>
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="4" >
                <asp:GridView ID="gridRx30Summary" runat="server" AutoGenerateColumns="False"  DataKeyNames="Clinic_ID"
                     Width="800px" onrowdatabound="gridRx30Summary_RowDataBound"   OnRowCommand="gridRx30Summary_RowCommand" EmptyDataText="No Records Found..."  >
                       <Columns>
                       <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="12px">
                            <ItemTemplate>
                                       <asp:Label ID="lblClinic_ID" runat="server" Text='<%#Eval("Clinic_ID")%>'></asp:Label>

                            </ItemTemplate>                        
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="264px">
                            <ItemTemplate>
                                       <asp:Label ID="lblClinic_Name" runat="server" Text='<%#Eval("Clinic_Name")%>' ></asp:Label>

                            </ItemTemplate>                        
                        </asp:TemplateField>
                                              
                       <asp:TemplateField HeaderText="New Patients" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblNewPatients" runat="server" Text='<%# Eval("NewPatients")%>' ></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="New Rx"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblNewRx" runat="server" Text='<%# Eval("NewRx")%>' ></asp:Label>                            
                            </ItemTemplate>
                       </asp:TemplateField>
                      
                       <asp:TemplateField HeaderText="Refills"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRefills" runat="server" Text='<%# Eval("Refills")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Sample"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblsample" runat="server" Text='<%# Eval("Sample")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="PAP"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblPAP" runat="server" Text='<%# Eval("PAP")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Payments"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblPayments" runat="server" Text='<%# Eval("Payments")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                      
                       <asp:TemplateField HeaderText="Med Reqs"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblMedReq" runat="server" Text='<%# Eval("MedRequests")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       
                       
                       <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="1px">
                           
                            <ItemTemplate>
                                &nbsp;                           
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       
                       
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                 </asp:GridView>
                 </td>
            </tr>
            </table>
            <%--
    <div id="div2"  style="position:absolute;border:solid black 0px;top:25%;right:25%;bottom:25%;left:25%;padding:25px; margin:25px;">
	 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updateActivitySummary" DisplayAfter="250">
       <ProgressTemplate>
          <div class="modalBackground"> 
           <table>
           <tr>
           <td class="label" valign="middle" align="center">
                 Processing...Please Wait..!
           </td>
           </tr>
           </table>
           </div>
       </ProgressTemplate>
   </asp:UpdateProgress>
    </div> --%>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

