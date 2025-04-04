using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Domain.Entities
{
    public class Vehicle
    {
        public string? Id { get; set; }
        public string? LicensePlate { get; set; }
        public string? Model { get; set; }
        public VehicleColorEnum Color { get; set; }
        public VehicleTypeEnum VehicleType { get; set; }
    }
}
