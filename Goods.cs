using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsCheck
{
    public class Goods
    {
        private int _goods_id;
        private string _category_name;
        private string _goods_name;
        private int _goods_price;
        private string _goods_description;

        public int Goods_id
        {
            get
            {
                return _goods_id;
            }
            set
            {
                _goods_id = value;
            }
        }

        public string Category_name
        {
            get 
            {
                return _category_name;
            }
            set
            {
                _category_name = value;
            }
        }

        public string Goods_name
        {
            get
            {
                return _goods_name;
            }
            set
            {
                _goods_name = value;
            }
        }

        public int Goods_price
        {
            get
            {
                return _goods_price;
            }
            set
            {
                _goods_price = value;
            }
        }

        public string Goods_description
        {
            get
            {
                return _goods_description;
            }
            set
            {
                _goods_description = value;
            }
        }

    }
}
