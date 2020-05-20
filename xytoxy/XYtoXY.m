%% 平面坐标四参数转换时的病态矩阵问题探讨?
clear

XY = [1,3808260.999,40423578.211,1200.000,885.000;
      2,3811317.151,40425135.398,1200.000,4315.000]

BL = [1, 0, 3808260.999, -40423578.211, 1200;
0, 1, 40423578.211, 3808260.999,  885;
1, 0, 3811317.151, -40425135.398, 1200;
0, 1, 40425135.398, 3811317.151,  4315];

B = BL(:, 1:4)
l = BL(:, 5)

N = B'*B

C2 = cond(N)      %%2-范数，   此处运算值为 C2 = 6.50782600892183e+23 ， 非常大
C1 = cond(N, 1)   %%1-范数，   此处运算值为 C1 = 9.24113245206839e+23 ， 非常大
C8 = cond(N, Inf) %%Inf-范数， 此处运算值为 C8 = 9.24113245206839e+23 ， 非常大

U = B'*l

X = N \ U %% 有矩阵病态问题存在?

K=sqrt(X(3)*X(3) + X(4)*X(4))