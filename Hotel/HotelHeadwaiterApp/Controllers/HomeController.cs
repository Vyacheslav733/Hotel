using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
using HostrelHeadwaiterApp;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.ViewModels;
using HotelDataBaseImplement.Models;
using HotelDataModels.Models;
using HotelHeadwaiterApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace HotelHeadwaiterApp.Controllers
{ 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReportHeadwaiterLogic _report;

        public HomeController(ILogger<HomeController> logger, IReportHeadwaiterLogic report)
        {
            _logger = logger;
            _report = report;
        }

        public IActionResult Index()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string login, string email, string password, string surname, string name, string patronymic, string telephone)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(patronymic))
            {
                throw new Exception("Введите логин, пароль, фамилию, имя и отчество");
            }
            APIClient.PostRequest("api/headwaiter/register", new HeadwaiterBindingModel
            {
                HeadwaiterSurname = surname,
                HeadwaiterName = name,
                HeadwaiterPatronymic = patronymic,
                HeadwaiterLogin = login,
                HeadwaiterPassword = password,
                HeadwaiterEmail = email,
                HeadwaiterPhoneNumber = telephone
            });

            Response.Redirect("Enter");
            return;
        }

        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Введите логин и пароль");
            }
            APIClient.Headwaiter = APIClient.GetRequest<HeadwaiterViewModel>($"api/headwaiter/login?login={login}&password={password}");
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Неверный логин/пароль");
            }
            Response.Redirect("Index");
        }

        public IActionResult Privacy()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.Headwaiter);
        }

        [HttpPost]
        public void Privacy(string login, string email, string password, string surname, string name, string patronymic, string number)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Вы как сюда попали? Сюда вход только авторизованным");
            }
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(patronymic))
            {
                throw new Exception("Введите логин, пароль, фамилию, имя и отчество");
            }
            APIClient.PostRequest("api/headwaiter/updatedata", new HeadwaiterBindingModel
            {
                Id = APIClient.Headwaiter.Id,
                HeadwaiterSurname = surname,
                HeadwaiterName = name,
                HeadwaiterPatronymic = patronymic,
                HeadwaiterLogin = login,
                HeadwaiterPassword = password,
                HeadwaiterEmail = email,
                HeadwaiterPhoneNumber = number
            });

            APIClient.Headwaiter.HeadwaiterSurname = surname;
            APIClient.Headwaiter.HeadwaiterName = name;
            APIClient.Headwaiter.HeadwaiterPatronymic = patronymic;
            APIClient.Headwaiter.HeadwaiterLogin = login;
            APIClient.Headwaiter.HeadwaiterPassword = password;
            APIClient.Headwaiter.HeadwaiterEmail = email;
            APIClient.Headwaiter.HeadwaiterPhoneNumber = number;
            Response.Redirect("Index");
        }

        public IActionResult ListLunches()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<LunchViewModel>>($"api/lunch/getlunches?headwaiterId={APIClient.Headwaiter.Id}"));
        }

        public IActionResult CreateLunch()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public void CreateLunch(string lunchName, double lunchPrice)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            if (string.IsNullOrEmpty(lunchName))
            {
                throw new Exception("Введите имя");
            }
            if (lunchPrice < 0)
            {
                throw new Exception("Цена не может быть отрицательной");
            }
            if (string.IsNullOrEmpty(lunchPrice.ToString()))
            {
                throw new Exception("Введите цену");
            }
            APIClient.PostRequest("api/lunch/createlunch", new LunchBindingModel
            {
                LunchPrice = lunchPrice,
                LunchName = lunchName,
                HeadwaiterId = APIClient.Headwaiter.Id,
            });
            Response.Redirect("ListLunches");
        }

        public IActionResult UpdateLunch()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Lunches = APIClient.GetRequest<List<LunchViewModel>>($"api/lunch/getlunches?headwaiterId={APIClient.Headwaiter.Id}");
            return View();
        }

        [HttpPost]
        public void UpdateLunch(int lunch, string lunchName, double lunchPrice)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            if (string.IsNullOrEmpty(lunchName))
            {
                throw new Exception("Имя не может быть пустым");
            }
            if (lunchPrice < 0)
            {
                throw new Exception("Цена не может быть отрицательной");
            }
            if (string.IsNullOrEmpty(lunchPrice.ToString()))
            {
                throw new Exception("Введите цену");
            }

            APIClient.PostRequest("api/lunch/updatelunch", new LunchBindingModel
            {
                Id = lunch,
                LunchName = lunchName,
                LunchPrice = lunchPrice,
                HeadwaiterId = APIClient.Headwaiter.Id,
            });

            Response.Redirect("ListLunches");
        }

        [HttpGet]
        public LunchViewModel? GetLunch(int lunchId)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            var result = APIClient.GetRequest<LunchViewModel>($"api/lunch/getlunch?lunchId={lunchId}");
            if (result == null)
            {
                return default;
            }

            return result;
        }

        public IActionResult DeleteLunch()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Lunches = APIClient.GetRequest<List<LunchViewModel>>($"api/lunch/getlunches?headwaiterId={APIClient.Headwaiter.Id}");
            return View();
        }

        [HttpPost]
        public void DeleteLunch(int lunch)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            APIClient.PostRequest("api/lunch/deletelunch", new LunchBindingModel
            {
                Id = lunch
            });
            Response.Redirect("ListLunches");
        }

        public IActionResult ListRooms()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<RoomViewModel>>($"api/room/getrooms?headwaiterId={APIClient.Headwaiter.Id}"));
        }

		public IActionResult CreateRoom()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Lunches = APIClient.GetRequest<List<LunchViewModel>>($"api/lunch/getlunches");
            return View();
        }

        [HttpPost]
        public void CreateRoom(string roomName, string roomFrame, double roomPrice, List<int> lunches)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            if (string.IsNullOrEmpty(roomName))
            {
                throw new Exception("Введите название");
            }
            if (string.IsNullOrEmpty(roomPrice.ToString()))
            {
                throw new Exception("Введите цену");
            }
            if (roomPrice < 0)
            {
                throw new Exception("Цена не может быть отрицательной");
            }
            Dictionary<int, ILunchModel> a = new Dictionary<int, ILunchModel>();
            foreach (int lunch in lunches)
            {
                a.Add(lunch, new LunchSearchModel { Id = lunch } as ILunchModel);
            }
            APIClient.PostRequest("api/room/createroom", new RoomBindingModel
            {
                RoomName = roomName,
                RoomPrice = roomPrice,
                RoomFrame = roomFrame,
                HeadwaiterId = APIClient.Headwaiter.Id,
                RoomLunches = a
            });
            Response.Redirect("ListRooms");
        }

        public IActionResult UpdateRoom()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Rooms = APIClient.GetRequest<List<RoomViewModel>>($"api/room/getrooms?headwaiterId={APIClient.Headwaiter.Id}");
            ViewBag.Lunches = APIClient.GetRequest<List<LunchViewModel>>($"api/lunch/getlunches");
            return View();
        }

        [HttpPost]
        public void UpdateRoom(int room, string roomName, double roomPrice, string roomFrame, List<int> lunches)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            if (string.IsNullOrEmpty(roomName))
            {
                throw new Exception("Введите название");
            }
            if (string.IsNullOrEmpty(roomPrice.ToString()))
            {
                throw new Exception("Введите цену");
            }
            if (roomPrice < 0)
            {
                throw new Exception("Цена не может быть отрицательной");
            }
            Dictionary<int, ILunchModel> a = new Dictionary<int, ILunchModel>();
            foreach (int lunch in lunches)
            {
                a.Add(lunch, new LunchSearchModel { Id = lunch } as ILunchModel);
            }
            APIClient.PostRequest("api/room/updateroom", new RoomBindingModel
            {
                Id = room,
                RoomName = roomName,
                RoomPrice = roomPrice,
                RoomFrame = roomFrame,
                HeadwaiterId = APIClient.Headwaiter.Id,
                RoomLunches = a
            });
            Response.Redirect("ListRooms");
        }

		[HttpGet]
		public Tuple<RoomViewModel, List<string>>? GetRoom(int roomId)
		{
			if (APIClient.Headwaiter == null)
			{
				throw new Exception("Необходима авторизация");
			}
			var result = APIClient.GetRequest<Tuple<RoomViewModel, List<string>>>($"api/room/getroom?roomId={roomId}");
			if (result == null)
			{
				return default;
			}

			return result;
		}

		public IActionResult DeleteRoom()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Rooms = APIClient.GetRequest<List<RoomViewModel>>($"api/room/getrooms?headwaiterId={APIClient.Headwaiter.Id}");
            return View();
        }

        [HttpPost]
        public void DeleteRoom(int room)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            APIClient.PostRequest("api/room/deleteroom", new RoomBindingModel
            {
                Id = room
            });
            Response.Redirect("ListRooms");
        }

        public IActionResult ListConferenceBookings()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<ConferenceBookingViewModel>>($"api/conferencebooking/getconferenceBookings?headwaiterId={APIClient.Headwaiter.Id}"));
        }

        [HttpGet]
        public Tuple<ConferenceBookingViewModel, List<string>>? GetConferenceBooking(int conferencebookingId)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            var result = APIClient.GetRequest<Tuple<ConferenceBookingViewModel, List<string>>>($"api/conferencebooking/getconferencebooking?conferencebookingId={conferencebookingId}");
            if (result == null)
            {
                return default;
            }

            return result;
        }

        public IActionResult CreateConferenceBooking()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Lunches = APIClient.GetRequest<List<LunchViewModel>>($"api/lunch/getlunches");
            return View();
        }

        [HttpPost]
        public void CreateConferenceBooking(string nameHall, DateTime bookingDate, List<int> lunches)
        {
            if (string.IsNullOrEmpty(nameHall))
            {
                throw new Exception("Введите название");
            }
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            Dictionary<int, ILunchModel> a = new Dictionary<int, ILunchModel>();
            foreach (int lunch in lunches)
            {
                a.Add(lunch, new LunchSearchModel { Id = lunch } as ILunchModel);
            }
            APIClient.PostRequest("api/conferencebooking/createconferenceBooking", new ConferenceBookingBindingModel
            {
                NameHall = nameHall,
				BookingDate = bookingDate,
				HeadwaiterId = APIClient.Headwaiter.Id,
                ConferenceBookingLunches = a
            });
            Response.Redirect("ListConferenceBookings");
        }

        public IActionResult UpdateConferenceBooking()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.ConferenceBookings = APIClient.GetRequest<List<ConferenceBookingViewModel>>($"api/conferencebooking/getconferenceBookings?headwaiterId={APIClient.Headwaiter.Id}");
            ViewBag.Lunches = APIClient.GetRequest<List<LunchViewModel>>($"api/lunch/getlunches");
            return View();
        }

        [HttpPost]
        public void UpdateConferenceBooking(int conferenceBooking, string nameHall, DateTime bookingDate, List<int> lunches)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            if (string.IsNullOrEmpty(nameHall))
            {
                throw new Exception("Название не может быть пустым");
            }
            Dictionary<int, ILunchModel> a = new Dictionary<int, ILunchModel>();
            foreach (int lunch in lunches)
            {
                a.Add(lunch, new LunchSearchModel { Id = lunch } as ILunchModel);
            }
            APIClient.PostRequest("api/conferencebooking/updateconferenceBooking", new ConferenceBookingBindingModel
            {
                Id = conferenceBooking,
                NameHall = nameHall,
                BookingDate = bookingDate,
                HeadwaiterId = APIClient.Headwaiter.Id,
                ConferenceBookingLunches = a
            });
            Response.Redirect("ListConferenceBookings");
        }

        public IActionResult DeleteConferenceBooking()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.ConferenceBookings = APIClient.GetRequest<List<ConferenceBookingViewModel>>($"api/conferencebooking/getconferenceBookings?headwaiterId={APIClient.Headwaiter.Id}");
            return View();
        }

        [HttpPost]
        public void DeleteConferenceBooking(int conferenceBooking)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Необходима авторизация");
            }
            APIClient.PostRequest("api/conferencebooking/deleteconferenceBooking", new ConferenceBookingBindingModel
            {
                Id = conferenceBooking
            });
            Response.Redirect("ListConferenceBookings");
        }

		public IActionResult RoomMealPlans()
		{
			if (APIClient.Headwaiter == null)
			{
				return Redirect("~/Home/Enter");
			}
			ViewBag.Rooms = APIClient.GetRequest<List<RoomViewModel>>($"api/room/getrooms?headwaiterId={APIClient.Headwaiter.Id}");
			ViewBag.MealPlans = APIClient.GetRequest<List<MealPlanViewModel>>($"api/mealplan/getmealplans");
			return View();
		}

		[HttpPost]
		public void RoomMealPlans(int room, int mealplan)
		{
			if (APIClient.Headwaiter == null)
			{
				throw new Exception("Вы как сюда попали? Сюда вход только авторизованным");
			}
            var roomElem = APIClient.GetRequest<RoomViewModel>($"api/room/getroombyid?roomId={room}");
            APIClient.PostRequest("api/room/updateroom", new RoomBindingModel
			{
				Id = room,
				HeadwaiterId = APIClient.Headwaiter.Id,
                RoomName = roomElem.RoomName,
                RoomFrame = roomElem.RoomFrame,
                RoomPrice = roomElem.RoomPrice,
                MealPlanId = mealplan
			});
			Response.Redirect("Index");
		}


		[HttpGet]
		public IActionResult ListLunchRoomToFile()
		{
			if (APIClient.Headwaiter == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<LunchViewModel>>($"api/lunch/getlunches?headwaiterId={APIClient.Headwaiter.Id}"));
		}

		[HttpPost]
		public void ListLunchRoomToFile(int[] Ids, string type)
		{
			if (APIClient.Headwaiter == null)
			{
				throw new Exception("Вы как суда попали? Суда вход только авторизованным");
			}

			if (Ids.Length <= 0)
			{
				throw new Exception("Количество должно быть больше 0");
			}

			if (string.IsNullOrEmpty(type))
			{
				throw new Exception("Неверный тип отчета");
			}

			List<int> res = new List<int>();

			foreach (var item in Ids)
			{
				res.Add(item);
			}

			if (type == "docx")
			{
				APIClient.PostRequest("api/report/createheadwaiterreporttowordfile", new ReportHeadwaiterBindingModel
				{
					Ids = res,
					FileName = "C:\\Reports\\wordfile.docx"
				});
				Response.Redirect("GetWordFile");
			}
			else
			{
				APIClient.PostRequest("api/report/createheadwaiterreporttoexcelfile", new ReportHeadwaiterBindingModel
				{
					Ids = res,
					FileName = "C:\\Reports\\excelfile.xlsx"
				});
				Response.Redirect("GetExcelFile");
			}
		}

		[HttpGet]
		public IActionResult GetWordFile()
		{
			return new PhysicalFileResult("C:\\Reports\\wordfile.docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
		}


		[HttpGet]
		public IActionResult GetExcelFile()
		{
			return new PhysicalFileResult("C:\\Reports\\excelfile.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
		}

        public IActionResult GetPdfFile()
        {
            return new PhysicalFileResult("C:\\ReportsCourseWork\\pdffile.pdf", "application/pdf");
        }

        [HttpGet]
        public IActionResult ListLunchesToPdfFile()
        {
            if (APIClient.Headwaiter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View("ListLunchesToPdfFile");
        }

        [HttpPost]
        public void ListLunchesToPdfFile(DateTime dateFrom, DateTime dateTo, string headwaiterEmail)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            if (string.IsNullOrEmpty(headwaiterEmail))
            {
                throw new Exception("Email пуст");
            }
            APIClient.PostRequest("api/report/CreateHeadwaiterReportToPdfFile", new ReportHeadwaiterBindingModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                HeadwaiterId = APIClient.Headwaiter.Id
            });
            APIClient.PostRequest("api/report/SendPdfToMail", new MailSendInfoBindingModel
            {
                MailAddress = headwaiterEmail,
                Subject = "Отчет по обедам (pdf)",
                Text = "Отчет по обедам с " + dateFrom.ToShortDateString() + " до " + dateTo.ToShortDateString()
            });
            Response.Redirect("ListLunchesToPdfFile");
        }

        [HttpGet]
        public string GetLunchesReport(DateTime dateFrom, DateTime dateTo)
        {
            if (APIClient.Headwaiter == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            List<ReportLunchesViewModel> result;
            try
            {
                result = _report.GetLunches(new ReportHeadwaiterBindingModel
                {
                    HeadwaiterId = APIClient.Headwaiter.Id,
                    DateFrom = dateFrom,
                    DateTo = dateTo
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
            double sum = 0;
            string table = "";
            table += "<h2 class=\"text-custom-color-1\">Предварительный отчет</h2>";
            table += "<div class=\"table-responsive\">";
            table += "<table class=\"table table-striped table-bordered table-hover\">";
            table += "<thead class=\"table-dark\">";
            table += "<tr>";
            table += "<th scope=\"col\">Обед</th>";
            table += "<th scope=\"col\">Имя комнаты</th>";
            table += "<th scope=\"col\">Цена комнаты</th>";
            table += "<th scope=\"col\">Конференция</th>";
            table += "<th scope=\"col\">Дата</th>";
            table += "</tr>";
            table += "</thead>";
            foreach (var report in result)
            {
                bool IsCost = true;
                if (report.RoomPrice == 0)
                {
                    IsCost = false;
                }
                table += "<tbody>";
                table += "<tr>";
                table += $"<td>{report.LunchName}</td>";
                table += $"<td>{report.RoomName}</td>";
                table += $"<td>{(IsCost ? report.RoomPrice.ToString() : string.Empty)}</td>";
                table += $"<td>{report.ConferenceName}</td>";
                table += $"<td>{report.StartDate?.ToShortDateString()}</td>";
                table += "</tr>";
                table += "</tbody>";
                sum += report.RoomPrice;
            }
            table += "<tfoot class=\"table-secondary\">";
            table += $"<tr><th colspan=\"2\">Итого:</th><th>{sum}</th><th colspan=\"2\"></th></tr>";
            table += "</tfoot>";
            table += "</table>";
            table += "</div>";
            return table;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
