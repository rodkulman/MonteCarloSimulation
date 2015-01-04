using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace malarazz.montecarlo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            int range, amount, interval, iterations;

            int.TryParse(txtRange.Text, out range);
            int.TryParse(txtAmount.Text, out amount);
            int.TryParse(txtInterval.Text, out interval);
            int.TryParse(txtIterations.Text, out iterations);

            var matching = new int[amount + 1];
            rnd = new Random();

            for (int i = 0; i < iterations; i++)
            {
                var numbers = getNumbers(range, amount, chkReplacement.IsChecked.Value);
                var index = numbers.Count(n => n <= interval);

                matching[index]++;
            }

            txtResult.Text = string.Empty;

            for (int i = 0; i < matching.Length; i++)
            {
                txtResult.Text += string.Format("{0}: {1}\r\n", i, matching[i]);
            }
        }

        private Random rnd;
        private int[] getNumbers(int range, int amount, bool replacement)
        {
            var retVal = new int[amount];

            Func<int> getNumber;

            if (replacement)
            {
                getNumber = new Func<int>(() => rnd.Next(1, range + 1));
            }
            else
            {
                getNumber = new Func<int>(() => 
                {
                    int ret;

                    do
                    {
                        ret = rnd.Next(1, range + 1);
                    } while (retVal.Contains(ret));

                    return ret;
                });
            }

            for (int i = 0; i < amount; i++)
            {
                retVal[i] = getNumber();
            }

            return retVal;
        }
    }
}
