using BonsaiManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.DTOs.CareRecords.Responses
{
    public class CareRecordResponse
    {
        public Guid Id { get; set; }
        public CareType CareType { get; set; }
        public string CareTypeName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
