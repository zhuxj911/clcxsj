using System;
using System.Collections.ObjectModel;

namespace CoordniateTransform
{
    /// <summary>
    /// 坐标系
    /// </summary>
    public class CoordinateSystem : NotificationObject
    {
        /// <summary>
        /// 公共点集
        /// </summary>
        private ObservableCollection<GeoPoint> knwPointList = 
                new ObservableCollection<GeoPoint>();

        /// <summary>
        /// 公共点集
        /// </summary>
        public ObservableCollection<GeoPoint> KnwPointList
        {
            get { return knwPointList; }
        }
        
        /// <summary>
        /// 待计算点集
        /// </summary>
        private ObservableCollection<GeoPoint> unKnwPointList = 
                new ObservableCollection<GeoPoint>();

        /// <summary>
        ///待计算点集
        /// </summary>
        public ObservableCollection<GeoPoint> UnKnwPointList
        {
            get { return unKnwPointList; }
        }

        /// <summary>
        ///X方向平移量
        /// </summary>
        private double _a;

        /// <summary>
        /// X方向平移量
        /// </summary>
        public double a
        {
            get { return _a; }
            set
            {
                _a = value;
                RaisePropertyChange("a");
            }
        }

        /// <summary>
        /// Y方向平移量
        /// </summary>
        private double _b;

        /// <summary>
        /// Y方向平移量
        /// </summary>
        public double b
        {
            get { return _b; }
            set
            {
                _b = value;
                RaisePropertyChange("b");
            }
        }

        /// <summary>
        /// 线性方程计算系数c
        /// </summary>
        public double _c;

        /// <summary>
        /// 线性方程计算系数c
        /// </summary>
        public double c
        {
            get { return _c; }
            set
            {
                _c = value;
                RaisePropertyChange("c");
            }
        }

        /// <summary>
        /// 线性方程计算系数d
        /// </summary>
        public double _d;

        /// <summary>
        /// 线性方程计算系数d
        /// </summary>
        public double d
        {
            get { return _d; }
            set
            {
                _d = value;
                RaisePropertyChange("d");
            }
        }

        /// <summary>
        /// 旋转角度α
        /// </summary>
        public double _alpha;

        /// <summary>
        ///  旋转角度α
        /// </summary>
        public double alpha
        {
            get { return _alpha; }
            set
            {
                _alpha = value;
                RaisePropertyChange("alpha");
            }
        }

        /// <summary>
        /// 尺度比因子k
        /// </summary>
        public double _k;

        /// <summary>
        ///  尺度比因子k
        /// </summary>
        public double k
        {
            get { return _k; }
            set
            {
                _k = value;
                RaisePropertyChange("k");
            }
        }

        public CoordinateSystem(){ }

        /// <summary>
        /// 读入坐标转换数据文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void ReadTextFileData(string fileName)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fileName))
            {
                string buffer;

                //读入点的坐标数据
                this.KnwPointList.Clear();
                while (true)//读入公共点源坐标系坐标数据,至空行退出
                {
                    buffer = sr.ReadLine();
                    if (string.IsNullOrEmpty(buffer)) break; //文件末尾或空行退出

                    if (buffer[0] == '#') continue;

                    string[] its = buffer.Split(new char[1] { ',' });
                    if (its.Length == 3)
                    {
                        GeoPoint pnt = new GeoPoint();
                        pnt.Name = its[0].Trim();
                        pnt.OX = double.Parse(its[1]);
                        pnt.OY = double.Parse(its[2]);
                        this.KnwPointList.Add(pnt);
                    }
                }

                while (true)//读入公共点新坐标系坐标数据,至空行退出
                {
                    buffer = sr.ReadLine();
                    if (string.IsNullOrEmpty(buffer)) break; //文件末尾或空行退出

                    if (buffer[0] == '#') continue;

                    string[] its = buffer.Split(new char[1] { ',' });
                    if (its.Length == 3)
                    {
                        string name = its[0].Trim();
                        GeoPoint pnt = GetGeoPoint(name);
                        pnt.NX = double.Parse(its[1]);
                        pnt.NY = double.Parse(its[2]);
                    }
                }

                this.UnKnwPointList.Clear();
                while (true)//读入待计算点源坐标系坐标数据,至空行退出
                {
                    buffer = sr.ReadLine();
                    if (string.IsNullOrEmpty(buffer)) break; //文件末尾或空行退出

                    if (buffer[0] == '#') continue;

                    string[] its = buffer.Split(new char[1] { ',' });
                    if (its.Length == 3)
                    {
                        GeoPoint pnt = new GeoPoint();
                        pnt.Name = its[0].Trim();
                        pnt.OX = double.Parse(its[1]);
                        pnt.OY = double.Parse(its[2]);

                        this.UnKnwPointList.Add(pnt);
                    }
                }
            }
        }
     
        /// <summary>
        /// 根据点名获取点对象
        /// </summary>
        /// <param name="name">点名</param>
        /// <returns>点对象</returns>
        private GeoPoint GetGeoPoint(string name)
        {
            foreach (var it in this.KnwPointList)
            {
                if (it.Name == name)
                    return it;
            }

            return null;
        }

        /// <summary>
        /// 写坐标转换数据文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void WriteTextFileData(string fileName)
        {
            using (System.IO.StreamWriter sr = new System.IO.StreamWriter(fileName))
            {
                sr.WriteLine("#赫尔默特四参数转换法数据文件");
                sr.WriteLine("#每行以“#”开头的行均被认为是注释行");
                sr.WriteLine("#公共点在源坐标系中的坐标: 点名, x, y");
                foreach (var pnt in this.knwPointList)
                {
                    sr.WriteLine("{0}, {1}, {2}", pnt.Name, pnt.OX, pnt.OY);
                }

                sr.WriteLine();
                sr.WriteLine("#公共点在新坐标系中的坐标: 点名, x, y");
                foreach (var pnt in this.knwPointList)
                {
                    sr.WriteLine("{0}, {1}, {2}", pnt.Name, pnt.NX, pnt.NY);
                }

                sr.WriteLine();
                sr.WriteLine("#待转换点在源坐标系中的坐标: 点名, x, y");
                foreach (var pnt in this.unKnwPointList)
                {
                    sr.WriteLine("{0}, {1}, {2}", pnt.Name, pnt.OX, pnt.OY);
                }
            }
        }
        /// <summary>
        /// 写计算成果数据文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void WriteResultTextFileData(string fileName)
        {
            using (System.IO.StreamWriter sr = new System.IO.StreamWriter(fileName))
            {
                sr.WriteLine("#赫尔默特四参数转换法计算成果数据文件");
                sr.WriteLine("# 公共点坐标");
                sr.WriteLine("# 点名, 源X, 源Y, 新X, 新Y");
                foreach (var pnt in this.knwPointList)
                {
                    sr.WriteLine(pnt);
                }

                sr.WriteLine();
                sr.WriteLine("# 转换参数");
                sr.WriteLine("a={0}，b={1}, c={2}, d={3}\n α={4}， k={5}", 
                    this.a, this.b, this.c, this.d, this.alpha, this.k);

                sr.WriteLine();
                sr.WriteLine("# 待计算点的坐标");
                sr.WriteLine("# 点名, 源X, 源Y, 新X, 新Y");
                foreach (var pnt in this.unKnwPointList)
                {
                    sr.WriteLine(pnt);
                }
            }
        }

        /// <summary>
        /// 根据尺度比因子d与旋转角度α计算线性计算量c和d
        /// </summary>
        public void CalCd()
        {
            this.c = this.k * Math.Cos(ZXY.SMath.DMS2RAD(this.alpha));
            this.d = this.k * Math.Sin(ZXY.SMath.DMS2RAD(this.alpha));
        }

        /// <summary>
        /// 赫尔默特法计算转换系数
        /// </summary>
        public void CalCoefficient()
        {
            int n0 = this.knwPointList.Count;
            if (n0 < 2) return; //少于两个公共点，无法计算

            double[,] B = new double[2 * n0, 4];
            double[] l = new double[2 * n0];
            double[,] N = new double[4, 4];
            double[] U = new double[4];

            for (int i = 0; i < n0; i++)
            {
                B[(2 * i), 0] = 1.0;
                B[(2 * i), 1] = 0.0;
                B[(2 * i), 2] = knwPointList[i].OX;
                B[(2 * i), 3] = knwPointList[i].OY;
                l[2 * i] = knwPointList[i].NX;

                B[(2 * i + 1), 0] = 0.0;
                B[(2 * i + 1), 1] = 1.0;
                B[(2 * i + 1), 2] = knwPointList[i].OY;
                B[(2 * i + 1), 3] = -knwPointList[i].OX;
                l[2 * i + 1] = knwPointList[i].NY;
            }

            for (int k = 0; k < 4; k++)
            {
                for (int j = 0; j < 4; j++)
                {
                    N[k, j] = 0.0;
                    for (int i = 0; i < 2 * n0; i++)
                    {
                        N[k, j] += B[i, k] * B[i, j];
                    }
                }

                U[k] = 0.0;
                for (int i = 0; i < 2 * n0; i++)
                    U[k] += B[i, k] * l[i];
            }

            NegativeMatrix(N, U, 4);

            this.a = U[0];
            this.b = U[1];
            this.c = U[2];
            this.d = U[3];
            this.alpha = ZXY.SMath.RAD2DMS(Math.Atan2(d, c));
            this.k = Math.Sqrt(d * d + c * c);
        }

        /// <summary>
        /// 计算点在新坐标系中的坐标
        /// </summary>
        public void CalUnKnwXY()
        {
            foreach (var it in this.unKnwPointList)
            {
                it.NX = this.a + this.c * it.OX + this.d * it.OY;
                it.NY = this.b - this.d * it.OX + this.c * it.OY;
            }
        }

        /// <summary>
        ///  高斯约化法解方程 AX = B中的X值, 结果存B中
        /// </summary>
        /// <param name="A">A: nxn</param>
        /// <param name="B">B: nx1</param>
        /// <param name="n">n</param>
        private void NegativeMatrix(double[,] A, double[] B, int n)
        {
            for (int k = 0; k < n - 1; k++)
            {
                for (int i = k + 1; i < n; i++)
                {
                    A[i, k] /= A[k, k];
                    for (int j = k + 1; j < n; j++)
                    {
                        A[i, j] -= A[i, k] * A[k, j];
                    }
                    B[i] -= A[i, k] * B[k];
                }
            }
            B[n - 1] /= A[(n - 1), (n - 1)];
            for (int i = n - 2; i >= 0; i--)
            {
                double s = 0.0;
                for (int j = i + 1; j < n; j++)
                {
                    s += A[i, j] * B[j];
                }
                B[i] = (B[i] - s) / A[i, i];
            }
        }
    }
}