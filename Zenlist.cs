using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*---------------------------------------------
 * NOTE - should be done, basic testing OK, would be good to do extensive testing
---------------------------------------------*/
namespace GenericCollection {
    class Zenlist<T> : IEnumerable<T>, ICollection<T> {
        private T[] _items;
        private string name;
        private const int BASESIZE = 5;
        private int _size = BASESIZE;
        private int userBase = 0;
        private Boolean isUserBase = false;
        private Boolean isInsertJump = false;
        private int lastAddedIndex = -1;

        public string Name { get => name; set => name = value; }
        public int Size => _size;
        public int UserBase { get => userBase; set { userBase = value; isUserBase = true; } }
        public bool IsReadOnly => false;
        public int Count {
            get {
                return lastAddedIndex + 1;
            }
        }

        //---------------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------------

        public Zenlist() : this("") { }

        public Zenlist(string name) {
            Name = name;
            _items = new T[_size];
            for (int i = 0; i < _items.Length; i++) {
                _items[i] = default(T);
            }
        }

        public Zenlist(int size, string name = "") {
            Name = name;
            this._size = size;
            _items = new T[_size];
            for (int i = 0; i < _items.Length; i++) {
                _items[i] = default(T);
            }
            UserBase = size; isUserBase = true;
        }

        public Zenlist(Zenlist<T> col, string name = "") {
            Console.WriteLine("zenlist constructor");
            Name = name;
            this._size = col.Count;
            _items = new T[_size];
            for (int i = 0; i < col.Count; i++) {
                if (!PM.IsSimpleType(this.GetType())) _put(default(T), i);
                if (col[i] == null) continue;
                Insert(i, col[i]);
            }
        }

        public Zenlist(ICollection<T> col, string name = "") {
            Console.WriteLine("collection constructor");
            this.Name = name;
            this._size = col.Count;
            _items = new T[_size];
            for (int i = 0; i < col.Count; i++) {
                if (PM.IsSimpleType(this.GetType())) _put(default(T), i);
                if (col.ElementAt(i) == null) continue;
                try {
                    _items[i] = col.ElementAt(i);
                    lastAddedIndex++;
                } catch (Exception e) {
                    Console.WriteLine(e);
                    continue;
                }

            }
        }

        public Zenlist(T[] arr, string name = "") {
            Console.WriteLine("arr constructor");
            Name = name;
            this._size = arr.Length;
            _items = new T[arr.Length];
            for (int i = 0; i < arr.Length; i++) {
                this.Insert(i, arr[i]);
            }
        }

        //---------------------------------------------------------------------------------------
        // Enumerators
        //---------------------------------------------------------------------------------------

        public enum MoveTo { first, last }

        //---------------------------------------------------------------------------------------
        // Public Methods
        //---------------------------------------------------------------------------------------

        public T this[int index] {
            set { _put(value,index); if (lastAddedIndex < index) lastAddedIndex = index; }
            get { return GetItem(index); }
        }

        public override string ToString() {
            string result = (Name == "" ? "" : Name + " ");
            Type t = this.GetType();
            Type[] typeArgs = t.GenericTypeArguments;
            string typeString = "";
            foreach (Type oxxo in typeArgs) typeString += oxxo;
            string[] typeStringArr = typeString.Split('.');
            return result + typeStringArr[typeStringArr.Length - 1];
        }

        private T GetItem(int index) {
            if (index > lastAddedIndex) throw new IndexOutOfRangeException();
            else return _items[index];
        }

        public void Insert(int index, T value) {
            if (index > Count) {
                _put(value, index);
                lastAddedIndex = index;
                isInsertJump = true;
            } else {
                if (Count == 0) {
                    _put(value, index);
                }
                else for (int i = Count; i > index; i--) {
                    _put(_items[i - 1], i);
                }
                _put(value, index);
                lastAddedIndex++;
            }
        }

        public void Clear() {
            for (int i = 0; i < _items.Length; i++) {
                _items[i] = default(T);
            }
            TidyUp();
            lastAddedIndex = -1;
        }

        public void RemoveAt(int index) {
            if (index >= _items.Length) throw new IndexOutOfRangeException();
            if (index == Count - 1) { Console.WriteLine("removeAt last index"); _items[index] = default(T); } else {
                for (int i = index; i < _items.Length; i++) {
                    if (i == _items.Length - 1) _items[i] = default(T);
                    else _put(_items[i + 1], i);
                }
            }
            lastAddedIndex--;

        }

        public bool Remove(T item) { //removes first occurence of an item
            for (int i = 0; i < this.Count; i++) {
                if (_items[i].Equals(item)) {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public int RemoveAll(Predicate<T> predicate) {
            int result = 0;
            for (int i = 0; i < _items.Length; i++) {
                if (predicate(_items[i])) {
                    result++;
                    this.RemoveAt(i);
                }
            }
            return result;
        }

        public bool Contains(T item) {
            for (int i = 0; i < _items.Length; i++) {
                if (_items[i].Equals(item)) return true;
            }
            return false;
        }

        public T PeekAt(int index, MoveTo movoTo = MoveTo.last) {
            if (index < _items.Length) {
                T tempItem = _items[index];
                RemoveAt(index);
                Insert(movoTo == MoveTo.first ? 0 : Count, tempItem); 
                return tempItem;
            } else {
                throw new IndexOutOfRangeException();
            }
        }

        public T[] ToArray() {
            int newLength = this.Count;
            if (isUserBase) newLength = UserBase;
            T[] resultArr = new T[newLength];
            int sorted = 0;
            for (int i = 0; i < _items.Length; i++) {
                if (!EqualityComparer<T>.Default.Equals(_items[i], default(T))) {
                    resultArr[sorted] = _items[i];
                    sorted++;
                }
            }
            return resultArr;
        }        

        public int GetCount() {
            return this.Count;
        }

        public string List() {
            string result = this.ToString() + ":";
            result += String.Format("Count: {0} ", this.Count);
            if (this.Count > 0)
                for (int i = 0; i < Count; i++) {
                    if (_items[i] != null) result += String.Format("[{0}]", _items[i].ToString());
                    else result += "[-]";
                }

            return result;
        }

        public void Add(T item) {
            if (lastAddedIndex == -1) {
                Console.WriteLine("Add \"-1\": {0}",item.ToString());
                Insert(0, item);
            } else {
                Console.WriteLine("Add else: {0}",item.ToString());
                Insert(lastAddedIndex + 1, item);
            }
        }

        public bool TidyUp() { // returns true if tyding up happened
            if (isInsertJump) return false;
            Boolean result = false;
            int newLength = this.Count;
            Console.WriteLine("TidyUp _getbase: {0}.", _getBase());
            if (newLength < _getBase()) newLength = _getBase();
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

        //---------------------------------------------------------------------------------------
        // Private Methods
        //---------------------------------------------------------------------------------------

        private void _put(T value, int index) {
            if (index > Count) isInsertJump = true;
            while (index >= _items.Length)
                _resize();
            if (value != null)
                _items[index] = value;
        }

        private void _resize() {
            T[] tempCol = new T[_items.Length + BASESIZE];
            for (int i = 0; i < _items.Length; i++) tempCol[i] = _items[i];
            _items = tempCol;
        }

        private int _getBase() {
            return lastAddedIndex;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<T>)_items).GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            int position = 0; // state
            while (position <= lastAddedIndex) {
                position++;
                if (_items[position - 1] == null) continue;
                yield return _items[position - 1];
            }
        }

        public void CopyTo(T[] array, int arrayIndex) {
            for (int i = arrayIndex; i < Count; i++) {
                if (array[i] == null) continue;
                array[i] = _items[i];
            }
        }        
    }
}
