namespace Simulator
{
    using Simulator.Helpers;
    using Simulator.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class SimulatorBasicThreadForm : Form
    {
        private readonly Random random = new Random();
        private readonly List<MarketData> marketDataList = new List<MarketData>();
        private CancellationTokenSource cts;

        public SimulatorBasicThreadForm()
        {
            InitializeComponent();
            UIHelpers.InitializeMarketDataGrid(dataGridViewPrices);
        }

        private void SimulatorBasicThread_Load(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            StartDataGeneration(cts.Token);
        }

        private void StartDataGeneration(CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
            {
                    GenerateRandomData();

                    // Invoca la actualización de la UI en el hilo principal
                    this.Invoke(new Action(UpdateDataGridView));

                    await Task.Delay(500); // espera 200 ms
                }
            }, token);
        }

        private void GenerateRandomData()
        {
            // Simula actualizar precios de 5 símbolos
            string[] symbols = { "AAPL", "GOOG", "MSFT", "AMZN", "TSLA","BTC" };

            marketDataList.Clear();

            foreach (var symbol in symbols)
            {
                decimal price = Math.Round((decimal)(random.NextDouble() * 1000 + 100), 2);
                marketDataList.Add(new MarketData
                {
                    Symbol = symbol,
                    Price = price,
                    Timestamp = DateTime.Now
                });
            }
        }

        private void UpdateDataGridView()
        {
            dataGridViewPrices.Rows.Clear();

            foreach (var data in marketDataList)
            {
                dataGridViewPrices.Rows.Add(data.Symbol, data.Price, data.Timestamp.ToString("ss.fff"));
            }
        }

        private void SimulatorBasicThread_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
        }
    }

}
