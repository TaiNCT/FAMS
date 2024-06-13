using OfficeOpenXml;
namespace SyllabusManagementAPI.Entities.Helpers
{
	public class ExcelHelper
	{
		public static string ReadExcelCell(ExcelWorksheet worksheet, int row, int column)
		{
			var cell = worksheet.Cells[row, column];
			if (cell.Merge)
			{
				// Nếu ô đã merge, lấy giá trị từ ô đầu tiên của vùng merge
				var startRow = cell.Start.Row;
				var startColumn = cell.Start.Column;
				return worksheet.Cells[startRow, startColumn].Value?.ToString();
			}
			else
			{
				// Nếu ô không merge, lấy giá trị bình thường
				var value = cell.Value?.ToString();

				// Kiểm tra nếu giá trị có chứa dấu xuống dòng (alt + enter) thì thay thế thành dấu xuống dòng thực sự
				if (value != null && value.Contains("\n"))
				{
					value = value.Replace("\n", Environment.NewLine);
				}

				return value;
			}
		}
		public static int GetMergedCellCount(ExcelWorksheet worksheet, int row, int column)
		{
			var range = worksheet.Cells[row, column];
			var mergedCells = worksheet.MergedCells;

			int mergedCellCount = 0;

			foreach (var mergedRange in mergedCells)
			{
				var address = new ExcelAddress(mergedRange);
				if (range.Start.Row >= address.Start.Row && range.End.Row <= address.End.Row &&
					range.Start.Column >= address.Start.Column && range.End.Column <= address.End.Column)
				{
					mergedCellCount += (address.End.Row - address.Start.Row + 1) * (address.End.Column - address.Start.Column + 1);
				}
			}

			return mergedCellCount;
		}
		public static string CombineCells(ExcelWorksheet worksheet, int row, int column1, int column2)
		{
			string cell1Value = worksheet.Cells[row, column1].Value?.ToString()?.Trim();
			string cell2Value = ExcelHelper.ReadExcelCell(worksheet, row, column2);

			if (!string.IsNullOrEmpty(cell1Value) && !string.IsNullOrEmpty(cell2Value))
			{
				return $"{cell1Value}: {cell2Value} \n";
			}
			return string.Empty;
		}
		public static string CombineRowsAndCells(ExcelWorksheet worksheet, int startRow, int endRows, int column1, int column2)
		{
			string combinedText = string.Empty;
			for (int row = startRow; row <= endRows; row++)
			{
				string cellText = ExcelHelper.CombineCells(worksheet, row, column1, column2);
				if (!string.IsNullOrEmpty(cellText))
				{
					if (!string.IsNullOrEmpty(combinedText))
					{
						combinedText += ", "; // Thêm dấu phẩy để ngăn cách giữa các mục
					}
					combinedText += cellText;
				}
			}
			return combinedText;
		}
	}
}
