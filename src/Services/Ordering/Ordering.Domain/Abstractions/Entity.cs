namespace Ordering.Domain.Abstractions
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Createdby { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedby { get; set; }
    }
}
