using HotelContracts.BindingModels;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HotelDataBaseImplement.Implemets
{
    public class LunchStorage : ILunchStorage
    {
        public List<LunchViewModel> GetFullList()
        {
            using var context = new HotelDataBase();

            return context.Lunches
                .Include(x => x.RoomLunches)
                .ThenInclude(x => x.Room)
                .Include(x => x.ConferenceBookingLunch)
                .ThenInclude(x => x.ConferenceBooking)
                .Include(x => x.Headwaiter)
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public List<LunchViewModel> GetFilteredList(LunchSearchModel model)
        {
            if (string.IsNullOrEmpty(model.LunchName) && !model.HeadwaiterId.HasValue)
            {
                return new();
            }

            using var context = new HotelDataBase();

            if (model.HeadwaiterId.HasValue)
            {
                return context.Lunches
                .Include(x => x.RoomLunches)
                .ThenInclude(x => x.Room)
                .Include(x => x.ConferenceBookingLunch)
                .ThenInclude(x => x.ConferenceBooking)
                .Include(x => x.Headwaiter)
                    .Where(x => x.HeadwaiterId == model.HeadwaiterId)
                    .Select(x => x.GetViewModel)
                    .ToList();
            }

            return context.Lunches
                .Include(x => x.RoomLunches)
                .ThenInclude(x => x.Room)
                .Include(x => x.ConferenceBookingLunch)
                .ThenInclude(x => x.ConferenceBooking)
                .Include(x => x.Headwaiter)
                .Where(x => x.LunchName.Contains(model.LunchName))
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public LunchViewModel? GetElement(LunchSearchModel model)
        {
            if (string.IsNullOrEmpty(model.LunchName) && !model.Id.HasValue)
            {
                return null;
            }

            using var context = new HotelDataBase();

            return context.Lunches
                .Include(x => x.RoomLunches)
                .ThenInclude(x => x.Room)
                .Include(x => x.ConferenceBookingLunch)
                .ThenInclude(x => x.ConferenceBooking)
                .FirstOrDefault(x => (!string.IsNullOrEmpty(model.LunchName) && x.LunchName == model.LunchName) || (model.Id.HasValue && x.Id == model.Id))?
                .GetViewModel;
        }

        public LunchViewModel? Insert(LunchBindingModel model)
        {
            using var context = new HotelDataBase();

            var newLunch = Lunch.Create(model);

            if (newLunch == null)
            {
                return null;
            }

            context.Lunches.Add(newLunch);
            context.SaveChanges();

            return newLunch.GetViewModel;
        }

        public LunchViewModel? Update(LunchBindingModel model)
        {
            using var context = new HotelDataBase();

            var lunch = context.Lunches.FirstOrDefault(x => x.Id == model.Id);

            if (lunch == null)
            {
                return null;
            }

            lunch.Update(model);
            context.SaveChanges();

            return lunch.GetViewModel;
        }

        public LunchViewModel? Delete(LunchBindingModel model)
        {
            using var context = new HotelDataBase();

            var element = context.Lunches.FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                context.Lunches.Remove(element);
                context.SaveChanges();

                return element.GetViewModel;
            }

            return null;
        }
    }
}
