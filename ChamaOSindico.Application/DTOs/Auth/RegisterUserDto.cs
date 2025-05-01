using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Application.DTOs.Auth
{
    public record RegisterUserDto
    {
        // General properties
        public string Name { get; init; }
        public string Email { get; init; }
        public string Rg { get; init; }
        public string Cpf { get; init; }
        public string Phone { get; init; }
        public DateOnly BirthDate { get; init; }
        
        // Resident properties
        public int ApartmentNumber { get; init; }

        // Condominal Manager properties
        public bool IsResident { get; set; }
        public double Salary { get; set; }

        // Account properties
        public string Password { get; init; }
        public UserRoleEnum Role { get; init; }
    }
}
