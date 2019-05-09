using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace WF {
    public partial class Visualization : Form {
        Graph graph = null;

        public Visualization() {
            InitializeComponent();
            MainLabel.Visible = false;
            GraphData.Visible = false;
            GraphData.Multiline = true;
            BeginButton.Visible = false;
            TimeLabel.Visible = false;
            EndButton.Visible = false;
            TimeTextBox.Visible = false;
            CountOfMarkersLabel.Visible = false;
            CountOfMarkersTextBox.Visible = false;
            ChooseLabel.Visible = false;
            Real.Visible = false;
            Whole.Visible = false;
        }

        private void Start_button(object sender, EventArgs e) {
            button_start.Visible = false;
            Whole.Visible = true;
            Real.Visible = true;
            ChooseLabel.Visible = true;
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

            double[] count = Array.ConvertAll(graph.GetCountOfMarkers(t, m), (int value) => (double)value);
            PointPairList points = new PointPairList();
            for (int i = 0; i < count.Length; ++i) {
                points.Add(i, count[i]);
            }
            LineItem graphic = graphPane.AddCurve("Graphic", points, Color.Blue, SymbolType.None);
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }

        private void BeginButton_Click(object sender, EventArgs e) {
            int CountOfVertex;
            try {
                if (flag) {
                    graph = new Graph(ParseWhole(out CountOfVertex), CountOfVertex);
                }
                else {
                    graph = new Graph(ParseReal(out CountOfVertex), CountOfVertex);
                }
            }
            catch (FormatException ex) {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (ArgumentOutOfRangeException ex) {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
            MainLabel.Visible = false;
            GraphData.Visible = false;
            BeginButton.Visible = false;
            TimeLabel.Visible = true;
            EndButton.Visible = true;
            TimeTextBox.Visible = true;
            CountOfMarkersLabel.Visible = true;
            CountOfMarkersTextBox.Visible = true;
        }

        bool flag = true;

        private void Real_Click(object sender, EventArgs e) {
            Whole.Visible = false;
            Real.Visible = false;
            ChooseLabel.Visible = false;
            MainLabel.Visible = true;
            GraphData.Visible = true;
            BeginButton.Visible = true;
            flag = false;
        }

        private void Whole_Click(object sender, EventArgs e) {
            Whole.Visible = false;
            Real.Visible = false;
            ChooseLabel.Visible = false;
            MainLabel.Visible = true;
            GraphData.Visible = true;
            BeginButton.Visible = true;
        }

        List<List<Pair<int, int>>> ParseWhole(out int CountOfVertex) {
            var graphData = GraphData.Text.Split('\n');
            if (graphData.Length == 0) {
                throw new FormatException("Вы ничего не ввели");
            }
            if (!int.TryParse(graphData[0], out CountOfVertex) || CountOfVertex <= 0) {
                throw new FormatException("В первой строке должно содержаться колиечество вершин графа, которое обязательно больше нуля");
            }
            List<List<Pair<int, int>>> data = new List<List<Pair<int, int>>>(CountOfVertex);
            for (int i = 0; i < CountOfVertex; ++i) {
                data.Add(new List<Pair<int, int>>());
            }
            for (int i = 1; i < graphData.Length; ++i) {
                string[] tmp = graphData[i].Split(' ');
                if (tmp.Length != 3) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                if (!int.TryParse(tmp[0], out int v) || v >= CountOfVertex) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                if (!int.TryParse(tmp[1], out int to) || to >= CountOfVertex) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                if (!int.TryParse(tmp[2], out int w)) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                data[v].Add(new Pair<int, int>(to, w));
            }
            return data;
        }

        List<List<Pair<int, double>>> ParseReal(out int CountOfVertex) {
            var graphData = GraphData.Text.Split('\n');
            if (graphData.Length == 0) {
                throw new FormatException("Вы ничего не ввели");
            }
            if (!int.TryParse(graphData[0], out CountOfVertex) || CountOfVertex <= 0) {
                throw new FormatException("В первой строке должно содержаться колиечество вершин графа, которое обязательно больше нуля");
            }
            List<List<Pair<int, double>>> data = new List<List<Pair<int, double>>>(CountOfVertex);
            for (int i = 0; i < CountOfVertex; ++i) {
                data.Add(new List<Pair<int, double>>());
            }
            for (int i = 1; i < graphData.Length; ++i) {
                string[] tmp = graphData[i].Split(' ');
                if (tmp.Length != 3) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                if (!int.TryParse(tmp[0], out int v) || v >= CountOfVertex) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                if (!int.TryParse(tmp[1], out int to) || to >= CountOfVertex) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                if (!int.TryParse(tmp[2], out int w)) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                data[v].Add(new Pair<int, double>(to, w));
            }
            return data;
        }

        private void EndButton_Click(object sender, EventArgs e) {
            if (!int.TryParse(TimeTextBox.Text, out int time) || time <= 0) {
                throw new FormatException("Время должно быть строго положительным целым числом");
            }
            if (!int.TryParse(CountOfMarkersTextBox.Text, out int markers_count) || markers_count < 0) {
                throw new FormatException("Количество маркеров должно быть неотрицательным целым числом");
            }
            TimeLabel.Visible = false;
            EndButton.Visible = false;
            TimeTextBox.Visible = false;
            CountOfMarkersLabel.Visible = false;
            CountOfMarkersTextBox.Visible = false;
            Controls.Add(zedGraph);
            InitializeZedGraph();
            DrawGraph(graph, time, markers_count);
        }
    }
}
