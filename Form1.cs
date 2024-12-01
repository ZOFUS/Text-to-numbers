using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Zadanie_1
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, int> unitsMap = new Dictionary<string, int>
        {
            {"zéro", 0},
            {"un", 1},
            {"une", 1},
            {"deux", 2},
            {"trois", 3},
            {"quatre", 4},
            {"cinq", 5},
            {"six", 6},
            {"sept", 7},
            {"huit", 8},
            {"neuf", 9},
            {"dix", 10},
            {"onze", 11},
            {"douze", 12},
            {"treize", 13},
            {"quatorze", 14},
            {"quinze", 15},
            {"seize", 16},
            {"dix-sept", 17},
            {"dix-huit", 18},
            {"dix-neuf", 19}
        };

        private readonly Dictionary<string, int> tensMap = new Dictionary<string, int>
        {
            {"vingt", 20},
            {"trente", 30},
            {"quarante", 40},
            {"cinquante", 50},
            {"soixante", 60},
            {"soixante-dix", 70},
            {"quatre-vingt", 80},
            {"quatre-vingts", 80},
            {"quatre-vingt-dix", 90}
        };

        private readonly Dictionary<string, int> hundredsMap = new Dictionary<string, int>
        {
            {"cent", 100},
            {"deux cents", 200},
            {"trois cents", 300},
            {"quatre cents", 400},
            {"cinq cents", 500},
            {"six cents", 600},
            {"sept cents", 700},
            {"huit cents", 800},
            {"neuf cents", 900}
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = number.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Вы ничего не ввели! Пожалуйста, введите числительное на французском.");
                return;
            }

            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int result = 0;
            bool isValidInput = true;

            for (int i = 0; i < words.Length; i++)
            {
                string writtenWord = words[i];

                if (writtenWord == "et")
                {
                    continue;
                }

                if (i < words.Length - 1)
                {
                    string twoWords = writtenWord + " " + words[i + 1];
                    if (hundredsMap.ContainsKey(twoWords))
                    {
                        result += hundredsMap[twoWords];
                        i++;
                        continue;
                    }
                }

                if (hundredsMap.ContainsKey(writtenWord))
                {
                    result += hundredsMap[writtenWord];
                    continue;
                }

                if (tensMap.ContainsKey(writtenWord))
                {
                    result += tensMap[writtenWord];
                    continue;
                }

                if (writtenWord.Contains("-"))
                {
                    string[] hyphenParts = writtenWord.Split('-');

                    int temp = 0;
                    bool wordValid = true;
                    int j = 0;
                    while (j < hyphenParts.Length)
                    {
                        if (j < hyphenParts.Length - 1)
                        {
                            string twoParts = hyphenParts[j] + "-" + hyphenParts[j + 1];
                            if (tensMap.ContainsKey(twoParts))
                            {
                                temp += tensMap[twoParts];
                                j += 2;
                                continue;
                            }
                        }

                        string singlePart = hyphenParts[j];
                        if (unitsMap.ContainsKey(singlePart))
                        {
                            temp += unitsMap[singlePart];
                        }
                        else if (tensMap.ContainsKey(singlePart))
                        {
                            temp += tensMap[singlePart];
                        }
                        else
                        {
                            MessageBox.Show($"Введённое вами слово \"{singlePart}\" не является числительным. Пожалуйста, проверьте ввод.");
                            isValidInput = false;
                            wordValid = false;
                            break;
                        }
                        j++;
                    }

                    if (wordValid)
                    {
                        result += temp;
                        continue;
                    }
                }

                if (unitsMap.ContainsKey(writtenWord))
                {
                    result += unitsMap[writtenWord];
                    continue;
                }

                MessageBox.Show($"Введённое вами слово \"{writtenWord}\" не является числительным. Пожалуйста, проверьте ввод.");
                isValidInput = false;
                break;
            }

            if (isValidInput)
            {
                resultLabel1.Text = result.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputText.Text) ||
                string.IsNullOrWhiteSpace(left.Text) ||
                string.IsNullOrWhiteSpace(right.Text))
            {
                MessageBox.Show("Вы ввели не все значения. Пожалуйста, заполните все поля.");
                return;
            }

            if (!(int.TryParse(left.Text, out int l) && int.TryParse(right.Text, out int r)))
            {
                MessageBox.Show("Один или несколько введенных вами индексов не являются числами. Пожалуйста, введите корректные числа.");
                return;
            }

            string[] str = inputText.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (l < 1 || r < 1 || l > str.Length || r > str.Length || r < l)
            {
                MessageBox.Show("Вы ввели некорректные значения. Убедитесь, что индексы находятся в пределах текста.");
                return;
            }

            if (l == r)
            {
                MessageBox.Show("Начальный и конечный индексы не могут быть одинаковыми.");
                return;
            }

            r--;
            l--;

            string answer = "";

            for (int i = 0; i < l; i++)
            {
                answer += str[i] + " ";
            }

            for (int i = r + 1; i < str.Length; i++)
            {
                answer += str[i] + " ";
            }

            for (int i = l; i <= r; i++)
            {
                answer += str[i] + " ";
            }

            resultLabel2.Text = answer.Trim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
