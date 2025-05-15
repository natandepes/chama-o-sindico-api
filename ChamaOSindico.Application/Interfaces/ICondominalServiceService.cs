using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Application.Interfaces
{
    public interface ICondominalServiceService
    {
        Task<ApiResponse<List<CondominalServiceDTO>>> GetAllServices();
        Task<ApiResponse<CondominalServiceDTO>> GetServiceById(string id);
        Task<ApiResponse<string>> SaveService(CondominalServiceDTO condominalService);
        Task<ApiResponse<string>> DeleteService(string id);
    }
}
