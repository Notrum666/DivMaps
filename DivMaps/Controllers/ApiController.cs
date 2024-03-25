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
            bool droprates = data?.DropRates ?? false;
            List<MapViewModel> maps = new List<MapViewModel>();
            int maxCards = 0;
            foreach (Map map in dataContainer.Maps)
            {
                MapViewModel mapVM = new MapViewModel();
                mapVM.Name = map.Name;
                mapVM.ValueSources = map.DivCards.Where(card => card.ChaosValue >= minValue)
                    .Select(card => new DivCardViewModel(card.Name, 
                    card.ChaosValue * (droprates ? card.DropRate : 1.0) * (scarab ? (4.0 + card.StackSize) / 5.0 : 1.0),
                    card.ChaosValue, (4.0 + card.StackSize) / 5.0, card.DropRate))
                    .Where(card => card.Value > 0)
                    .OrderByDescending(card => card.Value).ToList();
                maxCards = Math.Max(maxCards, mapVM.ValueSources.Count);
                mapVM.Value = mapVM.ValueSources.Sum(card => card.Value);
                if (mapVM.Value < minValue)
                    continue;
                maps.Add(mapVM);
            }
            return PartialView(new ResponseViewModel(maps.OrderByDescending(map => map.Value).ToList(), maxCards, scarab, droprates));
        }
    }
}
