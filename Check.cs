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
        private string _check_status;

        public string CHECK_ID
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

        public DateTime CHECK_DATE
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

        public string CHECK_STATUS
        {
            get
            {
                return _check_status;
            }
            set
            {
                _check_status = value;
            }
        }

    }
}
