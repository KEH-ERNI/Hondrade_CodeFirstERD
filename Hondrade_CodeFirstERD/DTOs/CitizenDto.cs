using System.ComponentModel.DataAnnotations;

namespace Hondrade_CodeFirstERD.DTOs
{
    public record struct CitizenDto (string FName, 
        string MName, 
        string LName, 
        string Address, 
        DateTime Bday, 
        string Phone, 
        string Email, 
        string UName, 
        string Password, 
        string ImgName,
        List<ApplicationDto> Applications)
    {
    }
}
