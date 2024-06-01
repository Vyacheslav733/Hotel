using HotelContracts.BindingModels;
using HotelContracts.ViewModels;
using HotelDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDataBaseImplement.Models
{
    public class Member : IMemberModel
    {
        public int Id { get; private set; }

        [Required]
        public string MemberSurname { get; set; } = string.Empty;
        [Required]
        public string MemberName { get; set; } = string.Empty;
        [Required]
        public string MemberPatronymic { get; set; } = string.Empty;
        [Required]
        public string MemberPhoneNumber { get; set; } = string.Empty;

        public int OrganiserId { get; private set; }

        public virtual Organiser Organiser { get; set; }

        [ForeignKey("MemberId")]
        public virtual List<MealPlanMember> MealPlanMember { get; set; } = new();


        [ForeignKey("MemberId")]
        public virtual List<ConferenceMember> ConferenceMembers { get; set; } = new();

        public static Member? Create(MemberBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Member()
            {
                Id = model.Id,
                MemberSurname = model.MemberSurname,
                MemberName = model.MemberName,
                MemberPatronymic = model.MemberPatronymic,
                MemberPhoneNumber = model.MemberPhoneNumber,
                OrganiserId = model.OrganiserId,
            };
        }

        public static Member Create(MemberViewModel model)
        {
            return new Member
            {
                Id = model.Id,
                MemberSurname = model.MemberSurname,
                MemberName = model.MemberName,
                MemberPatronymic = model.MemberPatronymic,
                MemberPhoneNumber = model.MemberPhoneNumber,
                OrganiserId = model.OrganiserId,
            };
        }

        public void Update(MemberBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            MemberSurname = model.MemberSurname;
            MemberName = model.MemberName;
            MemberPatronymic = model.MemberPatronymic;
            MemberPhoneNumber = model.MemberPhoneNumber;
            OrganiserId = model.OrganiserId;
        }

        public MemberViewModel GetViewModel => new()
        {
            Id = Id,
            MemberSurname = MemberSurname,
            MemberName = MemberName,
            MemberPatronymic = MemberPatronymic,
            MemberPhoneNumber = MemberPhoneNumber,
            OrganiserId = OrganiserId
        };
    }
}
