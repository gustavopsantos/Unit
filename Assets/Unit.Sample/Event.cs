using System;

namespace Unit.Sample
{
    [Serializable]
    public class Event
    {
        public string Name;
        public UDateTime StartsAt;
        public UDateTime FinishesAt;
    }
}