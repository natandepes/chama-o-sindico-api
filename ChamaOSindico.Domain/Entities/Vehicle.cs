using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public int CarSpace { get; set; }
        public byte[] VehicleImage { get; set; }
        public string ImageType { get; set; }
        public VehicleTypeEnum VehicleType { get; set; }
        public string CreatedByUserId { get; set; }
    }
}
