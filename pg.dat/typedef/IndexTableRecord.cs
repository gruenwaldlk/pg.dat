using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.typedef
{
    internal sealed class IndexTableRecord : IBinaryFile, ISizeable
    {
        private uint _crc32;
        private uint _keyLength;
        private uint _valueLength;

        internal IndexTableRecord(uint crc32, uint keyLength, uint valueLength)
        {
            Crc32 = crc32;
            KeyLength = keyLength;
            ValueLength = valueLength;
        }

        internal uint Crc32
        {
            get => _crc32;
            set => _crc32 = value;
        }

        internal uint KeyLength
        {
            get => _keyLength;
            set => _keyLength = value;
        }

        internal uint ValueLength
        {
            get => _valueLength;
            set => _valueLength = value;
        }

        public byte[] GetBytes()
        {
            List<byte> b = new List<byte>();
            b.AddRange(BitConverter.GetBytes(Crc32));
            b.AddRange(BitConverter.GetBytes(ValueLength));
            b.AddRange(BitConverter.GetBytes(KeyLength));
            return b.ToArray();
        }

        public uint Size()
        {
            return sizeof(uint) * 3;
        }
    }
}
