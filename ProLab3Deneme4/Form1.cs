using ExcelDataReader;
using ProLab3Deneme4;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProLab3Deneme4
{
    public partial class Form1 : Form
    {
        public class Node
        {
            public Person person;
            public List<Node> child = new List<Node>();
        };

        static Node newNode(Person person)
        {
            Node temp = new Node();
            temp.person = person;
            return temp;
        }

        static void LevelOrderTraversal(Node root)
        {
            if (root == null)
                return;


            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                int n = q.Count;

                while (n > 0)
                {

                    Node p = q.Peek();
                    q.Dequeue();
                    Console.Write(p.person.Name + " ");


                    for (int i = 0; i < p.child.Count; i++)
                        q.Enqueue(p.child[i]);
                    n--;
                }

                Console.WriteLine("----");
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabPage1.AutoScroll = true;
            tabPage2.AutoScroll = true;
            tabPage3.AutoScroll = true;
            tabPage4.AutoScroll = true;
            List<Person> personList1 = new List<Person>();

            int count2  = 0;
            int i = 0;
            int l = 0;
            int count = 0;
            FileStream stream = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Kopya Deneme.xlsx", FileMode.Open, FileAccess.Read);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
            DataSet result = excelReader.AsDataSet();
            int counter = 0;
            int count1 = 0;
            Node[] roots = new Node[4];
            do
            {
                count = 0;
                count1 = 0;
                List<Person> personList = new List<Person>();
                l = 0;
                int size = 100;
                i++;
                while (excelReader.Read())
                {
                    counter++;
                    if (counter > 1)
                    {
                        int[] birthdate = new int[3];
                        if (excelReader.GetFieldType(3) == typeof(string))
                        {
                            string birth = excelReader.GetString(3).ToString().Split(" ")[0];
                            string[] birthd = new string[3];
                            birthd = birth.Split(".");
                            for (int k = 0; k < 3; k++)
                                birthdate[k] = int.Parse(birthd[k]);
                        }
                        else
                        {
                            DateTime date = new DateTime();
                            date = excelReader.GetDateTime(3);
                            string bdate = date.ToString("dd/MM/yyyy");
                            string[] birthd = new string[3];
                            birthd = bdate.Split("/");
                            for (int k = 0; k < 3; k++)
                                birthdate[k] = int.Parse(birthd[k]);
                        }
                        string s = excelReader.GetString(4);
                        if (s == null)
                            s = "";
                        personList1.Add(new Person(excelReader.GetDouble(0), excelReader.GetString(1), excelReader.GetString(2), birthdate, s, excelReader.GetString(5)
                            , excelReader.GetString(6), excelReader.GetString(7), excelReader.GetString(8), excelReader.GetString(9), excelReader.GetString(10), excelReader.GetString(11)));
                        personList.Add(new Person(excelReader.GetDouble(0), excelReader.GetString(1), excelReader.GetString(2), birthdate, s, excelReader.GetString(5)
                            , excelReader.GetString(6), excelReader.GetString(7), excelReader.GetString(8), excelReader.GetString(9), excelReader.GetString(10), excelReader.GetString(11)));
                        if (counter == 2)
                        {
                            roots[i - 1] = newNode(personList[0]);
                        }
                        else
                        {
                            if (personList[personList.Count - 1].FatherName == personList[0].Name)
                            {
                                roots[i - 1].child.Add(newNode(personList[personList.Count - 1]));
                                count++;
                            }
                            else
                            {
                                for (int j = 0; j < count; j++)
                                {
                                    if ((roots[i - 1].child[j].person.Name == personList[personList.Count - 1].FatherName) || roots[i - 1].child[j].person.Name == personList[personList.Count - 1].MotherName)
                                    {
                                        roots[i - 1].child[j].child.Add(newNode(personList[personList.Count - 1]));
                                        count1++;
                                    }
                                }
                                if (count1 > 0)
                                {
                                    for (int h = 0; h < count; h++)
                                    {
                                        for (int p = 0; p < roots[i - 1].child[h].child.Count; p++)
                                        {
                                            if ((roots[i - 1].child[h].child[p].person.Name == personList[personList.Count - 1].FatherName) || roots[i - 1].child[h].child[p].person.Name == personList[personList.Count - 1].MotherName)
                                            {
                                                roots[i - 1].child[h].child[p].child.Add(newNode(personList[personList.Count - 1]));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (i == 1)
                        {
                            for (int a = count2; a < personList.Count; a++)
                            {
                                PersonUI pers = new PersonUI(personList[a]);
                                tabPage1.Controls.Add(pers);
                                pers.Location = new Point(size, 100);
                                size += 170;
                                if (personList[a].Partner != null && l > 1)
                                {
                                    PersonUI pers1 = new PersonUI(personList[a].Partner);
                                    tabPage1.Controls.Add(pers1);
                                    pers1.Location = new Point(size, 100);
                                    size += 170;
                                }
                            }
                            count2++;
                        }
                        else if (i == 2)
                        {
                            for (int a = count2; a < personList.Count; a++)
                            {
                                PersonUI pers = new PersonUI(personList[a]);
                                tabPage2.Controls.Add(pers);
                                pers.Location = new Point(size, 100);
                                size += 170;
                                if (personList[a].Partner != null && l > 1)
                                {
                                    PersonUI pers1 = new PersonUI(personList[a].Partner);
                                    tabPage2.Controls.Add(pers1);
                                    pers1.Location = new Point(size, 100);
                                    size += 170;
                                }
                            }
                            count2++;
                        }
                        else if (i == 3)
                        {
                            for (int a = count2; a < personList.Count; a++)
                            {
                                PersonUI pers = new PersonUI(personList[a]);
                                tabPage3.Controls.Add(pers);
                                pers.Location = new Point(size, 100);
                                size += 170;
                                if (personList[a].Partner != null && l > 1)
                                {
                                    PersonUI pers1 = new PersonUI(personList[a].Partner);
                                    tabPage3.Controls.Add(pers1);
                                    pers1.Location = new Point(size, 100);
                                    size += 170;
                                }
                            }
                            count2++;
                        }
                        else
                        {
                            for (int a = count2; a < personList.Count; a++)
                            {
                                PersonUI pers = new PersonUI(personList[a]);
                                tabPage4.Controls.Add(pers);
                                pers.Location = new Point(size, 100);
                                size += 170;
                                if (personList[a].Partner != null && l > 1)
                                {
                                    PersonUI pers1 = new PersonUI(personList[a].Partner);
                                    tabPage4.Controls.Add(pers1);
                                    pers1.Location = new Point(size, 100);
                                    size += 170;
                                }
                            }
                            count2++;
                        }
                        l++;
                    }
                }
                Console.WriteLine("Level order traversal " +
                                "Before Mirroring ");
                LevelOrderTraversal(roots[i - 1]);
                Console.WriteLine(". . .");
                counter = 0;
            } while (excelReader.NextResult());
            excelReader.Close();
            tabPage1.Text = roots[0].person.Surname + " Ailesi";
            tabPage2.Text = roots[1].person.Surname + " Ailesi";
            tabPage3.Text = roots[2].person.Surname + " Ailesi";
            tabPage4.Text = roots[3].person.Surname + " Ailesi";
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            Point point1 = new Point(10, 10);
            Point point2 = new Point(100, 100);

            // Grafik nesnesini oluşturun
            Graphics g = this.CreateGraphics();

            // Çizgi stilini ayarlayın
            Pen pen = new Pen(Color.Black, 3);
            pen.DashStyle = DashStyle.Custom;

            // Çizgi çizin
            g.DrawLine(pen, point1, point2);
        }
    }
}