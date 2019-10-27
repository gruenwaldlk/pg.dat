using System;
using System.Runtime.CompilerServices;
using System.Text;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.typedef
{
    internal sealed class KeyTableRecord : IBinaryFile, ISizeable
    {
        private string _key;
        internal readonly Encoding ENCODING = Encoding.ASCII;

        public string Key
        {
            get => _key;
            set => _key = value.Replace("\0", string.Empty);
        }

        public KeyTableRecord(string key)
        {
            Key = key.Replace("\0", "");
        }

        public KeyTableRecord(byte[] bytes, long index, long stringLength)
        {
            char[] chars = ENCODING.GetChars(bytes, Convert.ToInt32(index), Convert.ToInt32(stringLength));
            Key = new string(chars);
        }

        public byte[] GetBytes()
        {
            return ENCODING.GetBytes(Key);
        }

        public uint Size()
        {
            return Convert.ToUInt32(GetBytes().Length);
        }
    }
}
