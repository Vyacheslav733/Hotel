using HotelContracts.BindingModels;
using HotelContracts.SearchModels;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDataBaseImplement.Implemets
{
    public class MemberStorage : IMemberStorage
    {
        public List<MemberViewModel> GetFullList()
        {
            using var context = new HotelDataBase();

            return context.Members
                .Include(x => x.ConferenceMembers)
                .ThenInclude(x => x.Conference)
                .Include(x => x.MealPlanMember)
                .ThenInclude(x => x.MealPlan)
                .Include(x => x.Organiser)
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public List<MemberViewModel> GetFilteredList(MemberSearchModel model)
        {
            if (string.IsNullOrEmpty(model.MemberSurname) && string.IsNullOrEmpty(model.MemberName) 
                && string.IsNullOrEmpty(model.MemberPatronymic) && !model.OrganiserId.HasValue)
            {
                return new();
            }

            using var context = new HotelDataBase();

            if (model.OrganiserId.HasValue)
            {
                return context.Members
                    .Include(x => x.ConferenceMembers)
                    .ThenInclude(x => x.Conference)
                    .Include(x => x.MealPlanMember)
                    .ThenInclude(x => x.MealPlan)
                    .Include(x => x.Organiser)
                    .Where(x => x.OrganiserId == model.OrganiserId)
                    .Select(x => x.GetViewModel)
                    .ToList();
            }

            return context.Members
                .Include(x => x.ConferenceMembers)
                .ThenInclude(x => x.Conference)
                .Include(x => x.MealPlanMember)
                .ThenInclude(x => x.MealPlan)
                .Include(x => x.Organiser)
                .Where(x =>
                    (string.IsNullOrEmpty(model.MemberSurname) || x.MemberSurname.Contains(model.MemberSurname)) &&
                    (string.IsNullOrEmpty(model.MemberName) || x.MemberName.Contains(model.MemberName)) &&
                    (string.IsNullOrEmpty(model.MemberPatronymic) || x.MemberPatronymic.Contains(model.MemberPatronymic)))
                .Select(x => x.GetViewModel)
                .ToList();
        }

        public MemberViewModel? GetElement(MemberSearchModel model)
        {
            if ((string.IsNullOrEmpty(model.MemberSurname) && string.IsNullOrEmpty(model.MemberName) 
                && string.IsNullOrEmpty(model.MemberPatronymic)) && !model.Id.HasValue)
            {
                return null;
            }

            using var context = new HotelDataBase();

            return context.Members
                .Include(x => x.ConferenceMembers)
                .ThenInclude(x => x.Conference)
                .Include(x => x.MealPlanMember)
                .ThenInclude(x => x.MealPlan)
                .Include(x => x.Organiser)
                .FirstOrDefault(x =>
                    (!string.IsNullOrEmpty(model.MemberSurname) && x.MemberSurname == model.MemberSurname) ||
                    (!string.IsNullOrEmpty(model.MemberName) && x.MemberName == model.MemberName) ||
                    (!string.IsNullOrEmpty(model.MemberPatronymic) && x.MemberPatronymic == model.MemberPatronymic) ||
                    (model.Id.HasValue && x.Id == model.Id))?
                .GetViewModel;
        }

        public MemberViewModel? Insert(MemberBindingModel model)
        {
            using var context = new HotelDataBase();

            var newMember = Member.Create(model);

            if (newMember == null)
            {
                return null;
            }

            context.Members.Add(newMember);
            context.SaveChanges();

            return newMember.GetViewModel;
        }

        public MemberViewModel? Update(MemberBindingModel model)
        {
            using var context = new HotelDataBase();

            var member = context.Members.FirstOrDefault(x => x.Id == model.Id);

            if (member == null)
            {
                return null;
            }

            member.Update(model);
            context.SaveChanges();

            return member.GetViewModel;
        }

        public MemberViewModel? Delete(MemberBindingModel model)
        {
            using var context = new HotelDataBase();

            var element = context.Members.FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                context.Members.Remove(element);
                context.SaveChanges();

                return element.GetViewModel;
            }

            return null;
        }
    }
}
