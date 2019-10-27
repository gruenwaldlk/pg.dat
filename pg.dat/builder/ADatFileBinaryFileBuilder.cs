using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using pg.dat.builder.attributes;
using pg.dat.typedef;
using pg.dat.utility;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.builder
{
    internal abstract class ADatFileBinaryFileBuilder : IBinaryFileBuilder<DatFile, DatFileAttribute>
    {
        private int _currentOffset = INDEX_TABLE_STARTING_OFFSET;
        private const int HEADER_STARTING_OFFSET = 0;
        private const int INDEX_TABLE_STARTING_OFFSET = 4;

        public DatFile Build(byte[] bytes)
        {
            DatHeader header = BuildDatHeader(bytes);
            IndexTable indexTable = BuildIndexTable(bytes, header.KeyCount);
            ValueTable valueTable = BuildValueTable(bytes, indexTable, header.KeyCount);
            KeyTable keyTable = BuildKeyTable(bytes, indexTable, header.KeyCount);
            return new DatFile(header, indexTable, valueTable, keyTable);
        }

        private DatHeader BuildDatHeader(byte[] bytes)
        {
            return new DatHeader(BitConverter.ToUInt32(bytes, HEADER_STARTING_OFFSET));
        }

        private IndexTable BuildIndexTable(byte[] bytes, uint keyCount)
        {
            List<IndexTableRecord> indexTable = new List<IndexTableRecord>();
            for (int i = 0; i < keyCount; i++)
            {
                uint crc32 = BitConverter.ToUInt32(bytes, _currentOffset);
                _currentOffset += sizeof(uint);
                uint keyLength = BitConverter.ToUInt32(bytes, _currentOffset);
                _currentOffset += sizeof(uint);
                uint valueLength = BitConverter.ToUInt32(bytes, _currentOffset);
                _currentOffset += sizeof(uint);
                IndexTableRecord indexTableRecord = new IndexTableRecord(crc32, valueLength, keyLength);
                indexTable.Add(indexTableRecord);
            }

            return new IndexTable(indexTable);
        }

        private ValueTable BuildValueTable(byte[] bytes, IndexTable indexTable, uint keyCount)
        {
            List<ValueTableRecord> valueTableRecords = new List<ValueTableRecord>();

            for (int i = 0; i < keyCount; i++)
            {
                long valueLength = indexTable.IndexTableRecords[i].ValueLength;
                ValueTableRecord valueTableRecord = new ValueTableRecord(bytes, _currentOffset, valueLength);
                _currentOffset += Convert.ToInt32(valueTableRecord.Size());
                valueTableRecords.Add(valueTableRecord);
            }

            return new ValueTable(valueTableRecords);
        }

        private KeyTable BuildKeyTable(byte[] bytes, IndexTable indexTable, uint keyCount)
        {
            List<KeyTableRecord> keyTableRecords = new List<KeyTableRecord>();

            for (int i = 0; i < keyCount; i++)
            {
                long keyLength = indexTable.IndexTableRecords[i].KeyLength;
                KeyTableRecord keyTableRecord = new KeyTableRecord(bytes, _currentOffset, keyLength);
                _currentOffset += Convert.ToInt32(keyTableRecord.Size());
                keyTableRecords.Add(keyTableRecord);
            }

            return new KeyTable(keyTableRecords);
        }

        protected DatFile BuildGenericDatFile(DatFileAttribute attribute)
        {
            DatHeader header = new DatHeader(Convert.ToUInt32(attribute.Translations.Count));
            List<IndexTableRecord> indexTableRecords = new List<IndexTableRecord>();
            List<ValueTableRecord> valueTableRecords = new List<ValueTableRecord>();
            List<KeyTableRecord> keyTableRecords = new List<KeyTableRecord>();
            foreach (Translation translation in attribute.Translations)
            {
                ValueTableRecord valueTableRecord = new ValueTableRecord(translation.Value);
                valueTableRecords.Add(valueTableRecord);
                KeyTableRecord keyTableRecord = new KeyTableRecord(translation.Key);
                keyTableRecords.Add(keyTableRecord);
                IndexTableRecord indexTableRecord = new IndexTableRecord(
                    translation.Crc32,
                    Convert.ToUInt32(keyTableRecord.Key.Length),
                    Convert.ToUInt32(valueTableRecord.Value.Length));
                indexTableRecords.Add(indexTableRecord);
            }

            IndexTable indexTable = new IndexTable(indexTableRecords);
            ValueTable valueTable = new ValueTable(valueTableRecords);
            KeyTable keyTable = new KeyTable(keyTableRecords);
            return new DatFile(header, indexTable, valueTable, keyTable);
        }

        public abstract DatFile Build(DatFileAttribute attribute);
    }
}
