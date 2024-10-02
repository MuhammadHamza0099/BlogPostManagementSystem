namespace BPMS.API.Data.Abstractions
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
