using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sexy.Emit.Utils
{
    public class ReadOnlyListWrapper<TReference, TWrapped> : IReadOnlyList<TWrapped>
        where TReference : IReference<TWrapped>
    {
        private readonly IReadOnlyList<TReference> source;

        public ReadOnlyListWrapper(IReadOnlyList<TReference> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            this.source = source;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TWrapped> GetEnumerator()
        {
            return source.Select(x => x.Value).GetEnumerator();
        }

        public int Count => source.Count;
        public TWrapped this[int index] => source[index].Value;
    }
}
