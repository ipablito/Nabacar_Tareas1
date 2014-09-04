// =====================================================================
//  This file is part of the Microsoft Dynamics CRM SDK code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================

//<snippetUseLinqQuery>
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

// These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
// located in the SDK\bin folder of the SDK download.
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Data.OleDb;

using System.Data;

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

        #region How To Sample Code
        /// <summary>
        /// This method first connects to the Organization service and service context.
        /// Afterwards, several LINQ query techniques are demonstrated.
        /// </summary>
        /// <param name="serverConfig">Contains server connection information.</param>
        /// <param name="promptForDelete">When True, the user will be prompted to delete
        /// all created entities.</param>
        public void Run(ServerConnection.Configuration serverConfig, bool promptForDelete)
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
        }

        /// <summary>
        /// Creates any entity records that this sample requires.
        /// </summary>
        public void CreateRequiredRecords()
        {
            // Create 3 contacts.
            Contact contact = new Contact()
            {
                FirstName = "Ben",
                LastName = "Andrews",
                EMailAddress1 = "sample@example.com",
                Address1_City = "Redmond",
                Address1_StateOrProvince = "WA"
            };
            Guid benAndrewsContactId = _service.Create(contact);
            _contactIds.Add(benAndrewsContactId);

            //Create a task associated with Ben Andrews
            Task task = new Task()
            {
                Subject = "Sample Task",
                RegardingObjectId = new EntityReference()
                {
                    LogicalName = Contact.EntityLogicalName,
                    Id = benAndrewsContactId,
                    Name = contact.FullName
                }
            };
            _taskIds.Add(_service.Create(task));



            contact = new Contact()
            {
                FirstName = "Colin",
                LastName = "Wilcox",
                EMailAddress1 = "sample@example.com",
                Address1_City = "Bellevue",
                Address1_StateOrProvince = "WA"
            };
            _contactIds.Add(_service.Create(contact));

            contact = new Contact()
            {
                FirstName = "Ben",
                LastName = "Smith",
                EMailAddress1 = "sample@example.com",
                Address1_City = "Bellevue",
                Address1_StateOrProvince = "WA"
            };
            _contactIds.Add(_service.Create(contact));

            // Create 3 leads.
            Lead lead = new Lead()
            {
                FirstName = "Dan",
                LastName = "Wilson",
                EMailAddress1 = "sample@example.com",
                Address1_City = "Redmond",
                Address1_StateOrProvince = "WA"
            };
            _leadIds.Add(_service.Create(lead));

            lead = new Lead()
            {
                FirstName = "Jim",
                LastName = "Wilson",
                EMailAddress1 = "sample@example.com",
                Address1_City = "Bellevue",
                Address1_StateOrProvince = "WA"
            };
            _leadIds.Add(_service.Create(lead));

            lead = new Lead()
            {
                FirstName = "Denise",
                LastName = "Smith",
                EMailAddress1 = "sample@example.com",
                Address1_City = "Bellevue",
                Address1_StateOrProvince = "WA"
            };
            _leadIds.Add(_service.Create(lead));

            // Create 5 customized Accounts for the LINQ samples.
            Account account = new Account
            {
                Name = "A. Datum Corporation",
                Address1_StateOrProvince = "Colorado",
                Address1_Telephone1 = "(206)555-5555",
                PrimaryContactId =
                    new EntityReference(Contact.EntityLogicalName, _contactIds[0])
            };
            _accountIds.Add(_service.Create(account));

            account = new Account
            {
                Name = "Adventure Works",
                Address1_StateOrProvince = "Illinois",
                Address1_County = "Lake County",
                Address1_Telephone1 = "(206)555-5555",
                OriginatingLeadId =
                    new EntityReference(Lead.EntityLogicalName, _leadIds[0])
            };
            _accountIds.Add(_service.Create(account));

            account = new Account
            {
                Name = "Coho Vineyard",
                Address1_StateOrProvince = "Washington",
                Address1_County = "King County",
                Address1_Telephone1 = "(425)555-5555",
                PrimaryContactId =
                    new EntityReference(Contact.EntityLogicalName, _contactIds[1]),
                OriginatingLeadId =
                    new EntityReference(Lead.EntityLogicalName, _leadIds[0])
            };
            _accountIds.Add(_service.Create(account));

            account = new Account
            {
                Name = "Fabrikam",
                Address1_StateOrProvince = "Washington",
                Address1_Telephone1 = "(425)555-5555",
                PrimaryContactId =
                    new EntityReference(Contact.EntityLogicalName, _contactIds[0])
            };
            _accountIds.Add(_service.Create(account));

            account = new Account
            {
                Name = "Humongous Insurance",
                Address1_StateOrProvince = "Missouri",
                Address1_County = "Saint Louis County",
                Address1_Telephone1 = "(314)555-5555",
                PrimaryContactId =
                    new EntityReference(Contact.EntityLogicalName, _contactIds[1])
            };
            _accountIds.Add(_service.Create(account));

            // Create 10 basic Account records.
            for (int i = 1; i <= 10; i++)
            {
                account = new Account
                {
                    Name = "Fourth Coffee " + i,
                    Address1_StateOrProvince = "California"
                };
                _accountIds.Add(_service.Create(account));
            }
        }

        /// <summary>
        /// Deletes any entity records that were created for this sample.
        /// <param name="prompt">Indicates whether to prompt the user 
        /// to delete the records created in this sample.</param>
        /// </summary>
        public void DeleteRequiredRecords(bool prompt)
        {
            bool toBeDeleted = true;

            if (prompt)
            {
                // Ask the user if the created entities should be deleted.
                Console.Write("\nDo you want these entity records deleted? (y/n) [y]: ");
                String answer = Console.ReadLine();
                if (answer.StartsWith("y") ||
                    answer.StartsWith("Y") ||
                    answer == String.Empty)
                {
                    toBeDeleted = true;
                }
                else
                {
                    toBeDeleted = false;
                }
            }

            if (toBeDeleted)
            {
                // Delete all records created in this sample.


                foreach (Guid taskId in _taskIds)
                {
                    _service.Delete(Task.EntityLogicalName, taskId);
                }
                foreach (Guid accountId in _accountIds)
                {
                    _service.Delete(Account.EntityLogicalName, accountId);
                }
                foreach (Guid contactId in _contactIds)
                {
                    _service.Delete(Contact.EntityLogicalName, contactId);
                }
                foreach (Guid leadId in _leadIds)
                {
                    _service.Delete(Lead.EntityLogicalName, leadId);
                }
                Console.WriteLine("Entity record(s) have been deleted.");
            }
        }

        #endregion How To Sample Code



        #region crear o actualizar contactos poniendoles el campo de GUID que tenga en el Excel o el q genero CRM *ipablito* 13/08/2014


        // Define the IDs needed for this sample.
        public Guid _contactId;
        public Guid _account1Id;


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

        public void GUID_Createcontacts(ServerConnection.Configuration serverConfig, bool promptForDelete)
        {
            try
            {
                using (_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig))
                {
                    _serviceProxy.EnableProxyTypes();

                    _service = (IOrganizationService)_serviceProxy;


                    ServiceContext svcContext = new ServiceContext(_service);
                    DataSet temp = new DataSet();
                    string Archivo = "C:\\Users\\iPabLiiTo_\\Desktop\\CRM.xlsx";
                    string Hoja = "2.3";
                    temp = DevolverData(Archivo, Hoja);
                    DataTable tt = temp.Tables[0];
                    int filas = tt.Rows.Count;


                    DataSet_generalTableAdapters.sp_InsertInHST_contactTableAdapter hst = new DataSet_generalTableAdapters.sp_InsertInHST_contactTableAdapter();

                    for (int i = 0; i < filas; i++)
                    {
                        string guid_excel = tt.Rows[i][28].ToString();
                        string teniaGuid = "Si tenia GUID de la empresa y es: "+guid_excel;
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
                                                select new Account
                                                {
                                                    AccountId = a.AccountId
                                                };
                            if (leadsCompleto.ToList().Count != 0)
                            {
                                guid_excel = leadsCompleto.ToList()[0].AccountId.ToString();
                                teniaGuid = "No tenia GUID de la empresa, se busco y es: " + guid_excel;
                            }
                        }

                        if (guid_excel != "")
                        {
                            Contact NewContact = new Contact
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

                            _account1Id = new Guid(guid_excel);



                            EntityReferenceCollection relatedEntities = new EntityReferenceCollection();
                            relatedEntities.Add(new EntityReference(Account.EntityLogicalName, _account1Id));
                            Relationship relationship = new Relationship("contact_customer_accounts");
                            _service.Associate(Contact.EntityLogicalName, _contactId, relationship, relatedEntities);

                            hst.GetData_historialContactos(firstname + " " + lastname, guid_excel, teniaGuid, "si se inserto");
                        }
                        else
                        {
                            hst.GetData_historialContactos(firstname + " " + lastname, "no se encontro la empresa, en excel es la fila numero "+(i+2).ToString(), "no tenia guid", "no se inserto");
                        }
                    }

                }
            }

           
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                throw;
            }
        }
        #endregion

        static public void Main(string[] args)
        {

            // Obtain the target organization's Web address and client logon 
            // credentials from the user.
            ServerConnection serverConnect = new ServerConnection();
            ServerConnection.Configuration config = serverConnect.GetServerConfiguration();

            UseLinqQuery app = new UseLinqQuery();
            //app.Run(config, true);
            app.GUID_Createcontacts(config, false);

            Console.WriteLine("Press <Enter> to exit.");
            Console.ReadLine();
        }
    }
}