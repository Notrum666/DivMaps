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
            List<MapViewModel> maps = new List<MapViewModel>();
            int maxCards = 0;
            foreach (Map map in dataContainer.Maps)
            {
                MapViewModel mapVM = new MapViewModel();
                mapVM.Name = map.Name;
                mapVM.ValueSources = map.DivCards.Where(card => card.ChaosValue >= minValue)
                    .Select(card => new DivCardViewModel(card.Name, (int)Math.Round(card.ChaosValue * (scarab ? (4.0 + card.StackSize) / 5.0 : 1.0))))
                    .OrderByDescending(card => card.ChaosValue).ToList();
                maxCards = Math.Max(maxCards, mapVM.ValueSources.Count);
                mapVM.Value = mapVM.ValueSources.Sum(card => card.ChaosValue);
                if (mapVM.Value < minValue)
                    continue;
                maps.Add(mapVM);
            }
            return PartialView(new ResponseViewModel(maps.OrderByDescending(map => map.Value).ToList(), maxCards));
        }
    }
}
