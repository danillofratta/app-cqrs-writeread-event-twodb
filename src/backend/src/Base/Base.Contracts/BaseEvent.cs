namespace Base.Contracts
{
    public class BaseEvent
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Delete logic like cancel
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Register when update register
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
