using System;

namespace Unit
{
    [Serializable]
    public struct UDateTime
    {
        public int Year;
        public int Month;
        public int Day;
        public int Hour;
        public int Minute;
        public int Second;
        public DateTimeKind Kind;
    }
}