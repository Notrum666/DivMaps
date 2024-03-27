using System.Diagnostics;

using DivMaps.Models;

using Microsoft.AspNetCore.Mvc;

namespace DivMaps.Controllers
{
    [Route("/api")]
    public class ApiController : Controller
    {
        private readonly DataContainer dataContainer;

        public ApiController(DataContainer dataContainer)
        {
            this.dataContainer = dataContainer;
        }

        [HttpPost("maps")]
        public IActionResult GetMaps([FromForm]DivMapsRequestModel data)
        {
            double minValue = data?.MinValue ?? 0;
            bool scarab = data?.Scarab ?? false;
            int favSlots = Math.Clamp(data?.FavSlots ?? 12, 1, 12);

            int topSetsToStore = 100;

            bool allowRepetition = true;
            int repetitionOffset = allowRepetition ? 0 : 1;

            List<Map> maps = dataContainer.Maps;
            if (maps is null)
                return PartialView(new ResponseViewModel(new List<MapSetViewModel>(), scarab, minValue));

            List<MapSet> curSets = new List<MapSet>();
            for (int i = 0; i <= maps.Count - favSlots; i++)
                curSets.Add(new MapSet(new List<Map>() { maps[i] }, i + repetitionOffset, minValue, scarab));

            if (favSlots == 1)
                curSets = curSets.OrderByDescending(set => set.ExpectedValue).ToList();

            for (int i = 2; i <= favSlots; i++)
            {
                List<MapSet> nextSets = new List<MapSet>();
                foreach (MapSet set in curSets)
                    for (int j = set.RemainingMapsStart; j < maps.Count - favSlots + i; j++)
                        nextSets.Add(new MapSet(set.Maps.Concat(new List<Map>() { maps[j] }).ToList(), j + repetitionOffset, minValue, scarab));
                curSets = nextSets.OrderByDescending(set => set.ExpectedValue).Take(topSetsToStore).ToList();
            }
            return PartialView(new ResponseViewModel(curSets.Take(5)
                .Select(set => new MapSetViewModel(set.Maps.Select(map => map.Name).ToList(), set.ExpectedValue, set.Pool.ToList())).ToList(), scarab, minValue));
        }

        private class MapSet
        {
            public List<Map> Maps { get; }
            public int RemainingMapsStart { get; }
            private List<DivCardViewModel> pool;
            public IReadOnlyList<DivCardViewModel> Pool => pool;
            public double ExpectedValue { get; }
            public MapSet(List<Map> maps, int remainingMapsStart, double minValue, bool scarab)
            {
                Maps = maps;
                RemainingMapsStart = remainingMapsStart;

                IEnumerable<DivCard> cards = Maps.SelectMany(map => map.DivCards).Distinct();
                double totalWeight = cards.Sum(card => card.DropWeight);
                pool = cards.Select(card => new DivCardViewModel(card.Name, card.ChaosValue, card.StackSize, card.DropWeight,
                    card.ChaosValue >= minValue ? card.ChaosValue * card.DropWeight / totalWeight * (scarab ? (4.0 + card.StackSize) / 5.0 : 1.0) : 0.0))
                    .OrderByDescending(card => card.Value).ToList();
                ExpectedValue = pool.Sum(card => card.Value) * (1.0 + (scarab ? maps.Distinct().Count() / 10.0 : 0.0));
            }
        }
    }
}
