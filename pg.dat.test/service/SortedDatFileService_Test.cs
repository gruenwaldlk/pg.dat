using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pg.dat.builder;
using pg.dat.service;
using pg.dat.typedef;
using pg.dat.utility;

namespace pg.texts.test.service
{
    [TestClass]
    public class SortedDatFileService_Test
    {
        private static readonly string TEST_DATA_PATH_IN = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "test_data", "mastertextfile_english.dat"));

        private static readonly string TEST_DATA_PATH_OUT = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "test_data",
                "test_mastertextfile_english.dat"));

        [TestMethod]
        public void LoadDatFile_Test()
        {
            IEnumerable<Translation> translations =
                SortedDatFileService.LoadDatFile(TEST_DATA_PATH_IN, new CultureInfo("en-GB"));
            Assert.IsNotNull(translations);
            Assert.IsTrue(translations.Any());
        }

        [TestMethod]
        public void StoreDatFile_Test()
        {
            List<Translation> translations = new List<Translation>
            {
                new Translation("TEST_KEY_01", "Test value 1.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_02", "Test value 2.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_03", "Test value 3.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_04", "Test value 4.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_05", "Test value 5.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_06", "Test value 6.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_07", "Test value 7.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_08", "Test value 8.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_09", "Test value 9.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_10", "Test value 10.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_11", "Test value 11.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_12", "Test value 12.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_13", "Test value 13.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_14", "Test value 14.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_15", "Test value 15.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_16", "Test value 16.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_17", "Test value 17.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_18", "Test value 18.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_19", "Test value 19.", new CultureInfo("en-GB")),
                new Translation("TEST_KEY_20", "Test value 20.", new CultureInfo("en-GB"))
            };
            SortedDatFileService.StoreDatFile(TEST_DATA_PATH_OUT, translations);
            Assert.IsTrue(File.Exists(TEST_DATA_PATH_OUT));
            byte[] b = File.ReadAllBytes(TEST_DATA_PATH_OUT);
            SortedDatFileBinaryFileBuilder builder = new SortedDatFileBinaryFileBuilder();
            DatFile datFile = builder.Build(b);
            Assert.IsNotNull(datFile);
            Assert.IsTrue(datFile.GetTextItemCount() == translations.Count);
            foreach (Translation translation in translations)
            {
                bool containsKey = datFile.GetKeys().Any(keyTableRecord =>
                    keyTableRecord.CompareTo(new KeyTableRecord(translation.Key)) == 0);
                Assert.IsTrue(containsKey);
                bool containsValue = datFile.GetValues().Any(valueTableRecord =>
                    valueTableRecord.CompareTo(new ValueTableRecord(translation.Value)) == 0);
                Assert.IsTrue(containsValue);
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(TEST_DATA_PATH_OUT))
            {
                File.Delete(TEST_DATA_PATH_OUT);
            }
        }
    }
}
