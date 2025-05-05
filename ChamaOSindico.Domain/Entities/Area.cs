namespace ChamaOSindico.Domain.Entities
{
    public class Area : BaseEntity
    {
    
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }
        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }
    }
}
