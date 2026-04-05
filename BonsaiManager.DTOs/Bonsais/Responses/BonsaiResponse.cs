using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.DTOs.Bonsais.Responses
{
    public class BonsaiResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Style { get; set; } = string.Empty;
        public DateTime AcquisitionDate { get; set; }
        public string? Notes { get; set; }
        public string? ImageUrl { get; set; }
        public Guid SpeciesId { get; set; }
        public string SpeciesName { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
