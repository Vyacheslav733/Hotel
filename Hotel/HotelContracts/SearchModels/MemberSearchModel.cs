namespace HotelContracts.SearchModels
{
    public class MemberSearchModel
    {
        public int? Id { get; set; }
        public string? MemberSurname { get; set; }
        public string? MemberName { get; set; }
        public string? MemberPatronymic { get; set; }
        public int? OrganiserId { get; set; }
    }
}