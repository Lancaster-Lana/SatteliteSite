namespace Sattelite.Framework.Extensions
{
    using Sattelite.Framework.Contants;

    public static class ErrorMessageStringExtensions
    {
         public static string ToNotNullErrorMessage(this string source)
         {
             return string.Format(ConstantMessage.ShouldNotBeNull, source);
         }

        public static string ToExistErrorMessage(this string source)
        {
            return string.Format(ConstantMessage.ExistDuplicate, source);
        }
        
    }
}