using System.Windows.Forms;

namespace Simulator.Helpers.UI
{
    public static class UIHelpers
    {
        public static void InitializeMarketDataGrid(DataGridView dgv)
        {
            dgv.ColumnCount = 4;
            dgv.Columns[0].Name = "Symbol";
            dgv.Columns[1].Name = "Price";
            dgv.Columns[2].Name = "Timestamp";
            dgv.Columns[3].Name = "Price movement";
        }
    }

}
