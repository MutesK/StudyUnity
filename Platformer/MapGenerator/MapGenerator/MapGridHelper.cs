using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MapGenerator
{
    class MapGridHelper
    {
        DataGridView _View;
        Color GridViewBackColor = Color.FromArgb(0, 14, 36);

        public MapGridHelper(ref DataGridView View, int Rows, int Columns)
        {
            _View = View;

            _View.DefaultCellStyle.SelectionBackColor = GridViewBackColor;
            Refresh(Rows, Columns);
        }

        public void Refresh(int Rows, int Columns)
        {
            _View.Rows.Clear();
            _View.Columns.Clear();

            _View.ColumnCount = Columns;

            // DataGridView에 빈 스트링 배열을 한 행 단위로 삽입, 이 과정이 생략되면 그리드뷰에 선이 그려지지 않음
            string[] row = { "" };
            for (int n = 0; n < Rows; n++)
                _View.Rows.Add(row);

            _View.DefaultCellStyle.Font = new Font("Consolas", 32);

            for (int i = 0; i < _View.Columns.Count; i++)
            {
                _View.Columns[i].Width = 32;
            }

            _View.Width = Math.Min((32 * Columns) + 32, 700);
            _View.Height = Math.Min((32 * Rows) + 32, 500);

            _View.ClearSelection();

            FillVoidColor();
        }

        private void FillVoidColor()
        {
            for(int row = 0; row < _View.Rows.Count; ++row)
            {
                for (int col = 0; col <_View.Columns.Count; ++col)
                {
                    _View.Rows[row].Cells[col].Style.BackColor = GridViewBackColor;
                }
            }
        }

        public void FillCellImage(int row, int col, Image image)
        {
            DataGridViewImageCell img = new DataGridViewImageCell();

            img.ImageLayout = DataGridViewImageCellLayout.Stretch;
            img.Value = image;

            _View[row, col] = img;
        }

        public void RemoveCell(int row, int col)
        {
            DataGridViewImageCell img = new DataGridViewImageCell();

            Bitmap bitmap = new Bitmap(row, col);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(GridViewBackColor);

            img.Value = bitmap;
            img.ImageLayout = DataGridViewImageCellLayout.Stretch;


            _View[row, col] = img;
            _View.Rows[row].Cells[col].Style.BackColor = GridViewBackColor;
        }
    }
}
