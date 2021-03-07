using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    [Serializable]
    class _MySqlConnectorException : Exception
    {
        public _MySqlConnectorException()
        {

        }

        public _MySqlConnectorException(string message)
            : base(String.Format($"MysSqlConnectorException: {message}"))
        {

        }
    }
}
