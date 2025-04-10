﻿using ChamaOSindico.Domain.Enums;

namespace ChamaOSindico.Domain.Entities
{
    public class AreaReservation : BaseEntity
    {
  
        public string? AreaId { get; set; }
        public string? ResidentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public AreaReservationStatusEnum Status { get; set; }
    }
}
