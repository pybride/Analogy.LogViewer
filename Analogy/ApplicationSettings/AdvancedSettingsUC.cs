﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.Interfaces;
using DevExpress.XtraEditors;

namespace Analogy.ApplicationSettings
{
    public partial class AdvancedSettingsUC : XtraUserControl
    {
        private IAnalogyUserSettings Settings { get; } = UserSettingsManager.UserSettings;

        public AdvancedSettingsUC()
        {
            InitializeComponent();
        }

        private void AdvancedSettingsUC_Load(object sender, EventArgs e)
        {
            tsEnabledAdvancedSettings.IsOn = Settings.AdvancedMode;
            tsAdvancedModeAdditionalColumns.IsOn = Settings.AdvancedModeAdditionalFilteringColumnsEnabled;
            tsAdvancedModeRawSQLFiltering.IsOn = Settings.AdvancedModeRawSQLFilterEnabled;

            tsEnabledAdvancedSettings.IsOnChanged += (s, e) => Settings.AdvancedMode = tsEnabledAdvancedSettings.IsOn;
            tsAdvancedModeAdditionalColumns.IsOnChanged += (s, e) => Settings.AdvancedModeAdditionalFilteringColumnsEnabled = tsAdvancedModeAdditionalColumns.IsOn;
            tsAdvancedModeRawSQLFiltering.IsOnChanged += (s, e) => Settings.AdvancedModeRawSQLFilterEnabled = tsAdvancedModeRawSQLFiltering.IsOn;
        }
    }
}
