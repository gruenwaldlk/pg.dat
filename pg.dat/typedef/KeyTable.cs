using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.typedef
{
    internal sealed class KeyTable : IBinaryFile, ISizeable
    {
        private List<KeyTableRecord> _keyTableRecords;

        public KeyTable(List<KeyTableRecord> keyTableRecords)
        {
            _keyTableRecords = keyTableRecords;
        }

        internal List<KeyTableRecord> KeyTableRecords => _keyTableRecords;

        public byte[] GetBytes()
        {
            List<byte> b = new List<byte>();
            foreach (KeyTableRecord keyTableRecord in _keyTableRecords)
            {
                b.AddRange(keyTableRecord.GetBytes());
            }

            return b.ToArray();
        }

        public uint Size()
        {
            return _keyTableRecords.Aggregate<KeyTableRecord, uint>(0,
                (current, keyTableRecord) => current + keyTableRecord.Size());
        }
    }
}
