namespace Advertisements.Infrastructures.Services.Contracts
{
    public interface IValidationDictionary
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
    }
}
