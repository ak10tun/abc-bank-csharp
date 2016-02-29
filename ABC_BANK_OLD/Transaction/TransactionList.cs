using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace abc_bank.Draft
{
    public class CoreList<T> : ConcurrentBag<T>
    {
        public event EventHandler OnAdd;

        // Hide base member with new.
        public new void Add(T item)
        {
            if (null != OnAdd)
            {
                OnAdd(this, null);
            }

            base.Add(item);
        }
    }
}