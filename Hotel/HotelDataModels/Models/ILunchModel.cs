namespace HotelDataModels.Models
{
    public interface ILunchModel : IId
    {
        string LunchName { get; }
        double LunchPrice { get; }
        int HeadwaiterId { get; }
    }
}