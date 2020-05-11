namespace FTStore.Crosscutting.Model
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string DBHost { get; set; }
        public string DBUsuario { get; set; }
        public string DBSenha { get; set; }

        public override string ToString()
        {
            return $"Secret: {Secret}; DBHost: {DBHost}; DBUsuario: {DBUsuario}; DBSenha: {DBSenha}";
        }
    }
}