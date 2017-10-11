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
    }

    public struct SlideProgression
    {
        public bool autoProgress;
        public float autoProgressTime;
    }

    public static class Globals
    {

        public static ObservableCollection<ExcelFile> excelFiles = new ObservableCollection<ExcelFile>();
        public static ObservableCollection<PhotoFolder> photoFolders = new ObservableCollection<PhotoFolder>();

        public static DataBehaviour dataBehaviour = new DataBehaviour() { mergeFiles = true, masterFile = 0 };
        public static string pictureNameFormat = "{id}.jpg";
        public static string templateFile = "_blank.potx";
        public static SlideProgression slideProgression = new SlideProgression() { autoProgress = true, autoProgressTime = 2 };
        public static bool dualPictureSlide = true;
        public static StudentSort sortMethod = StudentSort.Surname;

    }
}
