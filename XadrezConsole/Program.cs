using System;
using tabuleiro;
using XadrezConsole.tabuleiro;
using XadrezConsole.xadrez;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();
            try {
               

                while (!partida.terminado)
                {
                    

                    
                    try {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);
                        Console.WriteLine();

                        Console.WriteLine("Partida: " + partida.turno);
                        Console.WriteLine("Aguardando jogada da cor: " + partida.jogadorAtual);

                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().ToPosicao();
                        partida.validarPosicaoDeOrigem(origem);
                        bool[,] movimentosPossiveisDestacados = partida.Tab.getPeca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiroNaTela(partida.Tab, movimentosPossiveisDestacados);


                        Console.Write("Destino:");
                        Posicao destino = Tela.lerPosicaoXadrez().ToPosicao();

                        partida.realizarJogada(origem, destino);

                       

                    } catch (TabuleiroException e)
                    {

                        Console.WriteLine(e.Message);
                        Console.ReadLine();

                    }
                   

                   
                }

                Console.Clear();

                Tela.ImprimirPartida(partida);


            } catch(TabuleiroException ex) {

                Console.Write(ex.Message);

            }

            
            

            

            Tela.ImprimirTabuleiroNaTela(partida.Tab);

            Console.Read();

        }
    }
}
