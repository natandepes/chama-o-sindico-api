﻿namespace ChamaOSindico.Domain.Entities
{
    public class Warnings : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
