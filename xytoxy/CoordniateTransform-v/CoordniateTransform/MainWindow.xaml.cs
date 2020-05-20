using Microsoft.Win32;
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

namespace CoordniateTransform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private CoordinateSystem cs;
        public MainWindow()
        {
            InitializeComponent();

            cs = new CoordinateSystem();
            this.DataContext = cs;
        }

        private void menuItem_OpenTextFileData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "平面坐标相似变换数据文件|*.txt|All File(*.*)|*.*";
            if (dlg.ShowDialog() != true) return;

            cs.ReadTextFileData(dlg.FileName);
        }

        private void menuItem_SaveTextFileData_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "平面坐标相似变换数据文件|*.txt|All File(*.*)|*.*";
            if (dlg.ShowDialog() != true) return;

            cs.WriteTextFileData(dlg.FileName);
        }

        private void menuItem_CalCoefficient_Click(object sender, RoutedEventArgs e)
        {
            cs.CalCoefficient();
        }

        private void menuItem_Cal_UnKnw_XY_Click(object sender, RoutedEventArgs e)
        {
            cs.CalUnKnwXY();
        }
                
        private void menuItem_Out_Cal_Detail_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "平面坐标相似变换计算过程数据|*.txt|All File(*.*)|*.*";
            if (dlg.ShowDialog() != true) return;

            cs.OutDetailTextFile(dlg.FileName);
        }


    private void menuItem_Write_Result_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "平面坐标相似变换成果数据文件|*.txt|All File(*.*)|*.*";
            if (dlg.ShowDialog() != true) return;

            cs.WriteResultTextFileData(dlg.FileName);
        }

        private void menuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Cal_cd_Click(object sender, RoutedEventArgs e)
        {
            cs.CalCd();
        }
    }
}
