namespace Thord.Core.Interfaces
{
    public interface ILogger
    {
        #region Public Methods

        void LogInfo(object output);

        void LogError(object output);

        void LogWarning(object output);

        #endregion
    }
}