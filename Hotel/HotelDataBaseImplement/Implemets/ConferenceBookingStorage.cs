using HotelContracts.BindingModels;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDataBaseImplement.Implemets
{
    public class ConferenceBookingStorage : IConferenceBookingStorage
    {
        public List<ConferenceBookingViewModel> GetFullList()
        {
            using var context = new HotelDataBase();

            return context.ConferenceBookings
                .Include(x => x.Lunches)
                .ThenInclude(x => x.Lunch)
                .ThenInclude(x => x.RoomLunches)
                .ThenInclude(x => x.Room)
                .Include(x => x.Conference)
                .Include(x => x.Headwaiter)
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public List<ConferenceBookingViewModel> GetFilteredList(ConferenceBookingSearchModel model)
        {
            if (!model.DateFrom.HasValue && !model.DateTo.HasValue && !model.HeadwaiterId.HasValue)
            {
                return new();
            }
            using var context = new HotelDataBase();
            if (model.DateFrom.HasValue)
            {
                return context.ConferenceBookings
                       .Include(x => x.Lunches)
                       .ThenInclude(x => x.Lunch)
                       .ThenInclude(x => x.RoomLunches)
                       .ThenInclude(x => x.Room)
                       .Include(x => x.Conference)
                       .Include(x => x.Headwaiter)
                        .Where(x => x.BookingDate >= model.DateFrom && x.BookingDate <= model.DateTo && x.HeadwaiterId == model.HeadwaiterId)
                        .Select(x => x.GetViewModel)
                        .ToList();
            }
            else if (model.HeadwaiterId.HasValue)
            {
                return context.ConferenceBookings
                    .Include(x => x.Lunches)
                    .ThenInclude(x => x.Lunch)
                    .ThenInclude(x => x.RoomLunches)
                    .ThenInclude(x => x.Room)
                    .Include(x => x.Conference)
                    .Include(x => x.Headwaiter)
                    .Where(x => x.HeadwaiterId == model.HeadwaiterId)
                    .ToList()
                    .Select(x => x.GetViewModel)
                    .ToList();
            }
            return context.ConferenceBookings
                .Include(x => x.Lunches)
                .ThenInclude(x => x.Lunch)
                .ThenInclude(x => x.RoomLunches)
                .ThenInclude(x => x.Room)
                .Include(x => x.Conference)
                .Include(x => x.Headwaiter)
                .Where(x => x.NameHall.Contains(model.NameHall))
                .ToList()
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public ConferenceBookingViewModel? GetElement(ConferenceBookingSearchModel model)
        {
            if (string.IsNullOrEmpty(model.NameHall) && !model.Id.HasValue)
            {
                return null;
            }

            using var context = new HotelDataBase();

            return context.ConferenceBookings
                .Include(x => x.Lunches)
                .ThenInclude(x => x.Lunch)
                .ThenInclude(x => x.RoomLunches)
                .ThenInclude(x => x.Room)
                .Include(x => x.Conference)
                .Include(x => x.Headwaiter)
                .FirstOrDefault(x => (!string.IsNullOrEmpty(model.NameHall) && x.NameHall == model.NameHall) || (model.Id.HasValue && x.Id == model.Id))?
               .GetViewModel;
        }

        public ConferenceBookingViewModel? Insert(ConferenceBookingBindingModel model)
        {
            using var context = new HotelDataBase();
            var newConferenceBooking = ConferenceBooking.Create(context, model);

            if (newConferenceBooking == null)
            {
                return null;
            }

            context.ConferenceBookings.Add(newConferenceBooking);
            context.SaveChanges();

            return context.ConferenceBookings
                .Include(x => x.Lunches)
                .ThenInclude(x => x.Lunch)
                .ThenInclude(x => x.RoomLunches)
                .ThenInclude(x => x.Room)
                .Include(x => x.Conference)
                .Include(x => x.Headwaiter)
                .FirstOrDefault(x => x.Id == newConferenceBooking.Id)
                ?.GetViewModel;
        }

        public ConferenceBookingViewModel? Update(ConferenceBookingBindingModel model)
        {
            using var context = new HotelDataBase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var elem = context.ConferenceBookings
                     .Include(x => x.Lunches)
                     .ThenInclude(x => x.Lunch)
                     .ThenInclude(x => x.ConferenceBookingLunch)
                     .ThenInclude(x => x.ConferenceBooking)
                     .ThenInclude(x => x.Conference)
                     .FirstOrDefault(rec => rec.Id == model.Id); if (elem == null)
                {
                    return null;
                }
                elem.Update(model);
                context.SaveChanges();
                if (model.ConferenceBookingLunches != null)
                {
                    elem.UpdateLunches(context, model);
                }
                transaction.Commit();
                return elem.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public ConferenceBookingViewModel? Delete(ConferenceBookingBindingModel model)
        {
            using var context = new HotelDataBase();

            var element = context.ConferenceBookings.Include(x => x.Lunches).FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                context.ConferenceBookings.Remove(element);
                context.SaveChanges();

                return element.GetViewModel;
            }

            return null;
        }
    }
}
