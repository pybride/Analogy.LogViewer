﻿using System.Windows.Forms;
using Analogy.Interfaces;
using DevExpress.XtraEditors;

namespace Analogy
{
    public partial class SearchInFilesUC : XtraUserControl
    {
        private IAnalogyOfflineDataProvider offlineAnalogy;

        public SearchInFilesUC()
        {
            InitializeComponent();
        }


        private async void sBtnSearch_Click(object sender, EventArgs e)
        {
            sBtnSearch.Enabled = false;
            var files = fileSystemUC1.GetSelectedFileNames();
            processFilesUC1.SetFilesToProcess(files);
            await processFilesUC1.ProcessFilesAndSearch(offlineAnalogy, txtbSearch.Text);
            sBtnSearch.Enabled = true;
        }

        public void SetDataSource(IAnalogyOfflineDataProvider offlineAnalogy)
        {

            this.offlineAnalogy = offlineAnalogy;
            fileSystemUC1.DataProvider = offlineAnalogy;
        }
    }
}
