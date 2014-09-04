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

//<snippetModernSoapApp>
using Microsoft.Preview.WindowsAzure.ActiveDirectory.Authentication;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Security.Authentication.Web;

namespace ModernSoapApp
{
    /// <summary>
    /// Manages authentication with the organization web service.
    /// </summary>
   public static class CurrentEnvironment
   {
       # region Class Level Members

       private static AuthenticationContext _authenticationContext;

       // TODO Set these string values as approppriate for your app registration and organization.
       // For more information, see the SDK topic "Walkthrough: Register an app with Active Directory".
       private const string _clientID = "893262be-fbdc-4556-9325-9f863b69495b";
       public const string CrmServiceUrl = "https://my-domain.crm.dynamics.com/";

       // Dyamics CRM Online OAuth URL.
       private const string _oauthUrl = "https://login.windows.net/common/wsfed";

       # endregion

       // <summary>
       /// Perform any required app initialization.
       /// This is where authentication with Active Directory is performed.
       public static async Task<string> Initialize()
       {
           // Obtain the redirect URL for the app. This is only needed for app registration.
           string redirectUrl = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();

           // Obtain an authentication token to access the web service. 
           _authenticationContext = new AuthenticationContext(_oauthUrl, false);
           AuthenticationResult result = await _authenticationContext.AcquireTokenAsync("Microsoft.CRM", _clientID);

           // Verify that an access token was successfully acquired.
           if (AuthenticationStatus.Succeeded != result.Status)
           {
               if (result.Error == "authentication_failed")
               {
                   // Clear the token cache and try again.
                   (AuthenticationContext.TokenCache as DefaultTokenCache).Clear();
                   _authenticationContext = new AuthenticationContext(_oauthUrl, false);
                   result = await _authenticationContext.AcquireTokenAsync("Microsoft.CRM", _clientID);
               }
               else
               {
                   DisplayErrorWhenAcquireTokenFails(result);
               }
           }
           return result.AccessToken;
       }

        /// <summary>
        /// Display an error message to the user.
        /// </summary>
        /// <param name="result">The authentication result returned from AcquireTokenAsync().</param>
        private static async void DisplayErrorWhenAcquireTokenFails(AuthenticationResult result)
        {
            MessageDialog dialog;

            switch (result.Error)
            {
                case "authentication_canceled":
                    // User cancelled, so no need to display a message.
                    break;
                case "temporarily_unavailable":
                case "server_error":
                    dialog = new MessageDialog("Please retry the operation. If the error continues, please contact your administrator.",
                        "Sorry, an error has occurred.");
                    await dialog.ShowAsync();
                    break;
                default:
                    // An error occurred when acquiring a token so show the error description in a MessageDialog.
                    dialog = new MessageDialog(string.Format(
                        "If the error continues, please contact your administrator.\n\nError: {0}\n\nError Description:\n\n{1}",
                        result.Error, result.ErrorDescription), "Sorry, an error has occurred.");
                    await dialog.ShowAsync();
                    break;
            }
        }
    }
}
//</snippetModernSoapApp>