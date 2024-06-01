namespace HotelDataModels.Models
{
    public interface IOrganiserModel : IId
    {
        string OrganiserSurname { get; }
        string OrganiserName { get; }
        string OrganiserPatronymic { get; }
        string OrganiserLogin { get; }
        string OrganiserPassword { get; }
        string OrganiserEmail { get; }
        string OrganiserPhoneNumber { get; }
    }
}
