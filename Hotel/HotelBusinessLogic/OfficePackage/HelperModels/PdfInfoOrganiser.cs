using HotelContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfoOrganiser
    {
        public string FileName { get; set; } = "C:\\Reports\\pdffile.pdf";
        public string Title { get; set; } = string.Empty;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportMembersViewModel> Members { get; set; } = new();
    }
}
