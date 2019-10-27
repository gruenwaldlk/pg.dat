using System;
using System.Collections.Generic;
using System.Globalization;
using pg.dat.service;
using pg.dat.utility;

namespace pg.dat
{
    public sealed class DatFileUtility
    {
        public static IEnumerable<Translation> Import(string filePath, CultureInfo locale,
            FileType fileType = FileType.SortedDat)
        {
            switch (fileType)
            {
                case FileType.SortedDat:
                    return SortedDatFileService.LoadDatFile(filePath, locale);
                case FileType.UnsortedDat:
                    return UnsortedDatFileService.LoadDatFile(filePath, locale);
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType,
                        "The file type specified is not supported.");
            }
        }

        public static void Export(string filePath, IEnumerable<Translation> translations, FileType fileType = FileType.SortedDat)
        {
            switch (fileType)
            {
                case FileType.SortedDat:
                    SortedDatFileService.StoreDatFile(filePath, translations);
                    break;
                case FileType.UnsortedDat:
                    UnsortedDatFileService.StoreDatFile(filePath, translations);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType, "The file type specified is not supported.");
            }
        }
    }
}
