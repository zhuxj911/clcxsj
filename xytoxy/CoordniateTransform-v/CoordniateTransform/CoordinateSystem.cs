using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CoordniateTransform
{
    /// <summary>
    /// 坐标系
    /// 平面坐标四参数转换的标准数学模型为：
    /// x = dx + k(x'*cosα - y'*sinα) 
    /// y = dy + k(y'*cosα + x'*sinα)
    /// 由于一般情况下测量坐标数值均较大，在解算转换参数时，容易形成病态矩阵
    /// 而且 也不应该由用户故意去将数值变小，来规避病态矩阵
    /// 因此，将标准四参数转换 改变为：
    /// (x-xk)/kk = dx + k[(x'-x'k)/kk*cosα - (y'-y'k)/kk*sinα] 
    /// (y-yk)/kk = dy + k[(y'-y'k)/kk*cosα + (x'-x'k)/kk*sinα]
    /// kk 一般取值为1000，即以公里做单位，若为1，则以m做单位
    /// 上边公式变换为
    /// (x-xk)/kk = dx + (x'-x'k)/kk*k*cosα - (y'-y'k)/kk*k*sinα
    /// (y-yk)/kk = dy + (y'-y'k)/kk*k*cosα + (x'-x'k)/kk*k*sinα
    /// 令a=dx, b=dy, c= k*cosα, d=k*sinα,则上式变换为
    /// (x-xk)/kk = a + (x'-x'k)/kk*c - (y'-y'k)/kk*d
    /// (y-yk)/kk = b + (y'-y'k)/kk*c + (x'-x'k)/kk*d
    /// 这样就可以将系数阵变小。
    /// 数据回代时，计算公式为：
    /// x = kk*[a + (x'-x'k)/kk*c - (y'-y'k)/kk*d] + xk
    /// y = kk*[b + (y'-y'k)/kk*c + (x'-x'k)/kk*d] + yk
    /// 将kk继续带入，计算公式为：
    /// x = (kk*a + xk) + (x'-x'k)*c - (y'-y'k)*d
    /// y = (kk*b + yk) + (y'-y'k)*c + (x'-x'k)*d
    /// 如此，研究分析
    /// </summary>
    public class CoordinateSystem : NotificationObject
    {
        /// <summary>
        /// 公共点集
        /// </summary>
        private ObservableCollection<GPoint> kPointList = 
                new ObservableCollection<GPoint>();

        /// <summary>
        /// 公共点集
        /// </summary>
        public ObservableCollection<GPoint> KPointList
        {
            get { return kPointList; }
        }
        
        /// <summary>
        /// 待计算点集
        /// </summary>
        private ObservableCollection<GPoint> uPointList = 
                new ObservableCollection<GPoint>();

        /// <summary>
        ///待计算点集
        /// </summary>
        public ObservableCollection<GPoint> UPointList
        {
            get { return uPointList; }
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

        /// <summary>
        /// 源坐标X的平均值，取已知点的 
        /// </summary>
        private double xTk= 0;

        /// <summary>
        /// 源坐标Y的平均值，取已知点的 
        /// </summary>
        private double yTk = 0;

        /// <summary>
        /// 新坐标X的平均值，取已知点的 
        /// </summary>
        private double xk = 0;

        /// <summary>
        /// 新坐标Y的平均值，取已知点的 
        /// </summary>
        private double yk = 0;

        /// <summary>
        /// 新旧坐标同时缩放的比例系数，为1000时以km为单位，为1时以m为单位
        /// </summary>
        private double kk = 1000;

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
                this.KPointList.Clear();
                while (true)//读入公共点源坐标系坐标数据,至空行退出
                {
                    buffer = sr.ReadLine();
                    if (string.IsNullOrEmpty(buffer)) break; //文件末尾或空行退出

                    if (buffer[0] == '#') continue;

                    string[] its = buffer.Split(new char[1] { ',' });
                    if (its.Length == 3)
                    {
                        GPoint pnt = new GPoint();
                        pnt.Name = its[0].Trim();
                        pnt.xT = double.Parse(its[1]);
                        pnt.yT = double.Parse(its[2]);
                        this.KPointList.Add(pnt);
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
                        GPoint pnt = GetGeoPoint(name);
                        if (pnt == null) continue;
                        pnt.x = double.Parse(its[1]);
                        pnt.y = double.Parse(its[2]);
                    }
                }

                this.UPointList.Clear();
                while (true)//读入待计算点源坐标系坐标数据,至空行退出
                {
                    buffer = sr.ReadLine();
                    if (string.IsNullOrEmpty(buffer)) break; //文件末尾或空行退出

                    if (buffer[0] == '#') continue;

                    string[] its = buffer.Split(new char[1] { ',' });
                    if (its.Length == 3)
                    {
                        GPoint pnt = new GPoint();
                        pnt.Name = its[0].Trim();
                        pnt.xT = double.Parse(its[1]);
                        pnt.yT = double.Parse(its[2]);

                        this.UPointList.Add(pnt);
                    }
                }
            }
        }
     
        /// <summary>
        /// 根据点名获取点对象
        /// </summary>
        /// <param name="name">点名</param>
        /// <returns>点对象</returns>
        private GPoint GetGeoPoint(string name)
        {
            foreach (var it in this.KPointList)
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
                foreach (var pnt in this.kPointList)
                {
                    sr.WriteLine("{0}, {1}, {2}", pnt.Name, pnt.xT, pnt.yT);
                }

                sr.WriteLine();
                sr.WriteLine("#公共点在目标坐标系中的坐标: 点名, X(N), Y(E)");
                foreach (var pnt in this.kPointList)
                {
                    sr.WriteLine("{0}, {1}, {2}", pnt.Name, pnt.x, pnt.y);
                }

                sr.WriteLine();
                sr.WriteLine("#待转换点在源坐标系中的坐标: 点名, X(N), Y(E)");
                foreach (var pnt in this.uPointList)
                {
                    sr.WriteLine("{0}, {1}, {2}", pnt.Name, pnt.xT, pnt.yT);
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
                foreach (var pnt in this.kPointList)
                {
                    sr.WriteLine(pnt);
                }
           
                sr.WriteLine("---------------------------------------------------------------");

                sr.WriteLine("转换公式：");
                sr.WriteLine("x = (a + xk) + (x'-x'k)*c - (y'-y'k)*d");
                sr.WriteLine("y = (b + yk) + (y'-y'k)*c + (x'-x'k)*d");
                sr.WriteLine("转换参数:");
                sr.WriteLine($"源坐标中心平均值：xk'={xTk}, y'k={yTk}");
                sr.WriteLine($"目标坐标中心平均值：xk={xk}, ykY={yk}");
                sr.WriteLine("---------------------------------------------------------------");
                sr.WriteLine("线性计算量：");
                sr.WriteLine($"北向(N) a={a}, 东向(E) b={b}, c=k*cos(α)={c}, d=k*sin(α)={d}");
                sr.WriteLine($"北向(N)dx={a}, 东向(E)dy={b}, 旋转角α={alpha}°, 尺度因子k={k}");

                sr.WriteLine();

                sr.WriteLine("待计算点的坐标:");
                sr.WriteLine("---------------------------------------------------------------");
                sr.WriteLine("待计算点坐标计算公式:");
                sr.WriteLine($"x = {a + xk} + (x'-{xTk}) * {c} - (y'-{yTk}) * {d}");
                sr.WriteLine($"y = {b + yk} + (y'-{yTk}) * {c} + (x'-{xTk}) * {d}");
                sr.WriteLine("-------------------------------------------------------------");
                sr.WriteLine("点名, 源X(N), 源Y(E), X(N), Y(E)");
                foreach (var pnt in this.uPointList)
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
            int n0 = this.kPointList.Count;
            if (n0 < 2) return; //少于两个公共点，无法计算

            B = new double[2 * n0, 4];
            l = new double[2 * n0];
            N = new double[4, 4];
            U = new double[4];
            X = new double[4];

            //采用Linq表达式进行平均值计算
            xTk = kPointList.Select(c => c.xT).Average();
            yTk = kPointList.Select(c => c.yT).Average();
            xk  = kPointList.Select(c => c.x).Average();
            yk  = kPointList.Select(c => c.y).Average();
            
            //组成系数阵B与l，此处应注意读入的坐标是测量坐标，
            //应将测量坐标转换为数学坐标
            double ox, oy, x, y;
            for (int i = 0; i < n0; i++)
            {
                ox = (kPointList[i].xT - xTk)/kk;//测量上的源x
                oy = (kPointList[i].yT - yTk)/kk;//测量上的源y
                x = (kPointList[i].x - xk)/kk;
                y = (kPointList[i].y - yk)/kk; 

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
            /// x = (kk*a + xk) + (x'-x'k)*c - (y'-y'k)*d
            /// y = (kk*b + yk) + (y'-y'k)*c + (x'-x'k)*d
            double ox, oy;
            foreach (var it in this.uPointList)
            {
                ox = it.xT - xTk; 
                oy = it.yT - yTk;

                it.x = (kk*a + xk) + c * ox - d * oy ;
                it.y = (kk*b + yk) + c * oy + d * ox ;
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
                sr.WriteLine("平面四参数转换法(赫尔默特法)计算公式：");
                sr.WriteLine("(x-xk)/kk = dx + k[(x'-x'k)/kk*cosα - (y'-y'k)/kk*sinα]");
                sr.WriteLine("(y-yk)/kk = dy + k[(y'-y'k)/kk*cosα + (x'-x'k)/kk*sinα]");
                sr.WriteLine("平面四参数转换法(赫尔默特法)计算过程数据：");
                sr.WriteLine("公共点坐标");
                sr.WriteLine("点名, 源X(N), 源Y(E), X(N), Y(E)");
                sr.WriteLine("---------------------------------");
                foreach (var pnt in this.kPointList)
                {
                    sr.WriteLine(pnt);
                }

                sr.WriteLine();

                int n0 = this.kPointList.Count;
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

                sr.WriteLine("---------------------------------------------------------------");

                sr.WriteLine("转换公式：");
                sr.WriteLine("x = (kk*a + xk) + (x'-x'k)*c - (y'-y'k)*d");
                sr.WriteLine("y = (kk*b + yk) + (y'-y'k)*c + (x'-x'k)*d");
                sr.WriteLine("转换参数:");
                sr.WriteLine($"源坐标中心平均值：kX'={xTk}, kY'={yTk}");
                sr.WriteLine($"目标坐标中心平均值：kX={xk}, kY={yk}");
                sr.WriteLine($"缩放比例：kk={kk}");
                sr.WriteLine("---------------------------------------------------------------");
                sr.WriteLine("线性计算量：");
                sr.WriteLine($"北向(N) a={a}, 东向(E) b={b}, c=k*cos(α)={c}, d=k*sin(α)={d}");
                sr.WriteLine($"北向(N)dx={a}, 东向(E)dy={b}, 旋转角α={alpha}°, 尺度因子k={k}");

                sr.WriteLine();

                sr.WriteLine("待计算点的坐标:");
                sr.WriteLine("---------------------------------------------------------------");
                sr.WriteLine("待计算点坐标计算数值公式:");
                sr.WriteLine($"x = {kk*a + xk} + (x'-{xTk}) * {c} - (y'-{yTk}) * {d}");
                sr.WriteLine($"y = {kk*b + yk} + (y'-{yTk}) * {c} + (x'-{xTk}) * {d}");
                sr.WriteLine("-------------------------------------------------------------");
                sr.WriteLine("点名, 源X(N), 源Y(E), X(N), Y(E)");
                foreach (var pnt in this.uPointList)
                {
                    sr.WriteLine(pnt);
                }
            }
        }
    }
}