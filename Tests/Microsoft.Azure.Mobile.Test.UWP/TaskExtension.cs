﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Mobile.Test
{
    public static class TaskExtension
    {
        public static T RunNotAsync<T>(this Task<T> @this)
        {
            @this.Wait();
            return @this.Result;
        }
        public static void RunNotAsync(this Task @this)
        {
            @this.Wait();
        }
    }
}