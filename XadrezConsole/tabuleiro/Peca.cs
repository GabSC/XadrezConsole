using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace XadrezConsole.tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; set; }
        public int QuantidadeDeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }


        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="tab">Tabuleiro desejado</param>
        ///  <param name="cor">Cor da peça desejada</param>
        public Peca(Tabuleiro tab, Cor cor)
        {
            this.Posicao = null;
            this.QuantidadeDeMovimentos = 0;
            this.Tabuleiro = tab;
            this.Cor = cor;

        }
        
        public void IncrementarQtdDeMovimentos()
        {

            QuantidadeDeMovimentos++;
        }

        public void DecrementarQtdDeMovimentos()
        {

            QuantidadeDeMovimentos--;
        }



        public bool existeMovimentosPossiveis()
        {
            bool retorno =false;
            bool[,] mat = movimentosPossiveis();

            for (int lin = 0; lin < Tabuleiro.Linhas; lin++)
            {

                for (int col = 0; col < Tabuleiro.Colunas; col++)
                {
                    if (mat[lin, col])
                    {

                        retorno = true;

                    }
                    
                   

                }

            }

            return retorno;

        }
       
        public abstract bool[,] movimentosPossiveis();

        public bool podeMoverPara(Posicao pos)
        {


            return movimentosPossiveis()[pos.Linha, pos.Coluna];
        }


    }
}
