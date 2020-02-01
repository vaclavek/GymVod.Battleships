using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymVod.Battleships.DataLayer.Model;
using GymVod.Battleships.DataLayer.Repositories;
using GymVod.Battleships.DataLayer.UnitOfWorks;
using GymVod.Battleships.Services.Players.ViewModels;
using Microsoft.AspNetCore.Http;

namespace GymVod.Battleships.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PlayerService(IPlayerRepository playerRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            this.playerRepository = playerRepository;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<PlayerListVM>> GetAllPlayersAsync()
        {
            var players = await playerRepository.GetAllPlayersAsync();
            return players.Select(x => new PlayerListVM
            {
                Id = x.Id,
                Name = x.Name,
                League = x.League,
                Created = x.Created
            }).ToList();
        }

        public async Task InsertNewPlayerAsync(PlayerVM player)
        {
            var ip = GetIpAddressFromHttpRequest(httpContextAccessor.HttpContext.Request);
            unitOfWork.AddForInsert(new Player
            {
                Name = player.Name,
                Password = player.Password,
                League = player.League,
                FileGuid = player.FileGuid,
                Created = player.Created,
                IP = ip
            });
            await unitOfWork.CommitAsync();
        }

        private static string GetIpAddressFromHttpRequest(HttpRequest httpRequest)
        {
            string ipAddressString = string.Empty;
            if (httpRequest == null)
            {
                return ipAddressString;
            }
            if (httpRequest.Headers != null && httpRequest.Headers.Count > 0)
            {
                if (httpRequest.Headers.ContainsKey("X-Forwarded-For") == true)
                {
                    string headerXForwardedFor = httpRequest.Headers["X-Forwarded-For"];
                    if (string.IsNullOrEmpty(headerXForwardedFor) == false)
                    {
                        string xForwardedForIpAddress = headerXForwardedFor.Split(':')[0];
                        if (string.IsNullOrEmpty(xForwardedForIpAddress) == false)
                        {
                            ipAddressString = xForwardedForIpAddress;
                        }
                    }
                }
            }
            else if (httpRequest.HttpContext == null ||
                 httpRequest.HttpContext.Connection == null ||
                 httpRequest.HttpContext.Connection.RemoteIpAddress == null)
            {
                ipAddressString = httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return ipAddressString;
        }
    }
}
