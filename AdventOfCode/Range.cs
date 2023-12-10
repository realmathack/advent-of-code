using System.Numerics;

namespace AdventOfCode
{
    public readonly record struct Range<T>(T Start, T End)
        where T : struct, IComparisonOperators<T, T, bool>, IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>
    {
        public static Range<T> operator +(Range<T> a, T offset) => new(a.Start + offset, a.End + offset);
        public static Range<T> operator -(Range<T> a, T offset) => new(a.Start - offset, a.End - offset);
        /*  this          ╠═════╣
         *        |---|                     is after
         *            |-------|             start overlaps
         *                |---|             start overlaps
         *  this          ╠═════╣
         *                  |-|             fully overlaps
         *                |-----|           fully overlaps / fully encloses
         *              |---------|         fully encloses
         *  this          ╠═════╣
         *                  |---|           end overlaps
         *                  |-------|       end overlaps
         *                          |---|   is before
         *  this          ╠═════╣           */
        public bool IsAfter(Range<T> other) => Start > other.End;
        public bool IsBefore(Range<T> other) => End < other.Start;
        public bool AnyOverlap(Range<T> other) => Start <= other.End && End >= other.Start;
        public bool StartOverlaps(Range<T> other) => Start >= other.Start && Start <= other.End;
        public bool EndOverlaps(Range<T> other) => End >= other.Start && End <= other.End;
        public bool FullyOverlaps(Range<T> other) => Start <= other.Start && End >= other.End;
        public bool FullyEncloses(Range<T> other) => Start >= other.Start && End <= other.End;
        public override string ToString() => $"{Start}-{End}";
    }
}
