using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavaiRobu
{
    internal class Table
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public List<Cell> Cells { get; set; } = new List<Cell>();

        public Table(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public void AddCell(string Value, int Row, char Col)
        {
            Cells.Add(new Cell(Value, Row, Col));
        }
        public static int[] CalculateColumnWidths(Table table, bool showParsed = false)
        {
            int[] widths = new int[table.Column];

            for (int col = 0; col < table.Column; col++)
            {
                widths[col] = TableWalker.GetMaxColumnWidth(table, col, showParsed);
            }

            return widths;
        }

        public void EvaluateCells()
        {
            foreach (var cell in Cells)
            {
                cell.ParsedValue = cell.ParseValue(GetCellByRef);
            }
        }

        private Cell? GetCellByRef(string reference)
        {
            if (string.IsNullOrWhiteSpace(reference)) return null;

            char col = reference[0];
            if (!int.TryParse(reference.Substring(1), out int row))
                return null;

            return Cells.FirstOrDefault(c => c.Col == col && c.Row == row);
        }


    }
}
