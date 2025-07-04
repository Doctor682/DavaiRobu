using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavaiRobu
{
    internal class Operations
    {
        public static Table SetupTable()
        {
            int n = 0, m = 0;
            bool isValidInput = false;
            do
            {
                Console.WriteLine("Enter table size (n-col, m-row) n,m: ");
                string input = Console.ReadLine()?.Trim() ?? "";
                string[] parts = input.Split(',');

                if (parts.Length == 2 &&
                int.TryParse(parts[0].Trim(), out n) &&
                int.TryParse(parts[1].Trim(), out m) &&
                n > 0 && m > 0)
                    isValidInput = true;
                else
                    Console.WriteLine("Invalid input, try enter positive integer numbers by comma like 3,4 ");
            } while (!isValidInput);
            
            return new Table(m, n);   
        }

        public static void FillingTable(Table table)
        {
            Console.WriteLine($"Enter value to {table.Column*table.Row} cells");
            for (int rowIndex = 0; rowIndex < table.Row; rowIndex++)
            {
                TableWalker.PrintRow(table, rowIndex);
            }
        }

        public static void PrintTable(Table table, bool showParsed = false)
        {
            var width = Table.CalculateColumnWidths(table);

            Console.WriteLine($"\nTable {table.Row}x{table.Column}:\n");

            TableWalker.PrintRowOfChar(table, width);

            for (int rowIndex = 1; rowIndex <= table.Row; rowIndex++)
            {
                TableWalker.SetValueToRow(table, rowIndex, width, showParsed);
            }
        }


        public static void CalculateTable(Table table)
        {
            table.EvaluateCells();
        }

    }
}
