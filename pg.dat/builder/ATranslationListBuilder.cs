using System.Collections.Generic;
using System.Globalization;
using pg.dat.typedef;
using pg.dat.utility;
using pg.util.interfaces;

namespace pg.dat.builder
{
    internal abstract class ATranslationListBuilder : IBinaryFileBuilder<List<Translation>, DatFile>
    {
        protected ATranslationListBuilder(CultureInfo locale)
        {
            Locale = locale;
        }

        internal CultureInfo Locale { get; }
        public abstract List<Translation> Build(byte[] bytes);

        public List<Translation> Build(DatFile attribute)
        {
            List<Translation> translations = new List<Translation>();
            for (int i = 0; i < attribute.GetTextItemCount(); i++)
            {
                string key = attribute.GetKeys()[i].Key;
                string value = attribute.GetValues()[i].Value;
                Translation translation = new Translation(key, value, Locale);
                translations.Add(translation);
            }

            return translations;
        }
    }
}
