using Simulator.Helpers.UI;
using Simulator.Models;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{
    public partial class SimulatorBlockingCollectionForm : Form
    {
        // CancellationTokenSource to control the lifetime of producer and consumer tasks
        private CancellationTokenSource cts;
        // Random number generator for simulating market data
        private readonly Random random = new Random();
        // BlockingCollection used as a thread-safe buffer between producer and consumer
        private readonly BlockingCollection<MarketData> blockingCollection = new BlockingCollection<MarketData>(new ConcurrentQueue<MarketData>());
        // ConcurrentDictionary to store the latest market data for each symbol
        private readonly ConcurrentDictionary<string, MarketData> marketDataDict = new ConcurrentDictionary<string, MarketData>();

        public SimulatorBlockingCollectionForm()
        {
            InitializeComponent();
            // Initialize the DataGridView for displaying market data
            UIHelpers.InitializeMarketDataGrid(dataGridViewPrices);
        }

        // Event handler for form load
        private void SimulatorBlockingCollectionForm_Load(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            StartDataGeneration(cts.Token);
        }

        // Starts the producer and consumer tasks
        private void StartDataGeneration(CancellationToken token)
        {
            ProduceMarketData(300, token);
            ConsumeMarketData(token);
        }

        // Producer: generates random market data at a fixed interval and adds it to the blocking collection
        private void ProduceMarketData(int delay, CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    GenerateRandomData();
                    await Task.Delay(delay);
                }
            }, token);
        }

        // Consumer: takes market data from the blocking collection and updates the UI
        private void ConsumeMarketData(CancellationToken token)
        {
            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    foreach (var item in blockingCollection.GetConsumingEnumerable())
                    {
                        if (token.IsCancellationRequested) break;

                        // Update the market data dictionary and refresh the DataGridView on the UI thread
                        this.BeginInvoke((MethodInvoker)(() =>
                        {
                            marketDataDict.AddOrUpdate(item.Symbol, item, (key, existing) =>
                            {
                                // Store the previous price before updating
                                item.PreviousPrice = existing.Price;
                                return item;
                            });

                            UpdateDataGridView();
                        }));
                    }

                }
            }, token);
        }

        // Generates a random MarketData object and adds it to the blocking collection
        private void GenerateRandomData()
        {
            string[] symbols = { "AAPL", "GOOG", "MSFT", "AMZN", "TSLA", "BTC" };

            int index = random.Next(symbols.Length);
            string randomSymbol = symbols[index];

            decimal price = Math.Round((decimal)(random.NextDouble() * 10000 + 100), 2);
            blockingCollection.Add(new MarketData
            {
                Symbol = randomSymbol,
                Price = price,
                Timestamp = DateTime.Now
            });
        }

        // Updates the DataGridView with the latest market data
        private void UpdateDataGridView()
        {
            dataGridViewPrices.Rows.Clear();

            foreach (var data in marketDataDict.Values.OrderBy(d => d.Symbol))
            {
                string changeSymbol = "";
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewPrices);

                row.Cells[0].Value = data.Symbol;
                row.Cells[1].Value = data.Price;
                row.Cells[2].Value = data.Timestamp.ToString("ss.fff");

                // Indicate price change direction with an arrow and color
                if (data.PreviousPrice != 0)
                {
                    if (data.Price > data.PreviousPrice)
                    {
                        changeSymbol = "🔼";
                        row.Cells[3].Style.ForeColor = Color.Green;
                    }
                    else if (data.Price < data.PreviousPrice)
                    {
                        changeSymbol = "🔽";
                        row.Cells[3].Style.ForeColor = Color.Red;
                    }
                }
                row.Cells[3].Value = changeSymbol;
                dataGridViewPrices.Rows.Add(row);
            }
        }

        // Event handler for form closing: cancels tasks and completes the blocking collection
        private void SimulatorBlockingCollectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
            blockingCollection.CompleteAdding();
        }
    }
}
