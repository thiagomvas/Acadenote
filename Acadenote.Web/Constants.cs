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
                "Pescando ideias",
                "Tirando um cochilo rápido",
                "Contando estrelas",
                "Pensando na vida",
                "Perguntando ao Google",
                "Consultando os astros",
                "Fazendo um bolo de 5 andares",
                "Fugindo do bug",
                "Arrumando a bagunça",
                "Desligando e ligando de novo",
                "Caçando wumpus",
                "Jogando paciência",
                "Ensinando um papagaio a falar",
                "Verificando a previsão do tempo",
                "Fazendo malabarismo",
                "Viajando na maionese",
                "Contando pixels",
                "Colocando o esqueleto para dançar",
                "Fazendo amizade com os bytes",
                "Ajustando a realidade",
                "Esperando o Wi-Fi melhorar",
                "Calibrando a bússola",
                "Verificando os cabos",
                "Desenrolando o novelo de lã",
                "Brincando com o gato",
                "Tentando entender o universo",
                "Descobrindo a fórmula da diversão",
                "Devorando um livro",
                "Sonhando acordado",
                "Procurando inspiração",
                "Cuidando do jardim",
                "Resolvendo quebra-cabeças",
                "Imaginando novos mundos",
                "Explorando o espaço sideral",
                "Limpando o cache",
                "Descompactando a realidade",
                "Varrendo para debaixo do tapete",
                "Dando uma festa com os bits"
            ];


        public static string GetRandomLoadingMessage()
        {
            return LoadingMessages[rand.Next(LoadingMessages.Length)];
        }
    }
}
