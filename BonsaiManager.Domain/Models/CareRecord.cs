using BonsaiManager.Domain.Enums;
using BonsaiManager.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.Domain.Models
{
    public class CareRecord : BaseEntity
    {
        public CareType CareType { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }

        public Guid BonsaiId { get; set; }
        public Bonsai Bonsai { get; set; } = null!;
    }
}
