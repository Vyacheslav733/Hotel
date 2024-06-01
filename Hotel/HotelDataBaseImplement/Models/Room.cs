using HotelContracts.BindingModels;
using HotelContracts.ViewModels;
using HotelDataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDataBaseImplement.Models
{
    public class Room : IRoomModel
    {
        public int Id { get; private set; }

        [Required]
        public string RoomName { get; set; } = string.Empty;

        [Required]
        public string RoomFrame { get; set; } = string.Empty;

        [Required]
        public double RoomPrice { get; set; }

        public int? MealPlanId { get; private set; }
        public int HeadwaiterId { get; private set; }

        public virtual Headwaiter? Headwaiter { get; set; }
        public virtual MealPlan? MealPlan { get; set; }

        [ForeignKey("RoomId")]
        public virtual List<RoomLunch> Lunches { get; set; }

        private Dictionary<int, ILunchModel> _roomLunches = null;

        [NotMapped]
        public Dictionary<int, ILunchModel> RoomLunches
        {
            get
            {
                if (_roomLunches == null)
                {
                    _roomLunches = Lunches.ToDictionary(x => x.LunchId, x => (x.Lunch as ILunchModel));
                }
                return _roomLunches;
            }
        }

        public static Room Create(HotelDataBase context, RoomBindingModel model)
        {
            return new Room()
            {
                Id = model.Id,
                RoomName = model.RoomName,
                RoomFrame = model.RoomFrame,
                RoomPrice = model.RoomPrice,
                HeadwaiterId = model.HeadwaiterId,
                MealPlanId = model.MealPlanId,
                Lunches = model.RoomLunches.Select(x => new RoomLunch
                {
                    Lunch = context.Lunches.First(y => y.Id == x.Key),
                }).ToList()
            };
        }

        public void Update(RoomBindingModel model)
        {
            RoomName = model.RoomName;
            RoomFrame = model.RoomFrame;
            RoomPrice = model.RoomPrice;
            HeadwaiterId = model.HeadwaiterId;
            MealPlanId = model.MealPlanId;
        }

        public RoomViewModel GetViewModel => new()
        {
            Id = Id,
            RoomName = RoomName,
            RoomFrame = RoomFrame,
            HeadwaiterId = HeadwaiterId,
            MealPlanId = MealPlanId,
            RoomPrice = RoomPrice,
            RoomLunches = RoomLunches
        };

        public void UpdateLunches(HotelDataBase context, RoomBindingModel model)
        {
            var roomLunches = context.RoomLunches.Where(rec => rec.RoomId == model.Id).ToList();

            if (roomLunches != null && roomLunches.Count > 0)
            {
                context.RoomLunches.RemoveRange(roomLunches.Where(rec => !model.RoomLunches.ContainsKey(rec.LunchId)));
                context.SaveChanges();

                foreach (var updateLunch in roomLunches)
                {
                    model.RoomLunches.Remove(updateLunch.LunchId);
                }
                context.SaveChanges();
            }

            var room = context.Rooms.First(x => x.Id == Id);

            foreach (var lunch in model.RoomLunches)
            {
                context.RoomLunches.Add(new RoomLunch
                {
                    Room = room,
                    Lunch = context.Lunches.First(x => x.Id == lunch.Key)
                });
                context.SaveChanges();
            }
            _roomLunches = null;
        }
    }
}
