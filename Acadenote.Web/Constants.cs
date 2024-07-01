namespace Acadenote.Web
{
    public static class Constants
    {
        private static readonly Random rand = new();
        public static readonly string[] LoadingMessages =
            [
                "Caçando pokemons",
                "Aguarde um momento.",
                "Carregando",
                "Fazendo café",
                "Aguarde um instante.",
                "Fazendo pipoca",
                "Fazendo bolo",
                "Aguarde um dia, talvez carregue",
                "Aguarde um mês, talvez finalmente carregue",
                "Tendo uma overdose de café",
                "Quebrando a cara com questão",
            "Estudando para prova",
            "Procrastinando",
            ];


        public static string GetRandomLoadingMessage()
        {
            return LoadingMessages[rand.Next(LoadingMessages.Length)];
        }
    }
}
