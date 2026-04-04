using BonsaiManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.DTOs.CareRecords.Requests
{
    public class AddCareRecordRequest
    {
        public Guid BonsaiId { get; set; }
        public CareType CareType { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }
}
