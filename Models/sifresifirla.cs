using System.ComponentModel.DataAnnotations;
public class SifreSifirla
{
    [Required(ErrorMessage = "Email boş bırakılamaz")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Token boş bırakılamaz")]
    public string token { get; set; }

    [Display(Name = "Şifre ")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Şifre boş bırakılamaz")]
    public string Sifre { get; set; }

    [Compare("Sifre")]
    [Display(Name = "Şifre Tekrarı")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Şifre tekrarı boş bırakılamaz")]
    public string SifreTekrari { get; set; }
}