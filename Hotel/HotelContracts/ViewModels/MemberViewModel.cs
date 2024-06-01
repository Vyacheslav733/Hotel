using HotelDataModels.Models;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class MemberViewModel : IMemberModel
    {
        public int Id { get; set; }

        [DisplayName("Фамилия участника")]
        public string MemberSurname { get; set; } = string.Empty;

        [DisplayName("Имя участника")]
        public string MemberName { get; set; } = string.Empty;

        [DisplayName("Отчество участника")]
        public string MemberPatronymic { get; set; } = string.Empty;

        [DisplayName("Номер телефона")]
        public string MemberPhoneNumber { get; set; } = string.Empty;

        public int OrganiserId { get; set; }
    }
}