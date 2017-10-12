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

        public void Generate(SlideshowSettings settings, List<Student> students, PowerPointProgressBar progress, PowerPointGenerationCompleted callback)
        {

            string powerPointfile = "";

            if (settings.templateFilename != null && settings.templateFilename.Length > 0)
            {

                //If they are using the blank powerpoint use the resource one
                if (settings.templateFilename.ToUpper() == "_BLANK.POTX") powerPointfile = $@"{AppDomain.CurrentDomain.BaseDirectory}\{BLANK_POWERPOINT}";
                else powerPointfile = settings.templateFilename;

                //Create the powerpoint
                CreatePowerPoint(powerPointfile, settings, students, progress);

                callback(true);

            } 
            else
            {
                callback(false);
            }


        }


        static void CreatePowerPoint(string template, SlideshowSettings settings, List<Student> students, PowerPointProgressBar progress)
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

                    //Add slides
                    AddPhotoSlide(settings, presentation, student);
                    AddAwardSlide(presentation, student);

                    //Increase progress values
                    progress.Value++;

                }

                //Save time
                presentation.SaveAs(dialog.FileName);

                //Clean up memory I hope
                presentation.Close();
                presentation = null;
                app = null;

            }

        }


        static void AddPhotoSlide(SlideshowSettings settings, Presentation presentation, Student student)
        {

            Slide currentSlide = presentation.Slides.Add(presentation.Slides.Count + 1, PpSlideLayout.ppLayoutText);
            currentSlide.Shapes.Title.TextFrame.TextRange.Text = student.Firstname + " " + student.Surname;

            //Dual pictures time
            if (student.Photos.Count > 1 && settings.dualPictureSlides)
            {

                Microsoft.Office.Interop.PowerPoint.Shape shp = currentSlide.Shapes.AddPicture(student.Photos[0], MsoTriState.msoFalse, MsoTriState.msoTrue, 50, 150);
                shp.LockAspectRatio = MsoTriState.msoTrue;
                shp.Width = 260f;

                currentSlide.TimeLine.MainSequence.AddEffect(shp, MsoAnimEffect.msoAnimEffectFade, MsoAnimateByLevel.msoAnimateLevelNone, MsoAnimTriggerType.msoAnimTriggerWithPrevious);


                Microsoft.Office.Interop.PowerPoint.Shape shp2 = currentSlide.Shapes.AddPicture(student.Photos[student.Photos.Count-1], MsoTriState.msoFalse, MsoTriState.msoTrue, 360, 150);
                shp2.LockAspectRatio = MsoTriState.msoTrue;
                shp2.Width = 260f;

                currentSlide.TimeLine.MainSequence.AddEffect(shp2, MsoAnimEffect.msoAnimEffectFade, MsoAnimateByLevel.msoAnimateLevelNone, MsoAnimTriggerType.msoAnimTriggerAfterPrevious);

            }
            else if (student.Photos.Count == 1 || !settings.dualPictureSlides)
            {

                Microsoft.Office.Interop.PowerPoint.Shape shp = currentSlide.Shapes.AddPicture(student.Photos[student.Photos.Count - 1], MsoTriState.msoFalse, MsoTriState.msoTrue, 0, 0);
                shp.LockAspectRatio = MsoTriState.msoTrue;
                shp.Width = 250;
                shp.Top = 140;
                shp.Left = 215;

                //Add the animation to the object
                currentSlide.TimeLine.MainSequence.AddEffect(shp, MsoAnimEffect.msoAnimEffectFade, MsoAnimateByLevel.msoAnimateLevelNone, MsoAnimTriggerType.msoAnimTriggerWithPrevious);

            }

            //Determine how they want to progress
            if (settings.autoProgress)
            {
                currentSlide.SlideShowTransition.AdvanceOnTime = MsoTriState.msoTrue;
                currentSlide.SlideShowTransition.AdvanceTime = settings.autoProgressTime;
            }
            else
            {
                currentSlide.SlideShowTransition.AdvanceOnClick = MsoTriState.msoTrue;
            }


            currentSlide.SlideShowTransition.EntryEffect = PpEntryEffect.ppEffectFadeSmoothly;
            currentSlide.SlideShowTransition.Speed = PpTransitionSpeed.ppTransitionSpeedMedium;
            currentSlide.SlideShowTransition.Duration = 1.5f;

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


    public class PowerPointProgressBar : INotifyPropertyChanged
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
