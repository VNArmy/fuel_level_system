using Microsoft.Reporting.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FuelLevelSystem.Utils;

namespace FuelLevelSystem.View.UserControls
{
    /// <summary>
    /// Interaction logic for ReportViewerUserControl.xaml
    /// </summary>
    public partial class ReportViewerUserControl : UserControl
    {
        public ReportViewerUserControl()
        {
            InitializeComponent();
            InitializeReportViewer();
        }

        private void InitializeReportViewer()
        {
            reportViewer.DocumentMapCollapsed = true;
            reportViewer.RefreshReport(); 
        }


        #region Dependency Properties

        /// <summary>
        /// Source for report data
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
            "DataSource",
            typeof(object),
            typeof(ReportViewerUserControl),
            new UIPropertyMetadata(DataSourceProperty_Changed));

        /// <summary>
        /// RDLC report added to project as Embedded Resource
        /// </summary>
        public static readonly DependencyProperty EmbeddedReportProperty = DependencyProperty.Register(
            "EmbeddedReport",
            typeof(string),
            typeof(ReportViewerUserControl),
            new UIPropertyMetadata(EmbeddedReport_Changed));


        /// <summary>
        /// Change ReportViewer's data source on DataSourceProperty change
        /// </summary>
        private static void DataSourceProperty_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = (ReportViewerUserControl)sender;
            control.reportViewer.LocalReport.DataSources.Clear();
            if (args.NewValue == null) return;

            var reportDataSource = CreateReportDataSource(args.NewValue);
            control.reportViewer.LocalReport.DataSources.Add(reportDataSource);
            control.Refresh();
        }

        /// <summary>
        /// Change ReportViewer's report on EmbeddedReport change
        /// </summary>
        private static void EmbeddedReport_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue == null) return;
            var control = (ReportViewerUserControl)sender;

            var reportEmbeddedPath = (string)args.NewValue;
            control.reportViewer.LocalReport.ReportEmbeddedResource = reportEmbeddedPath;
            control.Refresh();
        }


        /// <summary>
        /// Source for report data
        /// </summary>
        public object DataSource
        {
            get { return GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        /// <summary>
        /// RDLC report added to project as Embedded Resource
        /// </summary>
        public string EmbeddedReport
        {
            get { return (string)GetValue(EmbeddedReportProperty); }
            set { SetValue(EmbeddedReportProperty, value); }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Refresh ReportViewer
        /// </summary>
        /// <remarks>
        /// SetDisplayMode can be set only after supplying a data source
        /// </remarks>
        private void Refresh()
        {
            if (!CanBeRefreshed) return;

            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.RefreshReport();
        }

        

        /// <summary>
        /// Create new data source for a report
        /// </summary>
        /// <remarks>
        /// There are some constraint on a data source type.
        /// This method incapsulates them.
        /// </remarks>
        private static ReportDataSource CreateReportDataSource(object originalDataObject)
        {
            string name = originalDataObject.GetType().ToReportName();
            object value = originalDataObject;

            // DataTable
            if (originalDataObject is IListSource)
            {
            }
            // Collection
            if (originalDataObject is IEnumerable)
            {
                name = GetCollectionElementType(originalDataObject).ToReportName();
            }
            // Just an object 
            else
            {
                value = new ArrayList { originalDataObject };
            }

            Debug.Assert(!string.IsNullOrEmpty(name), "Data source's name must be defined ");
            Debug.Assert(value != null, "Data source must be defined");
            return new ReportDataSource(name, value);
        }

        /// <summary>
        /// Get collection element type
        /// </summary>
        /// <param name="objectDataSource">Array or generic collection</param>
        /// <returns>Type of an element</returns>
        private static Type GetCollectionElementType(object objectDataSource)
        {
            Type elementType = typeof(object);

            foreach (var item in objectDataSource.GetType().GetInterfaces())
            {
                // TODO: should only check collection's interfaces
                // NOTE: Dictionsry<T,S> will be processed wrong

                // If this is an array
                elementType = item.GetElementType();
                if (elementType != null) break;

                // If this is a generic colletion
                if (item.GetGenericArguments().Count() == 0) continue;
                elementType = item.GetGenericArguments()[0];
                if (elementType != null) break;
            }
            return elementType;
        }

        #endregion

        /// <summary>
        /// If the control can be refreshed
        /// </summary>
        private bool CanBeRefreshed
        {
            get
            {
                return reportViewer.LocalReport.DataSources.Count > 0
                    && !string.IsNullOrEmpty(reportViewer.LocalReport.ReportEmbeddedResource);
            }
        }
    }
}
