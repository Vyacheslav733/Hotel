using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using Microsoft.Extensions.Logging;

namespace HotelBusinessLogic.BusinessLogics
{
    public class LunchLogic : ILunchLogic
    {
        private readonly ILogger _logger;
        private readonly ILunchStorage _lunchStorage;

        public LunchLogic(ILogger<LunchLogic> logger, ILunchStorage lunchStorage)
        {
            _logger = logger;
            _lunchStorage = lunchStorage;
        }

        public List<LunchViewModel>? ReadList(LunchSearchModel? model)
        {
            _logger.LogInformation("ReadList. LunchName:{LunchName}.Id:{ Id}", model?.LunchName, model?.Id);

            var list = model == null ? _lunchStorage.GetFullList() : _lunchStorage.GetFilteredList(model);

            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }

            _logger.LogInformation("ReadList. Count:{Count}", list.Count);

            return list;
        }

        public LunchViewModel? ReadElement(LunchSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _logger.LogInformation("ReadElement. LunchName:{LunchName}.Id:{Id}", model.LunchName, model.Id);

            var element = _lunchStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }

            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);

            return element;
        }

        public bool Create(LunchBindingModel model)
        {
            CheckModel(model);

            if (_lunchStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }

            return true;
        }

        public bool Update(LunchBindingModel model)
        {
            CheckModel(model);

            if (_lunchStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }

            return true;
        }

        public bool Delete(LunchBindingModel model)
        {
            CheckModel(model, false);

            _logger.LogInformation("Delete. Id:{Id}", model.Id);

            if (_lunchStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }

            return true;
        }

        private void CheckModel(LunchBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!withParams)
            {
                return;
            }

            if (string.IsNullOrEmpty(model.LunchName))
            {
                throw new ArgumentNullException("Нет имени обеда", nameof(model.LunchName));
            }

            if (model.LunchPrice < 0)
            {
                throw new ArgumentNullException("Стоимость обеда не может быть меньше 0", nameof(model.LunchPrice));
            }

            _logger.LogInformation("Lunch. LunchName:{LunchName}.LunchPrice:{ LunchPrice}. Id: { Id}", model.LunchName, model.LunchPrice, model.Id);
        }
    }
}
