using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLToElasticSearch.Helper
{
    public static class Constants
    {
        public const string GroupId = "SQLTOELK";
        public const string Consumer = "consumer";
        public const string Producer = "producer";

        public const bool EnableAutoCommit = true;
        public const int StatisticsIntervalMs = 5000;
        public const int SessionTimeoutMs = 6000;
        public const decimal AutoOffsetReset = 0;
        public const bool EnableAutoOffsetStore = true;
        public const decimal SaslMechanism = 0;
        public const string SQLTOELASTIC = "SQLTOELASTIC";

        public const int Request_Timeout = 10000;
        public const int Session_Timeout = 10000;
        public const int Message_Timeout = 10000;
        public const int Socket_Timeout = 10000;
    }
}
