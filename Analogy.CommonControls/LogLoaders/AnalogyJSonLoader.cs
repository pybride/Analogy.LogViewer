﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Analogy.Interfaces;
using Newtonsoft.Json;

namespace Analogy.CommonControls.LogLoaders
{

    public class AnalogyJsonLogFile
    {

        public async Task<IEnumerable<IAnalogyLogMessage>> ReadFromFile(string fileName, CancellationToken token, ILogMessageCreatedHandler messageHandler)
        {

            if (string.IsNullOrEmpty(fileName))
            {
                AnalogyLogMessage empty = new AnalogyLogMessage($"File is null or empty. Aborting.",
                    AnalogyLogLevel.Critical, AnalogyLogClass.General, "Analogy", "None")
                {
                    Source = "Analogy",
                    Module = Process.GetCurrentProcess().ProcessName
                };
                messageHandler?.AppendMessage(empty, Utils.GetFileNameAsDataSource(fileName));
                return new List<AnalogyLogMessage>() { empty };
            }

            return await Task<IEnumerable<IAnalogyLogMessage>>.Factory.StartNew(() =>
            {
                try
                {
                    string data = string.Empty;
                    using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var textReader = new StreamReader(fileStream))
                    {
                        data = textReader.ReadToEnd();
                    }
                    List<IAnalogyLogMessage> messages = JsonConvert.DeserializeObject<List<IAnalogyLogMessage>>(data);
                    messageHandler?.AppendMessages(messages, Utils.GetFileNameAsDataSource(fileName));
                    return messages;
                }
                catch (Exception ex)
                {
                   
                    AnalogyLogMessage empty =
                        new AnalogyLogMessage($"File {fileName} is empty or corrupted. Error: {ex.Message}",
                            AnalogyLogLevel.Error, AnalogyLogClass.General, "Analogy", "None")
                        {
                            Source = "Analogy",
                            Module = Process.GetCurrentProcess().ProcessName
                        };
                    messageHandler?.AppendMessage(empty, Utils.GetFileNameAsDataSource(fileName));
                    return new List<AnalogyLogMessage>() { empty };
                }
            }, token);

        }

        public Task Save(List<IAnalogyLogMessage> messages, string fileName)
            => Task.Factory.StartNew(() =>
            {
                string json = JsonConvert.SerializeObject(messages);

                //write string to file
                File.WriteAllText(fileName, json);

            });


    }
}

