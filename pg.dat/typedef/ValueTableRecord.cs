using System;
using System.Runtime.CompilerServices;
using System.Text;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.dat.test")]

namespace pg.dat.typedef
{
    internal sealed class ValueTableRecord : IBinaryFile, ISizeable, IComparable
    {
        private string _value;
        internal static readonly Encoding ENCODING = Encoding.Unicode;

        public string Value
        {
            get => _value;
            set => _value = value.Replace("\0", string.Empty);
        }

        public ValueTableRecord(string value)
        {
            Value = value;
        }

        public ValueTableRecord(byte[] bytes, long index, long stringLength)
        {
            char[] chars = ENCODING.GetChars(bytes, Convert.ToInt32(index), Convert.ToInt32(stringLength * 2));
            Value = new string(chars);
        }

        public byte[] GetBytes()
        {
            return ENCODING.GetBytes(Value);
        }

        public uint Size()
        {
            return Convert.ToUInt32(GetBytes().Length);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return !(obj is ValueTableRecord v) ? 0 : Value.CompareTo(v.Value);
        }
    }
}
