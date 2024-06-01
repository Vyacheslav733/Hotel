using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using HotelBusinessLogic.BusinessLogics;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.ViewModels;
using HotelDataModels.Models;
using HotelOrganiserApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelOrganiserApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReportOrganiserLogic _report;

        public HomeController(ILogger<HomeController> logger, IReportOrganiserLogic report)
        {
            _logger = logger;
            _report = report;
        }

        public IActionResult Index()
        {
            if (APIClient.Organiser == null)
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
            APIClient.PostRequest("api/organiser/register", new OrganiserBindingModel
            {
                OrganiserSurname = surname,
                OrganiserName = name,
                OrganiserPatronymic = patronymic,
                OrganiserLogin = login,
                OrganiserPassword = password,
                OrganiserEmail = email,
                OrganiserPhoneNumber = telephone
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
            APIClient.Organiser = APIClient.GetRequest<OrganiserViewModel>($"api/organiser/login?login={login}&password={password}");
            if (APIClient.Organiser == null)
            {
                throw new Exception("Неверный логин/пароль");
            }
            Response.Redirect("Index");
        }

        public IActionResult Privacy()
        {
            if (APIClient.Organiser == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.Organiser);
        }

        [HttpPost]
        public void Privacy(string login, string email, string password, string surname, string name, string patronymic, string telephone)
        {
            if (APIClient.Organiser == null)
            {
                throw new Exception("Вы как сюда попали? Сюда вход только авторизованным");
            }
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(patronymic))
            {
                throw new Exception("Введите логин, пароль, фамилию, имя и отчество");
            }
            APIClient.PostRequest("api/organiser/updatedata", new OrganiserBindingModel
            {
                Id = APIClient.Organiser.Id,
                OrganiserSurname = surname,
                OrganiserName = name,
                OrganiserPatronymic = patronymic,
                OrganiserLogin = login,
                OrganiserPassword = password,
                OrganiserEmail = email,
                OrganiserPhoneNumber = telephone
            });

            APIClient.Organiser.OrganiserSurname = surname;
            APIClient.Organiser.OrganiserName = name;
            APIClient.Organiser.OrganiserPatronymic = patronymic;
            APIClient.Organiser.OrganiserLogin = login;
            APIClient.Organiser.OrganiserPassword = password;
            APIClient.Organiser.OrganiserEmail = email;
            APIClient.Organiser.OrganiserPhoneNumber = telephone;
            Response.Redirect("Index");
        }

        public IActionResult ListMembers()
        {
            if (APIClient.Organiser == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<MemberViewModel>>($"api/member/getmembers?organiserId={APIClient.Organiser.Id}"));
        }

        public IActionResult CreateMember()
        {
            if (APIClient.Organiser == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public void CreateMember(string memberSurname, string memberName, string memberPatronymic, string memberPhoneNumber)
        {
            if (APIClient.Organiser == null)
            {
                throw new Exception("Необходима авторизация");
            }
            if (string.IsNullOrEmpty(memberSurname) || string.IsNullOrEmpty(memberName) || string.IsNullOrEmpty(memberPatronymic) || string.IsNullOrEmpty(memberPhoneNumber))
            {
                throw new Exception("Введите фамилию, имя, отчество и номер телефона");
            }
            APIClient.PostRequest("api/member/createmember", new MemberBindingModel
            {
                MemberSurname = memberSurname,
                MemberName = memberName,
                MemberPatronymic = memberPatronymic,
                MemberPhoneNumber = memberPhoneNumber,
                OrganiserId = APIClient.Organiser.Id,
            });
            Response.Redirect("ListMembers");
        }

		public IActionResult UpdateMember()
		{
			if (APIClient.Organiser == null)
			{
				return Redirect("~/Home/Enter");
			}
			ViewBag.Members = APIClient.GetRequest<List<MemberViewModel>>($"api/member/getmembers?organiserId={APIClient.Organiser.Id}");
			return View();
		}

		[HttpPost]
		public void UpdateMember(int member, string memberSurname, string memberName, string memberPatronymic, string memberPhoneNumber)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Необходима авторизация");
			}
			if (string.IsNullOrEmpty(memberSurname))
			{
				throw new Exception("Фамилия не может быть пустая");
			}
			if (string.IsNullOrEmpty(memberName))
			{
				throw new Exception("Имя не может быть пустым");
			}
			if (string.IsNullOrEmpty(memberPatronymic))
			{
				throw new Exception("Отчество не может быть пустым");
			}
			if (string.IsNullOrEmpty(memberPhoneNumber))
			{
				throw new Exception("Номер телефона не может быть пустым");
			}

			APIClient.PostRequest("api/member/updatemember", new MemberBindingModel
			{
				Id = member,
				MemberSurname = memberSurname,
				MemberName = memberName,
				MemberPatronymic = memberPatronymic,
				MemberPhoneNumber = memberPhoneNumber,
				OrganiserId = APIClient.Organiser.Id,
			});

			Response.Redirect("ListMembers");
		}

		public IActionResult DeleteMember()
		{
			if (APIClient.Organiser == null)
			{
				return Redirect("~/Home/Enter");
			}
			ViewBag.Members = APIClient.GetRequest<List<MemberViewModel>>($"api/member/getmembers?organiserId={APIClient.Organiser.Id}");
			return View();
		}

		[HttpPost]
		public void DeleteMember(int member)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Необходима авторизация");
			}
			APIClient.PostRequest("api/member/deletemember", new MemberBindingModel
			{
				Id = member
			});
			Response.Redirect("ListMembers");
		}

		[HttpGet]
		public MemberViewModel? GetMember(int memberId)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Необходима авторизация");
			}
			var result = APIClient.GetRequest<MemberViewModel>($"api/member/getmember?memberid={memberId}");
			if (result == null)
			{
				return default;
			}
			var memberSurname = result.MemberSurname;
			var memberName = result.MemberName;
			var memberPatronymic = result.MemberPatronymic;
			var memberPhoneNumber = result.MemberPhoneNumber;

			return result;
		}

        public IActionResult ListMealPlans()
        {
            if (APIClient.Organiser == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<MealPlanViewModel>>($"api/mealplan/getmealplans?organiserId={APIClient.Organiser.Id}"));
        }

        public IActionResult CreateMealPlan()
        {
            if (APIClient.Organiser == null)
            {
                return Redirect("~/Home/Enter");
            }
			ViewBag.Member = APIClient.GetRequest<List<MemberViewModel>>($"api/member/getmembers?organiserId={APIClient.Organiser.Id}");
            return View();
        }

        [HttpPost]
        public void CreateMealPlan(string mealPlanName, double mealPlanPrice, List<int> memberselect)
        {
            if (APIClient.Organiser == null)
            {
                throw new Exception("Необходима авторизация");
            }
            if (string.IsNullOrEmpty(mealPlanName) || string.IsNullOrEmpty(mealPlanPrice.ToString()))
            {
                throw new Exception("Введите название");
            }
            if (string.IsNullOrEmpty(mealPlanPrice.ToString()))
            {
                throw new Exception("Введите стоимость");
            }
            if (mealPlanPrice < 0)
            {
                throw new Exception("Стоимость не может быть отрицательной");
            }

			Dictionary<int, IMemberModel> member = new Dictionary<int, IMemberModel>();
			foreach (int members in memberselect)
			{
				member.Add(members, new MemberSearchModel { Id = members } as IMemberModel);
			}
			APIClient.PostRequest("api/mealplan/createmealplan", new MealPlanBindingModel
            {
                MealPlanName = mealPlanName,
				MealPlanPrice = mealPlanPrice,
				OrganiserId = APIClient.Organiser.Id,
				MealPlanMembers = member
			});
			Response.Redirect("ListMealPlans");
        }

		public IActionResult UpdateMealPlan()
		{
			if (APIClient.Organiser == null)
			{
				return Redirect("~/Home/Enter");
			}
			ViewBag.MealPlans = APIClient.GetRequest<List<MealPlanViewModel>>($"api/mealplan/getmealplans?organiserId={APIClient.Organiser.Id}");
			ViewBag.Member = APIClient.GetRequest<List<MemberViewModel>>($"api/member/getmembers?organiserId={APIClient.Organiser.Id}");
			return View();
		}

		[HttpPost]
		public void UpdateMealPlan(int mealPlan, string mealPlanName, double mealPlanPrice, List<int> memberselect)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Необходима авторизация");
			}
			if (string.IsNullOrEmpty(mealPlanName))
			{
				throw new Exception("Название не может быть пустым");
			}
			if (string.IsNullOrEmpty(mealPlanPrice.ToString()))
			{
				throw new Exception("Введите стоимость");
			}
			if (mealPlanPrice < 0)
			{
				throw new Exception("Стоимость не может быть отрицательной");
			}
			Dictionary<int, IMemberModel> member = new Dictionary<int, IMemberModel>();
			foreach (int members in memberselect)
			{
				member.Add(members, new MemberSearchModel { Id = members } as IMemberModel);
			}
			APIClient.PostRequest("api/mealplan/updatemealplan", new MealPlanBindingModel
			{
				Id = mealPlan,
				MealPlanName = mealPlanName,
				MealPlanPrice = mealPlanPrice,
				OrganiserId = APIClient.Organiser.Id,
				MealPlanMembers = member
			});
			Response.Redirect("ListMealPlans"); 
		}

		[HttpGet]
		public Tuple<MealPlanViewModel, List<string>>? GetMealPlan(int mealPlanId)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Необходима авторизация");
			}
			var result = APIClient.GetRequest<Tuple<MealPlanViewModel, List<string>>>($"api/mealPlan/getmealPlan?mealPlanId={mealPlanId}");
			if (result == null)
			{
				return default;
			}

			return result;
		}

		public IActionResult DeleteMealPlan()
		{
			if (APIClient.Organiser == null)
			{
				return Redirect("~/Home/Enter");
			}
			ViewBag.MealPlans = APIClient.GetRequest<List<MealPlanViewModel>>($"api/mealplan/getmealplans?organiserId={APIClient.Organiser.Id}");
			return View();
		}

		[HttpPost]
		public void DeleteMealPlan(int mealPlan)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Необходима авторизация");
			}
			APIClient.PostRequest("api/mealplan/deletemealplan", new MealPlanBindingModel
			{
				Id = mealPlan
			});
			Response.Redirect("ListMealPlans");
		}

		public IActionResult ListConferences()
		{
			if (APIClient.Organiser == null)
			{
				return Redirect("~/Home/Enter");
			}
			return View(APIClient.GetRequest<List<ConferenceViewModel>>($"api/conference/getconferences?organiserId={APIClient.Organiser.Id}"));
		}

		public IActionResult CreateConference()
		{
			if (APIClient.Organiser == null)
			{
				return Redirect("~/Home/Enter");
			}
			ViewBag.Member = APIClient.GetRequest<List<MemberViewModel>>($"api/member/getmembers?organiserId={APIClient.Organiser.Id}");
			return View();
		}

		[HttpPost]
		public void CreateConference(string conferenceName, DateTime startDate, List<int> memberselect)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Необходима авторизация");
			}
			if (string.IsNullOrEmpty(conferenceName) || string.IsNullOrEmpty(startDate.ToString()))
			{
				throw new Exception("Введите название");
			}
			if (string.IsNullOrEmpty(startDate.ToString()))
			{
				throw new Exception("Введите дату");
			}

			Dictionary<int, IMemberModel> member = new Dictionary<int, IMemberModel>();
			foreach (int members in memberselect)
			{
				member.Add(members, new MemberSearchModel { Id = members } as IMemberModel);
			}
			APIClient.PostRequest("api/conference/createconference", new ConferenceBindingModel
			{
				ConferenceName = conferenceName,
				StartDate = startDate,
				OrganiserId = APIClient.Organiser.Id,
				ConferenceMembers = member
			});
			Response.Redirect("ListConferences");
		}

		public IActionResult UpdateConference()
		{
			if (APIClient.Organiser == null)
			{
				return Redirect("~/Home/Enter");
			}
			ViewBag.Conferences = APIClient.GetRequest<List<ConferenceViewModel>>($"api/conference/getconferences?organiserId={APIClient.Organiser.Id}");
			ViewBag.Member = APIClient.GetRequest<List<MemberViewModel>>($"api/member/getmembers?organiserId={APIClient.Organiser.Id}");
			return View();
		}

		[HttpPost]
		public void UpdateConference(int conference, string conferenceName, DateTime startDate, List<int> memberselect)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Необходима авторизация");
			}
			if (string.IsNullOrEmpty(conferenceName))
			{
				throw new Exception("Название не может быть пустым");
			}
			if (string.IsNullOrEmpty(startDate.ToString()))
			{
				throw new Exception("Дата не может быть пустым");
			}
			Dictionary<int, IMemberModel> member = new Dictionary<int, IMemberModel>();
			foreach (int members in memberselect)
			{
				member.Add(members, new MemberSearchModel { Id = members } as IMemberModel);
			}
			APIClient.PostRequest("api/conference/updateconference", new ConferenceBindingModel
			{
				Id = conference,
				ConferenceName = conferenceName,
				StartDate = startDate,
				OrganiserId = APIClient.Organiser.Id,
				ConferenceMembers = member
			});
			Response.Redirect("ListConferences");
		}

		[HttpGet]
		public Tuple<ConferenceViewModel, List<string>>? GetConference(int conferenceId)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Необходима авторизация");
			}
			var result = APIClient.GetRequest<Tuple<ConferenceViewModel, List<string>>>($"api/conference/getconference?conferenceId={conferenceId}");
			if (result == null)
			{
				return default;
			}

			return result;
		}

		public IActionResult DeleteConference()
		{
			if (APIClient.Organiser == null)
			{
				return Redirect("~/Home/Enter");
			}
			ViewBag.Conferences = APIClient.GetRequest<List<ConferenceViewModel>>($"api/conference/getconferences?organiserId={APIClient.Organiser.Id}");
			return View();
		}

		[HttpPost]
		public void DeleteConference(int conference)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Необходима авторизация");
			}
			APIClient.PostRequest("api/conference/deleteconference", new ConferenceBindingModel
			{
				Id = conference
			});
			Response.Redirect("ListConferences");
		}

		public IActionResult ConferenceConferenceBookings()
		{
			if (APIClient.Organiser == null)
			{
				return Redirect("~/Home/Enter");
			}
			ViewBag.Conferences = APIClient.GetRequest<List<ConferenceViewModel>>($"api/conference/getconferences?organiserId={APIClient.Organiser.Id}");
			ViewBag.ConferenceBookings = APIClient.GetRequest<List<ConferenceBookingViewModel>>($"api/conferenceBooking/getconferenceBookings");
			return View();
		}

		[HttpPost]
		public void ConferenceConferenceBookings(int conference, int conferenceBooking)
		{
			if (APIClient.Organiser == null)
			{
				throw new Exception("Вы как сюда попали? Сюда вход только авторизованным");
			}
			var roomElem = APIClient.GetRequest<ConferenceBookingViewModel>($"api/conferenceBooking/getconferenceBookingbyid?conferenceBookingId={conferenceBooking}");
			APIClient.PostRequest("api/conferencebooking/updateconferenceBooking", new ConferenceBookingBindingModel
			{
				Id = conferenceBooking,
				HeadwaiterId = roomElem.HeadwaiterId,
				ConferenceId = conference,
				BookingDate = roomElem.BookingDate,
				NameHall = roomElem.NameHall,

			});
			Response.Redirect("Index");
		}

        [HttpGet]
        public IActionResult ListMemberConferenceBookingToFile()
        {
            if (APIClient.Organiser == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<MemberViewModel>>($"api/member/getmembers?organiserId={APIClient.Organiser.Id}"));
        }

        [HttpPost]
        public void ListMemberConferenceBookingToFile(int[] Ids, string type)
        {
            if (APIClient.Organiser == null)
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
                APIClient.PostRequest("api/report/CreateOrganiserReportToWordFile", new ReportOrganiserBindingModel
                {
                    Ids = res,
                    FileName = "C:\\Reports\\wordfile.docx"
                });
                Response.Redirect("GetWordFile");
            }
            else
            {
                APIClient.PostRequest("api/report/CreateOrganiserReportToExcelFile", new ReportOrganiserBindingModel
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

        [HttpGet]
        public IActionResult GetPdfFile()
        {
            return new PhysicalFileResult("C:\\Reports\\pdffile.pdf", "application/pdf");
        }


        [HttpGet]
        public IActionResult ListMembersToPdfFile()
        {
            if (APIClient.Organiser == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View("ListMembersToPdfFile");
        }

        [HttpPost]
        public void ListMembersToPdfFile(DateTime dateFrom, DateTime dateTo, string organiserEmail)
        {
            if (APIClient.Organiser == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            if (string.IsNullOrEmpty(organiserEmail))
            {
                throw new Exception("Email пуст");
            }
            APIClient.PostRequest("api/report/CreateOrganiserReportToPdfFile", new ReportOrganiserBindingModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                OrganiserId = APIClient.Organiser.Id
            });
            APIClient.PostRequest("api/report/SendPdfToMail", new MailSendInfoBindingModel
            {
                MailAddress = organiserEmail,
                Subject = "Отчет по участникам (pdf)",
                Text = "Отчет по участникам с " + dateFrom.ToShortDateString() + " до " + dateTo.ToShortDateString()
            });
            Response.Redirect("ListMembersToPdfFile");
        }

        [HttpGet]
        public string GetMembersReport(DateTime dateFrom, DateTime dateTo)
        {
            if (APIClient.Organiser == null)
            {
                throw new Exception("Вы как суда попали? Суда вход только авторизованным");
            }
            List<ReportMembersViewModel> result;
            try
            {
                result = _report.GetMembers(new ReportOrganiserBindingModel
                {
                    OrganiserId = APIClient.Organiser.Id,
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
            table += "<th scope=\"col\">Участник</th>";
            table += "<th scope=\"col\">Конференция</th>";
            table += "<th scope=\"col\">Дата начала конференции</th>";
            table += "<th scope=\"col\">Номер</th>";
            table += "<th scope=\"col\">Стоимость номера</th>";
            table += "</tr>";
            table += "</thead>";
            foreach (var report in result)
            {
                bool IsDate = true;
                if (report.StartDate.ToShortDateString() == "01.01.0001")
                {
                    IsDate = false;
                }
                bool IsCost = true;
                if (report.RoomPrice.ToString() == "0")
                {
                    IsCost = false;
                }
                table += "<tbody>";
                table += "<tr>";
                table += $"<td>{report.MemberSurname} {report.MemberName} {report.MemberPatronymic}</td>";
                table += $"<td>{report.ConferenceName}</td>";
                table += $"<td>{(IsDate is true ? report.StartDate.ToShortDateString() : string.Empty)}</td>";
                table += $"<td>{report.RoomName}</td>";
                table += $"<td>{(IsCost is true ? report.RoomPrice.ToString() : string.Empty)}</td>";
                table += "</tr>";
                table += "</tbody>";
                sum += report.RoomPrice;
            }
            table += "<tfoot class=\"table-secondary\">";
            table += $"<tr><th colspan=\"4\">Итого:</th><th>{sum}</th></tr>";
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
