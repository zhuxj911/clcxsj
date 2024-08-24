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
dot("$O$",O, SW);

pair QZ=(0,R);
dot("$QZ$",QZ, NE);

pair JD =(0,JDY);
dot("$JD$",JD, NW);

pair ZY = JD + (-T*Cos(a2), -T*Sin(a2));
dot("$ZY$",ZY, NW);
pair ZY0 = ZY + (-1*Cos(a2), -1*Sin(a2));


pair YZ = JD  + (T*Cos(a2), -T*Sin(a2));
dot("$YZ$", YZ, NE);
pair YZ0 = YZ + (1*Cos(a2), -1*Sin(a2));

draw(arc(O, YZ, ZY, direction=CCW), magenta+linewidth(1.5)); //圆心，起点，方向点
//markangle("$\alpha$", O, ZY, YZ, radius = 1, red);


pair Li = rotate(-25, O)*ZY; //将ZY点绕O点旋转-25°得到Li点
dot("$l_i$", Li, N, darkblue+0.3mm, UnFill);
draw(O--Li, blue, Arrow);
draw(Label("$\alpha_i$", MidPoint,N), arc(ZY,O,Li,2),darkblue,Arrows);


draw(O -- JD, orange);
draw(Label("$R$",MidPoint,SW), O -- ZY, darkblue);
draw(O -- YZ, orange);
draw(Label("$T$",MidPoint, N), JD--ZY, darkblue);
draw(ZY--ZY0, magenta+linewidth(2));

draw(JD -- YZ, orange);
draw(YZ -- YZ0, magenta+linewidth(2));

pair OX = JD + (1*Cos(a2), 1*Sin(a2));
pair OY = O + (1*Sin(a2), -1*Cos(a2));

draw(Label("$x$",EndPoint), ZY--OX, Arrow);//测量坐标系的y轴
draw(Label("$y$",EndPoint), O--OY, Arrow);//测量坐标系的x轴

draw(Label("$\alpha$", red, align=(0,0), filltype=Fill(white)),
 arc(YZ,O,ZY,1),red,Arrows);
draw(Label("$\alpha$", red, align=(0,0), filltype=Fill(white)),
 arc(YZ,JD,OX,0.6),red,Arrows);


