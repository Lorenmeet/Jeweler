using Jeweler.Database;
using Jeweler.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeweler
{
    internal class Going
    {
        
        private static Authorization _autho;
        public static Authorization autho
        {
            get
            {
                if (_autho == null)
                {
                    _autho = new Authorization();
                }
                return _autho;
            }
        }

        private readonly Database.TradeData _tradeData;
        private readonly Database.User _user;

        public static View.Product _product;
        public static View.Product product
        {
            get
            {
                if ( _product == null)
                {
                    _product = new View.Product() ;
                }
                return _product;
            }
        }
    }
}
