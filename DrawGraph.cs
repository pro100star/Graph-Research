using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WF {
    class DrawGraph {
        Bitmap bitmap;
        Pen blackPen;
        Pen redPen;
        Pen darkGoldPen;
        Graphics gr;
        Font fo;
        Brush br;
        PointF point;
        public int R = 20;

        public DrawGraph(int w, int h) {
            bitmap = new Bitmap(w, h);
            gr = Graphics.FromImage(bitmap);
            clearSheet();
            blackPen = new Pen(Color.Black);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGoldPen = new Pen(Color.DarkGoldenrod);
            darkGoldPen.Width = 4;
            darkGoldPen.EndCap = LineCap.ArrowAnchor;
            fo = new Font("Arial", 15);
            br = Brushes.Black;
        }

        public Bitmap GetBitmap() {
            return bitmap;
        }

        public void clearSheet() {
            gr.Clear(Color.White);
        }

        public void drawVertex(int x, int y, string number) {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 9, y - 9);
            gr.DrawString(number, fo, br, point);
        }

        public void drawSelectedVertex(int x, int y) {
            gr.DrawEllipse(redPen, (x - R), (y - R), 2 * R, 2 * R);
        }

        public void drawEdge<T>(Vertex V1, Vertex V2, Edge<T> E, int numberE) {
            if (E.v1 == E.v2) {
                gr.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                gr.DrawString(E.weight.ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else {
                drawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
                int y1 = V1.y;
                int x1 = V1.x;
                int y2 = V2.y;
                int x2 = V2.x;
                int l1 = x2 - x1;
                int l2 = y2 - y1;
                double k = 1.0 * l2 / l1;
                double b = y1 - k * x1;
        
                if (x1 < x2) {
                    if (y1 < y2) {
                        gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x - 10, V2.y - 15);
                    } else {
                        gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x - 10, V2.y + 15);
                    }
                }
                else {
                    if (y1 < y2) {
                        gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x + 10, V2.y - 15);
                    }
                    else {
                        gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x + 10, V2.y + 15);
                    }   
                }
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                gr.DrawString(E.weight.ToString(), fo, br, point);
            }
        }

        public void drawALLGraph<T>(List<Vertex> V, List<Edge<T>> E) {
            //рисуем ребра
            for (int i = 0; i < E.Count; i++) {
                if (E[i].v1 == E[i].v2) {
                    gr.DrawArc(darkGoldPen, (V[E[i].v1].x - 2 * R), (V[E[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
                    gr.DrawString(E[i].weight.ToString(), fo, br, point);
                }
                else {
                    int y1 = V[E[i].v1].y;
                    int x1 = V[E[i].v1].x;
                    int y2 = V[E[i].v2].y;
                    int x2 = V[E[i].v2].x;
                    int l1 = x2 - x1;
                    int l2 = y2 - y1;
                    int k = l2 / l1;
                    if (x1 < x2) {
                        if (y1 < y2) {
                            gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x - 10, V[E[i].v2].y - 15);
                        }
                        else {
                            gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x - 10, V[E[i].v2].y + 15);
                        }
                    }
                    else {
                        if (y1 < y2) {
                            gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x + 10, V[E[i].v2].y - 15);
                        }
                        else {
                            gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x + 10, V[E[i].v2].y + 15);
                        }
                    }
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                    //gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    gr.DrawString(E[i].weight.ToString(), fo, br, point);
                }
            }
            //рисуем вершины
            for (int i = 0; i < V.Count; i++) {
                drawVertex(V[i].x, V[i].y, (i + 1).ToString());
            }
        }

        public List<List<Pair<int, T>>> GetAdjacencyList<T>(int CountOfVertex, List<Edge<T>> edges) {
            List<List<Pair<int, T>>> result = new List<List<Pair<int, T>>>(CountOfVertex);
            for (int i = 0; i < CountOfVertex; ++i) {
                result.Add(new List<Pair<int, T>>());
            }
            foreach (var edge in edges) {
                result[edge.v1].Add(new Pair<int, T>(edge.v2, edge.weight));
            }
            return result;
        }
    }
}
