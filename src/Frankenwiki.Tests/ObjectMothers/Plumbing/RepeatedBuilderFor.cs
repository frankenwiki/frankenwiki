using System.Linq;

namespace Frankenwiki.Tests.ObjectMothers.Plumbing
{
    public class RepeatedBuilderFor<T>
    {
        private readonly BuilderFor<T> _builder;
        private readonly int _times;

        public RepeatedBuilderFor(BuilderFor<T> builder, int times)
        {
            _builder = builder;
            _times = times;
        }

        public T[] Build()
        {
            return Enumerable.Range(0, _times)
                .Select(_ => _builder.Build())
                .ToArray();
        }
    }
}