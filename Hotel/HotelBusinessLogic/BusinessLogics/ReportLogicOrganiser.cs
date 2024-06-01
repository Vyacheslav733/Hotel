using HotelBusinessLogic.OfficePackage.HelperModels;
using HotelBusinessLogic.OfficePackage;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelContracts.BindingModels;
using HotelDataBaseImplement.Implemets;
using HotelDataBaseImplement.Models;

namespace HotelBusinessLogic.BusinessLogics
{
    public class ReportLogicOrganiser : IReportOrganiserLogic
    {
        private readonly IMealPlanStorage _mealPlanStorage;
        private readonly IMemberStorage _memberStorage;
        private readonly IConferenceStorage _conferenceStorage;
        private readonly IConferenceBookingStorage _conferenceBookingStorage;
        private readonly IRoomStorage _roomStorage;
        private readonly AbstractSaveToExcelOrganiser _saveToExcel;
        private readonly AbstractSaveToWordOrganiser _saveToWord;
        private readonly AbstractSaveToPdfOrganiser _saveToPdf;

        public ReportLogicOrganiser(IMealPlanStorage mealPlanStorage, IMemberStorage memberStorage, IConferenceStorage conferenceStorage, IConferenceBookingStorage conferenceBookingStorage, IRoomStorage roomStorage, AbstractSaveToExcelOrganiser saveToExcel, AbstractSaveToWordOrganiser saveToWord, AbstractSaveToPdfOrganiser saveToPdf)
        {
            _mealPlanStorage = mealPlanStorage;
            _memberStorage = memberStorage;
            _conferenceStorage = conferenceStorage;
            _conferenceBookingStorage = conferenceBookingStorage;
            _roomStorage = roomStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        public List<ReportMemberConferenceViewModel> GetMemberConferenceBooking(List<int> Ids)
        {
            if (Ids == null)
            {
                return new List<ReportMemberConferenceViewModel>();
            }

            var conferences = _conferenceStorage.GetFullList();
            var conferenceBookings = _conferenceBookingStorage.GetFullList();

            List<MemberViewModel> members = new List<MemberViewModel>();
            foreach (var memberId in Ids)
            {
                var res = _memberStorage.GetElement(new MemberSearchModel { Id = memberId });
                if (res != null)
                {
                    members.Add(res);
                }
            }
            var list = new List<ReportMemberConferenceViewModel>();
            foreach (var member in members)
            {
                var record = new ReportMemberConferenceViewModel
                {
                    MemberSurname = member.MemberSurname,
                    MemberName = member.MemberName,
                    MemberPatronymic = member.MemberPatronymic,
                    ConferenceBookings = new List<Tuple<string, DateTime>>()
                };
                foreach (var conference in conferences)
                {
                    if (conference.ConferenceMembers.ContainsKey(member.Id))
                    {
                        var bookingsForConference = conferenceBookings.Where(cb => cb.ConferenceId == conference.Id).ToList();
                        foreach (var booking in bookingsForConference)
                        {
                            record.ConferenceBookings.Add(new Tuple<string, DateTime>(booking.NameHall, booking.BookingDate.Value));
                        }
                    }
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportMembersViewModel> GetMembers(ReportOrganiserBindingModel model)
        {
            var listAll = new List<ReportMembersViewModel>();

            var listСonferences = _conferenceStorage.GetFilteredList(new ConferenceSearchModel
            {
                OrganiserId = model.OrganiserId,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });

            foreach (var conference in listСonferences)
            {
                foreach (var m in conference.ConferenceMembers.Values)
                {
                    listAll.Add(new ReportMembersViewModel
                    {
                        StartDate = conference.StartDate,
                        ConferenceName = conference.ConferenceName,
                        MemberSurname = m.MemberSurname,
                        MemberName = m.MemberName,
                        MemberPatronymic = m.MemberPatronymic
                    });
                }
            }

            var listMealPlans = _mealPlanStorage.GetFilteredList(new MealPlanSearchModel
            {
                OrganiserId = model.OrganiserId,
            });

            var rooms = _roomStorage.GetFullList();

            foreach (var mealPlan in listMealPlans)
            {
                foreach (var mp in mealPlan.MealPlanMembers.Values)
                {
                    var room = rooms.FirstOrDefault(r => r.MealPlanId == mealPlan.Id);
                    listAll.Add(new ReportMembersViewModel
                    {
                        MemberSurname = mp.MemberSurname,
                        MemberName = mp.MemberName,
                        MemberPatronymic = mp.MemberPatronymic,
                        RoomName = room.RoomName,
                        RoomPrice = room.RoomPrice
                    });
                }
            }

            return listAll;
        }

        public void SaveMemberConferenceToExcelFile(ReportOrganiserBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfoOrganiser
            {
                FileName = model.FileName,
                Title = "Список конференций",
                MemberConferences = GetMemberConferenceBooking(model.Ids)
            });
        }

        public void SaveMemberConferenceToWordFile(ReportOrganiserBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfoOrganiser
            {
                FileName = model.FileName,
                Title = "Список конференций",
                MemberConferences = GetMemberConferenceBooking(model.Ids)
            });
        }

        public void SaveMembersToPdfFile(ReportOrganiserBindingModel model)
        {
            if (model.DateFrom == null)
            {
                throw new ArgumentException("Дата начала не задана");
            }

            if (model.DateTo == null)
            {
                throw new ArgumentException("Дата окончания не задана");
            }
            _saveToPdf.CreateDoc(new PdfInfoOrganiser
            {
                FileName = model.FileName,
                Title = "Список участников",
                DateFrom = model.DateFrom!.Value,
                DateTo = model.DateTo!.Value,
                Members = GetMembers(model)
            });
        }
    }
}
