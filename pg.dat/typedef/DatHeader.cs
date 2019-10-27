using System;
using System.Runtime.CompilerServices;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.typedef
{
    internal sealed class DatHeader : IBinaryFile, ISizeable
    {
        private uint _keyCount;

        internal DatHeader(uint keyCount)
        {
            KeyCount = keyCount;
        }

        internal uint KeyCount
        {
            get => _keyCount;
            set => _keyCount = value;
        }

        public byte[] GetBytes()
        {
            byte[] bytes = BitConverter.GetBytes(KeyCount);
            return bytes;
        }

        public uint Size()
        {
            return sizeof(uint);
        }
    }
}
