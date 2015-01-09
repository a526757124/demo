using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace WpfApplication1
{
    /// <summary>
    /// 编辑集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IActionList<T> : IList<T>
    {
        /// <summary>
        /// 
        /// </summary>
        Action<T> AddItemAction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Func<T, bool> RemoveItemAction { get; set; }
        /// <summary>
        /// 添加的集合
        /// </summary>
        IList<T> AddList { get; }
        /// <summary>
        /// 移除的集合
        /// </summary>
        IList<T> RemoveList { get; }
        /// <summary>
        /// 修改的集合
        /// </summary>
        IList<T> EditList { get; }
    }

    /// <summary>
    /// 编辑集合
    /// </summary>
    public interface IActionList : IList
    {
        /// <summary>
        /// 
        /// </summary>
        Action<object> AddItemAction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Func<object, bool> RemoveItemAction { get; set; }
        /// <summary>
        /// 添加的集合
        /// </summary>
        IList AddList { get; }
        /// <summary>
        /// 移除的集合
        /// </summary>
        IList RemoveList { get; }
        /// <summary>
        /// 修改的集合
        /// </summary>
        IList EditList { get; }
    }
    /// <summary>
    /// 编辑集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionList<T> : IActionList<T>, IActionList, INotifyCollectionChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public ActionList()
        {
            items = new List<T>();
            _addList = new List<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection">编辑集合</param>
        public ActionList(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            var item =  new List<T>(collection);
            _editList = new List<T>(collection);
            items = item;

            _addList = new List<T>();
            _removeList = new List<T>();
        }
        #region IActionList

        public Action<T> AddItemAction { get; set; }
        public Func<T, bool> RemoveItemAction { get; set; }
        private List<T> items;

        private List<T> _addList;
        private List<T> _removeList;
        private List<T> _editList;
        public IList<T> AddList
        {
            get { return _addList; }

        }

        public IList<T> RemoveList
        {
            get { return _removeList; }

        }

        public IList<T> EditList
        {
            get { return _editList; }

        }

        Action<object> IActionList.AddItemAction
        {
            get
            {
                if (this.AddItemAction != null)
                    return o => AddItemAction((T)o);
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.AddItemAction = o => value(o);
                }
                else
                {
                    AddItemAction = null;

                }
            }
        }

        Func<object, bool> IActionList.RemoveItemAction
        {
            get
            {
                if (this.RemoveItemAction != null)
                    return o => RemoveItemAction((T)o);
                return null;

            }
            set
            {
                if (value != null)
                {
                    this.RemoveItemAction = o => value(o);
                }
                else
                {
                    RemoveItemAction = null;
                }
            }
        }

        IList IActionList.AddList
        {
            get { return this._addList; }
        }

        IList IActionList.RemoveList
        {
            get { return this._removeList; }
        }

        IList IActionList.EditList
        {
            get { return this._editList; }
        }

        #endregion

        #region  IList[T]



        public bool IsReadOnly
        {
            get { return true; }
        }

        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            BaseAdd(index, item);

        }



        public void RemoveAt(int index)
        {
            if (index == -1 || index >= items.Count)
                return;
            var item = items[index];

            Remove(item);
        }

        public T this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }

        public void Add(T item)
        {
            BaseAdd(items.Count + 1, item);
            //  OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void Clear()
        {
            if (_addList != null)
                _addList.Clear();
            if (_editList != null) 
                _editList.Clear();  
            if (_removeList != null)
                _removeList.Clear();
            items.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var index= items.IndexOf(item);
            if (index == -1)
                return false;
            if (RemoveItemAction != null)
            {
                if (!RemoveItemAction(item))
                    return false;
            }
            var flag= BaseRemove(item);
            if (flag)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            return flag;
        }



        #endregion

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        #region this
#if !SILVERLIGHT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newindx"></param>
        public void Move(int index, int newindx)
        {
            if (index == newindx)
                return;
            var item = items[index];
            items.RemoveAt(index);
            var moveindex = newindx;
            if (index < newindx)
                moveindex = newindx - index;
            items.Insert(moveindex, item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, item, moveindex, index));
            // OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="newindx"></param>
        public void MoveRange(int index, int count, int newindx)
        {
            if (index == newindx)
                return;
            T[] arr=new T[count];
            items.CopyTo(index, arr, 0, count);
            items.RemoveRange(index, count);
            var moveindex = newindx;
            if (index < newindx)
                moveindex = newindx - index;
            items.InsertRange(moveindex, arr);
            for (int i = 0; i < arr.Length; i++)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, arr[i], moveindex + i, index + i));
            }


            // OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }
#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(IEnumerable<T> collection)
        {
            int index = items.Count;
            items.AddRange(collection);

            if (_addList != null)
                _addList.AddRange(collection);
            foreach (var variable in collection)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, variable, index));
                index++;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Replace(int index, T item)
        {
            var old = items[index];
            items[index] = item;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, old, index));




        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void ReomveRange(int index, int count)
        {
            T[] arr=new T[count];
            items.CopyTo(index, arr, 0, count);
            for (int i = 0; i < count; i++)
            {
                BaseRemove(arr[i]);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, arr[i], index));
                index++;
            }



        }

        private void BaseAdd(int index, T item)
        {
            if (AddItemAction != null)
                AddItemAction(item);
            if (items.Count == index - 1)
                items.Add(item);
            else
                items.Insert(index, item);
            AddList.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }
        private bool BaseRemove(T item)
        {
            if (_addList != null)
            {
                var index = _addList.IndexOf(item);
                if (index != -1)
                    _addList.Remove(item);

            }
            if (_editList != null)
            {
                var  index = _editList.IndexOf(item);
                if (index != -1)
                {
                    _editList.Remove(item);
                    if (_removeList != null)
                        _removeList.Add(item);
                }
            }
            return items.Remove(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return new ReadOnlyCollection<T>(this);
        }
        #endregion

        #region IList


        public int Count
        {
            get { return items.Count; }
        }

        int IList.Add(object value)
        {
            if (value is IEnumerable<T>)
            {

                this.AddRange((IEnumerable<T>)value);
                return items.Count - 1;
            }
            if (value is T)
            {
                Add((T)value);
                return items.Count - 1;
            }
            return -1;
        }

        bool IList.Contains(object value)
        {
            return items.Contains((T)value);
        }

        int IList.IndexOf(object value)
        {
            if (value is IEnumerable<T>)
            {
                var ienum = (IEnumerable<T>)value;
                var tor= ienum.GetEnumerator();
                if (tor.MoveNext())
                    return items.IndexOf(tor.Current);
            }
            if (value is T)
                return items.IndexOf((T)value);
            return -1;
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        bool IList.IsFixedSize
        {
            get { return ((IList)items).IsFixedSize; }
        }

        void IList.Remove(object value)
        {
            Remove((T)value);
        }

        object IList.this[int index]
        {
            get { return items[index]; }
            set { items[index] = (T)value; }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)items).CopyTo(array, index);
        }

        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)items).IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return ((ICollection)items).SyncRoot; }
        }


        #endregion

        #region INotifyCollectionChanged

        /// <summary>
        /// 
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler handler = CollectionChanged;
            if (handler != null)
                handler(this, e);
        }

        #endregion






    }
}