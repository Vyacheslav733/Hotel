namespace HotelContracts.ViewModels
{
    public class ReportMemberConferenceViewModel
    {
        public string MemberSurname { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string MemberPatronymic { get; set; } = string.Empty;
        public List<Tuple<string, DateTime>> ConferenceBookings { get; set; } = new();
    }
}
