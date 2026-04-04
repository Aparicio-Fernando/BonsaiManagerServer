using BonsaiManager.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.Domain.Models
{
    public class Species : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Bonsai> Bonsais { get; set; } = new List<Bonsai>();
    }
}
