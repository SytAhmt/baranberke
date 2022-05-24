
using System.ComponentModel.DataAnnotations;
public class giris
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email Alanı Gereklidir.")]
    [EmailAddress(ErrorMessage = "Geçerli Bir Email Adresi Giriniz")]
    public string Email { get; set; }

    [Display(Name = "Şifre")]
    [MaxLength(16, ErrorMessage = "Şifre en fazla 16 karakter girilmeli.")]
    [MinLength(6, ErrorMessage = "Şifren az 6 karakter girilmeli")]
    [Required(ErrorMessage = "Şifre Alanı Gereklidir.")]
    [DataType(DataType.Password)]
    public string Sifre { get; set; }
}