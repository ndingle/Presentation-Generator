﻿using System;
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


        //Window variables go here!
        ObservableCollection<ExcelFile> excelFiles;


        public Page1()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            excelFiles = new ObservableCollection<ExcelFile>();
            lstFiles.DataContext = excelFiles;
        }


        void CompleteDrop(IDataObject data)
        {

            string[] files = (string[])data.GetData(DataFormats.FileDrop);

            //Ensure they are excel files
            foreach (string file in files)
            {
                
                excelFiles.Add(new ExcelFile(file));
                imgDragFiles.Visibility = Visibility.Hidden;

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


        public List<ExcelFile> GetFiles()
        {

            List<ExcelFile> result = new List<ExcelFile>();

            foreach(ExcelFile file in excelFiles)
            {
                if (file.Filetype.ToUpper() != "Invalid file")
                    result.Add(file);
            }

            return result;

        }


        public void btnDelete_Clicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is ExcelFile)
            {
                ExcelFile file = (ExcelFile)btn.DataContext;
                excelFiles.Remove(file);
            }
        }

    }
}
