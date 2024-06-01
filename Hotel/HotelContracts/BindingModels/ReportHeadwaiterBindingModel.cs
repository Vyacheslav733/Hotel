namespace HotelContracts.BindingModels
{
    public class ReportHeadwaiterBindingModel
    {
        public string FileName { get; set; } = string.Empty;
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<int>? Ids { get; set; }
        public int HeadwaiterId { get; set; }
    }
}
