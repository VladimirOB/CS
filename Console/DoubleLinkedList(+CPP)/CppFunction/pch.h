#ifndef PCH_H
#define PCH_H
#define _CRT_SECURE_NO_WARNINGS
// использование математических констант, современный стиль
#include <corecrt_math_defines.h>
#include <algorithm>
#include <iostream>
#include <iomanip> // для манипуляторов вывода setw(), setprecision()
#include <Windows.h> // для поддержки русского языка "тут"
#include <windows.h> // для поддержки русского языка "тут"
#include <conio.h> 
#include <stdio.h>
#include <string>
#include <cstring>
#include <string.h>
#include <time.h>
#include <vector>
#include <io.h>
#include <direct.h>
#include <fstream> // для работы с файловыми потоками
#include <stdarg.h>
#include <ciso646> // для С++. (для С iso646.h)
#include <set> // для set и multiset
#include <unordered_set> 
#include <sstream> // для set и multiset
#include <queue>
#include <deque>
#include <list>
#include <stack>
#include <map>
#include <unordered_map>
#include <random> // для shuffle
#include <functional> // для lambda
using namespace std;

//объявление символической константы - кодовой страницы
#define rus 1251

//объявление константы - команды включения цвета в консоли
// серые буквы на черном фоне
#define GRAY_ON_BLACK "color 07"

//для единообразия кодируем также команду очистки консоли
#define CLEAR "cls"

#endif //PCH_H
#pragma once