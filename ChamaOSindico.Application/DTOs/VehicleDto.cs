using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs
{

    public class VehicleDto: BaseDto
    {
        public string? LicensePlate { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;
        public VehicleColorEnum Color { get; set; }
        public VehicleTypeEnum VehicleType { get; set; }

        public VehicleDto(Vehicle vehicle)
        {
        LicensePlate = vehicle.LicensePlate;
        Model = vehicle.Model;
        Color = vehicle.Color;
        VehicleType = vehicle.VehicleType;
        }
    };

    
}