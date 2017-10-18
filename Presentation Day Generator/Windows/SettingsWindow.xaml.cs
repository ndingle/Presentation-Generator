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
using System.Windows.Shapes;

namespace Presentation_Day_Generator
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void optMergeFiles_Checked(object sender, RoutedEventArgs e)
        {
            if (cmbFiles != null)
                cmbFiles.IsEnabled = false;
        }

        private void optMasterFile_Checked(object sender, RoutedEventArgs e)
        {
            if (cmbFiles != null)
                cmbFiles.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Setup the data behaviour
            if (Globals.excelFiles.Count > 0)
                cmbFiles.ItemsSource = Globals.excelFiles;
            else
                cmbFiles.Items.Add("<No data files>");

            cmbFiles.SelectedIndex = 0;

            optMergeFiles.IsChecked = Globals.dataSettings.mergeFiles;
            optMasterFile.IsChecked = !Globals.dataSettings.mergeFiles;

            //Setup picture format
            txtFormat.Text = Globals.pictureNameFormat;

            //Setup template file
            txtTemplate.Text = Globals.slideshowSettings.templateFilename;

            //Setup progression style
            optAuto.IsChecked = Globals.slideshowSettings.autoProgress;
            optOnClick.IsChecked = !Globals.slideshowSettings.autoProgress;
            txtProgressTime.Text = Globals.slideshowSettings.autoProgressTime.ToString();

            //Setup slide format
            optDualPicture.IsChecked = Globals.slideshowSettings.dualPictureSlides;
            optSinglePicture.IsChecked = !Globals.slideshowSettings.dualPictureSlides;

            //Sorted
            cmbSort.SelectedIndex = (int)Globals.dataSettings.sortingMethod;

        }

        private void optOnClick_Checked(object sender, RoutedEventArgs e)
        {
            if (txtProgressTime != null)
                txtProgressTime.IsEnabled = false;
        }

        private void optAuto_Checked(object sender, RoutedEventArgs e)
        {
            if (txtProgressTime != null)
                txtProgressTime.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            Globals.dataSettings.mergeFiles = optMergeFiles.IsChecked.Value;
            Globals.dataSettings.masterFile = cmbFiles.SelectedIndex;
            Globals.dataSettings.sortingMethod = (StudentSort)cmbSort.SelectedIndex;

            Globals.pictureNameFormat = txtFormat.Text;

            Globals.slideshowSettings.templateFilename = txtTemplate.Text;
            Globals.slideshowSettings.autoProgress = optAuto.IsChecked.Value;
            Globals.slideshowSettings.dualPictureSlides = optDualPicture.IsChecked.Value;
            float.TryParse(txtProgressTime.Text, out Globals.slideshowSettings.autoProgressTime);

            this.Close();

        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select PowerPoint Template...";
            openDialog.Filter = "PowerPoint Templates|*.potx;*.pot";

            if (openDialog.ShowDialog().Value == true)
            {
                txtTemplate.Text = openDialog.FileName;
            }

        }
    }
}
