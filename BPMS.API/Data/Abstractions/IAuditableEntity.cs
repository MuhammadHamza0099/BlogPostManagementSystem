namespace BPMS.API.Data.Abstractions
{
    public interface IAuditableEntity : IBaseEntity
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
