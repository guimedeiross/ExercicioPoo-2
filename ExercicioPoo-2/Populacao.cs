using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExercicioPoo_2
{
    public class Informacao
    {
        public float SalBruto { get; set; }
        public int NumFilhos { get; set; }
    }
    class Populacao
    {
        List<Informacao> info = new List<Informacao>();
        String relatorio = "ex2.xml";

        private void CadNovaPesquisa()
        {
            Informacao infoo = new Informacao();

            Console.Clear();
            Console.WriteLine("Informe seu salário bruto:");
            infoo.SalBruto = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("Informe a quantidade de filhos:");
            infoo.NumFilhos = Convert.ToInt32(Console.ReadLine());
            info.Add(infoo);

        }
        private void MostraRelatorio()
        {
            Console.Clear();
            Console.WriteLine("RELATÓRIO");

            Console.WriteLine("Média de salário da população: " + CalculaMedia() + ".");

            Console.WriteLine("Média do número de filhos: " + MediaNumFilhos() + ".");

            Console.WriteLine("Três maiores salários entre os habitantes consultados:");
            TresMaioresSalarios();

            Console.WriteLine("Três menores salários entre os habitantes consultados:");
            TresMenoresSalarios();

            Console.WriteLine("Percentual de pessoas com salário menor que R$ 1500,00: " + PercM() + "%");
            Console.ReadKey();
            SalvarDados();
        }


        private void TresMaioresSalarios()
        {
            var Sal3Maior = info.OrderByDescending(i => i.SalBruto).Take(3);
            foreach (var sal in Sal3Maior)
            {
                Console.WriteLine(sal.SalBruto);
            }
        }
        private void TresMenoresSalarios()
        {
            var Sal3Menor = info.OrderBy(i => i.SalBruto).Take(3);
            foreach (var sal2 in Sal3Menor)
            {
                Console.WriteLine(sal2.SalBruto);
            }
        }
        private double PercM()
        {
            var s = 0;
            foreach (Informacao s1 in info)
            {
                if (s1.SalBruto < 1500)
                {
                    s++;
                }
            }
            return ((double)s / info.Count) * 100;
        }
        private float MediaNumFilhos()
        {
            float soma1 = 0, media1;
            foreach (Informacao som1 in info)
            {
                soma1 += som1.NumFilhos;
            }
            media1 = soma1 / info.Count;

            return media1;
        }
        private float CalculaMedia()
        {
            float soma = 0, media;
            foreach (Informacao som in info)
            {
                soma += som.SalBruto;
            }
            media = soma / info.Count;

            return media;
        }
        private void SalvarDados()
        {
            StreamWriter relato = new StreamWriter(relatorio);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Informacao>));
            xmlSerializer.Serialize(relato, info);
            relato.Close();
        }
        private void CarregarDados()
        {
            if (File.Exists(relatorio))
            {
                FileStream stream = System.IO.File.OpenRead(relatorio);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Informacao>));
                info = serializer.Deserialize(stream) as List<Informacao>;
                stream.Close();
            }
        }
        public void Executar()
        {
            CarregarDados();
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("PESQUISA");
                Console.WriteLine("1 - Cadastrar nova pesquisa");
                Console.WriteLine("2 - Mostrar Relatório");
                Console.WriteLine("0 - Sair");
                opcao = Convert.ToInt32(Console.ReadLine());
                switch (opcao)
                {
                    case 0: break;
                    case 1:
                        CadNovaPesquisa();
                        break;
                    case 2:
                        MostraRelatorio();
                        break;
                    default:
                        Console.WriteLine("Opção Inválida!");
                        Console.ReadLine();
                        break;
                }
            } while (opcao != 0);
        }
    }
}
