
using System.Collections;
using System.Collections.Generic;

namespace WebPareser.Models {
    public class Storage<T> : IEnumerable<T> where T : IComparable {
        public List<T> Elements;

        public Storage() {
            Elements = new List<T>();
        }

        public void Add(T element) {
            if (!Elements.Any(o => o.CompareTo(element) == 0))
                Elements.Add(element);
        }

        public void AddRange(IEnumerable<T> elements) {
            foreach(var e in elements)
                if (!Elements.Any(o => o.CompareTo(e) == 0))
                    Elements.Add(e);
        }

        public bool Check(T element) {
            return Elements.Any(o => o.CompareTo(element) == 0);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}