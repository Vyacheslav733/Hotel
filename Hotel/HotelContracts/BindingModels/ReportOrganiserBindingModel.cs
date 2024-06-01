﻿namespace HotelContracts.BindingModels
{
    public class ReportOrganiserBindingModel
    {
        public string FileName { get; set; } = string.Empty;
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<int>? Ids { get; set; }
        public int OrganiserId { get; set; }
    }
}
