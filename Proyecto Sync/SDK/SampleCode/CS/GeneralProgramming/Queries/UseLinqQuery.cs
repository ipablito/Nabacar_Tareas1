
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml;


using System.Data;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Data.OleDb;

namespace Microsoft.Crm.Sdk.Samples
{
 /// <summary>
 /// Demonstrates how to make LINQ queries with the OrganizationServiceContext
 /// object and strongly-typed entities.</summary>
 /// <remarks>
 /// At run-time, you will be given the option to delete all the
 /// database records created by this program.</remarks>
 public class UseLinqQuery
 {
  #region Class Level Members

  private List<Guid> _accountIds = new List<Guid>();
  private List<Guid> _contactIds = new List<Guid>();
  private List<Guid> _leadIds = new List<Guid>();
  private List<Guid> _taskIds = new List<Guid>();
  private OrganizationServiceProxy _serviceProxy;
  private IOrganizationService _service;
  #endregion Class Level Members






  #region Sync

  int buscarFeedSource(string label)
  {
      int va = 0;
      if (label == "HomenetFeedUser")
          va = 808840000;
      else if (label == "AutoRevoFeedUser")
          va = 808840001;
      else if (label == "DSFeedUser")
          va = 808840002;
      else if (label == "AultecFeedUser")
          va = 808840003;
      else if (label == "CarsForSaleUser")
          va = 808840004;
      else if (label == "DealerCarSearchUser")
          va = 808840005;
      else //if (label == "FirstLookFeedUser")
          va = 808840006;

      return va;
  }

  int buscarDealerType(string dt)
  {
      int va = 0;
      if (dt == "New")
          va = 808840000;
      else if (dt == "Used")
          va = 808840001;
      else// if (dt == "0")
          va = 808840002;
      return va;
  }

  int buscarDisplayStatus(string txt)
  {
      int va = 0;
      if (txt == "On")
          va = 808840000;
      else //(txt == "Off")
          va = 808840001;
      return va;
  }




  string buscardealertypevuelta(int dt)
  {
      string va = "";
      if (dt ==808840000 )
          va = "New";

      else if (dt == 808840001)
          va = "Used";
      else //(dt == 808840002)
          va = "0";
      return va;
  }

  string buscaraccounttypevuelta(int id)
  {
      string r = "";
      if (id == 808840002)
          r = "Data Company";
      
      else if (id == 808840001)
          r = "Private Seller";
      else //(id == 808840000)
          r = "Dealer";
      return r;
  }


  string buscaraccountstatusvuelta(int id)
  {
      string r = "";
      if (id == 808840000)
          r = "Suspect";
      else if (id == 808840001)
          r = "Interested";
      else if (id == 808840002)
          r = "Not Interested";
      
      else if (id == 808840004)
          r = "Client";
      else if (id == 808840005)
          r = "Former";
      else if (id == 808840006)
          r = "Feeds";
      else if (id == 808840007)
          r = "Resource";
      else if (id == 808840008)
          r = "Admin";
      else //(id == 808840003)
          r = "N/A";
      return r;
  }


  string buscarprimarycontact(string gc)
  {
      string ca="";
      Guid ng = new Guid(gc);
      CrmEarlyBound.CrmServiceContext svcContext = new CrmEarlyBound.CrmServiceContext(_service);
      var con = from c in svcContext.ContactSet
                where c.ContactId.Value == ng
                select new CrmEarlyBound.Contact
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName
                };
      if (con.ToList().Count != 0)
          ca = con.ToList()[0].FirstName + "  " + con.ToList()[0].LastName;
      return ca;
  }

  public void Sync(ServerConnection.Configuration serverConfig, bool promptForDelete)
  {
      DateTime inicio = DateTime.Now;
      DateTime fin;
      DataSet_DATOSTableAdapters.IngresoHeaderTableAdapter LogE = new DataSet_DATOSTableAdapters.IngresoHeaderTableAdapter();
      DataSet_DATOSTableAdapters.IngresoDetailTableAdapter LogD = new DataSet_DATOSTableAdapters.IngresoDetailTableAdapter();

      int id_temp_header = Convert.ToInt32(LogE.GetData_ingresoHeader(inicio + "   Begins sync").Rows[0][0]);
      System.Console.WriteLine(inicio.ToString() + "   Begins sync, sync #" + id_temp_header.ToString());



      try
      {
          using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig))
          {
              _serviceProxy.EnableProxyTypes();

              _service = (IOrganizationService)_serviceProxy;


              CrmEarlyBound.CrmServiceContext svcContext = new CrmEarlyBound.CrmServiceContext(_service);

              var LeadsCrm = from a in svcContext.AccountSet
                             select new CrmEarlyBound.Account
                             {
                                 //DNN->CRM
                                 AccountId = a.AccountId,
                                 nac_FeedSource = a.nac_FeedSource,
                                 nac_DealerFeed = a.nac_DealerFeed,
                                 nac_DealerFeedStartDate = a.nac_DealerFeedStartDate,
                                 nac_DealerFeedEndDate = a.nac_DealerFeedEndDate,
                                 nac_DealerType = a.nac_DealerType,
                                 //Dealership (es el q esta abajo)
                                 nac_FTPDealerName=a.nac_FTPDealerName,
                                 nac_FTPAddress1 = a.nac_FTPAddress1,
                                 nac_FTPCity = a.nac_FTPCity,
                                 nac_FTPState = a.nac_FTPState,
                                 nac_Zip5 = a.nac_Zip5,
                                 nac_FTPPhone = a.nac_FTPPhone,
                                 nac_FTPFax = a.nac_FTPFax,
                                 nac_FTPEmail = a.nac_FTPEmail,
                                 nac_FTPAltEmail = a.nac_FTPAltEmail,
                                 nac_FTPDealerContact = a.nac_FTPDealerContact,
                                 nac_DealerComment = a.nac_DealerComment,
                                 nac_FTPDealerWebsite = a.nac_FTPDealerWebsite,
                                 nac_FTPDealerBrand = a.nac_FTPDealerBrand,
                                 nac_DealerAmenities = a.nac_DealerAmenities,
                                 nac_FeedCreatedDate= a.nac_FeedCreatedDate,
                                 nac_FeedModifiedDate =a.nac_FeedModifiedDate,
                                 nac_FTPDisplayStatus = a.nac_FTPDisplayStatus,
                                 nac_DealerIDfromFeed = a.nac_DealerIDfromFeed,

                                 //CRM->DNN
                                 nac_ListCounty = a.nac_ListCounty,
                                 nac_DealerLatitude = a.nac_DealerLatitude,
                                 nac_DealerLongitude = a.nac_DealerLongitude,
                                 nac_SqFootage = a.nac_SqFootage,
                                 nac_TotalEmployees = a.nac_TotalEmployees,
                                 nac_TotalSales = a.nac_TotalSales,
                                 nac_Manager = a.nac_Manager,
                                 Name = a.Name,
                                 nac_AccountType = a.nac_AccountType,
                                 PrimaryContactId=a.PrimaryContactId,
                                 nac_AccountStatus = a.nac_AccountStatus,
                                 Telephone1 = a.Telephone1,
                                 Telephone2 = a.Telephone2,
                                 Fax = a.Fax,
                                 EMailAddress1 = a.EMailAddress1,
                                 WebSiteURL = a.WebSiteURL,
                                 Address1_Line1 = a.Address1_Line1,
                                 Address1_Line2 = a.Address1_Line2,
                                 Address1_Line3 = a.Address1_Line3,
                                 Address1_City = a.Address1_City,
                                 Address1_StateOrProvince = a.Address1_StateOrProvince,
                                 Address1_PostalCode = a.Address1_PostalCode
                             };

              DataSet_DATOSTableAdapters.DealerMasterCRMTableAdapter ListaBD = new DataSet_DATOSTableAdapters.DealerMasterCRMTableAdapter();
              DataTable LeadsBD = ListaBD.GetData_ListaDealerMasterCRM();
              int FilasBd = LeadsBD.Rows.Count;
              
              DataSet_DATOSTableAdapters.ActualizarDNNTableAdapter UpdateDNN = new DataSet_DATOSTableAdapters.ActualizarDNNTableAdapter();


              int tt = FilasBd;


              foreach (var t in LeadsCrm)
              {
                  string GuidCRM = t.AccountId.ToString();
                  string name_temp = "";
                  try
                  {
                      name_temp = t.Name.ToString();
                  }
                  catch { }


                  
                  

                  bool Existe = false;

                  if (GuidCRM == "65e160d1-6896-e311-9401-7446a0f45e24")
                  {
                      FilasBd = tt;
                  }
                  else
                  {
                      FilasBd = 0;
                      Existe = true;
                  }

                  for (int i = 0; i < FilasBd; i++)
                  {
                      string GuidBD = LeadsBD.Rows[i]["DealerGUID"].ToString();
                      
                      if (GuidBD == GuidCRM)
                      {
                          Existe = true;
                          
                          
                          //actualizar DNN->CRM
                          try
                          {
                              
                              
                              #region de DNN a CRM

                              if (LeadsBD.Rows[i]["FeedSource"].ToString() != "")
                              {
                                  if (t.nac_FeedSource == null)
                                      t.nac_FeedSource = new OptionSetValue();
                                  t.nac_FeedSource.Value = buscarFeedSource(LeadsBD.Rows[i]["FeedSource"].ToString());
                              }


                              t.nac_DealerFeed = LeadsBD.Rows[i]["DealerFeed"].ToString();

                              try
                              {
                                  t.nac_DealerFeedStartDate = Convert.ToDateTime(LeadsBD.Rows[i]["DealerFeedStartDate"]);
                              }
                              catch { }

                              try
                              {
                                  t.nac_DealerFeedEndDate = Convert.ToDateTime(LeadsBD.Rows[i]["DealerFeedEndDate"]);
                              }
                              catch { }

                              if (LeadsBD.Rows[i]["DealerType"].ToString() != "")
                              {
                                  if (t.nac_DealerType == null)
                                      t.nac_DealerType = new OptionSetValue();
                                  t.nac_DealerType.Value = buscarDealerType(LeadsBD.Rows[i]["DealerType"].ToString());
                              }

                              //ship (es el q esta abajo
                              t.nac_FTPDealerName = LeadsBD.Rows[i]["Dealership"].ToString();
                              t.nac_FTPAddress1 = LeadsBD.Rows[i]["Address"].ToString();
                              t.nac_FTPCity = LeadsBD.Rows[i]["City"].ToString();
                              t.nac_FTPState = LeadsBD.Rows[i]["State"].ToString();
                              t.nac_Zip5 = LeadsBD.Rows[i]["Zip5"].ToString();
                              t.nac_FTPPhone = LeadsBD.Rows[i]["Phone"].ToString();
                              t.nac_FTPFax = LeadsBD.Rows[i]["Fax"].ToString();
                              t.nac_FTPEmail = LeadsBD.Rows[i]["Email"].ToString();
                              t.nac_FTPAltEmail = LeadsBD.Rows[i]["AlternativeEmail"].ToString();
                              t.nac_FTPDealerContact = LeadsBD.Rows[i]["Contact"].ToString();
                              t.nac_DealerComment = LeadsBD.Rows[i]["DealerComment"].ToString();
                              t.nac_FTPDealerWebsite = LeadsBD.Rows[i]["Website"].ToString();
                              t.nac_FTPDealerBrand = LeadsBD.Rows[i]["DealerBrand"].ToString();
                              t.nac_DealerAmenities = LeadsBD.Rows[i]["DealerAmenities"].ToString();

                              try
                              {
                                  t.nac_FeedCreatedDate = Convert.ToDateTime(LeadsBD.Rows[i]["CreatedDate"]);
                              }
                              catch { }

                              try
                              {
                                  t.nac_FeedModifiedDate = Convert.ToDateTime(LeadsBD.Rows[i]["ModifiedDate"]);
                              }
                              catch { }

                              if (LeadsBD.Rows[i]["DisplayStatus"].ToString() != "")
                              {
                                  if (t.nac_FTPDisplayStatus == null)
                                      t.nac_FTPDisplayStatus = new OptionSetValue();
                                  t.nac_FTPDisplayStatus.Value = buscarDisplayStatus(LeadsBD.Rows[i]["DisplayStatus"].ToString());
                              }

                              t.nac_DealerIDfromFeed = LeadsBD.Rows[i]["DealerIDFromFeed"].ToString();
                              //Guardar cambios
                              svcContext.UpdateObject(t);
                              svcContext.SaveChanges();
                              LogD.GetData_IngresoDetail(id_temp_header, 2, GuidCRM, name_temp + " CRM upgraded from DNN ", false);
                              #endregion
                          }
                          catch
                          {
                              LogD.GetData_IngresoDetail(id_temp_header, 2, GuidCRM, name_temp + " CRM not upgraded from DNN", true);
                          }


                          
                          //actualizar CRM->DNN
                          try
                          {
                              #region de CRM a DNN
                              string v1 = "";
                              try
                              {
                                  v1 = t.nac_ListCounty.ToString();
                              }
                              catch{}
                              
                              double v2 = 0;
                                  try
                                  {
                                      v2 =Convert.ToDouble(t.nac_DealerLatitude);
                                  }
                              catch{}

                                  double v3 = 0;
                                  try
                                  {
                                      v3 = Convert.ToDouble(t.nac_DealerLongitude);
                                  }
                                  catch { }


                                  string v4 = "";
                                  try
                                  {
                                      v4 = t.nac_SqFootage.ToString();
                                  }
                                  catch { }

                                  string v5 = "";
                                  try
                                  {
                                      v5=t.nac_TotalEmployees.ToString();
                                  }
                              catch{}


                              string v6="";
                              try
                              {
                                  v6 = t.nac_TotalSales.ToString();
                              }
                              catch { }


                              string v7 = "";
                              try
                              {
                                  v7 = t.nac_Personnel1.ToString();
                              }
                              catch { }


                              string v8 = "";
                              try
                              {
                                  v8 = t.nac_Manager.ToString();
                              }
                              catch
                              { }


                              string v9 = "";
                              try
                              {
                                  v9 = t.Name.ToString();
                              }
                              catch { }

                              string v10 = "";
                                  try
                                  {
                              v10=buscaraccounttypevuelta(t.nac_AccountType.Value);
                                  }
                              catch{}


                                  string v11 = "";
                                  try
                                  {
                                      v11 = buscardealertypevuelta(t.nac_DealerType.Value);
                                  }
                                  catch { }

                                  string v12 = "";
                                  try
                                  {
                              v12=buscarprimarycontact(t.PrimaryContactId.Id.ToString());
                                  }
                              catch{}



                                  string v13 = "";
                                  try
                                  {
                                      v13 = buscaraccountstatusvuelta(t.nac_AccountStatus.Value);
                                  }
                                  catch { }

                              string v14 = "";
                              try
                              {
                                  v14 = t.Telephone1.ToString();
                              }
                              catch { }

                                  string v15 = "";
                                  try
                                  {
                                      v15 = t.Telephone2.ToString();
                                  }
                                  catch { }

                                  string v16 = "";
                                  try
                                  {
                                      v16 = t.Fax.ToString();
                                  }
                                  catch { }

                              string v17 = "";
                              try
                              {
                                  v17 = t.EMailAddress1.ToString();
                              }
                              catch { }

                              string v18 = "";
                              try
                              {
                                  v18 = t.WebSiteURL.ToString();
                              }
                              catch { }

                              string v19 = "";
                              try
                              {
                                  v19 = t.Address1_Line1.ToString();
                              }
                              catch { }

                              string v20 = "";
                              try
                              {
                                  v20 = t.Address1_Line2.ToString();
                              }
                              catch { }

                              string v21 = "";
                              try
                              {
                                  v21 = t.Address1_Line3.ToString();
                              }
                              catch { }

                              string v22 = "";
                              try
                              {
                                  v22 = t.Address1_City.ToString();
                              }
                              catch { }

                              string v23 = "";
                              try
                              {
                                  v23 = t.Address1_StateOrProvince.ToString();
                              }
                              catch { }

                              string v24 = "";
                              try
                              {
                                  v24 = t.Address1_PostalCode.ToString();
                              }
                              catch { }

                              UpdateDNN.GetData_ActualizarDNN(GuidCRM, v1,v2 , v3,v4 ,v5 ,v6 ,v7 ,v8 ,v9 ,v10 ,v11 ,v12 ,v13 ,v14 ,v15 ,v16 ,v17 ,v18,v19 ,v20 ,v21 , v22,v23 ,v24 );
                              LogD.GetData_IngresoDetail(id_temp_header, 2, GuidCRM, name_temp + " DNN upgraded from CRM", false);
                              #endregion
                          }
                          catch
                          {
                              LogD.GetData_IngresoDetail(id_temp_header, 2, GuidCRM, name_temp + " DNN not upgraded from CRM", true);
                          }
                          i = FilasBd;
                      }

                  }

                  if (Existe == false)
                  {
                          LogD.GetData_IngresoDetail(id_temp_header, 2, GuidCRM,name_temp+" Not found CRM<->DNN", false);
                      //crear el contacto de CRM en DNN
                  }
              }

          }


          fin = DateTime.Now;
          TimeSpan v = fin - inicio;

          DataSet_DATOSTableAdapters.ActualizarHeaderTableAdapter FinHeader = new DataSet_DATOSTableAdapters.ActualizarHeaderTableAdapter();
          string CfH = "Sync finished, duration:   " + v.ToString();
          FinHeader.GetData_actualizarHeader(id_temp_header, CfH);
          System.Console.WriteLine("\n");
          System.Console.WriteLine("\n");
          System.Console.WriteLine("\n");
          System.Console.WriteLine(fin.ToString() + "    " + CfH);

          System.Console.WriteLine("\n");
          System.Console.WriteLine("PROCESO TERMINADO SIN ERRORES...");
      }
      catch
      {
          DataSet_DATOSTableAdapters.ActualizarHeaderTableAdapter FinHeaderE = new DataSet_DATOSTableAdapters.ActualizarHeaderTableAdapter();
          System.Console.WriteLine("\n");
          System.Console.WriteLine("Sync finished by error irreparable...");
          FinHeaderE.GetData_actualizarHeader(id_temp_header, "Sync finished by error irreparable...");
      }
  }

  #endregion FinSync


  #region Main method
  static public void Main(string[] args)
  {
      ServerConnection serverConnect = new ServerConnection();
      ServerConnection.Configuration config = serverConnect.GetServerConfiguration();

      UseLinqQuery app = new UseLinqQuery();
      app.Sync(config, false);
    Console.WriteLine("Press <Enter> to exit.");
    Console.ReadLine();
  
  }
  #endregion Main method
 }
}
