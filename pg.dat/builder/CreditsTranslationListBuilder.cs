using System.Collections.Generic;
using System.Globalization;
using pg.dat.typedef;
using pg.dat.utility;

namespace pg.dat.builder
{
    internal class CreditsTranslationListBuilder : ATranslationListBuilder
    {
        public CreditsTranslationListBuilder(CultureInfo locale) : base(locale)
        {
        }

        public override List<Translation> Build(byte[] bytes)
        {
            UnsortedDatFileBinaryFileBuilder builder = new UnsortedDatFileBinaryFileBuilder();
            DatFile datFile = builder.Build(bytes);
            return Build(datFile);
        }
    }
}
