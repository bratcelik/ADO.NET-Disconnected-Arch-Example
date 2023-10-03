using System.Data;

namespace ADO.NET_Disconnected_Example
{
    public partial class WForm1 : Form
    {
        DataSet ds = new DataSet();

        public WForm1()
        {
            InitializeComponent();

            this.Load += WForm1_Load;
        }

        private void WForm1_Load(object? sender, EventArgs e)
        {
            TablolariOlustur();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


        }

        void VeriOlustur()
        {

            #region Categories Tablosuna Kategori Ekle

            DataRow newRow1 = ds.Tables["Categories"].NewRow();
            newRow1["CategoryName"] = "Bilgisayar";

            DataRow newRow2 = ds.Tables["Categories"].NewRow();
            newRow2["CategoryName"] = "Beyaz Eþya";

            DataRow newRow3 = ds.Tables["Categories"].NewRow();
            newRow3["CategoryName"] = "Elektronik";

            ds.Tables["Categories"].Rows.Add(newRow1);
            ds.Tables["Categories"].Rows.Add(newRow2);
            ds.Tables["Categories"].Rows.Add(newRow3);

            #endregion

            #region Products Tablosuna Ürünleri Ekle

            DataRow productRow3 = ds.Tables["Products"].NewRow();
            productRow3["ProductName"] = "Sony Müzik Seti";
            productRow3["CategoryID"] = 3; // Elektronik

            DataRow productRow2 = ds.Tables["Products"].NewRow();
            productRow2["ProductName"] = "Arçelik Buzdolabý";
            productRow2["CategoryID"] = 2; // Beyaz Eþya

            DataRow productRow1 = ds.Tables["Products"].NewRow();
            productRow1["ProductName"] = "Monster";
            productRow1["CategoryID"] = 1; // Bilgisayar

            ds.Tables[1].Rows.Add(productRow3);
            ds.Tables[1].Rows.Add(productRow2);
            ds.Tables[1].Rows.Add(productRow1);
            #endregion
        }


        void TablolariOlustur()
        {
            #region Categories Tablosunu Oluþtur

            DataTable categories = new DataTable("Categories");

            DataColumn categoryID = new DataColumn();
            categoryID.AllowDBNull = false;
            categoryID.AutoIncrement = true;
            categoryID.AutoIncrementSeed = 1;
            categoryID.AutoIncrementStep = 1;
            categoryID.ColumnName = "CategoryID";
            categoryID.DataType = typeof(int);

            DataColumn categoryName = new DataColumn();
            categoryName.AllowDBNull = false;
            categoryName.ColumnName = "CategoryName";
            categoryName.DataType = typeof(string);

            categories.Columns.Add(categoryID);
            categories.Columns.Add(categoryName);

            #endregion

            #region Products Tablosu Oluþtur

            DataTable products = new DataTable("Products");

            DataColumn productID = new DataColumn();
            productID.AllowDBNull = false;
            productID.AutoIncrement = true;
            productID.AutoIncrementSeed = 1;
            productID.AutoIncrementStep = 1;
            productID.ColumnName = "ProductID";
            productID.DataType = typeof(int);

            DataColumn productName = new DataColumn();
            productName.AllowDBNull = false;
            productName.ColumnName = "ProductName";
            productName.DataType = typeof(string);

            DataColumn productCategoryID = new DataColumn();
            productCategoryID.AllowDBNull = false;
            productCategoryID.AutoIncrement = true;
            productCategoryID.AutoIncrementSeed = 1;
            productCategoryID.AutoIncrementStep = 1;
            productCategoryID.ColumnName = "CategoryID";
            productCategoryID.DataType = typeof(int);

            products.Columns.Add(productID);
            products.Columns.Add(productName);
            products.Columns.Add(productCategoryID);

            #endregion

            ds.Tables.Add(categories);
            ds.Tables.Add(products);

            #region Tablolar arasý iliþkiyi oluþtur

            DataRelation drel = new DataRelation("CategoriesProduct", categoryID, productCategoryID);
            ds.Relations.Add(drel);

            #endregion
        }

        private void btnFillData_Click(object sender, EventArgs e)
        {
            VeriOlustur();

            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int categoryID = (int)dataGridView1.SelectedRows[0].Cells["CategoryID"].Value;
            DataRow[] rows = ds.Tables["Categories"].Select("CategoryID=" + categoryID);

            string productList = string.Empty;
            foreach(DataRow row in rows[0].GetChildRows("CategoriesProduct"))
            {
                productList += row["ProductName"].ToString() + Environment.NewLine;
            }

            MessageBox.Show(productList);
        }
    }
}