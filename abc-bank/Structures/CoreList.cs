using System;
using System.Collections.Generic;
using System.Threading;

namespace abc_bank
{
    /// <summary>
    /// Custom collection to improve transactional locking and event generation on items addition.
    /// The collection needs improvement for complex race conditions.
    /// The collection also implements an event on adding new items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CoreList<T> : List<T>
    {
        public event EventHandler OnAdd;
        public event EventHandler OnTryAdd;
        private ReaderWriterLockSlim _slimLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        // Hide base member with new.

        #region Overrides
        public new T this[int i]
        {
            get
            {
                _slimLock.TryEnterReadLock(1000);
                T _item = base[i];
                _slimLock.ExitReadLock();
                return _item;
            }
            set
            {
                _slimLock.TryEnterWriteLock(1000);
                base[i] = value;
                if(_slimLock.IsReadLockHeld) _slimLock.ExitWriteLock();
            }
        }


        public new void Add(T item)
        {
            _slimLock.TryEnterWriteLock(1000);

            base.Add(item);

            if (null != OnAdd)
            {
                OnAdd(this, null);
            }
            if (_slimLock.IsWriteLockHeld) _slimLock.ExitWriteLock();
        }

        #endregion


        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (_slimLock != null)
                    _slimLock.Dispose();
        }
        ~CoreList()
        {
            Dispose(false);
        }
        #endregion

    }
}
