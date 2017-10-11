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
        string[] pageTitles = { "Drag and drop excel files", "Drag and drop photo folders", "Ready, any settings?" };
        Page[] pages;
        int currentPage = 0;


        public MainWindow()
        {
            InitializeComponent();
            pages = new Page[3];
            pages[0] = new Page1();
            pages[1] = new Page2();
            pages[2] = new Page3();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            UpdatePage();

            //TODO: Remove
            Globals.excelFiles.Add(new ExcelFile(@"C:\Users\nicho\Documents\Visual Studio 2017\Projects\Presentation Day Generator\Presentation Day Generator\Test Data\awards.xls"));
            
        }


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            UpdatePage();
        }


        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            currentPage--;
            UpdatePage();
        }


        void UpdatePage()
        {

            if (currentPage > 2)
                currentPage = 2;
            else if (currentPage < 0)
                currentPage = 0;

            pageFrame.Navigate(pages[currentPage]);
            txbTitle.Text = pageTitles[currentPage];

            if (currentPage == 2)
            {
                btnDone.Visibility = Visibility.Visible;
                btnNext.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnDone.Visibility = Visibility.Collapsed;
                btnNext.Visibility = Visibility.Visible;
            }

            if (currentPage == 0)
            {
                btnPrevious.IsEnabled = false;
            }
            else
            {
                btnPrevious.IsEnabled = true;
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Remove
            ExcelReader reader = new ExcelReader();
            List<Student> students = reader.CollateAwards(Globals.excelFiles.ToArray()).ToList(StudentSort.Surname);
        }


    }

}
