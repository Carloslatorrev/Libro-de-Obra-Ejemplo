using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace LODapp.Helpers
{
    public class ConnectivityHelper
    {

        public ConnectivityHelper()
        {

        }

        public bool VerifyConnection()
        {
            bool connection = false;
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                connection = true;
                return connection;
            }
            return connection;
        }
    }
}
