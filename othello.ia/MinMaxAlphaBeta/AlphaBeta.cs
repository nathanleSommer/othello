using System;
using System.Collections.Generic;
using System.Text;

namespace othello.ia.MinMax
{
    public class AlphaBeta
    {
        bool MaxPlayer = true;
        int[,] Board;

        public AlphaBeta(int[,] board)
        {
            Board = board;
        }

        public int Iterate(Node node, int depth, int alpha, int beta, bool Player)
        {
            if (depth == 0 || node.IsTerminal(Player))
            {
                return node.GetTotalScore(Player);
            }

            if (Player == MaxPlayer)
            {
                foreach (Node child in node.Children(Player))
                {
                    alpha = Math.Max(alpha, Iterate(child, depth - 1, alpha, beta, !Player));
                    if (beta < alpha)
                    {
                        break;
                    }

                }

                return alpha;
            }
            else
            {
                foreach (Node child in node.Children(Player))
                {
                    beta = Math.Min(beta, Iterate(child, depth - 1, alpha, beta, !Player));

                    if (beta < alpha)
                    {
                        break;
                    }
                }

                return beta;
            }
        }
    }
}
