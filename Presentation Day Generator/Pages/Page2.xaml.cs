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

namespace Presentation_Day_Generator
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstPhotos.DataContext = Globals.photoFolders;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is PhotoFolder)
            {
                PhotoFolder file = (PhotoFolder)btn.DataContext;
                Globals.photoFolders.Remove(file);
            }
        }

        private void lstPhotos_Drop(object sender, DragEventArgs e)
        {
            CompleteDrop(e.Data);
        }

        void CompleteDrop(IDataObject data)
        {

            string[] files = (string[])data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {

                Globals.photoFolders.Add(new PhotoFolder(file));
                imgDragFiles.Visibility = Visibility.Hidden;

            }

        }

        private void lstPhotos_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Copy;
        }

        private void imgDragFiles_Drop(object sender, DragEventArgs e)
        {
            CompleteDrop(e.Data);
        }
    }
}
