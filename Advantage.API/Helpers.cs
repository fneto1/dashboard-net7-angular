namespace Advantage.API
{
    public class Helpers
    {

        private static Random _rand = new Random();

        private static string GetRandom(IList<string> items)
        {

            return items[_rand.Next(items.Count)];
        }
        internal static string MakeUniqueCustomerName(List<string> names)
        {
            var maxNames = bizPrefix.Count * bizSufix.Count;
            if (names.Count >= maxNames)
            {
                throw new System.InvalidOperationException("Atingiu o número máximo de empresas");
            }
            var prefix = GetRandom(bizPrefix);
            var sufix = GetRandom(bizSufix);

            var bizName = prefix + sufix;

            if (names.Contains(bizName))
            {
                MakeUniqueCustomerName(names);
            }
            return bizName;
        }

        internal static string MakeCustomerEmail(string costumerName)
        {
            return $"contact@{costumerName.ToLower()}.com";
        }

        internal static string GetRandomState()
        {
            return GetRandom(brStates);
        }

        internal static decimal GetRandomOrderTotal()
        {
            return _rand.Next(100, 5000);
        }

        internal static DateTimeOffset GetRandomOrderPlaced()
        {
            var end = DateTimeOffset.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            return start + newSpan;
        }

        internal static DateTimeOffset? GetRandomOrderCompleted(DateTimeOffset orderPlaced)
        {
            var now = DateTimeOffset.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced;

            if (timePassed < minLeadTime)
            {
                return null;
            }

            return orderPlaced.AddDays(_rand.Next(7, 14)).ToUniversalTime();
        }

        private static readonly List<string> brStates = new List<string>(){
            "BA",
            "SE",
            "PE",
            "RN",
            "AL",
            "CE",
            "MA",
            "PI",
            "PB",
            "AP",
            "PA",
            "RO",
            "RR",
            "AM",
            "AC",
            "MT",
            "MS",
            "GO",
            "DF",
            "TO",
            "SP",
            "MG",
            "RJ",
            "ES",
            "PR",
            "SC",
            "RS"
        };

        private static readonly List<string> bizPrefix = new List<string>(){
            "ABC",
            "XYZ",
            "MainSt",
            "Sales",
            "Enterprise",
            "Ready",
            "Quick",
            "Budget",
            "Peak",
            "Magic",
            "Family",
            "Comfort"
        };

        private static readonly List<string> bizSufix = new List<string>(){
            "Corporation",
            "Co",
            "Logistics",
            "Transit",
            "Bakery",
            "Goods",
            "Foods",
            "Cleaners",
            "Hotels",
            "Planners",
            "Automotive",
            "Books"
        };
    }
}