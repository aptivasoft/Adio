<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Rx30RxLog.aspx.cs" Inherits="Rx30RxLog" Title="eCarex Health Care System" %>
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
       function togglePannelStatus(content, val) {
        
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
 
    <table align="center"  width="100%" >
      <tr class="medication_info_th1">
               
                    <td align="center"  colspan="4" style="height:25px;vertical-align:middle;"  > <asp:Label ID="lblRX30Heading" runat="server" Text=""></asp:Label>
                    </td>
                   
            </tr>
              <tr class="medication_info_tr-odd">
              <td align="left" colspan="3" width="40%">
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
               <table>
                    <tr>
                    <td align="left" >
                        &nbsp;&nbsp;Select Year &nbsp; 
                        <asp:DropDownList ID="ddlYear" runat="server" 
                            onselectedindexchanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>&nbsp; &nbsp; 
                    </td>
                    <td align="left" >
                        &nbsp;&nbsp;Select Month &nbsp; 
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="100px">
                        </asp:DropDownList>&nbsp; &nbsp; 
                        
                    </td>
                    </tr>
                 </table>
                 </ContentTemplate>
          </asp:UpdatePanel>
                 </td>
                 <td align="left" width="60%" >
                   <asp:LinkButton ID="btnView" runat="server" onclick="btnView_Click">
                   <asp:Image ID="imgView" runat="server" ImageUrl="~/images/search_new.png" Style="height: 24px" />
                   </asp:LinkButton>
                 </td>
            </tr>
           
           
            
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="4" >
                <asp:GridView ID="gridRx30Summary" runat="server" AutoGenerateColumns="False"  DataKeyNames="Clinic_ID"
                     Width="1000px" onrowdatabound="gridRx30Summary_RowDataBound"   
                    OnRowCommand="gridRx30Summary_RowCommand" EmptyDataText="No Records Found..." 
                    onrowcreated="gridRx30Summary_RowCreated" CssClass="medication_info" >
                       <Columns>
                   <%--    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                            <ItemTemplate>
                                       <asp:Label ID="lblClinic_ID" runat="server" Text='<%#Eval("Clinic_ID")%>'></asp:Label>

                            </ItemTemplate>                        
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" Visible="false" >
                            <ItemTemplate>
                                       <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("Region")%>'></asp:Label>

                            </ItemTemplate>                        
                        </asp:TemplateField>
                       
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="400px">
                            <ItemTemplate>
                                       <asp:Label ID="lblClinic_Name" runat="server" Text='<%#Eval("Clinic_Name")%>' ></asp:Label>

                            </ItemTemplate>                        
                        </asp:TemplateField>
                                              
                       <asp:TemplateField HeaderText="New Rx"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblNewRx" runat="server" Text='<%# Eval("NewRx")%>' ></asp:Label>                            
                            </ItemTemplate>
                             
                       </asp:TemplateField>
                      
                      <%-- <asp:TemplateField HeaderText="Refills"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRefills" runat="server" Text='<%# Eval("Refills")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField> 
                       <asp:TemplateField HeaderText="Goal"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblGoal" runat="server" Text='<%# Eval("Goal")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Achieved Goal%"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="75px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblGoalP" runat="server" Text='<%# Eval("GoalP") %>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Goal Target%"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="75px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblGoalT" runat="server" Text='<%# Eval("GoalT")%>' ></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>    
                         
                        <asp:TemplateField HeaderText="Total">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTotalNewRx" Text="0" />
                        </ItemTemplate>
                        </asp:TemplateField>--%>
                       <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                       <asp:Label ID="lblClinic_ID" runat="server" Text='<%#Eval("Clinic_ID")%>'></asp:Label>

                            </ItemTemplate>                        
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" Visible="false" >
                            <ItemTemplate>
                                       <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("Region")%>'></asp:Label>

                            </ItemTemplate>                        
                        </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="400px" >
                            <ItemTemplate>
                                       <asp:Label ID="lblClinic_Name" runat="server" Text='<%#Eval("Clinic_Name")%>' ></asp:Label>

                            </ItemTemplate>                        
                        </asp:TemplateField>
                                              
                       <asp:TemplateField HeaderText="Scrip Totals"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px" HeaderStyle-BackColor="#806D7E">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblNewRx" runat="server" Text='<%# Eval("NewRx")%>' ></asp:Label>                            
                            </ItemTemplate>
                             
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Month Completion %"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px" HeaderStyle-BackColor="#806D7E">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblGoaT" runat="server" Text='<%# Eval("GoalT") %>'></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                      
                       <asp:TemplateField HeaderText="10% Month Goal"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px" HeaderStyle-BackColor="#566D7E">
                           
                            <ItemTemplate>
                                <asp:Label ID="lbl10PMonthGoal" runat="server"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="% of 10% Goal"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px" HeaderStyle-BackColor="#566D7E">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblPof10PGoal" runat="server"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Scrips Needed 10% Goal"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px" HeaderStyle-BackColor="#566D7E">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblScripsNeeded10PGoal" runat="server"></asp:Label>                            
                            </ItemTemplate>                            
                       </asp:TemplateField>
                       
                       
                        <asp:TemplateField HeaderText="25% Month Goal"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px" HeaderStyle-BackColor="#5E5A80" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lbl25PMonthGoal" runat="server"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="% of 25% Goal"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px" HeaderStyle-BackColor="#5E5A80" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblPof25PGoal" runat="server"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Scrips Needed 25% Goal"  ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="70px" HeaderStyle-BackColor="#5E5A80" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblScripsNeeded25PGoal" runat="server"></asp:Label>                            
                            </ItemTemplate>                            
                       </asp:TemplateField>
                       <asp:TemplateField   ItemStyle-HorizontalAlign="Right" Visible="false" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblAvgMonthly" runat="server" Text='<%# Eval("Goal") %>'></asp:Label>                            
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
 
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

