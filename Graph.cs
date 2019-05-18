using System;
using System.Collections.Generic;

namespace WF {
    /// <summary>
    /// Предикат, принимающий один целочисленный параметр, возвращающий bool
    /// </summary>
    /// <param name="k">
    /// Целочисленный параметр
    /// </param>
    /// <returns>
    /// true или false
    /// </returns>
    delegate bool Predicate(int k);

    /// <summary>
    /// Класс, описывающий сильносвязный ориентированный граф
    /// </summary>
    public class Graph {
        /// <summary>
        /// Список смежности графа с целочисленными весами ребёр
        /// </summary>
        List<List<Pair<int, int>>> IntData = null;

        /// <summary>
        /// Список смежности графа с вещественными весами ребёр
        /// </summary>
        List<List<Pair<int, double>>> DoubleData = null;

        /// <summary>
        /// Adjacency matrix
        /// </summary>
        public readonly int[][] Matrix;

        /// <summary>
        /// Количество вершин в графе
        /// </summary>
        int CountOfVertex = 0;

        /// <summary>
        /// Количество ребёр в графе
        /// </summary>
        int CountOfEdges = 0;

        /// <summary>
        /// Условие для выхода маркеров из вершины
        /// </summary>
        Predicate Condition = (k) => k >= 2;

        /// <summary>
        /// Cписок маркеров, идущих в вершину, в котором хранится значение, оставшееся до достижения вершины
        /// Граф имеет целочисленные веса ребер
        /// </summary>
        List<List<int>> markers = new List<List<int>>();

        /// <summary>
        /// Cписок маркеров, идущих в вершину, в котором хранится значение, оставшееся до достижения вершины
        /// Граф имеет вещественные веса ребер
        /// </summary>
        List<List<double>> double_markers;

        /// <summary>
        /// Количество маркеров в зависимости от времени
        /// </summary>
        List<int> counter = new List<int>();

        /// <summary>
        /// Время исследования
        /// </summary>
        int ResearchTime = 0;

        /// <summary>
        /// Количество маркеров, необходимое для выхода из вершины
        /// </summary>
        int CountOfMarkers = 0;

        /// <summary>
        /// Конструктор без параметров, создающий пустой граф
        /// </summary>
        public Graph() {
            Matrix = new int[0][];
        }

        /// <summary>
        /// Конструктор графа от списка смежности
        /// </summary>
        /// <param name="data">
        /// Список смежности графа с целочисленными ребрами
        /// </param>
        /// <param name="n">
        /// Количество вершин
        /// </param>
        public Graph(List<List<Pair<int, int>>> data, int n) {
            if (data.Count != n) {
                throw new ArgumentOutOfRangeException("Count of vertex is not equal to graph");
            }
            IntData = new List<List<Pair<int, int>>>(n);
            Matrix = new int[n][];
            for (int i = 0; i < data.Count; ++i) {
                IntData.Add(new List<Pair<int, int>>(data[i].Count));
                CountOfEdges += data[i].Count;
                IntData[i].AddRange(data[i]);
                Matrix[i] = new int[n];
                for (int j = 0; j < data[i].Count; ++j) {
                    Matrix[i][data[i][j].First] = 1;
                }
            }
            CountOfVertex = n;
            if (!StrongConnectivityCheck()) {
                throw new ArgumentOutOfRangeException("Граф должен быть связным");
            }
        }

        /// <summary>
        /// Конструктор графа от списка смежности
        /// </summary>
        /// <param name="data">
        /// Список смежности графа с вещественными ребрами
        /// </param>
        /// <param name="n">
        /// Количество вершин
        /// </param>
        public Graph(List<List<Pair<int, double>>> data, int n) {
            if (data.Count != n) {
                throw new ArgumentOutOfRangeException("Count of vertex is not equal to graph");
            }
            DoubleData = new List<List<Pair<int, double>>>(n);
            Matrix = new int[n][];
            for (int i = 0; i < data.Count; ++i) {
                DoubleData.Add(new List<Pair<int, double>>(data[i].Count));
                CountOfEdges += data[i].Count;
                DoubleData[i].AddRange(data[i]);
                Matrix[i] = new int[n];
                for (int j = 0; j < data[i].Count; ++j) {
                    Matrix[i][data[i][j].First] = 1;
                }
            }
            CountOfVertex = n;
            if (!StrongConnectivityCheck()) {
                throw new ArgumentOutOfRangeException("Граф должен быть сильно-связным");
            }
        }

        /// <summary>
        /// Конструктор графа от списка смежности с целочисленными весами ребер
        /// </summary>
        /// <param name="data">
        /// Список смежности графа с целочисленными весами ребер
        /// </param>
        public Graph(List<List<Pair<int, int>>> data) {
            CountOfVertex = data.Count;
            IntData = new List<List<Pair<int, int>>>(CountOfVertex);
            for (int i = 0; i < CountOfVertex; ++i) {
                IntData.Add(new List<Pair<int, int>>());
                for (int j = 0; j < data[i].Count; ++j) {
                    var p = data[i][j];
                    IntData[i].Add(new Pair<int, int>(p.First, p.Second));
                } 
            }
        }

        /// <summary>
        /// Конструктор графа от списка смежности с целочисленными весами ребер
        /// </summary>
        /// <param name="data">
        /// Список смежности графа с вещественными весами ребер
        /// </param>
        public Graph(List<List<Pair<int, double>>> data) {
            CountOfVertex = data.Count;
            DoubleData = new List<List<Pair<int, double>>>(CountOfVertex);
            for (int i = 0; i < CountOfVertex; ++i) {
                DoubleData.Add(new List<Pair<int, double>>());
                for (int j = 0; j < data[i].Count; ++j) {
                    var p = data[i][j];
                    DoubleData[i].Add(new Pair<int, double>(p.First, p.Second));
                }
            }
        }

        /// <summary>
        /// Метод для проверки графа на сильную связность
        /// </summary>
        /// <returns>
        /// true, если граф сильно связный, иначе false
        /// </returns>
        bool StrongConnectivityCheck() {
            int[] times = new int[CountOfVertex];
            bool[] used = new bool[CountOfVertex];
            Dfs(0, CreateInvertedGraph(), times, used, 0);
            Pair<int, int>[] pairs = new Pair<int, int>[CountOfVertex];
            for (int i = 0; i < CountOfVertex; ++i) {
                pairs[i] = new Pair<int, int>(times[i], i);
            }
            Array.Sort(pairs, (lhs, rhs) => {
                if (lhs.First > rhs.First) {
                    return -1;
                } else if (lhs.First < rhs.First) {
                    return 1;
                }
                if (lhs.Second == rhs.Second) {
                    return 0;
                }
                if (lhs.Second > rhs.Second) {
                    return -1;
                }
                return 1;
            });
            List<List<int>> components = new List<List<int>>();
            used = new bool[CountOfVertex];
            for (int i = 0; i < CountOfVertex; ++i) {
                if (!used[i]) {
                    components.Add(new List<int>());
                    Dfs(i, used, components[components.Count - 1]);
                }
            }
            return components.Count == 1;
        }
        
        /// <summary>
        /// Метод для получения количества маркеров в зависимости от времени
        /// </summary>
        /// <param name="Time">
        /// Время исследования
        /// </param>
        /// <param name="CountOfMarkersToGo">
        /// Количество маркеров, необходимое для выхода
        /// </param>
        /// <returns>
        /// Массив, в котором хранится количество маркеров в зависимости от времени
        /// </returns>
        public int[] GetCountOfMarkers(int Time, int CountOfMarkersToGo) {
            if (DoubleData == null) {
                return GetCountOfMarkersWhole(Time, CountOfMarkersToGo - 1);
            }
            return GetCountOfMarkersReal(Time, CountOfMarkersToGo - 1);
        }

        /// <summary>
        /// Метод для получения количества маркеров в зависимости от времени графа с целочисленными весами ребер
        /// </summary>
        /// <param name="Time">
        /// Время исследования
        /// </param>
        /// <param name="CountOfMarkersToGo">
        /// Количество маркеров, необходимое для выхода
        /// </param>
        /// <returns>
        /// Массив, в котором хранится количество маркеров в зависимости от времени
        /// </returns>
        int[] GetCountOfMarkersWhole(int Time, int CountOfMarkersToGo) {
            if (Time <= 0) {
                throw new ArgumentOutOfRangeException(@"Invalid value of the time, it can't be \leq 0");
            }
            if (CountOfMarkersToGo < 0) {
                throw new ArgumentOutOfRangeException(@"Invalid value of count of markers to go, it can't be less than 0");
            }
            ResearchTime = Time;
            CountOfMarkers = CountOfMarkersToGo;
            Condition = (k) => k > CountOfMarkersToGo;
            int[] Result = new int[Time];
            int[] Counter = new int[CountOfVertex];
            List<List<int>> Markers = new List<List<int>>(CountOfVertex);
            for (int i = 0; i < CountOfVertex; ++i) {
                Markers.Add(new List<int>());
            }
            for (int i = 0; i < CountOfVertex; ++i) {
                Counter[i] = 0;
            }
            int t = 0;
            int _count_ = 0;
            int f_v = 0, s_v = 0;
            int w = 0;
            if (markers_impuls) {
                for (int i = 0; i < CountOfVertex; ++i) {
                    for (int j = 0; j < IntData[i].Count; ++j) {
                        Markers[IntData[i][j].First].Add(IntData[i][j].Second);
                    }
                }
            } else {
                f_v = WholeInterval.first_vertex;
                s_v = WholeInterval.second_vertex;
                w = GetWeightWhole(f_v, s_v);
                Markers[s_v].Add(w);
                t = WholeInterval.interval;
                _count_ = WholeInterval.count;
                --_count_;
            }
            int NowCount = CountOfEdges;
            if (markers_impuls) {
                NowCount = CountOfEdges;
            } else {
                NowCount = 1;
            }
            for (int i = 0; i < Time; ++i) {
                Result[i] = NowCount;
                if (!markers_impuls) {
                    if (t == 0) {
                        if (_count_ != 0) {
                            Markers[s_v].Add(w);
                            ++Result[i];
                            --_count_;
                            t = WholeInterval.interval;
                        } 
                    }
                }
                for (int j = 0; j < CountOfVertex; ++j) {
                    int count = Markers[j].RemoveAll((k) => k == 0);
                    Counter[j] += count;
                    Result[i] -= count;
                    int count_ = Update(j, Condition(Counter[j]), Markers);
                    if (count_ != 0) {
                        Counter[j] -= count;
                    }
                    Result[i] += count_;
                    for (int z = 0; z < Markers[j].Count; ++z) {
                        --Markers[j][z];
                    }
                }
                NowCount = Result[i];
                if (!markers_impuls) {
                    --t;
                }
            }
            return Result;
        }

        /// <summary>
        /// Метод для получения количества маркеров в зависимости от времени графа с вещественными весами ребер
        /// </summary>
        /// <param name="Time">
        /// Время исследования
        /// </param>
        /// <param name="CountOfMarkersToGo">
        /// Количество маркеров, необходимое для выхода
        /// </param>
        /// <returns>
        /// Массив, в котором хранится количество маркеров в зависимости от времени
        /// </returns>
        int[] GetCountOfMarkersReal(int Time, int CountOfMarkersToGo) {
            if (Time <= 0) {
                throw new ArgumentOutOfRangeException(@"Invalid value of the time, it can't be \leq 0");
            }
            if (CountOfMarkersToGo < 0) {
                throw new ArgumentOutOfRangeException(@"Invalid value of count of markers to go, it can't be less than 0");
            }
            ResearchTime = Time;
            CountOfMarkers = CountOfMarkersToGo;
            Condition = (count) => count > CountOfMarkersToGo;
            int[] Result = new int[Time * 100];
            int[] Counter = new int[CountOfVertex];
            List<List<double>> Markers = new List<List<double>>(CountOfVertex);
            for (int i = 0; i < CountOfVertex; ++i) {
                Markers.Add(new List<double>());
            }
            for (int i = 0; i < CountOfVertex; ++i) {
                Counter[i] = 0;
            }
            double t = 0;
            int _count_ = 0;
            int f_v = 0, s_v = 0;
            double w = 0;
            if (markers_impuls) {
                for (int i = 0; i < CountOfVertex; ++i) {
                    for (int j = 0; j < DoubleData[i].Count; ++j) {
                        Markers[DoubleData[i][j].First].Add(DoubleData[i][j].Second);
                    }
                }
            } else {
                f_v = RealInterval.first_vertex;
                s_v = RealInterval.second_vertex;
                w = GetWeightReal(f_v, s_v);
                Markers[s_v].Add(w);
                t = RealInterval.interval;
                _count_ = RealInterval.count;
                --_count_;
            }
            int NowCount = CountOfEdges;
            if (markers_impuls) {
                NowCount = CountOfEdges;
            }
            else {
                NowCount = 1;
            }
            for (int i = 0; i < Result.Length; ++i) {
                Result[i] = NowCount;
                if (!markers_impuls) {
                    if (t < 0.00001) {
                        if (_count_ != 0) {
                            Markers[s_v].Add(w);
                            ++Result[i];
                            --_count_;
                            t = WholeInterval.interval;
                        }
                    }
                }
                for (int j = 0; j < CountOfVertex; ++j) {
                    int count = Markers[j].RemoveAll((k) => k < 0.00000000001);
                    Counter[j] += count;
                    Result[i] -= count;
                    int count_ = Update(j, Condition(Counter[j]), Markers);
                    if (count_ != 0) {
                        Counter[j] -= count;
                    }
                    Result[i] += count;
                    for (int z = 0; z < Markers[j].Count; ++z) {
                        Markers[j][z] -= 0.01;
                    }
                }
                NowCount = Result[i];
                if (!markers_impuls) {
                    t -= 0.01;
                }
            }
            return Result;
        }

        /// <summary>
        /// Метод для запуска новых маркеров в зависимости от условия f
        /// </summary>
        /// <param name="vertex">
        /// Вершина, которую пытаются обновить
        /// </param>
        /// <param name="f">
        /// Булев флаг, указывающий обновлять вершину или нет
        /// </param>
        /// <param name="Markers">
        /// Список маркеров, который обновляют
        /// Граф имеет целочисленный вес ребер
        /// </param>
        /// <returns>
        /// Количество вышедших маркеров
        /// </returns>
        int Update(int vertex, bool f, List<List<int>> Markers) {
            if (f) {
                for (int j = 0; j < IntData[vertex].Count; ++j) {
                    int v = IntData[vertex][j].First;
                    int w = IntData[vertex][j].Second;
                    Markers[v].Add(w);
                }
                return IntData[vertex].Count;
            }
            return 0;
        }

        /// <summary>
        /// Метод для запуска новых маркеров в зависимости от условия f
        /// </summary>
        /// <param name="vertex">
        /// Вершина, которую пытаются обновить
        /// </param>
        /// <param name="f">
        /// Булев флаг, указывающий обновлять вершину или нет
        /// </param>
        /// <param name="Markers">
        /// Список маркеров, который обновляют
        /// Граф имеет вещественный вес ребер
        /// </param>
        /// <returns>
        /// Количество вышедших маркеров
        /// </returns>
        int Update(int vertex, bool f, List<List<double>> Markers) {
            if (f) {
                for (int j = 0; j < DoubleData[vertex].Count; ++j) {
                    int v = DoubleData[vertex][j].First;
                    double w = DoubleData[vertex][j].Second;
                    Markers[v].Add(w);
                }
                return DoubleData[vertex].Count;
            }
            return 0;
        }

        /// <summary>
        /// Метод для получения графа с инвертированными ребрами от текущего графа
        /// </summary>
        /// <returns>
        /// Граф с инвертированными ребрами
        /// </returns>
        Graph CreateInvertedGraph() {
            Graph result = null;
            if (IntData != null) {
                List<List<Pair<int, int>>> data = new List<List<Pair<int, int>>>(CountOfVertex);
                for (int i = 0; i < CountOfVertex; ++i) {
                    data.Add(new List<Pair<int, int>>());
                }
                for (int i = 0; i < CountOfVertex; ++i) {
                    for (int j = 0; j < IntData[i].Count; ++j) {
                        var p = IntData[i][j];
                        data[p.First].Add(new Pair<int, int>(i, p.Second));
                    }
                }
                result = new Graph(data);
            }
            else {
                List<List<Pair<int, double>>> data = new List<List<Pair<int, double>>>(CountOfVertex);
                for (int i = 0; i < CountOfVertex; ++i) {
                    data.Add(new List<Pair<int, double>>());
                }
                for (int i = 0; i < CountOfVertex; ++i) {
                    for (int j = 0; j < DoubleData[i].Count; ++j) {
                        var p = DoubleData[i][j];
                        data[p.First].Add(new Pair<int, double>(i, p.Second));
                    }
                }
                result = new Graph(data);
            }
            return result;
        }

        /// <summary>
        /// Свойство для получения матрицы смежности
        /// </summary>
        public List<List<int>> GetData {
            get {
                List<List<int>> result = new List<List<int>>();
                for (int i = 0; i < CountOfVertex; ++i) {
                    result.Add(new List<int>());
                }
                if (IntData != null) { 
                    for (int i = 0; i < IntData.Count; ++i) {
                        for (int j = 0; j < IntData[i].Count; ++j) {
                            var p = IntData[i][j];
                            result[i].Add(p.First);
                        }
                    }
                } else {
                    for (int i = 0; i < DoubleData.Count; ++i) {
                        for (int j = 0; j < DoubleData[i].Count; ++j) {
                            var p = DoubleData[i][j];
                            result[i].Add(p.First);
                        }
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Обход графа в глубину
        /// </summary>
        /// <param name="v">
        /// Вершина в которую пришли сейчас
        /// </param>
        /// <param name="graph">
        /// Граф, который обходят
        /// </param>
        /// <param name="times">
        /// Массив времени окончаний обхода
        /// </param>
        /// <param name="used">
        /// Массив, характеризующий вершины
        /// true, если уже были в вершине, иначе false
        /// </param>
        /// <param name="counter">
        /// Текущее время обхода
        /// </param>
        void Dfs(int v, Graph graph, int[] times, bool[] used, int counter) {
            used[v] = true;
            ++counter;
            var list = graph.GetData;
            for (int i = 0; i < list[v].Count; ++i) {
                if (!used[list[v][i]]) {
                    Dfs(list[v][i], graph, times, used, counter);
                }
            }
            times[v] = counter;
        }

        /// <summary>
        /// Обход в глубину графа для получения компонент связности
        /// </summary>
        /// <param name="v">
        /// Вершина, в которой находимся сейчас
        /// </param>
        /// <param name="used">
        /// Массив, характеризующий вершины
        /// true, если уже были в вершине, иначе false
        /// </param>
        /// <param name="components">
        /// Текущая компонента связности
        /// </param>
        void Dfs(int v, bool[] used, List<int> components) {
            used[v] = true;
            components.Add(v);
            for (int i = 0; i < Matrix[v].Length; ++i) {
                if (Matrix[v][i] != 0 && !used[i]) {
                    Dfs(i, used, components);
                }
            }
        }

        /// <summary>
        /// Метод, определяющий какой тип имеет вес ребер графа
        /// </summary>
        /// <returns>
        /// true, если целочисленный, иначе false
        /// </returns>
        public bool WholeOrReal() {
            return IntData != null;
        }

        /// <summary>
        /// Свойство для получения списка смежности графа с целочисленными весами ребер
        /// </summary>
        public List<List<Pair<int, int>>> GetWholeData {
            get {
                if (IntData != null) {
                    return IntData;
                }
                throw new ArgumentNullException("Рёбра графа имеют вещественные значения");
            }
        }

        /// <summary>
        /// Свойство для получения списка смежности графа с вещественными весами ребер
        /// </summary>
        public List<List<Pair<int, double>>> GetRealData {
            get {
                if (DoubleData != null) {
                    return DoubleData;
                }
                throw new ArgumentNullException("Рёбра графа имеют целые значения");
            }
        }

        /// <summary>
        /// Свойство для получения количества вершин графа
        /// </summary>
        public int CountOfVertex_ {
            get {
                return CountOfVertex;
            }
        }

        /// <summary>
        /// Свойство для получения количества ребер графа
        /// </summary>
        public int CountOfEdges_ {
            get {
                return CountOfEdges;
            }
        }

        /// <summary>
        /// Свойство для получения количества маркеров, необходимых для выхода из вершины в текущем графе
        /// </summary>
        public int CountOfMarkers_ {
            get {
                return CountOfMarkers;
            }
        }

        /// <summary>
        /// Свойство для получения времени исследования графа
        /// </summary>
        public int ResearchTime_ {
            get {
                return ResearchTime;
            }
        }

        public bool markers_impuls = true;

        public struct Lauching<T> {
            public Lauching(int f_v, int s_v, int c, T inter) {
                first_vertex = f_v;
                second_vertex = s_v;
                count = c;
                interval = inter;
            }

            public readonly int first_vertex;
            public readonly int second_vertex;
            public readonly int count;
            public readonly T interval;
        }
        public Lauching<int> WholeInterval;
        public Lauching<double> RealInterval;

        public bool EdgeExist(int v1, int v2) {
            return Matrix[v1][v2] != 0;
        }

        public int GetWeightWhole(int v1, int v2) {
            foreach (var p in IntData[v1]) {
                if (p.First == v2) {
                    return p.Second;
                }
            }
            return 0;
        }

        public double GetWeightReal(int v1, int v2) {
            foreach (var p in DoubleData[v1]) {
                if (p.First == v2) {
                    return p.Second;
                }
            }
            return 0;
        }
    }
}
