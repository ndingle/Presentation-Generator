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

            optMergeFiles.IsChecked = Globals.dataBehaviour.mergeFiles;
            optMasterFile.IsChecked = !Globals.dataBehaviour.mergeFiles;

            //Setup picture format
            txtFormat.Text = Globals.pictureNameFormat;

            //Setup template file
            txtTemplate.Text = Globals.templateFile;

            //Setup progression style
            optAuto.IsChecked = Globals.slideProgression.autoProgress;
            optOnClick.IsChecked = !Globals.slideProgression.autoProgress;
            txtProgressTime.Text = Globals.slideProgression.autoProgressTime.ToString();

            //Setup slide format
            optDualPicture.IsChecked = Globals.dualPictureSlide;
            optSinglePicture.IsChecked = !Globals.dualPictureSlide;

            //Sorted
            cmbSort.SelectedIndex = (int)Globals.sortMethod;

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
            Globals.dataBehaviour.mergeFiles = optMergeFiles.IsChecked.Value;
            Globals.dataBehaviour.masterFile = cmbFiles.SelectedIndex;
            Globals.pictureNameFormat = txtFormat.Text;
            Globals.templateFile = txtTemplate.Text;
            Globals.slideProgression.autoProgress = optAuto.IsChecked.Value;
            float.TryParse(txtProgressTime.Text, out Globals.slideProgression.autoProgressTime);
            Globals.dualPictureSlide = optDualPicture.IsChecked.Value;
            Globals.sortMethod = (StudentSort)cmbSort.SelectedIndex;
            this.Close();
        }
    }
}
