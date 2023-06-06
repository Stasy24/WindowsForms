using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace KursovaKutovyiForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public class one : IComparable
        {
            public string surname;
            public string name;
            public string father;
            public string posada;
            public int zarp;
            public int godin;
            public string osna;
            public one(string sur, string nam, string fat, string pos,string osn, int zp, int god)
            {
                surname = sur;
                name = nam;
                father = fat;
                posada = pos;
                zarp = zp;
                godin=god;
                osna = osn;


                 
               
            }
            public virtual int salary()
            {

                return 0;
            }
            
            public one()
            {
                Random r = new Random();
                surname = "Кутовенко";
                name = "Хаос";
                 father = "Кутовий";
                posada = "Бос";
                zarp = r.Next(150, 300);
                godin = r.Next(150, 300);
                osna = "івостмлцмо";

                
            }
            public int CompareTo(object obj)
            {
                one b;
                b = (one)obj;
                return zarp.CompareTo(b.zarp);

            }

        }
        class fixedu : one
        {
            public override int salary()
            {
                return zarp;
            }
        }
        class pogod : one 
        {
            public override int salary()
            {
                return zarp*godin;
            }

        }
        class SortByAmount : IComparer<one>
        {
            public int Compare(one x, one y)
            {
                one t1 = x;
                one t2 = y;
                if (t1.zarp > t2.zarp) return 1;
                if (t1.zarp < t2.zarp) return -1;
                return 0;
            }
        }
        class Baza_Kormit
        {

            int cout = 0;

            public XmlSerializer ser = new XmlSerializer(typeof(List<one>));
            public List<one> mntu = new List<one>();
            public void rozr(Label l)
            {
                int sum1 = 0;
                int sum2 = 0;
                var fixedu = new List<fixedu>();
            var pogod=new List<pogod>(); 
                foreach(var eat in mntu)
                {
                    if (eat.osna == "Фіксована")
                    {
                        sum1 += eat.zarp;
                    }
                    else if (eat.osna == "Погодинна") { 
                    
                        sum2 += eat.zarp*eat.godin;
                    }
                }
                l.Text = ((sum1 + sum2)/2).ToString();
            }
            
            public void add(string t1, string t2, string t3, string t4, string t5, int a2,int a3, DataGridView dg)
            {
                mntu.Add(new one(t1, t2,t3,t4,t5, a2,a3));
                dg.Rows.Add(mntu[mntu.Count - 1].surname, mntu[mntu.Count - 1].name, mntu[mntu.Count - 1].father, mntu[mntu.Count - 1].posada, mntu[mntu.Count - 1].osna, mntu[mntu.Count - 1].zarp.ToString(), mntu[mntu.Count - 1].godin.ToString());
                cout++;
            }

            public void show(DataGridView dg)
            {
                dg.Rows.Clear();
                for (int i = 0; i < mntu.Count; i++)
                {
                    dg.Rows.Add(mntu[i].surname, mntu[i].name,mntu[i].father, mntu[i].posada,mntu[i].osna, mntu[i].zarp.ToString(), mntu[i].godin.ToString());

                }
            }


            public void del(int i)
            {
                mntu.RemoveAt(i);
            }

            public void zap(string f)
            {
                FileStream file = new FileStream(f, FileMode.Create, FileAccess.Write, FileShare.None);
                ser.Serialize(file, mntu);
                file.Close();
            }
            public void ct(string f)
            {
                FileStream file;
                file = new FileStream(f, FileMode.Open, FileAccess.Read, FileShare.None);
                mntu = (List<one>)ser.Deserialize(file);
                file.Close();
            }
            public void sort()
            {
                SortByAmount sa = new SortByAmount();
                mntu.Sort(sa);
            }

            public void sort1(int z, DataGridView dg)
            {
                mntu.Sort();
                dg.Rows.Clear();
                for (int i = 0; i < mntu.Count; i++)
                {
                    if (z < mntu[i].zarp)
                        dg.Rows.Add(mntu[i].surname, mntu[i].name, mntu[i].father, mntu[i].posada, mntu[i].osna, mntu[i].zarp.ToString(), mntu[i].godin.ToString());

                }

            }

        }
        Baza_Kormit pl = new Baza_Kormit();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                int i = dataGridView1.CurrentRow.Index;

                pl.del(i);
                pl.show(dataGridView1);
            }
            catch
            {
                MessageBox.Show("Рядки закінчились.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string surname = textBox1.Text;
            string name = textBox2.Text;
            string father = textBox3.Text;
            string posada = textBox4.Text;
            string osna = textBox7.Text;
            int zarp = Convert.ToInt32(textBox5.Text);
            int godin = Convert.ToInt32(textBox6.Text);

            pl.add(surname, name,father,posada,osna, zarp,godin, dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pl.sort();
            pl.show(dataGridView2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Отримати вибраний рядок 
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // Встановити ReadOnly властивість рядка на false 
            selectedRow.ReadOnly = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string path = @"C:\1111.txt"; //це адреса  файла блокноту, де будуть записані результати сортування 
            pl.zap(path);
            MessageBox.Show("Супер");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string path = @"C:\1111.txt";
            pl.ct(path);
            pl.show(dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pl.rozr(label8);
        }
    }
}
