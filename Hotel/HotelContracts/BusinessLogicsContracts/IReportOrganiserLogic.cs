using HotelContracts.BindingModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface IReportOrganiserLogic
    {
        List<ReportMemberConferenceViewModel> GetMemberConferenceBooking(List<int> Ids);
        List<ReportMembersViewModel> GetMembers(ReportOrganiserBindingModel model);
        void SaveMemberConferenceToWordFile(ReportOrganiserBindingModel model);
        void SaveMemberConferenceToExcelFile(ReportOrganiserBindingModel model);
        void SaveMembersToPdfFile(ReportOrganiserBindingModel model);
    }
}
