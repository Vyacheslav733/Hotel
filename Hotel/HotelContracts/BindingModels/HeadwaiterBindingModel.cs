using HotelDataModels.Models;

namespace HotelContracts.BindingModels
{
    public class HeadwaiterBindingModel : IHeadwaiterModel
    {
        public string HeadwaiterSurname { get; set; } = string.Empty;
        public string HeadwaiterName { get; set; } = string.Empty;
        public string HeadwaiterPatronymic { get; set; } = string.Empty;
        public string HeadwaiterLogin { get; set; } = string.Empty;
        public string HeadwaiterPassword { get; set; } = string.Empty;
        public string HeadwaiterEmail { get; set; } = string.Empty;
        public string HeadwaiterPhoneNumber { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}