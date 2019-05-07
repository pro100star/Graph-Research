using System;
using System.Collections.Generic;

namespace WF {
    delegate bool Predicate(int k);

    public class Graph {
        /// <summary>
        /// Adjacency list of graph with whole weights
        /// </summary>
        List<List<Pair<int, int>>> IntData = null;

        /// <summary>
        /// Adjacency list of graph with real weights
        /// </summary>
        List<List<Pair<int, double>>> DoubleData = null;

        /// <summary>
        /// Adjacency matrix
        /// </summary>
        public readonly int[][] Matrix;

        int CountOfVertex = 0, CountOfEdges = 0;
        Predicate Condition = (k) => k >= 2;

        List<List<int>> markers = new List<List<int>>(); // список маркеров, идущих в вершину, хранится значение, оставшееся до вершины
        List<List<double>> double_markers;
        List<int> counter = new List<int>();

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
        /// Список смежности
        /// </param>
        /// <param name="n">
        /// Количество вершин
        /// </param>
        /// <param name="m">
        /// Количество ребер
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
        }

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
        }

        bool StrongConnectivityCheck() {
            return true;
        }
        
        public int[] GetCountOfMarkers(int Time, int CountOfMarkersToGo) {
            if (DoubleData == null) {
                return GetCountOfMarkersWhole(Time, CountOfMarkersToGo);
            }
            return GetCountOfMarkersReal(Time, CountOfMarkersToGo);
        }

        int[] GetCountOfMarkersWhole(int Time, int CountOfMarkersToGo) {
            if (Time <= 0) {
                throw new ArgumentOutOfRangeException(@"Invalid value of the time, it can't be \leq 0");
            }
            if (CountOfMarkersToGo < 0) {
                throw new ArgumentOutOfRangeException(@"Invalid value of count of markers to go, it can't be less than 0");
            }
            Condition = (k) => k > CountOfMarkersToGo;
            int[] Result = new int[Time];
            int[] Counter = new int[CountOfVertex];
            List<List<int>> Markers = new List<List<int>>(CountOfVertex);
            for (int i = 0; i < CountOfVertex; ++i) {
                Markers.Add(new List<int>());
            }
            for (int i = 0; i < CountOfVertex; ++i) {
                for (int j = 0; j < IntData[i].Count; ++j) {
                    Markers[IntData[i][j].First].Add(IntData[i][j].Second);
                }
                Counter[i] = 0;
            }
            int NowCount = CountOfEdges;

            for (int i = 1; i < Time; ++i) {
                Result[i] = NowCount;
                for (int j = 0; j < CountOfVertex; ++j) {
                    for (int z = 0; z < Markers[j].Count; ++z) {
                        --Markers[j][z];
                    }
                    int count = Markers[j].RemoveAll((k) => k == 0);
                    Counter[j] += count;
                    Result[i] -= count;
                    count = Update(j, Condition(Counter[j]), Markers);
                    Result[i] += count;
                }
                NowCount = Result[i];
            }
            return Result;
        }

        int[] GetCountOfMarkersReal(int Time, int CountOfMarkersToGo) {
            if (Time <= 0) {
                throw new ArgumentOutOfRangeException(@"Invalid value of the time, it can't be \leq 0");
            }
            if (CountOfMarkersToGo < 0) {
                throw new ArgumentOutOfRangeException(@"Invalid value of count of markers to go, it can't be less than 0");
            }
            Condition = (count) => count > CountOfMarkersToGo;
            int[] Result = new int[Time * 100];
            int[] Counter = new int[CountOfVertex];
            List<List<double>> Markers = new List<List<double>>(CountOfVertex);
            for (int i = 0; i < CountOfVertex; ++i) {
                Markers.Add(new List<double>());
            }
            for (int i = 0; i < CountOfVertex; ++i) {
                for (int j = 0; j < DoubleData[i].Count; ++j) {
                    Markers[DoubleData[i][j].First].Add(DoubleData[i][j].Second);
                }
                Counter[i] = 0;
            }
            int NowCount = CountOfEdges;

            for (int i = 1; i < Result.Length; ++i) {
                Result[i] = NowCount;
                for (int j = 0; j < CountOfVertex; ++j) {
                    for (int z = 0; z < Markers[j].Count; ++z) {
                        Markers[j][z] -= 0.01;
                    }
                    int count = Markers[j].RemoveAll((k) => k < 0.00000000001);
                    Counter[j] += count;
                    Result[i] -= count;
                    count = Update(j, Condition(Counter[j]), Markers);
                    Result[i] += count;
                }
                NowCount = Result[i];
            }
            return Result;
        }

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
    }
}
