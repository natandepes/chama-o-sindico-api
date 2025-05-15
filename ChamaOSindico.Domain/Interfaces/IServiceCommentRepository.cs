using ChamaOSindico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Domain.Interfaces
{
    public interface IServiceCommentRepository
    {
        Task CreateServiceComment(ServiceComment serviceComment);
        Task<List<ServiceComment>> GetServiceComments(string serviceId);
    }
}
