using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TestDelete_MVVM.Model;
using TestDelete_MVVM.ViewModel;

namespace TestDelete_MVVM.Processor
{
    public class ProductProcessor
    {
        private ObservableCollection<Product> _List;
        public ObservableCollection<Product> List{get => _List; set { _List = value; }}
        public ProductProcessor(ObservableCollection<Product> list)
        {
            this.List=list;
        }

        public void DeleteProduct(object p)
        {
            var pro = p as Product;
            if (pro != null)
            {
                var product = DataProvider.Instance.Data.Products.Find(pro.Id);
                if (product != null)
                {
                    product.IsDeleted = true;
                    DataProvider.Instance.Data.SaveChanges();
                }

                List.Remove(pro); // Xóa khỏi danh sách hiển thị
            }
        }
        public void AddProduct(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;

            var pro = new Product() { Name = name };
            DataProvider.Instance.Data.Products.Add(pro);
            DataProvider.Instance.Data.SaveChanges();

            List.Add(pro);
        }
    }
}
