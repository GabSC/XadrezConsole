using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace XadrezConsole.xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }


        public PosicaoXadrez(char coluna,int linha)
        {
            this.Coluna = coluna;
            this.Linha = linha;
        }


        public override string ToString()
        {
            return "" + Coluna + Linha;
        }


        public Posicao ToPosicao()
        {

            return new Posicao(8 - Linha, Coluna - 'a');

        }
    }
}
