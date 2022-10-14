namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogActivity(string message);
    }
}