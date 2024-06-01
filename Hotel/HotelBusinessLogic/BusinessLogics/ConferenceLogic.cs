using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelDataModels.Models;
using Microsoft.Extensions.Logging;

namespace HotelBusinessLogic.BusinessLogics
{
    public class ConferenceLogic : IConferenceLogic
    {
        private readonly ILogger _logger;
        private readonly IConferenceStorage _conferenceStorage;

        public ConferenceLogic(ILogger<ConferenceLogic> logger, IConferenceStorage conferenceStorage)
        {
            _logger = logger;
            _conferenceStorage = conferenceStorage;
        }

        public List<ConferenceViewModel>? ReadList(ConferenceSearchModel? model)
        {
            _logger.LogInformation("ReadList. ConferenceName:{ConferenceName}.Id:{ Id}", model?.ConferenceName, model?.Id);

            var list = model == null ? _conferenceStorage.GetFullList() : _conferenceStorage.GetFilteredList(model);

            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }

            _logger.LogInformation("ReadList. Count:{Count}", list.Count);

            return list;
        }

        public ConferenceViewModel? ReadElement(ConferenceSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _logger.LogInformation("ReadElement. ConferenceName:{ConferenceName}.Id:{Id}", model.ConferenceName, model.Id);

            var element = _conferenceStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }

            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);

            return element;
        }

        public bool Create(ConferenceBindingModel model)
        {
            CheckModel(model);

            if (_conferenceStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }

            return true;
        }

        public bool Update(ConferenceBindingModel model)
        {
            CheckModel(model);

            if (_conferenceStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }

            return true;
        }

        public bool Delete(ConferenceBindingModel model)
        {
            CheckModel(model, false);

            _logger.LogInformation("Delete. Id:{Id}", model.Id);

            if (_conferenceStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }

            return true;
        }

        private void CheckModel(ConferenceBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!withParams)
            {
                return;
            }

            if (string.IsNullOrEmpty(model.ConferenceName))
            {
                throw new ArgumentNullException("Нет названия конференции", nameof(model.ConferenceName));
            }

            _logger.LogInformation("Conference. ConferenceName:{ConferenceName}.StartDate:{ StartDate}. Id: { Id}", model.ConferenceName, model.StartDate, model.Id);
        }
    }
}
