namespace BuzzCat.Lib.Base
{
    public interface IClient
    {
        void notify(string message);
        void show(string name, string message);
    }
}
