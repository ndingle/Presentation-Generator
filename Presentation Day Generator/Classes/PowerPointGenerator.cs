using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Presentation_Day_Generator
{

    public delegate void PowerPointGenerationCompleted(bool result);

    public class PowerPointGenerator
    {

        public static string BLANK_POWERPOINT = @"Resources\_blank.potx";

        public void Generate(string powerpointTemplate, List<Student> students, PowerPointProgress progress, PowerPointGenerationCompleted callback)
        {

            string powerPointfile = "";

            if (powerpointTemplate != null && powerpointTemplate.Length > 0)
            {

                //If they are using the blank powerpoint use the resource one
                if (powerpointTemplate.ToUpper() == "_BLANK.POTX") powerPointfile = $@"{AppDomain.CurrentDomain.BaseDirectory}\{BLANK_POWERPOINT}";
                else powerPointfile = powerpointTemplate;

                //Create the powerpoint
                CreatePowerPoint(powerPointfile, students, progress);

                callback(true);

            } 
            else
            {
                callback(false);
            }


        }


        static void CreatePowerPoint(string template, List<Student> students, PowerPointProgress progress)
        {

            progress.Minimum = 0;
            progress.Maximum = students.Count;
            progress.Value = 0;

            Application app;
            Presentation presentation;

            //Firstly ask where to save
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save your presentation...";
            dialog.Filter = "PowerPoint Presentations|*.pptx";

            //Show dialog and hopefully save after all that
            if (dialog.ShowDialog().Value == true)
            {

                //Create the overall slideshow
                app = new Application();
                presentation = app.Presentations.Open(template, MsoTriState.msoFalse, MsoTriState.msoCTrue, MsoTriState.msoFalse);

                //Loop through the students
                foreach (Student student in students)
                {
                    //TODO: Add photo slides
                    //Add the Award slide
                    AddAwardSlide(presentation, student);

                    //Increase progress values
                    progress.Value++;

                }

                //Save time
                presentation.SaveAs(dialog.FileName);

                //TODO: Check memory clean
                presentation.Close();
                presentation = null;
                app = null;

            }

        }


        static void AddAwardSlide(Presentation presentation, Student student)
        {

            //Create the slide
            Slide currentSlide = presentation.Slides.Add(presentation.Slides.Count + 1, PpSlideLayout.ppLayoutText);
            currentSlide.Shapes.Title.TextFrame.TextRange.Text = student.Firstname + " " + student.Surname;

            foreach (string award in student.Awards)
            {
                currentSlide.Shapes[2].TextFrame.TextRange.Text += award + "\n";
            }

            if (currentSlide.Shapes[2].TextFrame.TextRange.Text.Length > 0)
                currentSlide.Shapes[2].TextFrame.TextRange.Text = currentSlide.Shapes[2].TextFrame.TextRange.Text.Remove(currentSlide.Shapes[2].TextFrame.TextRange.Text.Length - 1);

            currentSlide.SlideShowTransition.EntryEffect = PpEntryEffect.ppEffectFadeSmoothly;
            currentSlide.SlideShowTransition.Speed = PpTransitionSpeed.ppTransitionSpeedMedium;
            currentSlide.SlideShowTransition.Duration = 1;
            currentSlide.Shapes[2].TextFrame2.AutoSize = MsoAutoSize.msoAutoSizeTextToFitShape;


        }


    }


    public class PowerPointProgress : INotifyPropertyChanged
    {

        private double m_Minimum;
        private double m_Maximum;
        private double m_Value;

        public double Minimum {

            get
            {
                return m_Minimum;
            }

            set
            {
                m_Minimum = value;
                OnPropertyChanged();
            }

        }

        public double Maximum
        {

            get
            {
                return m_Maximum;
            }

            set
            {
                m_Maximum = value;
                OnPropertyChanged();
            }

        }

        public double Value
        {

            get
            {
                return m_Value;
            }

            set
            {
                m_Value = value;
                OnPropertyChanged();
            }

        }

        protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }

}
