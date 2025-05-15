using ChamaOSindico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Application.DTOs
{
    public class CondominalServiceDTO
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public string ProviderPhotoUrl { get; set; }
        public string ImageType { get; set; }
        public string ProviderName { get; set; }
        public string Cellphone { get; set; }
        public string? Description { get; set; }


        public CondominalService Transform()
        {
            var photoData = ProviderPhotoUrl.Split(',')[1];
            return new CondominalService(Id, Title, ConvertBase64ToByte(photoData), ImageType , ProviderName, Cellphone, Description);
        }

        public static CondominalServiceDTO TransformBack(CondominalService condominalService)
        {
            return new CondominalServiceDTO()
            {
                Id = condominalService.Id,
                Title = condominalService.Title,
                Description = condominalService.Description,
                ProviderName = condominalService.ProviderName,
                ProviderPhotoUrl = ConvertByteToBase64(condominalService.ProviderPhotoUrl, condominalService.ImageType),
                ImageType = condominalService.ImageType,
                Cellphone = condominalService.Cellphone
            };
        }

        private byte[] ConvertBase64ToByte(string base64Data)
        {
            return Convert.FromBase64String(base64Data);
        }

        private static string ConvertByteToBase64(byte[] byteData, string imageType)
        {
            return $"data:{imageType};base64,{Convert.ToBase64String(byteData)}";
        }
    }
}
