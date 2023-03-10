using System;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public struct UDateTime : ISerializationCallbackReceiver
    {
        public enum Precision
        {
            Year,
            Month,
            Day,
            Hour,
            Minute,
            Second,
            Millisecond
        }
        
        [AttributeUsage(AttributeTargets.Field)]
        public class PrecisionAttribute : Attribute
        {
            public Precision Precision { get; }

            public PrecisionAttribute(Precision precision)
            {
                Precision = precision;
            }
        }
        
        private DateTime _dateTime;
        [SerializeField] private string _dateTimeString; // ISO 8601 format

        public UDateTime(DateTime dateTime)
        {
            _dateTime = dateTime;
            _dateTimeString = _dateTime.ToString("O");
        }

        public static implicit operator DateTime(UDateTime uDateTime)
        {
            return uDateTime._dateTime;
        }

        public static implicit operator UDateTime(DateTime dateTime)
        {
            return new UDateTime(dateTime);
        }

        public void OnBeforeSerialize()
        {
            _dateTimeString = _dateTime.ToString("O");
        }

        public void OnAfterDeserialize()
        {
            _dateTime = DateTime.Parse(_dateTimeString, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }
    }
}