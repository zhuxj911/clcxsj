settings.outformat = "pdf";
settings.pdfviewer = "D:/texlive/SumatraPDF.exe";
settings.tex = "xelatex";

import math;
import graph;
import geometry; 

size(15cm);
unitsize(1cm);

real _alpha = 100; //100°，左偏
real _R = 8;
real _l0 = 8; //ls

//绘图时的单位为度，须将beta0转换为度，不能直接用弧度
real _beta0 = _l0/2/_R*180/pi;
real _m = _l0/2 - _l0^3/240/_R^2;
real _p = _l0^2 / 24 /_R;
real _T = _m + (_R+_p)*Tan(_alpha /2);
real _E = (_R+_p)/Cos(_alpha/2) - _R;

pair ZH=(0,0);
pair JD=(0,_T);
pair O=(-(_p + _R), _m);

draw(JD--O, dashed);

pair QZ = O  + _R * dir(O--JD);
pair HZ = rotate(180+_alpha, JD)*ZH; //将ZH点绕JD点旋转180°+50°得到HZ点

draw(JD--ZH, dashed);
draw(JD--HZ, dashed);

pair ON=(0, 15);
pair OE=(-13, 0);
draw(Label("$x$",EndPoint), ZH--ON, N, black, Arrow);
draw(Label("$y$",EndPoint), OE--(ZH+(1,0)), E, black, Arrow); 


pair pm = (_p, _m);
//dot("$pm$",pm, E);
//draw(pm--O, dashed);
pair mp = rotate(_alpha, O)* pm; //将(p,m)点绕O点旋转50°得到(m,p)点
//dot("$mp$",mp, E);
//draw(mp--O, dashed);

//draw(arc(O,pm, mp, CCW), magenta+linewidth(0.3) + dashed);

pair HY =( -(_l0^2 / 6 / _R - _l0^4 / 336 / _R^3), _l0 - _l0^3 / 40 / _R^2);
draw(HY--O, dashed);

real beta = _alpha - _beta0*2;
pair YH = rotate(beta,O)* HY; //将HY点绕O点旋转50°/2得到YH点
draw(YH--O, dashed);

//圆心，起点，方向点
draw(arc(O, HY, YH, CCW), magenta+linewidth(2)); 

pair OT= O + _p *dir(O--JD);
draw(arc(O, -(_R + _p), 180, 180+_alpha), darkblue+linewidth(0.5) + dashed);


//（用Bezier曲线）画缓和曲线
pair z0= ZH;
pair z1= ZH + (xpart(HY)*0.01, ypart(HY)*0.5);
pair z2=ZH +(xpart(HY)*0.18, ypart(HY)*0.7);
pair z3= HY;
path lp=z0..controls z1 and z2..z3;
draw(lp, red+2pt);

//让lp路径图形绕直线 O-JD 做 反射（镜像）
path rp = reflect(O,JD)*lp;
draw(rp, red+2pt);

pair ROX = JD + 1*dir(HZ--JD); 
pair ROY = rotate(-90, HZ)* JD;
draw(Label("$x'$",EndPoint), HZ--ROX,red, Arrow);
pair ROYY = ROY + 1*dir(ROY--HZ); 
draw(Label("$y'$",EndPoint), HZ--ROYY, red, Arrow); 

draw(Label("$\alpha$", red, align=(0,0), filltype=Fill(white)),
 arc(ON,JD,HZ,0.6),red,Arrows);

dot("$ZH$",ZH, S, darkblue+0.3mm, UnFill);
dot("$JD$",JD, SE, darkblue+0.3mm, UnFill);
dot("$O$",O, SE, darkblue+0.3mm, UnFill);
dot("$QZ$",QZ, E, darkblue+0.3mm, UnFill);
dot("$HZ$",HZ, N, darkblue+0.3mm, UnFill);
dot("$HY$",HY, SW, darkblue+0.3mm, UnFill);
dot("$YH$",YH, SW, darkblue+0.3mm, UnFill);
dot("$O'$",OT, NW, red+0.3mm, UnFill);
