using System.ComponentModel.DataAnnotations;

namespace StaffInfoTracker.ViewModels;

public class AddressViewModel
{
    public int AddressId { get; set; }

    [Display(Name = "Країна")]
    [Required(ErrorMessage = "Це поле не повинно бути порожнім")]
    [MaxLength(25, ErrorMessage = "Довжина цього поля не може перевищувати 25 симолів")]
    public string Country { get; set; } = null!;

    [Display(Name = "Місто")]
    [Required(ErrorMessage = "Це поле не повинно бути порожнім")]
    [MaxLength(25, ErrorMessage = "Довжина цього поля не може перевищувати 25 симолів")]
    public string City { get; set; } = null!;

    [Display(Name = "Вулиця")]
    [Required(ErrorMessage = "Це поле не повинно бути порожнім")]
    [MaxLength(25, ErrorMessage = "Довжина цього поля не може перевищувати 25 симолів")]
    public string Street { get; set; } = null!;

    [Display(Name = "Номер будинку")]
    [Required(ErrorMessage = "Це поле не повинно бути порожнім")]
    [MaxLength(10, ErrorMessage = "Довжина цього поля не може перевищувати 10 симолів")]
    public string HouseNumber { get; set; } = null!;

    [Display(Name = "Номер квартири")]
    [Range(1, 1000, ErrorMessage = "Діапазон даних від 1 до 1000")]
    public short? ApartmentNumber { get; set; }

    [Display(Name = "Поштовий індекс")]
    [MaxLength(5, ErrorMessage = "Довжина цього поля не може перевищувати 5 симолів")]
    public string? PostIndex { get; set; }
}
