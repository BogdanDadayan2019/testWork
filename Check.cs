using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsCheck
{
    public class Check
    {
        private string _check_id;
        private DateTime _check_date;

        public string Check_id
        {
            get
            {
                return _check_id;
            }
            set
            {
                _check_id = value;
            }
        }

        public DateTime Check_date
        {
            get
            {
                return _check_date;
            }
            set
            {
                _check_date = value;
            }
        }

    }
}
