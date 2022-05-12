namespace ButterflySystems.EntityDataContracts.ResponseDataModels
{
    public class CalculationResponse
    {
        public DateTimeOffset processDateTimeOffset = DateTimeOffset.Now;

        public string Result { get; set; }
    }
}