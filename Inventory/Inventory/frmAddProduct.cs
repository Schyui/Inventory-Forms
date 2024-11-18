using System.Text.RegularExpressions;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        BindingSource showProductList;
        public frmAddProduct()
        {
            InitializeComponent();
            Console.WriteLine("INVENTORY");
            showProductList = new BindingSource();
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = { "Beverages", "Bread/Bakery", "Canned/Jarred Goods", "Dairy", "Frozen Goods", "Meat", "Personal Care", "Other" };
            foreach (string Category in ListOfProductCategory)
            {
                cbCategory.Items.Add(Category);
            }
        }
        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                throw new StringFormatException("Invalid Product Name Input");

            return name;

        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
                throw new NumberFormatException("Invalid Quantity Input");


            return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                throw new CurrencyFormatException("Invalid Selling Price Input");
            
            
            return Convert.ToDouble(price);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
                _ExpDate, _SellPrice, _Quantity, _Description)); gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; gridViewProductList.DataSource = showProductList;
            }
            
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);

            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            finally {
                Console.WriteLine("Proceeding...");
            }
        }
        class NumberFormatException : Exception
        {
            public NumberFormatException(string num) : base(num)
            {

            }

        }
        class StringFormatException : Exception
        {
            public StringFormatException(string str) : base(str)
            {

            }

        }
        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string currency) : base(currency)
            {

            }

        }
        private void label6_Click(object sender, EventArgs e) { }
    }
}
