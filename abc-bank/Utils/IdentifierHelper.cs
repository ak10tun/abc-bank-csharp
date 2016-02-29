using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace abc_bank
{
    public static class IdentifierHelper
    {
        private static long _NewId = 10000000001;
        private static ReaderWriterLockSlim _SlimLock = new ReaderWriterLockSlim();

        public static long NewAccountId()
        {
            /*
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int retval = BitConverter.ToInt64(buffer, 0);
            */
            _SlimLock.EnterWriteLock();
            _NewId++;
            _SlimLock.ExitWriteLock();

            return _NewId;
        }
    }
}
