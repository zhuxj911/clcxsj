# 对测量平面四参数坐标转换中存在的问题进行研究与探讨

CoordniateTransform-v 为 CoordniateTransform的改良版

CoordniateTransform-v的主要思想：
先把新旧坐标根据其中心点尽可能规划为小数，再进行计算
以避免病态矩阵的影响



XYtoXY*.m 系列文件为matlab模拟计算