using System;
using System.Collections.Generic;
using System.Globalization;
using pg.dat.service;
using pg.dat.utility;

namespace pg.dat
{
    /// <summary>
    /// A utility class to interact with Petroglyph String Files.
    /// <br/>
    /// String Files (extension: .DAT) in Petroglyph's games hold &lt;key, value&gt; string pairs and allow the game
    /// to be easily ported across different languages by having a separate String File for each language.
    /// In such a case, the sets of keys are typically identical for the different language string files while the
    /// sets of values differ for each language.
    /// </summary>
    public sealed class DatFileUtility
    {
        /// <summary>
        /// Imports a DAT file from a specified location.
        /// </summary>
        /// <param name="filePath">The path to the file to import.</param>
        /// <param name="locale">The locale of the file.</param>
        /// <param name="fileType">Type of dat file to import.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<Translation> Import(string filePath, CultureInfo locale,
            FileType fileType = FileType.SortedGameStringFile)
        {
            switch (fileType)
            {
                case FileType.SortedGameStringFile:
                    return SortedDatFileService.LoadDatFile(filePath, locale);
                case FileType.UnsortedCreditsStringFile:
                    return UnsortedDatFileService.LoadDatFile(filePath, locale);
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType,
                        "The file type specified is not supported.");
            }
        }

        /// <summary>
        /// Exports a list of translations to a given dat file.
        /// </summary>
        /// <param name="filePath">The file to export to. Must not already exist.</param>
        /// <param name="translations">A list of translations to write to the dat file.</param>
        /// <param name="fileType">Type of dat file to export.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Export(string filePath, IEnumerable<Translation> translations,
            FileType fileType = FileType.SortedGameStringFile)
        {
            switch (fileType)
            {
                case FileType.SortedGameStringFile:
                    SortedDatFileService.StoreDatFile(filePath, translations);
                    break;
                case FileType.UnsortedCreditsStringFile:
                    UnsortedDatFileService.StoreDatFile(filePath, translations);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType,
                        "The file type specified is not supported.");
            }
        }
    }
}
