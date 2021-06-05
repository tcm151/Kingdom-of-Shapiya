
using KOS.ItemData;

namespace KOS.Containers
{
    public interface IContainer
    {
        bool Take(Data item);
        bool Deposit(Data item);
        bool Contains(Data item);
    }
}