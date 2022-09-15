using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumDig__WordsToFile__C__Code
{
    internal class Program
    {

        static void Task1(string temp)
        {
            //1.Пользователь вводит имя текстового файла, программа подсчитывает сумму слов - чисел в файле
            try
            {

                StreamReader sw = new StreamReader(temp); // vin.txt
                string str;
                str = sw.ReadToEnd();
                Console.WriteLine(str);
                string[] str2 = str.Split(' ');
                double num = 0, sum = 0;
                foreach (var item in str2)
                {
                    if (double.TryParse(item, out num))
                        sum += num;
                }
                sw.Close();
                Console.WriteLine($"Sum = {sum}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
        }

        static void Task1_v2(string temp)
        {
            //1.Пользователь вводит имя текстового файла, программа подсчитывает сумму слов - чисел в файле
            try
            {

                StreamReader sw = new StreamReader(temp); // vin.txt
                string str, buffer = "";
                str = sw.ReadToEnd();
                Console.WriteLine(str);
                int sum = 0, num_temp = 0, num = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    if (Int32.TryParse(str[i].ToString(), out num_temp))
                    {
                        for (; Int32.TryParse(str[i].ToString(), out num_temp); i++)
                        {
                            buffer += str[i].ToString();
                            num_temp = Convert.ToInt32(buffer);
                            num = num_temp;
                            if (i == str.Length-1)
                                break;
                        }
                        sum += num;
                        num = 0;
                        buffer = "";
                    }
                    
                }
                sw.Close();
                Console.WriteLine($"Sum = {sum}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Task2(string temp)
        {
            /*2.Пользователь вводит 2 имени файла, программа делит первый файл на слова и записывает их во второй файл
            в столбик, отсортированными в алфавитном порядке убывания*/

            try
            {
                
                StreamReader sr = new StreamReader(temp); // dz.txt
                string str;
                str = sr.ReadToEnd();
                sr.Close();
                string[] str2 = str.Split(' ');
                List<string> strings = new List<string>();
                for (int i = 0; i < str2.Length; i++)
                {
                    strings.Add(str2[i]);
                }
                strings.Sort();
                strings.Reverse();

                Console.Write("Enter result file name: "); //result.txt
                temp = Console.ReadLine();
                StreamWriter sw = new StreamWriter(temp, false, Encoding.Default);
                foreach (var item in strings)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
                //File.WriteAllLines(@"result.txt", strings);
            }
            catch
            {
                Console.WriteLine("Error!");
            }
            
        }

        static void Task3(string file_name)
        {
            /*3. Пользователь вводит имя файла с исходным кодом на языке C++. Программа парсит код из файла и записывает
            его в другой файл, предварительно выровняв лесенкой
            Пример:
            void main(){if(true)cout<<"Hello"<<endl;}

            Результат:
            void main()
            {
	            if(true)
		            cout << "Hello" << endl;
            }*/
            int flag_cnt = 0, temp = -1;
            bool[] flag = new bool[10];
            //Console.Write("Enter source file name: ");
            //string file_name = "task3.cpp"; //Console.ReadLine(); // task3.cpp
            try
            {
                int length = 0;
                StreamReader sr = new StreamReader(file_name, Encoding.Default);
                string source = sr.ReadToEnd();
                StringBuilder builder = new StringBuilder();
                source = System.Text.RegularExpressions.Regex.Replace(source, @"\s+", ""); // удалил все пробелы
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] == '#')
                    {
                        for (; source[i] != '>' && source[i] != '\"'; i++)
                        {
                            builder.Append(source[i]);
                        }
                        if (source[i] == '\"')
                        {
                            builder.Append(source[i]);
                            i++;
                            for (; source[i] != '\"'; i++)
                            {
                                builder.Append(source[i]);
                            }
                        }
                        builder.Append(source[i]);
                        builder.Append('\n');
                        continue;
                    }

                    if (source[i] == 'u' && source[i + 1] == 's' && source[i + 2] == 'i' && source[i + 3] == 'n' && source[i + 4] == 'g' &&
                        source[i + 5] == 'n' && source[i + 6] == 'a')
                    {
                        int co = 0;
                        while(co < 5)
                        {
                            builder.Append(source[i++]);
                            co++;
                        }
                        builder.Append(' ');
                        co = 0;
                        while(co < 9)
                        {
                            builder.Append(source[i++]);
                            co++;
                        }
                        builder.Append(' ');
                        co = 0;
                        while (co < 3)
                        {
                            builder.Append(source[i++]);
                            co++;
                        }
                    }


                    if (source[i] == 'v' && source[i + 1] == 'o' && source[i + 2] == 'i' && source[i + 3] == 'd' ||
                        source[i] == 'i' && source[i + 1] == 'n' && source[i + 2] == 't'&& source[i+3] == 'm' && source[i+4] == 'a' && source[i + 5] == 'i' && source[i + 6] == 'n')
                    {
                        builder.Append('\n');
                        int count = 0;
                        if (source[i] == 'i')
                         count = 1;

                        for (; source[i] != ')'; i++)
                        {
                                count++;
                                if(count==5)
                                    builder.Append(' ');
                                builder.Append(source[i]);
                        }
                        flag[flag_cnt++] = true;
                        length = 0;
                        length += 3;
                        builder.Append(')');
                        for (; source[i] != '{'; i++)
                        {

                        }
                        builder.Append("\n{\n");
                        continue;
                    }

                    if(length != 0)
                    {
                        for (int k = 0; k < length; k++)
                        {
                            builder.Append(' ');
                        }

                        if (source[i] == 'f' && source[i + 1] == 'o' && source[i + 2] == 'r')
                        {
                            for (; source[i] != ')'; i++)
                            {
                                if (source[i] == 'i' && source[i + 1] == 'n' && source[i + 2] == 't')
                                {
                                    for (int q = 0; q < 3; q++)
                                    {
                                        builder.Append(source[i++]);
                                    }
                                    builder.Append(' ');
                                }
                                builder.Append(source[i]);
                            }
                            builder.Append(source[i]);
                            builder.Append('\n');
                            continue;
                        }

                        if (source[i] == 'i' && source[i + 1] == 'n' && source[i + 2] == 't')
                        {
                            for (int q = 0; q < 3; q++)
                            {
                                builder.Append(source[i++]);
                            }
                            builder.Append(' ');
                        }

                        if (source[i] == 'c' && source[i + 1] == 'h' && source[i + 2] == 'a' && source[i + 3] == 'r')
                        {
                            for (int q = 0; q < 4; q++)
                            {
                                builder.Append(source[i]);
                                i++;
                            }
                            builder.Append(' ');
                        }
                        if (source[i] == 'e' && source[i + 1] == 'l' && source[i + 2] == 's' && source[i + 3] == 'e')
                        {
                            temp = i + 4;
                        }
                        for (; source[i] != ')' && source[i] != ';' && source[i] != '}'; i++)
                        {
                            if (source[i] == '{')
                            {
                                if (temp == i)
                                {
                                    builder.Append('\n');
                                    for (int k = 0; k < length; k++)
                                    {
                                        builder.Append(' ');
                                    }
                                }
                                flag[flag_cnt++] = true;
                                length += 3;
                                builder.Append(source[i]);
                                builder.Append('\n');
                                for (int k = 0; k < length; k++)
                                {
                                    builder.Append(' ');
                                }
                                continue;
                            }
                            
                            if (source[i] == '<' && source[i+1] == '<' && source[i+2] != ' ')
                                builder.Append(' ');
                            builder.Append(source[i]);
                        }
                       

                        if (source[i] == ';')
                        {
                            if (source[i+1] == '}')
                            builder.Append(';');
                            else
                                builder.Append(";\n");
                        }
                        if (source[i] == '}' && flag[1] == true)
                        {
                            length -= 3;
                            flag[--flag_cnt] = false;
                            builder.Append('\n');
                            for (int k = 0; k < length; k++)
                            {
                                builder.Append(' ');
                            }
                            builder.Append("}\n");
                            continue;
                        }

                        if (source[i] == '}' && flag[1] == false)
                        {
                            flag[--flag_cnt] = false;
                            builder.Append('\n');
                            builder.Append("}\n");
                            continue;
                        }
                        if (source[i] == ')' && source[i+1] != ';')
                        {
                            length += 3;
                            builder.Append(source[i]);
                            builder.Append('\n');
                        }
                        if (source[i] == ')' && source[i + 1] == ';')
                        {
                            builder.Append(source[i]);
                            builder.Append(";\n");
                            i++;
                            continue;
                        }
                        continue;
                    }
                    builder.Append(source[i]);
                }
                File.WriteAllText("qqqq.txt", builder.ToString());
                Console.WriteLine(builder);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static void Task3_1()
        {
            /*3. Пользователь вводит имя файла с исходным кодом на языке C++. Программа парсит код из файла и записывает
            его в другой файл, предварительно выровняв лесенкой
            Пример:
            void main(){if(true)cout<<"Hello"<<endl;}

            Результат:
            void main()
            {
	            if(true)
		            cout << "Hello" << endl;
            }*/
            //Console.Write("Enter source file name: ");
            string file_name = "task3.cpp"; //Console.ReadLine(); // task3.cpp
            try
            {
                int strings = 0, length = 0;
                StreamReader sr = new StreamReader(file_name, Encoding.Default);
                string source = sr.ReadToEnd();
                char[] ss = new char[source.Length];
                for (int i = 0; i < source.Length; i++)
                {
                    ss[i] = source[i];
                }
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i] == 'v' && ss[i + 1] == 'o' && ss[i + 2] == 'i' && ss[i + 3] == 'd')
                    {
                        for (; ss[i] != ')'; i++)
                        {
                            builder.Append(ss[i]);
                        }
                        length = 0;
                        length += 5;
                        builder.Append(ss[i]);
                        builder.Append("\n");
                        builder.Append("{");
                        builder.Append("\n");
                        i++;
                        continue;
                    }
                    if (length != 0)
                    {
                        for (int k = 0; k < length; k++)
                        {
                            builder.Append(" ");
                        }
                        if (ss[i] == '{')
                        {
                            length += 5;

                        }
                        for (; ss[i] != ')' && ss[i] != ';' && ss[i] != '}'; i++)
                        {
                            if (ss[i] == '<' && ss[i + 1] == '<' && ss[i + 2] != ' ')
                                builder.Append(" ");
                            builder.Append(ss[i]);
                        }
                        if (ss[i] == '}')
                        {
                            builder.Append("\n");
                            builder.Append("}");
                        }
                        if (ss[i] == ')' && ss[i + 1] != ';')
                        {
                            length += 5;
                            builder.Append(ss[i]);
                            builder.Append("\n");
                        }
                        continue;
                    }
                    builder.Append(ss[i]);
                }
                Console.WriteLine(builder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void Main(string[] args)
        {
            Console.Write("Enter file name: ");
            string temp = Console.ReadLine();
            Task1(temp);
            //Task1_v2(temp);
            //Task2(temp);
            //Task3(temp);
            //Task3_1();
        }
    }
}