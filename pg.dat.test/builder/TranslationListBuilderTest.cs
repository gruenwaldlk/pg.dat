using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pg.dat.builder;
using pg.dat.typedef;
using pg.dat.utility;

namespace pg.texts.test.builder
{
    [TestClass]
    public class TranslationListBuilderTest
    {
        private static readonly string TEST_DATA_PATH_IN =
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\", "test_data\\mastertextfile_english.dat"));

        [TestMethod]
        public void BuildTranslationTableFromOrderedDatFileBinary_Test()
        {
            byte[] b = File.ReadAllBytes(TEST_DATA_PATH_IN);
            TranslationListBuilder builder = new TranslationListBuilder(new CultureInfo("en-GB"));
            List<Translation> translationList = builder.Build(b);
            Assert.IsNotNull(translationList);
            Assert.IsTrue(translationList.Any());
        }

        [TestMethod]
        public void BuildTranslationTableFromOrderedDatFileDat_Test()
        {
            byte[] b = File.ReadAllBytes(TEST_DATA_PATH_IN);
            SortedDatFileBinaryFileBuilder sortedDatBuilder = new SortedDatFileBinaryFileBuilder();
            DatFile datFile = sortedDatBuilder.Build(b);
            TranslationListBuilder builder = new TranslationListBuilder(new CultureInfo("en-GB"));
            List<Translation> translationList = builder.Build(datFile);
            Assert.IsNotNull(translationList);
            Assert.IsTrue(translationList.Any());
        }
    }
}
