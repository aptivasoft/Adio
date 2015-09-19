<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Facility.aspx.cs" Inherits="Facility" Title="eCarex Health Care System" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="updatePanelFacilityInfo" UpdateMode = "Conditional" runat="server">
<ContentTemplate>    
<cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtSearchFac" UseContextKey="True"  ContextKey="0" MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetFacilityNames" BehaviorID="ModelExtender" runat="server" OnClientItemSelected="publishFacInfo">
</cc1:AutoCompleteExtender>
<cc1:AutoCompleteExtender ID="AutoCompleteExtender2" TargetControlID="txtClinic" UseContextKey="True"  ContextKey="0" MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetClinic" runat="server">
</cc1:AutoCompleteExtender>


<table border="0" width="80%" align = "Center" >      
      <tr class="medication_info_th1">
                    <td colspan="4" align="center" width="100%" height="20px">
                        <asp:Label id="lblTitle" runat="server" Text="FACILITY INFORMATION" 
                            Font-Bold="True" Font-Size="Small"></asp:Label>
	
                    </td>
     </tr>   
      <tr class="medication_info_tr-even">
             <td align="left"><asp:Label ID="lblSearch" runat="server" Text="Search Facility" Width="150px" ></asp:Label></td>
             <td colspan="3" align = "left">
                        
                        <asp:TextBox ID="txtSearchFac" runat="server" Width="300px" TabIndex="18"></asp:TextBox>                 
                        <asp:ImageButton ID="btnSearchFac" runat="server" ImageUrl="~/images/search_new.png" border="0" onclick="btnSearchFac_Click" TabIndex="19" Width="24px" Height="24px"></asp:ImageButton>
               </td>
       </tr>
      <tr class="medication_info_tr-odd">
            <td width="150px" align="left">
              <asp:Label ID="lblfacID" runat="server" Text="Facility Code"></asp:Label>
            </td>
            <td width="350px" align="left">
              <asp:TextBox ID="txtfacID" runat="server" Width="150px"
                  style="margin-left: 0px" TabIndex="1"></asp:TextBox>
            </td>
            <td width="150px" align="left">
              <asp:Label ID="lblfacPhone" runat="server" Text="Phone"></asp:Label>
            </td>
            <td width="350px" align="left">
              <asp:TextBox ID="txtfacPhone" runat="server" Width="150px" 
                     TabIndex="8"></asp:TextBox>
            </td>
      </tr>
      <tr class="medication_info_tr-even">
        <td align="left">
          <asp:Label ID="lblfacName" runat="server" Text="Facility Name" ></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtfacName" runat="server" Width="150px" TabIndex="2"></asp:TextBox>
        </td>
        <td align="left">
          <asp:Label ID="lblfacFax" runat="server" Text="Fax"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtfacFax" runat="server" Width="150px" 
                TabIndex="9"></asp:TextBox>
        </td>
      </tr>
      <tr class="medication_info_tr-odd">
        <td align="left">
          <asp:Label ID="lblfacAdd" runat="server" Text="Address"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtfacAdd" runat="server" Width="150px" TabIndex="3"></asp:TextBox>
        </td>
        <td align="left">
          <asp:Label ID="lblfacEmail" runat="server" Text="e Mail"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtfacEmail" runat="server" Width="150px" TabIndex="10"></asp:TextBox>
        </td>
      </tr>
      <tr class="medication_info_tr-even">
        <td align="left">
          <asp:Label ID="lblfacCity" runat="server" Text="City"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtfacCity" runat="server" Width="150px" TabIndex="4"></asp:TextBox>
        </td>
        
        <td align="left">
          <asp:Label ID="lblfacTaxID" runat="server" Text="Tax ID"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtfacTaxID" runat="server" Width="150px" TabIndex="11"></asp:TextBox>
        </td>
      </tr>
      <tr class="medication_info_tr-odd">
        <td align="left">
          <asp:Label ID="lblfacState" runat="server" Text="State"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtfacState" runat="server" Width="150px" MaxLength="2" 
                TabIndex="5"></asp:TextBox>
        </td>
        
        <td align="left">
          <asp:Label ID="lblfacSpeciality" runat="server" Text="Speciality"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtfacSpeciality" runat="server" Width="150px" TabIndex="12"></asp:TextBox>
        </td>
      </tr>
      <tr class="medication_info_tr-even">
        <td align="left">
          <asp:Label ID="lblfacZip" runat="server" Text="ZIP"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtfacZip" runat="server" Width="150px"  
                 TabIndex="6" ></asp:TextBox>
        </td>
        <td align="left">
          <asp:Label ID="lblfacProvID" runat="server" Text="Provision ID"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtfacProvID" runat="server" Width="150px" TabIndex="13"></asp:TextBox>
        </td>
      </tr>
      <tr class="medication_info_tr-odd">
        <td align="left">
          <asp:Label ID="lblClinicName" runat="server" Text="Search Clinic"></asp:Label>
        </td>
        <td>
          <asp:TextBox ID="txtClinic" runat="server" Width="150px"  
                 TabIndex="7" ></asp:TextBox>
        </td>
        <td colspan="2">
        <asp:CheckBox ID="chkStampAddr" runat="server" Text="Mailing Facility Address" />
         </td>
      </tr>
      <tr class="medication_info_tr-even" align = "center">                             
          <td colspan="4" class="medication_info_td-odd">  
              <asp:ImageButton ID="btnFacSave" runat="server"  ImageUrl="../images/save.gif"
                          border="0" onclick="btnFacSave_Click" Style="height: 23px"
                  OnClientClick=" return validateNewFacility();" TabIndex="14"  ></asp:ImageButton>
          
            <asp:ImageButton ID="btnFacUpdate" runat="server" ImageUrl="~/images/update.png" 
                      border="0" Visible="false" 
            Style="height: 23px" OnClientClick = "return validateNewFacility();" 
                      onclick="btnFacUpdate_Click" TabIndex="15" ></asp:ImageButton>
           
            <asp:ImageButton ID="btnFacCancel" runat="server" 
                    ImageUrl="../images/cancelPopUp.gif" border="0" Style="height: 23px" onclick="btnFacCancel_Click" 
                    TabIndex="16"></asp:ImageButton>
            
            <asp:ImageButton ID="btnFacDelete" runat="server" 
                      ImageUrl="~/images/delete.png" border="0"
                    Visible="false" Style="height: 23px" onclick="btnFacDelete_Click"  OnClientClick="if(!confirm('Do you want to delete Facility')){ return false;}"
                      TabIndex="17" ></asp:ImageButton>
          </td>
          </tr>
          <tr class="medication_info_tr-odd">
         <td colspan="4" align="right"> 
                    <asp:LinkButton ID="lnkbtnPop" runat="server" OnClientClick = "return openwindow();">Click here</asp:LinkButton> &nbsp; to view Facility list
              
                   </td>
                   </tr>
            </table>
            
       
   </ContentTemplate></asp:UpdatePanel>
<script type = "text/javascript" language = "javascript">
    function openwindow() {
        window.open("FacilityList.aspx", "mywindow", "toolbar = no,width =600,scrollbars = yes");
    }
</script> 
</asp:Content>
