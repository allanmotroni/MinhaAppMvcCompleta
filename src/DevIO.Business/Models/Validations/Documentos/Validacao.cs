namespace DevIO.Business.Models.Validations.Documentos
{
    public static class CpfValidacao
    {
        public const int Tamanho = 11;

        public static bool Validar(string documento)
        {
            return true;
        }
    }

    public static class CnpjValidacao
    {
        public const int Tamanho = 14;
        public static bool Validar(string documento)
        {
            return true;
        }
    }
}
