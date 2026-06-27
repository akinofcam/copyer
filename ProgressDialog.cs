using System;
using System.Windows.Forms;
using System.Drawing;

namespace Copyer
{
    /// <summary>
    /// Windows Forms dialog showing copy progress with a progress bar.
    /// Features real-time file count, percentage, and visual progress indication.
    /// </summary>
    public class ProgressDialog : Form, IDisposable
    {
        private Label _statusLabel = null!;
        private ProgressBar _progressBar = null!;
        private Label _filesCountLabel = null!;
        private Label _percentageLabel = null!;
        private Label _speedLabel = null!;
        private long _totalFiles = 0;
        private DateTime _startTime = DateTime.Now;

        public ProgressDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form settings
            this.Text = "Copyer - Cloning Directory...";
            this.Width = 500;
            this.Height = 280;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false; // Disable close button during copy
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Icon = null; // Will use default icon

            // Title label
            var titleLabel = new Label
            {
                Text = "📁 Cloning directory...",
                Location = new Point(20, 15),
                Size = new Size(460, 30),
                AutoSize = false,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 32, 32)
            };

            // Status label
            _statusLabel = new Label
            {
                Text = "Initializing...",
                Location = new Point(20, 50),
                Size = new Size(460, 25),
                AutoSize = false,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            // Progress bar container (for better appearance)
            var progressContainer = new Panel
            {
                Location = new Point(20, 85),
                Size = new Size(460, 50),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(245, 245, 245)
            };

            // Progress bar
            _progressBar = new ProgressBar
            {
                Location = new Point(0, 5),
                Size = new Size(460, 25),
                Style = ProgressBarStyle.Continuous,
                Value = 0,
                ForeColor = Color.FromArgb(0, 120, 215)
            };

            // Percentage label
            _percentageLabel = new Label
            {
                Text = "0%",
                Location = new Point(0, 35),
                Size = new Size(460, 15),
                AutoSize = false,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(96, 96, 96),
                TextAlign = ContentAlignment.MiddleCenter
            };

            progressContainer.Controls.Add(_progressBar);
            progressContainer.Controls.Add(_percentageLabel);

            // Files count label
            _filesCountLabel = new Label
            {
                Text = "0 / 0 files",
                Location = new Point(20, 145),
                Size = new Size(460, 25),
                AutoSize = false,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            // Speed/ETA label
            _speedLabel = new Label
            {
                Text = "Speed: -- files/s | Time elapsed: 00:00",
                Location = new Point(20, 175),
                Size = new Size(460, 25),
                AutoSize = false,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(128, 128, 128)
            };

            // Info label
            var infoLabel = new Label
            {
                Text = "Please wait while files are being copied...",
                Location = new Point(20, 210),
                Size = new Size(460, 25),
                AutoSize = false,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.FromArgb(150, 150, 150)
            };

            // Add controls to form
            this.Controls.Add(titleLabel);
            this.Controls.Add(_statusLabel);
            this.Controls.Add(progressContainer);
            this.Controls.Add(_filesCountLabel);
            this.Controls.Add(_speedLabel);
            this.Controls.Add(infoLabel);
        }

        /// <summary>
        /// Sets the total number of files to copy.
        /// </summary>
        public void SetTotalFiles(long totalFiles)
        {
            _totalFiles = totalFiles;
            _startTime = DateTime.Now;

            if (_progressBar.InvokeRequired)
            {
                _progressBar.Invoke(new Action(() =>
                {
                    _progressBar.Maximum = totalFiles > 0 ? (int)Math.Min(totalFiles, int.MaxValue) : 100;
                    _filesCountLabel.Text = $"0 / {totalFiles} files";
                }));
            }
            else
            {
                _progressBar.Maximum = totalFiles > 0 ? (int)Math.Min(totalFiles, int.MaxValue) : 100;
                _filesCountLabel.Text = $"0 / {totalFiles} files";
            }
        }

        /// <summary>
        /// Updates the progress display with real-time statistics.
        /// </summary>
        public void UpdateProgress(long copiedFiles, long totalFiles)
        {
            if (_progressBar.InvokeRequired)
            {
                _progressBar.Invoke(new Action(() =>
                {
                    UpdateProgressInternal(copiedFiles, totalFiles);
                }));
            }
            else
            {
                UpdateProgressInternal(copiedFiles, totalFiles);
            }
        }

        private void UpdateProgressInternal(long copiedFiles, long totalFiles)
        {
            int progressValue = totalFiles > 0 ? (int)Math.Min((copiedFiles * 100) / totalFiles, 100) : 0;
            _progressBar.Value = Math.Min(progressValue, _progressBar.Maximum);
            _percentageLabel.Text = $"{progressValue}%";
            _filesCountLabel.Text = $"{copiedFiles} / {totalFiles} files";

            // Calculate speed and elapsed time
            TimeSpan elapsed = DateTime.Now - _startTime;
            double speed = elapsed.TotalSeconds > 0 ? copiedFiles / elapsed.TotalSeconds : 0;
            
            string speedText = speed > 0 ? $"{speed:F1} files/s" : "-- files/s";
            _speedLabel.Text = $"Speed: {speedText} | Time elapsed: {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";

            // Update status
            if (totalFiles > 0 && progressValue > 0)
            {
                double remainingFiles = totalFiles - copiedFiles;
                double secondsRemaining = speed > 0 ? remainingFiles / speed : 0;
                TimeSpan eta = TimeSpan.FromSeconds(Math.Max(0, secondsRemaining));
                
                string status = remainingFiles > 0 
                    ? $"Cloning... {copiedFiles}/{totalFiles} files | ETA: {eta.Hours:D2}:{eta.Minutes:D2}:{eta.Seconds:D2}"
                    : "Finalizing...";
                
                _statusLabel.Text = status;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Make sure dialog is on top and properly positioned
            this.TopMost = true;
            this.TopMost = false;
            this.BringToFront();
            this.Activate();
        }

        /// <summary>
        /// Completes the progress dialog display.
        /// </summary>
        public void Complete()
        {
            if (_progressBar.InvokeRequired)
            {
                _progressBar.Invoke(new Action(() =>
                {
                    _progressBar.Value = _progressBar.Maximum;
                    _percentageLabel.Text = "100%";
                    _statusLabel.Text = "✅ Copy operation completed successfully!";
                    _speedLabel.Text = "Copy operation finished.";
                }));
            }
            else
            {
                _progressBar.Value = _progressBar.Maximum;
                _percentageLabel.Text = "100%";
                _statusLabel.Text = "✅ Copy operation completed successfully!";
                _speedLabel.Text = "Copy operation finished.";
            }
        }
    }
}
