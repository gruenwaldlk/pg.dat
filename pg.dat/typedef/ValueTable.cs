using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.typedef
{
    internal sealed class ValueTable : IBinaryFile, ISizeable
    {
        private List<ValueTableRecord> _valueTableRecords;

        public ValueTable(List<ValueTableRecord> valueTableRecords)
        {
            _valueTableRecords = valueTableRecords;
        }

        internal List<ValueTableRecord> ValueTableRecords => _valueTableRecords;

        public byte[] GetBytes()
        {
            List<byte> b = new List<byte>();
            foreach (ValueTableRecord keyTableRecord in _valueTableRecords)
            {
                b.AddRange(keyTableRecord.GetBytes());
            }

            return b.ToArray();
        }

        public uint Size()
        {
            return _valueTableRecords.Aggregate<ValueTableRecord, uint>(0,
                (current, valueTableRecord) => current + valueTableRecord.Size());
        }
    }
}
