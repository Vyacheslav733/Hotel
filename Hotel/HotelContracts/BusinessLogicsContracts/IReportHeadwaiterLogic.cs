using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IReportHeadwaiterLogic
    {
        List<ReportLunchRoomViewModel> GetLunchRoom(List<int> Ids);
        List<ReportLunchesViewModel> GetLunches(ReportHeadwaiterBindingModel model);
        void SaveLunchRoomToWordFile(ReportHeadwaiterBindingModel model);
        void SaveLunchRoomToExcelFile(ReportHeadwaiterBindingModel model);
        void SaveLunchesToPdfFile(ReportHeadwaiterBindingModel model);
    }
}
