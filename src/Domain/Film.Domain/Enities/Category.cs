namespace Film.Domain.Enities
{
    public class Category : BaseEntity
    {
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Priority { get; set; }

    }
}
