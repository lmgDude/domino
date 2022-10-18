using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Domino
{
    public class Logic
    {
        private HashSet<Tuple<int, int>> allDomino;
        private LinkedList<Tuple<int, int>> tableDomino;
        private int sumPoints;
        private int playerPoints;
        private int botPoints;
        private bool botStop;
        private bool playerStop;
        public List<Tuple<int, int>> playerDomino;
        private List<Tuple<int, int>> botDomino;
        private FormGame game;

        public Logic(FormGame game)
        {
            tableDomino = new LinkedList<Tuple<int, int>>();
            playerDomino = new List<Tuple<int, int>>(7);
            botDomino = new List<Tuple<int, int>>(7);
            allDomino = new HashSet<Tuple<int, int>>(28);
            for (var i = 0; i <= 6; i++)
            for (var j = i; j <= 6; j++)
                allDomino.Add(new Tuple<int, int>(i, j));
            sumPoints = 0;
            playerPoints = 0;
            botPoints = 0;
            botStop = false;
            playerStop = false;
            FillDominoPlayers();
            this.game = game;
        }
        
        private void FillDominoPlayers()
        {
            Tuple<int, int> randomDomino;
            Tuple<int, int> randomDomino5Bot;
            if (new Random().Next(2) == 0)
                randomDomino5Bot = new Random().Next(2) == 0 
                    ? new Tuple<int, int>(5, new Random().Next(0, 5)) 
                    : new Tuple<int, int>(5, 6);
            else
                randomDomino5Bot = new Random().Next(2) == 0 
                    ? new Tuple<int, int>(new Random().Next(0, 5), 5) 
                    : new Tuple<int, int>(6, 5);

            botDomino.Add(randomDomino5Bot);
            allDomino.Remove(randomDomino5Bot);
            playerDomino.Add(new Tuple<int, int>(5, 5));
            allDomino.Remove(new Tuple<int, int>(5, 5));
            while (playerDomino.Count != 3)
            {
                randomDomino = new Tuple<int, int>(new Random().Next(0, 7), new Random().Next(0, 7));
                if (allDomino.Contains(randomDomino))
                {
                    playerDomino.Add(randomDomino);
                    allDomino.Remove(randomDomino);
                }
            }
            while (botDomino.Count != 7)
            {
                randomDomino = new Tuple<int, int>(new Random().Next(0, 7), new Random().Next(0, 7));
                if (allDomino.Contains(randomDomino))
                {
                    botDomino.Add(randomDomino);
                    allDomino.Remove(randomDomino);
                }
            }
            while (playerDomino.Count!= 7)
            {
                playerDomino.Add(new Tuple<int, int>(-1, -1));
            }
        }
        
        public void MakeMoveLeft(int indexPlayer)
        {
            var domino = playerDomino[indexPlayer];
            if (!tableDomino.Contains(new Tuple<int, int>(5,5)) && !Equals(domino, new Tuple<int, int>(5,5))) return;
            if (tableDomino.Count == 0)
            {
                game.RemoveDominoesToHand(domino);
                game.AddDominoOnTableLeft(domino);
                playerDomino[indexPlayer] = new Tuple<int, int>(-1, -1);
                tableDomino.AddFirst(domino);
                CountPoints(ref playerPoints);
                BotMove();
            }
            else if (tableDomino.First().Item1 == domino.Item1 || tableDomino.First().Item1 == domino.Item2)
            {
                game.RemoveDominoesToHand(domino);
                playerDomino[indexPlayer] = new Tuple<int, int>(-1, -1);
                if (tableDomino.First().Item1 != domino.Item2)
                    domino = new Tuple<int, int>(domino.Item2, domino.Item1);
                game.AddDominoOnTableLeft(domino);
                tableDomino.AddFirst(domino);
                CountPoints(ref playerPoints);
                BotMove();
            }
            AnyMovesPlayer();
        }
        
        public void MakeMoveRight(int indexPlayer)
        {
            var domino = playerDomino[indexPlayer];
            if (!tableDomino.Contains(new Tuple<int, int>(5,5)) && !Equals(domino, new Tuple<int, int>(5,5))) return;
            if (tableDomino.Count == 0)
            {
                game.RemoveDominoesToHand(domino);
                game.AddDominoOnTableRight(domino);
                playerDomino[indexPlayer] = new Tuple<int, int>(-1, -1);
                tableDomino.AddFirst(domino);
                CountPoints(ref playerPoints);
                BotMove();
            }
            else if (tableDomino.Last().Item2 == domino.Item1 || tableDomino.Last().Item2 == domino.Item2)
            {
                game.RemoveDominoesToHand(domino);
                playerDomino[indexPlayer] = new Tuple<int, int>(-1, -1);
                if (tableDomino.Last().Item2 != domino.Item1)
                    domino = new Tuple<int, int>(domino.Item2, domino.Item1);
                game.AddDominoOnTableRight(domino);
                tableDomino.AddLast(domino);
                CountPoints(ref playerPoints);
                BotMove();
            }

            AnyMovesPlayer();
        }

        public void TakeBazar(List<Tuple<int,int>> player)
        {
            if (tableDomino.Count == 0) return;
            if (allDomino.Count == 0 && player.Equals(playerDomino))
            {
                MessageBox.Show("Базар пуст", "",
                    MessageBoxButtons.OK);
                return;
            }
            if (!player.Contains(new Tuple<int, int>(-1,-1))) return;
            if (!playerStop) AnyMovesPlayer();
            var element = allDomino.ElementAt(new Random().Next(allDomino.Count));
            player[player.IndexOf(player.First(tuple
                    => Equals(tuple, new Tuple<int, int>(-1, -1))))] = element;
            allDomino.Remove(element);
            if (player.Equals(playerDomino)) 
                game.AddDominoesToHand(element);
            AnyMovesPlayer();

        }

        private void CountPoints(ref int pointPlayerOrBot)
        {
            if (tableDomino.Count == 0) return;
            sumPoints = tableDomino.Where(domino => domino.Item1 == domino.Item2)
                .Sum(domino => domino.Item1 + domino.Item2);
            if (tableDomino.First().Item1 != tableDomino.First().Item2)
                sumPoints += tableDomino.First().Item1;
            if (tableDomino.Last().Item1 != tableDomino.Last().Item2)
                sumPoints += tableDomino.Last().Item2;
            if (sumPoints % 5 == 0) pointPlayerOrBot += sumPoints;
            game.ChangeScore(playerPoints, botPoints);
            AnyMovesPlayer();
        }


        private void BotMove()
        {
            if (botStop) return;
            if (botDomino.Any(domino => Equals(domino, new Tuple<int, int>(-1, -1))))
                while (botDomino.Contains(new Tuple<int, int>(-1, -1)) && allDomino.Count != 0)
                    TakeBazar(botDomino); 
            
            if (tableDomino.Count != 0)
            {
                for (var i = 0; i < botDomino.Count; i++)
                {
                    if (botDomino[i].Item1 == tableDomino.First().Item1 || botDomino[i].Item2 == tableDomino.First().Item1)
                    {
                        if (tableDomino.First().Item1 != botDomino[i].Item2)
                            botDomino[i] = new Tuple<int, int>(botDomino[i].Item2, botDomino[i].Item1);
                        game.AddDominoOnTableLeft(botDomino[i]);
                        tableDomino.AddFirst(botDomino[i]);
                        botDomino[i] = new Tuple<int, int>(-1, -1);
                        CountPoints(ref botPoints);
                        return;
                    }
                    if (botDomino[i].Item1 == tableDomino.Last().Item2 || botDomino[i].Item2 == tableDomino.Last().Item2)
                    {
                        if (tableDomino.Last().Item2 != botDomino[i].Item1)
                            botDomino[i] = new Tuple<int, int>(botDomino[i].Item2, botDomino[i].Item1);
                        game.AddDominoOnTableRight(botDomino[i]);
                        tableDomino.AddLast(botDomino[i]);
                        botDomino[i] = new Tuple<int, int>(-1, -1);
                        CountPoints(ref botPoints);
                        return;
                    }
                }
                botStop = true;
                MessageBox.Show(playerStop ? "У Бота закончились ходы!" 
                    : "У Бота закончились ходы! \nПродолжайте играть, пока можете!", "", MessageBoxButtons.OK);
            }
            CountPoints(ref botPoints);
        }

        private void AnyMovesPlayer()
        {
            if (botStop && playerPoints > botPoints)
                ScoringFinalPoints();
            if ((!playerDomino.Contains(new Tuple<int, int>(-1,-1)) || allDomino.Count == 0) 
                && playerDomino.All(domino => domino.Item1 != tableDomino.First()?.Item1)
                && playerDomino.All(domino => domino.Item2 != tableDomino.First()?.Item1)
                && playerDomino.All(domino => domino.Item1 != tableDomino.Last()?.Item2)
                && playerDomino.All(domino => domino.Item2 != tableDomino.Last()?.Item2)
                && !playerStop)
            {
                playerStop = true;
                game.MakeButtonsEnabled();
                MessageBox.Show( botStop ? "У вас закончились ходы..." 
                        : "У вас закончились ходы... \nПодождите, пока Бот закончит игру ", "",
                    MessageBoxButtons.OK);
                while (!botStop && botPoints <= playerPoints) BotMove();
                botStop = true;
                ScoringFinalPoints();
            }
        }

        private void ScoringFinalPoints()
        {
            if (botStop && playerPoints > botPoints)
            {
                new FormPlayerWin().ShowDialog();
                game.Close();
                return;
            }

            if (botStop && playerStop)
            {
                MessageBox.Show(
                    playerPoints == botPoints
                        ? "Ничья... \nПопробуйте снова!"
                        : "Вы проиграли... \nПопробуйте снова!", "", MessageBoxButtons.OK);

                new Thread(Open).Start();
                game.Close();
            }
        }

        void Open(object obj)
        {
            Application.Run(new FormGame());
        }
    }
}