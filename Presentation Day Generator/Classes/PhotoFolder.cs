using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Day_Generator
{
    public class PhotoFolder : INotifyPropertyChanged
    {
        private string m_FolderPath;

        public string FolderPath
        {
            get
            {
                return m_FolderPath;
            }
            set
            {
                m_FolderPath = value;
                OnPropertyChanged();
            }
        }

        public string FolderName
        {
            get
            {
                return new DirectoryInfo(m_FolderPath).Name;
            }
        }

        public string Validity
        {
            get
            {
                int temp;
                if (IsFolder() && int.TryParse(FolderName, out temp))
                    return "";
                else
                    return "Invalid";
            }
        }


        public bool IsFolder()
        {
            return File.GetAttributes(m_FolderPath).HasFlag(FileAttributes.Directory);
        }


        public PhotoFolder(string path)
        {
            FolderPath = path;
        }


        protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
