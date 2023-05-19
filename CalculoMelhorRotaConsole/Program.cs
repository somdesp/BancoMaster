using CalculoMelhorRotaConsole.Service;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CalculoMelhorRotaConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string arg = args.Any() ? args[0] : "";

#if DEBUG
                arg = $@"{Directory.GetCurrentDirectory()}\rotas.csv";
#endif

                if (string.IsNullOrEmpty(arg) || Path.GetExtension(arg).ToLower() != ".csv")
                {
                    string name = Assembly.GetExecutingAssembly().ManifestModule.ToString().Replace(".dll", ".exe");

                    Console.WriteLine(@$"Aplicativo console deve ser inicializado com arquivo CSV nos parametros ex:
                                '{name} FILE.csv'");

                    return;
                }
                //Execução App
                AppService appService = new AppService();
                appService.ExecutaCalculoRota(arg);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
