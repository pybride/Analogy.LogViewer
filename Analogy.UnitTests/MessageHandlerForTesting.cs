﻿using System.Collections.Generic;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;

namespace Analogy.UnitTests
{
    class MessageHandlerForTesting : ILogMessageCreatedHandler
    {
        private List<IAnalogyLogMessage> messages;
        public bool ForceNoFileCaching { get; set; }
        public bool DoNotAddToRecentHistory { get; set; }
        public MessageHandlerForTesting()
        {
            messages = new List<IAnalogyLogMessage>();
        }
        public void AppendMessage(IAnalogyLogMessage message, string dataSource)
        {
            messages.Add(message);
        }

        public void AppendMessages(List<IAnalogyLogMessage> messages, string dataSource)
        {
            this.messages.AddRange(messages);
        }

        public void ReportFileReadProgress(AnalogyFileReadProgress progress)
        {
            //noop
        }
    }
}
