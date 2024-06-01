using HotelContracts.BindingModels;
using HotelContracts.SearchModels;
using HotelContracts.ViewModels;

namespace HotelContracts.StoragesContracts
{
    public interface ILunchStorage
    {
        List<LunchViewModel> GetFullList();    
        List<LunchViewModel> GetFilteredList(LunchSearchModel model);
        LunchViewModel? GetElement(LunchSearchModel model);
        LunchViewModel? Insert(LunchBindingModel model);
        LunchViewModel? Update(LunchBindingModel model);
        LunchViewModel? Delete(LunchBindingModel model);
    }
}
