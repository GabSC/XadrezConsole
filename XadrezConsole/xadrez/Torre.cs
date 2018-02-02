using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    class Torre : Peca
    {

        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {


        }

        private bool PodeMover(Posicao pos)
        {

            Peca p = Tabuleiro.getPeca(pos);

            return p == null || p.Cor != Cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);


            //Norte
            pos.DefinirValores(Posicao.Linha-1,Posicao.Coluna);

            while (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if(Tabuleiro.getPeca(pos) != null && Tabuleiro.getPeca(pos).Cor != this.Cor)
                {

                    break;

                }

                pos.Linha--;

            }

            //Sul
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);

            while (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.getPeca(pos) != null && Tabuleiro.getPeca(pos).Cor != this.Cor)
                {

                    break;

                }

                pos.Linha++;

            }
            //leste
            pos.DefinirValores(Posicao.Linha , Posicao.Coluna + 1);

            while (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.getPeca(pos) != null && Tabuleiro.getPeca(pos).Cor != this.Cor)
                {

                    break;

                }

                pos.Coluna++;

            }
            //Oeste
            pos.DefinirValores(Posicao.Linha , Posicao.Coluna -1);

            while (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.getPeca(pos) != null && Tabuleiro.getPeca(pos).Cor != this.Cor)
                {

                    break;

                }

                pos.Coluna--;

            }



            return mat;
        }




        public override string ToString()
        {
            return "T";
        }





    }
}
