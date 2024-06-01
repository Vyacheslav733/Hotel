using HotelDataModels.Models;

namespace HotelContracts.BindingModels
{
    public class OrganiserBindingModel : IOrganiserModel
    {
        public string OrganiserSurname { get; set; } = string.Empty;
        public string OrganiserName { get; set; } = string.Empty;
        public string OrganiserPatronymic { get; set; } = string.Empty;
        public string OrganiserPassword { get; set; } = string.Empty;
        public string OrganiserLogin { get; set; } = string.Empty;
        public string OrganiserEmail { get; set; } = string.Empty;
        public string OrganiserPhoneNumber { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
