using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Serial_Attributes_
{

    [AttributeUsage(AttributeTargets.Property)]
    class Storable : Attribute { }
    

    class MySerializer
    {

        public void Serialize(FileStream fileStream, object obj)
        {
            StreamWriter sw = new StreamWriter(fileStream);
            Type t = obj.GetType();
            sw.WriteLine(t.FullName);
            PropertyInfo[] propInfo = t.GetProperties();
            foreach (PropertyInfo prop in propInfo)
            {
                Attribute storableFieldAttr = prop.GetCustomAttribute(typeof(Storable));
                if(storableFieldAttr != null)
                {
                    if (prop.PropertyType.Name.Equals("Int32"))
                    {
                        var name = prop.Name;
                        var val = prop.GetValue(obj);

                        sw.WriteLine(name + "-" + val + "|");
                    }
                    if (prop.PropertyType.Name.Equals("Double"))
                    {
                        var name = prop.Name;
                        var val = prop.GetValue(obj);

                        sw.WriteLine(name + "-" + val + "|");
                    }
                    if (prop.PropertyType.Name.Equals("String"))
                    {
                        var name = prop.Name;
                        var val = prop.GetValue(obj);

                        sw.WriteLine(name + "-" + val + "|");
                    }
                    if (prop.PropertyType.Name.Equals("Boolean"))
                    {
                        var name = prop.Name;
                        var val = prop.GetValue(obj);

                        sw.WriteLine(name + "-" + val + "|");
                    }
                }
            }
            sw.Close();
        }

        public object Deserialize(FileStream fileStream, Type targetType)
        {
            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(fileStream);
            string className = sr.ReadLine();
            string allText = sr.ReadToEnd();
            string[] properties = allText.Split(new[] { '|', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            object obj = Assembly.GetExecutingAssembly().CreateInstance(className);
            Type t = obj.GetType();
            if(t != targetType)
            {
                throw new Exception("Invalid type");
                return null;
            }
            PropertyInfo[] propInfo = t.GetProperties();
            foreach (var prop in propInfo)
            {
                Attribute storableFieldAttr = prop.GetCustomAttribute(typeof(Storable));
                if (storableFieldAttr != null)
                {
                    var name = prop.Name;
                    if (prop.PropertyType.Name.Equals("Int32"))
                    {
                        foreach (var item in properties)
                        {
                            if(item.StartsWith(name))
                            {
                                for (int i = 0; i < item.Length; i++)
                                {
                                    if (item[i] == '-')
                                    {
                                        i++;
                                        for (int k = i; k < item.Length; k++)
                                        {
                                            sb.Append(item[k]);
                                        }
                                        prop.SetValue(obj, Convert.ToInt32(sb.ToString()));
                                        sb.Clear();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (prop.PropertyType.Name.Equals("Double"))
                    {
                        foreach (var item in properties)
                        {
                            if (item.StartsWith(name))
                            {
                                for (int i = 0; i < item.Length; i++)
                                {
                                    if (item[i] == '-')
                                    {
                                        i++;
                                        for (int k = i; k < item.Length; k++)
                                        {
                                            sb.Append(item[k]);
                                        }
                                        prop.SetValue(obj, Convert.ToDouble(sb.ToString()));
                                        sb.Clear();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (prop.PropertyType.Name.Equals("String"))
                    {
                        foreach (var item in properties)
                        {
                            if (item.StartsWith(name))
                            {
                                for (int i = 0; i < item.Length; i++)
                                {
                                    if (item[i] == '-')
                                    {
                                        i++;
                                        for (int k = i; k < item.Length; k++)
                                        {
                                            sb.Append(item[k]);
                                        }
                                        prop.SetValue(obj, sb.ToString());
                                        sb.Clear();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (prop.PropertyType.Name.Equals("Boolean"))
                    {
                        foreach (var item in properties)
                        {
                            if (item.StartsWith(name))
                            {
                                for (int i = 0; i < item.Length; i++)
                                {
                                    if (item[i] == '-')
                                    {
                                        i++;
                                        for (int k = i; k < item.Length; k++)
                                        {
                                            sb.Append(item[k]);
                                        }
                                        prop.SetValue(obj, Convert.ToBoolean(sb.ToString()));
                                        sb.Clear();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return obj;
        }
    }
}
