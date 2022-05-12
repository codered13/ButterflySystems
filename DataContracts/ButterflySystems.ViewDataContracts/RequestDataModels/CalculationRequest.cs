namespace ButterflySystems.ViewDataContracts.RequestDataModels
{
    public class CalculationRequest
    {
        public double FirstOperator { get; set; }

        public string CalculationOperator
        {
            get;
            set;
        }
        public double LastOperator { get; set; }
    }
}