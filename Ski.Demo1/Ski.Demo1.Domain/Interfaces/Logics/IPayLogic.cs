namespace Ski.Demo1.Domain
{
    public interface IPayLogic
    {
        object Payment(string orderNum, string username);

        void Dispose();
    }
}
