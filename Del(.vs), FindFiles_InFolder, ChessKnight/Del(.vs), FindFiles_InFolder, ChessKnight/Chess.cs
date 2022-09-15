//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Del_.vs___FindFiles_InFolder__ChessKnight
//{
//    internal class Chess
//    {
//        private const int size = 8;
//        private static readonly int[,] duska = new int[size, size];
//        private static readonly string[] redoveBukvi = { "A", "B", "C", "D", "E", "F", "G", "H" };
//        private static List<List<int>> minMinatRed = new List<List<int>>();
//        private static List<List<int>> minMinataKolona = new List<List<int>>();
//        private static int minCount = int.MaxValue;
//        private static int count;
//        private static int nachalenRed;
//        private static int nachalnaKolona;
//        private static int kraenRed;
//        private static int krainaKolona;
//        private static readonly int[] tempArr = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

//        private static readonly double numberOfVariations = Math.Pow(size, size);
//        private static readonly int[] result = new int[size];

//        private static void Main()
//        {
//            nachalenRed = VuvejdaneNaRed("исходную");
//            nachalnaKolona = VuvejdaneNaKolona("исходный");
//            kraenRed = VuvejdaneNaRed("конечную");
//            krainaKolona = VuvejdaneNaKolona("конечный");

//            for (int i = 0; i < size; i++)
//            {
//                for (int j = 0; j < size; j++)
//                {
//                    duska[i, j] = j + 1;
//                }
//            }
//            Dvijenie(nachalenRed, nachalnaKolona);
//            if (minCount == int.MaxValue)
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.WriteLine("Ход невозможен!");
//            }
//            else
//            {
//                Izvejdane();
//            }
//            Console.ReadKey();
//        }

//        private static void Dvijenie(int red, int kolona)
//        {
//            var comparer = new ListComparer<int>();

//            var tempHodove = new List<Tuple<bool, int[]>>();
//            int novMinatRed = red;
//            int novaMinataKolona = kolona;
//            for (int i = 0; i < numberOfVariations; i++)
//            {
//                var minatRed = new List<int>();
//                var minataKolona = new List<int>();
//                minatRed.Add(nachalenRed);
//                minataKolona.Add(nachalnaKolona);
//                tempHodove.Clear();
//                var hodove = UpdateList(novMinatRed, novaMinataKolona, minatRed, minataKolona);
//                int x = i;
//                for (int j = 0; j < size; j++)
//                {
//                    result[j] = tempArr[x % size];
//                    x /= size;
//                }
//                for (int k = result.Length - 1; k >= 0; k--)
//                {
//                    tempHodove.Add(hodove[result[k]]);
//                }
//                bool trueValue = tempHodove.Any(c => c.Item1);
//                while (trueValue)
//                {
//                    foreach (var hod in tempHodove.Where(hod => hod.Item1))
//                    {
//                        novMinatRed += hod.Item2[0];
//                        novaMinataKolona += hod.Item2[1];
//                        minatRed.Add(novMinatRed);
//                        minataKolona.Add(novaMinataKolona);
//                        count++;
//                        break;
//                    }
//                    if (novMinatRed == kraenRed && novaMinataKolona == krainaKolona)
//                    {
//                        if (minCount > count)
//                        {
//                            minCount = count;
//                        }

//                        minMinatRed.Add(minatRed);
//                        minMinataKolona.Add(minataKolona);
//                        minMinatRed = minMinatRed.Distinct(comparer).ToList();
//                        minMinataKolona = minMinataKolona.Distinct(comparer).ToList();
//                    }
//                    hodove = UpdateList(novMinatRed, novaMinataKolona, minatRed, minataKolona);
//                    tempHodove.Clear();
//                    for (int k = result.Length - 1; k >= 0; k--)
//                    {
//                        tempHodove.Add(hodove[result[k]]);
//                    }
//                    trueValue = tempHodove.Any(c => c.Item1);
//                }
//                count = 0;
//                novMinatRed = nachalenRed;
//                novaMinataKolona = nachalnaKolona;
//            }

//        }

//        private static List<Tuple<bool, int[]>> UpdateList(int red, int kolona, ICollection<int> minatiRedove,
//            ICollection<int> minatiKoloni)
//        {
//            var vsichkiHodove = new List<Tuple<bool, int[]>>()
//        {
//            new Tuple<bool, int[]>(Nadqsno(red, 1, minatiRedove) && Napred(kolona, 2, minatiKoloni), new[]
//            {
//                +1,
//                +2
//            }),
//            new Tuple<bool, int[]>(Nadqsno(red, 2, minatiRedove) && Napred(kolona, 1, minatiKoloni), new[]
//            {
//                +2,
//                +1
//            }),
//            new Tuple<bool, int[]>(Nalqvo(red, 1, minatiRedove) && Napred(kolona, 2, minatiKoloni), new[]
//            {
//                -1,
//                +2
//            }),
//            new Tuple<bool, int[]>(Nalqvo(red, 2, minatiRedove) && Napred(kolona, 1, minatiKoloni), new[]
//            {
//                -2,
//                +1
//            }),
//            new Tuple<bool, int[]>(Nadqsno(red, 2, minatiRedove) && Nazad(kolona, 1, minatiKoloni), new[]
//            {
//                +2,
//                -1
//            }),
//            new Tuple<bool, int[]>(Nalqvo(red, 2, minatiRedove) && Nazad(kolona, 1, minatiKoloni), new[]
//            {
//                -2,
//                -1
//            }),
//            new Tuple<bool, int[]>(Nadqsno(red, 1, minatiRedove) && Nazad(kolona, 2, minatiKoloni), new[]
//            {
//                +1,
//                -2
//            }),
//            new Tuple<bool, int[]>(Nalqvo(red, 1, minatiRedove) && Nazad(kolona, 2, minatiKoloni), new[]
//            {
//                -1,
//                -2
//            })
//        };
//            return vsichkiHodove;
//        }

//        private static bool Napred(int segashnaKolona, int broiHodove, ICollection<int> minatiKoloni)
//        {
//            if (segashnaKolona + broiHodove >= size) return false;
//            bool a = ValidnaKolona(segashnaKolona + broiHodove, minatiKoloni);
//            return a;
//        }

//        private static bool Nazad(int segashnaKolona, int broiHodove, ICollection<int> minatiKoloni)
//        {
//            if (segashnaKolona - broiHodove <= 0) return false;
//            bool a = ValidnaKolona(segashnaKolona - broiHodove, minatiKoloni);
//            return a;
//        }

//        private static bool Nalqvo(int segashenRed, int broiHodove, ICollection<int> minatiRedove)
//        {
//            if (segashenRed - broiHodove < 0) return false;
//            bool a = ValidenRed(segashenRed - broiHodove, minatiRedove);
//            return a;
//        }

//        private static bool Nadqsno(int segashenRed, int broiHodove, ICollection<int> minatiRedove)
//        {
//            if (segashenRed + broiHodove >= size) return false;
//            bool a = ValidenRed(segashenRed + broiHodove, minatiRedove);
//            return a;
//        }

//        private static bool ValidenRed(int novRed, ICollection<int> minatiRedove)
//        {
//            if (minatiRedove.Count <= 1) return true;
//            bool a = !minatiRedove.Contains(novRed);
//            return a;
//        }

//        private static bool ValidnaKolona(int novaKolona, ICollection<int> minatiKoloni)
//        {
//            if (minatiKoloni.Count <= 1) return true;
//            bool a = !minatiKoloni.Contains(novaKolona);
//            return a;
//        }

//        private static int VuvejdaneNaRed(string text)
//        {
//            Console.Write("Введите {0} строку (A-H) : ", text);
//            string red = Console.ReadLine();
//            while (!redoveBukvi.ToList().Contains(red?.ToUpper()))
//            {
//                Console.WriteLine("Неверная строка");
//                red = Console.ReadLine();
//            }
//            return Array.FindIndex(redoveBukvi, item => item == red?.ToUpper());
//        }

//        private static int VuvejdaneNaKolona(string text)
//        {
//            int ret;
//            Console.Write("Введите {0} столбец (1-8) : ", text);
//            while (!int.TryParse(Console.ReadLine(), out ret) || ret > size || ret < 1)
//            {
//                Console.WriteLine("Неверный столбец");
//            }
//            return ret - 1;
//        }

//        private static void Izvejdane()
//        {
//            Console.WriteLine("Начальная позиция : {0},{1}", redoveBukvi[nachalenRed], nachalnaKolona + 1);
//            var naiDobriRedove = new List<List<int>>();
//            var naiDobriKoloni = new List<List<int>>();
//            var skipRedove = new List<int>();
//            if (minMinatRed.Count > 1)
//            {
//                for (int i = 0; i < minMinatRed.Count; i++)
//                {
//                    var uspeshenHod = minMinatRed[i];
//                    if (uspeshenHod.Count - 1 != minCount) continue;
//                    naiDobriRedove.Add(uspeshenHod);
//                    skipRedove.Add(i);
//                }
//            }
//            if (minMinataKolona.Count > 1)
//            {
//                naiDobriKoloni.AddRange(minMinataKolona.Where(uspeshenHod => uspeshenHod.Count - 1 == minCount));
//            }
//            for (int i = 0; i < minMinatRed.Count; i++)
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//                if (skipRedove.Contains(i)) continue;
//                Console.WriteLine("Вариант {0} : {1} ходов", i + 1, minMinatRed[i].Count - 1);
//                var tempRedove = minMinatRed[i];
//                var tempKoloni = minMinataKolona[i];
//                for (int j = 1; j < tempRedove.Count; j++)
//                {
//                    Console.WriteLine("Ход {0} : {1},{2}", j, redoveBukvi[tempRedove[j]], tempKoloni[j] + 1);
//                }
//                Console.WriteLine();
//            }
//            Console.WriteLine();
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.WriteLine(naiDobriRedove.Count > 1 ? "Лучшие ходы : {0}" : "Лучший ход : {0}", minCount);
//            for (int i = 0; i < naiDobriRedove.Count; i++)
//            {
//                Console.WriteLine("Вариант {0} : {1} ходов", i + 1, minCount);
//                var tempRedove = naiDobriRedove[i];
//                var tempKoloni = naiDobriKoloni[i];
//                for (int j = 1; j < tempRedove.Count; j++)
//                {
//                    Console.WriteLine("Ход {0} : {1},{2}", j, redoveBukvi[tempRedove[j]], tempKoloni[j] + 1);
//                }
//                Console.WriteLine();
//            }
//        }
//    }

//    internal class ListComparer<T> : EqualityComparer<List<T>>
//    {
//        public override bool Equals(List<T> l1, List<T> l2)
//        {
//            if (l1 == null && l2 == null) return true;
//            if (l1 == null || l2 == null) return false;

//            return l1.SequenceEqual(l2);
//        }


//        public override int GetHashCode(List<T> list)
//        {
//            return list.Count;
//        }
//    }
//}
