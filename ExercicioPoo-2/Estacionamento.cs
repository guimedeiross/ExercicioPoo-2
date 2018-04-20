using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExercicioPoo_2
{
    public class Informacao2
    {
        public string Placa { get; set; }
        public float PrecoHora { get; set; }
        public double Valoraserpago { get; set; }
        public int Mes { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }
    }
    class Estacionamento
    {
        String relatorio = "relatex3.xml";
        double totalMes;
        DateTime horaAtual = DateTime.Now;
        StreamWriter arquivo = new StreamWriter("ex3.txt");
        List<Informacao2> informacoes = new List<Informacao2>();
        Informacao2 novaInformacao = new Informacao2();
        public void Executar()
        {
            CarregarDadosXml();
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Cadastrar Entrada de Veiculo");
                Console.WriteLine("2 - Cadastrar Saida do Veiculo");
                Console.WriteLine("3 - Mostrar Veiculos Estacionados no Mês");
                Console.WriteLine("0 - Sair");
                opcao = Convert.ToInt32(Console.ReadLine());
                switch (opcao)
                {
                    case 0: break;
                    case 1:
                        CadastraEntradaEPrecoVeiculo();
                        break;
                    case 2:
                        CadastraSaidaVeiculo();
                        break;
                    case 3:
                        MostraVeiculoEstcionadoMes();
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        Console.ReadLine();
                        break;
                }
            } while (opcao != 0);
        }
        private void CadastraEntradaEPrecoVeiculo()
        {
            char resp = 'S';
            do
            {
                Console.Clear();
                novaInformacao = new Informacao2();
                DateTime ent;
                string data1;
                Console.Write("Informe o preço da hora: ");
                novaInformacao.PrecoHora = Convert.ToSingle(Console.ReadLine());
                if (novaInformacao.PrecoHora <= 0)
                {
                    Console.WriteLine("SOMENTE NUMEROS INTEIRO MAIORES QUE 0");
                }
                else
                {
                    do
                    {
                        Console.WriteLine("Informe a data e hora de entrada do automóvel dd/MM/yyyy HH:mm");
                        data1 = Console.ReadLine();
                        if (!DateTime.TryParseExact(data1, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out ent))
                        {
                            Console.WriteLine("Formato Inválido!");
                        }
                    } while (!DateTime.TryParseExact(data1, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out ent));
                    novaInformacao.Entrada = ent;
                    novaInformacao.Mes = novaInformacao.Entrada.Month;
                    Console.Write("Informe a placa do veículo: ");
                    novaInformacao.Placa = Console.ReadLine();
                    novaInformacao.Placa = novaInformacao.Placa.ToUpper();
                    informacoes.Add(novaInformacao);
                    SalvarDadosXml();
                    Console.Write("Você gostaria de cadastrar outra entrada? S - Sim ou N - Não: ");
                    resp = Convert.ToChar(Console.ReadLine().ToUpper());
                }
            } while (resp == 'S');
        }
        private void SalvarDadosXml()
        {
            StreamWriter relat = new StreamWriter(relatorio);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Informacao2>));
            xmlSerializer.Serialize(relat, informacoes.OrderBy(m => m.Placa).ToList());
            relat.Close();
        }
        private void CarregarDadosXml()
        {
            if (File.Exists(relatorio))
            {
                FileStream stream = System.IO.File.OpenRead(relatorio);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Informacao2>));
                informacoes = serializer.Deserialize(stream) as List<Informacao2>;
                stream.Close();
            }
        }
        private void MostrarInformacoes()
        {
            Console.Clear();
            foreach (Informacao2 per in informacoes)
            {
                Console.WriteLine($"Placa: {per.Placa}");
                Console.WriteLine($"Preço da hora: {per.PrecoHora}");
                Console.WriteLine($"Horario de entrada: {per.Entrada.ToString("dd/MM/yyyy HH:mm")}");
                Console.WriteLine("---------------------------------------------");
            }
        }
        private void CadastraSaidaVeiculo()
        {
            char resp;
            do
            {
                MostrarInformacoes();
                Console.Write("Informe a placa: ");
                string placaPesquisa = Console.ReadLine();
                List<Informacao2> placaencontrada = informacoes.Where(c => c.Placa.ToUpper().Contains(placaPesquisa.ToUpper())).ToList();
                foreach (Informacao2 info in placaencontrada)
                {
                    info.Saida = horaAtual;
                    TimeSpan tempoEst = horaAtual - info.Entrada;
                    double conversao = Convert.ToDouble(tempoEst.TotalHours);
                    conversao = Math.Ceiling(conversao);
                    info.Valoraserpago = conversao * info.PrecoHora;
                    Console.WriteLine($"PLACA ENCONTRADA: {info.Placa}");
                    Console.WriteLine($"VALOR A SER PAGO {info.Valoraserpago}");
                    totalMes += info.Valoraserpago;
                    SalvarDadosXml();
                }
                SalvarDadosXml();
                Console.Write("Você gostaria de cadastrar outra saida? S - Sim ou N - Não: ");
                resp = Convert.ToChar(Console.ReadLine().ToUpper());
            } while (resp == 'S');
        }
        private void MostraVeiculoEstcionadoMes()
        {
            char resp;
            do
            {
                Console.Write("Informe o mês que deseja o relatório com 2 digitos: ");
                int mes = Convert.ToInt32(Console.ReadLine());
                foreach (Informacao2 per in informacoes)
                {
                    if (mes == per.Mes)
                    {
                        Console.WriteLine($"PLACA: {per.Placa}");
                        Console.WriteLine($"ENTRADA: {per.Entrada}");
                        Console.WriteLine($"SAIDA: {per.Saida}");
                        Console.WriteLine($"VALOR PAGO: {per.Valoraserpago}");

                        arquivo.WriteLine($"PLACA: {per.Placa}");
                        arquivo.WriteLine($"ENTRADA: {per.Entrada}");
                        arquivo.WriteLine($"SAIDA: {per.Saida}");
                        arquivo.WriteLine($"VALOR PAGO: {per.Valoraserpago}");
                    }
                }
                Console.WriteLine($"TOTAL DO MÊS: {totalMes} R$");
                arquivo.WriteLine($"TOTAL DO MÊS: {totalMes} R$");
                Console.Write("Você deseja consultar outro mês? S - Sim ou N - Não: ");
                resp = Convert.ToChar(Console.ReadLine().ToUpper());
                if (resp == 'N')
                {
                    arquivo.Close();
                }
            } while (resp == 'S');
        }
    }
}