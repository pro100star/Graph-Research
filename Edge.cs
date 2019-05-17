using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF {
    class Edge<T> {
        public int v1, v2;
        public T weight;

        public Edge(int v1, int v2, T weight) {
            this.v1 = v1;
            this.v2 = v2;
            this.weight = weight;
        }
    }
}
