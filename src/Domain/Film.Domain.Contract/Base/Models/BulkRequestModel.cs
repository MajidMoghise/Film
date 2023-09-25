namespace Film.Domain.Contract.Base.Models
{
    public class BulkRequestModel : BaseModel
    {
        public int Code { get; set; }
        public byte[] Hash { get; set; }
    }
}
