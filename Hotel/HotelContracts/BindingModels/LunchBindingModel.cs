using HotelDataModels.Models;

namespace HotelContracts.BindingModels
{
    public class LunchBindingModel : ILunchModel
    {
        public string LunchName { get; set; } = string.Empty;
        public double LunchPrice { get; set; }
        public int HeadwaiterId { get; set; }
        public int Id { get; set; }
    }
}