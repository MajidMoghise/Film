namespace Film.Domain.Contract.Base.Models
{
    public class BulkResponseModel : BaseModel
    {
        public List<int> InsertCodes { get; set; }
        public List<int> UpdateCodes { get; set; }
    }
}
