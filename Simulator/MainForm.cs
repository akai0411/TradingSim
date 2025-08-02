namespace Simulator
{
    using System.Windows.Forms;

    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

        }

        private void btnBasicThread_Click(object sender, System.EventArgs e)
        {
            SimulatorBasicThreadForm newBasicForm = new SimulatorBasicThreadForm();


            newBasicForm.Show();
        }

        private void btnConcDictionary_Click(object sender, System.EventArgs e)
        {
            SimulatorConcurrentDictionaryForm newConcForm = new SimulatorConcurrentDictionaryForm();
            newConcForm.Show();
        }
    }
}
