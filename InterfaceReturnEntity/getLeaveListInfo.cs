using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    public class getLeaveListInfo
    {
        public string leaveId { get; set; }
        public string termerId { get; set; }
        public string termerName { get; set; }
        public string orgName { get; set; }
        public string destinationSelText { get; set; }
        public string destination { get; set; }
        public Nullable<int> cumulativeDays { get; set; }
        public Nullable< int> reportBackStatus { get; set; }
        public Nullable<DateTime> reportBackTime { get; set; }
        public string reportBackFileIds { get; set; }
        public Nullable<DateTime> applyStartTime { get; set; }
        public Nullable<DateTime> applyEndTime { get; set; }
        public string applyReason { get; set; }
        public Nullable<DateTime> applyTime { get; set; }
        public string applyFileIds { get; set; }
        public Nullable<int> preApproveStatus { get; set; }
        public Nullable<int> processStatus { get; set; }

    }
}
