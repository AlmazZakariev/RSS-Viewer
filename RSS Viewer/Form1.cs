using System;
using System.Windows.Forms;


namespace RSS_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public bool CheckNumber(string t)
        {
            try
            {
                Convert.ToInt32(t);
                return true;

            }
            catch (System.FormatException)
            {
                MessageBox.Show("Введено некорректное количество новостей. Пожалуйста, попробуйте снова",
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            catch (System.OverflowException)
            {
                MessageBox.Show("Вы ввели слишком большое число. Пожалуйста, попробуйте снова",
                        "Ошибка!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return false;
            }
        }
        public bool SizeOfNumber(int t)
        {
            if (Convert.ToInt32(t) > feed.Length / 4 || Convert.ToInt32(t) < 1)
            {
                MessageBox.Show("Вы ввели недоступное число. Пожалуйста, попробуйте снова",
                            "Ошибка!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                return false;
            }
            else 
                return true;
        }
        public void Print(int count, int j)
        {
            for (; j < count; j++)
            {
                for(int i =0; i<4; i++)
                {
                    if (feed[j, i] != null)
                    {
                        richTextBox1.Text += feed[j, i] + "\n";
                    }
                    else
                    {
                        if (i == 0)
                        {
                            richTextBox1.Text += "Заголовок отсутствует" + "\n";
                        }
                        if (i == 1)
                        {
                            richTextBox1.Text += "Дата отсутствует" + "\n";
                        }
                        if (i == 2)
                        {
                            richTextBox1.Text += "Описание отсутствует" + "\n";
                        }
                        if (i == 3)
                        {
                            richTextBox1.Text += "Ссылка отсутствует" + "\n";
                        }
                    }
                }
                richTextBox1.Text += "\n" + "\n";
                label5.Text = "Новости получены успешно";
            }
        }
    public string[,] Feed(bool isOld)
        {
            NewRss CurrentFeed;
            if (isOld)
            {
                CurrentFeed = new NewRss(true);
            }
            else
            {
                CurrentFeed = new NewRss(textBox1.Text);
            }
            string[,] newfeed = new string[CurrentFeed.Items.Count, 4];
            int i = 0;
            foreach (Item feedItem in CurrentFeed.Items)
            {

                newfeed[i, 0] = feedItem.title;
                newfeed[i, 1] = feedItem.pubDate;
                newfeed[i, 2] = feedItem.description;
                newfeed[i, 3] = feedItem.link;
                i++;
            }
            return newfeed;
        }
        string[,] feed;
        int FirstNumber;
        private void button1_Click(object sender, EventArgs e)
        {
            label5.Text = "Поиск новостей";
            richTextBox1.Clear();
            feed = Feed(false);
            if (CheckNumber(textBox2.Text) == true )
            {
                if (SizeOfNumber(Convert.ToInt32(textBox2.Text)) == true)
                {
                    FirstNumber = Convert.ToInt32(textBox2.Text);
                    Print(Convert.ToInt32(FirstNumber), 0);
                }
            }
        }
        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label5.Text = "Поиск новостей";
            if (CheckNumber( textBox3.Text) == true)
            {
                if (SizeOfNumber(FirstNumber + Convert.ToInt32(textBox3.Text)) == true)
                {
                    richTextBox1.Clear();
                    Print(FirstNumber+ Convert.ToInt32(textBox3.Text), FirstNumber);
                    FirstNumber += Convert.ToInt32(textBox3.Text);
                } 
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для использования программы найдите в интернете интересующий вас новостной RSS ресурс. " +
                "Вставьте ссылка на него в после URL, введите количества новостей, которое нужно вывести на экран и" +
                " нажмите кнопку «Получить новости». \n Если вы захотите увидеть больше новостей с того же сайта, " +
                "введите желаемое количество новостей в поле снизу и нажмите кнопку «Ещё».",
                            "Справка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            feed = Feed(true);
            if (CheckNumber(textBox2.Text) == true)
            {
                if (SizeOfNumber(Convert.ToInt32(textBox2.Text)) == true)
                {
                    FirstNumber = Convert.ToInt32(textBox2.Text);
                    Print(Convert.ToInt32(FirstNumber), 0);
                }
            }

        }
    }
}
