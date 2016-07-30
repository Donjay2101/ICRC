﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Infrastructure
{
    public class Disposable:IDisposable
    {
        private bool IsDisposable;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if(!IsDisposable && disposing)
            {
                DisposeCore();
            }
            IsDisposable = true;
        }

        protected virtual void DisposeCore() { }

    }
}