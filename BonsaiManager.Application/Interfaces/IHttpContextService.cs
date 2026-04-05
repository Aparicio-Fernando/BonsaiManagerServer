using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonsaiManager.Application.Interfaces
{
    public interface IHttpContextService
    {
        Guid GetCurrentUserId();
    }
}
