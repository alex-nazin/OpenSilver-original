﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

#if MIGRATION
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
#else
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IGridProvider.ColumnCount", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IGridProvider.GetItem(System.Int32,System.Int32)", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IGridProvider.RowCount", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IScrollProvider.HorizontallyScrollable", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IScrollProvider.HorizontalScrollPercent", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IScrollProvider.HorizontalViewSize", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IScrollProvider.Scroll(System.Windows.Automation.ScrollAmount,System.Windows.Automation.ScrollAmount)", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IScrollProvider.SetScrollPercent(System.Double,System.Double)", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IScrollProvider.VerticallyScrollable", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IScrollProvider.VerticalScrollPercent", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.IScrollProvider.VerticalViewSize", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.ISelectionProvider.CanSelectMultiple", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.ISelectionProvider.GetSelection()", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.ISelectionProvider.IsSelectionRequired", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.ITableProvider.GetColumnHeaders()", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.ITableProvider.GetRowHeaders()", Justification = "Base functionality is available through the GetPattern method.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "System.Windows.Automation.Peers.DataGridAutomationPeer.#System.Windows.Automation.Provider.ITableProvider.RowOrColumnMajor", Justification = "Base functionality is available through the GetPattern method.")]
#if MIGRATION
namespace System.Windows.Automation.Peers
#else
namespace Windows.UI.Xaml.Automation.Peers
#endif
{
    /// <summary>
    /// Exposes <see cref="T:System.Windows.Controls.DataGrid" /> types to UI Automation.
    /// </summary>
    /// <QualityBand>Mature</QualityBand>
    public class DataGridAutomationPeer : FrameworkElementAutomationPeer,
        IGridProvider, IScrollProvider, ISelectionProvider, ITableProvider
    {
#region Data

        private Dictionary<object, DataGridGroupItemAutomationPeer> _groupItemPeers = new Dictionary<object, DataGridGroupItemAutomationPeer>();
        private Dictionary<object, DataGridItemAutomationPeer> _itemPeers = new Dictionary<object, DataGridItemAutomationPeer>();
        private bool _oldHorizontallyScrollable;
        private double _oldHorizontalScrollPercent;
        private double _oldHorizontalViewSize;
        private bool _oldVerticallyScrollable;
        private double _oldVerticalScrollPercent;
        private double _oldVerticalViewSize;

#endregion

#region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Automation.Peers.DataGridAutomationPeer" /> class.
        /// </summary>
        /// <param name="owner">
        /// The <see cref="T:System.Windows.Controls.DataGrid" /> that is associated with this <see cref="T:System.Windows.Automation.Peers.DataGridAutomationPeer" />.
        /// </param>
        public DataGridAutomationPeer(DataGrid owner)
            : base(owner)
        {
            if (this.HorizontallyScrollable)
            {
                _oldHorizontallyScrollable = true;
                _oldHorizontalScrollPercent = this.HorizontalScrollPercent;
                _oldHorizontalViewSize = this.HorizontalViewSize;
            }
            else
            {
                _oldHorizontallyScrollable = false;
                _oldHorizontalScrollPercent = ScrollPatternIdentifiers.NoScroll;
                _oldHorizontalViewSize = 100.0;
            }

            if (this.VerticallyScrollable)
            {
                _oldVerticallyScrollable = true;
                _oldVerticalScrollPercent = this.VerticalScrollPercent;
                _oldVerticalViewSize = this.VerticalViewSize;
            }
            else
            {
                _oldVerticallyScrollable = false;
                _oldVerticalScrollPercent = ScrollPatternIdentifiers.NoScroll;
                _oldVerticalViewSize = 100.0;
            }
        }

#endregion

#region Properties

        private bool HorizontallyScrollable
        {
            get
            {
                return (OwningDataGrid.HorizontalScrollBar != null && OwningDataGrid.HorizontalScrollBar.Maximum > 0);
            }
        }

        private double HorizontalScrollPercent
        {
            get
            {
                if (!this.HorizontallyScrollable)
                {
                    return ScrollPatternIdentifiers.NoScroll;
                }
                return (double)(this.OwningDataGrid.HorizontalScrollBar.Value * 100.0 / this.OwningDataGrid.HorizontalScrollBar.Maximum);
            }
        }

        private double HorizontalViewSize
        {
            get
            {
                if (!this.HorizontallyScrollable || DoubleUtil.IsZero(this.OwningDataGrid.HorizontalScrollBar.Maximum))
                {
                    return 100.0;
                }
                return (double)(this.OwningDataGrid.HorizontalScrollBar.ViewportSize * 100.0 /
                    (this.OwningDataGrid.HorizontalScrollBar.ViewportSize + this.OwningDataGrid.HorizontalScrollBar.Maximum));
            }
        }

        private DataGrid OwningDataGrid
        {
            get
            {
                return (DataGrid)Owner;
            }
        }

        private bool VerticallyScrollable
        {
            get
            {
                return (OwningDataGrid.VerticalScrollBar != null && OwningDataGrid.VerticalScrollBar.Maximum > 0);
            }
        }

        private double VerticalScrollPercent
        {
            get
            {
                if (!this.VerticallyScrollable)
                {
                    return ScrollPatternIdentifiers.NoScroll;
                }
                return (double)(this.OwningDataGrid.VerticalScrollBar.Value * 100.0 / this.OwningDataGrid.VerticalScrollBar.Maximum);
            }
        }

        private double VerticalViewSize
        {
            get
            {
                if (!this.VerticallyScrollable || DoubleUtil.IsZero(this.OwningDataGrid.VerticalScrollBar.Maximum))
                {
                    return 100.0;
                }
                return (double)(this.OwningDataGrid.VerticalScrollBar.ViewportSize * 100.0 /
                    (this.OwningDataGrid.VerticalScrollBar.ViewportSize + this.OwningDataGrid.VerticalScrollBar.Maximum));
            }
        }

#endregion

#region AutomationPeer Overrides

        /// <summary>
        /// Gets the control type for the element that is associated with the UI Automation peer.
        /// </summary>
        /// <returns>The control type.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.DataGrid;
        }

        /// <summary>
        /// Gets the collection of elements that are represented in the UI Automation tree as immediate
        /// child elements of the automation peer.
        /// </summary>
        /// <returns>The children elements.</returns>
#if SILVERLIGHT
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required method signature for automation peers")]
#endif
        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> children = base.GetChildrenCore();
            if (this.OwningDataGrid != null)
            {
                children.Remove(ScrollBarAutomationPeer.FromElement(this.OwningDataGrid.HorizontalScrollBar));
                children.Remove(ScrollBarAutomationPeer.FromElement(this.OwningDataGrid.VerticalScrollBar));
            }
            return children;
        }

        /// <summary>
        /// Called by GetClassName that gets a human readable name that, in addition to AutomationControlType, 
        /// differentiates the control represented by this AutomationPeer.
        /// </summary>
        /// <returns>The string that contains the name.</returns>
        protected override string GetClassNameCore()
        {
            return Owner.GetType().Name;
        }

        /// <summary>
        /// Called by GetName.
        /// </summary>
        /// <returns>
        /// Returns the first of these that is not null or empty:
        /// - Value returned by the base implementation
        /// - Name of the owning DataGrid
        /// - DataGrid class name
        /// </returns>
        protected override string GetNameCore()
        {
            string name = base.GetNameCore();
            if (string.IsNullOrEmpty(name))
            {
                if (this.OwningDataGrid != null)
                {
                    name = this.OwningDataGrid.Name;
                }
                if (string.IsNullOrEmpty(name))
                {
                    name = this.GetClassName();
                }
            }
            return name;
        }

        /// <summary>
        /// Gets the control pattern that is associated with the specified System.Windows.Automation.Peers.PatternInterface.
        /// </summary>
        /// <param name="patternInterface">A value from the System.Windows.Automation.Peers.PatternInterface enumeration.</param>
        /// <returns>The object that supports the specified pattern, or null if unsupported.</returns>
        public override object GetPattern(PatternInterface patternInterface)
        {
            switch (patternInterface)
            {
                case PatternInterface.Grid:
                case PatternInterface.Selection:
                case PatternInterface.Table:
                    return this;
                case PatternInterface.Scroll:
                    {
                        if (this.HorizontallyScrollable || this.VerticallyScrollable)
                        {
                            return this;
                        }
                        break;
                    }

            }
            return base.GetPattern(patternInterface);
        }

#endregion

#region IGridProvider

        int IGridProvider.ColumnCount
        {
            get
            {
                return this.OwningDataGrid.Columns.Count;
            }
        }

        int IGridProvider.RowCount
        {
            get
            {
                return this.OwningDataGrid.DataConnection.Count;
            }
        }

        IRawElementProviderSimple IGridProvider.GetItem(int row, int column)
        {
            if (this.OwningDataGrid != null &&
                this.OwningDataGrid.DataConnection != null &&
                row >= 0 && row < this.OwningDataGrid.SlotCount &&
                column >= 0 && column < this.OwningDataGrid.Columns.Count)
            {
                object item = null;
                if (!this.OwningDataGrid.IsSlotVisible(this.OwningDataGrid.SlotFromRowIndex(row)))
                {
                    item = this.OwningDataGrid.DataConnection.GetDataItem(row);
                }
                this.OwningDataGrid.ScrollIntoView(item, this.OwningDataGrid.Columns[column]);

                DataGridRow dgr = this.OwningDataGrid.DisplayData.GetDisplayedRow(row);
                if (this.OwningDataGrid.ColumnsInternal.RowGroupSpacerColumn.IsRepresented)
                {
                    column++;
                }
                Debug.Assert(column >= 0);
                Debug.Assert(column < this.OwningDataGrid.ColumnsItemsInternal.Count);
                DataGridCell cell = dgr.Cells[column];
                AutomationPeer peer = CreatePeerForElement(cell);
                if (peer != null)
                {
                    return ProviderFromPeer(peer);
                }
            }
            return null;
        }

#endregion

#region IScrollProvider

        bool IScrollProvider.HorizontallyScrollable
        {
            get
            {
                return this.HorizontallyScrollable;
            }
        }

        double IScrollProvider.HorizontalScrollPercent
        {
            get
            {
                return this.HorizontalScrollPercent;
            }
        }

        double IScrollProvider.HorizontalViewSize
        {
            get
            {
                return this.HorizontalViewSize;
            }
        }

        bool IScrollProvider.VerticallyScrollable
        {
            get
            {
                return this.VerticallyScrollable;
            }
        }

        double IScrollProvider.VerticalScrollPercent
        {
            get
            {
                return this.VerticalScrollPercent;
            }
        }

        double IScrollProvider.VerticalViewSize
        {
            get
            {
                return this.VerticalViewSize;
            }
        }

        void IScrollProvider.Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            bool scrollHorizontally = (horizontalAmount != ScrollAmount.NoAmount);
            bool scrollVertically = (verticalAmount != ScrollAmount.NoAmount);

            if (scrollHorizontally && !this.HorizontallyScrollable || scrollVertically && !this.VerticallyScrollable)
            {
                // 



                return;
            }

            switch (horizontalAmount)
            {
                // In the small increment and decrement calls, ScrollHorizontally will adjust the
                // ScrollBar.Value itself, so we don't need to do it here
                case ScrollAmount.SmallIncrement:
                    this.OwningDataGrid.ProcessHorizontalScroll(ScrollEventType.SmallIncrement);
                    break;
                case ScrollAmount.LargeIncrement:
                    this.OwningDataGrid.HorizontalScrollBar.Value += this.OwningDataGrid.HorizontalScrollBar.LargeChange;
                    this.OwningDataGrid.ProcessHorizontalScroll(ScrollEventType.LargeIncrement);
                    break;
                case ScrollAmount.SmallDecrement:
                    this.OwningDataGrid.ProcessHorizontalScroll(ScrollEventType.SmallDecrement);
                    break;
                case ScrollAmount.LargeDecrement:
                    this.OwningDataGrid.HorizontalScrollBar.Value -= this.OwningDataGrid.HorizontalScrollBar.LargeChange;
                    this.OwningDataGrid.ProcessHorizontalScroll(ScrollEventType.LargeDecrement);
                    break;
                case ScrollAmount.NoAmount:
                    break;
                default:
                    // 



                    return;
            }

            switch (verticalAmount)
            {
                // In the small increment and decrement calls, ScrollVertically will adjust the
                // ScrollBar.Value itself, so we don't need to do it here
                case ScrollAmount.SmallIncrement:
                    this.OwningDataGrid.ProcessVerticalScroll(ScrollEventType.SmallIncrement);
                    break;
                case ScrollAmount.LargeIncrement:
                    this.OwningDataGrid.VerticalScrollBar.Value += this.OwningDataGrid.VerticalScrollBar.LargeChange;
                    this.OwningDataGrid.ProcessVerticalScroll(ScrollEventType.LargeIncrement);
                    break;
                case ScrollAmount.SmallDecrement:
                    this.OwningDataGrid.ProcessVerticalScroll(ScrollEventType.SmallDecrement);
                    break;
                case ScrollAmount.LargeDecrement:
                    this.OwningDataGrid.VerticalScrollBar.Value -= this.OwningDataGrid.VerticalScrollBar.LargeChange;
                    this.OwningDataGrid.ProcessVerticalScroll(ScrollEventType.LargeDecrement);
                    break;
                case ScrollAmount.NoAmount:
                    break;
                default:
                    // 



                    return;
            }
        }

        void IScrollProvider.SetScrollPercent(double horizontalPercent, double verticalPercent)
        {

            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }

            bool scrollHorizontally = (horizontalPercent != (double)ScrollPatternIdentifiers.NoScroll);
            bool scrollVertically = (verticalPercent != (double)ScrollPatternIdentifiers.NoScroll);

            if (scrollHorizontally && !this.HorizontallyScrollable || scrollVertically && !this.VerticallyScrollable)
            {
                // 



                return;
            }

            if (scrollHorizontally && (horizontalPercent < 0.0) || (horizontalPercent > 100.0))
            {
                // 



                return;
            }
            if (scrollVertically && (verticalPercent < 0.0) || (verticalPercent > 100.0))
            {
                // 



                return;
            }

            if (scrollHorizontally)
            {
                this.OwningDataGrid.HorizontalScrollBar.Value =
                    (double)(this.OwningDataGrid.HorizontalScrollBar.Maximum * (horizontalPercent / 100.0));
                this.OwningDataGrid.ProcessHorizontalScroll(ScrollEventType.ThumbPosition);
            }
            if (scrollVertically)
            {
                this.OwningDataGrid.VerticalScrollBar.Value =
                    (double)(this.OwningDataGrid.VerticalScrollBar.Maximum * (verticalPercent / 100.0));
                this.OwningDataGrid.ProcessVerticalScroll(ScrollEventType.ThumbPosition);
            }
        }

#endregion

#region ISelectionProvider

        IRawElementProviderSimple[] ISelectionProvider.GetSelection()
        {
            if (this.OwningDataGrid != null &&
                this.OwningDataGrid.SelectedItems != null)
            {
                List<IRawElementProviderSimple> selectedProviders = new List<IRawElementProviderSimple>();
                foreach (object item in this.OwningDataGrid.SelectedItems)
                {
                    DataGridItemAutomationPeer peer = GetOrCreateItemPeer(item);
                    if (peer != null)
                    {
                        selectedProviders.Add(ProviderFromPeer(peer));
                    }
                }
                return selectedProviders.ToArray();
            }
            return null;
        }

        bool ISelectionProvider.CanSelectMultiple
        {
            get
            {
                return (this.OwningDataGrid.SelectionMode == DataGridSelectionMode.Extended);
            }
        }

        bool ISelectionProvider.IsSelectionRequired
        {
            get
            {
                return false;
            }
        }

#endregion

#region ITableProvider

        RowOrColumnMajor ITableProvider.RowOrColumnMajor
        {
            get
            {
                return RowOrColumnMajor.RowMajor;
            }
        }

        IRawElementProviderSimple[] ITableProvider.GetColumnHeaders()
        {
            if (this.OwningDataGrid.AreColumnHeadersVisible)
            {
                List<IRawElementProviderSimple> providers = new List<IRawElementProviderSimple>();
                foreach (DataGridColumn column in this.OwningDataGrid.Columns)
                {
                    if (column.HeaderCell != null)
                    {
                        AutomationPeer peer = CreatePeerForElement(column.HeaderCell);
                        if (peer != null)
                        {
                            providers.Add(ProviderFromPeer(peer));
                        }
                    }
                }
                if (providers.Count > 0)
                {
                    return providers.ToArray();
                }
            }
            return null;
        }

        IRawElementProviderSimple[] ITableProvider.GetRowHeaders()
        {
            if (this.OwningDataGrid.AreRowHeadersVisible)
            {
                List<IRawElementProviderSimple> providers = new List<IRawElementProviderSimple>();
                foreach (DataGridRow row in this.OwningDataGrid.DisplayData.GetScrollingRows())
                {
                    if (row.HeaderCell != null)
                    {
                        AutomationPeer peer = CreatePeerForElement(row.HeaderCell);
                        if (peer != null)
                        {
                            providers.Add(ProviderFromPeer(peer));
                        }
                    }
                }
                if (providers.Count > 0)
                {
                    return providers.ToArray();
                }
            }
            return null;
        }

#endregion

#region Methods

        private AutomationPeer GetCellPeer(int slot, int column)
        {
            if (slot >= 0 && slot < this.OwningDataGrid.SlotCount &&
                column >= 0 && column < this.OwningDataGrid.ColumnsItemsInternal.Count &&
                this.OwningDataGrid.IsSlotVisible(slot))
            {
                DataGridRow row = this.OwningDataGrid.DisplayData.GetDisplayedElement(slot) as DataGridRow;
                if (row != null)
                {
                    Debug.Assert(column >= 0);
                    Debug.Assert(column < this.OwningDataGrid.ColumnsItemsInternal.Count);
                    DataGridCell cell = row.Cells[column];
                    return CreatePeerForElement(cell);
                }
            }
            return null;
        }

        internal List<AutomationPeer> GetChildPeers()
        {
            List<AutomationPeer> peers = new List<AutomationPeer>();
            PopulateGroupItemPeers();
            PopulateItemPeers();
            if (_groupItemPeers != null && _groupItemPeers.Count > 0)
            {
                foreach (object group in this.OwningDataGrid.DataConnection.CollectionView.Groups)
                {
                    peers.Add(_groupItemPeers[group]);
                }
            }
            else
            {
                foreach (DataGridItemAutomationPeer itemPeer in _itemPeers.Values)
                {
                    peers.Add(itemPeer);
                }
            }
            return peers;
        }

        internal DataGridGroupItemAutomationPeer GetOrCreateGroupItemPeer(object group)
        {
            DataGridGroupItemAutomationPeer peer = null;

            if (group != null)
            {
                if (_groupItemPeers.ContainsKey(group))
                {
                    peer = _groupItemPeers[group];
                }
                else
                {
                    peer = new DataGridGroupItemAutomationPeer(group as CollectionViewGroup, this.OwningDataGrid);
                    _groupItemPeers.Add(group, peer);
                }

                DataGridRowGroupHeaderAutomationPeer rghPeer = peer.OwningRowGroupHeaderPeer;
                if (rghPeer != null)
                {
                    rghPeer.EventsSource = peer;
                }
            }

            return peer;
        }

        internal DataGridItemAutomationPeer GetOrCreateItemPeer(object item)
        {
            DataGridItemAutomationPeer peer = null;

            if (item != null)
            {
                if (_itemPeers.ContainsKey(item))
                {
                    peer = _itemPeers[item];
                }
                else
                {
                    peer = new DataGridItemAutomationPeer(item, this.OwningDataGrid);
                    _itemPeers.Add(item, peer);
                }

                DataGridRowAutomationPeer rowPeer = peer.OwningRowPeer;
                if (rowPeer != null)
                {
                    rowPeer.EventsSource = peer;
                }
            }

            return peer;
        }

        internal void PopulateGroupItemPeers()
        {
            Dictionary<object, DataGridGroupItemAutomationPeer> oldChildren = new Dictionary<object, DataGridGroupItemAutomationPeer>(_groupItemPeers);
            _groupItemPeers.Clear();

            if (this.OwningDataGrid.DataConnection.CollectionView != null
                && this.OwningDataGrid.DataConnection.CollectionView.CanGroup
                && this.OwningDataGrid.DataConnection.CollectionView.Groups != null
                && this.OwningDataGrid.DataConnection.CollectionView.Groups.Count > 0)
            {
                List<object> groups = new List<object>(this.OwningDataGrid.DataConnection.CollectionView.Groups);
                while (groups.Count > 0)
                {
                    CollectionViewGroup cvGroup = groups[0] as CollectionViewGroup;
                    groups.RemoveAt(0);
                    if (cvGroup != null)
                    {
                        // Add the group's peer to the collection
                        DataGridGroupItemAutomationPeer peer = null;

                        if (oldChildren.ContainsKey(cvGroup))
                        {
                            peer = oldChildren[cvGroup] as DataGridGroupItemAutomationPeer;
                        }
                        else
                        {
                            peer = new DataGridGroupItemAutomationPeer(cvGroup, this.OwningDataGrid);
                        }

                        if (peer != null)
                        {
                            DataGridRowGroupHeaderAutomationPeer rghPeer = peer.OwningRowGroupHeaderPeer;
                            if (rghPeer != null)
                            {
                                rghPeer.EventsSource = peer;
                            }
                        }

                        // This guards against the addition of duplicate items
                        if (!_groupItemPeers.ContainsKey(cvGroup))
                        {
                            _groupItemPeers.Add(cvGroup, peer);
                        }

                        // Look for any sub groups
                        if (!cvGroup.IsBottomLevel)
                        {
                            int position = 0;
                            foreach (object subGroup in cvGroup.Items)
                            {
                                groups.Insert(position, subGroup);
                                position++;
                            }
                        }
                    }
                }
            }
        }

        internal void PopulateItemPeers()
        {
            Dictionary<object, DataGridItemAutomationPeer> oldChildren = new Dictionary<object, DataGridItemAutomationPeer>(_itemPeers);
            _itemPeers.Clear();

            if (this.OwningDataGrid.ItemsSource != null)
            {
                foreach (object item in this.OwningDataGrid.ItemsSource)
                {
                    if (item != null)
                    {
                        DataGridItemAutomationPeer peer = null;

                        if (oldChildren.ContainsKey(item))
                        {
                            peer = oldChildren[item] as DataGridItemAutomationPeer;
                        }
                        else
                        {
                            peer = new DataGridItemAutomationPeer(item, this.OwningDataGrid);
                        }

                        if (peer != null)
                        {
                            DataGridRowAutomationPeer rowPeer = peer.OwningRowPeer;
                            if (rowPeer != null)
                            {
                                rowPeer.EventsSource = peer;
                            }
                        }

                        // This guards against the addition of duplicate items
                        if (!_itemPeers.ContainsKey(item))
                        {
                            _itemPeers.Add(item, peer);
                        }
                    }
                }
            }
        }

        internal void RaiseAutomationCellSelectedEvent(int slot, int column)
        {
            AutomationPeer cellPeer = GetCellPeer(slot, column);
            if (cellPeer != null)
            {
                cellPeer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementSelected);
            }
        }

        internal void RaiseAutomationFocusChangedEvent(int slot, int column)
        {
            if (slot >= 0 && slot < this.OwningDataGrid.SlotCount &&
                column >= 0 && column < this.OwningDataGrid.ColumnsItemsInternal.Count &&
                this.OwningDataGrid.IsSlotVisible(slot))
            {
                if (this.OwningDataGrid.RowGroupHeadersTable.Contains(slot))
                {
                    DataGridRowGroupHeader header = this.OwningDataGrid.DisplayData.GetDisplayedElement(slot) as DataGridRowGroupHeader;
                    if (header != null)
                    {
                        AutomationPeer headerPeer = CreatePeerForElement(header);
                        if (headerPeer != null)
                        {
                            headerPeer.RaiseAutomationEvent(AutomationEvents.AutomationFocusChanged);
                        }
                    }
                }
                else
                {
                    AutomationPeer cellPeer = GetCellPeer(slot, column);
                    if (cellPeer != null)
                    {
                        cellPeer.RaiseAutomationEvent(AutomationEvents.AutomationFocusChanged);
                    }
                }
            }
        }

        internal void RaiseAutomationInvokeEvents(DataGridEditingUnit editingUnit, DataGridColumn column, DataGridRow row)
        {
            switch (editingUnit)
            {
                case DataGridEditingUnit.Cell:
                    {
                        DataGridCell cell = row.Cells[column.Index];
                        AutomationPeer peer = FromElement(cell);
                        if (peer != null)
                        {
                            peer.InvalidatePeer();
                        }
                        else
                        {
                            peer = CreatePeerForElement(cell);
                        }

                        if (peer != null)
                        {
                            peer.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
                        }
                        break;
                    }
                case DataGridEditingUnit.Row:
                    {
                        DataGridItemAutomationPeer peer = GetOrCreateItemPeer(row.DataContext);
                        peer.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
                        break;
                    }
            }
        }

        internal void RaiseAutomationScrollEvents()
        {
            IScrollProvider isp = (IScrollProvider)this;

            bool newHorizontallyScrollable = isp.HorizontallyScrollable;
            double newHorizontalViewSize = isp.HorizontalViewSize;
            double newHorizontalScrollPercent = isp.HorizontalScrollPercent;

            bool newVerticallyScrollable = isp.VerticallyScrollable;
            double newVerticalViewSize = isp.VerticalViewSize;
            double newVerticalScrollPercent = isp.VerticalScrollPercent;

            if (_oldHorizontallyScrollable != newHorizontallyScrollable)
            {
                RaisePropertyChangedEvent(
                    ScrollPatternIdentifiers.HorizontallyScrollableProperty,
                    _oldHorizontallyScrollable,
                    newHorizontallyScrollable);
                _oldHorizontallyScrollable = newHorizontallyScrollable;
            }

            if (_oldHorizontalViewSize != newHorizontalViewSize)
            {
                RaisePropertyChangedEvent(
                    ScrollPatternIdentifiers.HorizontalViewSizeProperty,
                    _oldHorizontalViewSize,
                    newHorizontalViewSize);
                _oldHorizontalViewSize = newHorizontalViewSize;
            }

            if (_oldHorizontalScrollPercent != newHorizontalScrollPercent)
            {
                RaisePropertyChangedEvent(
                    ScrollPatternIdentifiers.HorizontalScrollPercentProperty,
                    _oldHorizontalScrollPercent,
                    newHorizontalScrollPercent);
                _oldHorizontalScrollPercent = newHorizontalScrollPercent;
            }

            if (_oldVerticallyScrollable != newVerticallyScrollable)
            {
                RaisePropertyChangedEvent(
                    ScrollPatternIdentifiers.VerticallyScrollableProperty,
                    _oldVerticallyScrollable,
                    newVerticallyScrollable);
                _oldVerticallyScrollable = newVerticallyScrollable;
            }

            if (_oldVerticalViewSize != newVerticalViewSize)
            {
                RaisePropertyChangedEvent(
                    ScrollPatternIdentifiers.VerticalViewSizeProperty,
                    _oldVerticalViewSize,
                    newVerticalViewSize);
                _oldVerticalViewSize = newVerticalViewSize;
            }

            if (_oldVerticalScrollPercent != newVerticalScrollPercent)
            {
                RaisePropertyChangedEvent(
                    ScrollPatternIdentifiers.VerticalScrollPercentProperty,
                    _oldVerticalScrollPercent,
                    newVerticalScrollPercent);
                _oldVerticalScrollPercent = newVerticalScrollPercent;
            }
        }

        internal void RaiseAutomationSelectionEvents(SelectionChangedEventArgs e)
        {
            // If the result of an AddToSelection or RemoveFromSelection is a single selected item,
            // then all we raise is the ElementSelectedEvent for single item
            if (AutomationPeer.ListenerExists(AutomationEvents.SelectionItemPatternOnElementSelected) &&
                this.OwningDataGrid.SelectedItems.Count == 1)
            {
                if (this.OwningDataGrid.SelectedItem != null && _itemPeers.ContainsKey(this.OwningDataGrid.SelectedItem))
                {
                    DataGridItemAutomationPeer peer = _itemPeers[this.OwningDataGrid.SelectedItem];
                    peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementSelected);
                }
            }
            else
            {
                int i;

                if (AutomationPeer.ListenerExists(AutomationEvents.SelectionItemPatternOnElementAddedToSelection))
                {
                    for (i = 0; i < e.AddedItems.Count; i++)
                    {
                        if (e.AddedItems[i] != null && _itemPeers.ContainsKey(e.AddedItems[i]))
                        {
                            DataGridItemAutomationPeer peer = _itemPeers[e.AddedItems[i]];
                            peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementAddedToSelection);
                        }
                    }
                }

                if (AutomationPeer.ListenerExists(AutomationEvents.SelectionItemPatternOnElementRemovedFromSelection))
                {
                    for (i = 0; i < e.RemovedItems.Count; i++)
                    {
                        if (e.RemovedItems[i] != null &&_itemPeers.ContainsKey(e.RemovedItems[i]))
                        {
                            DataGridItemAutomationPeer peer = _itemPeers[e.RemovedItems[i]];
                            peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementRemovedFromSelection);
                        }
                    }
                }
            }
        }

        internal void UpdateRowGroupHeaderPeerEventsSource(DataGridRowGroupHeader header)
        {
            object group = header.RowGroupInfo.CollectionViewGroup;
            DataGridRowGroupHeaderAutomationPeer peer = DataGridRowGroupHeaderAutomationPeer.FromElement(header) as DataGridRowGroupHeaderAutomationPeer;
            if (peer != null && group != null && _groupItemPeers.ContainsKey(group))
            {
                peer.EventsSource = _groupItemPeers[group];
            }
        }

        internal void UpdateRowPeerEventsSource(DataGridRow row)
        {
            DataGridRowAutomationPeer peer = FromElement(row) as DataGridRowAutomationPeer;
            if (peer != null && row.DataContext != null && _itemPeers.ContainsKey(row.DataContext))
            {
                peer.EventsSource = _itemPeers[row.DataContext];
            }
        }

#endregion
    }
}
