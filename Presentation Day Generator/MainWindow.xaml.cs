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
        int currentPage = 1;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Show();
            //Load the first page
            Page1 newPage = new Page1();
            pageFrame.Content = newPage;
            
        }


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            NextPage();
        }


        void NextPage()
        {

            Page newPage = null;


            if (currentPage == 1)
            {
                //Collect the data files
                dataFiles = ((Page1)pageFrame.Content).GetFiles();
                newPage = new Page2();
            }
            else if (currentPage == 2)
            {
                newPage = new Page3();
            }


            //If we have a new page, move on
            if (newPage != null)
            {
                currentPage++;
                pageFrame.Content = newPage;
            }


        }


    }
}
