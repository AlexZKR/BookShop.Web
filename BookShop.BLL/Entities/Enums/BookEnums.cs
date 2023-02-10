using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShop.BLL.Entities.Enums;

public enum Genre
{
    [Display(Name = "Все")]
    All,
    [Display(Name = "Художественная")]
    Fiction,
    [Display(Name = "Нехудожественная")]
    NonFiction,
    [Display(Name = "Детская")]
    ChildrenLiterature,
    [Display(Name = "Бизнес-литература")]
    Business,
    [Display(Name = "Учебная литература")]
    Educational,
    [Display(Name = "На иностранном языке")]
    Foreign,
    [Display(Name = "Психология")]
    Psychology,
    [Display(Name = "Научно-популярная")]
    ScienceFiction,


};

public enum Cover
{
    [Display(Name = "Все")]
    All,
    [Display(Name = "Твердая")]
    HardCover,
    [Display(Name = "Мягкая")]
    SoftCover,
    [Display(Name = "Суперобложка")]
    SuperCover
};

public enum Language
{
    [Display(Name = "Все")]
    All,
    [Display(Name = "Русский")]
    Russian,
    [Display(Name = "Белорусский")]
    Belarusian,
    [Display(Name = "Английский")]
    English
};

public enum Tag
{
    None,
    Classic,
    Bestseller
};