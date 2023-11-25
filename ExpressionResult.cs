namespace WebCalcApi
{
    public class ExpressionResult
    {
        public ExpressionData Data { get; set; }

        public float Result { get; set; }

        public ExpressionResult(ExpressionData data,float result)
        {
            this.Data = data;
            this.Result = result;
        }
    }
}
