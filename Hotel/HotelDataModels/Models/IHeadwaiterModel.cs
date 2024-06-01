namespace HotelDataModels.Models
{
    public interface IHeadwaiterModel : IId
    {
        string HeadwaiterSurname { get; }
        string HeadwaiterName { get; }
        string HeadwaiterPatronymic { get; }
        string HeadwaiterLogin { get; }
        string HeadwaiterPassword { get; }
        string HeadwaiterEmail { get; }
        string HeadwaiterPhoneNumber { get; }
    }
}