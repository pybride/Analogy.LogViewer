﻿using System.Collections.Generic;
using Analogy.Interfaces;

namespace Analogy.DataTypes
{
    public class FolderSelectionEventArgs : EventArgs
    {
        public string SelectedFolderPath { get; }

        public FolderSelectionEventArgs(string folder)
        {
            SelectedFolderPath = folder;
        }
    }

    public class SelectionEventArgs : EventArgs
    {
        public List<string> SelectedFileNames { get; private set; }

        public SelectionEventArgs(List<string> files)
        {
            SelectedFileNames = files;

        }
    }
}
