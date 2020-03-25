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

dot("$o'$", locate(0),SW, red);
dot("$P$", P);
dot(PX, red);
dot(PY, red);
draw(Label("$y'_p$", EndPoint), P--PX, red+dashed);
draw(Label("$x'_p$", EndPoint), P--PY, red+dashed);

draw(Label("$y'$", EndPoint), locate(0)--(point)(6, 0), red, Arrow);
draw(Label("$x'$", EndPoint), locate(0)--(point)(0, 6),red,  Arrow);


//在默认坐标系作图
//show(defaultcoordsys);
pair OP = changecoordsys(defaultcoordsys, P);//将P点转换到默认坐标系

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
dot("$o$", locate(0),SW);

draw(Label("$dx$", MidPoint, align=(0,0), filltype=Fill(white)),
 (ox,oy)--(ox, 0), dashed);
draw(Label("$dy$", MidPoint,align=(0,0), filltype=Fill(white)),
 (ox,oy)--(0, oy), dashed);

 //画辅助计算虚线
draw((ox, oy)--(ox, oy)+(0,4.5), dashed); //x方向延伸

//标注坐标系转角
pair PXX = changecoordsys(defaultcoordsys, PY);//将PX点转换到默认坐标系
draw(Label("$\alpha_i$", blue, align=N, filltype=Fill(white)),
arc(PXX,(ox, oy),(ox, oy)+(0,3),1.3),darkblue,Arrow); //画源坐标系 -> 新坐标系 的旋转角

draw(PXX--PXX+(0,-1.3), dashed); //x'p -> y画虚线, PXX -> 图上的x'p点
draw(PXX--PXX+(-1.5,0), dashed); //x'p -> x画虚线

