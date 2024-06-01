using HotelDataModels.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class RoomViewModel : IRoomModel
    {
        public int Id { get; set; }

        [DisplayName("Название комнаты")]
        public string RoomName { get; set; } = string.Empty;

        [DisplayName("Корпус комнаты")]
        public string RoomFrame { get; set; } = string.Empty;

        [DisplayName("Стоимость комнаты")]
        public double RoomPrice { get; set; }

        public int? MealPlanId { get; set; }

        public int HeadwaiterId { get; set; }

        public Dictionary<int, ILunchModel> RoomLunches { get; set; }
    }
}
