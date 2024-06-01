using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.ViewModels;
using HotelDataBaseImplement;
using HotelDataBaseImplement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MealPlanController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMealPlanLogic _mealPlan;

        public MealPlanController(ILogger<MealPlanController> logger, IMealPlanLogic mealPlan)
        {
            _logger = logger;
            _mealPlan = mealPlan;
        }

		[HttpGet]
		public List<MealPlanViewModel>? GetMealPlans(int? organiserId = null)
		{
			try
			{
				List<MealPlanViewModel> res;
				if (!organiserId.HasValue)
				{
					res = _mealPlan.ReadList(null);
				}
				else
				{
					res = _mealPlan.ReadList(new MealPlanSearchModel { OrganiserId = organiserId });
				}
				foreach (var mealPlan in res)
				{
					mealPlan.MealPlanMembers = null!;
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
		public Tuple<MealPlanViewModel, List<string>>? GetMealPlan(int mealPlanId)
		{
			try
			{
				var elem = _mealPlan.ReadElement(new MealPlanSearchModel { Id = mealPlanId });
				if (elem == null)
				{
					return null;
				}
				var res = Tuple.Create(elem, elem.MealPlanMembers.Select(x => $"{x.Value.MemberSurname} {x.Value.MemberName} {x.Value.MemberPatronymic}").ToList());
				res.Item1.MealPlanMembers = null!;
				return res;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения плана питания по id={Id}", mealPlanId);
				throw;
			}
		}

		[HttpPost]
        public void CreateMealPlan(MealPlanBindingModel model)
        {
            try
            {
                _mealPlan.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания плана питания");
                throw;
            }
        }

        [HttpPost]
        public void UpdateMealPlan(MealPlanBindingModel model)
        {
            try
            {
                model.MealPlanMembers = null!;
                _mealPlan.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления данных");
                throw;
            }
        }

        [HttpPost]
        public void DeleteMealPlan(MealPlanBindingModel model)
        {
            try
            {
                _mealPlan.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления плана питания");
                throw;
            }
        }
    }
}
