namespace Film.Application.Contract.Base.Dtos
{
    public class GeneralResponseDto<T> : ResponseDto where T :BaseDto 
    {
        public T Data{ get; set; }
    }

}
