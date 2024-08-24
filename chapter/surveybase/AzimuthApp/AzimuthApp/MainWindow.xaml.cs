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

namespace AzimuthApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AzimuthUI azimuthUI;
        public MainWindow()
        {
            InitializeComponent();

            azimuthUI = new AzimuthUI();
            this.DataContext = azimuthUI;
        }

        private void buttonAzimuth_Click(object sender, RoutedEventArgs e)
        {
            //double.TryParse(textBoxAX.Text, out double xA);
            //double.TryParse(textBoxAY.Text, out double yA);
            //double.TryParse(textBoxBX.Text, out double xB);
            //double.TryParse(textBoxBY.Text, out double yB);

            //double distanceAB = ZXY.SMath.Azimuth(xA, yA, xB, yB, out double azimuthAB);

            //textBoxAzimuth.Text = ZXY.SMath.RADtoString(azimuthAB);
            //textBoxDistance.Text = distanceAB.ToString();
            //textBlockAzimuth.Text = $"{textBoxAName.Text}->{textBoxBName.Text}坐标方位角:";

            azimuthUI.CalAzimuthDistanceAB();
        }
    }
}
