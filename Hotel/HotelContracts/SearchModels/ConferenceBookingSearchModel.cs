using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelContracts.SearchModels
{
    public class ConferenceBookingSearchModel
    {
        public int? Id { get; set; }
        public int? HeadwaiterId { get; set; }
        public string? NameHall { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
