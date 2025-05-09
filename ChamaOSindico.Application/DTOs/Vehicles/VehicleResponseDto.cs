namespace ChamaOSindico.Application.DTOs.Vehicles
{
    public record VehicleResponseDto
    {
        public string Id { get; init; }
        public string LicensePlate { get; init; }
        public string Model { get; init; }
        public string VehicleImage { get; init; }
        public int CarSpace { get; init; }
        public string ImageType { get; init; }
        public string VehicleType { get; init; }
        public string? CreatedByUserName { get; init; }
    }
}
