using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using pg.dat.utility;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.builder.attributes
{
    internal sealed class DatFileAttribute : IBuilderAttribute
    {
        private List<Translation> _translations;

        public List<Translation> Translations
        {
            get => _translations ?? new List<Translation>();
            set => _translations = value;
        }
    }
}
