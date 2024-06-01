using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConferenceBookingController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConferenceBookingLogic _conferenceBooking;

        public ConferenceBookingController(ILogger<ConferenceBookingController> logger, IConferenceBookingLogic conferenceBooking)
        {
            _logger = logger;
            _conferenceBooking = conferenceBooking;
        }

		[HttpGet]
		public List<ConferenceBookingViewModel>? GetConferenceBookings(int? headwaiterId = null)
		{
			try
			{
				List<ConferenceBookingViewModel> res;
                if (!headwaiterId.HasValue)
                {
                    res = _conferenceBooking.ReadList(null);
                }
                else
                {
                    res = _conferenceBooking.ReadList(new ConferenceBookingSearchModel { HeadwaiterId = headwaiterId });
                }
                foreach (var conferencebooking in res)
                {
					conferencebooking.ConferenceBookingLunches = null!;
                }
				return res;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения списка бронирований по конференциям");
				throw;
			}
		}

		[HttpGet]
		public Tuple<ConferenceBookingViewModel, List<string>>? GetConferenceBooking(int conferencebookingId)
		{
			try
			{
				var elem = _conferenceBooking.ReadElement(new ConferenceBookingSearchModel { Id = conferencebookingId });
                if (elem == null)
                {
                    return null;
                }
				var res = Tuple.Create(elem, elem.ConferenceBookingLunches.Select(x => x.Value.LunchName).ToList());
				res.Item1.ConferenceBookingLunches = null!;
				return res;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения бронирования по конференции по id={Id}", conferencebookingId);
				throw;
			}
		}

		[HttpGet]
		public ConferenceBookingViewModel GetConferenceBookingById(int conferencebookingId)
		{
			try
			{
				var elem = _conferenceBooking.ReadElement(new ConferenceBookingSearchModel { Id = conferencebookingId });
				if (elem == null)
				{
					return null;
				}
				elem.ConferenceBookingLunches = null!;
				return elem;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения бронирования по конференции по id={Id}", conferencebookingId);
				throw;
			}
		}

		[HttpPost]
        public void CreateConferenceBooking(ConferenceBookingBindingModel model)
        {
            try
            {
                _conferenceBooking.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания бронирования по конференции");
                throw;
            }
        }

        [HttpPost]
        public void UpdateConferenceBooking(ConferenceBookingBindingModel model)
        {

            try
            {
                model.ConferenceBookingLunches = null!;
                _conferenceBooking.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления данных бронирования по конференции");
                throw;
            }
        }

        [HttpPost]
        public void DeleteConferenceBooking(ConferenceBookingBindingModel model)
        {
            try
            {
                _conferenceBooking.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления бронирования по конференции");
                throw;
            }
        }
    }
}
