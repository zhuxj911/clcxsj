import settings;
settings.outformat = "pdf";
settings.pdfviewer = "SumatraPDF.exe";
settings.tex = "xelatex";

import math;
import graph;
import geometry; 

size(10cm);
unitsize(1cm);

import geometry;
size(10cm);

real ox = 2;
real oy = 3; 

coordsys R = shift((ox,oy))*rotate(-20)*currentcoordsys; 
currentcoordsys = R;
//show(currentcoordsys);

real px = 3.5;
real py = 4.5; 

//在R坐标系下定义point
point P =(px, py);
point PX =(px, 0);
point PY =(0, py);

dot("$ZH$", locate(0),SW, red);
dot("$P$", P);
dot(PX, red);
dot(PY, red);
draw(Label("$y'_p$", EndPoint), P--PX, red+dashed);
draw(Label("$x'_p$", EndPoint), P--PY, red+dashed);

point JD =(0,py+1);
dot("$JD$", JD, red);

point CO =(px+1, 0);
dot("$O$", CO, N, red);

draw(Label("$y'$", EndPoint), locate(0)--(point)(6, 0), red, Arrow);
draw(Label("$x'$", EndPoint), locate(0)--(point)(0, 6),red,  Arrow);


//在默认坐标系作图
//show(defaultcoordsys);
pair OP = changecoordsys(defaultcoordsys, P);//将P点转换到默认坐标系
pair PXX = changecoordsys(defaultcoordsys, PX);//将PX点转换到默认坐标系
currentcoordsys = defaultcoordsys;
point OPX =(xpart(OP),0);
point OPY =(0, ypart(OP));

//dot(OP);
dot(OPX);
dot(OPY);
draw(Label("$y_p$", EndPoint), OP--OPX, dashed);
draw(Label("$x_p$", EndPoint), OP--OPY, dashed);
//draw(locate(0)--OP, dashed);

draw(Label("$y$", EndPoint), locate(0)--(point)(9, 0), Arrow);
draw(Label("$x$", EndPoint), locate(0)--(point)(0, 9), Arrow);
dot("$O$", locate(0),SW);

draw(Label("$x_{ZH}$", MidPoint, align=(0,0), filltype=Fill(white)),
 (ox,oy)--(ox, 0), dashed);
draw(Label("$y_{ZH}$", MidPoint,align=(0,0), filltype=Fill(white)),
 (ox,oy)--(0, oy), dashed);

draw((ox, oy)--(ox, oy)+(0,5), dashed);

//标注坐标系转角
pair PJD = changecoordsys(defaultcoordsys, JD);//将JD点转换到默认坐标系
draw(Label("$\alpha$", blue, align=N, filltype=Fill(white)),
 arc((ox, oy)+(0,5),(ox, oy),PJD,1.3),darkblue,Arrow);

