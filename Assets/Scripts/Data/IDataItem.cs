
namespace KOS.ItemData
{
    public interface IDataItem<in T> where T : Data
    {
        void SetData(T data);
    }
}
