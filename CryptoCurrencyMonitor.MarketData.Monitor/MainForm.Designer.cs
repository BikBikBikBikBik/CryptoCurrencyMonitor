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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this._lblLastUpdated = new System.Windows.Forms.Label();
			this._lblLastUpdatedValue = new System.Windows.Forms.Label();
			this._prgrssGlobalRefresh = new System.Windows.Forms.ProgressBar();
			this._gridExchangeData = new System.Windows.Forms.DataGridView();
			this._clmnExchangeCoin = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnExchangeCurrentUsdPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnExchangeCurrentBtcPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnExchangeSatoshi = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnExchangePercentChange1h = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnExchangePercentChange24h = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnExchangePercentChange7D = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnExchangeVolumeUsd24h = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnExchangeMarketCapUsd = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnExchangeRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._btnPauseRefreshTimer = new System.Windows.Forms.Button();
			this._ntfyMain = new System.Windows.Forms.NotifyIcon(this.components);
			this._gridHoldingsData = new System.Windows.Forms.DataGridView();
			this._clmnHoldingsCoin = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnHoldingsQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnHoldingsPriceInUsd = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._clmnHoldingsPriceInBtc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._txtbxErrors = new System.Windows.Forms.TextBox();
			this._lblTotalValUsd = new System.Windows.Forms.Label();
			this._lblTotalValUsdValue = new System.Windows.Forms.Label();
			this._lblTotalValBtc = new System.Windows.Forms.Label();
			this._lblTotalValBtcValue = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this._gridExchangeData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._gridHoldingsData)).BeginInit();
			this.SuspendLayout();
			// 
			// _lblLastUpdated
			// 
			this._lblLastUpdated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._lblLastUpdated.AutoSize = true;
			this._lblLastUpdated.Location = new System.Drawing.Point(835, 561);
			this._lblLastUpdated.Name = "_lblLastUpdated";
			this._lblLastUpdated.Size = new System.Drawing.Size(74, 13);
			this._lblLastUpdated.TabIndex = 2;
			this._lblLastUpdated.Text = "Last Updated:";
			// 
			// _lblLastUpdatedValue
			// 
			this._lblLastUpdatedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._lblLastUpdatedValue.AutoSize = true;
			this._lblLastUpdatedValue.Location = new System.Drawing.Point(915, 561);
			this._lblLastUpdatedValue.Name = "_lblLastUpdatedValue";
			this._lblLastUpdatedValue.Size = new System.Drawing.Size(106, 13);
			this._lblLastUpdatedValue.TabIndex = 3;
			this._lblLastUpdatedValue.Text = "LAST_REFRESHED";
			// 
			// _prgrssGlobalRefresh
			// 
			this._prgrssGlobalRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._prgrssGlobalRefresh.Location = new System.Drawing.Point(511, 284);
			this._prgrssGlobalRefresh.Name = "_prgrssGlobalRefresh";
			this._prgrssGlobalRefresh.Size = new System.Drawing.Size(428, 23);
			this._prgrssGlobalRefresh.TabIndex = 4;
			// 
			// _gridExchangeData
			// 
			this._gridExchangeData.AllowUserToAddRows = false;
			this._gridExchangeData.AllowUserToDeleteRows = false;
			this._gridExchangeData.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this._gridExchangeData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this._gridExchangeData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._gridExchangeData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._gridExchangeData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._gridExchangeData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this._gridExchangeData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._clmnExchangeCoin,
            this._clmnExchangeCurrentUsdPrice,
            this._clmnExchangeCurrentBtcPrice,
            this._clmnExchangeSatoshi,
            this._clmnExchangePercentChange1h,
            this._clmnExchangePercentChange24h,
            this._clmnExchangePercentChange7D,
            this._clmnExchangeVolumeUsd24h,
            this._clmnExchangeMarketCapUsd,
            this._clmnExchangeRank});
			this._gridExchangeData.Location = new System.Drawing.Point(12, 5);
			this._gridExchangeData.Name = "_gridExchangeData";
			this._gridExchangeData.ReadOnly = true;
			this._gridExchangeData.RowHeadersVisible = false;
			this._gridExchangeData.RowHeadersWidth = 60;
			this._gridExchangeData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._gridExchangeData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._gridExchangeData.Size = new System.Drawing.Size(1008, 273);
			this._gridExchangeData.TabIndex = 5;
			this._gridExchangeData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.OnGridExchangeDataCellFormatting);
			this._gridExchangeData.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.OnGridExchangeDataSortCompare);
			// 
			// _clmnExchangeCoin
			// 
			this._clmnExchangeCoin.HeaderText = "Coin";
			this._clmnExchangeCoin.Name = "_clmnExchangeCoin";
			this._clmnExchangeCoin.ReadOnly = true;
			// 
			// _clmnExchangeCurrentUsdPrice
			// 
			this._clmnExchangeCurrentUsdPrice.HeaderText = "USD (CMC / CC)";
			this._clmnExchangeCurrentUsdPrice.Name = "_clmnExchangeCurrentUsdPrice";
			this._clmnExchangeCurrentUsdPrice.ReadOnly = true;
			// 
			// _clmnExchangeCurrentBtcPrice
			// 
			this._clmnExchangeCurrentBtcPrice.HeaderText = "BTC";
			this._clmnExchangeCurrentBtcPrice.Name = "_clmnExchangeCurrentBtcPrice";
			this._clmnExchangeCurrentBtcPrice.ReadOnly = true;
			// 
			// _clmnExchangeSatoshi
			// 
			this._clmnExchangeSatoshi.HeaderText = "Satoshi";
			this._clmnExchangeSatoshi.Name = "_clmnExchangeSatoshi";
			this._clmnExchangeSatoshi.ReadOnly = true;
			// 
			// _clmnExchangePercentChange1h
			// 
			this._clmnExchangePercentChange1h.HeaderText = "% 1H";
			this._clmnExchangePercentChange1h.Name = "_clmnExchangePercentChange1h";
			this._clmnExchangePercentChange1h.ReadOnly = true;
			// 
			// _clmnExchangePercentChange24h
			// 
			this._clmnExchangePercentChange24h.HeaderText = "% 24H";
			this._clmnExchangePercentChange24h.Name = "_clmnExchangePercentChange24h";
			this._clmnExchangePercentChange24h.ReadOnly = true;
			// 
			// _clmnExchangePercentChange7D
			// 
			this._clmnExchangePercentChange7D.HeaderText = "% 7D";
			this._clmnExchangePercentChange7D.Name = "_clmnExchangePercentChange7D";
			this._clmnExchangePercentChange7D.ReadOnly = true;
			// 
			// _clmnExchangeVolumeUsd24h
			// 
			this._clmnExchangeVolumeUsd24h.HeaderText = "Vol (USD) 24H";
			this._clmnExchangeVolumeUsd24h.Name = "_clmnExchangeVolumeUsd24h";
			this._clmnExchangeVolumeUsd24h.ReadOnly = true;
			// 
			// _clmnExchangeMarketCapUsd
			// 
			this._clmnExchangeMarketCapUsd.HeaderText = "Cap (USD)";
			this._clmnExchangeMarketCapUsd.Name = "_clmnExchangeMarketCapUsd";
			this._clmnExchangeMarketCapUsd.ReadOnly = true;
			// 
			// _clmnExchangeRank
			// 
			this._clmnExchangeRank.HeaderText = "Rank";
			this._clmnExchangeRank.Name = "_clmnExchangeRank";
			this._clmnExchangeRank.ReadOnly = true;
			// 
			// _btnPauseRefreshTimer
			// 
			this._btnPauseRefreshTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnPauseRefreshTimer.Location = new System.Drawing.Point(945, 284);
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
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this._gridHoldingsData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
			this._gridHoldingsData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._gridHoldingsData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this._gridHoldingsData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._gridHoldingsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this._gridHoldingsData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._clmnHoldingsCoin,
            this._clmnHoldingsQuantity,
            this._clmnHoldingsPriceInUsd,
            this._clmnHoldingsPriceInBtc});
			this._gridHoldingsData.Location = new System.Drawing.Point(12, 284);
			this._gridHoldingsData.Name = "_gridHoldingsData";
			this._gridHoldingsData.RowHeadersVisible = false;
			this._gridHoldingsData.RowHeadersWidth = 60;
			this._gridHoldingsData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._gridHoldingsData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._gridHoldingsData.Size = new System.Drawing.Size(493, 273);
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
			this._clmnHoldingsPriceInUsd.HeaderText = "$ Val (CMC / CC)";
			this._clmnHoldingsPriceInUsd.Name = "_clmnHoldingsPriceInUsd";
			this._clmnHoldingsPriceInUsd.ReadOnly = true;
			// 
			// _clmnHoldingsPriceInBtc
			// 
			this._clmnHoldingsPriceInBtc.HeaderText = "BTC Val";
			this._clmnHoldingsPriceInBtc.Name = "_clmnHoldingsPriceInBtc";
			// 
			// _txtbxErrors
			// 
			this._txtbxErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._txtbxErrors.Location = new System.Drawing.Point(511, 313);
			this._txtbxErrors.Multiline = true;
			this._txtbxErrors.Name = "_txtbxErrors";
			this._txtbxErrors.ReadOnly = true;
			this._txtbxErrors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._txtbxErrors.Size = new System.Drawing.Size(508, 244);
			this._txtbxErrors.TabIndex = 8;
			// 
			// _lblTotalValUsd
			// 
			this._lblTotalValUsd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._lblTotalValUsd.AutoSize = true;
			this._lblTotalValUsd.Location = new System.Drawing.Point(12, 561);
			this._lblTotalValUsd.Name = "_lblTotalValUsd";
			this._lblTotalValUsd.Size = new System.Drawing.Size(118, 13);
			this._lblTotalValUsd.TabIndex = 9;
			this._lblTotalValUsd.Text = "Total $ Val (CMC / CC):";
			// 
			// _lblTotalValUsdValue
			// 
			this._lblTotalValUsdValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._lblTotalValUsdValue.AutoSize = true;
			this._lblTotalValUsdValue.Location = new System.Drawing.Point(136, 561);
			this._lblTotalValUsdValue.Name = "_lblTotalValUsdValue";
			this._lblTotalValUsdValue.Size = new System.Drawing.Size(112, 13);
			this._lblTotalValUsdValue.TabIndex = 10;
			this._lblTotalValUsdValue.Text = "TOTAL_VALUE_USD";
			// 
			// _lblTotalValBtc
			// 
			this._lblTotalValBtc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._lblTotalValBtc.AutoSize = true;
			this._lblTotalValBtc.Location = new System.Drawing.Point(334, 561);
			this._lblTotalValBtc.Name = "_lblTotalValBtc";
			this._lblTotalValBtc.Size = new System.Drawing.Size(76, 13);
			this._lblTotalValBtc.TabIndex = 11;
			this._lblTotalValBtc.Text = "Total BTC Val:";
			// 
			// _lblTotalValBtcValue
			// 
			this._lblTotalValBtcValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._lblTotalValBtcValue.AutoSize = true;
			this._lblTotalValBtcValue.Location = new System.Drawing.Point(416, 561);
			this._lblTotalValBtcValue.Name = "_lblTotalValBtcValue";
			this._lblTotalValBtcValue.Size = new System.Drawing.Size(110, 13);
			this._lblTotalValBtcValue.TabIndex = 12;
			this._lblTotalValBtcValue.Text = "TOTAL_VALUE_BTC";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1032, 583);
			this.Controls.Add(this._lblTotalValBtcValue);
			this.Controls.Add(this._lblTotalValBtc);
			this.Controls.Add(this._lblTotalValUsdValue);
			this.Controls.Add(this._lblTotalValUsd);
			this.Controls.Add(this._txtbxErrors);
			this.Controls.Add(this._gridHoldingsData);
			this.Controls.Add(this._btnPauseRefreshTimer);
			this.Controls.Add(this._gridExchangeData);
			this.Controls.Add(this._prgrssGlobalRefresh);
			this.Controls.Add(this._lblLastUpdatedValue);
			this.Controls.Add(this._lblLastUpdated);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(1040, 610);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Currency Exchange Monitor";
			this.Load += new System.EventHandler(this.OnFormMainLoad);
			this.Resize += new System.EventHandler(this.OnFormMainResize);
			((System.ComponentModel.ISupportInitialize)(this._gridExchangeData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._gridHoldingsData)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label _lblLastUpdated;
		private System.Windows.Forms.Label _lblLastUpdatedValue;
		private System.Windows.Forms.ProgressBar _prgrssGlobalRefresh;
		private System.Windows.Forms.DataGridView _gridExchangeData;
		private System.Windows.Forms.Button _btnPauseRefreshTimer;
		private System.Windows.Forms.NotifyIcon _ntfyMain;
		private System.Windows.Forms.DataGridView _gridHoldingsData;
		private System.Windows.Forms.TextBox _txtbxErrors;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnExchangeCoin;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnExchangeCurrentUsdPrice;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnExchangeCurrentBtcPrice;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnExchangeSatoshi;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnExchangePercentChange1h;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnExchangePercentChange24h;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnExchangePercentChange7D;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnExchangeVolumeUsd24h;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnExchangeMarketCapUsd;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnExchangeRank;
		private System.Windows.Forms.Label _lblTotalValUsd;
		private System.Windows.Forms.Label _lblTotalValUsdValue;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnHoldingsCoin;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnHoldingsQuantity;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnHoldingsPriceInUsd;
		private System.Windows.Forms.DataGridViewTextBoxColumn _clmnHoldingsPriceInBtc;
		private System.Windows.Forms.Label _lblTotalValBtc;
		private System.Windows.Forms.Label _lblTotalValBtcValue;
	}
}

