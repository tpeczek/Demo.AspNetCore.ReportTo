using System;
using System.Collections.Generic;

namespace Demo.AspNetCore.ReportTo.ReportingApi
{
    internal class CrashReportBody
    {
        private const string REASON = "reason";

        private readonly IDictionary<string, object> _reportBody;

        public string Reason
        {
            get { return _reportBody[REASON].ToString(); }
        }

        public CrashReportBody(IDictionary<string, object> reportBody)
        {
            _reportBody = reportBody ?? throw new ArgumentNullException(nameof(reportBody));
        }
    }
}
