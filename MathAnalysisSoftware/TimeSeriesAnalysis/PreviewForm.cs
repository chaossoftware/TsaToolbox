using System;
using System.Drawing;
using System.Windows.Forms;
using MathLib.DrawEngine.Charts;

namespace TimeSeriesAnalysis
{
    public partial class PreviewForm : Form
    {

        public PlotObject plotObject;

        public PreviewForm(string caption)
        {
            InitializeComponent();
            Text = caption;
        }


        private void redraw() {
            if (plotObject != null) {
                plotObject.BitmapSize = new Size(previewPBox.Width, previewPBox.Height);
                previewPBox.Image = plotObject.Plot();
            }
        }


        private void PreviewContextMenu(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                contextMenu.Show(previewPBox, new Point(e.X, e.Y));
            }
        }


        private void ContextMenuClick(object sender, EventArgs e) {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.AddExtension = true;
            saveDialog.Filter = "Png image|*.png";
            saveDialog.ShowDialog();
            savePreview(saveDialog.FileName);
        }


        private void savePreview(string fileName) {
            if (fileName.Equals("")) {
                MessageBox.Show("Выберите путь для сохранения.");
                return;
            }
            if (previewPBox.Image != null) {
                previewPBox.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void previewPBox_SizeChanged(object sender, EventArgs e) {
            redraw();
        }

    }
}
