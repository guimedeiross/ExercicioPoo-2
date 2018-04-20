using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioPoo_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Selecione um exercício:\n");
                Console.WriteLine("1 - Aposentadoria");
                Console.WriteLine("2 - Pesquisa da População");
                Console.WriteLine("3 - Estacionamento");
                Console.WriteLine("4 - Agendamentos");
                Console.WriteLine("0 - Sair");

                opcao = Convert.ToInt32(Console.ReadLine());

                switch (opcao)
                {
                    case 0: break;
                    case 1:
                        Aposentadoria aposentadoria = new Aposentadoria();
                        aposentadoria.Executar();
                        break;
                    case 2:
                        Populacao populacao = new Populacao();
                        populacao.Executar();
                        break;
                    case 3:
                        Estacionamento estacionamento = new Estacionamento();
                        estacionamento.Executar();
                        break;
                    case 4:
                        Agenda agenda = new Agenda();
                        agenda.Executar();
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        Console.ReadLine();
                        break;
                }
            } while (opcao != 0);
        }
    }
}
