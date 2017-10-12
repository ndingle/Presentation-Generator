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
        PowerPointProgress generationProgress = new PowerPointProgress();


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

            //TODO: Remove
            Globals.excelFiles.Add(new ExcelFile(@"C:\Users\nicho\Documents\Visual Studio 2017\Projects\Presentation Day Generator\Presentation Day Generator\Test Data\awards.xls"));
            Globals.photoFolders.Add(new PhotoFolder(@"C:\Users\nicho\Desktop\Presentation\Photos\7"));
            Globals.photoFolders.Add(new PhotoFolder(@"C:\Users\nicho\Desktop\Presentation\Photos\8"));
            Globals.photoFolders.Add(new PhotoFolder(@"C:\Users\nicho\Desktop\Presentation\Photos\9"));
            Globals.photoFolders.Add(new PhotoFolder(@"C:\Users\nicho\Desktop\Presentation\Photos\10"));
            Globals.photoFolders.Add(new PhotoFolder(@"C:\Users\nicho\Desktop\Presentation\Photos\11"));
            Globals.photoFolders.Add(new PhotoFolder(@"C:\Users\nicho\Desktop\Presentation\Photos\12"));
            
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


        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Implement the settings correctly
            List<Student> students = ExcelReader.CollateAwards(Globals.excelFiles.ToArray(), Globals.dataBehaviour).ToList(StudentSort.Surname);
            PhotoCollector.CollectPhotos(students, Globals.photoFolders.ToArray(), Globals.pictureNameFormat);

            //Create the power point generator and callback function
            PowerPointGenerator generator = new PowerPointGenerator();
            PowerPointGenerationCompleted callback = GenerationCallback;

            //Create a thread with a callback
            Thread generationThread = new Thread(() => generator.Generate(Globals.templateFile, students, generationProgress, Globals.slideProgression, Globals.dualPictureSlide, callback));
            ThreadStart();
            generationThread.Start();
            
        }


        void GenerationCallback(bool result)
        {
            MessageBox.Show("Complete.");
            Dispatcher.Invoke(ThreadFinish);
        }


        void ThreadStart()
        {
            generating = true;
            btnNext.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnDone.IsEnabled = false;
            proGeneration.Visibility = Visibility.Visible;

        }


        void ThreadFinish()
        {
            generating = false;
            btnNext.IsEnabled = true;
            btnPrevious.IsEnabled = true;
            btnDone.IsEnabled = true;
            proGeneration.Visibility = Visibility.Collapsed;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (generating)
                e.Cancel = true;
        }
    }

}
