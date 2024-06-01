using HotelDataModels.Models;
using Newtonsoft.Json;
using System.ComponentModel;


namespace HotelContracts.ViewModels
{
    public class ConferenceViewModel : IConferenceModel
    {
        public int Id { get; set; }

        [DisplayName("Название конференции")]
        public string ConferenceName { get; set; } = string.Empty;

        [DisplayName("Дата начала конференции")]
        public DateTime StartDate { get; set; }

        public int OrganiserId { get; set; }

        public Dictionary<int, IMemberModel> ConferenceMembers { get; set; } = new();
    }
}