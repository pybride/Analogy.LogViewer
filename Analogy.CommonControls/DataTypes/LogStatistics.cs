﻿using System;
using System.Collections.Generic;
using System.Linq;
using Analogy.Interfaces;

namespace Analogy.CommonControls.DataTypes
{
    public class LogStatistics
    {
        private Func<List<IAnalogyLogMessage>> MessagesSource { get; set; }
        private List<IAnalogyLogMessage> Messages => MessagesSource();
        private List<string> Sources => Messages.Select(m => m.Source).Distinct().ToList();
        private List<string> Modules => Messages.Select(m => m.Module).Distinct().ToList();
        public List<string> Texts { get; set; }= new List<string>();
        public LogStatistics(List<IAnalogyLogMessage> messages)
        {
            MessagesSource = () => messages;
        }
        //public LogStatistics(Func<List<AnalogyLogMessage>> messagesFunc)
        //{
        //    MessagesSource = messagesFunc;
        //}
        public LogAnalyzerLogLevel CalculateGlobalStatistics()
        {
            return new LogAnalyzerLogLevel("Global", Messages.Count, CountMessages(Messages, AnalogyLogLevel.Error),
                CountMessages(Messages, AnalogyLogLevel.Warning), CountMessages(Messages, AnalogyLogLevel.Critical),
                CountMessages(Messages, AnalogyLogLevel.Information), CountMessages(Messages, AnalogyLogLevel.Debug),
                CountMessages(Messages, AnalogyLogLevel.Verbose), CountMessages(Messages, AnalogyLogLevel.Trace));
        }

        public List<LogAnalyzerSingleDataPoint> CalculateTextStatistics()
        {
            var total = Messages.Count;
            List<LogAnalyzerSingleDataPoint> items = new List<LogAnalyzerSingleDataPoint>();
            //items.Add(new Statistics("Total messages", total));
            foreach (string text in Texts)
            {
                items.Add(new LogAnalyzerSingleDataPoint(text, Messages.Count(m => Contains(m.Text,text, StringComparison.InvariantCultureIgnoreCase))));
            }

            return items;
        }

        public void AddText(string text) => Texts.Add(text);
        public void ClearTexts() => Texts.Clear();
        public IEnumerable<LogAnalyzerLogLevel> CalculateModulesStatistics()
        {
            foreach (var module in Modules.Where(m => m != null))
            {
                yield return CalculateSingleStatistics(module);

            }

        }

        public LogAnalyzerLogLevel CalculateSingleStatistics(string module)
        {
            return new LogAnalyzerLogLevel(module, Messages.Count(m => module.Equals(m.Module)),
                CountModuleMessages(module, AnalogyLogLevel.Error),
                CountModuleMessages(module, AnalogyLogLevel.Warning),
                CountModuleMessages(module, AnalogyLogLevel.Critical),
                CountModuleMessages(module, AnalogyLogLevel.Information),
                CountModuleMessages(module, AnalogyLogLevel.Debug),
                CountModuleMessages(module, AnalogyLogLevel.Verbose), CountMessages(Messages, AnalogyLogLevel.Trace));

        }

        public IEnumerable<LogAnalyzerLogLevel> CalculateSourcesStatistics()
        {
            foreach (var source in Sources.Where(s => s != null))
            {
                yield return new LogAnalyzerLogLevel(source, Messages.Count(m => source.Equals(m.Source)),
                    CountSourceMessages(source, AnalogyLogLevel.Error),
                    CountSourceMessages(source, AnalogyLogLevel.Warning),
                    CountSourceMessages(source, AnalogyLogLevel.Critical),
                    CountSourceMessages(source, AnalogyLogLevel.Information),
                    CountSourceMessages(source, AnalogyLogLevel.Debug),
                    CountSourceMessages(source, AnalogyLogLevel.Verbose),
                    CountMessages(Messages, AnalogyLogLevel.Trace));
            }

        }

        private int CountMessages(List<IAnalogyLogMessage> messages, AnalogyLogLevel level) => messages.Count(m => m.Level == level);
        private int CountModuleMessages(string module, AnalogyLogLevel level) => Messages.Count(m => m.Level == level && module.Equals(m.Module));
        private int CountSourceMessages(string source, AnalogyLogLevel level) => Messages.Count(m => m.Level == level && source.Equals(m.Source));
        /// <summary>
        /// Case insensitive contains(string)
        /// </summary>
        /// <param name="source">the original string</param>
        /// <param name="toCheck">the string</param>
        /// <param name="comp">string comparison</param>
        /// <returns></returns>
        private bool Contains(string source, string toCheck, StringComparison comp)
        {
            return string.IsNullOrEmpty(toCheck) || (!string.IsNullOrEmpty(source) && source.IndexOf(toCheck, comp) >= 0);
        }
    }
}
