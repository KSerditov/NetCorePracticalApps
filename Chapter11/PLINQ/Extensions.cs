namespace PLINQ
{
    public static class Extensions
    {
        static IEnumerable<int> FilterMoreFour(this IEnumerable<int> e)
        {
            return e.Where(e => e > 4);
        }

    }
}