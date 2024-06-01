using HotelDataModels.Models;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class OrganiserViewModel : IOrganiserModel
    {
        public int Id { get; set; }

        [DisplayName("Фамилия организатора")]
        public string OrganiserSurname { get; set; } = string.Empty;

        [DisplayName("Имя организатора")]
        public string OrganiserName { get; set; } = string.Empty;

        [DisplayName("Отчество организатора")]
        public string OrganiserPatronymic { get; set; } = string.Empty;

        [DisplayName("Логин организатора")]
        public string OrganiserLogin { get; set; } = string.Empty;

        [DisplayName("Пароль организатора")]
        public string OrganiserPassword { get; set; } = string.Empty;

        [DisplayName("Mail организатора")]
        public string OrganiserEmail { get; set; } = string.Empty;

        [DisplayName("Телефон организатора")]
        public string OrganiserPhoneNumber { get; set; } = string.Empty;
    }
}