using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Columns;
using System;

public class GridViewColumnButtonMenu : GridViewMenu
{
    public GridViewColumnButtonMenu(DevExpress.XtraGrid.Views.Grid.GridView view) : base(view) { }
    // Create menu items. 
    // This method is automatically called by the menu's public Init method. 
    protected override void CreateItems()
    {
        Items.Clear();
        DXSubMenuItem columnsItem = new DXSubMenuItem("Columns");
        Items.Add(columnsItem);
        Items.Add(CreateMenuItem("Runtime Column Customization", GridMenuImages.Column.Images[3],
          "Customization", true));
        foreach (GridColumn column in View.Columns)
        {
            if (column.OptionsColumn.ShowInCustomizationForm)
                columnsItem.Items.Add(CreateMenuCheckItem(column.Caption, column.VisibleIndex >= 0,
                  null, column, true));
        }
    }
    protected override void OnMenuItemClick(object sender, EventArgs e)
    {
        if (RaiseClickEvent(sender, null)) return;
        DXMenuItem item = sender as DXMenuItem;
        if (item.Tag == null) return;
        if (item.Tag is GridColumn)
        {
            GridColumn column = item.Tag as GridColumn;
            column.VisibleIndex = column.VisibleIndex >= 0 ? -1 : View.VisibleColumns.Count;
        }
        else if (item.Tag.ToString() == "Customization") View.ShowCustomization();
    }
}