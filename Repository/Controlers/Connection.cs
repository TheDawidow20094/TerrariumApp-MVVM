using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Controlers
{
    public class Connection
    {
        private string _localConnectionString = "Data Source = Database/database.db";

        public string GetLocalConnectionString()
        {
            return _localConnectionString;
        }

        public void SetLocalConnectionString(string localConnectionString)
        {
            _localConnectionString = localConnectionString;
        }
    }
}
