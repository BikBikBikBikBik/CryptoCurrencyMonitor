namespace CryptoCurrencyMonitor.MarketData.Monitor {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this._lblLastUpdated = new System.Windows.Forms.Label();
			this._lblLastUpdatedValue = new System.Windows.Forms.Label();
			this._prgrssGlobalRefresh = new System.Windows.Forms.ProgressBar();
			this._gridMarketData = new System.Windows.Forms.DataGridView();
			this._clmnMarketCoin = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnMarketCurrentUsdPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnMarketCurrentBtcPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnMarketSatoshi = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnMarketPercentChange1h = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnMarketPercentChange24h = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnMarketPercentChange7D = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnMarketVolumeUsd24h = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnMarketMarketCapUsd = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnMarketRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._btnPauseRefreshTimer = new System.Windows.Forms.Button();
			this._ntfyMain = new System.Windows.Forms.NotifyIcon(this.components);
			this._gridHoldingsData = new System.Windows.Forms.DataGridView();
			this._clmnHoldingsCoin = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnHoldingsQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnHoldingsPriceInUsd = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnHoldingsPriceInBtc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._lblTotalValUsd = new System.Windows.Forms.Label();
			this._lblTotalValUsdValue = new System.Windows.Forms.Label();
			this._lblTotalValBtc = new System.Windows.Forms.Label();
			this._lblTotalValBtcValue = new System.Windows.Forms.Label();
			this._cntnrGridData = new System.Windows.Forms.SplitContainer();
			this._menuMain = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.currencyListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.holdingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.marketToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this._gridMarketData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._gridHoldingsData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._cntnrGridData)).BeginInit();
			this._cntnrGridData.Panel1.SuspendLayout();
			this._cntnrGridData.Panel2.SuspendLayout();
			this._cntnrGridData.SuspendLayout();
			this._menuMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lblLastUpdated
			// 
			this._lblLastUpdated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._lblLastUpdated.AutoSize = true;
			this._lblLastUpdated.Location = new System.Drawing.Point(833, 559);
			this._lblLastUpdated.Name = "_lblLastUpdated";
			this._lblLastUpdated.Size = new System.Drawing.Size(74, 13);
			this._lblLastUpdated.TabIndex = 2;
			this._lblLastUpdated.Text = "Last Updated:";
			// 
			// _lblLastUpdatedValue
			// 
			this._lblLastUpdatedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._lblLastUpdatedValue.AutoSize = true;
			this._lblLastUpdatedValue.Location = new System.Drawing.Point(913, 559);
			this._lblLastUpdatedValue.Name = "_lblLastUpdatedValue";
			this._lblLastUpdatedValue.Size = new System.Drawing.Size(106, 13);
			this._lblLastUpdatedValue.TabIndex = 3;
			this._lblLastUpdatedValue.Text = "LAST_REFRESHED";
			// 
			// _prgrssGlobalRefresh
			// 
			this._prgrssGlobalRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._prgrssGlobalRefresh.Location = new System.Drawing.Point(10, 584);
			this._prgrssGlobalRefresh.Name = "_prgrssGlobalRefresh";
			this._prgrssGlobalRefresh.Size = new System.Drawing.Size(928, 23);
			this._prgrssGlobalRefresh.TabIndex = 4;
			// 
			// _gridMarketData
			// 
			this._gridMarketData.AllowUserToAddRows = false;
			this._gridMarketData.AllowUserToDeleteRows = false;
			this._gridMarketData.AllowUserToResizeRows = false;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this._gridMarketData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
			this._gridMarketData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._gridMarketData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._gridMarketData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._gridMarketData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this._gridMarketData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._clmnMarketCoin,
            this._clmnMarketCurrentUsdPrice,
            this._clmnMarketCurrentBtcPrice,
            this._clmnMarketSatoshi,
            this._clmnMarketPercentChange1h,
            this._clmnMarketPercentChange24h,
            this._clmnMarketPercentChange7D,
            this._clmnMarketVolumeUsd24h,
            this._clmnMarketMarketCapUsd,
            this._clmnMarketRank});
			this._gridMarketData.Location = new System.Drawing.Point(1, 3);
			this._gridMarketData.Name = "_gridMarketData";
			this._gridMarketData.ReadOnly = true;
			this._gridMarketData.RowHeadersVisible = false;
			this._gridMarketData.RowHeadersWidth = 60;
			this._gridMarketData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._gridMarketData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._gridMarketData.Size = new System.Drawing.Size(1006, 255);
			this._gridMarketData.TabIndex = 5;
			this._gridMarketData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.OnGridMarketDataCellFormatting);
			this._gridMarketData.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.OnGridMarketDataSortCompare);
			// 
			// _clmnMarketCoin
			// 
			this._clmnMarketCoin.HeaderText = "Coin";
			this._clmnMarketCoin.Name = "_clmnMarketCoin";
			this._clmnMarketCoin.ReadOnly = true;
			// 
			// _clmnMarketCurrentUsdPrice
			// 
			this._clmnMarketCurrentUsdPrice.HeaderText = "$ Value";
			this._clmnMarketCurrentUsdPrice.Name = "_clmnMarketCurrentUsdPrice";
			this._clmnMarketCurrentUsdPrice.ReadOnly = true;
			// 
			// _clmnMarketCurrentBtcPrice
			// 
			this._clmnMarketCurrentBtcPrice.HeaderText = "BTC Value";
			this._clmnMarketCurrentBtcPrice.Name = "_clmnMarketCurrentBtcPrice";
			this._clmnMarketCurrentBtcPrice.ReadOnly = true;
			// 
			// _clmnMarketSatoshi
			// 
			this._clmnMarketSatoshi.HeaderText = "Satoshi Value";
			this._clmnMarketSatoshi.Name = "_clmnMarketSatoshi";
			this._clmnMarketSatoshi.ReadOnly = true;
			// 
			// _clmnMarketPercentChange1h
			// 
			this._clmnMarketPercentChange1h.HeaderText = "% 1H";
			this._clmnMarketPercentChange1h.Name = "_clmnMarketPercentChange1h";
			this._clmnMarketPercentChange1h.ReadOnly = true;
			// 
			// _clmnMarketPercentChange24h
			// 
			this._clmnMarketPercentChange24h.HeaderText = "% 24H";
			this._clmnMarketPercentChange24h.Name = "_clmnMarketPercentChange24h";
			this._clmnMarketPercentChange24h.ReadOnly = true;
			// 
			// _clmnMarketPercentChange7D
			// 
			this._clmnMarketPercentChange7D.HeaderText = "% 7D";
			this._clmnMarketPercentChange7D.Name = "_clmnMarketPercentChange7D";
			this._clmnMarketPercentChange7D.ReadOnly = true;
			// 
			// _clmnMarketVolumeUsd24h
			// 
			this._clmnMarketVolumeUsd24h.HeaderText = "Vol (USD) 24H";
			this._clmnMarketVolumeUsd24h.Name = "_clmnMarketVolumeUsd24h";
			this._clmnMarketVolumeUsd24h.ReadOnly = true;
			// 
			// _clmnMarketMarketCapUsd
			// 
			this._clmnMarketMarketCapUsd.HeaderText = "Cap (USD)";
			this._clmnMarketMarketCapUsd.Name = "_clmnMarketMarketCapUsd";
			this._clmnMarketMarketCapUsd.ReadOnly = true;
			// 
			// _clmnMarketRank
			// 
			this._clmnMarketRank.HeaderText = "Rank";
			this._clmnMarketRank.Name = "_clmnMarketRank";
			this._clmnMarketRank.ReadOnly = true;
			// 
			// _btnPauseRefreshTimer
			// 
			this._btnPauseRefreshTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnPauseRefreshTimer.Location = new System.Drawing.Point(944, 584);
			this._btnPauseRefreshTimer.Name = "_btnPauseRefreshTimer";
			this._btnPauseRefreshTimer.Size = new System.Drawing.Size(75, 23);
			this._btnPauseRefreshTimer.TabIndex = 6;
			this._btnPauseRefreshTimer.Text = "Pause";
			this._btnPauseRefreshTimer.UseVisualStyleBackColor = true;
			this._btnPauseRefreshTimer.Click += new System.EventHandler(this.OnBtnPauseRefreshTimerClick);
			// 
			// _ntfyMain
			// 
			this._ntfyMain.Icon = ((System.Drawing.Icon)(resources.GetObject("_ntfyMain.Icon")));
			this._ntfyMain.Text = "notifyIcon1";
			this._ntfyMain.Visible = true;
			this._ntfyMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnNtfyMainMouseDoubleClick);
			// 
			// _gridHoldingsData
			// 
			this._gridHoldingsData.AllowUserToAddRows = false;
			this._gridHoldingsData.AllowUserToDeleteRows = false;
			this._gridHoldingsData.AllowUserToResizeRows = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this._gridHoldingsData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this._gridHoldingsData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._gridHoldingsData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._gridHoldingsData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._gridHoldingsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this._gridHoldingsData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._clmnHoldingsCoin,
            this._clmnHoldingsQuantity,
            this._clmnHoldingsPriceInUsd,
            this._clmnHoldingsPriceInBtc});
			this._gridHoldingsData.Location = new System.Drawing.Point(3, 3);
			this._gridHoldingsData.Name = "_gridHoldingsData";
			this._gridHoldingsData.RowHeadersVisible = false;
			this._gridHoldingsData.RowHeadersWidth = 60;
			this._gridHoldingsData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._gridHoldingsData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._gridHoldingsData.Size = new System.Drawing.Size(1006, 257);
			this._gridHoldingsData.TabIndex = 7;
			this._gridHoldingsData.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.OnGridHoldingsDataCellValidating);
			this._gridHoldingsData.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.OnGridHoldingsDataSortCompare);
			// 
			// _clmnHoldingsCoin
			// 
			this._clmnHoldingsCoin.HeaderText = "Coin";
			this._clmnHoldingsCoin.Name = "_clmnHoldingsCoin";
			this._clmnHoldingsCoin.ReadOnly = true;
			// 
			// _clmnHoldingsQuantity
			// 
			this._clmnHoldingsQuantity.HeaderText = "Quantity";
			this._clmnHoldingsQuantity.Name = "_clmnHoldingsQuantity";
			// 
			// _clmnHoldingsPriceInUsd
			// 
			this._clmnHoldingsPriceInUsd.HeaderText = "$ Value";
			this._clmnHoldingsPriceInUsd.Name = "_clmnHoldingsPriceInUsd";
			this._clmnHoldingsPriceInUsd.ReadOnly = true;
			// 
			// _clmnHoldingsPriceInBtc
			// 
			this._clmnHoldingsPriceInBtc.HeaderText = "BTC Value";
			this._clmnHoldingsPriceInBtc.Name = "_clmnHoldingsPriceInBtc";
			this._clmnHoldingsPriceInBtc.ReadOnly = true;
			// 
			// _lblTotalValUsd
			// 
			this._lblTotalValUsd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._lblTotalValUsd.AutoSize = true;
			this._lblTotalValUsd.Location = new System.Drawing.Point(10, 559);
			this._lblTotalValUsd.Name = "_lblTotalValUsd";
			this._lblTotalValUsd.Size = new System.Drawing.Size(73, 13);
			this._lblTotalValUsd.TabIndex = 9;
			this._lblTotalValUsd.Text = "Total $ Value:";
			// 
			// _lblTotalValUsdValue
			// 
			this._lblTotalValUsdValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._lblTotalValUsdValue.AutoSize = true;
			this._lblTotalValUsdValue.Location = new System.Drawing.Point(89, 559);
			this._lblTotalValUsdValue.Name = "_lblTotalValUsdValue";
			this._lblTotalValUsdValue.Size = new System.Drawing.Size(112, 13);
			this._lblTotalValUsdValue.TabIndex = 10;
			this._lblTotalValUsdValue.Text = "TOTAL_VALUE_USD";
			// 
			// _lblTotalValBtc
			// 
			this._lblTotalValBtc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._lblTotalValBtc.AutoSize = true;
			this._lblTotalValBtc.Location = new System.Drawing.Point(332, 559);
			this._lblTotalValBtc.Name = "_lblTotalValBtc";
			this._lblTotalValBtc.Size = new System.Drawing.Size(88, 13);
			this._lblTotalValBtc.TabIndex = 11;
			this._lblTotalValBtc.Text = "Total BTC Value:";
			// 
			// _lblTotalValBtcValue
			// 
			this._lblTotalValBtcValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._lblTotalValBtcValue.AutoSize = true;
			this._lblTotalValBtcValue.Location = new System.Drawing.Point(426, 559);
			this._lblTotalValBtcValue.Name = "_lblTotalValBtcValue";
			this._lblTotalValBtcValue.Size = new System.Drawing.Size(110, 13);
			this._lblTotalValBtcValue.TabIndex = 12;
			this._lblTotalValBtcValue.Text = "TOTAL_VALUE_BTC";
			// 
			// _cntnrGridData
			// 
			this._cntnrGridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._cntnrGridData.Location = new System.Drawing.Point(12, 27);
			this._cntnrGridData.Name = "_cntnrGridData";
			this._cntnrGridData.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// _cntnrGridData.Panel1
			// 
			this._cntnrGridData.Panel1.Controls.Add(this._gridMarketData);
			this._cntnrGridData.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			// 
			// _cntnrGridData.Panel2
			// 
			this._cntnrGridData.Panel2.Controls.Add(this._gridHoldingsData);
			this._cntnrGridData.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cntnrGridData.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._cntnrGridData.Size = new System.Drawing.Size(1006, 529);
			this._cntnrGridData.SplitterDistance = 261;
			this._cntnrGridData.TabIndex = 13;
			// 
			// _menuMain
			// 
			this._menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.currencyListToolStripMenuItem});
			this._menuMain.Location = new System.Drawing.Point(0, 0);
			this._menuMain.Name = "_menuMain";
			this._menuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this._menuMain.Size = new System.Drawing.Size(1032, 24);
			this._menuMain.TabIndex = 14;
			this._menuMain.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// currencyListToolStripMenuItem
			// 
			this.currencyListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.holdingsToolStripMenuItem1,
            this.marketToolStripMenuItem1});
			this.currencyListToolStripMenuItem.Name = "currencyListToolStripMenuItem";
			this.currencyListToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
			this.currencyListToolStripMenuItem.Text = "&Currency List";
			// 
			// holdingsToolStripMenuItem1
			// 
			this.holdingsToolStripMenuItem1.Name = "holdingsToolStripMenuItem1";
			this.holdingsToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.holdingsToolStripMenuItem1.Text = "&Holdings";
			// 
			// marketToolStripMenuItem1
			// 
			this.marketToolStripMenuItem1.Name = "marketToolStripMenuItem1";
			this.marketToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.marketToolStripMenuItem1.Text = "&Market";
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.quitToolStripMenuItem.Text = "&Quit";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1032, 609);
			this.Controls.Add(this._lblLastUpdated);
			this.Controls.Add(this._lblTotalValBtcValue);
			this.Controls.Add(this._lblTotalValBtc);
			this.Controls.Add(this._lblTotalValUsdValue);
			this.Controls.Add(this._lblTotalValUsd);
			this.Controls.Add(this._lblLastUpdatedValue);
			this.Controls.Add(this._cntnrGridData);
			this.Controls.Add(this._btnPauseRefreshTimer);
			this.Controls.Add(this._prgrssGlobalRefresh);
			this.Controls.Add(this._menuMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this._menuMain;
			this.MinimumSize = new System.Drawing.Size(1040, 636);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Crypto Currency Market Monitor";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormMainClosed);
			this.Load += new System.EventHandler(this.OnFormMainLoad);
			this.Resize += new System.EventHandler(this.OnFormMainResize);
			((System.ComponentModel.ISupportInitialize)(this._gridMarketData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._gridHoldingsData)).EndInit();
			this._cntnrGridData.Panel1.ResumeLayout(false);
			this._cntnrGridData.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._cntnrGridData)).EndInit();
			this._cntnrGridData.ResumeLayout(false);
			this._menuMain.ResumeLayout(false);
			this._menuMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label _lblLastUpdated;
		private System.Windows.Forms.Label _lblLastUpdatedValue;
		private System.Windows.Forms.ProgressBar _prgrssGlobalRefresh;
		private System.Windows.Forms.DataGridView _gridMarketData;
		private System.Windows.Forms.Button _btnPauseRefreshTimer;
		private System.Windows.Forms.NotifyIcon _ntfyMain;
		private System.Windows.Forms.DataGridView _gridHoldingsData;
		private System.Windows.Forms.Label _lblTotalValUsd;
		private System.Windows.Forms.Label _lblTotalValUsdValue;
		private System.Windows.Forms.Label _lblTotalValBtc;
		private System.Windows.Forms.Label _lblTotalValBtcValue;
		private System.Windows.Forms.SplitContainer _cntnrGridData;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnMarketCoin;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnMarketCurrentUsdPrice;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnMarketCurrentBtcPrice;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnMarketSatoshi;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnMarketPercentChange1h;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnMarketPercentChange24h;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnMarketPercentChange7D;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnMarketVolumeUsd24h;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnMarketMarketCapUsd;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnMarketRank;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnHoldingsCoin;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnHoldingsQuantity;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnHoldingsPriceInUsd;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnHoldingsPriceInBtc;
		private System.Windows.Forms.MenuStrip _menuMain;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem currencyListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem holdingsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem marketToolStripMenuItem1;
	}
}

