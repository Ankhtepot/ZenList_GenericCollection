using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GenericCollection {
    class MyCollection<T> {
        private T[] _items;
        readonly int _size;
        private string name;
        private const int BASESIZE = 5;
        //constructors
        public MyCollection() : this(BASESIZE, "") { }
        public MyCollection(string name) : this(BASESIZE, name) { }
        public MyCollection(int size, string name = "") {
            Name = name;
            this._size = size;
            _items = new T[_size];
            for (int i = 0; i < _items.Length; i++) {
                _items[i] = default(T);
            }
        }
        public MyCollection(MyCollection<T> col, string name = "") {
            Name = name;
            this._size = col.Size;
            _items = new T[_size];
            for (int i = 0; i < _items.Length; i++) {
                _items[i] = col[i];
            }
        }
        public MyCollection(T[] arr, string name = "") {
            Name = name;
            this._size = arr.Length;
            _items = new T[_size];
            for (int i = 0; i < _items.Length; i++) {
                _items[i] = arr[i];
            }
        }

        public T this[int index] {
            set => SetItem(value, index);
            get { return GetItem(index); }
        }

        public override string ToString() {
            string result = (Name == "" ? "" : Name + " ");
            Type t = this.GetType();
            Type[] typeArgs = t.GenericTypeArguments;
            foreach (Type oxxo in typeArgs) result += oxxo;

            return result;
        }

        private T GetItem(int index) {
            T item = default(T);
            int timesFound = -1;
            for (int i = 0; i < _items.Length; i++) {
                if (!EqualityComparer<T>.Default.Equals(_items[i], default(T))) {
                    timesFound++;
                    if (timesFound == index) return _items[i];
                }
            }
            return item;
        }

        private void SetItem(T value, int index) {
            while (index >= this.Length)
                _resize();
        }

        public int Length {
            get {
                int result = 0;
                for (int i = 0; i < _items.Length; i++) {
                    if (!EqualityComparer<T>.Default.Equals(_items[i], default(T))) {
                        result++;
                    }
                }
                return result;
            }
        }

        public string List() {
            string result = this.ToString() + ":";
            Console.WriteLine("Length: {0}", this.Length);
            if (this.Length > 0)
                foreach (T oxxo in this._items)
                    //if (!EqualityComparer<T>.Default.Equals(oxxo, default(T)))
                    if (oxxo != null)
                        result += String.Format("[{0}]", oxxo.ToString());
            return result;
        }
        public void Add(T item) {
            //find last free position
            int lastFreePos = -1;
            for (int i = 0; i < this.Length; i++) {
                if (!EqualityComparer<T>.Default.Equals(_items[i], default(T))) lastFreePos = i;
            }
            if (lastFreePos != -1) _items[lastFreePos] = item;
            else {
                lastFreePos = this.Length;
                _resize();
                _items[lastFreePos] = item;
            }
        }
        private void _resize() {
            T[] tempCol = new T[_items.Length + BASESIZE];
            Array.Copy(_items,tempCol,_items.Length);            
        }

        private bool _trimItems() {
            Boolean result = false;
            if (this.Length == _items.Length) return false;
            else { TidyUp(); }
            return result;
        }

        public bool TidyUp() { // returns true is tyding up happened
            Boolean result = false;
            int newLength = this.Length;
            if (newLength < BASESIZE) newLength = BASESIZE;
            T[] tempArr = new T[newLength];
            try {
                int sorted = 0;
                for (int i = 0; i < _items.Length; i++) {
                    if (!EqualityComparer<T>.Default.Equals(_items[i], default(T))) {
                        tempArr[sorted] = _items[i];
                        sorted++;
                        result = true;
                    }
                }
            } catch (Exception E) {
                Console.WriteLine(E);
            }
            _items = tempArr;
            return result;
        }

        public string Name { get => name; set => name = value; }

        public int Size => _size;

    }
}
