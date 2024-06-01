using HotelContracts.ViewModels;

namespace HotelBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfoHeadwaiter
    {
        public string FileName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<ReportLunchRoomViewModel> LunchRooms { get; set; } = new();
    }
}
