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

        public int GOODS_ID
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

        public string CATEGORY_NAME
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

        public string GOODS_NAME
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

        public int GOODS_PRICE
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

        public string GOODS_DESCRIPTION
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
