namespace CoordniateTransform
{
    public class GeoPoint : NotificationObject
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

        private double _oX;
        public double oX //源X坐标
        {
            get { return _oX; }
            set
            {
                _oX = value;
                RaisePropertyChange("oX");
            }
        }
  
        private double _oY;
        public double oY  //源Y坐标
        {
            get { return _oY; }
            set
            {
                _oY = value;
                RaisePropertyChange("oY");
            }
        }


        private double _X;
        public double X //新X坐标
        {
            get { return _X; }
            set
            {
                _X = value;
                RaisePropertyChange("X");
            }
        }

        private double _Y;
        public double Y  //新Y坐标
        {
            get { return _Y; }
            set
            {
                _Y = value;
                RaisePropertyChange("Y");
            }
        }

 
        public override string ToString()
        {
            return $"{Name},{oX:0.000},{oY:0.000},{X:0.000},{Y:0.000}";
        }
    }
}
