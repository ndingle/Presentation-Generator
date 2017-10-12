using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Presentation_Day_Generator
{
    public class PhotoCollector
    {

        public static void CollectPhotos(List<Student> students, PhotoFolder[] photoFolders, string filenameFormat)
        {

            //Now load all the folders
            foreach (PhotoFolder folder in photoFolders)
            {
                EnumerateFolder(folder, students, filenameFormat.ToLower());
            }


        }


        public static void EnumerateFolder(PhotoFolder folder, List<Student> students, string filenameFormat)
        {

            //Get all files in the folder
            List<string> files = new List<string>();
            files.AddRange(Directory.EnumerateFiles(folder.FolderPath));

            //Loop through students and try and find their data
            foreach (Student student in students)
            {

                string studentFilename = filenameFormat.Replace("{id}",student.Id.ToString());
                studentFilename = studentFilename.Replace("{firstname}", student.Firstname);
                studentFilename = studentFilename.Replace("{surname}", student.Surname);

                int photoIndex = files.FindIndex(x => x.IndexOf(studentFilename, StringComparison.OrdinalIgnoreCase) >= 0);

                //If we found a photo, then add to the student and remove the photo from the list
                if (photoIndex >= 0)
                {
                    student.Photos.Add(files[photoIndex]);
                    files.RemoveAt(photoIndex);
                }

            }


        }

    }
}
