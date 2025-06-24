using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.CPU {
    internal class Register {
        private byte _value = 0;

        public byte Value {
            get { return _value; }
            set { _value = value; }
        }
    }
}
