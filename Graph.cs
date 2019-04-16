using System;
using System.Collections.Generic;

namespace WF {
    delegate bool Condition(int k);

    public class Graph {
        List<List<Pair<int, int>>> data = null; // список смежности, если рёбра имеют целый вес
        Pair<int, double>[][] double_data = null; // список смежности, если рёбра вещественные
        int n, m; // количество вершин, ребер
        Condition condition = (k) => k >= 2; // условие выхода из вершины
        List<List<int>> markers = new List<List<int>>(); // список маркеров, идущих в вершину, хранится значение, оставшееся до вершины
        List<List<double>> double_markers;
        List<int> counter = new List<int>();
        int[][] matrix;
        //bool[] used;

        /// <summary>
        /// Конструктор без параметров, создающий пустой граф
        /// </summary>
        public Graph() {
            n = m = 0;
            matrix = new int[0][];
        }

        /*void dfs(int vertex) {
            used[vertex] = true;
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < n; ++j) {
                    if (matrix[i][j] == 1) {
                        dfs(j);
                    }
                }
            }
        }*/

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
        public Graph(List<List<Pair<int, int>>> data, int n, int m) {
            this.data = new List<List<Pair<int, int>>>(n);
            counter = new List<int>(n);
            markers = new List<List<int>>(n);
            matrix = new int[n][];
            for (int i = 0; i < n; ++i) {
                counter.Add(0);
                this.data.Add(new List<Pair<int, int>>());
                markers.Add(new List<int>());
            }
            for (int i = 0; i < data.Count; ++i) {
                this.data[i].AddRange(data[i]);
                matrix[i] = new int[n];
                for (int j = 0; j < data[i].Count; ++j) {
                    matrix[i][data[i][j].first] = 1;
                }
            }
            this.n = n;
            this.m = m;
        }

        public Graph(List<List<Pair<int, double>>> data, int n, int m) {
            double_data = new Pair<int, double>[n][];
            counter = new List<int>(n);
            double_markers = new List<List<double>>(n);
            matrix = new int[n][];
            for (int i = 0; i < n; ++i) {
                counter.Add(0);
                double_markers.Add(new List<double>());
            }
            for (int i = 0; i < data.Count; ++i) {
                double_data[i] = new Pair<int, double>[data[i].Count];
                matrix[i] = new int[n];
                for (int j = 0; j < data[i].Count; ++j) {
                    double_data[i][j] = data[i][j];
                    matrix[i][double_data[i][j].first] = 1;
                }
            }
            this.n = n;
            this.m = m;
        }

        /// <summary>
        /// Метод класса для анализа количества маркеров
        /// </summary>
        /// <param name="t">
        /// Время, до которого мы считаем
        /// </param>
        /// <returns>
        /// Возвращает одномерный массив, i-й элемент отвечает количеству маркеров в данную секунду
        /// </returns>
        public int[] Research(int t, int m) {
            if (double_data == null) {
                return intResearch(t, m);
            }
            return doubleResearch(t, m);
        }

        /// <summary>
        /// Запускает новые маркеры, если уже пришло достаточное количество
        /// </summary>
        /// <param name="vertex">
        /// Вершина, которая проверяется
        /// </param>
        /// <param name="f">
        /// Условие выполнения
        /// </param>
        /// <returns>
        /// Количество запущенных маркеров
        /// </returns>
        int Update(int vertex, bool f) {
            if (double_data == null) {
                return intUpdate(vertex, f);
            }
            return doubleUpdate(vertex, f);
        }

        int[] intResearch(int t, int m) {
            if (t <= 0) {
                throw new ArgumentOutOfRangeException("Время не может быть отрицательным");
            }
            if (m <= 0) {
                throw new ArgumentOutOfRangeException("Количество маркеров не может быть отрицательным");
            }
            condition = (m_counter) => m_counter > m;
            int[] result = new int[t];
            int now_count = 0;
            for (int i = 0; i < data.Count; ++i) {
                int k = Update(i, true);
                now_count += k;
            }

            for (int i = 0; i < t; ++i) {
                result[i] = now_count;
                for (int j = 0; j < n; ++j) {
                    for (int z = 0; z < markers[j].Count; ++z) {
                        --markers[j][z];
                    }
                    int c = markers[j].RemoveAll((k) => k == 0);
                    counter[j] += c;
                    result[i] -= c;
                    int count = Update(j, condition(counter[j]));
                    result[i] += count;
                }
                now_count = result[i];
            }
            return result;
        }

        int[] doubleResearch(int t, int m) {
            if (t <= 0) {
                throw new ArgumentOutOfRangeException("Время не может быть отрицательным");
            }
            if (m <= 0) {
                throw new ArgumentOutOfRangeException("Количество маркеров не может быть отрицательным");
            }
            condition = (m_counter) => m_counter > m;
            int[] result = new int[t*100];
            int now_count = 0;
            for (int i = 0; i < double_data.Length; ++i) {
                int k = doubleUpdate(i, true);
                now_count += k;
            }

            for (int i = 0; i < t * 100; ++i) {
                result[i] = now_count;
                for (int j = 0; j < n; ++j) {
                    for (int z = 0; z < double_markers[j].Count; ++z) {
                        double_markers[j][z] -= 0.01;
                    }
                    int c = double_markers[j].RemoveAll((k) => k < 0.001);
                    counter[j] += c;
                    result[i] -= c;
                    int count = Update(j, condition(counter[j]));
                    result[i] += count;
                }
                now_count = result[i];
            }
            return result;
        }

        int intUpdate(int vertex, bool f) {
            if (f) {
                for (int j = 0; j < data[vertex].Count; ++j) {
                    int v = data[vertex][j].first;
                    int w = data[vertex][j].second;
                    markers[v].Add(w);
                }
                return data[vertex].Count;
            }
            return 0;
        }

        int doubleUpdate(int vertex, bool f) {
            if (f) {
                for (int j = 0; j < double_data[vertex].Length; ++j) {
                    int v = double_data[vertex][j].first;
                    double w = double_data[vertex][j].second;
                    double_markers[v].Add(w);
                }
                return double_data[vertex].Length;
            }
            return 0;
        }
    }
}
