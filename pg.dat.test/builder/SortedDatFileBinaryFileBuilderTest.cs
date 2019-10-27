using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pg.dat.builder;
using pg.dat.builder.attributes;
using pg.dat.typedef;
using pg.dat.utility;

namespace pg.texts.test.builder
{
    [TestClass]
    public class SortedDatFileBinaryFileBuilderTest
    {
        private static readonly string TEST_DATA_PATH_IN =
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\",
                "test_data\\mastertextfile_english.dat"));

        [TestMethod]
        public void BuildOrderedDatFileFromBinary_Test()
        {
            byte[] b = File.ReadAllBytes(TEST_DATA_PATH_IN);
            SortedDatFileBinaryFileBuilder builder = new SortedDatFileBinaryFileBuilder();
            DatFile datFile = builder.Build(b);
            Assert.IsNotNull(datFile);
        }

        [TestMethod]
        public void BuildOrderedDatFileFromTranslationList_Test()
        {
            byte[] b = File.ReadAllBytes(TEST_DATA_PATH_IN);
            TranslationListBuilder translationListBuilder = new TranslationListBuilder(new CultureInfo("en-GB"));
            List<Translation> translations = translationListBuilder.Build(b);
            SortedDatFileBinaryFileBuilder sortedDatFileBinaryFileBuilder = new SortedDatFileBinaryFileBuilder();
            DatFile datFile = sortedDatFileBinaryFileBuilder.Build(new DatFileAttribute()
            {
                Translations = translations
            });
            Assert.IsNotNull(datFile);
        }
    }
}
