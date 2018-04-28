using Grid;

namespace Merge
{
    public interface IMergable
    {
        int Id { get; set; }
        void Merge(ICollidible targetItem);
    }
}
