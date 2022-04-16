using System;
using System.Collections.Generic;
using System.IO;

namespace PhotoGrouperByYear
{
    class Program
    {
        #region Search Criterias - Don't Change
        private const string searchString_1 = "_BURST";
        private const string searchString_2 = "IMG_";
        private const string searchString_3 = "PXL_";
        private const string searchString_4 = "IMG-";
        private const string searchString_5 = "VID-";
        private const string searchString_6 = "VID_";
        private const string searchString_7 = "V_";
        private const string searchString_8 = "P_";
        private const string searchString_9 = "PHOTO_";
        private const string searchString_10 = "B612_";
        private const string searchString_11 = "GEAK_";
        private const string searchString_12 = "Screenshot_";
        #endregion

        private const string rawPathToSearch = "E:\\ALL PHOTOS\\ALL PHONE PHOTOS (2011-22)";
        private static int SearchYearStart = 2011;
        private static string pathToSearch = Path.GetFullPath(rawPathToSearch);

        static void Main(string[] args)
        {
            // Basic Seach - Based on pre-defined search criteria
            List<PictureInfo> listOfFilesThatMeetsSearchCriteria = GetListOfFileThatMeetSearchCriteria(SearchYearStart, pathToSearch);
            MoveFilesInBulk(listOfFilesThatMeetsSearchCriteria);

            // Advance Search - Based on last file modification date stamp
            List<PictureInfo> listOfFilesBasedOnCreatedDate = GetListOfFileBasedOnCreatedDate(pathToSearch);
            MoveFilesInBulk(listOfFilesBasedOnCreatedDate);

            Console.WriteLine($"Files meeting criteria was moved to respective year level folders.");
        }

        /// <summary>
        /// Get List Of Files Based On File Created Date
        /// </summary>
        /// <param name="searchYearStart"></param>
        /// <param name="pathToSearch"></param>
        /// <returns></returns>
        private static List<PictureInfo> GetListOfFileBasedOnCreatedDate(string pathToSearch)
        {
            string[] listOfFiles = Directory.GetFiles(pathToSearch);
            List<PictureInfo> listOfFilesBasedOnCreatedDate = new List<PictureInfo>();

            for (int i = 0; i < listOfFiles.Length; i++)
            {
                DateTime CreatedOn = File.GetLastWriteTime(listOfFiles[i]);
                listOfFilesBasedOnCreatedDate.Add(new PictureInfo(CreatedOn.Year, listOfFiles[i]));
            }

            return listOfFilesBasedOnCreatedDate;
        }

        /// <summary>
        /// Get List Of Files That Meet Search Criteria
        /// </summary>
        /// <param name="searchYearStart"></param>
        /// <param name="pathToSearch"></param>
        /// <returns></returns>
        private static List<PictureInfo> GetListOfFileThatMeetSearchCriteria(int searchYearStart, string pathToSearch)
        {
            string[] listOfFiles = Directory.GetFiles(pathToSearch);
            List<PictureInfo> listOfFilesThatMeetsSearchCriteria = new List<PictureInfo>();

            // Searching & Collecting Files Meeting Search Criteria
            while (searchYearStart <= DateTime.Now.Year)
            {
                for (int i = 0; i < listOfFiles.Length; i++)
                {
                    if (
                        (listOfFiles[i].Contains(searchString_1 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_2 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_3 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_4 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_5 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_6 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_7 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_8 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_9 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_10 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_11 + searchYearStart)) ||
                        (listOfFiles[i].Contains(searchString_12 + searchYearStart)) ||
                        (Path.GetFileName(listOfFiles[i]).Contains(searchYearStart + "-")) ||
                        (Path.GetFileName(listOfFiles[i]).StartsWith(searchYearStart.ToString()))
                       )
                        listOfFilesThatMeetsSearchCriteria.Add(new PictureInfo(searchYearStart, listOfFiles[i]));
                }
                searchYearStart++;
            }
            return listOfFilesThatMeetsSearchCriteria;
        }

        /// <summary>
        /// Moved Files In Bulk Based On Picture Info Class Content
        /// </summary>
        /// <param name="listOfFilesThatMeetsSearchCriteria"></param>
        private static void MoveFilesInBulk(List<PictureInfo> listOfFilesThatMeetsSearchCriteria)
        {
            foreach (PictureInfo item in listOfFilesThatMeetsSearchCriteria)
                File.Move(item.FileName, Path.GetFullPath($"{rawPathToSearch}\\{item.Year}\\{Path.GetFileName(item.FileName)}"));
        }

        public class PictureInfo
        {
            public int Year;
            public string FileName;

            public PictureInfo(int year, string fileName)
            {
                this.Year = year;
                this.FileName = fileName;
            }
        }
    }
}
