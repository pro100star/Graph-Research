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
            _zedGraph_.Visible = false;
            DataListBox.Visible = false;
            _zedGraph_.Location = new Point(ClientRectangle.Width / 2, ClientRectangle.Height);
            MainLabel.Text = "Введите данные о графе.\nВ первой строчке должно содержаться количество вершин.\n" +
                "В остальных ребра в формате:\n{вершина из которой исходит ребро} {вершина, в которую идёт ребро} {вес ребра}."
                + "\nВершины нумеруются с нуля.";
            GraphData.Height = ClientRectangle.Height / 2;
            GraphData.Width = ClientRectangle.Width / 2;
        }

        private void Start_button(object sender, EventArgs e) {
            button_start.Visible = false;
            Whole.Visible = true;
            Real.Visible = true;
            ChooseLabel.Visible = true;
        }

        private void DrawGraph(Graph graph, int t, int m) {
            GraphPane graphPane = _zedGraph_.GraphPane;
            graphPane.Title.Text = "graphic";
            graphPane.XAxis.Title.Text = "Count";
            graphPane.YAxis.Title.Text = "Time";
            double[] count = Array.ConvertAll(graph.GetCountOfMarkers(t, m), (int value) => (double)value);
            PointPairList points = new PointPairList();
            for (int i = 0; i < count.Length; ++i) {
                points.Add(i, count[i]);
            }
            LineItem graphic = graphPane.AddCurve("Graphic", points, Color.Blue, SymbolType.None);
            _zedGraph_.AxisChange();
        }
        private void SetSize() {
            _zedGraph_.Location = new Point(0, 0);
            _zedGraph_.Size = new Size(ClientRectangle.Width / 2, ClientRectangle.Height);
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
            DrawGraph(graph, time, markers_count);
            DataListBox.Location = new Point(ClientRectangle.Width / 2, 0);
            DataListBox.Size = new Size(ClientRectangle.Width / 2, ClientRectangle.Height);
            ChangeDataListBox();
            DataListBox.Visible = true;
            _zedGraph_.Visible = true;
        }

        private void Visualization_Resize(object sender, EventArgs e) {
            SetSize();
            BeginButton.Location = new Point(ClientRectangle.Width / 2, ClientRectangle.Height * 3 / 4);
            button_start.Location = new Point(ClientRectangle.Width * 2 / 5, ClientRectangle.Height * 2 / 5);
            MainLabel.Location = new Point(ClientRectangle.Width / 4, ClientRectangle.Height / 7);
            GraphData.Location = new Point(ClientRectangle.Width / 4, ClientRectangle.Height / 3);
            ChooseLabel.Location = new Point(ClientRectangle.Width * 4 / 9, ClientRectangle.Height / 3);
            Real.Location = new Point(ClientRectangle.Width / 3, ClientRectangle.Height / 2);
            Whole.Location = new Point(ClientRectangle.Width * 4 / 7, ClientRectangle.Height / 2);
            int tmp1 = (ClientRectangle.Height - (GraphData.Height + GraphData.Location.Y)) / 2;
            int tmp2 = GraphData.Width / 3 + GraphData.Location.X;
            BeginButton.Location = new Point(tmp2, GraphData.Height + GraphData.Location.Y + tmp1);
            TimeTextBox.Location = new Point(ClientRectangle.Width / 4, ClientRectangle.Height / 2);
            int w = CountOfMarkersTextBox.Width;
            CountOfMarkersTextBox.Location = new Point(ClientRectangle.Width * 3 / 4 - w, ClientRectangle.Height / 2);
            w = (-TimeTextBox.Location.X + CountOfMarkersTextBox.Location.X + CountOfMarkersTextBox.Width) / 2;
            EndButton.Location = new Point(w + TimeTextBox.Location.X - EndButton.Width / 2, ClientRectangle.Height * 4 / 5);
            TimeLabel.Location = new Point(TimeTextBox.Location.X, TimeTextBox.Location.Y - 20);
            CountOfMarkersLabel.Location = new Point(CountOfMarkersTextBox.Location.X, CountOfMarkersTextBox.Location.Y - 20);
            DataListBox.Size = new Size(ClientRectangle.Width / 2, ClientRectangle.Height);
            DataListBox.Location = new Point(ClientRectangle.Width / 2, 0);
        }

        private void ChangeDataListBox() {
            DataListBox.Items.Add($"Количество вершин в графе: {graph.CountOfVertex_}.");
            DataListBox.Items.Add($"Количество ребер в графе: {graph.CountOfEdges_}.");
            DataListBox.Items.Add($"Ребра графа: ");
            bool flag = graph.WholeOrReal();
            if (flag) {
                int k = 0;
                foreach (var item in graph.GetWholeData) {
                    foreach (var edge in item) {
                        DataListBox.Items.Add(k.ToString() + " " + edge.ToString());
                    }
                    ++k;
                }
            }
            else {
                int k = 0;
                foreach (var item in graph.GetRealData) {
                    foreach (var edge in item) {
                        DataListBox.Items.Add(k.ToString() + " " + edge.ToString());
                    }
                    ++k;
                }
            }
            DataListBox.Items.Add($"Количество маркеров,");
            DataListBox.Items.Add($"необходимое для выхода из вершины: {graph.CountOfMarkers_}.");
            DataListBox.Items.Add($"Время исследования: {graph.ResearchTime_}.");
        }

        private void Visualization_Load(object sender, EventArgs e) {
            Visualization_Resize(sender, e);
        }
    }
}
