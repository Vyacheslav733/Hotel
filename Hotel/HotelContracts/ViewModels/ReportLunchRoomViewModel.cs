namespace HotelContracts.ViewModels
{
    public class ReportLunchRoomViewModel
    {
        public string LunchName { get; set; } = string.Empty;
        public List<Tuple<string, double>> MealPlans { get; set; } = new();
    }
}
