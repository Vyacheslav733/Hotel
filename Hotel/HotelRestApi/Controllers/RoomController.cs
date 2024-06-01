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
	public class RoomController : Controller
	{
		private readonly ILogger _logger;
		private readonly IRoomLogic _room;

		public RoomController(ILogger<RoomController> logger, IRoomLogic room)
		{
			_logger = logger;
			_room = room;
		}

		[HttpGet]
		public List<RoomViewModel> GetRooms(int? headwaiterId = null)
		{
			try
			{
				List<RoomViewModel> res;
				if (!headwaiterId.HasValue)
				{
					res = _room.ReadList(null);
				}
				else
				{
					res = _room.ReadList(new RoomSearchModel { HeadwaiterId = headwaiterId });
				}
				foreach (var service in res)
				{
					service.RoomLunches = null;
				}
				return res;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения списка номеров");
				throw;
			}
		}

		[HttpGet]
		public Tuple<RoomViewModel, List<string>>? GetRoom(int roomId)
		{
			try
			{
				var elem = _room.ReadElement(new RoomSearchModel { Id = roomId });
				if (elem == null)
				{
					return null;
				}
				var res = Tuple.Create(elem, elem.RoomLunches.Select(x => x.Value.LunchName).ToList());
				res.Item1.RoomLunches = null!;
				return res;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка получения номера по id={Id}", roomId);
				throw;
			}
		}

        [HttpGet]
        public RoomViewModel GetRoomById(int roomId)
        {
            try
            {
                var elem = _room.ReadElement(new RoomSearchModel { Id = roomId });
				if (elem == null)
				{
					return null;
				}
                elem.RoomLunches = null!;
                return elem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения комнаты по id={Id}", roomId);
                throw;
            }
        }

        [HttpPost]
		public void CreateRoom(RoomBindingModel model)
		{
			try
			{
				_room.Create(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка создания номера");
				throw;
			}
		}

		[HttpPost]
		public void UpdateRoom(RoomBindingModel model)
		{
			try
			{
				model.RoomLunches = null!;
				_room.Update(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка обновления данных номера");
				throw;
			}
		}

		[HttpPost]
		public void DeleteRoom(RoomBindingModel model)
		{
			try
			{
				_room.Delete(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка удаления номера");
				throw;
			}
		}
	}
}
