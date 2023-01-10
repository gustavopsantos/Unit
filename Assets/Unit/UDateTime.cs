using System;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public struct UDateTime : ISerializationCallbackReceiver
    {
        private DateTime _dateTime;
        [SerializeField] private string _serialized; // ISO 8601 format

        public UDateTime(DateTime dateTime)
        {
            _dateTime = dateTime;
            _serialized = _dateTime.ToString("O");
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
            _serialized = _dateTime.ToString("O");
        }

        public void OnAfterDeserialize()
        {
            _dateTime = DateTime.Parse(_serialized, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }
    }
}