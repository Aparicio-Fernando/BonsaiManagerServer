using BonsaiManager.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.Domain.Models
{
    public class Bonsai : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Style { get; set; } = string.Empty;
        public DateTime AcquisitionDate { get; set; }
        public string? Notes { get; set; }
        public string? ImageUrl { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid SpeciesId { get; set; }
        public Species Species { get; set; } = null!;

        public ICollection<CareRecord> CareRecords { get; set; } = new List<CareRecord>();
    }
}
