using CapturaArquivoData.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapturaArquivoData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Execucao();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        public static void Execucao()
        {
            CriarArquivo criarArquivo = new CriarArquivo();
            criarArquivo.CriaArquivo();
        }

    }
}
