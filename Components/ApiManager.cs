using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Components
{
    public class ApiManager
    {
        protected Dictionary<string, string> response;
        protected DataTable table;

        public ApiManager()
        {

        }

        public Dictionary<string, string> GetTIme()
        {

            response = new Dictionary<string, string>();
            response.Add("time", DateTime.UtcNow.ToString());
            return response;
        }
    }

    public class DatabaseApiManager : ApiManager
    {
        public int RecordLimit { get; set; }
        public int Timeout { get; set; }

        public DatabaseApiManager() : base()
        {


        }

        public Dictionary<string, string> GetUSerInfo(Dictionary<string, string> parm)
        {
            this.response = new Dictionary<string, string>();

            using (var connector = new MySqlConnector()) {

                connector.GetUser(parm["USID"], parm["HASH"]);

            }
            return response;
        }

    }
}
