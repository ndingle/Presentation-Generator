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


        //Window variables go here!
        ObservableCollection<ExcelFile> excelFiles;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            excelFiles = new ObservableCollection<ExcelFile>();
            lstFiles.ItemsSource = excelFiles;
        }


        void CompleteDrop(IDataObject data)
        {

            string[] files = (string[])data.GetData(DataFormats.FileDrop);

            //Ensure they are excel files
            foreach (string file in files)
            {

                //Get the extension of the file
                string extension = file.Substring(file.LastIndexOf('.') + 1);

                //Are they excel?
                if (extension == "xlsx" || extension == "xls")
                {
                    excelFiles.Add(new ExcelFile(file));
                    imgDragFiles.Visibility = Visibility.Hidden;
                }

            }

        }

        private void lstFiles_Drop(object sender, DragEventArgs e)
        {
            CompleteDrop(e.Data);
        }

        private void imgDragFiles_Drop(object sender, DragEventArgs e)
        {
            CompleteDrop(e.Data);
        }
    }
}
