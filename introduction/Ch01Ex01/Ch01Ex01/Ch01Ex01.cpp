// Ch01Ex01.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#define _USE_MATH_DEFINES
#include <math.h>
#include <string.h>
#define PI M_PI

typedef struct _point {
    char name[11];
    double x, y, z;
}Point;

typedef struct _circle {
    Point center;
    double r;
    double area;
    double length;
}Circle;

//计算圆的面积
void Area(Circle * c) {
    c->area = PI * c->r * c->r;
}

//计算两点的距离
double Distance(const Point * p1, const Point * p2) {
    double dx = p2->x - p1->x;
    double dy = p2->y - p1->y;
    return sqrt(dx*dx + dy * dy);
}

//判断两圆是否相交
bool IsIntersectWithCircle(const Circle * c1, const Circle * c2) {
    double d = Distance(&c1->center, &c2->center);
	return d <= (c1->r + c2->r);
}

int main()
{
    Point pt1;
    strcpy_s(pt1.name, 11, "pt1");
    pt1.x = 100; pt1.y = 100; pt1.z = 425.324;

    Point pt2;
    strcpy_s(pt2.name, 11,  "pt2");
    pt2.x = 200; pt2.y = 200; pt2.z = 417.626;

    Circle c1;
    c1.center = pt1; c1.r = 80;

    Circle c2;
    c2.center = pt2; c2.r = 110;
    
    Area(&c1) ; //计算圆c1的面积
    printf("Circle1 的面积 = %lf \n", c1.area );

    bool yes = IsIntersectWithCircle(&c1, &c2);
    printf("Circle1 与 Circle2 是否相交 ：%s\n", (yes ? "是" : "否")  );

    return 0;
}
