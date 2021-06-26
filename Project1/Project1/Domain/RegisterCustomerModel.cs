using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class RegisterCustomerModel
    {
        [Required]
        [StringLength(20, ErrorMessage ="Must be between 5 and 25 characters", MinimumLength = 5)]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        //Yoinked the above annotation from here: https://liftcodeplay.com/2017/07/15/asp-net-core-password-complexity-validation-using-a-regular-expression-in-a-view-model/
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "Must be a 2 character state abbreviation")]
        public string AddressState { get; set; }
    }
}
