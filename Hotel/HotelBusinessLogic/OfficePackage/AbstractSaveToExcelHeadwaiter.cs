using HotelBusinessLogic.OfficePackage.HelperEnums;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcelHeadwaiter
    {
        public void CreateReport(ExcelInfoHeadwaiter info)
        {
            CreateExcel(info);

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            uint rowIndex = 2;

            foreach (var mc in info.LunchRooms)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = mc.LunchName,
                    StyleInfo = ExcelStyleInfoType.Text
                });

                rowIndex++;

                foreach (var mealPlan in mc.MealPlans)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = mealPlan.Item1,
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });

                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = mealPlan.Item2.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });

                    rowIndex++;
                }

                rowIndex++;
            }

            SaveExcel(info);
        }

        protected abstract void CreateExcel(ExcelInfoHeadwaiter info);

        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

        protected abstract void MergeCells(ExcelMergeParameters excelParams);

        protected abstract void SaveExcel(ExcelInfoHeadwaiter info);
    }
}
