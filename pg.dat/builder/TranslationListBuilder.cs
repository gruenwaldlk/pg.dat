using System.Collections.Generic;
using System.Globalization;
using pg.dat.typedef;
using pg.dat.utility;

namespace pg.dat.builder
{
    internal sealed class TranslationListBuilder : ATranslationListBuilder
    {
        public TranslationListBuilder(CultureInfo locale) : base(locale)
        {
        }

        public override List<Translation> Build(byte[] bytes)
        {
            SortedDatFileBinaryFileBuilder builder = new SortedDatFileBinaryFileBuilder();
            DatFile datFile = builder.Build(bytes);
            return Build(datFile);
        }
    }
}
