using HotelDataModels.Models;
using System.ComponentModel;

namespace HotelContracts.BindingModels
{
    public class RoomBindingModel : IRoomModel
    {
        [DisplayName("Название комнаты")]
        public string RoomName { get; set; } = string.Empty;

        [DisplayName("Корпус комнаты")]
        public string RoomFrame { get; set; } = string.Empty; 
        public double RoomPrice { get; set; }
        public int? MealPlanId { get; set; }
        public int HeadwaiterId { get; set; }
        public int Id { get; set; }
        public Dictionary<int, ILunchModel> RoomLunches { get; set; } = new();
    }
}