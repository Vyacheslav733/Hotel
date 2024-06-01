using HotelContracts.BindingModels;
using HotelContracts.ViewModels;
using HotelDataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDataBaseImplement.Models
{
    public class Organiser : IOrganiserModel
    {
        [Required]
        public string OrganiserSurname { get; set; } = string.Empty;
        [Required]
        public string OrganiserName { get; set; } = string.Empty;
        [Required]
        public string OrganiserPatronymic { get; set; } = string.Empty;
        [Required]
        public string OrganiserLogin { get; set; } = string.Empty;
        [Required]
        public string OrganiserPassword { get; set; } = string.Empty;
        [Required]
        public string OrganiserEmail { get; set; } = string.Empty;
        [Required]
        public string OrganiserPhoneNumber { get; set; } = string.Empty;

        public int Id { get; private set; }

        [ForeignKey("OrganiserId")]
        public virtual List<Conference> Conferences { get; set; } = new();
        [ForeignKey("OrganiserId")]
        public virtual List<MealPlan> MealPlans { get; set; } = new();
        [ForeignKey("OrganiserId")]
        public virtual List<Member> Members { get; set; } = new();

        public static Organiser? Create(OrganiserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Organiser()
            {
                Id = model.Id,
                OrganiserSurname = model.OrganiserSurname,
                OrganiserName = model.OrganiserName,
                OrganiserPatronymic = model.OrganiserPatronymic,
                OrganiserLogin = model.OrganiserLogin,
                OrganiserPassword = model.OrganiserPassword,
                OrganiserEmail = model.OrganiserEmail,
                OrganiserPhoneNumber = model.OrganiserPhoneNumber
            };
        }

        public static Organiser Create(OrganiserViewModel model)
        {
            return new Organiser
            {
                Id = model.Id,
                OrganiserSurname = model.OrganiserSurname,
                OrganiserName = model.OrganiserName,
                OrganiserPatronymic = model.OrganiserPatronymic,
                OrganiserLogin = model.OrganiserLogin,
                OrganiserPassword = model.OrganiserPassword,
                OrganiserEmail = model.OrganiserEmail,
                OrganiserPhoneNumber = model.OrganiserPhoneNumber
            };
        }

        public void Update(OrganiserBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            OrganiserSurname = model.OrganiserSurname;
            OrganiserName = model.OrganiserName;
            OrganiserPatronymic = model.OrganiserPatronymic;
            OrganiserLogin = model.OrganiserLogin;
            OrganiserPassword = model.OrganiserPassword;
            OrganiserEmail = model.OrganiserEmail;
            OrganiserPhoneNumber = model.OrganiserPhoneNumber;
        }

        public OrganiserViewModel GetViewModel => new()
        {
            Id = Id,
            OrganiserSurname = OrganiserSurname,
            OrganiserName = OrganiserName,
            OrganiserPatronymic = OrganiserPatronymic,
            OrganiserLogin = OrganiserLogin,
            OrganiserPassword = OrganiserPassword,
            OrganiserEmail = OrganiserEmail,
            OrganiserPhoneNumber = OrganiserPhoneNumber
        };
    }
}