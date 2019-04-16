using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace WF {
    public partial class Visualization : Form {
        public Visualization() {
            InitializeComponent();
        }

        private void Start_button(object sender, EventArgs e) {
            button_start.Visible = false;
            Controls.Add(zedGraph);
            InitializeZedGraph();

            List<List<Pair<int, int>>> data = new List<List<Pair<int, int>>>();
            data.Add(new List<Pair<int, int>>());
            data[0].Add(new Pair<int, int>(0, 3));
            data[0].Add(new Pair<int, int>(0, 5));
            data[0].Add(new Pair<int, int>(0, 7));
            Graph graph = new Graph(data, 1, 3);
            DrawGraph(graph, 50, 1);
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
            PointPairList points = new PointPairList();
            for (int i = 0; i < count.Length; ++i) {
                points.Add(i, count[i]);
            }
            LineItem graphic = graphPane.AddCurve("Graphic", points, Color.Blue, SymbolType.None);
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }
    }
}
