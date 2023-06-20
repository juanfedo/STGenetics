using Application.Constants;

namespace Application.Validation
{
    public static class ValidateInput
    {
        public static bool ValidateSex(string? input)
        {
            if (input != null)
            {
                return Constants.Constants.SexTypes.Contains(input.Trim().ToLower());
            }
            return false;
        }

        public static bool ValidateStatus(string? input)
        { 
            if (input != null)
            {
                return Constants.Constants.StatusTypes.Contains(input.Trim().ToLower());
            }
            return false;
        }
    }
}
