using HotelContracts.BindingModels;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDataBaseImplement.Implemets
{
    public class RoomStorage : IRoomStorage
    {
        public List<RoomViewModel> GetFullList()
        {
            using var context = new HotelDataBase();
            return context.Rooms
                .Include(x => x.Lunches)
                .ThenInclude(x => x.Lunch)
                .ThenInclude(x => x.ConferenceBookingLunch)
                .ThenInclude(x => x.ConferenceBooking)
                .Include(x => x.MealPlan)
                .Include(x => x.Headwaiter)
                .ToList()
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public List<RoomViewModel> GetFilteredList(RoomSearchModel model)
        {
            if (!model.Id.HasValue && !model.HeadwaiterId.HasValue)
            {
                return new();
            }

            using var context = new HotelDataBase();

            if (model.HeadwaiterId.HasValue)
            {
                return context.Rooms
                    .Include(x => x.Lunches)
                    .ThenInclude(x => x.Lunch)
                    .ThenInclude(x => x.ConferenceBookingLunch)
                    .ThenInclude(x => x.ConferenceBooking)
                    .Include(x => x.MealPlan)
                    .Include(x => x.Headwaiter)
                    .Where(x => x.HeadwaiterId == model.HeadwaiterId)
                    .ToList()
                    .Select(x => x.GetViewModel)
                    .ToList();
            }

            return context.Rooms
                .Include(x => x.Lunches)
                .ThenInclude(x => x.Lunch)
                .ThenInclude(x => x.ConferenceBookingLunch)
                .ThenInclude(x => x.ConferenceBooking)
                .Include(x => x.MealPlan)
                .Include(x => x.Headwaiter)
                .Where(x => x.RoomName.Contains(model.RoomName))
                .ToList()
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public RoomViewModel? GetElement(RoomSearchModel model)
        {
            if (!model.Id.HasValue && string.IsNullOrEmpty(model.RoomName))
            {
                return null;
            }

            using var context = new HotelDataBase();

            return context.Rooms
                .Include(x => x.Lunches)
                .ThenInclude(x => x.Lunch)
                .ThenInclude(x => x.ConferenceBookingLunch)
                .ThenInclude(x => x.ConferenceBooking)
                .Include(x => x.MealPlan)
                .Include(x => x.Headwaiter)
                .FirstOrDefault(x => (!string.IsNullOrEmpty(model.RoomName) && x.RoomName == model.RoomName) || (model.Id.HasValue && x.Id == model.Id))?
                .GetViewModel;
        }

        
        public RoomViewModel? Insert(RoomBindingModel model)
        {
            using var context = new HotelDataBase();
            var newRoom = Room.Create(context, model);

            if (newRoom == null)
            {
                return null;
            }

            context.Rooms.Add(newRoom);
            context.SaveChanges();

            return context.Rooms
                .Include(x => x.Lunches)
                .ThenInclude(x => x.Lunch)
                .ThenInclude(x => x.ConferenceBookingLunch)
                .ThenInclude(x => x.ConferenceBooking)
                .ThenInclude(x => x.Conference)
                .Include(x => x.MealPlan)
                .Include(x => x.Headwaiter)
                .FirstOrDefault(x => x.Id == newRoom.Id)
                ?.GetViewModel;
        }

        public RoomViewModel? Update(RoomBindingModel model)
        {
            using var context = new HotelDataBase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var elem = context.Rooms
                    .Include(x => x.Lunches)
                    .ThenInclude(x => x.Lunch)
                    .ThenInclude(x => x.ConferenceBookingLunch)
                    .ThenInclude(x => x.ConferenceBooking)
                    .ThenInclude(x => x.Conference)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                if (elem == null)
                {
                    return null;
                }

                elem.Update(model);
                context.SaveChanges();
                if (model.RoomLunches != null)
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

        public RoomViewModel? Delete(RoomBindingModel model)
        {
            using var context = new HotelDataBase();

            var element = context.Rooms
                .Include(x => x.Lunches)
                .ThenInclude(x => x.Lunch)
                .ThenInclude(x => x.ConferenceBookingLunch)
                .ThenInclude(x => x.ConferenceBooking)
                .ThenInclude(x => x.Conference)
                .FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                context.Rooms.Remove(element);
                context.SaveChanges();

                return element.GetViewModel;
            }

            return null;
        }
    }
}
