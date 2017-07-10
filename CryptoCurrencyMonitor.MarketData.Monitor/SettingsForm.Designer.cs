namespace CryptoCurrencyMonitor.MarketData.Monitor {
	partial class SettingsForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			this._btnOk = new System.Windows.Forms.Button();
			this._btnCancel = new System.Windows.Forms.Button();
			this._lblRefreshInterval = new System.Windows.Forms.Label();
			this._txtRefreshInterval = new System.Windows.Forms.TextBox();
			this._lblRefreshIntervalSeconds = new System.Windows.Forms.Label();
			this._grpCoinDisplayFormat = new System.Windows.Forms.GroupBox();
			this._radioSymbolAndName = new System.Windows.Forms.RadioButton();
			this._radioSymbol = new System.Windows.Forms.RadioButton();
			this._radioNameAndSymbol = new System.Windows.Forms.RadioButton();
			this._radioName = new System.Windows.Forms.RadioButton();
			this._lblCoinDisplayFormat = new System.Windows.Forms.Label();
			this._grpCoinDisplayFormat.SuspendLayout();
			this.SuspendLayout();
			// 
			// _btnOk
			// 
			this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._btnOk.Location = new System.Drawing.Point(12, 149);
			this._btnOk.Name = "_btnOk";
			this._btnOk.Size = new System.Drawing.Size(154, 40);
			this._btnOk.TabIndex = 3;
			this._btnOk.Text = "Ok";
			this._btnOk.UseVisualStyleBackColor = true;
			// 
			// _btnCancel
			// 
			this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnCancel.Location = new System.Drawing.Point(186, 149);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(154, 40);
			this._btnCancel.TabIndex = 4;
			this._btnCancel.Text = "Cancel";
			this._btnCancel.UseVisualStyleBackColor = true;
			// 
			// _lblRefreshInterval
			// 
			this._lblRefreshInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._lblRefreshInterval.AutoSize = true;
			this._lblRefreshInterval.Location = new System.Drawing.Point(26, 9);
			this._lblRefreshInterval.Name = "_lblRefreshInterval";
			this._lblRefreshInterval.Size = new System.Drawing.Size(85, 13);
			this._lblRefreshInterval.TabIndex = 5;
			this._lblRefreshInterval.Text = "Refresh Interval:";
			// 
			// _txtRefreshInterval
			// 
			this._txtRefreshInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtRefreshInterval.Location = new System.Drawing.Point(117, 6);
			this._txtRefreshInterval.Name = "_txtRefreshInterval";
			this._txtRefreshInterval.Size = new System.Drawing.Size(170, 20);
			this._txtRefreshInterval.TabIndex = 6;
			this._txtRefreshInterval.Validating += new System.ComponentModel.CancelEventHandler(this.OnTxtRefreshIntervalValidating);
			// 
			// _lblRefreshIntervalSeconds
			// 
			this._lblRefreshIntervalSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._lblRefreshIntervalSeconds.AutoSize = true;
			this._lblRefreshIntervalSeconds.Location = new System.Drawing.Point(293, 9);
			this._lblRefreshIntervalSeconds.Name = "_lblRefreshIntervalSeconds";
			this._lblRefreshIntervalSeconds.Size = new System.Drawing.Size(47, 13);
			this._lblRefreshIntervalSeconds.TabIndex = 7;
			this._lblRefreshIntervalSeconds.Text = "seconds";
			// 
			// _grpCoinDisplayFormat
			// 
			this._grpCoinDisplayFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._grpCoinDisplayFormat.Controls.Add(this._radioSymbolAndName);
			this._grpCoinDisplayFormat.Controls.Add(this._radioSymbol);
			this._grpCoinDisplayFormat.Controls.Add(this._radioNameAndSymbol);
			this._grpCoinDisplayFormat.Controls.Add(this._radioName);
			this._grpCoinDisplayFormat.Location = new System.Drawing.Point(117, 32);
			this._grpCoinDisplayFormat.Name = "_grpCoinDisplayFormat";
			this._grpCoinDisplayFormat.Size = new System.Drawing.Size(223, 103);
			this._grpCoinDisplayFormat.TabIndex = 8;
			this._grpCoinDisplayFormat.TabStop = false;
			// 
			// _radioSymbolAndName
			// 
			this._radioSymbolAndName.AutoSize = true;
			this._radioSymbolAndName.Location = new System.Drawing.Point(7, 79);
			this._radioSymbolAndName.Name = "_radioSymbolAndName";
			this._radioSymbolAndName.Size = new System.Drawing.Size(106, 17);
			this._radioSymbolAndName.TabIndex = 3;
			this._radioSymbolAndName.TabStop = true;
			this._radioSymbolAndName.Text = "\"Symbol (Name)\"";
			this._radioSymbolAndName.UseVisualStyleBackColor = true;
			// 
			// _radioSymbol
			// 
			this._radioSymbol.AutoSize = true;
			this._radioSymbol.Location = new System.Drawing.Point(7, 56);
			this._radioSymbol.Name = "_radioSymbol";
			this._radioSymbol.Size = new System.Drawing.Size(69, 17);
			this._radioSymbol.TabIndex = 2;
			this._radioSymbol.TabStop = true;
			this._radioSymbol.Text = "\"Symbol\"";
			this._radioSymbol.UseVisualStyleBackColor = true;
			// 
			// _radioNameAndSymbol
			// 
			this._radioNameAndSymbol.AutoSize = true;
			this._radioNameAndSymbol.Location = new System.Drawing.Point(7, 33);
			this._radioNameAndSymbol.Name = "_radioNameAndSymbol";
			this._radioNameAndSymbol.Size = new System.Drawing.Size(106, 17);
			this._radioNameAndSymbol.TabIndex = 1;
			this._radioNameAndSymbol.TabStop = true;
			this._radioNameAndSymbol.Text = "\"Name (Symbol)\"";
			this._radioNameAndSymbol.UseVisualStyleBackColor = true;
			// 
			// _radioName
			// 
			this._radioName.AutoSize = true;
			this._radioName.Location = new System.Drawing.Point(7, 10);
			this._radioName.Name = "_radioName";
			this._radioName.Size = new System.Drawing.Size(63, 17);
			this._radioName.TabIndex = 0;
			this._radioName.TabStop = true;
			this._radioName.Text = "\"Name\"";
			this._radioName.UseVisualStyleBackColor = true;
			// 
			// _lblCoinDisplayFormat
			// 
			this._lblCoinDisplayFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._lblCoinDisplayFormat.AutoSize = true;
			this._lblCoinDisplayFormat.Location = new System.Drawing.Point(8, 32);
			this._lblCoinDisplayFormat.Name = "_lblCoinDisplayFormat";
			this._lblCoinDisplayFormat.Size = new System.Drawing.Size(103, 13);
			this._lblCoinDisplayFormat.TabIndex = 9;
			this._lblCoinDisplayFormat.Text = "Coin Display Format:";
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 201);
			this.Controls.Add(this._lblCoinDisplayFormat);
			this.Controls.Add(this._grpCoinDisplayFormat);
			this.Controls.Add(this._lblRefreshIntervalSeconds);
			this.Controls.Add(this._txtRefreshInterval);
			this.Controls.Add(this._lblRefreshInterval);
			this.Controls.Add(this._btnCancel);
			this.Controls.Add(this._btnOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(360, 228);
			this.MinimumSize = new System.Drawing.Size(360, 228);
			this.Name = "SettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this._grpCoinDisplayFormat.ResumeLayout(false);
			this._grpCoinDisplayFormat.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _btnOk;
		private System.Windows.Forms.Button _btnCancel;
		private System.Windows.Forms.Label _lblRefreshInterval;
		private System.Windows.Forms.TextBox _txtRefreshInterval;
		private System.Windows.Forms.Label _lblRefreshIntervalSeconds;
		private System.Windows.Forms.GroupBox _grpCoinDisplayFormat;
		private System.Windows.Forms.RadioButton _radioSymbolAndName;
		private System.Windows.Forms.RadioButton _radioSymbol;
		private System.Windows.Forms.RadioButton _radioNameAndSymbol;
		private System.Windows.Forms.RadioButton _radioName;
		private System.Windows.Forms.Label _lblCoinDisplayFormat;
	}
}