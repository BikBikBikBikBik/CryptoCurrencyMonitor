namespace CryptoCurrencyMonitor.MarketData.Monitor {
	partial class CurrencySelectionForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrencySelectionForm));
			this._lstUnselectedCurrencies = new System.Windows.Forms.ListBox();
			this._lstSelectedCurrencies = new System.Windows.Forms.ListBox();
			this._cntnr = new System.Windows.Forms.SplitContainer();
			this._btnOk = new System.Windows.Forms.Button();
			this._btnAddSelectedCurrencies = new System.Windows.Forms.Button();
			this._btnCancel = new System.Windows.Forms.Button();
			this._btnRemoveSelectedCurrencies = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._cntnr)).BeginInit();
			this._cntnr.Panel1.SuspendLayout();
			this._cntnr.Panel2.SuspendLayout();
			this._cntnr.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lstUnselectedCurrencies
			// 
			this._lstUnselectedCurrencies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._lstUnselectedCurrencies.FormattingEnabled = true;
			this._lstUnselectedCurrencies.Location = new System.Drawing.Point(3, 3);
			this._lstUnselectedCurrencies.Name = "_lstUnselectedCurrencies";
			this._lstUnselectedCurrencies.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this._lstUnselectedCurrencies.Size = new System.Drawing.Size(311, 485);
			this._lstUnselectedCurrencies.TabIndex = 0;
			// 
			// _lstSelectedCurrencies
			// 
			this._lstSelectedCurrencies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._lstSelectedCurrencies.FormattingEnabled = true;
			this._lstSelectedCurrencies.Location = new System.Drawing.Point(3, 3);
			this._lstSelectedCurrencies.Name = "_lstSelectedCurrencies";
			this._lstSelectedCurrencies.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this._lstSelectedCurrencies.Size = new System.Drawing.Size(311, 485);
			this._lstSelectedCurrencies.TabIndex = 0;
			// 
			// _cntnr
			// 
			this._cntnr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._cntnr.Location = new System.Drawing.Point(13, 13);
			this._cntnr.Name = "_cntnr";
			// 
			// _cntnr.Panel1
			// 
			this._cntnr.Panel1.Controls.Add(this._btnOk);
			this._cntnr.Panel1.Controls.Add(this._btnAddSelectedCurrencies);
			this._cntnr.Panel1.Controls.Add(this._lstUnselectedCurrencies);
			// 
			// _cntnr.Panel2
			// 
			this._cntnr.Panel2.Controls.Add(this._btnCancel);
			this._cntnr.Panel2.Controls.Add(this._btnRemoveSelectedCurrencies);
			this._cntnr.Panel2.Controls.Add(this._lstSelectedCurrencies);
			this._cntnr.Size = new System.Drawing.Size(637, 583);
			this._cntnr.SplitterDistance = 316;
			this._cntnr.TabIndex = 1;
			// 
			// _btnOk
			// 
			this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._btnOk.Location = new System.Drawing.Point(158, 540);
			this._btnOk.Name = "_btnOk";
			this._btnOk.Size = new System.Drawing.Size(155, 40);
			this._btnOk.TabIndex = 2;
			this._btnOk.Text = "Ok";
			this._btnOk.UseVisualStyleBackColor = true;
			// 
			// _btnAddSelectedCurrencies
			// 
			this._btnAddSelectedCurrencies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._btnAddSelectedCurrencies.Location = new System.Drawing.Point(3, 494);
			this._btnAddSelectedCurrencies.Name = "_btnAddSelectedCurrencies";
			this._btnAddSelectedCurrencies.Size = new System.Drawing.Size(310, 40);
			this._btnAddSelectedCurrencies.TabIndex = 1;
			this._btnAddSelectedCurrencies.Text = "Add Selected";
			this._btnAddSelectedCurrencies.UseVisualStyleBackColor = true;
			this._btnAddSelectedCurrencies.Click += new System.EventHandler(this.OnBtnAddSelectedCurrenciesClick);
			// 
			// _btnCancel
			// 
			this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnCancel.Location = new System.Drawing.Point(3, 540);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(155, 40);
			this._btnCancel.TabIndex = 3;
			this._btnCancel.Text = "Cancel";
			this._btnCancel.UseVisualStyleBackColor = true;
			// 
			// _btnRemoveSelectedCurrencies
			// 
			this._btnRemoveSelectedCurrencies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._btnRemoveSelectedCurrencies.Location = new System.Drawing.Point(3, 494);
			this._btnRemoveSelectedCurrencies.Name = "_btnRemoveSelectedCurrencies";
			this._btnRemoveSelectedCurrencies.Size = new System.Drawing.Size(310, 40);
			this._btnRemoveSelectedCurrencies.TabIndex = 2;
			this._btnRemoveSelectedCurrencies.Text = "Remove Selected";
			this._btnRemoveSelectedCurrencies.UseVisualStyleBackColor = true;
			this._btnRemoveSelectedCurrencies.Click += new System.EventHandler(this.OnBtnRemoveSelectedCurrenciesClick);
			// 
			// CurrencySelectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(662, 608);
			this.Controls.Add(this._cntnr);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(670, 635);
			this.Name = "CurrencySelectionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Currencies";
			this._cntnr.Panel1.ResumeLayout(false);
			this._cntnr.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._cntnr)).EndInit();
			this._cntnr.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ListBox _lstUnselectedCurrencies;
		private System.Windows.Forms.ListBox _lstSelectedCurrencies;
		private System.Windows.Forms.SplitContainer _cntnr;
		private System.Windows.Forms.Button _btnAddSelectedCurrencies;
		private System.Windows.Forms.Button _btnRemoveSelectedCurrencies;
		private System.Windows.Forms.Button _btnOk;
		private System.Windows.Forms.Button _btnCancel;
	}
}