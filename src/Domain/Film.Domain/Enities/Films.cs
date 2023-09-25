namespace Film.Domain.Enities
{
    public class Film : BaseEntity
    {
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
         

    }
}
