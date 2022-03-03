using System.ComponentModel.DataAnnotations;

namespace AlKardeslerTicaretApp.Models
{
    public enum eEnumOrderState
    {
        [Display(Name= "Bekliyor")]
        Waiting,
        [Display(Name = "Sipariş Hazırlanıyor")]
        Preparing,
        [Display(Name = "Sipariş Teslimata Hazır")]
        ReadyForDelivery,
        [Display(Name = "Sipariş Teslim Edildi")]
        Delivered
    }
}