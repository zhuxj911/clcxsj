settings.outformat = "pdf";
settings.pdfviewer = "SumatraPDF.exe";
settings.tex = "xelatex";

import math;
import graph;
import geometry; 

size(10cm);
unitsize(1cm);

pair O=(1.9,4.3);
pair ZH=(0,0);
pair JD=(8.3,0);
pair P=(1.9,0);

pair CZ =(5.17,2.11);
pair HY = rotate(-28,O)*CZ;
pair QZ = rotate(28,O)*CZ;

//pair HY=(3.7,0.8);
//pair QZ=(5.8,3.75);
//real R = 3.8;
//real alpha = 28;

draw(arc(O, HY, QZ, CCW), magenta+linewidth(1)); //圆心，起点，方向点
//（用Bezier曲线）画缓和曲线
pair z0=(0,0), z1=(1.8,0), z2=(2.6,0.25), z3=HY;
path p=z0..controls z1 and z2..z3;
draw(p, red+1pt);

dot("$ZH$",ZH, W);
dot("$JD$",JD, SW);
dot("$p$",P, NW);
dot("$HY$",HY, SE);

dot("$i$",CZ, E);
dot("$QZ$",QZ, NE);
dot("$O$",O, N);

draw(ZH--JD, orange);
draw(O--JD, orange);
draw(O--HY, orange);
draw(O--(1.9,-0.6), orange+dashed);
draw(CZ--(5.17,-1), orange);

draw(Label("$\alpha_i$", blue, align=(0,0), filltype=Fill(white)), arc(P,O,CZ,1.7),darkblue,Arrows);
draw(Label("$\beta_0$", blue), arc(P,O,HY,1),darkblue,Arrows);

draw(Label("$R$",MidPoint), O--CZ, N, orange);

draw(Label("$x$",EndPoint), ZH--(JD+(0.5,0)), E, black, Arrow);
draw(Label("$y$",EndPoint), (0,5)--ZH+(0, -1.5), S, black, Arrow);

//标注l0与li
pair HO= O +(-1.5*Cos(34),1.5*Sin(34)); //画缓和曲线的圆心点
draw(Label("$l_i$", blue, align=(0,0), filltype=Fill(white)), 
    arc(CZ,HO,(0,1.1),4),darkblue,Arrows);
	
pair HO= O +(-3*Cos(62),3*Sin(62)); //画缓和曲线的圆心点
draw(Label("$l_0$", blue, align=(0,0), filltype=Fill(white)),
    arc(HY,HO,(0,1.95),5),darkblue,Arrows);

draw(Label("$x_i$", position=MidPoint, align=(0,0), filltype=Fill(white)),
 (0,-0.8)--(5.17,-0.8),arrow=Arrows(), bar=Bars);
 draw(Label("$y_i$", position=MidPoint, align=(0,0), filltype=Fill(white)),
 CZ--(5.17,0),bar=Bars);
 
 draw(Label("$m$", position=MidPoint, align=(0,0), filltype=Fill(white)),
 (0,-0.4)--(1.9,-0.4),arrow=Arrows(), bar=Bars);
