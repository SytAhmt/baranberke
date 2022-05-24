using System.ComponentModel.DataAnnotations;

public class Kayit
{
    [Required(ErrorMessage = "Ad Boş bırakılamaz")]
    public string Ad { get; set; }

    [Required(ErrorMessage = "Soyad Boş bırakılamaz")]
    public string Soyad { get; set; }
   
    [Required(ErrorMessage = "Okul Numarası Boş bırakılamaz")] 
    [Display(Name ="Okul Numarası")]
    public string OkulNumarasi { get; set; }
   
    [Required(ErrorMessage = "Email Boş bırakılamaz")] 
    [EmailAddress]
    public string Email { get; set; }
    
[DataType(DataType.Password)]
    [Required(ErrorMessage = "Şifre Boş bırakılamaz")] 
    public string Sifre { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Şifre Tekrarı Boş bırakılamaz")] 
    [Compare("Sifre")]
    [Display(Name ="Şifre Takrarı")]
    public string SifreTekrari { get; set; }

}