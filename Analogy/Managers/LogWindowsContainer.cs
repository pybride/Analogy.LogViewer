﻿using Analogy.Interfaces;
using System.Collections.Generic;
using Analogy.Common.Interfaces;
using Analogy.CommonControls.Interfaces;

namespace Analogy.Managers
{
    internal class LogWindowsContainer
    {
        private static readonly Lazy<LogWindowsContainer> instance =
            new Lazy<LogWindowsContainer>(() => new LogWindowsContainer());

        public static LogWindowsContainer Instance => instance.Value;

        private List<ILogWindow> Windows { get; set; } = new List<ILogWindow>();

        
        public void Register(ILogWindow windows) => Windows.Add(windows);
        public void UnRegister(ILogWindow windows) => Windows.Remove(windows);
    }
}
