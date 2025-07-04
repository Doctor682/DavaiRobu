using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavaiRobu
{
    internal class DavaiRobu1
    {
        public static void DavaiRobu2()
        {
            var table = Operations.SetupTable();
            Operations.FillingTable(table);
            Operations.PrintTable(table);
            Operations.CalculateTable(table);
            Operations.PrintTable(table, true);
        }
    }
}
