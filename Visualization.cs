using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace WF {
    /// <summary>
    /// Класс, описывающий оконный интерфейс
    /// </summary>
    public partial class Visualization : Form {
        /// <summary>
        /// Исследуемый граф
        /// </summary>
        Graph graph = null;

        /// <summary>
        /// Инициализация формы
        /// </summary>
        public Visualization() {
            InitializeComponent();
            MainLabel.Text = "Введите данные о графе.\nВ первой строчке должно содержаться количество вершин.\n" +
                "В остальных ребра в формате:\n{вершина из которой исходит ребро} {вершина, в которую идёт ребро} {вес ребра}."
                + "\nВершины нумеруются с нуля.";
            CountOfMarkersLabel.Text = "Введите количество маркеров,\nнеобходимое для выхода из вершины.";
            GraphData.Height = ClientRectangle.Height / 2;
            GraphData.Width = ClientRectangle.Width / 2;
            Whole.Height = Real.Height;
        }

        /// <summary>
        /// Метод для установки изначальной видимости элементов формы
        /// </summary>
        private void SetVisible() {
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
        }

        /// <summary>
        /// Метод для обработки события нажатия на кнопку Start_button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_button(object sender, EventArgs e) {
            button_start.Visible = false;
            Whole.Visible = true;
            Real.Visible = true;
            ChooseLabel.Visible = true;
        }

        /// <summary>
        /// Метод, рисующий график зависимости количества маркеров на графе от времени
        /// </summary>
        /// <param name="graph">
        /// Исследуемый граф
        /// </param>
        /// <param name="t">
        /// Время исследования
        /// </param>
        /// <param name="m">
        /// Количество маркеров, необходимое для выхода из вершины
        /// </param>
        private void DrawGraph(Graph graph, int t, int m) {
            GraphPane graphPane = _zedGraph_.GraphPane;
            graphPane.Title.Text = "Зависимость количества маркеров от времени";
            graphPane.XAxis.Title.Text = "Количество маркеров";
            graphPane.YAxis.Title.Text = "Время";
            double[] count = Array.ConvertAll(graph.GetCountOfMarkers(t, m), (int value) => (double)value);
            PointPairList points = new PointPairList();
            for (int i = 0; i < count.Length; ++i) {
                points.Add(i, count[i]);
            }
            LineItem graphic = graphPane.AddCurve("Graphic", points, Color.Blue, SymbolType.None);
            _zedGraph_.AxisChange();
        }

        /// <summary>
        /// Метод для установки размера и положения графика
        /// </summary>
        private void SetSize() {
            _zedGraph_.Location = new Point(0, 0);
            _zedGraph_.Size = new Size(ClientRectangle.Width / 2, ClientRectangle.Height);
        }

        /// <summary>
        /// Метод для обработки события нажатия на кнопку BeginButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Булево значения, равное true, если веса ребер графа имеют целочисленный тип, иначе false
        /// </summary>
        bool flag = true;

        /// <summary>
        /// Метод для обработки события нажатия на кнопку Real
        /// Т.о., пользователь выбирает граф с вещественными ребрами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Real_Click(object sender, EventArgs e) {
            Whole.Visible = false;
            Real.Visible = false;
            ChooseLabel.Visible = false;
            MainLabel.Visible = true;
            GraphData.Visible = true;
            BeginButton.Visible = true;
            flag = false;
        }

        /// <summary>
        /// Метод для обработки события нажатия на кнопку Whole
        /// Т.о., пользователь выбирает граф с целыми ребрами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Whole_Click(object sender, EventArgs e) {
            Whole.Visible = false;
            Real.Visible = false;
            ChooseLabel.Visible = false;
            MainLabel.Visible = true;
            GraphData.Visible = true;
            BeginButton.Visible = true;
        }

        /// <summary>
        /// Метод для обработки данных графа с целочисленными весами ребер
        /// </summary>
        /// <param name="CountOfVertex">
        /// Количество вершин графа
        /// </param>
        /// <returns>
        /// Список смежности графа
        /// </returns>
        List<List<Pair<int, int>>> ParseWhole(out int CountOfVertex) {
            var graphData = GraphData.Text.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
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
                bool f = true;
                foreach (char c in graphData[i]) {
                    if (c != ' ') {
                        f = false;
                    }
                }
                if (f) {
                    continue;
                }
                string[] tmp = graphData[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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

        /// <summary>
        /// Метод для обработки данных графа с вещественными весами ребер
        /// </summary>
        /// <param name="CountOfVertex">
        /// Количество вершин графа
        /// </param>
        /// <returns>
        /// Список смежности графа
        /// </returns>
        List<List<Pair<int, double>>> ParseReal(out int CountOfVertex) {
            var graphData = GraphData.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
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
                bool f = true;
                foreach (char c in graphData[i]) {
                    if (c != ' ') {
                        f = false;
                        break;
                    }
                }
                if (f) {
                    continue;
                }
                string[] tmp = graphData[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tmp.Length != 3) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                if (!int.TryParse(tmp[0], out int v) || v >= CountOfVertex) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                if (!int.TryParse(tmp[1], out int to) || to >= CountOfVertex) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                if (!double.TryParse(tmp[2], out double w)) {
                    throw new FormatException($"Строка №{i + 1} имеет неверный формат, повторите ввод");
                }
                data[v].Add(new Pair<int, double>(to, w));
            }
            return data;
        }

        /// <summary>
        /// Метод для обработки события нажатия на кнопку EndButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            ChangeDataListBox();
            DataListBox.Visible = true;
            _zedGraph_.Visible = true;
        }

        /// <summary>
        /// Метод, изменяющий положения элементов формы при изменении её размера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Visualization_Resize(object sender, EventArgs e) {
            SetSize();
            int width = ClientRectangle.Width;
            int height = ClientRectangle.Height;
            BeginButton.Location = new Point(width / 2, height * 3 / 4);
            button_start.Location = new Point(width * 2 / 5, height * 2 / 5);
            MainLabel.Location = new Point(width / 4, height / 7);
            GraphData.Location = new Point(width / 4, height / 3);
            Real.Location = new Point(width / 3, height / 2);
            Whole.Location = new Point(width * 4 / 7, height / 2);
            int place = (-Whole.Location.X + Real.Location.X + Real.Width) / 2;
            ChooseLabel.Location = new Point(Whole.Location.X + place - ChooseLabel.Width / 2, height / 3);
            int tmp1 = (height - (GraphData.Height + GraphData.Location.Y)) / 2;
            int tmp2 = GraphData.Width / 3 + GraphData.Location.X;
            BeginButton.Location = new Point(tmp2, GraphData.Height + GraphData.Location.Y + tmp1);
            TimeTextBox.Location = new Point(width / 4, height / 2);
            int w = CountOfMarkersTextBox.Width;
            CountOfMarkersTextBox.Location = new Point(width * 3 / 4 - w, height / 2);
            w = (-TimeTextBox.Location.X + CountOfMarkersTextBox.Location.X + CountOfMarkersTextBox.Width) / 2;
            EndButton.Location = new Point(w + TimeTextBox.Location.X - EndButton.Width / 2, height * 4 / 5);
            TimeLabel.Location = new Point(TimeTextBox.Location.X, TimeTextBox.Location.Y - 30);
            CountOfMarkersLabel.Location = new Point(CountOfMarkersTextBox.Location.X, CountOfMarkersTextBox.Location.Y - 30);
            DataListBox.Size = new Size(width / 2, height);
            DataListBox.Location = new Point(width / 2, 0);
        }

        /// <summary>
        /// Метод заполняющий DataListBox, в котором отображаются заданные пользователем данные о графе
        /// </summary>
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

        /// <summary>
        /// Метод, в котором устанавливается начальное состояние формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Visualization_Load(object sender, EventArgs e) {
            Visualization_Resize(sender, e);
            SetVisible();
        }
    }
}
