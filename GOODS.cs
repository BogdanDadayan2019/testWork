using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsCheck
{
    public class GOODS : INotifyPropertyChanged
    {
        [Key]
        public int GOODS_ID { get; set; }
        public int CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string GOODS_NAME { get; set; }
        public int GOODS_PRICE { get; set; }
        public string GOODS_DESCRIPTION { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
