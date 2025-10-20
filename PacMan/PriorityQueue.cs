using System;
using System.Collections.Generic;

namespace PacMan
{
    public class PriorityQueue<T>
    {
        private readonly List<Tuple<T, int>> _elements = new List<Tuple<T, int>>();

        public int Count => _elements.Count;

        public void Enqueue(T item, int priority)
        {
            _elements.Add(Tuple.Create(item, priority));
            int i = _elements.Count - 1;
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (_elements[p].Item2 <= _elements[i].Item2)
                {
                    break;
                }
                var temp = _elements[i];
                _elements[i] = _elements[p];
                _elements[p] = temp;
                i = p;
            }
        }

        public T Dequeue()
        {
            var bestItem = _elements[0].Item1;
            _elements[0] = _elements[_elements.Count - 1];
            _elements.RemoveAt(_elements.Count - 1);

            int i = 0;
            while (true)
            {
                int l = 2 * i + 1;
                int r = 2 * i + 2;
                if (l >= _elements.Count)
                {
                    break;
                }
                int min = l;
                if (r < _elements.Count && _elements[r].Item2 < _elements[l].Item2)
                {
                    min = r;
                }
                if (_elements[i].Item2 <= _elements[min].Item2)
                {
                    break;
                }
                var temp = _elements[i];
                _elements[i] = _elements[min];
                _elements[min] = temp;
                i = min;
            }

            return bestItem;
        }
    }
}
