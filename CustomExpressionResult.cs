namespace WebCalcApi
{
    public class CustomExpressionResult
    {
        public CustomExpressionData Data {  get; set; }

        public float Result {  get; set; }

        public CustomExpressionResult(CustomExpressionData data, float result)
        {
            Data = data;
            Result = result;
        }
    }
}
