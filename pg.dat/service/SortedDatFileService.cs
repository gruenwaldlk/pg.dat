using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using pg.dat.builder;
using pg.dat.builder.attributes;
using pg.dat.typedef;
using pg.dat.utility;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.service
{
    internal sealed class SortedDatFileService
    {
        internal static IEnumerable<Translation> LoadDatFile(string filePath, CultureInfo locale)
        {
            if (locale == null)
            {
                throw new ArgumentNullException($"The argument {nameof(locale)} must not be null.");
            }

            if (filePath == null)
            {
                throw new ArgumentNullException($"The argument {nameof(filePath)} must not be null.");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} does not exist.");
            }

            byte[] bytes = File.ReadAllBytes(filePath);
            TranslationListBuilder builder = new TranslationListBuilder(locale);
            return builder.Build(bytes);
        }

        internal static void StoreDatFile(string filePath, IEnumerable<Translation> translations)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException($"The argument {nameof(filePath)} must not be null.");
            }

            if (translations == null)
            {
                throw new ArgumentNullException($"The argument {nameof(translations)} must not be null.");
            }

            List<Translation> enumerable = translations.ToList();
            if (!enumerable.Any())
            {
                throw new ArgumentException($"{nameof(translations)} must not be empty.");
            }

            SortedDatFileBinaryFileBuilder builder = new SortedDatFileBinaryFileBuilder();
            DatFile datFile = builder.Build(new DatFileAttribute() {Translations = enumerable});

            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.CreateNew)))
            {
                writer.Write(datFile.GetBytes());
            }
        }
    }
}
