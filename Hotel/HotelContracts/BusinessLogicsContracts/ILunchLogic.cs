using HotelContracts.BindingModels;
using HotelContracts.SearchModels;
using HotelContracts.ViewModels;

namespace HotelContracts.BusinessLogicsContracts
{
    public interface ILunchLogic
    {
        List<LunchViewModel>? ReadList(LunchSearchModel? model);
        LunchViewModel? ReadElement(LunchSearchModel model);
        bool Create(LunchBindingModel model);
        bool Update(LunchBindingModel model);
        bool Delete(LunchBindingModel model);
    }
}
