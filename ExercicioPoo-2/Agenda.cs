using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExercicioPoo_2
{
    public class Agendamento
    {
        public string Matricula { get; set; }
        public DateTime HorarioAtendimento { get; set; }
    }
    class Agenda
    {
        string relatorio = "relatex4.xml";
        List<Agendamento> Agendamentos = new List<Agendamento>();
        Agendamento agendamento = new Agendamento();
        public void Executar()
        {
            CarregarDadosXml();
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Cadastrar Agendamento");
                Console.WriteLine("2 - Remover Agendamento");
                Console.WriteLine("3 - Relatorio de Agendamentos");
                Console.WriteLine("0 - Sair");
                opcao = Convert.ToInt32(Console.ReadLine());
                switch (opcao)
                {
                    case 0: break;
                    case 1:
                        CadastrarNovoAgendamento();
                        break;
                    case 2:
                        RemoverAgendamentoDaLista();
                        break;
                    case 3:
                        ListarAgendamentosMeiaHora();
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        Console.ReadLine();
                        break;
                }
            } while (opcao != 0);
        }
        private void CadastrarNovoAgendamento()
        {
            Console.Clear();
            Boolean validacao = false, entra = false;
            string data, matricula, maisDeUmDia = string.Empty;
            DateTime dataValidada;
            agendamento = new Agendamento();
            Console.Write("Informe a matricula do Aluno: ");
            matricula = Console.ReadLine();
            agendamento.Matricula = matricula;
            do
            {
                Console.Write("Informe a data e horario para atendimento dd/MM/yyyy HH:mm: ");
                data = Console.ReadLine();
                if (DateTime.TryParseExact(data, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out dataValidada))
                {
                    agendamento.HorarioAtendimento = dataValidada;
                    maisDeUmDia = dataValidada.ToString("dd/MM/yyyy");
                }
                else
                {
                    Console.WriteLine("Formato Inválido!");
                    Console.ReadLine();
                }
            } while (!DateTime.TryParseExact(data, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out dataValidada));

            foreach (Agendamento agenda in Agendamentos)
            {
                if (agenda.Matricula.Equals(matricula) && agenda.HorarioAtendimento.ToString("dd/MM/yyyy").Equals(maisDeUmDia))
                {
                    validacao = true;
                    Console.WriteLine("Já EXISTE UM AGENDAMENTO PARA ESTE DIA.");
                    Console.ReadLine();
                }
                else
                {
                    validacao = false;
                }
            }

            foreach (Agendamento agenda2 in Agendamentos)
            {
                if (validacao == false)
                {
                    TimeSpan Meia = dataValidada - agenda2.HorarioAtendimento;
                    int MeiaHora = Math.Abs(Meia.Hours);
                    if (MeiaHora < 0.5)
                    {
                        Console.WriteLine("JA EXISTE UM AGENDAMENTO PARA ESTE PERIODO 30 MIN.");
                        entra = true;
                    }
                    else
                    {
                        entra = false;
                    }
                    Console.ReadLine();
                }
            }

            if (validacao == false && entra == false)
            {
                Agendamentos.Add(agendamento);
                SalvarDadosXml();
            }
        }
        private void MostrarAgendamentos(List<Agendamento> Agendamentos)
        {
            Console.Clear();
            foreach (Agendamento age in Agendamentos)
            {
                Console.WriteLine($"MATRICULA: {age.Matricula}");
                Console.WriteLine();
                Console.WriteLine($"HORARIO DE ATENDIMENTO: {age.HorarioAtendimento}");
            }
        }
        private void RemoverAgendamentoDaLista()
        {
            Console.Write("Informe a matricula: ");
            string mat = Console.ReadLine();
            List<Agendamento> matriculaEncontrada = Agendamentos.Where(d => d.Matricula.Contains(mat)).ToList();
            MostrarAgendamentos(matriculaEncontrada);
            Console.WriteLine("Tem certeza que quer remover? (S/N)");
            string confirmacao = Console.ReadLine();
            if (confirmacao.ToUpper() == "S")
            {
                foreach (Agendamento per in matriculaEncontrada)
                {
                    Agendamentos.Remove(per);
                }
                Console.WriteLine("Contatos removidos com sucesso!");
                SalvarDadosXml();
                Console.ReadLine();
            }
        }
        private void ListarAgendamentosMeiaHora()
        {
            MostrarAgendamentos(Agendamentos.OrderBy(d => d.HorarioAtendimento).ToList());
            Console.ReadLine();
        }
        private void SalvarDadosXml()
        {
            StreamWriter relato = new StreamWriter(relatorio);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Agendamento>));
            xmlSerializer.Serialize(relato, Agendamentos.OrderBy(m => m.HorarioAtendimento).ToList());
            relato.Close();
        }
        private void CarregarDadosXml()
        {
            if (File.Exists(relatorio))
            {
                FileStream stream = System.IO.File.OpenRead(relatorio);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Agendamento>));
                Agendamentos = serializer.Deserialize(stream) as List<Agendamento>;
                stream.Close();
            }
        }
    }
}