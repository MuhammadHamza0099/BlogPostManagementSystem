using Sqids;

namespace BPMS.API.Extensions
{
    public static class SqidsExtensions
    {
        private static SqidsEncoder<int> _sqids;

        public static void Configure(SqidsEncoder<int> sqids)
        {
            _sqids = sqids;
        }

        public static string ToSqid(this int number)
        {
            return _sqids.Encode(number);
        }

        public static int FromSqid(this string encoded)
        {
            return _sqids.Decode(encoded).Single();
        }
    }
}
