//sin, cos, tan 等标准三角函数单位为弧度
// Asymptote中定义的Sin,Cos,Tan,aSin, aTan的单位为度

//圆曲线图

settings.outformat = "pdf";
settings.pdfviewer = "SumatraPDF.exe";
settings.tex = "xelatex";

//usepackage("xeCJK");
//usepackage("amsmath,  amssymb, amsfonts");

import math;
import graph;
import geometry;

unitsize(1cm);
size(10cm);
defaultpen(fontsize(10pt));

//xaxis("$X$",blue, Arrow); 
//yaxis("$Y$",blue, Arrow); 

real R = 5;
real a = 70;
real a2 = a*0.5;
real T = R*Tan(a2);
real JDY = R/Cos(a2); 

pair O = (0,0);
dot("$O$",O, N);

pair QZ=(0,-R);
dot("$QZ$",QZ, SE);

pair JD =(0,-JDY);
dot("$JD$",JD, S);

pair ZY = JD + (-T*Cos(a2), T*Sin(a2));
dot("$ZY$",ZY, SW);
pair ZY0 = ZY + (-2*Cos(a2), 2*Sin(a2));


pair YZ = JD  + (T*Cos(a2), T*Sin(a2));
dot("$YZ$", YZ, SE);
pair YZ0 = YZ + (2*Cos(a2), 2*Sin(a2));

draw(arc(O, YZ, ZY, direction=CW), magenta+linewidth(2)); //圆心，起点，方向点
//markangle("$\alpha$", O, ZY, YZ, radius = 1, red);
draw(Label("$\alpha$", red), arc(ZY,O,YZ,1),red,Arrows);

pair Li = rotate(25, O)*ZY; //将ZY点绕O点旋转-25°得到Li点
dot("$l_i$", Li, S, darkblue+0.3mm, UnFill);
draw(O--Li, blue, Arrow);
draw(Label("$\alpha_i$", blue), arc(ZY,O,Li,2),darkblue,Arrows);


draw(O -- JD, orange);
draw(Label("$R$",MidPoint), O -- ZY, orange);
draw(O -- YZ, orange);
draw(Label("$T$",MidPoint, S), JD--ZY, orange);
draw(JD--ZY0, orange);

draw(JD -- YZ0, orange);

pair OX = JD + (2*Cos(a2), -2*Sin(a2));
pair OY = O + (2*Sin(a2), 2*Cos(a2));

draw(Label("$x$",EndPoint), ZY--OX, Arrow);
draw(Label("$y$",EndPoint), ZY--OY, Arrow);


