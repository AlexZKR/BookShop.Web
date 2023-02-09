using System.ComponentModel.DataAnnotations;

namespace BookShop.BLL.Entities.Enums;

public enum PaymentType
{
    [Display(Name = "Наличные")]
    Cash,
    [Display(Name = "Карта")]
    PaymentCard
};

public enum DeliveryType
{
    [Display(Name = "Стандартная доставка")]
    FreeShipment,
    [Display(Name = "Самовывоз")]
    Self_delivery,
    [Display(Name = "Почтой")]
    PostShipment
}

public enum Region
{
    [Display(Name = "Минск")]
    Minsk,
    [Display(Name = "Минская обл.")]
    Minskaya,
    [Display(Name = "Гродненская")]
    Hrodno,
    [Display(Name = "Могилевская")]
    Mogilev,
    [Display(Name = "Брестская")]
    Brest,
    [Display(Name = "Витебская")]
    Vitebsk,
    [Display(Name = "Гомельская")]
    Homel,
}