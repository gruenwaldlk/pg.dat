using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.typedef
{
    internal sealed class IndexTable : IBinaryFile, ISizeable
    {
        private readonly List<IndexTableRecord> _indexTableRecords;

        internal IndexTable(List<IndexTableRecord> indexTableRecords)
        {
            _indexTableRecords = indexTableRecords;
        }

        public List<IndexTableRecord> IndexTableRecords
        {
            get => _indexTableRecords ?? new List<IndexTableRecord>();
        }

        public byte[] GetBytes()
        {
            List<byte> b = new List<byte>();
            foreach (IndexTableRecord indexTableRecord in IndexTableRecords)
            {
                b.AddRange(indexTableRecord.GetBytes());
            }

            return b.ToArray();
        }

        public uint Size()
        {
            return IndexTableRecords.Aggregate<IndexTableRecord, uint>(0,
                (current, indexTableRecord) => current + indexTableRecord.Size());
        }
    }
}
