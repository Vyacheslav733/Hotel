using HotelBusinessLogic.OfficePackage.HelperModels;
using HotelBusinessLogic.OfficePackage;
using HotelContracts.BindingModels;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelDataBaseImplement.Models;

namespace HotelBusinessLogic.BusinessLogics
{ 
    public class ReportLogicHeadwaiter : IReportHeadwaiterLogic
    {
        private readonly IRoomStorage _roomStorage;
        private readonly ILunchStorage _lunchStorage;
        private readonly IMealPlanStorage _mealPlanStorage;
        private readonly IConferenceBookingStorage _conferenceBookingStorage;
        private readonly IConferenceStorage _conferenceStorage;
        private readonly AbstractSaveToExcelHeadwaiter _saveToExcel;
        private readonly AbstractSaveToWordHeadwaitre _saveToWord;
        private readonly AbstractSaveToPdfHeadwaiter _saveToPdf;

        public ReportLogicHeadwaiter(IRoomStorage roomStorage, ILunchStorage lunchStorage, IMealPlanStorage mealPlanStorage, IConferenceBookingStorage conferenceBookingStorage, IConferenceStorage conferenceStorage, AbstractSaveToExcelHeadwaiter saveToExcel, AbstractSaveToWordHeadwaitre saveToWord, AbstractSaveToPdfHeadwaiter saveToPdf)
        {
            _roomStorage = roomStorage;
            _lunchStorage = lunchStorage;
            _mealPlanStorage = mealPlanStorage;
            _conferenceBookingStorage = conferenceBookingStorage;
            _conferenceStorage = conferenceStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        public List<ReportLunchRoomViewModel> GetLunchRoom(List<int> Ids)
        {
            if (Ids == null)
            {
                return new List<ReportLunchRoomViewModel>();
            }

            var rooms = _roomStorage.GetFullList();
            var mealplans = _mealPlanStorage.GetFullList();

            List<LunchViewModel> lunches = new List<LunchViewModel>();
            foreach (var memId in Ids)
            {
                var res = _lunchStorage.GetElement(new LunchSearchModel { Id = memId });
                if (res != null)
                {
                    lunches.Add(res);
                }
            }

            var list = new List<ReportLunchRoomViewModel>();
            foreach (var lunch in lunches)
            {
                var record = new ReportLunchRoomViewModel
                {
                    LunchName = lunch.LunchName,
                    MealPlans = new List<Tuple<string, double>>()
                };

                foreach (var room in rooms)
                {
                    if (room.RoomLunches.ContainsKey(lunch.Id))
                    {
                        var mealPlanId = room.MealPlanId;
                        var mealPlan = mealplans.FirstOrDefault(dp => dp.Id == mealPlanId);
                        if (mealPlan != null)
                        {
                            record.MealPlans.Add(new Tuple<string, double>(mealPlan.MealPlanName, mealPlan.MealPlanPrice));
                        }
                    }
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportLunchesViewModel> GetLunches(ReportHeadwaiterBindingModel model)
        {
            var listAll = new List<ReportLunchesViewModel>();

            var listRooms = _roomStorage.GetFilteredList(new RoomSearchModel
            {
                HeadwaiterId = model.HeadwaiterId,

            });

            foreach (var room in listRooms)
            {
                foreach (var m in room.RoomLunches.Values)
                {
                    listAll.Add(new ReportLunchesViewModel
                    {
                        RoomName = room.RoomName,
                        RoomPrice = room.RoomPrice,
                        LunchName = m.LunchName,
                        LunchPrice = m.LunchPrice
                    });
                }
            }

            var listConferenceBookings = _conferenceBookingStorage.GetFilteredList(new ConferenceBookingSearchModel
            {
                HeadwaiterId = model.HeadwaiterId,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });

            var conferenced = _conferenceStorage.GetFullList();

            foreach (var conferenceBooking in listConferenceBookings)
            {
                foreach (var mp in conferenceBooking.ConferenceBookingLunches.Values)
                {
                    var conference = conferenced.FirstOrDefault(dp => dp.Id == conferenceBooking.ConferenceId);
                    listAll.Add(new ReportLunchesViewModel
                    {
                        LunchName = mp.LunchName,
                        LunchPrice = mp.LunchPrice,
                        ConferenceName = conference.ConferenceName,
                        StartDate = conference.StartDate
                    });
                }
            }

            return listAll;
        }

        public void SaveLunchRoomToExcelFile(ReportHeadwaiterBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfoHeadwaiter
            {
                FileName = model.FileName,
                Title = "Список номеров",
                LunchRooms = GetLunchRoom(model.Ids)
            });
        }

        public void SaveLunchRoomToWordFile(ReportHeadwaiterBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfoHeadwaiter
            {
                FileName = model.FileName,
                Title = "Список номеров",
                LunchRooms = GetLunchRoom(model.Ids)
            });
        }

        public void SaveLunchesToPdfFile(ReportHeadwaiterBindingModel model)
        {
            if (model.DateFrom == null)
            {
                throw new ArgumentException("Дата начала не задана");
            }

            if (model.DateTo == null)
            {
                throw new ArgumentException("Дата окончания не задана");
            }

            _saveToPdf.CreateDoc(new PdfInfoHeadwaiter
            {
                FileName = model.FileName,
                Title = "Список обедов",
                DateFrom = model.DateFrom!.Value,
                DateTo = model.DateTo!.Value,
                Lunches = GetLunches(model)
            });
        }
    }
}
