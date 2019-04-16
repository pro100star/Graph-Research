using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace WF {
    public partial class Visualization : Form {
        public Visualization() {
            InitializeComponent();
            GraphData.Visible = false;
        }

        private void Start_button(object sender, EventArgs e) {
            button_start.Visible = false;
            GraphData.Visible = true;
            Controls.Add(zedGraph);
            InitializeZedGraph();

            List<List<Pair<int, double>>> data = new List<List<Pair<int, double>>>();
            data.Add(new List<Pair<int, double>>());
            data[0].Add(new Pair<int, double>(0, Math.Sqrt(2)));
            data[0].Add(new Pair<int, double>(0, Math.Sqrt(3)));
            data[0].Add(new Pair<int, double>(0, Math.Sqrt(5)));
            Graph graph = new Graph(data, 1, 3);
            DrawGraph(graph, 10, 1);
        }

        private void ReadGraph() {
            //string 
        }

        private void InitializeZedGraph() {
            zedGraph.Dock = DockStyle.Fill;
            zedGraph.Location = new Point(0, 0);
            zedGraph.Name = "zedGraph";
            zedGraph.ScrollGrace = 0D;
            zedGraph.ScrollMaxX = 0D;
            zedGraph.ScrollMaxY = 0D;
            zedGraph.ScrollMaxY2 = 0D;
            zedGraph.ScrollMinX = 0D;
            zedGraph.ScrollMinY = 0D;
            zedGraph.ScrollMinY2 = 0D;
            zedGraph.Size = new Size(783, 471);
            zedGraph.TabIndex = 0;
        }

        private void DrawGraph(Graph graph, int t, int m) {
            GraphPane graphPane = zedGraph.GraphPane;
            graphPane.CurveList.Clear();

            double[] count = Array.ConvertAll(graph.Research(t, m), (int value) => (double)value);
            //double[] time = new double[count.Length];
            PointPairList points = new PointPairList();
            //for (int i = 0; i < 100; ++i) points.Add(i, i);
            int counter = 0;
            for (double i = 0; i * 100 < count.Length && counter < count.Length; i += 0.01, ++counter) {
                points.Add(i, count[counter]);
            }
            LineItem graphic = graphPane.AddCurve("Sinc", points, Color.Blue, SymbolType.None);
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }
    }
}
