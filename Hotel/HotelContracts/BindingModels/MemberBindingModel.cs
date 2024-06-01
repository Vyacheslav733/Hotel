using HotelDataModels.Models;

namespace HotelContracts.BindingModels
{
    public class MemberBindingModel : IMemberModel
    {
        public string MemberSurname { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string MemberPatronymic { get; set; } = string.Empty;
        public string MemberPhoneNumber { get; set; } = string.Empty;
        public int OrganiserId { get; set; }
        public int Id { get; set; }
    }
}
