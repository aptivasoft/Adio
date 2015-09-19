<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="GenerateStamp.aspx.cs" Inherits="GenerateStamp" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" width="100%" cellpadding="1">
       <tr>
       <td valign="top"  width="30%"  >
          <asp:UpdatePanel ID="upPatRx" runat="server">
          <ContentTemplate>
             <table align="center" width="100%">
       <tr class="medication_info_th1">
         <td colspan="4" align="center" valign="middle" height="25px" width="100%">Stamps</td>
       </tr>
       <tr class="medication_info_tr-odd" >
       <td><asp:Label ID="lblRxType" runat="server" Text="Rx Type :" Font-Bold="true"></asp:Label></td>
       <td align="left" colspan="3"> 
           <asp:RadioButton ID="rbtnRegular" runat="server" Text="Regular" GroupName="Rxtype" Checked="true" />
           &nbsp;
           <asp:RadioButton ID="rbtnSample" runat="server" Text="Sample" GroupName="Rxtype" />
           &nbsp;
           <asp:RadioButton ID="rbtnPAP" runat="server" Text="PAP" GroupName="Rxtype" />&nbsp;
       </td> 
       </tr>
       <tr class="medication_info_tr-even" >  
      <td><asp:Label ID="lblPhrm" runat="server" Text="Pharmacy :" Font-Bold="true"></asp:Label></td>
       <td align="left" valign="top" colspan="3">
         <div id="divSelect" style="display:inline" runat="server">
           <asp:RadioButtonList ID="rbtnSelect" runat="server" RepeatColumns="2">
           <asp:ListItem Text="Paducah" Value="P" Selected="True"></asp:ListItem>
           <asp:ListItem Text="ETown" Value="E"></asp:ListItem>
           </asp:RadioButtonList>
        </div>
       </td>
           
       </tr>
       <tr class="medication_info_tr-odd">
        <td><asp:Label ID="lblDate" runat="server" Text="Date :" Font-Bold="true"></asp:Label></td>    
            <td colspan="3">
              <asp:TextBox ID="txtDate" Width="90px" runat="server" ></asp:TextBox>
               <cc1:CalendarExtender ID="CE_ExpiryDate" runat="server" TargetControlID="txtDate" PopupButtonID="txtDate" Enabled="True"></cc1:CalendarExtender>
               <asp:ImageButton ID="imgSrcDrugs"  runat="server" ImageUrl="../Images/search_new.png" 
                   ToolTip="Search" Width="24px" Height="24px" onclick="imgSrcDrugs_Click"/> 
            </td>   
 
       
       </tr>
       <tr class="medication_info_tr-odd">
       <td colspan="4" >
           <asp:UpdatePanel ID="upGridRx1"  UpdateMode="Conditional" runat="server">
           <ContentTemplate>
           <asp:GridView ID="gridRx1" runat="server"  Width="100%" AutoGenerateColumns="False"  OnRowCommand="gridRx1_RowCommand"  EmptyDataText="No Records Found..."  ShowFooter="true"  onrowcreated="gridRx1_RowCreated">
                    <FooterStyle BackColor="#4f81bc"/>
               <Columns>
               <asp:TemplateField HeaderText="Patient Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="75%" >
               <ItemTemplate>
                    <asp:LinkButton ID="btnGenrateStamp" runat="server" Text='<%# Eval("Patient")%>' CommandArgument='<%# Eval("Pat_id")%>' CommandName="Generate"></asp:LinkButton>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField HeaderText="Drug Count" DataField='Drugs' ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
 
                   </asp:BoundField>
               </Columns>
                         <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
               </asp:GridView>
           </ContentTemplate>
           </asp:UpdatePanel>
         </td>
       </tr>
       </table>
          </ContentTemplate>
          </asp:UpdatePanel>
       </td>
       
       
       <td valign="top" width="70%" >
       <asp:UpdatePanel ID="upGenStamp" runat="server">
       <ContentTemplate>
            <cc1:ModalPopupExtender ID="MPE_ChangeAddress" runat="server" TargetControlID="btn"  PopupControlID="pnlChangeAddress" CancelControlID="btnChangeCancel" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
            <cc1:ModalPopupExtender ID="MPE_FromAddress" runat="server" TargetControlID="btnFromedit"  PopupControlID="pnlFromAddress" CancelControlID="btnFromCancel" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
       <table width="100%">
       <tr class="medication_info_th1">
       <td colspan="6" align="center" valign="middle" height="25px">Postage Details</td>
       </tr>
       
       <tr class="medication_info_tr-odd">
       <td><asp:Label ID="lblFromAddress" runat="server" Text="Return Address"></asp:Label><font color="red">
           *</font>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnFromedit" runat="server"><asp:Image ID="imgEditFrom" ImageUrl="../images/edit.gif" ToolTip="Edit" runat="server" /></asp:LinkButton></td>       
       <td><asp:Label ID="lblToAddress" runat="server" Text="Delivery Address"></asp:Label><font color="red">
           *</font>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btn" runat="server"><asp:Image ID="Image1" ImageUrl="../images/edit.gif" ToolTip="Edit" runat="server" /></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton 
               ID="imgBtnChkAddr" ImageUrl="../images/check.gif" ToolTip="Check Address" 
               runat="server" onclick="imgBtnChkAddr_Click" /></td>
       <td  colspan="4"><asp:Label ID="lblRxDrugs" runat="server" Text="Rx Drugs"></asp:Label></td>
       </tr>
       <tr class="medication_info_tr-even">
       <td>
       <asp:TextBox ID="txtFromAddress" runat="server" TextMode="MultiLine" Rows="5" ReadOnly="true"  Width="175px"></asp:TextBox>
       </td>
       <td>
       <asp:TextBox ID="txtToAddress" runat="server" TextMode="MultiLine" Rows="5" ReadOnly="true"  Width="175px"></asp:TextBox>
       </td>       
       <td colspan="4" valign="top">
       <asp:UpdatePanel ID="upGridRx2" UpdateMode="Conditional" runat="server">
       <ContentTemplate>
       <asp:GridView ID="gridRx2" Width="100%" runat="server" OnRowDataBound="gridRx2_DataBound" AutoGenerateColumns="False" EmptyDataText="No Drugs...">
           <Columns>
            <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRxNum" runat="server" ToolTip='<%# Eval("RXNum")%>'  ></asp:CheckBox>
                            </ItemTemplate>
                       </asp:TemplateField>
                       <asp:BoundField HeaderText="Drugs"   DataField='Drugs' ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80%" />
                       <asp:BoundField HeaderText="Qty"     DataField='Qty' ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20%" />                      
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
           </asp:GridView></td>
           </ContentTemplate>
       </asp:UpdatePanel>
       </tr>
       <tr class="medication_info_tr-odd">
       <td >
        <asp:Label ID="lblPackageType" runat="server" Text="MailPiece"></asp:Label>
       </td>       
       <td>
           <asp:DropDownList ID="ddlPackageType" runat="server">
           <asp:ListItem Text="Package" Value="5"></asp:ListItem>
           <asp:ListItem Text="Large Package" Value="10"></asp:ListItem>
           <asp:ListItem Text="Oversized Package" Value="11"></asp:ListItem>
           <asp:ListItem Text="Flat Rate Box" Value="6"></asp:ListItem>
           <asp:ListItem Text="Small Flat Rate Box" Value="7"></asp:ListItem>
           <asp:ListItem Text="Large Flat Rate Box" Value="8"></asp:ListItem>
           <asp:ListItem Text="Flat Rate Envelope" Value="9"></asp:ListItem>
           <asp:ListItem Text="Large Envelope or Flat" Value="3"></asp:ListItem>
           <asp:ListItem Text="Thick Envelope" Value="4"></asp:ListItem>
           </asp:DropDownList>
       </td>
       
       <td><asp:Label ID="lblweight" runat="server" Text="Weight" ></asp:Label><font color="red">*</font></td>       
       <td  colspan="3"><asp:TextBox ID="txtlbsWeight" runat="server" Width="50px" Text="0.0"></asp:TextBox>
           &nbsp;
       <asp:Label ID="lbllbs" runat="server" Text="lbs." ></asp:Label>&nbsp;&nbsp;
       <asp:TextBox ID="txtozWeight" runat="server" Width="50px" Text="0.0"></asp:TextBox>&nbsp;
       <asp:Label ID="lbloz" runat="server" Text="oz." ></asp:Label>
       
       <cc1:FilteredTextBoxExtender ID="ftxtlbsWeight" TargetControlID="txtlbsWeight" runat="server" FilterMode="ValidChars" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
       <cc1:FilteredTextBoxExtender ID="ftxtozWeight" TargetControlID="txtozWeight" runat="server" FilterMode="ValidChars" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
       
       </td>
       </tr>
       <tr class="medication_info_tr-even">
       <td><asp:Label ID="lblServiceType" runat="server" Text="Mail Class"></asp:Label></td>       
       <td  colspan="5"  >
           <asp:RadioButtonList ID="rbtnServiceType" runat="server" RepeatColumns="5">
           <asp:ListItem Text="First-Class Mail" Value="1" Selected="True"></asp:ListItem>
           <asp:ListItem Text="Priority Mail" Value="2"></asp:ListItem>
           <asp:ListItem Text="Express Mail" Value="3"></asp:ListItem>
           <asp:ListItem Text="Parcel Post" Value="4"></asp:ListItem>
           <asp:ListItem Text="Media Mail" Value="5"></asp:ListItem>
           </asp:RadioButtonList>
       </td>
       </tr>
       <tr class="medication_info_tr-odd">
           
       
       <td ><asp:Label ID="lblAddOnType" runat="server" Text="e/Confirm"></asp:Label></td>       
       <td>
           <asp:DropDownList ID="ddlAddOnType" runat="server" onselectedindexchanged="ddlAddOnType_SelectedIndexChanged" AutoPostBack="True">
           <asp:ListItem Text="--Select--"></asp:ListItem>
           <asp:ListItem Text="Delivery Confirmation" Selected="True" Value="2"></asp:ListItem>
           <asp:ListItem Text="Signature Confirmation" Value="3"></asp:ListItem>
           </asp:DropDownList>
           
       </td>
       <td><asp:Label ID="lblDimensions" runat="server" Text="Dimensions"></asp:Label></td>       
           <td colspan="3"><asp:TextBox ID="txtLength" runat="server" Width="50px" Text="0"></asp:TextBox><asp:Label ID="lblLenght" runat="server" Text="&#34;L"></asp:Label>
               &nbsp;
           <asp:TextBox ID="txtWidth" runat="server" Width="50px" Text="0"></asp:TextBox><asp:Label ID="lblWidth" runat="server" Text="&#34;W"></asp:Label>
               &nbsp;
           <asp:TextBox ID="txtHeight" runat="server" Width="50px" Text="0"></asp:TextBox><asp:Label ID="lblHeight" runat="server" Text="&#34;H"></asp:Label>
               &nbsp;
           </td>
       </tr>
       <tr class="medication_info_tr-even">
       <td  ><asp:Label ID="lblShipDate" runat="server" Text="Mailing Date"></asp:Label><font color="red">
           *</font></td>       
       <td  colspan="2" >
       <asp:TextBox ID="txtShipDate" runat="server" width="90px"></asp:TextBox>
           
            <cc1:CalendarExtender ID="CE_ShipDate" runat="server" TargetControlID="txtShipDate" 
                                  PopupButtonID="txtShipDate" Enabled="True"></cc1:CalendarExtender>
           
       </td>
       
       <td  ><asp:Label ID="lblMessage" runat="server" Text="Print Message"></asp:Label></td>       
        <td  colspan="2" >
            <asp:TextBox ID="txtMessage" runat="server"></asp:TextBox> </td>
        </tr>
        <tr class="medication_info_tr-odd">
            <td ><asp:Label ID="lblRates" runat="server" Text="Rate"></asp:Label></td>       
            <td ><asp:Label ID="txtRate" runat="server" ></asp:Label></td>        
            <td colspan="2"><asp:Label ID="lblPostalBalance" runat="server" Text="Postal Balance"></asp:Label></td>       
            <td><asp:Label ID="lblPostalBalance1" runat="server" ></asp:Label></td>
            <td align="center" valign="middle">
              <asp:HyperLink ID="hplStamp" runat="server" Target="_blank">Stamp</asp:HyperLink>
            </td>        
        </tr>
        <tr class="medication_info_tr-even">
        <td colspan="6" align="center"  >
             <asp:LinkButton ID="btnTest" runat="server" onclick="btnTest_Click"><asp:Image ID="imgGenStamp" runat="server"  ImageUrl="../Images/search_new.png" ToolTip="Preview Stamp"/></asp:LinkButton>
             &nbsp;&nbsp;<asp:Image ID="Image3" runat="server"  ImageUrl="../Images/toolsep.gif" ToolTip="Preview Stamp"/>&nbsp;&nbsp;
             <asp:LinkButton ID="btnGenerate" runat="server"  onclick="btnGenerate_Click" OnClientClick="return StampGenerateValid();"><asp:Image ID="Image2" runat="server" ImageUrl="../images/genstamp.gif" ToolTip="Generate Stamp"/></asp:LinkButton></td>
        </tr>

        <tr class="medication_info_tr-odd">
        <td colspan="6"align="left"  >
            <asp:Label ID="lblReqFields" runat="server" Text="* indicates fields are mandatory." ForeColor="Red"></asp:Label>
        </td>
        </tr>
       </table>
           <asp:HiddenField ID="hidPatID" runat="server" />
           <asp:HiddenField ID="hidRxType" runat="server" />
           <asp:HiddenField ID="hidPharmID" runat="server" />
           <asp:HiddenField ID="hidRxDate" runat="server" />
           <asp:HiddenField ID="hidDocID" runat="server" />
      </ContentTemplate>
      </asp:UpdatePanel>
      
      <asp:UpdatePanel ID="upAddress" UpdateMode="Conditional"  runat="server">
      <ContentTemplate>
       <asp:Panel ID="pnlChangeAddress" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
                <table width="350"   border="0" cellpadding="0" cellspacing="0" >
                  <tr class="medication_info_th1" width="100%">
                    <td  height="30px" align="left">
                        &nbsp;&nbsp;Delivery Address 
                    </td>
                    <td align="right">
                      <asp:ImageButton ID="btnChangeCancel" runat="server"  ImageUrl="../images/CloseRx.gif" border="0"></asp:ImageButton>
                    </td>
                  </tr>
                  
                  <tr>
                    <td width="350" colspan="2" align="center">
                      <table align="center" class="medication_info_popup" cellpadding="0" cellspacing="1">
                      <tr >
                          <td align="left">
                            <asp:Label ID = "lblPatientName" Text = "Patient" runat = "server" ></asp:Label>
                          </td>
                          <td align="left">
                            <asp:Label ID = "lblPatientFName"  runat = "server" ></asp:Label>&nbsp;
                            <asp:Label ID = "lblPatientLName"  runat = "server" ></asp:Label>&nbsp;
                            <asp:Label ID = "lblPatientMName"  runat = "server" ></asp:Label>
                          </td>
                        </tr>
                        <tr >
                          <td align="left">
                            <asp:Label ID = "lblToAddress1" Text = "Address1" runat = "server" ></asp:Label>
                            <font color="red">*</font>
                          </td>
                          <td align="left">
                                <asp:TextBox ID = "txtToAddress1"  runat = "server" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr >
                          <td align="left">
                            <asp:Label ID = "lblToAddress2" Text = "Address2" runat = "server" ></asp:Label>
                          </td>
                          <td align="left">
                                <asp:TextBox ID = "txtToAddress2"  runat = "server" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblToCity" Text = "City" runat = "server" ></asp:Label><font color="red">
                              *</font>
                          </td>
                          <td align="left" height="25px">
                              <asp:TextBox ID="txtToCity" runat="server" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblToState" Text = "State" runat = "server" ></asp:Label><font color="red">
                              *</font>
                          </td>
                          <td align="left" height="25px">
                              <asp:TextBox ID="txtToState" runat="server" MaxLength="2" ></asp:TextBox>
                              <cc1:FilteredTextBoxExtender ID="ftxtToState" runat="server" FilterType="UppercaseLetters" TargetControlID="txtToState"></cc1:FilteredTextBoxExtender>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblToZip" Text = "Zip" runat = "server" ></asp:Label><font color="red">
                              *</font>
                          </td>
                          <td align="left" height="25px">
                              <asp:TextBox ID="txtToZip" runat="server" MaxLength="5" ></asp:TextBox>
                              <cc1:FilteredTextBoxExtender ID="ftxtToZip" runat="server" FilterType="Numbers" TargetControlID="txtToZip"></cc1:FilteredTextBoxExtender>
                          </td>
                        </tr>
                        
                         
                        <tr>
                        <td colspan="2" align="center">
                        
                         <asp:ImageButton ID="btn_Process_Save" runat="server"  ImageUrl="../images/save.gif" border="0" OnClick="btn_Process_Save_Click" ></asp:ImageButton>
                         </td>
                        </tr>
                        
                      </table>
                    </td>
                   
                  </tr>
                </table>
              </asp:Panel>
      
         
   
         <asp:Panel ID="pnlFromAddress" runat="server" BorderColor="Black" BorderWidth="1px"   BackColor="#FBFBFB" >
                <table width="350" align="left"  border="0" cellpadding="0" cellspacing="0">
                  <tr class="medication_info_th1" width="100%">
                    <td  height="30px" align="left">
                        &nbsp;&nbsp;From Address 
                    </td>
                    <td align="right">
                     <asp:ImageButton ID="btnFromCancel" runat="server"  ImageUrl="../images/CloseRx.gif" border="0"></asp:ImageButton>
                    </td>
                  </tr>
                  
                  <tr>
                    <td width="350" colspan="2" align="center">
                      <table align="center" class="medication_info_popup" cellpadding="0" cellspacing="1">
                      <tr >
                          <td align="left">
                            <asp:Label ID = "lblFromName" Text = "FirstName" runat = "server" ></asp:Label>
                          </td>
                          <td align="left">
                            <asp:TextBox ID = "txtFromFName"  runat = "server" ></asp:TextBox>&nbsp;
                          
                            
                            
                             
                          </td>
                        </tr>
                        <tr >
                          <td align="left">
                            <asp:Label ID = "Label1" Text = "MiddleName" runat = "server" ></asp:Label>
                          </td>
                          <td align="left">
                            <asp:TextBox ID = "txtFromMName"  runat = "server" ></asp:TextBox>
                             
                          </td>
                        </tr>
                        <tr >
                          <td align="left">
                            <asp:Label ID = "Label2" Text = "LastName" runat = "server" ></asp:Label>
                          </td>
                          <td align="left">
                              <asp:TextBox ID = "txtFromLName"  runat = "server" ></asp:TextBox>&nbsp;
                            
                             
                          </td>
                        </tr>
                        <tr >
                          <td align="left">
                            <asp:Label ID = "lblFromAddress1" Text = "Address1" runat = "server" ></asp:Label><font color="red">
                              *</font>
                          </td>
                          <td align="left">
                                <asp:TextBox ID = "txtFromAddress1"  runat = "server" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr >
                          <td align="left">
                            <asp:Label ID = "lblFromAddress2" Text = "Address2" runat = "server" ></asp:Label>
                          </td>
                          <td align="left">
                                <asp:TextBox ID = "txtFromAddress2"  runat = "server" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblFromCity" Text = "City" runat = "server" ></asp:Label><font color="red">
                              *</font>
                          </td>
                          <td align="left" height="25px">
                              <asp:TextBox ID="txtFromCity" runat="server" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblFromState" Text = "State" runat = "server" ></asp:Label><font color="red">
                              *</font>
                          </td>
                          <td align="left" height="25px">
                              <asp:TextBox ID="txtFromState" runat="server" MaxLength="2" ></asp:TextBox>
                              
                          </td>
                        </tr>
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblFromZip" Text = "Zip" runat = "server" ></asp:Label>
                            <font color="red">*</font>
                          </td>
                          <td align="left" height="25px">
                              <asp:TextBox ID="txtFromZip" runat="server" MaxLength="5" ></asp:TextBox>
                              <cc1:FilteredTextBoxExtender ID="ftxtFromZip" runat="server" FilterType="Numbers" TargetControlID="txtFromZip"></cc1:FilteredTextBoxExtender>
                          </td>
                        </tr>
                        
                          <tr>
                        <td colspan="2" align="center">
                        
                         <asp:ImageButton ID="btnUpdateFromAddress" runat="server"  ImageUrl="../images/save.gif" border="0" OnClick="btnUpdateFromAddress_Click" ></asp:ImageButton>
                         </td>
                        </tr>
                       
                        
                      </table>
                    </td>
                   
                  </tr>
                </table>
              </asp:Panel>
           </ContentTemplate>
       </asp:UpdatePanel>
       </td>
       </tr>
    </table>
</asp:Content>

