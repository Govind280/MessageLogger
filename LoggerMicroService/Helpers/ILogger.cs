namespace LoggerMicroService.Helpers
{
    /// <summary>
    /// Logger Interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs message used for debugging purpose
        /// </summary>
        /// <param name="message">Log text</param>
        void Debug(string message);

        /// <summary>
        /// Logs message used as Information
        /// </summary>
        /// <param name="message">Log text</param>
        void Info(string message);

        /// <summary>
        /// Logs Error message
        /// </summary>
        /// <param name="message">Log text</param>
        void Error(string message);

        /// <summary>
        /// Logs message used as Warning
        /// </summary>
        /// <param name="message">Log text</param>
        void Warn(string message);
    }
}
