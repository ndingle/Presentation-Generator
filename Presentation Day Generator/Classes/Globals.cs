using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Day_Generator
{
    public static class Globals
    {

        public static ObservableCollection<ExcelFile> excelFiles = new ObservableCollection<ExcelFile>();
        public static ObservableCollection<PhotoFolder> photoFolders = new ObservableCollection<PhotoFolder>();

    }
}
