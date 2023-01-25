using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum Genre
{
    [Display(Name = "Художественная")]
    Fiction,
    [Display(Name = "Нехудожественная")]
    NonFiction,
    [Display(Name = "Научно-популярная")]
    ScienceFiction,
    [Display(Name = "Детская")]
    ChildrenLiterature,

};

public enum Cover
{
    [Display(Name = "Твердая")]
    HardCover,
    [Display(Name = "Мягкая")]
    SoftCover,
    [Display(Name = "Суперобложка")]
    SuperCover
};

public enum Language
{
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