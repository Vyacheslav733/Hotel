using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.ViewModels;
using HotelDataBaseImplement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConferenceController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConferenceLogic _conference;

        public ConferenceController(ILogger<ConferenceController> logger, IConferenceLogic conference)
        {
            _logger = logger;
            _conference = conference;
        }

		[HttpGet]
		public List<ConferenceViewModel>? GetConferences(int? organiserId = null)
		{
			try
			{
				List<ConferenceViewModel> res;
				if (!organiserId.HasValue)
				{
					res = _conference.ReadList(null);
				}
				else
				{
					res = _conference.ReadList(new ConferenceSearchModel { OrganiserId = organiserId });
				}
				foreach (var conference in res)
				{
					conference.ConferenceMembers = null!;
				}
				return res;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения конференций");
				throw;
			}
		}

		[HttpGet]
		public Tuple<ConferenceViewModel, List<string>>? GetConference(int conferenceId)
		{
			try
			{
				var elem = _conference.ReadElement(new ConferenceSearchModel { Id = conferenceId });
				if (elem == null)
				{
					return null;
				}
				var res = Tuple.Create(elem, elem.ConferenceMembers.Select(x => $"{x.Value.MemberSurname} {x.Value.MemberName} {x.Value.MemberPatronymic}").ToList());
				res.Item1.ConferenceMembers = null!;
				return res;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения конференции по id={Id}", conferenceId);
				throw;
			}
		}

		[HttpPost]
        public void CreateConference(ConferenceBindingModel model)
        {
            try
            {
                _conference.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания конференции");
                throw;
            }
        }

        [HttpPost]
        public void UpdateConference(ConferenceBindingModel model)
        {
            try
            {
                model.ConferenceMembers = null!;
                _conference.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления данных");
                throw;
            }
        }

        [HttpPost]
        public void DeleteConference(ConferenceBindingModel model)
        {
            try
            {
                _conference.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления конференции");
                throw;
            }
        }
    }
}
