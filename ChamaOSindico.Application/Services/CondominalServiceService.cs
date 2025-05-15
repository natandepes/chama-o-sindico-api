using ChamaOSindico.Application.Commom;
using ChamaOSindico.Application.DTOs;
using ChamaOSindico.Application.DTOs.Vehicles;
using ChamaOSindico.Application.Interfaces;
using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Application.Services
{
    public class CondominalServiceService : ICondominalServiceService
    {
        private readonly ICondominalServiceRepository _condominalServiceRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly IServiceCommentRepository _serviceCommentRepository;
        public CondominalServiceService(ICondominalServiceRepository condominalServiceRepository, IServiceCommentRepository serviceCommentRepository, IResidentRepository residentRepository)
        {
            _condominalServiceRepository = condominalServiceRepository;
            _serviceCommentRepository = serviceCommentRepository;
            _residentRepository = residentRepository;
        }

        public async Task<ApiResponse<List<CondominalServiceDTO>>> GetAllServices()
        {
            var services = await _condominalServiceRepository.GetAllServices();
            var servicesDto = services.Select(x =>
            {
                return CondominalServiceDTO.TransformBack(x);
            }).ToList();


            return ApiResponse<List<CondominalServiceDTO>>.SuccessResult(servicesDto, null);
        } 

        public async Task<ApiResponse<CondominalServiceDTO>> GetServiceById(string id)
        {
            var service = await _condominalServiceRepository.GetServiceById(id);
            var serviceDto = CondominalServiceDTO.TransformBack(service);
            return ApiResponse<CondominalServiceDTO>.SuccessResult(serviceDto, null);
        }

        public async Task<ApiResponse<string>> SaveService(CondominalServiceDTO condominalDTO)
        {
            var condominal = condominalDTO.Transform();
            var serviceId = "";
            if(condominal.Id is not null)
            {
                serviceId = await _condominalServiceRepository.UpdateServiceAsync(condominal.Id, condominal);
            }
            else
            {
                serviceId = await _condominalServiceRepository.CreateServiceAsync(condominal);
            }

            return ApiResponse<string>.SuccessResult(serviceId, "Service Salvo com Sucesso !");
        }
        
        public async Task<ApiResponse<string>> DeleteService(string serviceId)
        {
            if(!string.IsNullOrEmpty(serviceId))
            {
                await _condominalServiceRepository.DeleteVehicleAsync(serviceId);
            }

            return ApiResponse<string>.SuccessResult(null, "Service Removido com Sucesso !");
        }

        public async Task<ApiResponse<string>> CreateServiceComment(ServiceCommentDTO serviceCommentDto)
        {
            var serviceComment = serviceCommentDto.Transform();
            var resident = await _residentRepository.GetResidentByUserIdAsync(serviceCommentDto.CommentByUserId);
            serviceComment.CommentByUserName = resident.Name;
            await _serviceCommentRepository.CreateServiceComment(serviceComment);

            return ApiResponse<string>.SuccessResult(null, "Comentário Salvo com Sucesso !");
        }

        public async Task<ApiResponse<List<ServiceCommentDTO>>> GetServiceComments(string serviceId)
        {
            var comments = await _serviceCommentRepository.GetServiceComments(serviceId);

            var commentsDto = comments.Select(x =>
            {
                return ServiceCommentDTO.TransformBack(x);
            }).ToList();

            return ApiResponse<List<ServiceCommentDTO>>.SuccessResult(commentsDto, null);
        }
    }
}
