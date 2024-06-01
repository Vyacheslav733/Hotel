namespace HotelDataModels.Models
{
    public interface IRoomModel : IId
    {
        string RoomName { get; }
        string RoomFrame { get; }
        double RoomPrice { get; }
        int? MealPlanId { get; }
        int HeadwaiterId { get; }
        public Dictionary<int, ILunchModel> RoomLunches { get; }
    }
}