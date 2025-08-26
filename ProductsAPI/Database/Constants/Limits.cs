namespace ProductsAPI.Database.Constants
{
    public static class Limits
    {
        public static readonly int MaxStringSize = (int)Math.Pow(2, 31) - 1;
        public static readonly decimal MaxDecimal = (int)Math.Pow(2, 14) - 1;
    }
}
