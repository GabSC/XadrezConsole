using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    class Rei : Peca 
    {

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab,cor)
        {

            this.partida = partida;

        }

        private PartidaDeXadrez partida;

        private bool TesteTorreParaRoque(Posicao pos)
        {
            bool ret = false;

            Peca p = Tabuleiro.getPeca(pos);

            if (p != null && p is Torre && p.Cor == Cor && p.QuantidadeDeMovimentos == 0)
            {

                ret = true;
            }

            return ret;
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
            if(Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

            }

            //Nordeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna+1);
            if (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

            }


            //Leste
            pos.DefinirValores(Posicao.Linha , Posicao.Coluna + 1);
            if (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

            }

            //Sudeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

            }


            //Sul
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna );
            if (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

            }

            //Sudoeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

            }


            //Oeste
            pos.DefinirValores(Posicao.Linha , Posicao.Coluna - 1);
            if (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

            }


            //Nororeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.TestarPosicao(pos) && PodeMover(pos))
            {

                mat[pos.Linha, pos.Coluna] = true;

            }


            // jogadas especiais ROQUE


            if(QuantidadeDeMovimentos == 0 && !partida.xeque)
            {
                Posicao posT1 = new Posicao(pos.Linha,pos.Coluna + 3);

                if (TesteTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if (Tabuleiro.getPeca(p1)== null & Tabuleiro.getPeca(p2)==null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;

                    }

                }

                Posicao posT2 = new Posicao(pos.Linha, pos.Coluna - 4);

                if (TesteTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

                    if (Tabuleiro.getPeca(p1) == null & Tabuleiro.getPeca(p2) == null && Tabuleiro.getPeca(p3) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;

                    }

                }


            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
        

    }
}
