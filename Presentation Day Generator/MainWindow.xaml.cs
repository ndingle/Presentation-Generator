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
using System.Threading;

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
        bool generating = false;
        PowerPointProgressBar generationProgress = new PowerPointProgressBar();


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
            proGeneration.DataContext = generationProgress;
            
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
                btnPrevious.Visibility = Visibility.Hidden;
            }
            else
            {
                btnPrevious.Visibility = Visibility.Visible;
            }

        }


        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            //Collate students and add their photos
            List<Student> students = ExcelReader.CollateAwards(Globals.excelFiles.ToArray(), Globals.dataSettings).ToList(Globals.dataSettings.sortingMethod);
            PhotoCollector.CollectPhotos(students, Globals.photoFolders.ToArray(), Globals.pictureNameFormat);

            //Create the power point generator and callback function
            PowerPointGenerator generator = new PowerPointGenerator();
            PowerPointGenerationCompleted callback = GenerationCallback;

            //Create a thread with a callback
            Thread generationThread = new Thread(() => generator.Generate(Globals.slideshowSettings, students, generationProgress, callback));
            ThreadStart();
            generationThread.Start();
            
        }


        void GenerationCallback(bool result)
        {
            MessageBox.Show("Complete.");
            //Invoke the UI thread to update restore buttons and such
            Dispatcher.Invoke(ThreadFinish);
        }


        void ThreadStart()
        {
            generating = true;
            btnNext.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnDone.IsEnabled = false;
            pageFrame.IsEnabled = false;
            proGeneration.Visibility = Visibility.Visible;

        }


        void ThreadFinish()
        {
            generating = false;
            btnNext.IsEnabled = true;
            btnPrevious.IsEnabled = true;
            btnDone.IsEnabled = true;
            pageFrame.IsEnabled = true;
            proGeneration.Visibility = Visibility.Collapsed;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (generating)
                e.Cancel = true;
        }
    }

}
