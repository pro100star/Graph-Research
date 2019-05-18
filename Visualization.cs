using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        DrawGraph G;
        List<Vertex> V;
        List<Edge<int>> E_Whole;
        List<Edge<double>> E_Real;

        int time_, markers_count_;

        int selected1, selected2;

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
            saveGraphic.Location = new Point(ClientRectangle.Width * 8 / 9, ClientRectangle.Width * 8 / 9);
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
            sheet.Visible = false;
            selectButton.Visible = false;
            drawEdgeButton.Visible = false;
            drawVertexButton.Visible = false;
            deleteButton.Visible = false;
            deleteALLButton.Visible = false;
            ReadyButton.Visible = false;
            graphTypeButton.Visible = false;
            textTypeButton.Visible = false;
            selectTypeLabel.Visible = false;
            saveButton.Visible = false;
            loadFromFile.Visible = false;
            saveGraphic.Visible = false;
            OneMarkerButton.Visible = false;
            someMarkersButton.Visible = false;
            SelectMarkersLabel.Visible = false;
        }

        /// <summary>
        /// Метод для обработки события нажатия на кнопку Start_button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_button(object sender, EventArgs e) {
            button_start.Visible = false;
            //
            ChooseLabel.Visible = true;
            Real.Visible = true;
            Whole.Visible = true;
            //
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
            graphPane.XAxis.Title.Text = "Время";
            graphPane.YAxis.Title.Text = "Количество маркеров";
            double[] count = Array.ConvertAll(graph.GetCountOfMarkers(t, m), (int value) => (double)value);
            PointPairList points = new PointPairList();
            for (int i = 0; i < count.Length; ++i) {
                if (flag) {
                    points.Add(i, count[i]);
                } else {
                    points.Add(i * 1.0 / 100, count[i]);
                }
            }
            LineItem graphic = graphPane.AddCurve("Graphic", points, Color.Blue, SymbolType.None);
            _zedGraph_.AxisChange();
        }

        void InitializePictureBox() {
            V = new List<Vertex>();
            G = new DrawGraph(sheet.Width, sheet.Height);
            if (flag) {
                E_Whole = new List<Edge<int>>();
            } else {
                E_Real = new List<Edge<double>>();
            }
            sheet.Image = G.GetBitmap();
            saveButton.Visible = true;
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
            flag = false;
            /*
            OneMarkerButton.Visible = true;
            someMarkersButton.Visible = true;
            SelectMarkersLabel.Visible = true;
            */
                //
            graphTypeButton.Visible = true;
            textTypeButton.Visible = true;
            selectTypeLabel.Visible = true;
            loadFromFile.Visible = true;
            /*if (textOrGraphic) {
                MainLabel.Visible = true;
                GraphData.Visible = true;
                BeginButton.Visible = true;
            } else {
                sheet.Visible = true;
                selectButton.Visible = true;
                drawEdgeButton.Visible = true;
                drawVertexButton.Visible = true;
                deleteButton.Visible = true;
                deleteALLButton.Visible = true;
                ReadyButton.Visible = true;
                InitializePictureBox();
            }*/
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
            graphTypeButton.Visible = true;
            textTypeButton.Visible = true;
            selectTypeLabel.Visible = true;
            loadFromFile.Visible = true;
            /*if (textOrGraphic) {
                MainLabel.Visible = true;
                GraphData.Visible = true;
                BeginButton.Visible = true;
            } else {
                sheet.Visible = true;
                selectButton.Visible = true;
                drawEdgeButton.Visible = true;
                drawVertexButton.Visible = true;
                deleteButton.Visible = true;
                deleteALLButton.Visible = true;
                ReadyButton.Visible = true;
                InitializePictureBox();
            }*/
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
                if (!int.TryParse(tmp[2], out int w) || w <= 0) {
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
                throw new FormatException("В первой строке должно содержаться количество вершин графа, которое обязательно больше нуля");
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
                if (!double.TryParse(tmp[2], out double w) || w <= 0) {
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
                MessageBox.Show("Время должно быть строго положительным целым числом");
                return;
            }
            if (!flag && time >= 1000) {
                MessageBox.Show("Время в случае с вещественными весами ребер не может быть больше 1000");
                return;
            }
            if (flag && time >= 1000000) {
                MessageBox.Show("Время не может превышать 1000000 в случае с целыми весами ребер");
                return;
            }
            if (!int.TryParse(CountOfMarkersTextBox.Text, out int markers_count) || markers_count < 0) {
                MessageBox.Show("Количество маркеров должно быть неотрицательным целым числом");
                return;
            }
            OneMarkerButton.Visible = true;
            someMarkersButton.Visible = true;
            SelectMarkersLabel.Visible = true;
            time_ = time;
            markers_count_ = markers_count;

            TimeLabel.Visible = false;
            EndButton.Visible = false;
            TimeTextBox.Visible = false;
            CountOfMarkersLabel.Visible = false;
            CountOfMarkersTextBox.Visible = false;
            /*
            DrawGraph(graph, time, markers_count);
            ChangeDataListBox();
            DataListBox.Visible = true;
            _zedGraph_.Visible = true;
            saveGraphic.Visible = true;
            */
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
            DataListBox.Size = new Size(width / 2, height * 7 / 8);
            DataListBox.Location = new Point(width / 2, 0);
            saveGraphic.Location = new Point(ClientRectangle.Width * 7 / 8, ClientRectangle.Height * 8 / 9);
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
            DataListBox.Items.Add($"необходимое для выхода из вершины: {graph.CountOfMarkers_ + 1}.");
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

        bool textOrGraphic = true;

        private void textTypeButton_Click(object sender, EventArgs e) {
            loadFromFile.Visible = false;
            MainLabel.Visible = true;
            GraphData.Visible = true;
            BeginButton.Visible = true;
            graphTypeButton.Visible = false;
            textTypeButton.Visible = false;
            selectTypeLabel.Visible = false;
        }

        private void selectButton_Click(object sender, EventArgs e) {
            selectButton.Enabled = false;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            if (flag) {
                G.drawALLGraph(V, E_Whole);
            } else {
                G.drawALLGraph(V, E_Real);
            }
            sheet.Image = G.GetBitmap();
            selected1 = -1;
        }

        private void drawVertexButton_Click(object sender, EventArgs e) {
            drawVertexButton.Enabled = false;
            selectButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            if (flag) {
                G.drawALLGraph(V, E_Whole);
            } else {
                G.drawALLGraph(V, E_Real);
            }
            sheet.Image = G.GetBitmap();
        }

        private void drawEdgeButton_Click(object sender, EventArgs e) {
            drawEdgeButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            if (flag) {
                G.drawALLGraph(V, E_Whole); 
            } else {
                G.drawALLGraph(V, E_Real);
            }
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            selected2 = -1;
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            deleteButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            G.clearSheet();
            if (flag) {
                G.drawALLGraph(V, E_Whole);
            }
            else {
                G.drawALLGraph(V, E_Real);
            }
            sheet.Image = G.GetBitmap();
        }

        private void deleteALLButton_Click(object sender, EventArgs e) {
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            const string message = "Вы действительно хотите полностью удалить граф?";
            const string caption = "Удаление";
            var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MBSave == DialogResult.Yes) {
                V.Clear();
                if (flag) {
                    E_Whole.Clear();
                } else {
                    E_Real.Clear();
                }
                G.clearSheet();
                sheet.Image = G.GetBitmap();
            }
        }

        private void sheet_MouseClick(object sender, MouseEventArgs e) {
            if (selectButton.Enabled == false) {
                for (int i = 0; i < V.Count; i++) {
                    if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R) {
                        if (selected1 != -1) {
                            selected1 = -1;
                            G.clearSheet();
                            if (flag) {
                                G.drawALLGraph(V, E_Whole);
                            }
                            else {
                                G.drawALLGraph(V, E_Real);
                            }
                            sheet.Image = G.GetBitmap();
                        }
                    }
                }
            }
            //нажата кнопка "рисовать вершину"
            if (drawVertexButton.Enabled == false) {
                V.Add(new Vertex(e.X, e.Y));
                if (V.Count == 200) {
                    V.Clear();
                    if (flag) {
                        E_Whole.Clear();
                    } else {
                        E_Real.Clear();
                    }
                    graph = null;
                    G.clearSheet();
                    return;
                }
                G.drawVertex(e.X, e.Y, V.Count.ToString());
                sheet.Image = G.GetBitmap();
            }
            //нажата кнопка "рисовать ребро"
            if (drawEdgeButton.Enabled == false) {
                if (flag) {
                    if (E_Whole.Count == 200) {
                        E_Whole.Clear();
                        V.Clear();
                        graph = null;
                        G.clearSheet();
                        return;
                    }
                } else {
                    if (E_Real.Count == 200) {
                        E_Real.Clear();
                        V.Clear();
                        graph = null;
                        G.clearSheet();
                        return;
                    }
                }
                if (e.Button == MouseButtons.Left) {
                    for (int i = 0; i < V.Count; i++) {
                        if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R) {
                            if (selected1 == -1) {
                                G.drawSelectedVertex(V[i].x, V[i].y);
                                selected1 = i;
                                sheet.Image = G.GetBitmap();
                                break;
                            }
                            if (selected2 == -1) {
                                G.drawSelectedVertex(V[i].x, V[i].y);
                                selected2 = i;
                                string weight_input;
                                if (flag) {
                                    int w_in;
                                    do {
                                        weight_input = Microsoft.VisualBasic.Interaction.InputBox("Введите вес данного ребра");
                                        if (weight_input.Length == 0) {
                                            return;
                                        }
                                        if (!int.TryParse(weight_input, out w_in) || w_in <= 0) {
                                            MessageBox.Show("Ребро графа должно иметь целый положительный вес");
                                        }
                                    } while (!int.TryParse(weight_input, out w_in) || w_in <= 0);
                                    E_Whole.Add(new Edge<int>(selected1, selected2, w_in));
                                }
                                else {
                                    double w_in;
                                    do {
                                        weight_input = Microsoft.VisualBasic.Interaction.InputBox("Введите вес данного ребра");
                                        if (weight_input.Length == 0) {
                                            return;
                                        }
                                        if (!double.TryParse(weight_input, out w_in) || w_in < 0.00001) {
                                            MessageBox.Show("Ребро графа должно иметь положительный вес");
                                        }
                                    } while (!double.TryParse(weight_input, out w_in) || w_in < 0.00001);
                                    E_Real.Add(new Edge<double>(selected1, selected2, w_in));
                                }
                                if (flag) {
                                    G.drawEdge(V[selected1], V[selected2], E_Whole[E_Whole.Count - 1], E_Whole.Count - 1);
                                } else {
                                    G.drawEdge(V[selected1], V[selected2], E_Real[E_Real.Count - 1], E_Real.Count - 1);
                                }
                                selected1 = -1;
                                selected2 = -1;
                                sheet.Image = G.GetBitmap();
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right) {
                    if ((selected1 != -1) &&
                        (Math.Pow((V[selected1].x - e.X), 2) + Math.Pow((V[selected1].y - e.Y), 2) <= G.R * G.R)) {
                        G.drawVertex(V[selected1].x, V[selected1].y, (selected1 + 1).ToString());
                        selected1 = -1;
                        sheet.Image = G.GetBitmap();
                    }
                }
            }
            //нажата кнопка "удалить элемент"
            if (deleteButton.Enabled == false) {
                bool _flag_ = false; //удалили ли что-нибудь по ЭТОМУ клику
                //ищем, возможно была нажата вершина
                for (int i = 0; i < V.Count; i++) {
                    if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R) {
                        if (flag) {
                            for (int j = 0; j < E_Whole.Count; j++) {
                                if ((E_Whole[j].v1 == i) || (E_Whole[j].v2 == i)) {
                                    E_Whole.RemoveAt(j);
                                    j--;
                                }
                                else {
                                    if (E_Whole[j].v1 > i) E_Whole[j].v1--;
                                    if (E_Whole[j].v2 > i) E_Whole[j].v2--;
                                }
                            }
                        } else {
                            for (int j = 0; j < E_Real.Count; j++) {
                                if ((E_Real[j].v1 == i) || (E_Real[j].v2 == i)) {
                                    E_Real.RemoveAt(j);
                                    j--;
                                }
                                else {
                                    if (E_Real[j].v1 > i) E_Real[j].v1--;
                                    if (E_Real[j].v2 > i) E_Real[j].v2--;
                                }
                            }
                        }
                        V.RemoveAt(i);
                        _flag_ = true;
                        break;
                    }
                }
                //ищем, возможно было нажато ребро
                if (!_flag_) {
                    if (flag) {
                        for (int i = 0; i < E_Whole.Count; i++) {
                            if (E_Whole[i].v1 == E_Whole[i].v2) //если это петля
                            {
                                if ((Math.Pow((V[E_Whole[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E_Whole[i].v1].y - G.R - e.Y), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                    (Math.Pow((V[E_Whole[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E_Whole[i].v1].y - G.R - e.Y), 2) >= ((G.R - 2) * (G.R - 2)))) {
                                    E_Whole.RemoveAt(i);
                                    _flag_ = true;
                                    break;
                                }
                            }
                            else //не петля
                            {
                                if (((e.X - V[E_Whole[i].v1].x) * (V[E_Whole[i].v2].y - V[E_Whole[i].v1].y) / (V[E_Whole[i].v2].x - V[E_Whole[i].v1].x) + V[E_Whole[i].v1].y) <= (e.Y + 4) &&
                                    ((e.X - V[E_Whole[i].v1].x) * (V[E_Whole[i].v2].y - V[E_Whole[i].v1].y) / (V[E_Whole[i].v2].x - V[E_Whole[i].v1].x) + V[E_Whole[i].v1].y) >= (e.Y - 4)) {
                                    if ((V[E_Whole[i].v1].x <= V[E_Whole[i].v2].x && V[E_Whole[i].v1].x <= e.X && e.X <= V[E_Whole[i].v2].x) ||
                                        (V[E_Whole[i].v1].x >= V[E_Whole[i].v2].x && V[E_Whole[i].v1].x >= e.X && e.X >= V[E_Whole[i].v2].x)) {
                                        E_Whole.RemoveAt(i);
                                        _flag_ = true;
                                        break;
                                    }
                                }
                            }
                        }
                    } else {
                        for (int i = 0; i < E_Real.Count; i++) {
                            if (E_Real[i].v1 == E_Real[i].v2) //если это петля
                            {
                                if ((Math.Pow((V[E_Real[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E_Real[i].v1].y - G.R - e.Y), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                    (Math.Pow((V[E_Real[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E_Real[i].v1].y - G.R - e.Y), 2) >= ((G.R - 2) * (G.R - 2)))) {
                                    E_Real.RemoveAt(i);
                                    _flag_ = true;
                                    break;
                                }
                            }
                            else //не петля
                            {
                                if (((e.X - V[E_Real[i].v1].x) * (V[E_Real[i].v2].y - V[E_Real[i].v1].y) / (V[E_Real[i].v2].x - V[E_Real[i].v1].x) + V[E_Real[i].v1].y) <= (e.Y + 4) &&
                                    ((e.X - V[E_Real[i].v1].x) * (V[E_Real[i].v2].y - V[E_Real[i].v1].y) / (V[E_Real[i].v2].x - V[E_Real[i].v1].x) + V[E_Real[i].v1].y) >= (e.Y - 4)) {
                                    if ((V[E_Real[i].v1].x <= V[E_Real[i].v2].x && V[E_Real[i].v1].x <= e.X && e.X <= V[E_Real[i].v2].x) ||
                                        (V[E_Real[i].v1].x >= V[E_Real[i].v2].x && V[E_Real[i].v1].x >= e.X && e.X >= V[E_Real[i].v2].x)) {
                                        E_Real.RemoveAt(i);
                                        _flag_ = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                //если что-то было удалено, то обновляем граф на экране
                if (_flag_) {
                    G.clearSheet();
                    if (flag) {
                        G.drawALLGraph(V, E_Whole);
                    }
                    else {
                        G.drawALLGraph(V, E_Real);
                    }
                    sheet.Image = G.GetBitmap();
                }
            }
        }

        private void ReadyButton_Click(object sender, EventArgs e) {
            sheet.Visible = false;
            selectButton.Visible = false;
            drawEdgeButton.Visible = false;
            drawVertexButton.Visible = false;
            deleteButton.Visible = false;
            deleteALLButton.Visible = false;
            ReadyButton.Visible = false;
            saveButton.Visible = false;
            if (flag) {
                graph = new Graph(G.GetAdjacencyList(V.Count, E_Whole), V.Count);
            } else {
                graph = new Graph(G.GetAdjacencyList(V.Count, E_Real), V.Count);
            }
            TimeLabel.Visible = true;
            EndButton.Visible = true;
            TimeTextBox.Visible = true;
            CountOfMarkersLabel.Visible = true;
            CountOfMarkersTextBox.Visible = true;
        }

        private void saveButton_Click(object sender, EventArgs e) {
            if (sheet.Image != null) {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) {
                    try {
                        sheet.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                List<string> result = new List<string>();
                if (E_Whole != null) {
                    
                    var matr = G.GetAdjacencyList(V.Count, E_Whole);
                    result.Add(V.Count.ToString());
                    for (int i = 0; i < matr.Count; ++i) {
                        for (int j = 0; j < matr[i].Count; ++j) {
                            result.Add(i.ToString() + ' ' + matr[i][j].First.ToString() + ' ' + matr[i][j].Second.ToString());
                        }
                    }
                } else {
                    var matr = G.GetAdjacencyList(V.Count, E_Real);
                    result.Add(V.Count.ToString());
                    for (int i = 0; i < matr.Count; ++i) {
                        for (int j = 0; j < matr[i].Count; ++j) {
                            result.Add(i.ToString() + ' ' + matr[i][j].First.ToString() + ' ' + matr[i][j].Second.ToString());
                        }
                    }
                }
                savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить информацию о графе как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "All files (*.gr*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) {
                    try {
                        File.WriteAllLines(savedialog.FileName, result);
                    }
                    catch {
                        MessageBox.Show("Невозможно сохранить файл", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void loadFromFile_Click(object sender, EventArgs e) {
            string path = Microsoft.VisualBasic.Interaction.InputBox("Введите название файла формата .gr");
            if (path.Length == 0) {
                return;
            }
            if (path.Length < 3) {
                MessageBox.Show("Неверный формат");
                return;
            }
            if (path.Substring(path.Length - 3) != ".gr") {
                MessageBox.Show("Неверный формат");
                return;
            }
            if (File.Exists(path)) {
                try {
                    StreamReader sr = new StreamReader(path);
                    string tmps = sr.ReadLine();
                    if (!int.TryParse(tmps, out int count_of_vertex) || count_of_vertex <= 0) {
                        MessageBox.Show("В первой строке должно содержаться количество вершин графа");
                        return;
                    }
                    if (flag) {
                        List<List<Pair<int, int>>> data = new List<List<Pair<int, int>>>();
                        for (int i = 0; i < count_of_vertex; ++i) {
                            data.Add(new List<Pair<int, int>>());
                        }
                        int k = 0;
                        for (; !sr.EndOfStream; ++k) {
                            string s = sr.ReadLine();
                            bool f = true;
                            foreach (char c in s) {
                                if (c != ' ') {
                                    f = false;
                                    break;
                                }
                            }
                            if (f) {
                                continue;
                            }
                            string[] tmp = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (tmp.Length != 3) {
                                MessageBox.Show($"Строка №{k + 1} имеет неверный формат, повторите ввод");
                                return;
                            }
                            if (!int.TryParse(tmp[0], out int v) || v >= count_of_vertex) {
                                MessageBox.Show($"Строка №{k + 1} имеет неверный формат, повторите ввод");
                                return;
                            }
                            if (!int.TryParse(tmp[1], out int to) || to >= count_of_vertex) {
                                MessageBox.Show($"Строка №{k + 1} имеет неверный формат, повторите ввод");
                                return;
                            }
                            if (!int.TryParse(tmp[2], out int w) || w <= 0) {
                                MessageBox.Show($"Строка №{k + 1} имеет неверный формат, повторите ввод");
                                return;
                            }
                            data[v].Add(new Pair<int, int>(to, w));
                        }
                        graph = new Graph(data, count_of_vertex);
                    } else {
                        List<List<Pair<int, double>>> data = new List<List<Pair<int, double>>>();
                        for (int i = 0; i < count_of_vertex; ++i) {
                            data.Add(new List<Pair<int, double>>());
                        }
                        int k = 0;
                        for (; !sr.EndOfStream; ++k) {
                            string s = sr.ReadLine();
                            bool f = true;
                            foreach (char c in s) {
                                if (c != ' ') {
                                    f = false;
                                    break;
                                }
                            }
                            if (f) {
                                continue;
                            }
                            string[] tmp = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (tmp.Length != 3) {
                                MessageBox.Show($"Строка №{k + 1} имеет неверный формат, повторите ввод");
                                return;
                            }
                            if (!int.TryParse(tmp[0], out int v) || v >= count_of_vertex) {
                                MessageBox.Show($"Строка №{k + 1} имеет неверный формат, повторите ввод");
                                return;
                            }
                            if (!int.TryParse(tmp[1], out int to) || to >= count_of_vertex) {
                                MessageBox.Show($"Строка №{k + 1} имеет неверный формат, повторите ввод");
                                return;
                            }
                            if (!double.TryParse(tmp[2], out double w) || w <= 0) {
                                MessageBox.Show($"Строка №{k + 1} имеет неверный формат, повторите ввод");
                                return;
                            }
                            data[v].Add(new Pair<int, double>(to, w));
                        }
                        graph = new Graph(data, count_of_vertex);
                    }
                } catch (Exception ex){
                    MessageBox.Show(ex.Message);
                    return;
                }
            } else {
                MessageBox.Show("Данный файл не существует");
                return;
            }
            TimeLabel.Visible = true;
            EndButton.Visible = true;
            TimeTextBox.Visible = true;
            CountOfMarkersLabel.Visible = true;
            CountOfMarkersTextBox.Visible = true;
            loadFromFile.Visible = false;
            graphTypeButton.Visible = false;
            textTypeButton.Visible = false;
            selectTypeLabel.Visible = false;
        }

        private void saveGraphic_Click(object sender, EventArgs e) {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить график как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
            savedialog.ShowHelp = true;
            if (savedialog.ShowDialog() == DialogResult.OK) {
                try {
                    var image = _zedGraph_.GetImage();
                    image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OneMarkerButton_Click(object sender, EventArgs e) {
            TimeLabel.Visible = false;
            EndButton.Visible = false;
            TimeTextBox.Visible = false;
            CountOfMarkersLabel.Visible = false;
            CountOfMarkersTextBox.Visible = false;
            DrawGraph(graph, time_, markers_count_);
            ChangeDataListBox();
            DataListBox.Visible = true;
            _zedGraph_.Visible = true;
            saveGraphic.Visible = true;
            someMarkersButton.Visible = false;
            OneMarkerButton.Visible = false;
            SelectMarkersLabel.Visible = false;
        }

        private void someMarkersButton_Click(object sender, EventArgs e) {
            string cm;
            do {
                cm = Microsoft.VisualBasic.Interaction.InputBox("Введите, на какое ребро, сколько маркеров запускать и с каким интервалом" +
                    "в формате {исходящая вершина} {вершина, в которую приходит ребро} {количество маркеров} {интервал}\n"
                    + "Если ребра имеют целый вес, интервал может быть только целый\n"
                    + "Вершины нумеруются с нуля");
                if (cm.Length == 0) {
                    return;
                }
                var edge_ = cm.Split(' ');
                string hint = "неверный формат";
                if (edge_.Length != 4) {
                    MessageBox.Show(hint);
                    return;
                }
                if (!int.TryParse(edge_[0], out int f_v) || f_v < 0 || f_v >= graph.CountOfVertex_) {
                    MessageBox.Show(hint);
                    return;
                }
                if (!int.TryParse(edge_[1], out int s_v) || s_v < 0 || s_v >= graph.CountOfVertex_) {
                    MessageBox.Show(hint);
                    return;
                }
                if (!int.TryParse(edge_[2], out int k_m) || k_m < 0) {
                    MessageBox.Show(hint);
                    return;
                }
                if (!graph.EdgeExist(f_v, s_v)) {
                    MessageBox.Show(hint + ". Такого ребра не существует");
                    return;
                }
                if (flag) {
                    if (!int.TryParse(edge_[3], out int interv)) {
                        MessageBox.Show(hint);
                        return;
                    }
                    graph.WholeInterval = new Graph.Lauching<int>(f_v, s_v, k_m, interv); 
                } else {
                    if (!double.TryParse(edge_[3], out double interv)) {
                        MessageBox.Show(hint);
                        return;
                    }
                    graph.RealInterval = new Graph.Lauching<double>(f_v, s_v, k_m, interv);
                }
                graph.markers_impuls = false;
            } while (false);
            DrawGraph(graph, time_, markers_count_);
            ChangeDataListBox();
            DataListBox.Visible = true;
            _zedGraph_.Visible = true;
            saveGraphic.Visible = true;
            someMarkersButton.Visible = false;
            OneMarkerButton.Visible = false;
            SelectMarkersLabel.Visible = false;
        }

        private void graphTypeButton_Click(object sender, EventArgs e) {
            graphTypeButton.Visible = false;
            textTypeButton.Visible = false;
            selectTypeLabel.Visible = false;
            loadFromFile.Visible = false;
            sheet.Visible = true;
            selectButton.Visible = true;
            drawEdgeButton.Visible = true;
            drawVertexButton.Visible = true;
            deleteButton.Visible = true;
            deleteALLButton.Visible = true;
            ReadyButton.Visible = true;
            InitializePictureBox();
        }
    }
}
