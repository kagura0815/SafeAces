using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Database;
using System.Threading.Tasks;

namespace SafeAces.Includes
{
    internal class GlobalVariables
    {
        public static FirebaseClient client = new("https://safeaces-5ada1-default-rtdb.firebaseio.com/");

        public static string id, purpose, fullname, visitorkey, date, timein, timeout;
    }
}
