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
    public class MemberController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMemberLogic _member;

        public MemberController(ILogger<MemberController> logger, IMemberLogic member)
        {
            _logger = logger;
            _member = member;
        }

		[HttpGet]
		public List<MemberViewModel>? GetMembers(int? organiserId = null)
		{
			try
			{
				if (!organiserId.HasValue)
				{
					return _member.ReadList(null);
				}
				return _member.ReadList(new MemberSearchModel
				{
					OrganiserId = organiserId
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения списка участников");
				throw;
			}
		}

		[HttpGet]
		public MemberViewModel GetMember(int memberId)
		{
			try
			{
				var elem = _member.ReadElement(new MemberSearchModel { Id = memberId });
				return elem;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения участника по id={Id}", memberId);
				throw;
			}
		}

		[HttpPost]
        public void CreateMember(MemberBindingModel model)
        {
            try
            {
                _member.Create(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания участника");
                throw;
            }
        }

        [HttpPost]
        public void UpdateMember(MemberBindingModel model)
        {
            try
            {
                _member.Update(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления данных");
                throw;
            }
        }

        [HttpPost]
        public void DeleteMember(MemberBindingModel model)
        {
            try
            {
                _member.Delete(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления участника");
                throw;
            }
        }
    }
}
