using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Base;
using System.Reflection;

namespace ControlsDev
{
    public class clsGridLine
    {
        #region Functions
        public Brush GetCurrentLineColorBrush()
        {
            DevExpress.Skins.Skin currentSkin;
            DevExpress.Skins.SkinElement element;
            string elementName;

            currentSkin = DevExpress.Skins.GridSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);
            elementName = DevExpress.Skins.GridSkins.SkinGridLine;
            element = currentSkin[elementName];

            Color skinBorderColor = element.Color.BackColor;
            return new SolidBrush(skinBorderColor);
        }

        public void DrawCellBorder(int rowHandle, Rectangle rect, Graphics g)
        {
            if ((rowHandle + 1) % 5 != 0) return;
            int lineWidth = 2;
            g.FillRectangle(GetCurrentLineColorBrush(), new Rectangle(rect.X - 1, rect.Bottom - lineWidth, rect.Width + 2, lineWidth));
        }

        public void DrawCellBorder(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            Brush brush = Brushes.Black;
            e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width + 2, 2));
            e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.Right - 1, e.Bounds.Y - 1, 2, e.Bounds.Height + 2));
            e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.X - 1, e.Bounds.Bottom - 1, e.Bounds.Width + 2, 2));
            e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.X - 1, e.Bounds.Y - 1, 2, e.Bounds.Height + 2));
        }


        public void DoDefaultDrawCell(GridView view, RowCellCustomDrawEventArgs e)
        {
            GridViewInfo info = view.GetViewInfo() as GridViewInfo;
            GridCellInfo cell = e.Cell as GridCellInfo;
            GridControl grid = view.GridControl;
            PropertyInfo pi = grid.GetType().GetProperty("EditorHelper", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            GridEditorContainerHelper helper = pi.GetValue(grid, null) as GridEditorContainerHelper;
            GridViewDrawArgs args = new GridViewDrawArgs(e.Cache, info, e.Bounds);
            helper.DrawCellEdit(args, cell.Editor, cell.ViewInfo, e.Appearance, cell.CellValueRect.Location);
        }
        #endregion
    }
}
