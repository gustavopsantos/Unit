using System.Collections.Generic;
using UnityEngine;

namespace Unit.Sample
{
    public class UnitSample : MonoBehaviour
    {
        [Header("Non-Nested")]
        [SerializeField] private UDateTime _dateTime;
        [SerializeField] private UDateTime[] _dateTimeArray;
        [SerializeField] private List<UDateTime> _dateTimeList;
        
        [Header("Nested")]
        [SerializeField] private Event _event;
        [SerializeField] private Event[] _eventArray;
        [SerializeField] private List<Event> _eventList;
    }
}