using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminado { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> pecasCapturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }
        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.branca;
            terminado = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();
            InicializarTabuleiro();
        }



        public Peca ExecutarMovimento(Posicao origem,Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdDeMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);

            if(pecaCapturada != null)
            {

                pecasCapturadas.Add(pecaCapturada);
            }

            //# jogadda especial Roque pequeno

            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha,origem.Coluna +3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = Tab.RetirarPeca(origemT);
                t.IncrementarQtdDeMovimentos();
                Tab.ColocarPeca(t, destinoT);
            }

            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca t = Tab.RetirarPeca(origemT);
                t.IncrementarQtdDeMovimentos();
                Tab.ColocarPeca(t, destinoT);
            }

            //#jogada especial en passant


            if( p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP = null;

                    if (p.Cor == Cor.branca)
                    {
                        posP = new Posicao(destino.Linha + 1,destino.Coluna);

                    }
                    else{

                        posP = new Posicao(destino.Linha - 1, destino.Coluna);

                    }
                    pecaCapturada = Tab.RetirarPeca(posP);
                    pecasCapturadas.Add(pecaCapturada);
                }

            }

            return pecaCapturada;
        }



        public HashSet<Peca> getConjuntoPecasCapturadas(Cor cor)
        {

            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecasCapturadas)
            {

                if(x.Cor == cor)
                {

                    aux.Add(x);

                }


            }


            return aux;
        }

        public HashSet<Peca> getPecasEmJogo(Cor cor)
        {

            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecasCapturadas)
            {

                if (x.Cor == cor)
                {

                    aux.Add(x);

                }


            }

            aux.ExceptWith(getConjuntoPecasCapturadas(cor));
            return aux;
        }


        private Cor getAdversario(Cor cor)
        {
            Cor x;

            if(cor == Cor.branca)
            {

                x = Cor.branca;
            }
            else{

                x = Cor.preta;


            }

            return x;
        }


        private Peca getRei(Cor cor)
        {
            Peca ret = null;
            foreach (Peca item in getPecasEmJogo(cor))
            {

                if(item is Rei)
                {

                    ret = item;

                }
                else
                {

                    ret = null;

                }

            }

            return ret;
        }


        public bool EstaEmXeque(Cor cor)
        {
            Peca R = getRei(cor);
            bool ret = false;

            foreach (Peca item in getPecasEmJogo(getAdversario(cor)))
            {

                bool[,] mat = item.movimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    ret =true;

                }
                else
                {

                    ret = false;
                }

            }

            return ret;
        }


        public bool TesteXequeMate(Cor cor)
        {

            if (!EstaEmXeque(cor))
            {

                return false;
            }

            foreach (Peca item in getPecasEmJogo(cor))
            {

                bool[,] mat = item.movimentosPossiveis();

                for (int li = 0; li < Tab.Linhas; li++)
                {

                    for (int co = 0; co < Tab.Colunas; co++)
                    {

                        if (mat[li, co])
                        {
                            Posicao origem = item.Posicao;
                            Posicao destino = new Posicao(li,co);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            desfazMovimento(origem,destino,pecaCapturada);
                            if (!testeXeque)
                            {

                                return false;
                            }

                        }

                    }

                }
                
            }
            return true;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {

            Peca p = Tab.RetirarPeca(destino);


            p.DecrementarQtdDeMovimentos();

            if(pecaCapturada != null)
            {

                Tab.ColocarPeca(pecaCapturada,destino);

                pecasCapturadas.Remove(pecaCapturada);

            }

            Tab.ColocarPeca(p, origem);
            // roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = Tab.RetirarPeca(destinoT);
                t.DecrementarQtdDeMovimentos();
                Tab.ColocarPeca(t, origemT);
            }

            // roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca t = Tab.RetirarPeca(destinoT);
                t.DecrementarQtdDeMovimentos();
                Tab.ColocarPeca(t, origemT);
            }


            //# en passant


            if(p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posP = null;
                    if (p.Cor == Cor.branca)
                    {

                        posP = new Posicao(3,destino.Coluna);
                    }
                    else
                    {

                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tab.ColocarPeca(peao, posP);
                }

            }

        }

        public void realizarJogada(Posicao origem,Posicao destino)
        {
            
           Peca pecaCapturada = ExecutarMovimento(origem, destino);
            Peca p = Tab.getPeca(destino);

            //jogada especial promocao
            if(p is Peao)
            {

                if((p.Cor == Cor.branca && destino.Linha == 0) || (p.Cor == Cor.preta && destino.Linha == 7))
                {

                    p = Tab.RetirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(Tab,p.Cor);
                    Tab.ColocarPeca(dama,destino);

                    pecas.Add(dama);

                }

            }

            if (EstaEmXeque(jogadorAtual))
            {

                desfazMovimento(origem, destino, pecaCapturada);

                throw new TabuleiroException("Você não pode se colocar em Xeque!");

            }

            if (EstaEmXeque(getAdversario(jogadorAtual)))
            {

                xeque = true;

            }
            else
            {

                xeque = false;
            }


            if (TesteXequeMate(getAdversario(jogadorAtual)))
            {
                terminado = true;

            }
            else
            {

                turno++;
                mudarJogador();

            }

           

            //#jogada en passant

            if(p is Peao && (destino.Linha == origem.Linha -2 || destino.Linha == origem.Linha + 2))
            {
                vulneravelEnPassant = p;

            }
            else
            {

                vulneravelEnPassant = null;

            }

        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(Tab.getPeca(pos) == null)
            {

                throw new TabuleiroException("Não existe peca na posição de origem escolhida!");

            }

            if(jogadorAtual != Tab.getPeca(pos).Cor)
            {

                throw new TabuleiroException("A peca escolhida não é sua!! LADRÃO!!");

            }

            if (!Tab.getPeca(pos).existeMovimentosPossiveis())
            {


                throw new TabuleiroException("Não existe movimentos ppossiveis para essa peca!");
            }

        }


        public void validarPosicaoDeDestino(Posicao origem,Posicao destino)
        {
            if (!Tab.getPeca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posicao de destino invalida!");

            }
        }

        public void mudarJogador()
        {
            if(jogadorAtual == Cor.branca)
            {

                jogadorAtual = Cor.preta;


            }else
            {

                jogadorAtual = Cor.branca;

            }

        }

        public void colocarNovaPeca(char coluna,int linha,Peca p)
        {

            Tab.ColocarPeca(p, new PosicaoXadrez( coluna, linha).ToPosicao());
            pecas.Add(p);
        }

        public void InicializarTabuleiro() {

            colocarNovaPeca('a',1,new Torre(Tab,Cor.branca));
            colocarNovaPeca('b', 1, new Cavalo(Tab, Cor.branca));
            colocarNovaPeca('c', 1, new Bispo(Tab, Cor.branca));
            colocarNovaPeca('d', 1, new Dama(Tab, Cor.branca));
            colocarNovaPeca('e', 1, new Rei(Tab, Cor.branca,this));
            colocarNovaPeca('f', 1, new Bispo(Tab, Cor.branca));
            colocarNovaPeca('g', 1, new Cavalo(Tab, Cor.branca));
            colocarNovaPeca('h', 1, new Torre(Tab, Cor.branca));
            colocarNovaPeca('a', 2, new Peao(Tab, Cor.branca,this));
            colocarNovaPeca('b', 2, new Peao(Tab, Cor.branca,this));
            colocarNovaPeca('c', 2, new Peao(Tab, Cor.branca,this));
            colocarNovaPeca('d', 2, new Peao(Tab, Cor.branca,this));
            colocarNovaPeca('e', 2, new Peao(Tab, Cor.branca,this));
            colocarNovaPeca('f', 2, new Peao(Tab, Cor.branca,this));
            colocarNovaPeca('g', 2, new Peao(Tab, Cor.branca,this));
            colocarNovaPeca('h', 2, new Peao(Tab, Cor.branca,this));


            colocarNovaPeca('a', 8, new Torre(Tab, Cor.preta));
            colocarNovaPeca('b', 8, new Cavalo(Tab, Cor.preta));
            colocarNovaPeca('c', 8, new Bispo(Tab, Cor.preta));
            colocarNovaPeca('d', 8, new Dama(Tab, Cor.preta));
            colocarNovaPeca('e', 8, new Rei(Tab, Cor.preta,this));
            colocarNovaPeca('f', 8, new Bispo(Tab, Cor.preta));
            colocarNovaPeca('g', 8, new Cavalo(Tab, Cor.preta));
            colocarNovaPeca('h', 8, new Torre(Tab, Cor.preta));
            colocarNovaPeca('a', 7, new Peao(Tab, Cor.preta,this));
            colocarNovaPeca('b', 7, new Peao(Tab, Cor.preta,this));
            colocarNovaPeca('c', 7, new Peao(Tab, Cor.preta,this));
            colocarNovaPeca('d', 7, new Peao(Tab, Cor.preta,this));
            colocarNovaPeca('e', 7, new Peao(Tab, Cor.preta,this));
            colocarNovaPeca('f', 7, new Peao(Tab, Cor.preta,this));
            colocarNovaPeca('g', 7, new Peao(Tab, Cor.preta,this));
            colocarNovaPeca('h', 7, new Peao(Tab, Cor.preta,this));

        }

    }
}
