using HotelContracts.BindingModels;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDataBaseImplement.Implemets
{
    public class HeadwaiterStorage : IHeadwaiterStorage
    {
        public List<HeadwaiterViewModel> GetFullList()
        {
            using var context = new HotelDataBase();

            return context.Headwaiters.Select(x => x.GetViewModel).ToList();
        }

        public List<HeadwaiterViewModel> GetFilteredList(HeadwaiterSearchModel model)
        {
            if (string.IsNullOrEmpty(model.HeadwaiterSurname) && string.IsNullOrEmpty(model.HeadwaiterName) 
                && string.IsNullOrEmpty(model.HeadwaiterPatronymic))
            {
                return new();
            }

            using var context = new HotelDataBase();

            return context.Headwaiters
               .Include(x => x.ConferenceBookings)
               .Include(x => x.Lunches)
               .Include(x => x.Rooms)
               .Where(x => x.HeadwaiterLogin.Contains(model.HeadwaiterLogin) && x.HeadwaiterPassword == model.HeadwaiterPassword)
               .Select(x => x.GetViewModel)
               .ToList();
        }

        public HeadwaiterViewModel? GetElement(HeadwaiterSearchModel model)
        {
            using var context = new HotelDataBase();

            if (model.Id.HasValue)
            {
                return context.Headwaiters
                    .Include(x => x.ConferenceBookings)
                    .Include(x => x.Lunches)
                    .Include(x => x.Rooms)
                    .FirstOrDefault(x => x.Id == model.Id)?
                    .GetViewModel;
            }

            if (!string.IsNullOrEmpty(model.HeadwaiterEmail) && !string.IsNullOrEmpty(model.HeadwaiterPassword))
            {
                return context.Headwaiters
                    .Include(x => x.ConferenceBookings)
                    .Include(x => x.Lunches)
                    .Include(x => x.Rooms)
                    .FirstOrDefault(x => x.HeadwaiterEmail.Equals(model.HeadwaiterEmail) && x.HeadwaiterPassword.Equals(model.HeadwaiterPassword))?
                    .GetViewModel;
            }

            if (!string.IsNullOrEmpty(model.HeadwaiterEmail))
            {
                return context.Headwaiters
                    .Include(x => x.ConferenceBookings)
                    .Include(x => x.Lunches)
                    .Include(x => x.Rooms)
                    .FirstOrDefault(x => x.HeadwaiterEmail.Equals(model.HeadwaiterEmail))?
                    .GetViewModel;
            }

            return null;
        }

        public HeadwaiterViewModel? Insert(HeadwaiterBindingModel model)
        {
            var newHeadwaiter = Headwaiter.Create(model);

            if (newHeadwaiter == null)
            {
                return null;
            }

            using var context = new HotelDataBase();

            context.Headwaiters.Add(newHeadwaiter);
            context.SaveChanges();

            return newHeadwaiter.GetViewModel;
        }

        public HeadwaiterViewModel? Update(HeadwaiterBindingModel model)
        {
            using var context = new HotelDataBase();

            var headwaiter = context.Headwaiters.FirstOrDefault(x => x.Id == model.Id);

            if (headwaiter == null)
            {
                return null;
            }

            headwaiter.Update(model);
            context.SaveChanges();

            return headwaiter.GetViewModel;
        }

        public HeadwaiterViewModel? Delete(HeadwaiterBindingModel model)
        {
            using var context = new HotelDataBase();

            var element = context.Headwaiters.FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                context.Headwaiters.Remove(element);
                context.SaveChanges();

                return element.GetViewModel;
            }

            return null;
        }
    }
}
