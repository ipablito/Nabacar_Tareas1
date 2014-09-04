
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




  /*public void Run(ServerConnection.Configuration serverConfig, bool promptForDelete)
  {
   try
   {

    // Connect to the Organization service. 
    // The using statement assures that the service proxy will be properly disposed.
    using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig))
    {
     // This statement is required to enable early-bound type support.
     _serviceProxy.EnableProxyTypes();

     _service = (IOrganizationService)_serviceProxy;

     CreateRequiredRecords();

     ServiceContext svcContext =
         new ServiceContext(_service);


     // Retrieve records with Skip/Take record paging. Setting a page size
     // can help you manage your Skip and Take calls, since Skip must be
     // passed a multiple of Take's parameter value.
     //<snippetUseLinqQuery1>
     int pageSize = 5;

     var accountsByPage = (from a in svcContext.AccountSet
                           select new Account
                           {
                            Name = a.Name,
                           });
     System.Console.WriteLine("Skip 10 accounts, then Take 5 accounts");
     System.Console.WriteLine("======================================");
     foreach (var a in accountsByPage.Skip(2 * pageSize).Take(pageSize))
     {
      System.Console.WriteLine(a.Name);
     }
     //</snippetUseLinqQuery1>
     System.Console.WriteLine();
     System.Console.WriteLine("<End of Listing>");
     System.Console.WriteLine();
     //OUTPUT:
     //Skip 10 accounts, then Take 5 accounts
     //======================================
     //Fourth Coffee 6
     //Fourth Coffee 7
     //Fourth Coffee 8
     //Fourth Coffee 9
     //Fourth Coffee 10

     //<End of Listing>



     // Use orderBy to order items retrieved.
     //<snippetUseLinqQuery2>
     var orderedAccounts = from a in svcContext.AccountSet
                           orderby a.Name
                           select new Account
                           {
                            Name = a.Name,
                           };
     System.Console.WriteLine("Display accounts ordered by name");
     System.Console.WriteLine("================================");
     foreach (var a in orderedAccounts)
     {
      System.Console.WriteLine(a.Name);
     }
     //</snippetUseLinqQuery2>
     System.Console.WriteLine();
     System.Console.WriteLine("<End of Listing>");
     System.Console.WriteLine();
     //OUTPUT:
     //Display accounts ordered by name
     //================================
     //A. Datum Corporation
     //Adventure Works
     //Coho Vineyard
     //Fabrikam
     //Fourth Coffee 1
     //Fourth Coffee 10
     //Fourth Coffee 2
     //Fourth Coffee 3
     //Fourth Coffee 4
     //Fourth Coffee 5
     //Fourth Coffee 6
     //Fourth Coffee 7
     //Fourth Coffee 8
     //Fourth Coffee 9
     //Humongous Insurance

     //<End of Listing>


     // Filter multiple entities using LINQ.
     //<snippetUseLinqQuery3>
     var query = from c in svcContext.ContactSet
                 join a in svcContext.AccountSet
                               on c.ContactId equals a.PrimaryContactId.Id
                 where c.LastName == "Wilcox" || c.LastName == "Andrews"
                 where a.Address1_Telephone1.Contains("(206)")
                     || a.Address1_Telephone1.Contains("(425)")
                 select new
                 {
                  Contact = new Contact
                  {
                   FirstName = c.FirstName,
                   LastName = c.LastName,
                  },
                  Account = new Account
                  {
                   Address1_Telephone1 = a.Address1_Telephone1
                  }
                 };

     Console.WriteLine("Join account and contact");
     Console.WriteLine("List all records matching specified parameters");
     Console.WriteLine("Contact name: Wilcox or Andrews");
     Console.WriteLine("Account area code: 206 or 425");
     Console.WriteLine("==============================================");
     foreach (var record in query)
     {
      Console.WriteLine("Contact Name: {0} {1}",
          record.Contact.FirstName, record.Contact.LastName);
      Console.WriteLine("Account Phone: {0}",
          record.Account.Address1_Telephone1);
     }
     //</snippetUseLinqQuery3>
     Console.WriteLine("<End of Listing>");
     Console.WriteLine();
     //OUTPUT:
     //Join account and contact
     //List all records matching specified parameters
     //Contact name: Wilcox or Andrews
     //Account area code: 206 or 425
     //==============================================
     //Contact Name: Ben Andrews
     //Account Phone: (206)555-5555
     //Contact Name: Ben Andrews
     //Account Phone: (425)555-5555
     //Contact Name: Colin Wilcox
     //Account Phone: (425)555-5555
     //<End of Listing>



     // Build a complex query with LINQ. This query includes multiple
     // JOINs and a complex WHERE statement.
     //<snippetUseLinqQuery4>
     var complexQuery = from c in svcContext.ContactSet
                        join a in svcContext.AccountSet
                               on c.ContactId equals a.PrimaryContactId.Id
                        join l in svcContext.CreateQuery<Lead>()
                               on a.OriginatingLeadId.Id equals l.LeadId
                        where c.LastName == "Wilcox" || c.LastName == "Andrews"
                        where a.Address1_Telephone1.Contains("(206)")
                            || a.Address1_Telephone1.Contains("(425)")
                        select new
                        {
                         Contact = new Contact
                         {
                          FirstName = c.FirstName,
                          LastName = c.LastName,
                         },
                         Account = new Account
                         {
                          Address1_Telephone1 = a.Address1_Telephone1
                         },
                         Lead = new Lead
                         {
                          LeadId = l.LeadId
                         }
                        };

     Console.WriteLine("Join account, contact and lead");
     Console.WriteLine("List all records matching specified parameters");
     Console.WriteLine("Contact name: Wilcox or Andrews");
     Console.WriteLine("Account area code: 206 or 425");
     Console.WriteLine("==============================================");
     foreach (var record in complexQuery)
     {
      Console.WriteLine("Lead ID: {0}",
          record.Lead.LeadId);
      Console.WriteLine("Contact Name: {0} {1}",
          record.Contact.FirstName, record.Contact.LastName);
      Console.WriteLine("Account Phone: {0}",
          record.Account.Address1_Telephone1);
     }
     //</snippetUseLinqQuery4>
     Console.WriteLine("<End of Listing>");
     Console.WriteLine();
     //OUTPUT:
     //Join account, contact and lead
     //List all records matching specified parameters
     //Contact name: Wilcox or Andrews
     //Account area code: 206 or 425
     //==============================================
     //Lead ID: 78d5df14-64a3-e011-aea3-00155dba3818
     //Contact Name: Colin Wilcox
     //Account Phone: (425)555-5555
     //<End of Listing>

     //Retrieve a related Task for a Contact
     //Shows requirement that LoadProperty must be used to access the related record.
     //<snippetUseLinqQuery5>
     Contact benAndrews = svcContext.ContactSet.Where(c => c.FullName == "Ben Andrews").FirstOrDefault();
     if (benAndrews != null)
     {
      //benAndrews.Contact_Tasks is null until LoadProperty is used.
      svcContext.LoadProperty(benAndrews, "Contact_Tasks");
      Task benAndrewsFirstTask = benAndrews.Contact_Tasks.FirstOrDefault();
      if (benAndrewsFirstTask != null)
      {
       Console.WriteLine("Ben Andrews first task with Subject: '{0}' retrieved.", benAndrewsFirstTask.Subject);
      }
     }
     //</snippetUseLinqQuery5>

     DeleteRequiredRecords(promptForDelete);
    }
   }

   // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
   catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
   {
    // You can handle an exception here or pass it back to the calling method.
    throw;
   }
  }*/

 
     
  #region extraer informacion
  public void RunExtractAccountInfo(ServerConnection.Configuration serverConfig, bool promptForDelete)
  {
      try
      {

          // Connect to the Organization service. 
          // The using statement assures that the service proxy will be properly disposed.
          using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig))
          {
              // This statement is required to enable early-bound type support.
              _serviceProxy.EnableProxyTypes();

              _service = (IOrganizationService)_serviceProxy;

              // CreateRequiredRecords();

              CrmEarlyBound.CrmServiceContext svcContext = new CrmEarlyBound.CrmServiceContext(_service);
              /*ServiceContext svcContext =
                  new ServiceContext(_service);*/


              // Retrieve records with Skip/Take record paging. Setting a page size
              // can help you manage your Skip and Take calls, since Skip must be
              // passed a multiple of Take's parameter value.
              //<snippetUseLinqQuery1>
              int pageSize = 5;

              /* jramirez 2014.06.25 start 
               Query to pull accounts, contacts & LEads Info*/

              var complexQuery = (from a in svcContext.AccountSet
                                  where a.Name != null
                                  orderby a.Name

                                  // where a.StatusCode = 'Active' 

                                  select new CrmEarlyBound.Account
                                  {
                                      Address1_PrimaryContactName = a.Address1_PrimaryContactName,
                                      Name = a.Name,
                                      Description = a.Description,
                                      Id = a.Id,

                                      //PaymentTermsCode = a.PaymentTermsCode,
                                      //PaymentTermsCodeValue =  a.FormattedValues["PaymentTermsCode"],
                                      Address1_Line1 = a.Address1_Line1,
                                      Address1_City = a.Address1_City,
                                      Address1_StateOrProvince = a.Address1_StateOrProvince,
                                      Address1_PostalCode = a.Address1_PostalCode,
                                      Telephone1 = a.Telephone1,
                                      EMailAddress1 = a.EMailAddress1,
                                      Address1_Telephone1 = a.Address1_Telephone1,
                                      StatusCode = a.StatusCode,
                                      nac_InDNN=a.nac_InDNN
                                  });

              /* jramirez 2014.06.25 end*/

              System.Console.WriteLine("Get Accounts Info");
              System.Console.WriteLine("======================================");
              // jramirez
              String filename = String.Concat("Accounts.xml");
              using (StreamWriter sw = new StreamWriter(filename))
              {
                  // Create Xml Writer.
                  XmlTextWriter metadataWriter = new XmlTextWriter(sw);

                  // Start Xml File.
                  metadataWriter.WriteStartDocument();

                  // Metadata Xml Node.
                  metadataWriter.WriteStartElement("Accounts");

                  //foreach (var a in accountsByPage.Skip(2 * pageSize).Take(pageSize))
                  foreach (var a in complexQuery)
                  {
                      System.Console.WriteLine(a.Name);
                      //jramirez
                      metadataWriter.WriteStartElement("Entity");
                      metadataWriter.WriteElementString("Account_ID", a.Id.ToString());
                      metadataWriter.WriteElementString("Address1_PrimaryContactName", a.Address1_PrimaryContactName);
                      // metadataWriter.WriteElementString("LastName", a.Contact.LastName);
                      metadataWriter.WriteElementString("Name", a.Name);
                      metadataWriter.WriteElementString("Description", a.Description);
                      //metadataWriter.WriteElementString("PaymentTermsCode", a.PaymentTermsCode);
                      metadataWriter.WriteElementString("Address1_Line1", a.Address1_Line1);
                      metadataWriter.WriteElementString("Address1_City", a.Address1_City);
                      metadataWriter.WriteElementString("Address1_StateOrProvince", a.Address1_StateOrProvince);
                      metadataWriter.WriteElementString("Address1_PostalCode", a.Address1_PostalCode);
                      metadataWriter.WriteElementString("Telephone1", a.Telephone1);

                      metadataWriter.WriteElementString("EMailAddress1", a.EMailAddress1);
                      metadataWriter.WriteElementString("Address1_Telephone1", a.Address1_Telephone1);
                      metadataWriter.WriteElementString("Telephone1", a.Telephone1);
                      metadataWriter.WriteElementString("StatusCode", a.StatusCode.Value.ToString());
                      metadataWriter.WriteElementString("nac_INDNN", a.nac_InDNN.ToString());


                      // metadataWriter.WriteElementString("LeadId", a.Lead.LeadId.ToString());
                      //metadataWriter.WriteElementString("CreatedBy", a.CreatedOn.ToString());
                      // metadataWriter.WriteElementString("LeadSourceCode", a.Lead.LeadSourceCode.ToString());
                      // metadataWriter.WriteElementString("CreatedBy", a.CreatedOn.ToString());
                      metadataWriter.WriteEndElement();
                  }




                  // jramirez
                  // End Metadata Xml Node
                  metadataWriter.WriteEndElement();
                  metadataWriter.WriteEndDocument();

                  // Close xml writer.
                  metadataWriter.Close();




              }

              //</snippetUseLinqQuery1>
              System.Console.WriteLine();
              System.Console.WriteLine("<End of Listing>");
              System.Console.WriteLine();
              //OUTPUT:
              //Skip 10 accounts, then Take 5 accounts
              //======================================
              //Fourth Coffee 6
              //Fourth Coffee 7
              //Fourth Coffee 8
              //Fourth Coffee 9
              //Fourth Coffee 10

              //<End of Listing>




              //</snippetUseLinqQuery5>

              // DeleteRequiredRecords(promptForDelete);
          }
      }

      // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
      catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
      {
          // You can handle an exception here or pass it back to the calling method.
          throw;
      }
  }
  #endregion
     



  #region comprobacion de los clientes *ipablito* *04/08/2014
  public void  EvaluarClientes(ServerConnection.Configuration serverConfig, bool promptForDelete)
  {
      try
      {
          using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig))
          {
              _serviceProxy.EnableProxyTypes();

              _service = (IOrganizationService)_serviceProxy;

              
              CrmEarlyBound.CrmServiceContext svcContext = new CrmEarlyBound.CrmServiceContext(_service);
              var leadsCompleto = (from a in svcContext.AccountSet
                                   where a.Name != null
                                   orderby a.Name
                                   select new CrmEarlyBound.Account
                                   {
                                       AccountId = a.AccountId,
                                       Name = a.Name,
                                       Address1_AddressId = a.Address1_AddressId,
                                       Telephone1 = a.Telephone1,
                                       nac_InDNN = a.nac_InDNN
                                   });


              DataTable temp = new DataTable();
              DataSet_DATOSTableAdapters.DealerMasterCRMTableAdapter lg = new DataSet_DATOSTableAdapters.DealerMasterCRMTableAdapter();
              DataSet_DATOSTableAdapters.SP_InsertRepetidosTableAdapter lR = new DataSet_DATOSTableAdapters.SP_InsertRepetidosTableAdapter();
              DataSet_DATOSTableAdapters.sp_InsertNoExisteTableAdapter lN = new DataSet_DATOSTableAdapters.sp_InsertNoExisteTableAdapter();
              DataSet_DATOSTableAdapters.sp_actualizarexisteTableAdapter AE = new DataSet_DATOSTableAdapters.sp_actualizarexisteTableAdapter();

              temp = lg.GetData_todosLosLeads();
              int yt=temp.Rows.Count;

              string GUIDtemporal_bd = "";
              string GUIDtemporal_crm = "";

              
              foreach (var t_ in leadsCompleto)
              {
                  GUIDtemporal_crm = t_.AccountId.ToString();
                  bool Existe = false;
                  for (int i = 0; i < yt; i++)
                  {
                      GUIDtemporal_bd = temp.Rows[i]["DealerGUID"].ToString();
                      
                      if (GUIDtemporal_crm == GUIDtemporal_bd)
                      {
                          Existe = true;
                          AE.GetData_Actualizarexistencia(GUIDtemporal_bd);
                          if (t_.nac_InDNN == true)
                          {
                              try {
                                  //lR.GetData_insertarrepetido(Convert.ToDouble(temp.Rows[i][0].ToString()), temp.Rows[i][1].ToString(), temp.Rows[i][2].ToString(), temp.Rows[i][3].ToString(), temp.Rows[i][4].ToString(),
                                  //Convert.ToDouble(temp.Rows[i][5].ToString()), Convert.ToDouble(temp.Rows[i][6].ToString()), temp.Rows[i][7].ToString(), temp.Rows[i][8].ToString(),
                                  //temp.Rows[i][9].ToString(), temp.Rows[i][10].ToString(), temp.Rows[i][11].ToString(), temp.Rows[i][12].ToString(),
                                  //temp.Rows[i][13].ToString(), temp.Rows[i][14].ToString(), Convert.ToDouble(temp.Rows[i][15].ToString()), Convert.ToDouble(temp.Rows[i][16].ToString()),
                                  //temp.Rows[i][17].ToString(), temp.Rows[i][18].ToString(), temp.Rows[i][19].ToString(), temp.Rows[i][20].ToString(),
                                  //temp.Rows[i][21].ToString(), Convert.ToDouble(temp.Rows[i][22].ToString()), temp.Rows[i][23].ToString(), Convert.ToDouble(temp.Rows[i][24].ToString()),
                                  //Convert.ToDateTime(temp.Rows[i][25].ToString()), Convert.ToDateTime(temp.Rows[i][26].ToString()), temp.Rows[i][27].ToString(), temp.Rows[i][28].ToString(),
                                  //temp.Rows[i][29].ToString(), temp.Rows[i][30].ToString(), Convert.ToDateTime(temp.Rows[i][31].ToString()), Convert.ToDateTime(temp.Rows[i][32].ToString()),
                                  //temp.Rows[i][33].ToString(), temp.Rows[i][34].ToString(), Convert.ToDouble(temp.Rows[i][35].ToString()), temp.Rows[i][36].ToString(),
                                  //temp.Rows[i][37].ToString(), temp.Rows[i][38].ToString(), temp.Rows[i][39].ToString(), temp.Rows[i][40].ToString(),
                                  //temp.Rows[i][41].ToString(), temp.Rows[i][42].ToString(), temp.Rows[i][43].ToString(), temp.Rows[i][44].ToString(),
                                  //temp.Rows[i][44].ToString(), temp.Rows[i][46].ToString(), temp.Rows[i][47].ToString(), temp.Rows[i][48].ToString(),
                                  //temp.Rows[i][49].ToString(), temp.Rows[i][50].ToString(), temp.Rows[i][51].ToString(), Convert.ToDouble(temp.Rows[i][52].ToString()),
                                  //temp.Rows[i][53].ToString());
                              }
                              catch { }
                          }
                          else
                          {
                              t_.nac_InDNN = true;
                              svcContext.UpdateObject(t_);
                              svcContext.SaveChanges();
                          }
                          
                      }
                  }
                  if (Existe == false)
                  {
                      string id = "";
                      string nombre = "";
                      string direccion = "";
                      string telefono = "";
                      string GUID = "";
                      try
                      {
                          id = t_.AccountId.ToString();
                          nombre = t_.Name.ToString();
                          direccion = t_.Address1_AddressId.ToString();
                          telefono = t_.Telephone1.ToString();
                          GUID = t_.nac_InDNN.ToString();
                      }
                      catch
                      { }
                      lN.GetData_insertarsinoexiste(id, nombre, direccion, telefono, GUID);
                  }
              }

              System.Console.WriteLine("\n");
              System.Console.WriteLine("PROCESO TERMINADO...");

          }
      }

      // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
      catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
      {
          // You can handle an exception here or pass it back to the calling method.
          throw;
      }
  }
  #endregion


  #region insertar en CRM los no existentes jalados desde excel *ipablito* 12/08/2014
  private DataSet DevolverData(string archivo, string hoja)
  {
      //declaramos las variables         
      OleDbConnection conexion = null;
      DataSet dataSet = null;
      OleDbDataAdapter dataAdapter = null;
      string consultaHojaExcel = "Select * from [" + hoja + "$]";

      //esta cadena es para archivos excel 2007 y 2010
      string cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + archivo + "';Extended Properties=Excel 12.0;";

      if (string.IsNullOrEmpty(hoja))
      {
          
      }
      else
      {
          try
          {
              //Si el usuario escribio el nombre de la hoja se procedera con la busqueda
              conexion = new OleDbConnection(cadenaConexionArchivoExcel);//creamos la conexion con la hoja de excel
              conexion.Open(); //abrimos la conexion
              dataAdapter = new OleDbDataAdapter(consultaHojaExcel, conexion); //traemos los datos de la hoja y las guardamos en un dataSdapter
              dataSet = new DataSet(); // creamos la instancia del objeto DataSet
              dataAdapter.Fill(dataSet, hoja);//llenamos el dataset
              //dataGridView1.DataSource = dataSet.Tables[0]; //le asignamos al DataGridView el contenido del dataSet
              conexion.Close();//cerramos la conexion
              //dataGridView1.AllowUserToAddRows = false;       //eliminamos la ultima fila del datagridview que se autoagrega
          }
          catch
          { }
      }

      return dataSet;
  }
  public void InsertarEnCRM(ServerConnection.Configuration serverConfig, bool promptForDelete)
  {
      try
      {
          using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig))
          {
              _serviceProxy.EnableProxyTypes();

              _service = (IOrganizationService)_serviceProxy;

              CrmEarlyBound.CrmServiceContext svcContext = new CrmEarlyBound.CrmServiceContext(_service);
              //var leadsCompleto = (from a in svcContext.AccountSet
              //                     where a.Name != null
              //                     orderby a.Name
              //                     select new CrmEarlyBound.Account
              //                     {
              //                         AccountId = a.AccountId,
              //                         Name = a.Name,
              //                         Address1_AddressId = a.Address1_AddressId,
              //                         Telephone1 = a.Telephone1,
              //                         nac_InDNN = a.nac_InDNN
              //                     });

              //double temp = Convert.ToDouble(leadsCompleto.ToList().Count);
              DataSet temp = new DataSet();
              string Archivo = "C:\\Users\\iPabLiiTo_\\Desktop\\CRM.xlsx";
              string Hoja = "2.1";
              temp = DevolverData(Archivo, Hoja);
              DataTable tt = temp.Tables[0];
              int filas = tt.Rows.Count;
              for (int i = 0; i < filas; i++)
              {
                  string nom = tt.Rows[i][4].ToString();
                  CrmEarlyBound.Account NuevaCuenta = new CrmEarlyBound.Account()
                  {
                      Name = tt.Rows[i][4].ToString(),
                      Address1_Line1 = tt.Rows[i][6].ToString(),
                      Address1_City = tt.Rows[i][7].ToString(),
                      Address1_StateOrProvince = tt.Rows[i][8].ToString(),
                      Address1_PostalCode = tt.Rows[i][9].ToString(),
                      Address1_Country = tt.Rows[i][10].ToString(),
                      EMailAddress1 = tt.Rows[i][14].ToString(),
                      Telephone1 = tt.Rows[i][17].ToString(),
                      Fax = tt.Rows[i][18].ToString(),
                      WebSiteURL = tt.Rows[i][20].ToString(),
                      nac_DealerLatitude = tt.Rows[i][21].ToString(),
                      nac_DealerLongitude = tt.Rows[i][22].ToString(),
                      nac_SqFootage = tt.Rows[i][23].ToString(),
                      nac_TotalEmployees = tt.Rows[i][24].ToString(),
                      nac_TotalSales = tt.Rows[i][25].ToString(),
                      SIC = tt.Rows[i][26].ToString()
                  };
                  svcContext.AddObject(NuevaCuenta);
                  svcContext.SaveChanges();
              }
          }
      }

      // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
      catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
      {
          // You can handle an exception here or pass it back to the calling method.
          throw;
      }
  }
  #endregion




  #region actualizar correos de las cuentas o de los contactos (jalados desde excel, y se comparan con los del CRM) *ipablito* 12/08/2014
  public void ActualizarCorreos(ServerConnection.Configuration serverConfig, bool promptForDelete)
  {
      try
      {
          using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig))
          {
              _serviceProxy.EnableProxyTypes();

              _service = (IOrganizationService)_serviceProxy;
              
              CrmEarlyBound.CrmServiceContext svcContext = new CrmEarlyBound.CrmServiceContext(_service);
              var leadsCompleto = (from a in svcContext.AccountSet
                                   where a.Name != null
                                   orderby a.Name
                                   select new CrmEarlyBound.Account
                                   {
                                       AccountId = a.AccountId,
                                       EMailAddress1 =a.EMailAddress1
                                   });

              double temp2 = Convert.ToDouble(leadsCompleto.ToList().Count);
              DataSet temp = new DataSet();
              string Archivo = "C:\\Users\\iPabLiiTo_\\Desktop\\CRM.xlsx";
              string Hoja = "2.2";
              temp = DevolverData(Archivo, Hoja);
              DataTable tt = temp.Tables[0];
              int filas = tt.Rows.Count;

              string guidCRM="";
              string guidEXCEL="";
              string CorreoTemporalCRM = "";
              string CorreoTemporalExcel = "";

              DataSet_DATOSTableAdapters.sp_InsertInHSTTableAdapter reg = new DataSet_DATOSTableAdapters.sp_InsertInHSTTableAdapter();

              bool exi = false;

              foreach (var _t in leadsCompleto)
              {
                  guidCRM=_t.AccountId.ToString();
                  
                  try
                  {
                      CorreoTemporalCRM = _t.EMailAddress1.ToString();
                  }
                  catch
                  {}

                  exi = false;
                  for (int i = 0; i < filas; i++)
                  {
                      guidEXCEL = tt.Rows[i][27].ToString();
                      CorreoTemporalExcel = tt.Rows[i][14].ToString();

                      if (guidCRM == guidEXCEL)
                      {
                          exi = true;
                          if ((CorreoTemporalCRM == "") || (CorreoTemporalCRM == null))
                          {
                              _t.EMailAddress1 = CorreoTemporalExcel;
                              svcContext.UpdateObject(_t);
                              svcContext.SaveChanges();
                              reg.GetData_InsertarEnHistorial("Accounts", guidCRM, "encontrado", "correo actualizado");
                          }
                          else
                          {
                              reg.GetData_InsertarEnHistorial("Accounts", guidCRM, "encontrado", "correo no actualizado");
                              
                              string fn = tt.Rows[i][12].ToString();
                              string ln = tt.Rows[i][13].ToString();

                              var ListContacts = from a in svcContext.ContactSet
                                                 join c in svcContext.AccountSet
                                                 on a.ContactId equals c.PrimaryContactId.Id
                                             where c.AccountId ==_t.AccountId
                                             select new CrmEarlyBound.Contact
                                             {
                                                 ContactId = a.ContactId,
                                                 FirstName = a.FirstName,
                                                 LastName = a.LastName,
                                                 EMailAddress1 = a.EMailAddress1
                                             };

                              if (ListContacts.ToList().Count != 0)
                              {
                                  reg.GetData_InsertarEnHistorial("Account-Contact", "", "De aqui para abajo los contact del GUID " + guidCRM, "");
                                  string correoContacto = "";
                                  bool contacexis = false;
                                  foreach (var tc in ListContacts)
                                  {
                                      correoContacto = tc.EMailAddress1;

                                      if ((tc.FirstName == fn) && (tc.LastName == ln))
                                      {
                                          contacexis = true;
                                          if ((correoContacto == "") || (correoContacto == null))
                                          {
                                              tc.EMailAddress1 = CorreoTemporalExcel;
                                              svcContext.UpdateObject(tc);
                                              svcContext.SaveChanges();
                                              reg.GetData_InsertarEnHistorial("Contact", tc.ContactId.ToString(), "Encontrado", "Correo actualizado");
                                          }
                                          else
                                          {
                                              reg.GetData_InsertarEnHistorial("Contact", tc.ContactId.ToString(), "Encontrado", "Correo No actualizado");
                                          }
                                      }
                                  }
                                  if (contacexis == false)
                                  {
                                      reg.GetData_InsertarEnHistorial("Contact", fn + "" + ln, "No encontrado", "");
                                  }
                              }
                              else
                              {
                                  reg.GetData_InsertarEnHistorial("Contact", "", "Ningun contacto encontrado con el GUID "+guidCRM, "No Actualizado");
                              }
                              
                          }
                      }
                  }


                  if (exi == false)
                  {
                      reg.GetData_InsertarEnHistorial("Account", guidCRM, "No encontrado en el excel", "No Actualizado");
                  }
              }
          }
      }

      // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
      catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
      {
          // You can handle an exception here or pass it back to the calling method.
          throw;
      }
  }
  #endregion



  #region Cambiar el valor na_dealertye a "used" en los q tengan en el campo nac_InDNN=null
  public void ActualizarNac_indnnDondeNULL(ServerConnection.Configuration serverConfig, bool promptForDelete)
  {int contador = 0;
      try
      {
          using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig))
          {
              _serviceProxy.EnableProxyTypes();

              _service = (IOrganizationService)_serviceProxy;

              
              CrmEarlyBound.CrmServiceContext svcContext = new CrmEarlyBound.CrmServiceContext(_service);
              var leadsCompleto = (from a in svcContext.AccountSet
                                   where a.nac_InDNN == false
                                   orderby a.Name
                                   select new CrmEarlyBound.Account
                                   {
                                       AccountId = a.AccountId,
                                       Name =a.Name,
                                       nac_InDNN = a.nac_InDNN,
                                       nac_DealerType = a.nac_DealerType
                                   });


              
              foreach (var t_ in leadsCompleto)
              {
                  string id = t_.Name.ToString();
                  try
                  {
                      string tedfs = t_.nac_DealerType.Value.ToString();
                  }
                  catch
                  { }

                  if (t_.nac_InDNN == false)
                  {
                      if (t_.nac_DealerType == null)
                      {
                          t_.nac_DealerType = new OptionSetValue();
                      }
                      t_.nac_DealerType.Value = 808840001;
                      svcContext.UpdateObject(t_);
                      svcContext.SaveChanges();
                      contador++;
                  }
              }

          }

      }
      catch
      { }
      System.Console.WriteLine(contador.ToString() + " Registros actualizados");
      System.Console.WriteLine("\n");
      System.Console.WriteLine("PROCESO TERMINADO...");
  }
  #endregion







  #region crear o actualizar contactos poniendoles el campo de GUID que tenga en el Excel o el q genero CRM *ipablito* 13/08/2014
  

  // Define the IDs needed for this sample.
  public Guid _contactId;
  public Guid _account1Id;
  


  public void GUID_Createcontacts(ServerConnection.Configuration serverConfig, bool promptForDelete)
  {
      try
      {
          using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig))
          {
              _serviceProxy.EnableProxyTypes();

              _service = (IOrganizationService)_serviceProxy;

              CrmEarlyBound.CrmServiceContext svcContext = new CrmEarlyBound.CrmServiceContext(_service);
              
              DataSet temp = new DataSet();
              string Archivo = "C:\\Users\\iPabLiiTo_\\Desktop\\CRM.xlsx";
              string Hoja = "2.3";
              temp = DevolverData(Archivo, Hoja);
              DataTable tt = temp.Tables[0];
              int filas = tt.Rows.Count;


              for (int i = 0; i < filas; i++)
              {
                  string guid_excel = tt.Rows[i][28].ToString();
                  string address_street = tt.Rows[i][7].ToString(); ;
                  string address_city = tt.Rows[i][8].ToString();
                  string addres_state = tt.Rows[i][9].ToString();
                  string zip_code = tt.Rows[i][10].ToString();
                  string addres_country = tt.Rows[i][11].ToString();
                  string firstname = tt.Rows[i][13].ToString();
                  string lastname = tt.Rows[i][14].ToString();
                  string email = tt.Rows[i][15].ToString();
                  string title = tt.Rows[i][16].ToString();
                  string phone = tt.Rows[i][18].ToString();
                  string fax = tt.Rows[i][19].ToString();
                  string mobile = tt.Rows[i][20].ToString();
                  string website = tt.Rows[i][21].ToString();

                  if (guid_excel == "")
                  {
                      string nameCompany = tt.Rows[i][4].ToString();
                      string addressCompany = tt.Rows[i][7].ToString();

                      var leadsCompleto = from a in svcContext.AccountSet
                                          where a.Name == nameCompany && a.Address1_Line1 == addressCompany
                                          select new CrmEarlyBound.Account
                                          {
                                              AccountId = a.AccountId
                                          };
                      if (leadsCompleto.ToList().Count != 0)
                      {
                          guid_excel = leadsCompleto.ToList()[0].AccountId.ToString();
                      }
                  }

                  Guid l = new Guid(guid_excel);
                  CrmEarlyBound.Contact NewContact = new CrmEarlyBound.Contact
                  {
                      Address1_Line1 = address_street,
                      Address1_City = address_city,
                      Address1_StateOrProvince = addres_state,
                      Address1_PostalCode = zip_code,
                      Address1_Country = addres_country,
                      FirstName = firstname,
                      LastName = lastname,
                      EMailAddress1 = email,
                      JobTitle = title,
                      Telephone1 = phone,
                      Fax = fax,
                      MobilePhone = mobile,
                      WebSiteUrl = website,
                  };
                  _contactId = _service.Create(NewContact);

                  _account1Id = l;


                  EntityReferenceCollection relatedEntities = new EntityReferenceCollection();
                  relatedEntities.Add(new EntityReference(CrmEarlyBound.Account.EntityLogicalName, _account1Id));
                  Relationship relationship = new Relationship("account_primary_contact");
                  _service.Associate(CrmEarlyBound.Contact.EntityLogicalName, _contactId, relationship,relatedEntities);
                  //EntityReferenceCollection relatedEntities = new EntityReferenceCollection();
                  //Relationship relationship = new Relationship("account_primary_contact");
                  //relatedEntities.Add(new EntityReference(CrmEarlyBound.Account.EntityLogicalName, _account1Id));
                  //_service.Associate(CrmEarlyBound.Contact.EntityLogicalName, _contactId, relationship, relatedEntities);
                  //svcContext.SaveChanges();

              }

          }
      }

      // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
      catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
      {
          // You can handle an exception here or pass it back to the calling method.
          throw;
      }
  }
  #endregion




  public void comparar(ServerConnection.Configuration serverConfig, bool promptForDelete)
  {
      try
      {
          using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig))
          {
              _serviceProxy.EnableProxyTypes();

              _service = (IOrganizationService)_serviceProxy;

              CrmEarlyBound.CrmServiceContext svcContext = new CrmEarlyBound.CrmServiceContext(_service);
         var leadsCompleto = from c in svcContext.ContactSet
                            join a in svcContext.AccountSet
                            on c.ContactId equals a.PrimaryContactId.Id
                                          select new 
                                          {
                                           c.ContactId,
                                           c.FirstName,
                                           a.PrimaryContactId.Id,
                                           a.Name
                                          };
        foreach (var a in leadsCompleto)
        {
            string tttt = a.FirstName.ToString()+" "+a.ContactId.ToString();
            string tttt2 = a.Name.ToString()+" "+ a.Id.ToString();
        }
          }
      }

      // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
      catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
      {
          // You can handle an exception here or pass it back to the calling method.
          throw;
      }
  }



  #region Main method

  /// <summary>
  /// Standard Main() method used by most SDK samples.
  /// </summary>
  /// <param name="args"></param>
  /// 

  static public void Main(string[] args)
  {
      ServerConnection serverConnect = new ServerConnection();
      ServerConnection.Configuration config = serverConnect.GetServerConfiguration();

      UseLinqQuery app = new UseLinqQuery();

      //app.EvaluarClientes(config, false);
      //app.RunExtractAccountInfo(config, false);
      //app.InsertarEnCRM(config, false);
      //app.ActualizarCorreos(config, false);
      //app.ActualizarNac_indnnDondeNULL(config,false);
      

      //app.comparar(config, false);

      app.GUID_Createcontacts(config, false);
      
    Console.WriteLine("Press <Enter> to exit.");
    Console.ReadLine();
  
  }
  #endregion Main method
 }
}
