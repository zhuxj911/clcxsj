%% 平面坐标四参数转换时的病态矩阵问题探讨?
%% 以下计算将坐标值进行了中心化处理
%% (x-xk)/kk = dx + k[(x'-x'k)/kk*cosα - (y'-y'k)/kk*sinα]
%% (y-yk)/kk = dy + k[(y'-y'k)/kk*cosα + (x'-x'k)/kk*sinα]
clear

XY = [103,3927002.191,449256.848,327156.644,485664.463;
      100,3928471.180,451589.920,328638.283,487989.669;
      102,3928308.824,446388.500,328447.816,482788.977]

BL = [1, 0, -0.925207333333325, -0.178425333333376, -0.924270333333348;
      0, 1,  0.178425333333376, -0.925207333333325,  0.183426666666695;
      1, 0,  0.543781666666735, -2.51149733333336,   0.557368666666676;
      0, 1,  2.51149733333336,   0.543781666666735,  2.5086326666667;
      1, 0,  0.38142566666659,   2.68992266666662,   0.366901666666672;
      0, 1, -2.68992266666662,   0.38142566666659,  -2.69205933333328];

B = BL(:, 1:4)
l = BL(:, 5)

N = B'*B

C2 = cond(N)      %%2-范数，   此处运算值为 C2 = 2941224.10013783 ， 仍然很大
C1 = cond(N, 1)   %%1-范数，   此处运算值为 C1 = 2941224.10001782 ， 仍然很大
C8 = cond(N, Inf) %%Inf-范数， 此处运算值为 C8 = 2941224.10001782 ， 仍然很大

U = B'*l

X = N \ U %% 有矩阵病态问题存在?

K=sqrt(X(3)*X(3) + X(4)*X(4))

%{
N =
                         3                         0                         0      -1.1607381722456e-13
                         0                         3      1.15907283770866e-13     -5.55111512312578e-17
                         0      1.15907283770866e-13          14.8723310574112                         0
      -1.1607381722456e-13     -5.55111512312578e-17                         0          14.8723310574112
	  
C2 =
          4.95744368580374
C1 =
          4.95744368580382
C8 =
          4.95744368580382
U =
                         0
      1.15019105351166e-13
          14.8727579032329
       -0.0803593386142741
X =
     -2.09059699442154e-16
     -2.97268326079454e-16
          1.00002870066703
      -0.00540327795986152
K =
           1.0000432978479
%}
