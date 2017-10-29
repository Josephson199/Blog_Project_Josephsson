

namespace Infrastructure.Interfaces
{
    public interface IOutputStrategy
    {
        string[] Transform(IBlogPost blogPost);
    }
}
