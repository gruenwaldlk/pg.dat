using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.typedef
{
    internal sealed class DatFile : IBinaryFile
    {
        private readonly DatHeader _header;
        private readonly IndexTable _indexTable;
        private readonly ValueTable _valueTable;
        private readonly KeyTable _keyTable;

        internal DatFile(DatHeader header, IndexTable indexTable, ValueTable valueTable, KeyTable keyTable)
        {
            _header = header;
            _indexTable = indexTable;
            _valueTable = valueTable;
            _keyTable = keyTable;
        }


        internal int GetTextItemCount()
        {
            return Convert.ToInt32(_header.KeyCount);
        }

        internal List<ValueTableRecord> GetValues()
        {
            return _valueTable.ValueTableRecords;
        }

        internal List<KeyTableRecord> GetKeys()
        {
            return _keyTable.KeyTableRecords;
        }

        public byte[] GetBytes()
        {
            List<byte> b = new List<byte>();
            b.AddRange(_header.GetBytes());
            b.AddRange(_indexTable.GetBytes());
            b.AddRange(_valueTable.GetBytes());
            b.AddRange(_keyTable.GetBytes());
            return b.ToArray();
        }
    }
}
