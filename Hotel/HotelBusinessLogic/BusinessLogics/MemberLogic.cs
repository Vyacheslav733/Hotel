using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using Microsoft.Extensions.Logging;

namespace HotelBusinessLogic.BusinessLogics
{
    public class MemberLogic : IMemberLogic
    {
        private readonly ILogger _logger;
        private readonly IMemberStorage _memberStorage;

        public MemberLogic(ILogger<MemberLogic> logger, IMemberStorage memberStorage)
        {
            _logger = logger;
            _memberStorage = memberStorage;
        }

        public List<MemberViewModel>? ReadList(MemberSearchModel? model)
        {
            _logger.LogInformation("ReadList. MemberSurname:{MemberSurname}.MemberName:{MemberName}." +
                "MemberPatronymic:{MemberPatronymic}.Id:{ Id}", model?.MemberSurname, model?.MemberName, model?.MemberPatronymic, model?.Id);

            var list = model == null ? _memberStorage.GetFullList() : _memberStorage.GetFilteredList(model);

            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }

            _logger.LogInformation("ReadList. Count:{Count}", list.Count);

            return list;
        }

        public MemberViewModel? ReadElement(MemberSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _logger.LogInformation("ReadList. MemberSurname:{MemberSurname}.MemberName:{MemberName}." +
                "MemberPatronymic:{MemberPatronymic}.Id:{ Id}", model?.MemberSurname, model?.MemberName, model?.MemberPatronymic, model?.Id);

            var element = _memberStorage.GetElement(model);

            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }

            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);

            return element;
        }

        public bool Create(MemberBindingModel model)
        {
            CheckModel(model);

            if (_memberStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }

            return true;
        }

        public bool Update(MemberBindingModel model)
        {
            CheckModel(model);

            if (_memberStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }

            return true;
        }

        public bool Delete(MemberBindingModel model)
        {
            CheckModel(model, false);

            _logger.LogInformation("Delete. Id:{Id}", model.Id);

            if (_memberStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }

            return true;
        }
        
        private void CheckModel(MemberBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!withParams)
            {
                return;
            }

            if (string.IsNullOrEmpty(model.MemberSurname))
            {
                throw new ArgumentNullException("Нет фамилии участника", nameof(model.MemberSurname));
            }

            if (string.IsNullOrEmpty(model.MemberName))
            {
                throw new ArgumentNullException("Нет имени участника", nameof(model.MemberName));
            }

            if (string.IsNullOrEmpty(model.MemberPatronymic))
            {
                throw new ArgumentNullException("Нет отчества участника", nameof(model.MemberPatronymic));
            }

            if (string.IsNullOrEmpty(model.MemberPhoneNumber))
            {
                throw new ArgumentNullException("Не указан номер телефона участника", nameof(model.MemberPhoneNumber));
            }

            _logger.LogInformation("ReadList. MemberSurname:{MemberSurname}.MemberName:{MemberName}." +
                "MemberPatronymic:{MemberPatronymic}.MemberPhoneNumber:{MemberPhoneNumber}.Id:{ Id}", model?.MemberSurname, model?.MemberName, model?.MemberPatronymic, model?.MemberPhoneNumber, model?.Id);
        }
    }
}
