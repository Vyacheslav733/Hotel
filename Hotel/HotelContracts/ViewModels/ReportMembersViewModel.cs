namespace HotelContracts.ViewModels
{
    public class ReportMembersViewModel
    {
        public DateTime StartDate { get; set; }
        public string MemberSurname { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string MemberPatronymic { get; set; } = string.Empty;
        public string ConferenceName { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public double RoomPrice { get; set; }
    }
}
