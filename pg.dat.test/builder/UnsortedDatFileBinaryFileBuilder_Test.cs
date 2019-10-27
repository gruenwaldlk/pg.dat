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
    public class UnsortedDatFileBinaryFileBuilder_Test
    {
        private static readonly string TEST_DATA_PATH_IN =
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\",
                "test_data\\creditstext_english.dat"));

        [TestMethod]
        public void BuildUnsortedDatFile_Test()
        {
            byte[] b = File.ReadAllBytes(TEST_DATA_PATH_IN);
            UnsortedDatFileBinaryFileBuilder builder = new UnsortedDatFileBinaryFileBuilder();
            DatFile datFile = builder.Build(b);
            Assert.IsNotNull(datFile);
        }

        [TestMethod]
        public void BuildUnsortedDatFileFromTranslationList_Test()
        {
            byte[] b = File.ReadAllBytes(TEST_DATA_PATH_IN);
            CreditsTranslationListBuilder translationListBuilder = new CreditsTranslationListBuilder(new CultureInfo("en-GB"));
            List<Translation> translations = translationListBuilder.Build(b);
            UnsortedDatFileBinaryFileBuilder unsortedDatFileBinaryFileBuilder = new UnsortedDatFileBinaryFileBuilder();
            DatFile datFile = unsortedDatFileBinaryFileBuilder.Build(new DatFileAttribute()
            {
                Translations = translations
            });
            Assert.IsNotNull(datFile);
        }
    }
}
