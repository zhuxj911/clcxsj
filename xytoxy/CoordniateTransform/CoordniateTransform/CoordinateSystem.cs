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
        ///X(N)方向平移量dx
        /// </summary>
        private double _a;

        /// <summary>
        /// X(N)方向平移量dx
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
        /// Y(E)方向平移量dy
        /// </summary>
        private double _b;

        /// <summary>
        /// Y(E)方向平移量dy
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
        /// 线性方程计算系数c = k*cos(α)
        /// </summary>
        public double _c;

        /// <summary>
        /// 线性方程计算系数c = k*cos(α)
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
        /// 线性方程计算系数d = k*sin(α)
        /// </summary>
        public double _d;

        /// <summary>
        /// 线性方程计算系数d = k*cos(α)
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
        /// 旋转角度α=atan(d/c)
        /// </summary>
        public double _alpha;

        /// <summary>
        ///  旋转角度α=atan(d/c)
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
        /// 尺度比因子k=sqrt(c^2 + d^2)
        /// </summary>
        public double _k;

        /// <summary>
        ///  尺度比因子k=sqrt(c^2 + d^2)
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


        //计算过程数据
        private double[,] B;     //系数阵
        private double[] l;      //常数项  
        private double[,] N;     //法方程  NX=U
        private double[] U;
        private double[] X;      //未知数  


        public CoordinateSystem(){
            B = null;
            l = null;
            N = null;
            U = null;
        }

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
                        pnt.oX = double.Parse(its[1]);
                        pnt.oY = double.Parse(its[2]);
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
                        if (pnt == null) continue;
                        pnt.X = double.Parse(its[1]);
                        pnt.Y = double.Parse(its[2]);
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
                        pnt.oX = double.Parse(its[1]);
                        pnt.oY = double.Parse(its[2]);

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
                sr.WriteLine("#公共点在源坐标系中的坐标: 点名, X(N), Y(E)");
                foreach (var pnt in this.knwPointList)
                {
                    sr.WriteLine("{0}, {1}, {2}", pnt.Name, pnt.oX, pnt.oY);
                }

                sr.WriteLine();
                sr.WriteLine("#公共点在目标坐标系中的坐标: 点名, X(N), Y(E)");
                foreach (var pnt in this.knwPointList)
                {
                    sr.WriteLine("{0}, {1}, {2}", pnt.Name, pnt.X, pnt.Y);
                }

                sr.WriteLine();
                sr.WriteLine("#待转换点在源坐标系中的坐标: 点名, X(N), Y(E)");
                foreach (var pnt in this.unKnwPointList)
                {
                    sr.WriteLine("{0}, {1}, {2}", pnt.Name, pnt.oX, pnt.oY);
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
                sr.WriteLine("# 点名, 源X(N), 源Y(E), X(N), Y(E)");
                foreach (var pnt in this.knwPointList)
                {
                    sr.WriteLine(pnt);
                }

                sr.WriteLine();
                sr.WriteLine("# 转换参数");
                sr.WriteLine("北向(N)平移量a={0}，东向(E)平移量b={1}, c={2}, d={3}", this.a, this.b, this.c, this.d);
                sr.WriteLine("北向(N)平移量dx={0}，东向(E)平移量dy={1}, 旋转角α={2}°，尺度因子k={3}", this.a, this.b, this.alpha, this.k);

                sr.WriteLine();
                sr.WriteLine("# 待计算点的坐标");
                sr.WriteLine("# 点名, 源X(N), 源Y(E), X(N), Y(E)");
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
            double ap = this.alpha / 180.0 * Math.PI;
            this.c = this.k * Math.Cos( ap );
            this.d = this.k * Math.Sin( ap );
        }

        /// <summary>
        /// 赫尔默特法计算转换系数
        /// </summary>
        public void CalCoefficient()
        {
            int n0 = this.knwPointList.Count;
            if (n0 < 2) return; //少于两个公共点，无法计算

            B = new double[2 * n0, 4];
            l = new double[2 * n0];
            N = new double[4, 4];
            U = new double[4];
            X = new double[4];

            //组成系数阵B与l，此处应注意读入的坐标是测量坐标，
            //应将测量坐标转换为数学坐标
            double ox, oy, x, y;
            for (int i = 0; i < n0; i++)
            {
                ox = knwPointList[i].oX;//测量上的源x
                oy = knwPointList[i].oY;//测量上的源y
                x = knwPointList[i].X;
                y = knwPointList[i].Y; 

                B[(2 * i), 0] = 1.0;
                B[(2 * i), 1] = 0.0;
                B[(2 * i), 2] = ox;
                B[(2 * i), 3] = -oy;
                l[2 * i] = x;

                B[(2 * i + 1), 0] = 0.0;
                B[(2 * i + 1), 1] = 1.0;
                B[(2 * i + 1), 2] = oy; 
                B[(2 * i + 1), 3] = ox; 
                l[2 * i + 1] = y;
            }

			//N = B' * B
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

               X[k] = U[k];
            }

            NegativeMatrix(N, X, 4);

            this.a = X[0];
            this.b = X[1];
            this.c = X[2];
            this.d = X[3];
            this.alpha = Math.Atan2(d, c) * 180.0/Math.PI;
            this.k = Math.Sqrt(d * d + c * c);
        }

        /// <summary>
        /// 计算点在新坐标系中的坐标
        /// </summary>
        public void CalUnKnwXY()
        {
            //测量坐标
            double ox, oy, x, y;
            foreach (var it in this.unKnwPointList)
            {
                ox = it.oX; oy = it.oY;

                x = a + c * ox - d * oy;
                y = b + c * oy + d * ox;

                it.Y = y; it.X = x;
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

        
        /// <summary>
        /// 输出计算过程数据文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void OutDetailTextFile(string fileName)
        {
            if(B is null || l is null || N is null || U is null || X is null) return;

                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(fileName))
            {
                sr.WriteLine("赫尔默特四参数转换法计算过程数据：");
                sr.WriteLine("公共点坐标");
                sr.WriteLine("点名, 源X(N), 源Y(E), X(N), Y(E)");
                sr.WriteLine("---------------------------------");
                foreach (var pnt in this.knwPointList)
                {
                    sr.WriteLine(pnt);
                }

                sr.WriteLine();

                int n0 = this.knwPointList.Count;
                sr.WriteLine("误差方程式V=BX - l");
                sr.WriteLine("误差方程式: B[i,0],B[i,1],B[i,2],B[i,3],l[i]");
                sr.WriteLine("--------------------------------------------");
                for (int i = 0; i < 2 * n0; i++)
                {
                    sr.WriteLine($"{B[i, 0]}, {B[i, 1]}, {B[i, 2]}, {B[i, 3]}, {l[i]}");
                }

                sr.WriteLine();

                sr.WriteLine("法方程式NX - U = 0");
                sr.WriteLine("误差方程式: N[i,0],N[i,1],N[i,2],N[i,3],U[i]");
                sr.WriteLine("--------------------------------------------");
                for (int i = 0; i < 4; i++)
                {
                    sr.WriteLine($"{N[i, 0]}, {N[i, 1]}, {N[i, 2]}, {N[i, 3]}, {U[i]}");
                }

                sr.WriteLine();

                sr.WriteLine("系数: X[0], X[1], X[2], X[3]");
                sr.WriteLine("---------------------------------");
                sr.WriteLine($"{X[0]}, {X[1]}, {X[2]}, {X[3]}");

                sr.WriteLine();

                sr.WriteLine("转换参数");
                sr.WriteLine("---------------------------------------------------------------");
                sr.WriteLine("北向(N)平移量a={0}，东向(E)平移量b={1}, c={2}, d={3}", this.a, this.b, this.c, this.d);
                sr.WriteLine("北向(N)平移量dx={0}，东向(E)平移量dy={1}, 旋转角α={2}°，尺度因子k={3}", this.a, this.b, this.alpha, this.k);

                sr.WriteLine();

                sr.WriteLine("待计算点的坐标:");
                sr.WriteLine("待计算点坐标计算公式:");
                sr.WriteLine($"x = {a} + x'*{c} - y' * {d}");
                sr.WriteLine($"y = {b} + y'*{c} + x' * {d}");
                sr.WriteLine("-------------------------------------------------------------");
                sr.WriteLine("点名, 源X(N), 源Y(E), X(N), Y(E)");
                foreach (var pnt in this.unKnwPointList)
                {
                    sr.WriteLine(pnt);
                }
            }
        }
    }
}