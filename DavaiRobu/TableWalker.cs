using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavaiRobu
{
    internal class TableWalker
    {
        public static void PrintRow(Table table, int rowIndex)
        {
            for (int columnIndex = 0; columnIndex < table.Column; columnIndex++)
            {
                Console.Write($"Value for cell [{rowIndex + 1}, {(char)('A' + columnIndex)}]: ");
                string value = Console.ReadLine() ?? "";
                table.AddCell(value, rowIndex + 1, (char)('A' + columnIndex));
            }
        }

        public static void PrintRowOfChar(Table table, int[] colWidths)
        {
            Console.Write("".PadRight(6));
            for (int columnIndex = 0; columnIndex < table.Column; columnIndex++)
            {
                char colChar = (char)('A' + columnIndex);
                Console.Write(colChar.ToString().PadRight(colWidths[columnIndex]));
            }
            Console.WriteLine();
        }

        public static void SetValueToRow(Table table, int rowIndex, int[] colWidths, bool showParsed = false)
        {
            Console.Write($"{rowIndex,-6}");
            for (int columnIndex = 0; columnIndex < table.Column; columnIndex++)
            {
                char colChar = (char)('A' + columnIndex);
                var cell = table.Cells.FirstOrDefault(c => c.Row == rowIndex && c.Col == colChar);
                string value = GetCellValue(cell, showParsed);
                Console.Write(value.PadRight(colWidths[columnIndex]));
            }
            Console.WriteLine();
        }

        private static string GetCellValue(Cell? cell, bool showParsed)
        {
            if (cell == null) return "";
            return showParsed ? cell.ParsedValue?.ToString() ?? "" : cell.Value;
        }

        public static int GetMaxColumnWidth(Table table, int columnIndex, bool showParsed)
        {
            char colChar = (char)('A' + columnIndex);
            int maxWidth = colChar.ToString().Length;

            foreach (var cell in table.Cells.Where(c => c.Col == colChar))
            {
                string value = GetCellValue(cell, showParsed);
                maxWidth = Math.Max(maxWidth, value.Length);
            }

            return maxWidth + 2;
        }
    }
}
