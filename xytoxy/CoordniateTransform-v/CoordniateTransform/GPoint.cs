namespace CoordniateTransform
{
    public class GPoint : NotificationObject
    {
        private string _name;
        public string Name //点名
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChange("Name");
            }
        }       

        private double _xT;
        /// <summary>
        /// /源(旧)X坐标
        /// </summary>
        public double xT 
        {
            get { return _xT; }
            set
            {
                _xT = value;
                RaisePropertyChange("xT");
            }
        }
  
        private double _yT;
        /// <summary>
        /// /源(旧)Y坐标
        /// </summary>
        public double yT  //源Y坐标
        {
            get { return _yT; }
            set
            {
                _yT = value;
                RaisePropertyChange("yT");
            }
        }


        private double _x;
        /// <summary>
        /// (新)X坐标
        /// </summary>
        public double x //新X坐标
        {
            get { return _x; }
            set
            {
                _x = value;
                RaisePropertyChange("x");
            }
        }

        private double _y;
        /// <summary>
        /// (新)Y坐标
        /// </summary>
        public double y
        {
            get { return _y; }
            set
            {
                _y = value;
                RaisePropertyChange("y");
            }
        }

 
        public override string ToString()
        {
            return $"{Name},{xT:0.000},{yT:0.000},{x:0.000},{y:0.000}";
        }
    }
}
