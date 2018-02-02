using System;
using System.Collections.Generic;
using XadrezConsole.tabuleiro;
using XadrezConsole.xadrez;

namespace XadrezConsole
{
    class Tela
    {

        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiroNaTela(partida.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno "+ partida.turno);
            if (!partida.terminado)
            {
                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

                if (partida.xeque)
                {

                    Console.WriteLine("Xeque!");
                }

            }else
            {

                Console.WriteLine("XEQUEMATE!!");
                Console.WriteLine("Vencedor: " + partida.jogadorAtual);
            }

           

        }


        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida){

            Console.WriteLine("Peças capturadas");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.getConjuntoPecasCapturadas(Cor.branca));

            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.getConjuntoPecasCapturadas(Cor.preta));

            Console.ForegroundColor = aux;
            Console.WriteLine();

        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {

            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.WriteLine(x + " ");


            }

            Console.Write("]");

        }


        public static void ImprimirTabuleiroNaTela(Tabuleiro tab) {


            for (int lin = 0; lin < tab.Linhas; lin++)
            {
                Console.Write(tab.Linhas - lin + " ");

                for (int col = 0; col < tab.Colunas; col++)
                {
                    Tela.ImprimirPeca(tab.getPeca(lin, col));
                   
                    
                }
                Console.WriteLine();

            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiroNaTela(Tabuleiro tab, bool[,] possicoesPossiveisDestacadas)
        {

            ConsoleColor fundoOriginal = Console.BackgroundColor;

            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int lin = 0; lin < tab.Linhas; lin++)
            {
                Console.Write(tab.Linhas - lin + " ");

                for (int col = 0; col < tab.Colunas; col++)
                {
                    if (possicoesPossiveisDestacadas[lin, col])
                    {
                        Console.BackgroundColor = fundoAlterado;

                    }else
                    {
                         Console.BackgroundColor = fundoOriginal;

                    }

                    Tela.ImprimirPeca(tab.getPeca(lin, col));

                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
                Console.BackgroundColor = fundoOriginal;

            }
            Console.WriteLine("  a b c d e f g h");
        }


        public static PosicaoXadrez lerPosicaoXadrez() {
            String s = Console.ReadLine();

            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }


        public static void ImprimirPeca(Peca p)
        {


            if (p == null)
            {

                Console.Write("- ");

            }
            else
            {
                if (p.Cor == Cor.branca)
                {

                    Console.Write(p);


                }
                else
                {

                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(p);
                    Console.ForegroundColor = aux;

                }

                Console.Write(" ");

            }



            

        }

    }
}
