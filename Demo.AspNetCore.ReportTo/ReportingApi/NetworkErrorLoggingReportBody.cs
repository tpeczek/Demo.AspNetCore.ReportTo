using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.AspNetCore.ReportTo.ReportingApi
{
    internal class NetworkErrorLoggingReportBody
    {
        private const string REFERRER = "referrer";
        private const string SAMPLING_FRACTION = "sampling_fraction";
        private const string SERVER_IP = "server_ip";
        private const string PROTOCOL = "protocol";
        private const string METHOD = "method";
        private const string STATUS_CODE = "status_code";
        private const string ELAPSED_TIME = "elapsed_time";
        private const string PHASE = "phase";
        private const string TYPE = "type";

        private readonly IDictionary<string, object> _reportBody;

        public string Referrer
        {
            get { return _reportBody[REFERRER].ToString(); }
        }

        public decimal SamplingFraction
        {
            get { return Convert.ToDecimal(_reportBody[SAMPLING_FRACTION].ToString()); }
        }

        public string ServerIp
        {
            get { return _reportBody[SERVER_IP].ToString(); }
        }

        public string Protocol
        {
            get { return _reportBody[PROTOCOL].ToString(); }
        }

        public string Method
        {
            get { return _reportBody[METHOD].ToString(); }
        }

        public int StatusCode
        {
            get { return Convert.ToInt32(_reportBody[STATUS_CODE].ToString()); }
        }

        public int ElapsedTime
        {
            get { return Convert.ToInt32(_reportBody[ELAPSED_TIME].ToString()); }
        }

        public string Phase
        {
            get { return _reportBody[PHASE].ToString(); }
        }

        public string Type
        {
            get { return _reportBody[TYPE].ToString(); }
        }

        public NetworkErrorLoggingReportBody(IDictionary<string, object> reportBody)
        {
            _reportBody = reportBody ?? throw new ArgumentNullException(nameof(reportBody));
        }
    }
}
