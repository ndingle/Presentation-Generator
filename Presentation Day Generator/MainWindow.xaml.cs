using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Presentation_Day_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //The variables of niceness
        List<ExcelFile> dataFiles;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //Load the first page
            Page1 newPage = new Page1();
            pageFrame.Content = newPage;
            
        }


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

            //Page1 to Page2 transition work
            dataFiles = ((Page1)pageFrame.Content).GetFiles();

            MessageBox.Show(dataFiles.Count.ToString());

            Page2 newPage = new Page2();
            pageFrame.Content = newPage;

        }


    }
}
