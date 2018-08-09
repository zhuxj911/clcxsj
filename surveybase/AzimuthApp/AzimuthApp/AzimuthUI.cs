using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace AzimuthApp
{
    class AzimuthUI : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _AName = "";
        public string AName
        {
            get => _AName;
            set
            {
                _AName = value;
                RaisePropertyChanged("AName");
            }
        }

        private double _xA;
        public double XA
        {
            get => _xA;
            set
            {
                _xA = value;
                RaisePropertyChanged("XA");
            }
        }


        private double _yA;
        public double YA
        {
            get => _yA;
            set
            {
                _yA = value;
                RaisePropertyChanged("YA");
            }
        }

        private string _BName = "";
        public string BName
        {
            get => _BName;
            set
            {
                _BName = value;
                RaisePropertyChanged("BName");
            }
        }

        private double _xB;
        public double XB
        {
            get => _xB;
            set
            {
                _xB = value;
                RaisePropertyChanged("XB");
            }
        }

        private double _yB;
        public double YB
        {
            get => _yB;
            set
            {
                _yB = value;
                RaisePropertyChanged("YB");
            }
        }

        public string AzimuthName
        {
            get => $"{AName}->{BName}坐标方位角:";
        }

        private double _azimuthAB;
        public string AzimuthAB
        {
            get => ZXY.SMath.RADtoString(_azimuthAB);
        }

        private double _distanceAB;
        public double DistanceAB
        {
            get => _distanceAB;
        }

        public void CalAzimuthDistanceAB()
        {
            _distanceAB = ZXY.SMath.Azimuth(XA, YA, XB, YB, out _azimuthAB);
            RaisePropertyChanged("AzimuthAB");
            RaisePropertyChanged("DistanceAB");
            RaisePropertyChanged("AzimuthName");
        }
    }
}
