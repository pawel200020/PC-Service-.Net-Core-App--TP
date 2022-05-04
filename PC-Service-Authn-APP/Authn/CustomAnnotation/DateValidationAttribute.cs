namespace Authn.CustomAnnotation
{
    using System.ComponentModel.DataAnnotations;
    public class DateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate <= DateTime.Now;
        }
    }
}
