using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    class Bispo : Peca
    {

        public Bispo(Tabuleiro tab,Cor cor) :base (tab,cor)
        {

        }


        public override string ToString()
        {
            return "B";
        }

        private bool PodeMover(Posicao pos)
        {

            Peca p = Tabuleiro.getPeca(pos);

            return p == null || p.Cor != Cor;
        }


        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(0,0);

            //NO
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.getPeca(pos) != null && Tabuleiro.getPeca(pos).Cor != Cor)
                {
                    break;

                }
                pos.DefinirValores(pos.Linha - 1,pos.Coluna - 1);

            }

            //NE
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.getPeca(pos) != null && Tabuleiro.getPeca(pos).Cor != Cor)
                {
                    break;

                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);

            }

            //SE
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.getPeca(pos) != null && Tabuleiro.getPeca(pos).Cor != Cor)
                {
                    break;

                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);

            }

            //SO
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.getPeca(pos) != null && Tabuleiro.getPeca(pos).Cor != Cor)
                {
                    break;

                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);

            }

            return mat;
        }

    }
}
