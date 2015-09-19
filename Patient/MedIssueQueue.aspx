<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="MedIssueQueue.aspx.cs" Inherits="MedIssueQueue" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
 <cc1:ModalPopupExtender ID="popMedIssue" runat="server" TargetControlID="btnMedIssue" PopupControlID="pnlMedIssue" CancelControlID="btnMedIssueCancel" BackgroundCssClass="modalBackground" BehaviorID="MedIssue"></cc1:ModalPopupExtender>

   
 
    <table  align="center"  width="800px" class="patient_info">
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="4" style="height:25px;vertical-align:middle;"  > <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
                    </td>
                   
            </tr>
           
            <tr class="medication_info_tr-even">
            <td align="center" colspan="4" >
                 <asp:GridView ID="gridRxMedIssue" runat="server" AutoGenerateColumns="False" 
                     Width="800px"  OnRowCommand="gridRxMedIssue_RowCommand"  EmptyDataText="No Records Found..."  >
                     <Columns>
                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Rx_Date")%>'></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Patient" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                            <ItemTemplate>
                               <asp:Label ID="lblpatientName" runat="server" Text='<%#Eval("PatientName")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Desc")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       
                       
                       <asp:TemplateField ItemStyle-Width="100px" HeaderText="Action" >
                                <ItemTemplate >
                                     <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Action" CommandArgument='<%# Eval("Call_ID")%>' Font-Bold="false">Action</asp:LinkButton>
                               
                                      
                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                           
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                 </asp:GridView>
                 </td>
            </tr>
            </table>
     
         <asp:Panel ID="pnlMedIssue" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
                <table width="350"   border="0" cellpadding="0" cellspacing="0">
                  <tr class="medication_info_th1" width="100%">
                    <td  height="30px" align="left">
                        Med Issue
                    </td>
                    <td align="right">
                      <asp:ImageButton ID="btnMedIssueCancel" runat="server"  ImageUrl="../images/CloseRx.gif" border="0"></asp:ImageButton>
                    </td>
                  </tr>
                  <tr>
                    <td width="350" colspan="2" align="center">
                      <table aligin="center" class="medication_info_popup" cellpadding="0" cellspacing="1">
                        <tr >
                          <td align="left">
                            <asp:Label ID = "lblMedIssue" Text = "Medication Issue:" runat = "server" ></asp:Label>
                          </td>
                          <td>
                            
                                <asp:Label ID = "lblMedIssue1"  runat = "server" ></asp:Label>
                             
                          </td>
                        </tr>
                       
                        
                       
                       
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblMedIssueComment" Text = "Comment:" runat = "server" ></asp:Label>
                          </td>
                          <td align="left">
                            <asp:TextBox ID="txtMedIssueComment" runat="server" TextMode="multiLine" Width="180px"></asp:TextBox>
                          </td>
                        </tr>
                        
                        
                        <tr>
                          <td  align="center" colspan="2">
                           <asp:HiddenField ID="hfCallID" runat="server" />
                            
                                <asp:ImageButton ID="btnMedIssueSave" runat="server"  ImageUrl="../images/save.gif" border="0" OnClick="btnMedIssueSave_Click"></asp:ImageButton>
                             
                          </td>
                          
                        </tr>
                      </table>
                    </td>
                   
                  </tr>
                </table>
              </asp:Panel>
                  <asp:Button ID="btnMedIssue" runat="server" Text="Button" Style="visibility:hidden" />
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

