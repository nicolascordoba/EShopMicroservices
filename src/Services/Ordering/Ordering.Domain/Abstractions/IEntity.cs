namespace Ordering.Domain.Abstractions
{
    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
    public interface IEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string? Createdby { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedby { get; set; }
    }
}
