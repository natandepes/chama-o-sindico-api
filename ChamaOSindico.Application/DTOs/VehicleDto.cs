using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;
using System.Reflection.Metadata;

namespace ChamaOSindico.Application.DTOs
{

    public class VehicleDto: BaseDto
    {
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public string VehicleImage { get; set; }
        public int CarSpace { get; set; }
        public string ImageType { get; set; }
        public string VehicleType { get; set; }
        public string? CreatedByUserId { get; set; }

        public static VehicleDto TranslateTo(Vehicle vehicle)
        {
            return new VehicleDto()
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                VehicleImage = ConvertByteToBase64(vehicle.VehicleImage, vehicle.ImageType),
                CarSpace = vehicle.CarSpace,
                Model = vehicle.Model,
                ImageType = vehicle.ImageType,
                VehicleType = vehicle.VehicleType.ToString() == "Car" ? "Carrro" : "Moto",
            };
        }

        public Vehicle TranslateBack()
        {
            var base64Data = VehicleImage.Contains(",")
            ? VehicleImage.Split(',')[1]
            : VehicleImage;

            return new Vehicle
            {
                Id = Id,
                LicensePlate = LicensePlate,
                VehicleImage = ConvertBase64ToByte(base64Data),
                CarSpace = CarSpace,
                Model = Model,
                ImageType = ImageType,
                CreatedByUserId = CreatedByUserId!,
                VehicleType = (VehicleTypeEnum)Enum.Parse(typeof(VehicleTypeEnum), VehicleType.ToString() == "Carro" ? "Car" : "Motorcycle")
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
    };

    
}