using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
public class AppUser : IdentityUser
{
    [PersonalData]

    public string? Ad { get; set; }
    [PersonalData]
    [Required(ErrorMessage = "Soyad Boş bırakılamaz")]
    public string? Soyad { get; set; }


    [Required(ErrorMessage = "Okul Numarası bırakılamaz")]
    [StringLength(5)]
    public string OkulNumarasi { get; set; }


}