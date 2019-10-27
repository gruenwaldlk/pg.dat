using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pg.dat;
using pg.dat.builder;
using pg.dat.typedef;
using pg.dat.utility;

namespace pg.texts.test
{
    [TestClass]
    public class DatFileUtility_Test
    {
        private static readonly string TEST_DATA_SORTED_PATH_IN = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\", "test_data\\mastertextfile_english.dat"));

        private static readonly string TEST_DATA_SORTED_PATH_OUT = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\", "test_data\\test_mastertextfile_english.dat"));

        private static readonly string TEST_DATA_UNSORTED_PATH_IN = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\", "test_data\\creditstext_english.dat"));

        private static readonly string TEST_DATA_UNSORTED_PATH_OUT = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\", "test_data\\test_creditstext_english.dat"));

        private static readonly List<Translation> TRANSLATIONS = new List<Translation>
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

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(TEST_DATA_SORTED_PATH_OUT))
            {
                File.Delete(TEST_DATA_SORTED_PATH_OUT);
            }

            if (File.Exists(TEST_DATA_UNSORTED_PATH_OUT))
            {
                File.Delete(TEST_DATA_UNSORTED_PATH_OUT);
            }
        }

        [TestMethod]
        public void Import_TestSorted()
        {
            IEnumerable<Translation> translations = DatFileUtility.Import(TEST_DATA_SORTED_PATH_IN, new CultureInfo("en-GB"));
            Assert.IsNotNull(translations);
            Assert.IsTrue(translations.Any());
        }

        [TestMethod]
        public void Export_TestSorted()
        {
            DatFileUtility.Export(TEST_DATA_SORTED_PATH_OUT, TRANSLATIONS);
            Assert.IsTrue(File.Exists(TEST_DATA_SORTED_PATH_OUT));
            byte[] b = File.ReadAllBytes(TEST_DATA_SORTED_PATH_OUT);
            SortedDatFileBinaryFileBuilder builder = new SortedDatFileBinaryFileBuilder();
            DatFile datFile = builder.Build(b);
            Assert.IsNotNull(datFile);
            Assert.IsTrue(datFile.GetTextItemCount() == TRANSLATIONS.Count);
            foreach (Translation translation in TRANSLATIONS)
            {
                bool containsKey = datFile.GetKeys().Any(keyTableRecord => keyTableRecord.CompareTo(new KeyTableRecord(translation.Key)) == 0);
                Assert.IsTrue(containsKey);
                bool containsValue = datFile.GetValues().Any(valueTableRecord => valueTableRecord.CompareTo(new ValueTableRecord(translation.Value)) == 0);
                Assert.IsTrue(containsValue);
            }
        }

        [TestMethod]
        public void Import_TestUnsorted()
        {
            IEnumerable<Translation> translations = DatFileUtility.Import(TEST_DATA_UNSORTED_PATH_IN, new CultureInfo("en-GB"), FileType.UnsortedCreditsStringFile);
            Assert.IsNotNull(translations);
            Assert.IsTrue(translations.Any());
        }

        [TestMethod]
        public void Export_TestUnsorted()
        {
            DatFileUtility.Export(TEST_DATA_UNSORTED_PATH_OUT, TRANSLATIONS, FileType.UnsortedCreditsStringFile);
            Assert.IsTrue(File.Exists(TEST_DATA_UNSORTED_PATH_OUT));
            byte[] b = File.ReadAllBytes(TEST_DATA_UNSORTED_PATH_OUT);
            UnsortedDatFileBinaryFileBuilder builder = new UnsortedDatFileBinaryFileBuilder();
            DatFile datFile = builder.Build(b);
            Assert.IsNotNull(datFile);
            Assert.IsTrue(datFile.GetTextItemCount() == TRANSLATIONS.Count);
            for (int i = 0; i < TRANSLATIONS.Count; i++)
            {
                Assert.IsTrue(datFile.GetKeys()[i].CompareTo(new KeyTableRecord(TRANSLATIONS[i].Key)) == 0);
                Assert.IsTrue(datFile.GetValues()[i].CompareTo(new ValueTableRecord(TRANSLATIONS[i].Value)) == 0);
            }
        }
    }
}
