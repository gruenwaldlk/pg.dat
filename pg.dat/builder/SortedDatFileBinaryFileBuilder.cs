using pg.dat.builder.attributes;
using pg.dat.typedef;

namespace pg.dat.builder
{
    internal sealed class SortedDatFileBinaryFileBuilder : ADatFileBinaryFileBuilder
    {
        public override DatFile Build(DatFileAttribute attribute)
        {
            attribute.Translations.Sort();
            return BuildGenericDatFile(attribute);
        }
    }
}
