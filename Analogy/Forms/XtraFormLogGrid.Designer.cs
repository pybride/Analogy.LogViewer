﻿using Analogy.UserControls;

namespace Analogy.Forms
{
    partial class XtraFormLogGrid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Threading.CancellationTokenSource cancellationTokenSource1 = new System.Threading.CancellationTokenSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraFormLogGrid));
            this.ucLogs1 = new UCLogs();
            this.SuspendLayout();
            // 
            // ucLogs1
            // 
            this.ucLogs1.CancellationTokenSource = cancellationTokenSource1;
            this.ucLogs1.CurrentColumnsFields = ((System.Collections.Generic.List<System.ValueTuple<string, string>>)(resources.GetObject("ucLogs1.CurrentColumnsFields")));
            this.ucLogs1.DataProvider = null;
            this.ucLogs1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogs1.DoNotAddToRecentHistory = false;
            this.ucLogs1.ExcludeFilterCriteriaUIOptions = ((System.Collections.Generic.List<Analogy.CommonControls.DataTypes.FilterCriteriaUIOption>)(resources.GetObject("ucLogs1.ExcludeFilterCriteriaUIOptions")));
            this.ucLogs1.FileDataProvider = null;
            this.ucLogs1.ForceNoFileCaching = false;
            this.ucLogs1.IncludeFilterCriteriaUIOptions = ((System.Collections.Generic.List<Analogy.CommonControls.DataTypes.FilterCriteriaUIOption>)(resources.GetObject("ucLogs1.IncludeFilterCriteriaUIOptions")));
            this.ucLogs1.Location = new System.Drawing.Point(0, 0);
            this.ucLogs1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucLogs1.Name = "ucLogs1";
            this.ucLogs1.Size = new System.Drawing.Size(1200, 711);
            this.ucLogs1.TabIndex = 0;
            // 
            // XtraFormLogGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 711);
            this.Controls.Add(this.ucLogs1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "XtraFormLogGrid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analogy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XtraFormLogGrid_FormClosing);
            this.Load += new System.EventHandler(this.XtraFormLogGrid_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UCLogs ucLogs1;
    }
}