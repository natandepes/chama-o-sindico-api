using ChamaOSindico.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Application.DTOs
{
    public class AreaReservationDTO
    {
        public string? Id { get; set; }
        public string? AreaId { get; set; }
        public string? ResidentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public AreaReservationStatusEnum Status { get; set; }
    }
}
