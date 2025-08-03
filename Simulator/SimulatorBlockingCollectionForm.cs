using Simulator.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
/*
- Detalles clave del ejercicio
Debes usar una BlockingCollection<MarketData> como buffer entre el productor y el consumidor.

    - Productor: genera datos aleatorios (por ejemplo, símbolo de acción + precio) y los mete en la BlockingCollection periódicamente.

    - Consumidor: toma esos datos uno a uno y actualiza una DataGridView o cualquier otra parte de la UI.

💡 Pistas para resolverlo (sin darte el código):
Define una clase MarketData con propiedades como Symbol, Price, Timestamp.

Usa dos Task:

   - Una que produce: cada cierto tiempo (ej. 200ms) genera un MarketData aleatorio.

   - Una que consume: se queda esperando en un bucle foreach (var item in blockingCollection.GetConsumingEnumerable()) y actualiza la UI con Invoke.

Cuando el usuario pulse un botón “Detener”, debes usar un CancellationTokenSource y llamar a CompleteAdding() en la colección.

Cuida bien el acceso a la UI: el consumidor está en un hilo de fondo, así que cualquier cambio visual debe ir dentro de un Invoke o BeginInvoke.

⚙️ ¿Qué estás practicando?
Comunicación segura entre hilos (sin usar lock)

Separación de responsabilidades (productor/consumidor)

Control de flujo usando BlockingCollection (ideal en entrevistas)

Cómo no bloquear la UI y mantener todo fluido

Interacción entre lógica de negocio e interfaz
 */
namespace Simulator
{
    public partial class SimulatorBlockingCollectionForm : Form
    {
        private CancellationTokenSource cts;
        private readonly Random random = new Random();
        //private readonly List<MarketData> marketDataList = new List<MarketData>();
        private readonly BlockingCollection<MarketData> blockingCollection = new BlockingCollection<MarketData>(new ConcurrentQueue<MarketData>());
        private readonly ConcurrentDictionary<string, MarketData> marketDataDict = new ConcurrentDictionary<string, MarketData>();
        public SimulatorBlockingCollectionForm()
        {
            InitializeComponent();
            UIHelpers.InitializeMarketDataGrid(dataGridViewPrices);
        }

        private void SimulatorBlockingCollectionForm_Load(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            StartDataGeneration(cts.Token);
        }
        private void StartDataGeneration(CancellationToken token)
        {

            ProduceMarketData(300, token);
            ConsumeMarketData(token);
        }
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
        private void ConsumeMarketData(CancellationToken token)
        {
            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    foreach (var item in blockingCollection.GetConsumingEnumerable())
                    {
                        if (token.IsCancellationRequested) break;

                        this.BeginInvoke((MethodInvoker)(() =>
                        {
                            marketDataDict.AddOrUpdate(item.Symbol, item, (key, existing) =>
                            {
                                // Guardar el precio anterior antes de actualizar
                                item.PreviousPrice = existing.Price;
                                return item;
                            });

                            UpdateDataGridView();
                        }));
                    }

                }
            }, token);
        }


        private void GenerateRandomData()
        {
            // Simula actualizar precios de 5 símbolos
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

                if (data.PreviousPrice != 0)
                {
                    if (data.Price > data.PreviousPrice)
                    {
                        changeSymbol = "🔼";  // Subida
                        row.Cells[3].Style.ForeColor = Color.Green;
                    }
                    else if (data.Price < data.PreviousPrice)
                    {
                        changeSymbol = "🔽";  // Bajada
                        row.Cells[3].Style.ForeColor = Color.Red;
                    }
                }
                row.Cells[3].Value = changeSymbol;
                dataGridViewPrices.Rows.Add(row);
            }
        }

        private void SimulatorBlockingCollectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
            blockingCollection.CompleteAdding();
        }
    }
}
