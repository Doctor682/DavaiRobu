using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavaiRobu
{
    internal class Cell
    {
        public string Value { get; set; }
        public int Row {  get; set; }
        public char Col { get; set; }
        public CellType Type { get; set; }
        public object? ParsedValue { get; set; }


        public Cell(string value, int row, char col)
        {
            Value = value;
            Row = row;
            Col = col;
            Type = DetermineType(value);
        }

        public CellType DetermineType(string value)
        {
            if (value.StartsWith("=")) return CellType.Formula;
            else if (double.TryParse(value, out _)) return CellType.Number;
            else return CellType.Text;
        }

        public object? ParseValue(Func<string, Cell?> resolveCell)
        {
            switch(Type)
            {
                case CellType.Formula:
                    return CalculateFormula(Value, resolveCell); ;
                case CellType.Number:
                    return Math.Round(double.Parse(Value),3);
                case CellType.Text:
                    return Value;
                default:
                    return null;
            }
        }

        public object CalculateFormula(string value, Func<string, Cell?> resolveCell)
        {
            value = value.TrimStart('=');
            var parts = value.Split('+', StringSplitOptions.RemoveEmptyEntries);

            var values = new List<object>();

            foreach (var part in parts.Select(p => p.Trim()))
            {
                var cell = resolveCell(part);
                if (cell == null || cell.ParsedValue == null)
                    return $"err!: {part} not found";

                values.Add(cell.ParsedValue);
            }

            if (values.All(v => v is double))
                return Math.Round(values.Sum(v =>(double)v), 3);
            else if (values.All(v => v is string))
                return string.Concat(values);
            else
                return "err!";
        }
    }
}
