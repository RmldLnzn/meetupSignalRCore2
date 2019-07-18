using meetupSignalRCore2.Hubs;
using meetupSignalRCore2.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;

namespace meetupSignalRCore2.Services
{
    public class TennisGameService
    {
        private readonly IHubContext<TennisHub, ITennisClient> _hubContext;
        private Timer _timer;

        private int[] points = new int[2];
        private int[] games = new int[2];
        private readonly string playerOne;
        private readonly string playerTwo;
        private bool gameStarted;

        private object _syncLock = "lock";

        public TennisGameService(string playerOne, string playerTwo, IHubContext<TennisHub, ITennisClient> hubContext)
        {
            this.playerOne = playerOne;
            this.playerTwo = playerTwo;
            _hubContext = hubContext;
        }

        #region TennisGame
        public void StartGame()
        {
            if (gameStarted == false)
            {
                SendFinishGame(string.Empty);
                games[0] = 0;
                games[1] = 0;
                points[0] = 0;
                points[1] = 0;
                _timer = new Timer(SendInformation, null, 2000, 1000);
                gameStarted = true;
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private string GetScore()
        {
            if (points[0] >= 3 && points[1] >= 3 && 
                (Math.Abs(points[0] - points[1]) < 2))
            {
                if (points[0] == points[1])
                    return "Deuce";
                else if (points[0] > points[1])
                    return $"Advantage {playerOne}";
                else
                    return $"Advantage {playerTwo}";
            }

            if (points[0] < 4 && points[1] < 4)
            {
                return $"{playerOne} {TranslateScore(points[0])} - {TranslateScore(points[1])} {playerTwo}";
            }

            if (points[0] >= 4)
            {
                var playerOneResult = $"{TranslateScore(points[0])} {playerOne}";
                AddGame(playerOne);
                return playerOneResult;
            }

            var playerTwoResult = $"{TranslateScore(points[1])} {playerTwo}";
            AddGame(playerTwo);
            return playerTwoResult;
        }

        private string GetGame() => $"{playerOne} {games[0].ToString()} - {games[1].ToString()} {playerTwo}";

        private void Volley(string scoringPlayer)
        {
            if (scoringPlayer == playerOne)
            {
                points[0]++;
            }
            else
            {
                points[1]++;
            }
        }

        private string TranslateScore(int points)
        {
            switch (points)
            {
                case 3:
                    return "40";
                case 2:
                    return "30";
                case 1:
                    return "15";
                case 0:
                    return "0";
                default:
                    return "Game";
            }
        }

        private void AddGame(string scoringPlayer)
        {
            points[0] = 0;
            points[1] = 0;

            if (scoringPlayer == playerOne)
            {
                games[0]++;
            }
            else
            {
                games[1]++;
            }

            if (games[0] >= 6 && games[0]- games[1] >= 2 && gameStarted == true)
            {
                FinishGame($"{playerOne} won {games[0].ToString()} - {games[1].ToString()}");
            }

            if (games[1] >= 6 && games[1] - games[0] >= 2 && gameStarted == true)
            {
                FinishGame($"{playerTwo} won {games[1].ToString()} - {games[0].ToString()}");
            }
        }

        private void MovePlayer()
        {
            bool player = new Random().Next(0, 99) % 2 == 0;

            if (player)
            {
                Volley(playerOne);
            }
            else
            {
                Volley(playerTwo);
            }
        }

        private void FinishGame(string result)
        {
            SendFinishGame(result);
            Dispose();
            gameStarted = false;
        }

        #endregion

        #region SignalR
        private void SendInformation(object state)
        {
            lock (_syncLock)
            {
                MovePlayer();

                _hubContext.Clients.Group("pointsEventsSubscribers").ReceivePointsEvents(GetScore());

                _hubContext.Clients.Group("gamesEventsSubscribers").ReceiveGamesEvents(GetGame());
            }
        }

        private void SendFinishGame(string result)
        {
            _hubContext.Clients.All.SendFinishGame(result);
        }

        #endregion
    }
}
