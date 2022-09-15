#define CPP_Func _declspec(dllexport)
#include "pch.h"

extern "C"
{
	// при добавлении новых методов не забывай пересобрать решение!
	priority_queue<char> q;
	CPP_Func int AddNumbers(int a, int b)
	{
		return a + b;
	}
	CPP_Func void Add(char ch)
	{
		
		q.push(ch);
	}

	CPP_Func char Pop()
	{
		char ch = q.top();
		q.pop();
		return ch;
	}

	CPP_Func void Print()
	{
		while (!q.empty())
		{
			cout << q.top() << endl;
			q.pop();
		}
	}
}