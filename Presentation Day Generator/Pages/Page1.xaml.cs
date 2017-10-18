using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {


        public Page1()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstFiles.DataContext = Globals.excelFiles;
        }


        void CompleteDrop(IDataObject data)
        {

            
            string[] files = (string[])data.GetData(DataFormats.FileDrop);

            //Ensure they are excel files
            foreach (string file in files)
            {

                Globals.excelFiles.Add(new ExcelFile(file));
                imgDragFiles.Visibility = Visibility.Hidden;

            }

        }


        private void lstFiles_Drop(object sender, DragEventArgs e)
        {
            //TODO: Check file formats as they come in
            //MessageBox.Show(e.Data.GetFormats()[0]);
            CompleteDrop(e.Data);
        }


        private void imgDragFiles_Drop(object sender, DragEventArgs e)
        {
            CompleteDrop(e.Data);
        }


        public List<ExcelFile> GetFiles()
        {

            List<ExcelFile> result = new List<ExcelFile>();

            if (Globals.excelFiles != null)
            {

                foreach (ExcelFile file in Globals.excelFiles)
                {
                    if (file.Filetype.ToUpper() != "Invalid file")
                        result.Add(file);
                }

            }

            return result;

        }


        public void btnDelete_Clicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is ExcelFile)
            {
                ExcelFile file = (ExcelFile)btn.DataContext;
                Globals.excelFiles.Remove(file);
            }
        }


        private void lstFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Copy;
        }


    }
}
