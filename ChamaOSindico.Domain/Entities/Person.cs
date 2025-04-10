﻿namespace ChamaOSindico.Domain.Entities
{
    public class Person
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
