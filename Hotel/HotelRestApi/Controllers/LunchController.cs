using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LunchController : Controller
    {
        private readonly ILogger _logger;
        private readonly ILunchLogic _lunch;

        public LunchController(ILogger<LunchController> logger, ILunchLogic lunch)
        {
            _logger = logger;
            _lunch = lunch;
        }

		[HttpGet]
		public List<LunchViewModel>? GetLunches(int? headwaiterId = null)
		{
			try
			{
                if (!headwaiterId.HasValue)
                {
                    return _lunch.ReadList(null);
                }
				return _lunch.ReadList(new LunchSearchModel
				{
					HeadwaiterId = headwaiterId
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения списка обедов");
				throw;
			}
		}

		[HttpGet]
		public LunchViewModel GetLunch(int lunchId)
		{
			try
			{
				var elem = _lunch.ReadElement(new LunchSearchModel { Id = lunchId });
				return elem;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения обеда по id={Id}", lunchId);
				throw;
			}
		}

		[HttpPost]
        public void CreateLunch(LunchBindingModel model)
        {
            try
            {
                _lunch.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания обеда");
                throw;
            }
        }

        [HttpPost]
        public void UpdateLunch(LunchBindingModel model)
        {
            try
            {
                _lunch.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления данных обеда");
                throw;
            }
        }

        [HttpPost]
        public void DeleteLunch(LunchBindingModel model)
        {
            try
            {
                _lunch.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления обеда");
                throw;
            }
        }
    }
}
