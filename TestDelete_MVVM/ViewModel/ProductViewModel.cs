using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using TestDelete_MVVM.Model;
using TestDelete_MVVM.Processor;

namespace TestDelete_MVVM.ViewModel
{
    public class ProductViewModel:BaseViewModel
    {
        private ObservableCollection<Product> _List;
        public ObservableCollection<Product> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private ProductProcessor _Processor;
        public ProductProcessor Processor { get => _Processor; set => _Processor = value; }
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private Product _SelectedItem;
        public Product SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Id = SelectedItem.Id;
                    Name = SelectedItem.Name;
                }
            }
        }
        public ProductViewModel()
        {
            List=new ObservableCollection<Product>(DataProvider.Instance.Data.Products);
            Processor = new ProductProcessor(List);
            DeleteCommand = new RelayCommand<object>((p) => p!=null, Processor.DeleteProduct);
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrWhiteSpace(Name))
                    return false;

                bool exists = DataProvider.Instance.Data.Products.Any(x => x.Name == Name);
                return !exists;
            }, (p) => Processor.AddProduct(Name));
        }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
    }
}
