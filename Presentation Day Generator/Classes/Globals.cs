using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Day_Generator
{

    public struct DataBehaviour
    {
        public bool mergeFiles;
        public int masterFile;
        public StudentSort sortingMethod;
    }

    public struct SlideshowSettings
    {
        public string templateFilename;
        public bool autoProgress;
        public float autoProgressTime;
        public bool dualPictureSlides;
    }

    public static class Globals
    {

        public static ObservableCollection<ExcelFile> excelFiles = new ObservableCollection<ExcelFile>();
        public static ObservableCollection<PhotoFolder> photoFolders = new ObservableCollection<PhotoFolder>();

        public static DataBehaviour dataSettings = new DataBehaviour() { mergeFiles = true, masterFile = 0, sortingMethod = StudentSort.Surname };
        public static string pictureNameFormat = "{surname}, {firstname}.jpg";
        public static SlideshowSettings slideshowSettings = new SlideshowSettings() { templateFilename = "_blank.potx", autoProgress = true, autoProgressTime = 2, dualPictureSlides = true };

    }
}
