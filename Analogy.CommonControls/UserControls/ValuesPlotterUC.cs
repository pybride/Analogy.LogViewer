﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.CommonControls.DataTypes;
using Analogy.CommonControls.Plotting;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;

namespace Analogy.CommonControls.UserControls
{
    public partial class ValuesPlotterUC : UserControl, IAnalogyPlotting
    {
        private readonly AnalogyPlottingInteractor _interactor;
        private readonly List<(string SeriesName, AnalogyPlottingSeriesType SeriesViewType)> _series;
        private Func<List<IAnalogyLogMessage>> _messages;
        private PlottingUC _plottingUC;

        public ValuesPlotterUC()
        {
            _interactor = new AnalogyPlottingInteractor();
            InitializeComponent();
            ActiveColumns = new BindingList<string>();
            ColumnNames = new BindingList<string>();
            CmbColumns.DataSource = ColumnNames;
            LstPlotted.DataSource = ActiveColumns;
            _series = new List<(string SeriesName, AnalogyPlottingSeriesType SeriesViewType)>();
        }

        public BindingList<string> ColumnNames { get; set; }
        public BindingList<string> ActiveColumns { get; set; }

        public IEnumerable<(string SeriesName, AnalogyPlottingSeriesType SeriesViewType)> GetChartSeries()
        {
            return _series;
        }

        public Task InitializePlotting(IAnalogyPlottingInteractor uiInteractor, IAnalogyLogger logger)
        {
            return Task.CompletedTask;
            ;
        }

        public Task StartPlotting()
        {
            _plottingUC = new PlottingUC(this, _interactor);
            PnlPlot.Controls.Add(_plottingUC);
            _plottingUC.Dock = DockStyle.Fill;
            _plottingUC.Start();
            foreach ((string SeriesName, AnalogyPlottingSeriesType SeriesViewType) series in _series)
            {
                List<IAnalogyLogMessage> messages = _messages.Invoke().Where(i => i.AdditionalProperties != null && i.AdditionalProperties.ContainsKey(series.SeriesName)).OrderBy(i => i.Date)
                    .ToList();
                List<AnalogyPlottingPointData> points = new(messages.Count);
                foreach (IAnalogyLogMessage message in messages)
                {
                    if (Double.TryParse(message.AdditionalProperties![series.SeriesName], out double val))
                    {
                        AnalogyPlottingPointData data = new(series.SeriesName, val, message.Date);
                        points.Add(data);
                    }
                }
                OnNewPointsData?.Invoke(this, points);
            }
            return Task.CompletedTask;
            ;
        }

        public Task StopPlotting()
        {
            if (_plottingUC != null)
            {
                _plottingUC.Stop();
                PnlPlot.Controls.Remove(_plottingUC);
                _plottingUC.Dispose();
            }
            return Task.CompletedTask;
            ;
        }

        public Guid Id { get; set; }
        public Guid FactoryId { get; set; }
        public string Title { get; set; }
        public event EventHandler<AnalogyPlottingPointData>? OnNewPointData;
        public event EventHandler<List<AnalogyPlottingPointData>>? OnNewPointsData;

        public void Init(Func<List<IAnalogyLogMessage>> messagesFunc, IAnalogyLogger analogyLogger)
        {
            InitializePlotting(_interactor, analogyLogger);
            _messages = messagesFunc;
            RefreshColumnList();
        }

        private void RefreshColumnList()
        {
            List<IAnalogyLogMessage> m = _messages.Invoke();
            ColumnNames.Clear();
            IOrderedEnumerable<string> columns = m.Where(i => i.AdditionalProperties != null).SelectMany(i => i.AdditionalProperties!.Keys).Distinct().OrderBy(i => i);
            foreach (string column in columns)
                ColumnNames.Add(column);
            
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string column = CmbColumns.SelectedItem as string;
            if (column == null)
                return;
            if (!ActiveColumns.Contains(column))
                AddSeries(column);
        }

        private void AddSeries(string column)
        {
            ActiveColumns.Add(column);
            StopPlotting();
            _series.Add((column, AnalogyPlottingSeriesType.Line));
            StartPlotting();
        }

        private void RemoveSeries(string column)
        {
            ActiveColumns.Remove(column);
            StopPlotting();
            _series.Remove((column, AnalogyPlottingSeriesType.Line));
            StartPlotting();
        }

        private void BtnRefreshColumns_Click(object sender, EventArgs e)
        {
            RefreshColumnList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string? column = LstPlotted.SelectedItem as string;
            if (column != null)
            {
                RemoveSeries(column);
            }
        }

        public void AppendMessage(IAnalogyLogMessage message, string dataSource)
        {
            foreach ((string SeriesName, AnalogyPlottingSeriesType SeriesViewType) series in _series)
            {
                if (message.AdditionalProperties != null && message.AdditionalProperties.ContainsKey(series.SeriesName))
                {
                    if (Double.TryParse(message.AdditionalProperties![series.SeriesName], out double val))
                    {
                        AnalogyPlottingPointData data = new(series.SeriesName, val, message.Date);
                        OnNewPointData?.Invoke(this, data);
                    }
                }
            }
        }

        public void AppendMessages(List<IAnalogyLogMessage> messages, string dataSource)
        {
            foreach ((string SeriesName, AnalogyPlottingSeriesType SeriesViewType) series in _series)
            {
                List<IAnalogyLogMessage> messagesFiltered = messages.Where(i => i.AdditionalProperties != null && i.AdditionalProperties.ContainsKey(series.SeriesName)).OrderBy(i => i.Date)
                    .ToList();
                List<AnalogyPlottingPointData> points = new(messagesFiltered.Count);
                foreach (IAnalogyLogMessage message in messagesFiltered)
                {
                    if (Double.TryParse(message.AdditionalProperties![series.SeriesName], out double val))
                    {
                        AnalogyPlottingPointData data = new(series.SeriesName, val, message.Date);
                        points.Add(data);
                    }
                }
                OnNewPointsData?.Invoke(this, points);
            }
        }
    }
}