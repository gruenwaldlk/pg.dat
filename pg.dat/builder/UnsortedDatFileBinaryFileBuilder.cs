using pg.dat.builder.attributes;
using pg.dat.typedef;

namespace pg.dat.builder
{
    internal sealed class UnsortedDatFileBinaryFileBuilder : ADatFileBinaryFileBuilder
    {
        public override DatFile Build(DatFileAttribute attribute)
        {
            return BuildGenericDatFile(attribute);
        }
    }
}
