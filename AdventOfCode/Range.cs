using System.Numerics;

namespace AdventOfCode
{
    public readonly record struct Range<T>(T Start, T End)
        where T : struct, INumber<T>
    {
        public static Range<T> operator +(Range<T> a, T offset) => new(a.Start + offset, a.End + offset);
        public static Range<T> operator -(Range<T> a, T offset) => new(a.Start - offset, a.End - offset);
        /// <summary>Includes Start & End</summary>
        public T Length => T.One + End - Start;
        /*  this          ╠═════╣
         *        |---|                     is after other
         *            |-------|             start overlaps with other
         *                |---|             start overlaps with other
         *  this          ╠═════╣
         *                  |-|             fully overlaps with other
         *                |-----|           fully overlaps with other / fully enclosed by other
         *              |---------|         fully enclosed by other
         *  this          ╠═════╣
         *                  |---|           end overlaps with other
         *                  |-------|       end overlaps with other
         *                          |---|   is before other
         *  this          ╠═════╣           */
        public bool IsAfter(Range<T> other) => Start > other.End;
        public bool IsBefore(Range<T> other) => End < other.Start;
        public bool HasAnyOverlapWith(Range<T> other) => Start <= other.End && End >= other.Start;
        public bool StartOverlapsWith(Range<T> other) => Start >= other.Start && Start <= other.End;
        public bool EndOverlapsWith(Range<T> other) => End >= other.Start && End <= other.End;
        public bool FullyOverlapsWith(Range<T> other) => Start <= other.Start && End >= other.End;
        public bool IsFullyEnclosedBy(Range<T> other) => Start >= other.Start && End <= other.End;
        public override string ToString() => $"{Start}-{End}";
    }
}
