using CapturaArquivoData.ValueObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CapturaArquivoData.Business
{
    public class CriarArquivo
    {
        public void CriaArquivo()
        {
            if (!File.Exists("C:\\Arquivos_Teste\\arquivo.txt"))
            {
                int saldo = 1;
                DateTime date = DateTime.Now;

                StreamWriter sw = new StreamWriter("C:\\Arquivos_Teste\\arquivo.txt");

                for (int i = 0; i < 2; i++)
                {

                    sw.WriteLine(date.ToString() + ";" + saldo.ToString()
                        + ";" + date.Day.ToString() + ";" + date.Month.ToString()
                        + ";" + date.Year.ToString());
                    saldo++;
                    date = date.AddDays(1);
                }
                sw.Close();

            }
            CarregarArquivo();
        }

        public void CarregarArquivo()
        {
            if (File.Exists("C:\\Arquivos_Teste\\arquivo.txt"))
            {
                StreamReader sw = new StreamReader("C:\\Arquivos_Teste\\arquivo.txt");

                List<Arquivo> listArquivos = new List<Arquivo>();
                string linha;
                while ((linha = sw.ReadLine()) != null)
                {
                    var separacao = linha.Split(";");

                    listArquivos.Add(new Arquivo
                    {
                        Data = separacao[0],
                        Saldo = separacao[1],
                        Dia = separacao[2],
                        Mes = separacao[3],
                        Ano = separacao[4],
                    });
                }

                SqlConnection conn = new SqlConnection(@"Data Source=PC-LEANDRO;Integrated Security=True;Connect Timeout=30;Encrypt=False;
                TrustServerCertificate=False;ApplicationIntent=ReadWrite
                ;MultiSubnetFailover=False");

                //definição do comando sql
                string sql = "INSERT INTO DataValor(DATA_HOJE, SALDO, DIA, MES, ANO)VALUES(@data, @saldo, @dia, @mes, @ano)";


                SqlCommand command = new SqlCommand(sql, conn);
                foreach (var item in listArquivos)
                {

                    conn.Open();
                    command.Parameters.Add(new SqlParameter("@data",Convert.ToDateTime(item.Data)));
                    command.Parameters.Add(new SqlParameter("@saldo",int.Parse(item.Saldo)));
                    command.Parameters.Add(new SqlParameter("@dia", item.Dia));
                    command.Parameters.Add(new SqlParameter("@mes", item.Mes));
                    command.Parameters.Add(new SqlParameter("@ano", item.Ano));
                    command.ExecuteNonQuery();
                    conn.Close();
                }

            }
            else
            {
                CriaArquivo();
                CarregarArquivo();
            }
        }
    }
}
