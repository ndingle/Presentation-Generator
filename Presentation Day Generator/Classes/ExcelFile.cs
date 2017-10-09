using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Presentation_Day_Generator
{
    public class ExcelFile : INotifyPropertyChanged
    {

        private string m_Filepath;


        public string Filepath
        {
            get
            {
                return m_Filepath;
            }
            set
            {
                m_Filepath = value;
                OnPropertyChanged();
            }
        }


        public string Filename
        {
            get
            {
                return System.IO.Path.GetFileName(m_Filepath);
            }
        }

        public string Filetype
        {
            get
            {
                return ExcelReader.GetFiletype(m_Filepath);
            }
        }


        public ExcelFile(string filepath)
        {

            Filepath = filepath;

        }

        public override string ToString()
        {
            return Filename;
        }


        protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
