using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace DevsTeam.Framework.Core
{
    public static class CollectionExtensions
    {
        public static T[] AsArray<T>(this T item) => new[] {item};

        public static IObservable<Tuple<TSource, TSource>> PairWithPrevious<TSource>(this IObservable<TSource> source) =>
            source.Scan(
                Tuple.Create(default(TSource), default(TSource)),
                (acc, current) => Tuple.Create(acc.Item2, current));

        public static void Foreach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }

        public static IEnumerable<T> WithItem<T>(this IEnumerable<T> items, T withItem)
        {
            foreach (var item in items) yield return item;
            yield return withItem;
        }

        public static IEnumerable<IReadOnlyCollection<T>> DivibeByChunks<T>(this IReadOnlyCollection<T> source, int chunkSize)
        {
            var pageNumber = 0;
            while (true)
            {
                var chunk = source.Skip(pageNumber * chunkSize).Take(chunkSize).ToArray();
                if (!chunk.Any()) yield break;
                yield return chunk;
                pageNumber++;
            }
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> fieldSelector) => source.GroupBy(fieldSelector).Select(g => g.First());
    }
}