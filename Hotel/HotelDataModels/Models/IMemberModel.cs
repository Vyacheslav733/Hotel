namespace HotelDataModels.Models
{
    public interface IMemberModel : IId
    {
        string MemberSurname { get; }
        string MemberName { get; }
        string MemberPatronymic { get; }
        string MemberPhoneNumber { get; }
        int OrganiserId { get; }
    }
}
