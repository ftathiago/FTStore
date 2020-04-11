namespace FTStore.Lib.Common.Services
{
    public interface IServiceBase
    {
        string GetErrorMessages();

        bool IsValid { get; }
    }
}
