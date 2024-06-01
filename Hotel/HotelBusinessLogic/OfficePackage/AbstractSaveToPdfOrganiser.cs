using HotelBusinessLogic.OfficePackage.HelperEnums;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdfOrganiser
    {
        public void CreateDoc(PdfInfoOrganiser info)
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

            CreateTable(new List<string> { "4cm", "5cm", "3cm", "4cm", "2cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "ФИО участника", "Название конференции", "Дата начала конференции", "Название номера", "Стоимость номера" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var member in info.Members)
            {
                bool IsDate = true;
                if (member.StartDate.ToShortDateString() == "01.01.0001")
                {
                    IsDate = false;
                }

                bool IsCost = true;
                if (member.RoomPrice.ToString() == "0")
                {
                    IsCost = false;
                }

                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { $"{member.MemberSurname} {member.MemberName} {member.MemberPatronymic}", member.ConferenceName, IsDate is true ? member.StartDate.ToShortDateString() : string.Empty, member.RoomName, IsCost is true ? member.RoomPrice.ToString() : string.Empty },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }

            CreateParagraph(new PdfParagraph
            {
                Text = $"Итого: {info.Members.Sum(x => x.RoomPrice)}\t",
                Style = "Normal",
                ParagraphAlignment = PdfParagraphAlignmentType.Rigth
            });

            SavePdf(info);
        }

        protected abstract void CreatePdf(PdfInfoOrganiser info);
        protected abstract void CreateParagraph(PdfParagraph paragraph);
        protected abstract void CreateTable(List<string> columns);
        protected abstract void CreateRow(PdfRowParameters rowParameters);
        protected abstract void SavePdf(PdfInfoOrganiser info);
    }
}
