using HotelContracts.ViewModels;

namespace HotelBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfoHeadwaiter
    {
        public string FileName { get; set; } = "C:\\Reports\\pdffile.pdf";
        public string Title { get; set; } = string.Empty;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportLunchesViewModel> Lunches { get; set; } = new();
    }
}
