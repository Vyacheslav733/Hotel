using HotelDataModels.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class MealPlanViewModel : IMealPlanModel
    {
        public int Id { get; set; }

        [DisplayName("Название плана питания")]
        public string MealPlanName { get; set; } = string.Empty;

        [DisplayName("Цена плана питания")]
        public double MealPlanPrice { get; set; }

        public int OrganiserId { get; set; }

        public Dictionary<int, IMemberModel> MealPlanMembers { get; set; } = new();
    }
}