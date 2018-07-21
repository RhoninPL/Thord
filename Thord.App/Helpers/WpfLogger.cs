using System;
using Thord.Core.Interfaces;

namespace Thord.App.Helpers
{
    public class WpfLogger : ILogger
    {
        #region Fields

        private readonly Action<string> _callback;

        #endregion

        #region Constructors

        public WpfLogger(Action<string> callback)
        {
            _callback = callback;
        }

        #endregion

        #region Implementation of ILogger

        public void LogInfo(object output)
        {
            var message = output as string;
            _callback?.Invoke(message);
        }

        public void LogError(object output)
        {
            var message = output as string;
            _callback?.Invoke(message);
        }

        public void LogWarning(object output)
        {
            var message = output as string;
            _callback?.Invoke(message);
        }

        #endregion
    }
}