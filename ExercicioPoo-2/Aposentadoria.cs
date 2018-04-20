using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExercicioPoo_2
{
    public class Info
    {
        public string Cpf { get; set; }
        public DateTime DataN { get; set; }
        public DateTime DataI { get; set; }
    }
    class Aposentadoria
    {
        Info info = new Info();
        String nomearquivo = "ex1.xml";
        public void Executar()
        {
            CarregarDados();
            DateTime das, das2;
            string dateys2 = string.Empty, dateys = string.Empty;
            Console.WriteLine("Informe seu cpf sem pontos e sem traços");
            info.Cpf = Console.ReadLine();
            do
            {
                Console.Write("Informe a data do seu nascimento dd/MM/yyyy: ");
                dateys2 = Console.ReadLine();

                if (!DateTime.TryParseExact(dateys2, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out das2))
                {
                    Console.WriteLine("Formato Inválido!!");
                }
            } while (!DateTime.TryParseExact(dateys2, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out das2));
            info.DataN = das2;
            DateTime atual = DateTime.Now;
            TimeSpan idade = atual - info.DataN;
            float idadeD = (float)Math.Floor(idade.TotalDays / 365);
            do
            {
                Console.Write("Informe a data de admissão na empresa dd/MM/yyyy: ");
                dateys = Console.ReadLine();

                if (!DateTime.TryParseExact(dateys, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out das))
                {
                    Console.WriteLine("Formato Inválido!!");
                }
            } while (!DateTime.TryParseExact(dateys, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out das));
            info.DataI = das;
            TimeSpan dataAdm = atual - info.DataI;
            float dataAdmFloat = (float)Math.Floor(dataAdm.TotalDays / 365);
            if (idadeD >= 65 | dataAdmFloat >= 30)
            {
                Console.WriteLine($"Sua idade é de: {idadeD} anos");
                Console.WriteLine($"Vc esta: {dataAdmFloat} anos trabalhando.");
                Console.WriteLine("Requerer aposentadoria");
            }
            else
            {
                if (idadeD >= 60 && dataAdmFloat >= 25)
                {
                    Console.WriteLine($"Sua idade é de: {idadeD} anos");
                    Console.WriteLine($"Vc esta: {dataAdmFloat} anos trabalhando.");
                    Console.WriteLine("Requerer aposentadoria");
                }
                else
                {
                    Console.WriteLine("Não Requerer aposentadoria");
                }
            }
            SalvarDados();
            Console.ReadLine();
        }
        private void SalvarDados()
        {
            StreamWriter arquivo = new StreamWriter(nomearquivo);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Info));
            xmlSerializer.Serialize(arquivo, info);
            arquivo.Close();
        }
        private void CarregarDados()
        {
            if (File.Exists(nomearquivo))
            {
                FileStream arquivo = File.OpenRead(nomearquivo);
                XmlSerializer serializer = new XmlSerializer(typeof(Info));
                Info informacao = serializer.Deserialize(arquivo) as Info;
                arquivo.Close();
            }
        }
    }
}
