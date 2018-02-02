using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace XadrezConsole.tabuleiro
{
    class Tabuleiro
    {
        public int Colunas { get; set; }
        public int Linhas { get; set; }

        private Peca[,] pecas;

        public Tabuleiro(int cols,int lins)
        {

            this.Colunas = cols;
            this.Linhas = lins;

            pecas = new Peca[lins, cols];

        }

        public Peca getPeca(int lin,int col)
        {


            return pecas[lin, col];
        }

        public void ColocarPeca(Peca peca, Posicao pos)
        {

            if (ExistePecaEm(pos)){

                throw new TabuleiroException("Já existe peça nessa posição!");
            }
            else
            {

                pecas[pos.Linha, pos.Coluna] = peca;
                peca.Posicao = pos;

            }

        }

        public Peca RetirarPeca(Posicao pos)
        {
            Peca p = null;

            if(getPeca(pos) == null)
            {

                p = null;
            }
            else
            {

                Peca aux = getPeca(pos);
                aux.Posicao = null;
                pecas[pos.Linha,pos.Coluna] = null;
                p = aux;

            }

            return p;
        }

        public Peca getPeca(Posicao pos)
        {

            return pecas[pos.Linha, pos.Coluna];
        }




        public bool TestarPosicao(Posicao pos)
        {
            bool ret;

            if(pos.Linha <0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {

                ret = false;
            }
            else
            {

                ret = true;

            }

            return ret;
        }

        public bool ExistePecaEm(Posicao pos)
        {
            ValidarPosicao(pos);
            return getPeca(pos) != null;
        }

        private void ValidarPosicao(Posicao pos)
        {

            if(TestarPosicao(pos) == false)
            {

                throw new TabuleiroException("Posição Inválida!");

            }
        }

    }
}
