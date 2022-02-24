namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge( this DateTime dateOfbirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfbirth.Year;
            if(dateOfbirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}