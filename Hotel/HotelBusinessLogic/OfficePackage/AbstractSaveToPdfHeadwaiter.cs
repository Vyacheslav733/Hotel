using HotelBusinessLogic.OfficePackage.HelperEnums;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdfHeadwaiter
    {
        public void CreateDoc(PdfInfoHeadwaiter info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}",
                Style = "Normal",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            CreateTable(new List<string> { "3cm", "3cm", "3cm", "4cm", "4cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Обед", "Комната", "Цена комнаты", "Конференцияя", "Дата" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var lunch in info.Lunches)
            {

                bool IsCost = true;
                if (lunch.RoomPrice.ToString() == "0")
                {
                    IsCost = false;
                }
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { lunch.LunchName.ToString(), lunch.RoomName, IsCost is true ? lunch.RoomPrice.ToString() : string.Empty, lunch.ConferenceName, lunch.StartDate?.ToShortDateString() ?? string.Empty },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            CreateParagraph(new PdfParagraph
            {
                Text = $"Итого: {info.Lunches.Sum(x => x.RoomPrice)}\t",
                Style = "Normal",
                ParagraphAlignment = PdfParagraphAlignmentType.Rigth
            });
            SavePdf(info);
        }
        protected abstract void CreatePdf(PdfInfoHeadwaiter info);
        protected abstract void CreateParagraph(PdfParagraph paragraph);
        protected abstract void CreateTable(List<string> columns);
        protected abstract void CreateRow(PdfRowParameters rowParameters);
        protected abstract void SavePdf(PdfInfoHeadwaiter info);
    }
}
