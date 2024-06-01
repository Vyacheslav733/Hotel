using HotelDataModels.Models;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class HeadwaiterViewModel : IHeadwaiterModel
    {
        public int Id { get; set; }

        [DisplayName("Фамилия метрдотеля")]
        public string HeadwaiterSurname { get; set; } = string.Empty;

        [DisplayName("Имя метрдотеля")]
        public string HeadwaiterName { get; set; } = string.Empty;

        [DisplayName("Отчество метрдотеля")]
        public string HeadwaiterPatronymic { get; set; } = string.Empty;

        [DisplayName("Логин метрдотеля")]
        public string HeadwaiterLogin { get; set; } = string.Empty;

        [DisplayName("Пароль метрдотеля")]
        public string HeadwaiterPassword { get; set; } = string.Empty;

        [DisplayName("Mail метрдотеля")]
        public string HeadwaiterEmail { get; set; } = string.Empty;

        [DisplayName("Телефон метрдотеля")]
        public string HeadwaiterPhoneNumber { get; set; } = string.Empty;
    }
}
