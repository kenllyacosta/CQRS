namespace CQRSDemo.Models
{
    public class ExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
        public ExceptionEventArgs(Exception exception)
            => this.Exception = exception;
    }
}