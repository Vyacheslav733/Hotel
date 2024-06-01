using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelDataModels.Models;
using Microsoft.Extensions.Logging;

namespace HotelBusinessLogic.BusinessLogics
{
    public class ConferenceBookingLogic : IConferenceBookingLogic
    {
        private readonly ILogger _logger;
        private readonly IConferenceBookingStorage _conferenceBookingStorage;

        public ConferenceBookingLogic(ILogger<ConferenceBookingLogic> logger, IConferenceBookingStorage conferenceBookingStorage)
        {
            _logger = logger;
            _conferenceBookingStorage = conferenceBookingStorage;
        }

        public List<ConferenceBookingViewModel>? ReadList(ConferenceBookingSearchModel? model)
        {
            _logger.LogInformation("ReadList. Id:{ Id}", model?.Id);

            var list = model == null ? _conferenceBookingStorage.GetFullList() : _conferenceBookingStorage.GetFilteredList(model);

            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }

            _logger.LogInformation("ReadList. Count:{Count}", list.Count);

            return list;
        }

        public ConferenceBookingViewModel? ReadElement(ConferenceBookingSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _logger.LogInformation("ReadElement. Id:{Id}", model.Id);

            var element = _conferenceBookingStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }

            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);

            return element;
        }

        public bool Create(ConferenceBookingBindingModel model)
        {
            CheckModel(model);

            if (_conferenceBookingStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }

            return true;
        }

        public bool Update(ConferenceBookingBindingModel model)
        {
            CheckModel(model);

            if (_conferenceBookingStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }

            return true;
        }

        public bool Delete(ConferenceBookingBindingModel model)
        {
            CheckModel(model, false);

            _logger.LogInformation("Delete. Id:{Id}", model.Id);

            if (_conferenceBookingStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }

            return true;
        }

        private void CheckModel(ConferenceBookingBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!withParams)
            {
                return;
            }

            if (string.IsNullOrEmpty(model.NameHall))
            {
                throw new ArgumentNullException("Нет названия конференции", nameof(model.NameHall));
            }

            _logger.LogInformation("ConferenceBooking. Id: { Id}", model.Id);

        }
    }
}
