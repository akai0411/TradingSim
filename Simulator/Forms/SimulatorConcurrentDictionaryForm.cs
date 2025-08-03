namespace Simulator
{
    using Simulator.Helpers.UI;
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class SimulatorConcurrentDictionaryForm : Form
    {
        private readonly Random random = new Random();
        private CancellationTokenSource cts;
        private readonly ConcurrentDictionary<string, decimal> symbolPrices = new ConcurrentDictionary<string, decimal>
        {
            ["AAPL"] = 0,
            ["GOOG"] = 0,
            ["MSFT"] = 0,
            ["AMZN"] = 0,
            ["TSLA"] = 0,
            ["META"] = 0
        };

        public SimulatorConcurrentDictionaryForm()
        {
            InitializeComponent();
            UIHelpers.InitializeMarketDataGrid(dataGridViewPrices);
        }

        private void SimulatorConcurrentDictionary_Load(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            StartDataGeneration(cts.Token);
        }

        private void StartDataGeneration(CancellationToken token)
        {
            string[] group1 = { "AAPL", "MSFT" };
            string[] group2 = { "GOOG", "META" };
            string[] group3 = { "TSLA", "AMZN" };

            CreateUpdateTask(group1, 200, token);
            CreateUpdateTask(group2, 100, token);
            CreateUpdateTask(group3, 150, token);
        }
        private void CreateUpdateTask(string[] symbols, int delay, CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    GenerateRandomData(symbols);

                    this.Invoke(new Action(UpdateDataGridView));

                    await Task.Delay(delay);
                }
            }, token);
        }

        private void GenerateRandomData(string[] symbolsToUpdate)
        {
            foreach (var symbol in symbolsToUpdate)
            {
                decimal price = Math.Round((decimal)(random.NextDouble() * 1000 + 100), 2);
                symbolPrices[symbol] = price;
            }
        }

        private void UpdateDataGridView()
        {
            dataGridViewPrices.Rows.Clear();

            foreach (var kvp in symbolPrices)
            {
                dataGridViewPrices.Rows.Add(kvp.Key, kvp.Value.ToString("F2"), DateTime.Now.ToString("ss.fff"));
            }
        }

        private void ConcurrentDictionary_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
        }
    }

 
}
