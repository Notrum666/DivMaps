﻿using DivMaps.Models;

using Newtonsoft.Json;

using RestSharp;

namespace DivMaps.Services
{
    public class UpdateBackgroundService : IHostedService, IDisposable
    {
        private Timer? _timer = null;

        private readonly DataContainer dataContainer;

        public UpdateBackgroundService(DataContainer dataContainer)
        {
            this.dataContainer = dataContainer;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, Timeout.InfiniteTimeSpan);

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            try
            {
                Dictionary<string, List<string>>? maps = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText("./divcards.json"));
                if (maps is null)
                    return;

                List<string> mapNames = maps.Select(map => map.Key).ToList();
                HashSet<HashSet<string>> sets = new HashSet<HashSet<string>>();
                

                Dictionary<string, double>? droprates = JsonConvert.DeserializeObject<Dictionary<string, double>>(File.ReadAllText("./droprates.json"));
                if (droprates is null)
                    return;
                PoeNinjaResponseModel? response = new RestClient("https://poe.ninja/api/data").Get<PoeNinjaResponseModel>(new RestRequest("itemoverview?league=Affliction&type=DivinationCard"));
                if (response is null)
                    return;
                DateTime now = DateTime.UtcNow;
                dataContainer.LastUpdate = now;
                _timer?.Change(new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0).AddHours(1) - now, Timeout.InfiniteTimeSpan);
                response.Lines.ForEach(card => card.DropWeight = droprates[card.Name]);
                Dictionary<string, DivCard> cards = response.Lines.ToDictionary(card => card.Name);
                dataContainer.Maps = maps.Select(map => new Map(map.Key, map.Value.Select(card => cards.GetValueOrDefault(card)).Where(card => card is not null).ToList())).ToList();
            }
            catch (Exception e)
            {

            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
