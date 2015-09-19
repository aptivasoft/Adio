<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:asp="remove" xmlns:cc1="remove">
 <xsl:output method="html" indent="yes"/>
  <xsl:template name="PatientInfo" match="/PatientInfo/Patient">
    <table border="0" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td width="100%" height="96" background="images/adio_Topbg.gif" align="center" valign="top">
          <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" height="97">
            <tr>
              <td width="148">
                <p align="center">
                  <img src="images/logo.gif" width="115" height="56" border="0" />
                </p>
              </td>
              <td width="85%" align="right" valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="396" background="images/adio_titleBbg.gif">
                  <tr>
                    <td width="23" background="images/adio_titleBLcor.gif" height="34">
                      
                    </td>
                    <td width="377">

                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td class="linkTitle" width="225" align="left"></td>
                          <td width="25" valign="top" align="right">
                            <p align="center">
                              <img src="images/home.gif" width="16" height="16" border="0" />
                            </p>
                          </td>
                          <td class="linkTitle" width="46" valign="middle" align="left">
                            <a href="#">Home</a>
                          </td>
                          <td width="24" valign="top" align="right">
                            <p align="center">
                              <img src="images/logout.gif" width="16" height="16" border="0" />
                            </p>
                          </td>
                          <td class="linkTitle" width="52" valign="middle" align="left">
                            <asp:LinkButton ID="lnkLogout" runat="server" Text="Logout"></asp:LinkButton>
                          </td>
                          <td class="linkTitle" width="225" align="left">
                            <asp:Label ID="lblRxDate" Font-Bold="true" runat="server"></asp:Label>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    
      <tr>
        <td width="100%" height="100%" background="images/adio_Contentbg.gif" align="center" valign="top">
          <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
              <td width="100%" align="left" valign="top" class="mainText">
                <div class="chromestyle" id="chromemenu">
                  <ul>
                    <li style="color:Gray">
                      eScript
                    </li>
                    <li>
                      <a href="Request.aspx">Message</a>
                    </li>
                    <li>
                      <a href="Reports.aspx">Reports</a>
                    </li>
                  </ul>
                </div> </td>
            </tr>
          <tr>
              <td width="100%" align="center" valign="top">
                <table align="center" border="0" cellpadding="0" cellspacing="10" width="966" height="70%">
                  <tr>
                    <td width="950" valign="top" height="40" colspan="2" align="center">
                      <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td width="8" height="35" background="images/adio_PanelLcor.gif">
                            <img src="images/adio_PanelLcor.gif" width="9" height="35" border="0" />
                          </td>
                          <td width="100%" height="35" background="images/adio_PanelTbg.gif" align="left">
                            <img src="images/Provider.gif" width="336" height="35" border="0" />
                          </td>
                          <td width="12" height="35" background="images/adio_PanelRcor.gif">
                            <img src="images/adio_PanelRcor.gif" width="12" height="35" border="0" />
                          </td>
                        </tr>
                        <tr>
                          <td width="8" background="images/adio_contentL.gif">&#160;</td>
                          <td width="100%" bgcolor="white" valign="top" align="left" height="40">
                            <asp:Panel id="pnlProviderInfo" runat="server">
                             <table class="mainText" cellspacing="10">
                                    <tr>
                                      <td align="left" colspan="2"  valign="top">
                                        <asp:Label ID = "lblProviderName" Text = "Provider Name:" runat = "server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID = "lblProviderName1"  runat = "server" ></asp:Label>
                                      </td>
                                      <td align="left" colspan="2" valign="top">
                                        <asp:ImageButton ID="btnChangeProvider" runat="server" ImageUrl="images/change.gif" ></asp:ImageButton>
                                      </td>
                                      <td align="left" colspan="2"  valign="top">
                                        <asp:Label ID = "lblLicNo" Text = "Licence No: " runat = "server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID = "lblLicNo1" Text = "1106 " runat = "server"></asp:Label>
                                     </td>
                                        <td align="left" colspan="2"  valign="top">
                                        <asp:Label ID = "lblPh" Text = "Phone: " runat = "server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID = "lblPh1" Text = "732-752-792" runat = "server"></asp:Label>
                                      </td>
                                      </tr>
                                  </table>
                            </asp:Panel>
                                </td>
                          <td width="12" background="images/adio_contentR.gif" height="59">&#160;</td>
                        </tr>
                        <tr>
                          <td width="8" height="7" background="images/adio_PaneBlLcor.gif"></td>
                          <td width="98%" height="7" background="images/adio_PaneBbgcor.gif"></td>
                          <td width="12" height="7" background="images/adio_PaneBlRcor.gif">
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td width="950" valign="top" height="68" colspan="2" align="center">
                      <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" height="102">
                        <tr>
                          <td width="8" height="35" background="images/adio_PanelLcor.gif">
                            <img src="images/adio_PanelLcor.gif" width="9" height="35" border="0" />
                          </td>
                          <td width="100%" height="35" background="images/adio_PanelTbg.gif" align="left">
                            <img src="images/Patient_Info.gif" width="336" height="35" border="0" />
                          </td>
                          <td width="12" height="35" background="images/adio_PanelRcor.gif">
                            <img src="images/adio_PanelRcor.gif" width="12" height="35" border="0" />
                          </td>
                        </tr>
                        <tr>
                          <td width="8" background="images/adio_contentL.gif" >&#160;</td>
                          <td  width="100%" bgcolor="white" valign="top">
                            <table class="mainText" border="0" cellpadding="0" cellspacing="10" width="100%">
                              <tr>
                                <td width="92" valign="middle">
                                  <p align="right">Search by Name</p>
                                </td>
                                <td width="783" valign="middle">
                                  <asp:TextBox ID = "txtSearchPatient" AutoCompleteType="Search" runat = "server" ></asp:TextBox>
                                </td>
                              </tr>
                              <tr>
                                <td width="100%"  valign="middle" colspan="2">
                                  <asp:Panel ID="pnlPatientInfo" width="100%"  runat="server" style="visibility:hidden;height:0px">
                                    <div id="divPatInfo" runat="server"  width="100%">
                                    <table width="100%" cellspacing="0">
                                      <tr>
                                        <td align="left"  colspan="2">
                                          <asp:Label ID = "lblPatientName" Text = "Patient Name:" runat = "server" Font-Bold="True"></asp:Label>
                                          <asp:Label ID = "lblPatientName1" Text = "" runat = "server"></asp:Label>
                                        </td>
                                        <td align="left"  colspan="2">
                                          <asp:Label ID = "lblDOB" Text = "Date Of Birth:" runat = "server" Font-Bold="True"></asp:Label>
                                          <asp:Label ID = "lblDOB1" Text = "" runat = "server" ></asp:Label>
                                        </td>
                                        <td align="left"  colspan="2">
                                          <asp:Label ID = "lblGender" Text = "Gender:" runat = "server" Font-Bold="True"></asp:Label>
                                          <asp:Label ID = "lblGender1" Text = "" runat = "server" ></asp:Label>
                                        </td>
                                        <td align="left"  colspan="2">
                                          <asp:Label ID = "lblAllergies" Text = "Allergies:" runat = "server" Font-Bold="True"></asp:Label>
                                          <asp:Label ID = "lblAllergies1" Text = "" runat = "server" ></asp:Label>
                                        </td>
                                      </tr>
                                    </table>
                                    </div>
                                    <div id="divPatInsurance" runat="server"  width="100%">
                                    <table width="100%" cellspacing="0">
                                      <tr>
                                        <td align="left"   colspan="2">
                                          <asp:Label ID = "lblHealthPlanID" Text = "Policy ID:" runat = "server" Font-Bold="True"></asp:Label>
                                          <asp:Label ID = "lblHealthPlan1" Text = "" runat = "server" ></asp:Label>
                                        </td>
                                        <td align="left"   colspan="2">
                                          <asp:Label ID = "lblHealthPlanName" Text = "Policy Name:" runat = "server" Font-Bold="True"></asp:Label>
                                          <asp:Label ID = "lblHealthPlanName1" Text = "" runat = "server" ></asp:Label>
                                        </td>
                                        <td align="left"   colspan="2">
                                          <asp:Label ID = "lblInsGroupNo" Text = "Group No:" runat = "server" Font-Bold="True"></asp:Label>
                                          <asp:Label ID = "lblInsGroupNo1" Text = "" runat = "server" ></asp:Label>
                                        </td>
                                        <td align="left"   colspan="2">
                                          <asp:Label ID = "lblInsBinNo" Text = "BIN No:" runat = "server" Font-Bold="True"></asp:Label>
                                          <asp:Label ID = "lblInsBinNo1" Text = "" runat = "server" ></asp:Label>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td align="left"   colspan="2">
                                          <asp:Label ID = "lblInsdName" Text = "Insured Name:" runat = "server" Font-Bold="True"></asp:Label>
                                          <asp:Label ID = "lblInsdName1" Text = "" runat = "server" ></asp:Label>
                                        </td>
                                        <td align="left"   colspan="2">
                                          <asp:Label ID = "lblInsdRel" Text = "Relationship:" runat = "server" Font-Bold="True"></asp:Label>
                                          <asp:Label ID = "lblInsdRel1" Text = "" runat = "server" ></asp:Label>
                                        </td>
                                        <td align="left"  colspan="2"></td>
                                        <td align="left"  colspan="2"></td>
                                      </tr>
                                    </table>
                                    </div>
                                  </asp:Panel>
                                </td>
                              </tr>
                            </table>
                          </td>
                          <td width="12" background="images/adio_contentR.gif" height="59">&#160;</td>
                        </tr>
                        <tr>
                          <td width="8" height="7" background="images/adio_PaneBlLcor.gif">

                          </td>
                          <td width="98%" height="7" background="images/adio_PaneBbgcor.gif"></td>
                          <td width="12" height="7" background="images/adio_PaneBlRcor.gif">

                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td width="950" height="67" colspan="2" align="center" valign="top">
                      <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td width="8" height="35" background="images/adio_PanelLcor.gif" align="left">
                          </td>
                          <td width="100%" height="35" background="images/adio_PanelTbg.gif" align="left">
                            <img src="images/Medication_info.gif" width="336" height="35" border="0" />
                          </td>
                          <td width="12" height="35" background="images/adio_PanelRcor.gif">
                            <img src="images/adio_PanelRcor.gif" width="12" height="35" border="0" />
                          </td>
                        </tr>
                        <tr>
                          <td width="8" background="images/adio_contentL.gif" height="40">&#160;</td>
                          <td class="mainText" width="100%" bgcolor="white" height="40" valign="top">
                            <table class="mainText" border="0" cellpadding="0" cellspacing="10" width="100%" height="30">
                                <tr>
                                  <td width="92" valign="middle" align="right">
                                    <p align="right">Search by Name</p>
                                  </td>
                                  <td  valign="middle" align="left">
                                    <asp:TextBox ID = "txtSearchMedication" AutoCompleteType="Search" runat = "server" ></asp:TextBox>
                                  </td>
                                <td width="566" valign="middle" align="left">
                                  <asp:UpdatePanel ID="updatePanelAddMed" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                      <asp:ImageButton ID="btnAddMed" runat="server" onClientClick="return validateMed()" ImageUrl="images/add.gif"/>
                                    </ContentTemplate>
                                  </asp:UpdatePanel>
                                      <asp:ImageButton ID="btnAddMed1" runat="server" Style="display: none" ImageUrl="images/add.gif"/>
                                  <asp:ImageButton ID="btnPreviewRx1" runat="server" Style="display: none" ImageUrl="images/add.gif"/>
                                </td>
                              </tr>
                              <tr>
                                <td width="886" valign="top" colspan="3">
                                  <div id="divMedications" runat="server" style="overflow-x:hidden; ">
                                  <table width="100%" class="mainText">
                                    <tr>
                                      <td width="80%">
                                        <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
                                          <ContentTemplate>
                                            <asp:DataList id="medList"  RepeatLayout="Table" RepeatDirection="Vertical" RepeatColumns="2" runat="server" CellSpacing="10">
                                              <ItemTemplate>
                                                <table width="100%" CellPadding="0" CellSpacing="0" border="1px">
                                                  <tr>
                                                    <td colspan="4">
                                                      <asp:Label runat="server" ID="lblDrugNameT" Text="Medicine Name:" Font-Bold="true"></asp:Label>
                                                      <asp:Label runat="server" ID="lblDrugName" Font-Bold="true" Text="&lt;%# Bind('MedicineName') %&gt;" ></asp:Label>
                                                    </td>
                                                   </tr>
                                                  <tr>
                                                    <td colspan="2">
                                                      <asp:Label runat="server" ID="lblStrengthT" Text="Strength/Form:" ></asp:Label>
                                                      <asp:Label runat="server" ID="lblStrength" Text="&lt;%# Bind('Strength') %&gt;" ></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                      <asp:Label runat="server" ID="lblDrugQtyT" Text="Quantity:" ></asp:Label>
                                                      <asp:Label runat="server" ID="lblDrugQty" Text="&lt;%# Bind('Quantity') %&gt;" ></asp:Label>
                                                    </td>
                                                    </tr>
                                                  <tr>
                                                    <td colspan="2">
                                                      <asp:Label runat="server" ID="lblDrugTypeT" Text="Drug Type:" ></asp:Label>
                                                      <asp:Label runat="server" ID="lblDrugType" Text="&lt;%# Bind('DrugType') %&gt;" ></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                      <asp:Label runat="server" ID="lblRefillsT" Text="Refills:" ></asp:Label>
                                                      <asp:Label runat="server" ID="lblRefills" Text="&lt;%# Bind('Refills') %&gt;" ></asp:Label>
                                                    </td>
                                                    </tr>
                                                  <tr>
                                                    <td colspan="4">
                                                      <asp:Label runat="server" ID="lblDrugSIGT" Text="SIG:" ></asp:Label>
                                                      <asp:Label runat="server" ID="lblSIG" Text="&lt;%# Bind('SIG') %&gt;" ></asp:Label>
                                                    </td>
                                                    </tr>
                                                  <tr>
                                                    <td colspan="4">
                                                    <asp:Label runat="server" ID="lblNote2PharmacistT" Text="Note To Pharmacist:" ></asp:Label>
                                                    <asp:Label runat="server" ID="lblNote2Pharmacist" Text="&lt;%# Bind('NoteToPhamacist') %&gt;" ></asp:Label>
                                                    </td>
                                                  </tr>
                                                </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                          </ContentTemplate>
                                        </asp:UpdatePanel>
                                      </td>
                                      <td widht="20%" valign="top">
                                        <div id="DrugFormDiv" valign="top" with="100%" cellspacing="0"></div>
                                      </td>
                                    </tr>
                                  </table>
                                  </div>
                                </td>
                              </tr>
                            </table>
                          </td>
                          <td width="12" background="images/adio_contentR.gif" height="40">&#160;</td>
                        </tr>
                        <tr>
                          <td width="8" height="7" background="images/adio_PaneBlLcor.gif">
                          </td>
                          <td width="98%" height="7" background="images/adio_PaneBbgcor.gif"></td>
                          <td width="12" height="7" background="images/adio_PaneBlRcor.gif">
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td width="391" height="69" align="center" valign="top">
                      <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td width="8" height="35" background="images/adio_PanelLcor.gif">
                            <img src="images/adio_PanelLcor.gif" width="9" height="35" border="0" />
                          </td>
                          <td width="100%" height="35" background="images/adio_PanelTbg.gif" align="left">
                            <img src="images/Pharmacy_Info.gif" width="336" height="35" border="0" />
                          </td>
                          <td width="12" height="35" background="images/adio_PanelRcor.gif">
                            <img src="images/adio_PanelRcor.gif" width="12" height="35" border="0" />
                          </td>
                        </tr>
                        <tr>
                          <td width="8" background="images/adio_contentL.gif" height="40">&#160;</td>
                          <td class="mainText" width="100%" bgcolor="white" height="40" valign="top">
                            <asp:Panel ID="pnlPharmacyInfo" width="100%"  runat="server" style="visibility:hidden;height:10px">
                            <table width="100%">
                              <tr>
                                <td align="left" colspan="2">
                                  <asp:Label ID = "lblPharmacyName" Text = "Pharmacy Name:" runat = "server" Font-Bold="true"></asp:Label>
                                  <asp:Label ID = "lblPatPharmacyName1"  runat = "server" ></asp:Label>
                                </td>
                                <td></td>
                                <td  align="right" >
                                  <asp:ImageButton ID="btnChangePharm" runat="server" ImageUrl="images/change.gif" ></asp:ImageButton>
                                </td>
                              </tr>
                              <tr>
                                <td  align="left" colspan="3">
                                  <asp:Label ID = "lblAddr1" Text = "Address1:" runat = "server" Font-Bold="true"></asp:Label>
                                  <asp:Label ID = "lblAddr11" Text = "" runat = "server" ></asp:Label>
                                </td>
                              </tr>
                              <tr>
                                <td  align="left" colspan="3">
                                  <asp:Label ID = "lblAddr2" Text = "Address2:" runat = "server" Font-Bold="true"></asp:Label>
                                  <asp:Label ID = "lblAddr21" Text = "" runat = "server" ></asp:Label>
                                </td>
                              </tr>
                              <tr>
                                <td  align="left" colspan="2">
                                  <asp:Label ID = "lblCity" Text = "City:" runat = "server" Font-Bold="true"></asp:Label>
                                  <asp:Label ID = "lblCity1" Text = "" runat = "server" ></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                  <asp:Label ID = "lblState" Text = "State:" runat = "server" Font-Bold="true"></asp:Label>
                                  <asp:Label ID = "lblState1" Text = "" runat = "server" ></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                  <asp:Label ID = "lblZip" Text = "Zip:" runat = "server" Font-Bold="true"></asp:Label>
                                  <asp:Label ID = "lblZip1" Text = "" runat = "server" ></asp:Label>
                                </td>
                              </tr>
                              <tr>
                                <td  align="left" colspan="2">
                                  <asp:Label ID = "lblPhone" Text = "Phone:" runat = "server" Font-Bold="true"></asp:Label>
                                  <asp:Label ID = "lblPhone1" Text = "" runat = "server" ></asp:Label>
                                </td>
                                <td align="left" colspan="2">
                                  <asp:Label ID = "lblFax" Text = "Fax:" runat = "server" Font-Bold="true"></asp:Label>
                                  <asp:Label ID = "lblFax1" Text = "" runat = "server" ></asp:Label>
                                </td>
                                
                              </tr>
                            </table>
                            </asp:Panel>
                          </td>
                          <td width="12" background="images/adio_contentR.gif" height="40">&#160;</td>
                        </tr>
                        <tr>
                          <td width="8" height="7" background="images/adio_PaneBlLcor.gif">

                          </td>
                          <td width="98%" height="7" background="images/adio_PaneBbgcor.gif"></td>
                          <td width="12" height="8" background="images/adio_PaneBlRcor.gif">

                          </td>
                        </tr>
                      </table>
                    </td>
                    <td width="551" height="69" align="center" valign="top">
                      <table class="mainText" align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td width="9" height="35" background="images/adio_PanelLcor.gif">
                            <img src="images/adio_PanelLcor.gif" width="9" height="35" border="0" />
                          </td>
                          <td width="100%" height="35" background="images/adio_PanelTbg.gif" align="left">
                            <img src="images/CM_Info.gif" width="336" height="35" border="0" />
                          </td>
                          <td width="12" height="35" background="images/adio_PanelRcor.gif">
                            <img src="images/adio_PanelRcor.gif" width="12" height="35" border="0" />
                          </td>
                        </tr>
                        <tr>
                          <td width="8" background="images/adio_contentL.gif" height="40">&#160;</td>
                          <td width="100%" bgcolor="white" height="40" align="center" valign="top">
                            <div id="divCM" style="height:10px;width:100%;">
                              <asp:UpdatePanel ID="updateGetMed" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                  <asp:Button ID="btnGetCM" runat="server" style="visibility:hidden;"/>
                                </ContentTemplate>
                              </asp:UpdatePanel>
                              <asp:UpdatePanel ID="updateCMGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                  <asp:GridView ID="grdCurrMed" runat="server" AutoGenerateColumns="False" CSSClass="tabulardata">
                                    <Columns>
                                      <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                          <asp:Label ID="lblRxItemID" runat="server"  Text='&lt;%# Bind("Rx_ItemID") %&gt;'></asp:Label>
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField  HeaderText="Medicine">
                                        <ItemTemplate>
                                          <asp:Label ID="lblDrugName" runat="server" Text='&lt;%# Bind("Rx_DrugName") %&gt;'></asp:Label>
                                        </ItemTemplate>
                                      </asp:TemplateField>

                                      <asp:TemplateField  HeaderText="Rx Date">
                                        <ItemTemplate>
                                          <asp:Label ID="lblRxDate" runat="server" Text='&lt;%# Bind("RxDate") %&gt;'></asp:Label>
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Dosage">
                                        <EditItemTemplate>
                                          <asp:TextBox ID="txtEditDosage"  runat="server" Text='&lt;%# Bind("Rx_Dosage") %&gt;'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                          <asp:Label ID="lblDosage" runat="server" Text='&lt;%# Bind("Rx_Dosage") %&gt;'></asp:Label>
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="SIG">
                                        <EditItemTemplate>
                                          <asp:TextBox ID="txtEditSIG"  runat="server" Text='&lt;%# Bind("Rx_SIG") %&gt;'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                          <asp:Label ID="lblSig" runat="server" Text='&lt;%# Bind("Rx_SIG") %&gt;'></asp:Label>
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField  HeaderText="Refills">
                                        <EditItemTemplate>
                                          <asp:DropDownList ID="ddlEditRxRefill" runat="server">
                                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                          </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                          <asp:Label ID="Label1" runat="server" CSSClass="textAlign" Text='&lt;%# Bind("Rx_Refills") %&gt;'></asp:Label>
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="EDIT" ShowHeader="False">
                                        <EditItemTemplate>
                                          <asp:LinkButton ID="lnkUpdateDrug" runat="server" CausesValidation="True" CommandName="Update"
                                              Text="Update"></asp:LinkButton>
                                          <asp:LinkButton ID="lnkCancelDrug" runat="server" CausesValidation="False" CommandName="Cancel"
                                              Text="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkEditDrug" runat="server" CausesValidation="False" CommandName="Edit"
                                              Text="Renew"></asp:LinkButton>
                                          <asp:LinkButton ID="lnkDeleteDrug" runat="server" CausesValidation="False" CommandName="Delete"
                                              Text="Cancel"></asp:LinkButton>
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                    </Columns>
                                  </asp:GridView>
                                </ContentTemplate>
                              </asp:UpdatePanel>
                            </div>
                            
                          </td>
                          <td width="12" background="images/adio_contentR.gif" height="40">&#160;</td>
                        </tr>
                        <tr>
                          <td width="8" height="7" background="images/adio_PaneBlLcor.gif">

                          </td>
                          <td width="98%" height="7" background="images/adio_PaneBbgcor.gif"></td>
                          <td width="12" height="8" background="images/adio_PaneBlRcor.gif">

                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td>
                    <table class="mainText">
                      <tr>
                        <td>
                          <asp:Panel ID="pnlAddMedication" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB">
                            <table width="350" height="37" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="13">
                                  <img src="images/popup_window_01.jpg" width="13" height="37" alt=""/>
                                </td>
                                <td width="350">
                                  <img src="images/popup_window_02.jpg" width="380" height="37" alt=""/>
                                </td>
                                <td width="16">

                                </td>
                                <td width="10">
                                  <img src="images/popup_window_04.jpg" width="13" height="37" alt=""/>
                                </td>
                              </tr>
                              <tr>
                                <td width="13"></td>
                                <td width="350" colspan="2">
                                  <table aligin="center">
                                    <tr>
                                      <td colspan="2" align="left" height="25px">
                                        <asp:UpdatePanel ID="updatePanelMedName" runat="server" >
                                          <ContentTemplate>
                                            <asp:Label ID = "Label12" Text = "Medication Name:" runat = "server" ></asp:Label>
                                            <asp:Label ID = "lblMedicationName"  runat = "server" ></asp:Label>
                                          </ContentTemplate>
                                        </asp:UpdatePanel>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td align="left" height="25px">
                                        <asp:Label ID = "Label13" Text = "Strength / Form:" runat = "server" ></asp:Label>
                                      </td>
                                      <td align="right" height="25px">
                                        <asp:UpdatePanel ID="updatePanelStrengths" runat="server" >
                                          <ContentTemplate>
                                            <asp:DropDownList ID="ddlStrength" runat="server" Width="185px"></asp:DropDownList>
                                          </ContentTemplate>
                                        </asp:UpdatePanel>
                                      </td>
                                    </tr>
                                    <tr bgcolor="#e2edf5">
                                      <td>
                                        <asp:Label ID="lblDrugCatg" runat="server" Text="Drug Category:"></asp:Label>
                                      </td>
                                      <td colspan="2">
                                        <asp:UpdatePanel ID="updateDrugCategory" runat="server">
                                          <ContentTemplate>
                                            <asp:Label ID="lblDrugCatg1" runat="server" ></asp:Label>
                                          </ContentTemplate>
                                        </asp:UpdatePanel>
                                      </td>
                                    </tr>
                                    <tr bgcolor="#e2edf5">
                                      <td colspan="3">
                                        <asp:UpdatePanel ID="updateDrugCategory1" runat="server">
                                          <ContentTemplate>
                                            <asp:Label ID="lblDrugCatgNote" runat="server" visible="false" ForeColor="Red"></asp:Label>
                                          </ContentTemplate>
                                        </asp:UpdatePanel>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td align="left" height="25px">
                                        <asp:Label ID = "Label14" Text = "Quantity:" runat = "server" ></asp:Label>
                                      </td>
                                      <td align="right" height="25px">
                                        <asp:TextBox ID="txtQuantity" onkeypress="return isNumberKey(event)" runat="server" Width="50px"></asp:TextBox>
                                        <asp:DropDownList ID="ddlMedType" runat="server" Width="125px">
                                          <asp:ListItem Text="Tablet(s)"></asp:ListItem>
                                          <asp:ListItem Text="Capsule(s)"></asp:ListItem>
                                          <asp:ListItem Text="Syrup"></asp:ListItem>
                                          <asp:ListItem Text="Injection"></asp:ListItem>
                                        </asp:DropDownList>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td  align="left" height="25px">
                                        <asp:Label ID = "Label15" Text = "SIG:" runat = "server" ></asp:Label>
                                      </td>
                                      <td align="right" height="25px">
                                        <asp:TextBox ID="txtSIG" runat="server" Width="180px"></asp:TextBox>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td align="left" height="25px">
                                        <asp:Label ID = "Label16" Text = "Refills:" runat = "server" ></asp:Label>
                                      </td>
                                      <td align="right" height="25px">
                                        <asp:DropDownList ID="ddlRefills" runat="server" Width="185px">
                                          <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                          <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                          <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                          <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                          <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td align="left" height="25px">
                                        <asp:Label ID = "Label17" Text = "Note to Pharmacist:" runat = "server" ></asp:Label>
                                      </td>
                                      <td align="right" height="25px">
                                        <asp:TextBox ID="txtNote" runat="server" TextMode="multiLine" Width="180px"></asp:TextBox>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td  align="right" height="25px">
                                        <asp:RadioButton ID="rbtNewRx" GroupName="rbtRx" Checked="true" runat="server" Text="New Rx" />
                                        <asp:RadioButton ID="rbtPAPRx" GroupName="rbtRx" runat="server" Text="PAP" />
                                      </td>
                                      <td  align="right" height="25px">
                                        <asp:RadioButton ID="rbtSampleRx" GroupName="rbtRx" runat="server" Text="Sample" />
                                        <asp:RadioButton ID="rbtRenewalRx" GroupName="rbtRx" runat="server" Text="Renewal" />
                                      </td>
                                      <td></td>
                                    </tr>
                                    <tr>
                                      <td  align="left" height="25px">
                                        <asp:CheckBox ID="chkAllowSub" runat="server"  Text="Dispense As Written"/>
                                      </td>
                                     
                                    </tr>
                                    <tr>
                                      <td  align="center" height="25px">
                                        <asp:UpdatePanel ID="updatePanel2" runat="server" >
                                          <ContentTemplate>
                                            <asp:ImageButton ID="btnSave" runat="server"  ImageUrl="images/save.gif" border="0" OnClientClick="clearSearchMedText()"></asp:ImageButton>
                                          </ContentTemplate>
                                        </asp:UpdatePanel>
                                      </td>
                                      <td>
                                        <asp:ImageButton ID="btnCancel" runat="server"  ImageUrl="images/cancelPopUp.gif" border="0"></asp:ImageButton>
                                      </td>
                                    </tr>
                                  </table>
                                </td>
                                <td>
                                  <asp:UpdatePanel ID="updateHidMedRx" runat="server" >
                                    <ContentTemplate>
                                      <input type="hidden" id="hidMedRx" runat="server"/>
                                    </ContentTemplate>
                                  </asp:UpdatePanel>
                                </td>
                              </tr>
                            </table>
                          </asp:Panel>
                        </td>
                      </tr>

                      <tr>
                        <td>
                          <asp:Panel ID="pnlPreviewRx" width="500px" runat="server"  BackColor="#FBFBFB" BorderWidth="1px" BorderColor="Black">
                            <!--<asp:UpdatePanel ID="updatePreviewRx" runat="server" >
                              <ContentTemplate>
                                <asp:Literal ID="litPreviewRx" runat="server"></asp:Literal>
                              </ContentTemplate>
                            </asp:UpdatePanel>-->
                            <table align="center" width="490px">
                              <tr>
                                <td align="right">
                                  <asp:ImageButton ID="btnClose" runat="server"  ImageUrl="images/CloseRx.gif" border="0" ToolTip="Close"></asp:ImageButton>
                                </td>
                              </tr>
                              <tr>
                                <td align="center" style="width:100%">
                                  <div id="divPreviewRx"></div>
                                </td>
                              </tr>
                            </table>
                            </asp:Panel>
                        </td>
                      </tr>

                      <tr>
                        <td>
                          <asp:Panel ID="pnlRxStatus" runat="server"  BackColor="#FBFBFB" BorderWidth="1px" BorderColor="Black">
                            <table width="300" height="60">
                              <tr>
                                <td align="center" colspan="2">
                                      <asp:UpdatePanel ID="updatePatientRx" runat="server">
                                        <ContentTemplate>
                                          <asp:Label ID="lblPatientRxStatus"  runat="server" Font-Bold="true"></asp:Label>
                                        </ContentTemplate>
                                      </asp:UpdatePanel>
                                 </td>
                              </tr>
                                <tr>
                                  <td align="center" colspan="2">
                                    <asp:ImageButton ID="btnOkClear" runat="server" ImageUrl="images/Ok.jpg" border="0"></asp:ImageButton>
                                  </td>
                                </tr>
                            </table>
                           </asp:Panel>
                        </td>
                      </tr>

                      <tr>
                        <td>
                          <asp:Panel ID="pnlChangePharmacy" runat="server"  BackColor="#FBFBFB" BorderWidth="1px" BorderColor="Black">
                            <table width="380">
                              <tr>
                                <td width="13"></td>
                                <td colspan="2">
                                  <table>
                                    <tr>
                                      <td align="left">
                                        <asp:Label ID = "lblSearchPharm" Text = "Search Pharmacy:" runat = "server" ></asp:Label>
                                      </td>
                                      <td align="right" colspan="2">
                                        <asp:TextBox ID="txtSearchPharmacy" runat="server"  Width="175px"></asp:TextBox>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td  align="left" colspan="2">
                                        <asp:UpdatePanel ID="updatePh" UpdateMode="Conditional" runat="server" >
                                          <ContentTemplate>
                                            <asp:RadioButtonList ID="rbtPhGroup" RepeatDirection="Horizontal" AutoPostBack="True" runat="server">
                                              <asp:ListItem Text="Name" Value="0" Selected="True" onclick ="getIndex(0);"/>
                                              <asp:ListItem Text="Zip" Value="1" onclick ="getIndex(1);"/>
                                            </asp:RadioButtonList>
                                          </ContentTemplate>
                                        </asp:UpdatePanel>
                                            </td>
                                    </tr>
                                    <tr>
                                      <td  align="left" colspan="3">
                                        <asp:Label ID = "lblPharmAddr1" Text = "Address1:" runat = "server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID = "lblPharmAddr11" Text = "" runat = "server" ></asp:Label>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td  align="left" colspan="3">
                                        <asp:Label ID = "lblPharmAddr2" Text = "Address2:" runat = "server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID = "lblPharmAddr21" Text = "" runat = "server" ></asp:Label>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td  align="left">
                                        <asp:Label ID = "lblPharmCity" Text = "City:" runat = "server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID = "lblPharmCity1" Text = "" runat = "server" ></asp:Label>
                                      </td>
                                      <td align="left">
                                        <asp:Label ID = "lblPharmState" Text = "State:" runat = "server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID = "lblPharmState1" Text = "" runat = "server" ></asp:Label>
                                      </td>
                                      <td align="left">
                                        <asp:Label ID = "lblPharmZip" Text = "Zip:" runat = "server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID = "lblPharmZip1" Text = "" runat = "server" ></asp:Label>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td  align="left" colspan="2">
                                        <asp:Label ID = "lblPharmPhone" Text = "Phone:" runat = "server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID = "lblPharmPhone1" Text = "" runat = "server" ></asp:Label>
                                      </td>
                                      <td align="left" colspan="2">
                                        <asp:Label ID = "lblPharmFax" Text = "Fax:" runat = "server" Font-Bold="true"></asp:Label>
                                        <asp:Label ID = "lblPharmFax1" Text = "" runat = "server" ></asp:Label>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td colspan="3" align="center">
                                        <asp:ImageButton ID="btnSavePharm" runat="server"  ImageUrl="images/save.gif" border="0"></asp:ImageButton>
                                        <asp:ImageButton ID="btnCancelPharm" runat="server"  ImageUrl="images/cancelPopUp.gif" border="0"></asp:ImageButton>
                                      </td>
                                    </tr>
                                  </table>
                                </td>
                              </tr>
                            </table>
                          </asp:Panel>
                        </td>
                      </tr>

                      <tr>
                        <td valign="top">
                          <asp:Panel ID="pnlChangeProvider" runat="server"  BackColor="#FBFBFB" BorderWidth="1px" BorderColor="Black">
                            <table width="325" valign="top">
                              <tr>
                                <td colspan="2">
                                  <table>
                                    <tr>
                                      <td align="left">
                                        <asp:Label ID = "lblSearchProvider" Text = "Search By Name:" runat = "server" ></asp:Label>
                                      </td>
                                      <td align="right">
                                        <asp:TextBox ID="txtSearchProvider" runat="server" Width="175px"></asp:TextBox>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td colspan="2" align="center">
                                        <asp:ImageButton ID="btnSaveProvider" runat="server"  ImageUrl="images/save.gif" border="0"></asp:ImageButton>
                                        <asp:ImageButton ID="btnCancelProvider" runat="server"  ImageUrl="images/cancelPopUp.gif" border="0"></asp:ImageButton>
                                      </td>
                                    </tr>
                                  </table>
                                </td>
                              </tr>
                            </table>
                          </asp:Panel>
                        </td>
                      </tr>
                      <tr>
                        <td width="34%"  colspan="2" >
                          <input type="hidden" id="hidPatID" runat="server"/>
                          <input type="hidden" id="hidPIID" runat="server"/>
                          <input type="hidden" id="hidPhrmID" runat="server"/>
                          <input type="hidden" id="hidDocID" runat="server"/>
                          <input type="hidden" id="hidMedID" runat="server"/>
                          <input type="hidden" id="hidLicNo" runat="server"/>
                          <input type="hidden" id="hidWphNo" runat="server"/>
                          <input type="hidden" id="hidtxtKey" runat="server"/>
                          <input type="hidden" id="hidprDeg" runat="server"/>
                          <input type="hidden" id="hidprDeaNo" runat="server"/>
                          <input type="hidden" id="hidPatPharmName" runat="server"/>
                          <input type="hidden" id="hidMedName" runat="server"/>
                          <input type="hidden" id="hidMedCount" runat="server"/>
                        </td>
                      </tr>
                    </table>
                    </td>
                  </tr>
                  <tr>
                    <td width="926"  align="center" valign="top" colspan="2" height="1px">
                      <asp:Panel id="pnlRxButtons" runat="server" style="visibility:hidden;height:0px">
                      <table class="mainText" border="0" cellpadding="0" cellspacing="0" width="60%" height="100%" align="center">
                        <tr>
                          <td width="25" align="center" valign="middle">
                            <asp:UpdatePanel ID="updatePreviewRx1" runat="server" UpdateMode="Conditional">
                              <ContentTemplate>
                                <asp:ImageButton ID="btnPreviewRx" runat="server" onClientClick="showPreview()"  ImageUrl="images/preview.gif" border="0" Enabled="false"></asp:ImageButton>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                                </td>
                          <td width="25" align="center" valign="middle">
                            <asp:UpdatePanel ID="updateSendRx" runat="server" UpdateMode="Conditional">
                              <ContentTemplate>
                                <asp:ImageButton ID="btnSendRx" runat="server"  ImageUrl="images/sendrx.gif" border="0" Enabled="false"></asp:ImageButton>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                          </td>
                          <td width="25" align="center" valign="middle">
                            <asp:UpdatePanel ID="updatePrintRx1" runat="server" UpdateMode="Conditional">
                              <ContentTemplate>
                                <asp:ImageButton ID="btnPrintRx" runat="server" onClientClick="printRx()"  ImageUrl="images/printrx.gif" border="0" Enabled="false"></asp:ImageButton>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                          </td>
                          <td width="25" align="center" valign="middle">
                            <asp:UpdatePanel ID="updateCancelRx" runat="server" UpdateMode="Conditional">
                              <ContentTemplate>
                                <asp:ImageButton ID="btnCancelRx" runat="server" onClientClick="ClearScreen()"  ImageUrl="images/cancelRx.gif" border="0"></asp:ImageButton>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                          </td>
                        </tr>
                      </table>
                      </asp:Panel>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td class="bottomText" width="100%" height="30" background="images/adio_bottombg.gif">© Copyright 2009. ADiO Health Management Solutions. All Rights Reserved. &#160;</td>
      </tr>
      </table>
 
  </xsl:template>
</xsl:stylesheet> 
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
 

