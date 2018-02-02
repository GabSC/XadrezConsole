using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    class Peao : Peca
    {

       

        public Peao(Tabuleiro tab,Cor cor, PartidaDeXadrez partida) : base (tab, cor)
        {
            this.partida = partida;
        }

        private PartidaDeXadrez partida;


        public override string ToString()
        {
            return "P";
        }


        private bool existeInimigo(Posicao pos)
        {

            Peca p = Tabuleiro.getPeca(pos);

            return p != null && p.Cor != Cor;
        }

        private bool livre(Posicao pos)
        {

            return Tabuleiro.getPeca(pos) == null;
        }


        public override bool[,] movimentosPossiveis()
        {

            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            if(Cor == Cor.branca)
            {

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);

                if(Tabuleiro.TestarPosicao(pos) && livre(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;
                }


                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);

                if (Tabuleiro.TestarPosicao(pos) && livre(pos) && QuantidadeDeMovimentos == 0)
                {

                    mat[pos.Linha, pos.Coluna] = true;
                }


                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);

                if (Tabuleiro.TestarPosicao(pos) && existeInimigo(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;
                }


                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);

                if (Tabuleiro.TestarPosicao(pos) && existeInimigo(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;
                }

                if(Posicao.Linha == 3)
                {

                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.TestarPosicao(esquerda) && existeInimigo(esquerda) && Tabuleiro.getPeca(esquerda) == partida.vulneravelEnPassant)
                    {

                        mat[esquerda.Linha -1, esquerda.Coluna] = true;

                    }

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.TestarPosicao(direita) && existeInimigo(direita) && Tabuleiro.getPeca(direita) == partida.vulneravelEnPassant)
                    {

                        mat[direita.Linha -1, direita.Coluna] = true;

                    }
                }


            }
            else
            {

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);

                if (Tabuleiro.TestarPosicao(pos) && livre(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;
                }


                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);

                if (Tabuleiro.TestarPosicao(pos) && livre(pos) && QuantidadeDeMovimentos == 0)
                {

                    mat[pos.Linha, pos.Coluna] = true;
                }


                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);

                if (Tabuleiro.TestarPosicao(pos) && existeInimigo(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;
                }


                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);

                if (Tabuleiro.TestarPosicao(pos) && existeInimigo(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;
                }

                if (Posicao.Linha == 4)
                {

                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.TestarPosicao(esquerda) && existeInimigo(esquerda) && Tabuleiro.getPeca(esquerda) == partida.vulneravelEnPassant)
                    {

                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;

                    }

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.TestarPosicao(direita) && existeInimigo(direita) && Tabuleiro.getPeca(direita) == partida.vulneravelEnPassant)
                    {

                        mat[direita.Linha + 1, direita.Coluna] = true;

                    }
                }


            }



            return mat;
        }


     }
}
