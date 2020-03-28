namespace FTStore.App.Services
{
    public interface IServiceBase
    {
        string GetErrorMessages();
        bool IsValid { get; }
    }
}