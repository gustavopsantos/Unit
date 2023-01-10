using System;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public struct UDateTime : ISerializationCallbackReceiver
    {
        public int Year;
        public int Month;
        public int Day;
        public int Hour;
        public int Minute;
        public int Second;
        public int Millisecond;
        public long Ticks;
        public DateTimeKind Kind;

        private DateTime _value;
        private string _serialized; // ISO 8601 format
        
        public static implicit operator DateTime(UDateTime uDateTime)
        {
            return uDateTime._value;
        }
        
        public void OnBeforeSerialize()
        {
            _value = new DateTime(Year, Month, Day, Hour, Minute, Second, Millisecond, Kind);
            _serialized = _value.ToString("O");
        }

        public void OnAfterDeserialize()
        {
            _value = DateTime.Parse(_serialized, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }
    }
}