using System.Text;

namespace AL.LeagueRoster.Common
{
    public static class EnumList
    {
        public static string ToChoices<T>() where T : Enum
        {
            var items = Enum.GetNames(typeof(T)).ToList();

            var sb = new StringBuilder();

            foreach (var item in items.Take(items.Count - 1))
            {
                if (sb.Length > 0)
                    sb.Append(", ");

                sb.Append(item);
            }

            sb.Append(" or ");

            sb.Append(items.Last());

            return sb.ToString();
        }
    }
}
