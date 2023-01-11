using UnityEngine;

namespace Unit.Sample
{
    public class UnitSample : MonoBehaviour
    {
        [field: SerializeField, UDateTime.Precision(UDateTime.Precision.Hour)] public UDateTime DateTime { get; private set; }
        
        // [SerializeField] private UDateTime[] _dateTimeArray;
        // [SerializeField] private List<UDateTime> _dateTimeList;

        // [Header("Nested")]
        // [SerializeField] private Event _event;
        // [SerializeField] private Event[] _eventArray;
        // [SerializeField] private List<Event> _eventList;
    }
}