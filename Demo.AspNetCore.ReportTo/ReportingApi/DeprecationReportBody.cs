using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.AspNetCore.ReportTo.ReportingApi
{
    public class DeprecationReportBody
    {
        private const string ID = "id";
        private const string ANTICIPATED_REMOVAL = "anticipatedRemoval";
        private const string MESSAGE = "message";
        private const string SOURCE_FILE = "sourceFile";
        private const string LINE_NUMBER = "lineNumber";
        private const string COLUMN_NUMBER = "columnNumber";

        private readonly IDictionary<string, object> _reportBody;

        public string Id
        {
            get { return GetStringFromBody(ID); }
        }

        public string AnticipatedRemoval
        {
            get { return GetStringFromBody(ANTICIPATED_REMOVAL); }
        }

        public string Message
        {
            get { return GetStringFromBody(MESSAGE); }
        }

        public string SourceFile
        {
            get { return GetStringFromBody(SOURCE_FILE); }
        }

        public int? LineNumber
        {
            get { return GetInt32FromBody(LINE_NUMBER); }
        }

        public int? ColumnNumber
        {
            get { return GetInt32FromBody(COLUMN_NUMBER); }
        }

        public DeprecationReportBody(IDictionary<string, object> reportBody)
        {
            _reportBody = reportBody ?? throw new ArgumentNullException(nameof(reportBody));
        }

        private string GetStringFromBody(string key)
        {
            if (_reportBody.ContainsKey(key) && !(_reportBody[key] is null))
            {
                return _reportBody[key].ToString();
            }

            return null;
        }

        private int? GetInt32FromBody(string key)
        {
            if (_reportBody.ContainsKey(key) && !(_reportBody[key] is null))
            {
                return Convert.ToInt32(_reportBody[key].ToString());
            }

            return null;
        }
    }
}
