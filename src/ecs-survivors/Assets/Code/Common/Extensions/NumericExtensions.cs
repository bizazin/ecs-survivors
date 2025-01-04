namespace Code.Common.Extensions
{
    public static class NumericExtensions
    {
        public static float ZeroIfNegative(this float value)
        {
            return value >= 0 ? value : 0;
        }

        public static int ZeroIfNegative(this int value)
        {
            return value >= 0 ? value : 0;
        }
    }
}