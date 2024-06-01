using HotelDataModels.Models;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class LunchViewModel : ILunchModel
    {
        public int Id { get; set; }
        public int HeadwaiterId { get; set; }

        [DisplayName("Название обеда")]
        public string LunchName { get; set; } = string.Empty;

        [DisplayName("Цена обеда")]
        public double LunchPrice { get; set; }
    }
}