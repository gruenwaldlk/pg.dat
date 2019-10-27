using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using pg.util;

namespace pg.dat.utility
{
    public sealed class Translation : INotifyPropertyChanged, IComparable
    {
        private uint _crc32;
        private string _key;
        private string _value;

        public string Key
        {
            get => _key;
            set
            {
                if (_key.Equals(value))
                {
                    return;
                }

                _key = value;
                Crc32 = ChecksumUtility.GetChecksum(_key.Replace("\0", string.Empty));
                OnPropertyChanged(nameof(Key));
            }
        }

        public string Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value))
                {
                    return;
                }

                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public uint Crc32
        {
            get => _crc32;
            private set
            {
                _crc32 = value;
                OnPropertyChanged(nameof(Crc32));
            }
        }

        public CultureInfo Locale { get; }

        public Translation(string key, string value, CultureInfo locale)
        {
            _key = key;
            _value = value;
            _crc32 = ChecksumUtility.GetChecksum(_key.Replace("\0", string.Empty));
            Locale = locale;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return !(obj is Translation t) ? 0 : Crc32.CompareTo(t.Crc32);
        }
    }
}
