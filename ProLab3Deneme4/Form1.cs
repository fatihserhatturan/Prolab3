using ExcelDataReader;
using Microsoft.VisualBasic;
using System.Data;
namespace ProLab3Deneme4
{
    public partial class Form1 : Form
    {
        public static List<Person> personList0 = new List<Person>();
        public static List<Person> personList1 = new List<Person>();
        public static List<Person> personList2 = new List<Person>();
        public static List<Person> personList3 = new List<Person>();
        public static List<Person> personListAll = new List<Person>();
        public static Node[] roots = new Node[4];
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


        public static List<Person> uvey = new List<Person>();

        static void bfs(Node root)
        {
            if (root == null)
                return;
            string fname = "";
            string mname = "";
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                int n = q.Count;
                int count = 0;
                while (n > 0)
                {
                    Node p = q.Peek();
                    q.Dequeue();
                    if (count == 0)
                    {
                        fname = p.person.FatherName;
                        mname = p.person.MotherName;
                        count++;
                    }
                    else
                    {
                        if ((p.person.FatherName == fname && p.person.MotherName != mname) || (p.person.FatherName != fname && p.person.MotherName == mname))
                        {
                            Console.WriteLine(p.person.Name);
                            uvey.Add(p.person);
                            uvey.Sort((x, y) => string.Compare(x.Name, y.Name));
                        }
                    }
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
            int i = 0;
            int count = 0;
            FileStream stream = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Kopya Deneme.xlsx", FileMode.Open, FileAccess.Read);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
            DataSet result = excelReader.AsDataSet();
            int counter = 0;
            int count1 = 0;
            do
            {
                count = 0;
                count1 = 0;
                List<Person> personList = new List<Person>();
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
                        if (i == 1)
                            personList0.Add(new Person(excelReader.GetDouble(0), excelReader.GetString(1), excelReader.GetString(2), birthdate, s, excelReader.GetString(5)
                            , excelReader.GetString(6), excelReader.GetString(7), excelReader.GetString(8), excelReader.GetString(9), excelReader.GetString(10), excelReader.GetString(11)));
                        else if (i == 2)
                            personList1.Add(new Person(excelReader.GetDouble(0), excelReader.GetString(1), excelReader.GetString(2), birthdate, s, excelReader.GetString(5)
                            , excelReader.GetString(6), excelReader.GetString(7), excelReader.GetString(8), excelReader.GetString(9), excelReader.GetString(10), excelReader.GetString(11)));
                        else if (i == 3)
                            personList2.Add(new Person(excelReader.GetDouble(0), excelReader.GetString(1), excelReader.GetString(2), birthdate, s, excelReader.GetString(5)
                            , excelReader.GetString(6), excelReader.GetString(7), excelReader.GetString(8), excelReader.GetString(9), excelReader.GetString(10), excelReader.GetString(11)));
                        else
                            personList3.Add(new Person(excelReader.GetDouble(0), excelReader.GetString(1), excelReader.GetString(2), birthdate, s, excelReader.GetString(5)
                            , excelReader.GetString(6), excelReader.GetString(7), excelReader.GetString(8), excelReader.GetString(9), excelReader.GetString(10), excelReader.GetString(11)));
                        personList.Add(new Person(excelReader.GetDouble(0), excelReader.GetString(1), excelReader.GetString(2), birthdate, s, excelReader.GetString(5)
                            , excelReader.GetString(6), excelReader.GetString(7), excelReader.GetString(8), excelReader.GetString(9), excelReader.GetString(10), excelReader.GetString(11)));

                        personListAll.Add(new Person(excelReader.GetDouble(0), excelReader.GetString(1), excelReader.GetString(2), birthdate, s, excelReader.GetString(5)
                            , excelReader.GetString(6), excelReader.GetString(7), excelReader.GetString(8), excelReader.GetString(9), excelReader.GetString(10), excelReader.GetString(11)));
                        if (counter == 2)
                        {
                            roots[i - 1] = newNode(personList[0]);
                        }
                        else
                        {
                            if (personList[personList.Count - 1].FatherName == personList[0].Name || personList[personList.Count - 1].MotherName == personList[1].Name)
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
                    }
                }
                int[][] loc = new int[count][];
                Console.WriteLine(". . .");
                counter = 0;
            } while (excelReader.NextResult());
            excelReader.Close();
            tabPage1.Text = roots[0].person.Surname + " Ailesi";
            tabPage2.Text = roots[1].person.Surname + " Ailesi";
            tabPage3.Text = roots[2].person.Surname + " Ailesi";
            tabPage4.Text = roots[3].person.Surname + " Ailesi";
            for (int r = 0; r < 4; r++)
            {
                for (int t = 0; t < roots[r].child.Count; t++)
                {
                    esiOlanlariBulveEslestir(roots[r].child[t]);
                }
            }
            for (int r = 0; r < 4; r++)
            {
                for (int t = 0; t < roots[r].child.Count; t++)
                {
                    for (int y = 0; y < roots[r].child[t].child.Count; y++)
                    {
                        esiOlanlariBulveEslestir(roots[r].child[t].child[y]);
                    }
                }
            }
            for (int r = 0; r < 4; r++)
            {
                showFamilyTree(roots[r], r);
            }
        }
        // Helper class to.Push node and it's index
        // into the stack
        class Pair
        {
            public Node node;
            public int childrenIndex;
            public Pair(Node _node, int _childrenIndex)
            {
                node = _node;
                childrenIndex = _childrenIndex;
            }
        }
        // We will keep the start index as 0,
        // because first we always
        // process the left most children
        int currentRootIndex = 0;
        Stack<Pair> stack = new Stack<Pair>();
        List<Person> dfsTraversal =  new List<Person>();
        // Function to perform iterative dfs traversal
        public List<Person> dfs(Node root)
        {
            dfsTraversal.Clear();
            while (root != null || stack.Count != 0)
            {
                if (root != null)
                {
                    // Push the root and it's index
                    // into the stack
                    stack.Push(new Pair(root, currentRootIndex));
                    currentRootIndex = 0;
                    // If root don't have any children's that
                    // means we are already at the left most
                    // node, so we will mark root as null
                    if (root.child.Count >= 1)
                    {
                        root = root.child[0];
                    }
                    else
                    {
                        dfsTraversal.Add(root.person);
                        for (int j = 0; j < dfsTraversal.Count - 1; j++)
                        {
                            for (int i = 0; i < dfsTraversal.Count - 1; i++)
                            {
                                if (dfsTraversal[i].BirthDate[2] < dfsTraversal[i + 1].BirthDate[2])
                                {
                                    var temp1 = dfsTraversal[i];
                                    dfsTraversal[i] = dfsTraversal[i + 1];
                                    dfsTraversal[i + 1] = temp1;
                                }
                            }
                        }
                        root = null;
                    }
                    continue;
                }
                // We will.Pop the top of the stack and
                //.Add it to our answer
                Pair temp = stack.Pop();
                // Repeatedly we will the.Pop all the
                // elements from the stack till.Popped
                // element is last children of top of
                // the stack
                while (stack.Count != 0 && temp.childrenIndex ==
                        stack.Peek().node.child.Count - 1)
                {
                    temp = stack.Pop();
                }
                // If stack is not empty, then simply assign
                // the root to the next children of top
                // of stack's node
                if (stack.Count != 0)
                {
                    root = stack.Peek().node.child[temp.childrenIndex + 1];
                    currentRootIndex = temp.childrenIndex + 1;
                }
            }
            return dfsTraversal;
        }

        static int depthOfTree(Node ptr)
        {
            // Base case
            if (ptr == null)
                return 0;
            // Check for all children and find
            // the maximum depth
            int maxdepth = 0;
            foreach (Node it in ptr.child)
                maxdepth = Math.Max(maxdepth, depthOfTree(it));
            return maxdepth + 1;
        }

        static void findPerson(Node root, string isim)
        {
            if (root == null)
                return;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            int count = 0;
            while (q.Count != 0)
            {
                int n = q.Count;
                while (n > 0)
                {
                    Node p = q.Peek();
                    q.Dequeue();
                    if (p.person.Partner != null)
                    {
                        if ((p.person.Name + " " + p.person.Surname) == isim)
                        {
                            Console.WriteLine(depthOfTree(p) - 1);
                            count++;
                            break;
                        }
                        else if (p.person.PartnerName == isim)
                        {
                            Console.WriteLine(depthOfTree(p) - 1);
                            count++;
                            break;
                        }
                    }
                    else if ((p.person.Name + " " + p.person.Surname) == isim)
                    {
                        Console.WriteLine(depthOfTree(p) - 1);
                        count++;
                        break;
                    }
                    if (count != 0)
                        break;
                    for (int i = 0; i < p.child.Count; i++)
                        q.Enqueue(p.child[i]);
                    n--;
                }
                if (count != 0)
                    break;
            }
        }

        static void esiOlanlariBulveEslestir(Node root)
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
                    if (p.person.Partner != null)
                    {
                        for (int i = 0; i < personListAll.Count; i++)
                        {
                            if (p.person.Partner.Name == (personListAll[i].Name + " " + personListAll[i].Surname))
                            {
                                p.person.Partner = personListAll[i];
                            }
                        }
                    }
                    for (int i = 0; i < p.child.Count; i++)
                        q.Enqueue(p.child[i]);
                    n--;
                }
            }
        }

        public void showFamilyTree(Node root, int j)
        {
            if (root == null)
                return;
            int count = 0;
            int size = 50;
            int len = 0;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                if (count == 0)
                    len = q.Count + 2 * 170 + q.Count * 100;
                else
                    len = 100;
                int n = q.Count;
                while (n > 0)
                {
                    Node p = q.Peek();
                    q.Dequeue();
                    if (j == 0)
                    {
                        PersonUI pers = new PersonUI(p.person);
                        tabPage1.Controls.Add(pers);
                        pers.Location = new Point(len, size);
                        if (p.person.Partner != null)
                        {
                            len += 170;
                            PersonUI pers1 = new PersonUI(p.person.Partner);
                            tabPage1.Controls.Add(pers1);
                            pers1.Location = new Point(len, size);
                        }
                        len += 270;
                    }
                    else if (j == 1)
                    {
                        PersonUI pers = new PersonUI(p.person);
                        tabPage2.Controls.Add(pers);
                        pers.Location = new Point(len, size);
                        if (p.person.Partner != null)
                        {
                            len += 170;
                            PersonUI pers1 = new PersonUI(p.person.Partner);
                            tabPage2.Controls.Add(pers1);
                            pers1.Location = new Point(len, size);
                        }
                        len += 270;
                    }
                    else if (j == 2)
                    {
                        PersonUI pers = new PersonUI(p.person);
                        tabPage3.Controls.Add(pers);
                        pers.Location = new Point(len, size);
                        if (p.person.Partner != null)
                        {
                            len += 170;
                            PersonUI pers1 = new PersonUI(p.person.Partner);
                            tabPage3.Controls.Add(pers1);
                            pers1.Location = new Point(len, size);
                        }
                        len += 270;
                    }
                    else
                    {
                        PersonUI pers = new PersonUI(p.person);
                        tabPage4.Controls.Add(pers);
                        pers.Location = new Point(len, size);
                        if (p.person.Partner != null)
                        {
                            len += 170;
                            PersonUI pers1 = new PersonUI(p.person.Partner);
                            tabPage4.Controls.Add(pers1);
                            pers1.Location = new Point(len, size);
                        }
                        len += 270;
                    }
                    for (int i = 0; i < p.child.Count; i++)
                        q.Enqueue(p.child[i]);
                    n--;
                    count++;
                }
                size += 200;
            }
        }
        public void printspecificFamilyTree(Node root)
        {
            if (root == null)
                return;
            int count = 0;
            int size = 50;
            int len = 0;
            TabPage tabPage = new TabPage();
            tabControl1.TabPages.Add(tabPage);
            tabPage.AutoScroll = true;
            tabPage.Text = root.person.Name + " " + root.person.Surname + " Aile Soy Ağacı";
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                if (count == 0)
                    len = q.Count + 2 * 170 + q.Count * 100;
                else
                    len = 100;
                int n = q.Count;
                while (n > 0)
                {
                    Node p = q.Peek();
                    q.Dequeue();
                    PersonUI pers = new PersonUI(p.person);
                    tabPage.Controls.Add(pers);
                    pers.Location = new Point(len, size);
                    if (p.person.Partner != null)
                    {
                        len += 170;
                        PersonUI pers1 = new PersonUI(p.person.Partner);
                        tabPage.Controls.Add(pers1);
                        pers1.Location = new Point(len, size);
                    }
                    len += 270;
                    for (int i = 0; i < p.child.Count; i++)
                        q.Enqueue(p.child[i]);
                    n--;
                    count++;
                }
                size += 200;
            }
        }
        public void specificFamilyTree(Node root, string isim)
        {
            if (root == null)
                return;
            int count = 0;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                int n = q.Count;
                while (n > 0)
                {
                    Node p = q.Peek();
                    q.Dequeue();
                    if (p.person.Partner != null)
                    {
                        if ((p.person.Name + " " + p.person.Surname) == isim)
                        {
                            printspecificFamilyTree(p);
                            count++;
                            break;
                        }
                        else if (p.person.PartnerName == isim)
                        {
                            printspecificFamilyTree(p);
                            count++;
                            break;
                        }
                    }
                    else if ((p.person.Name + " " + p.person.Surname) == isim)
                    {
                        printspecificFamilyTree(p);
                        count++;
                        break;
                    }
                    if (count != 0)
                        break;
                    for (int i = 0; i < p.child.Count; i++)
                        q.Enqueue(p.child[i]);
                    n--;
                }
                if (count != 0)
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            string kangrubu = Interaction.InputBox("Aranacak Kan Grubunu Giriniz: \nA(+)\nA(-)\nAB(+)\nAB(-)\nB(+)\nB(-)\n0(+)\n0(-)", "Kan Grubu Giriş", "", 0, 0);
            if (tabControl1.SelectedTab.Text == roots[0].person.Surname + " Ailesi")
            {
                for (int i = 0; i < personList0.Count; i++)
                {
                    if (personList0[i].BloodGroup == kangrubu)
                    {
                        richTextBox1.Text += personList0[i].Name + " " + personList0[i].Surname + "\n";
                        Console.WriteLine(personList0[i].Name + " " + personList0[i].Surname);
                    }
                }
            }
            else if (tabControl1.SelectedTab.Text == roots[1].person.Surname + " Ailesi")
            {
                for (int i = 0; i < personList1.Count; i++)
                {
                    if (personList1[i].BloodGroup == kangrubu)
                    {
                        richTextBox1.Text += personList1[i].Name + " " + personList1[i].Surname + "\n";
                        Console.WriteLine(personList1[i].Name + " " + personList1[i].Surname);
                    }
                }
            }
            else if (tabControl1.SelectedTab.Text == roots[2].person.Surname + " Ailesi")
            {
                for (int i = 0; i < personList2.Count; i++)
                {
                    if (personList2[i].BloodGroup == kangrubu)
                    {
                        richTextBox1.Text += personList2[i].Name + " " + personList2[i].Surname + "\n";
                        Console.WriteLine(personList2[i].Name + " " + personList2[i].Surname);
                    }
                }
            }
            else
            {
                for (int i = 0; i < personList3.Count; i++)
                {
                    if (personList3[i].BloodGroup == kangrubu)
                    {
                        richTextBox1.Text += personList3[i].Name + " " + personList3[i].Surname + "\n";
                        Console.WriteLine(personList3[i].Name + " " + personList3[i].Surname);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == roots[0].person.Surname + " Ailesi")
            {
                richTextBox1.Text = roots[0].person.Surname + " Ailesi " + depthOfTree(roots[0]) + " nesilden oluşmaktadır.";
                //Console.WriteLine(roots[0].person.Surname + " Ailesi " + depthOfTree(roots[0]) + " nesilden oluşmaktadır");
            }
            else if (tabControl1.SelectedTab.Text == roots[1].person.Surname + " Ailesi")
            {
                richTextBox1.Text = roots[1].person.Surname + " Ailesi " + depthOfTree(roots[1]) + " nesilden oluşmaktadır.";
                //Console.WriteLine(roots[1].person.Surname + " Ailesi " + depthOfTree(roots[1]) + " nesilden oluşmaktadır");
            }
            else if (tabControl1.SelectedTab.Text == roots[2].person.Surname + " Ailesi")
            {
                richTextBox1.Text = roots[2].person.Surname + " Ailesi " + depthOfTree(roots[2]) + " nesilden oluşmaktadır.";
                // Console.WriteLine(roots[2].person.Surname + " Ailesi " + depthOfTree(roots[2]) + " nesilden oluşmaktadır");
            }
            else
            {
                richTextBox1.Text = roots[3].person.Surname + " Ailesi " + depthOfTree(roots[3]) + " nesilden oluşmaktadır.";
                // Console.WriteLine(roots[3].person.Surname + " Ailesi " + depthOfTree(roots[3]) + " nesilden oluşmaktadır");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == roots[0].person.Surname + " Ailesi")
            {
                string iş = personList0[0].Job;
                string iş1 = personList0[1].Job;
                richTextBox1.Text = "Ata Meslekleri: \n" + personList0[0].Job + "\n" + personList0[1].Job;
                // Console.WriteLine("Ata Meslekleri: \n"+ personList0[0].Job +"\n"+ personList0[1].Job);
                for (int i = 2; i < personList0.Count; i++)
                {
                    if (personList0[i].Job == iş)
                    {
                        richTextBox1.Text += "\n" + personList0[i].Name + " " + personList0[i].Surname + " " + personList0[i].Job;
                        // Console.WriteLine(personList0[i].Name + " " + personList0[i].Surname + " " + personList0[i].Job);
                    }
                    else if (personList0[i].Job == iş1)
                    {
                        richTextBox1.Text += "\n" + personList0[i].Name + " " + personList0[i].Surname + " " + personList0[i].Job;
                        //Console.WriteLine(personList0[i].Name + " " + personList0[i].Surname + " " + personList0[i].Job);
                    }
                }
            }
            else if (tabControl1.SelectedTab.Text == roots[1].person.Surname + " Ailesi")
            {
                string iş = personList1[0].Job;
                string iş1 = personList1[1].Job;
                richTextBox1.Text = "Ata Meslekleri: \n" + personList1[0].Job + "\n" + personList1[1].Job;
                //Console.WriteLine("Ata Meslekleri: \n" + personList1[0].Job + "\n" + personList1[1].Job);
                for (int i = 2; i < personList1.Count; i++)
                {
                    if (personList1[i].Job == iş)
                    {
                        richTextBox1.Text += "\n" + personList1[i].Name + " " + personList1[i].Surname + " " + personList1[i].Job;
                        //Console.WriteLine(personList1[i].Name + " " + personList1[i].Surname + " " + personList1[i].Job);
                    }
                    else if (personList1[i].Job == iş1)
                    {
                        richTextBox1.Text += "\n" + personList1[i].Name + " " + personList1[i].Surname + " " + personList1[i].Job;
                        //Console.WriteLine(personList1[i].Name + " " + personList1[i].Surname + " " + personList1[i].Job);
                    }
                }
            }
            else if (tabControl1.SelectedTab.Text == roots[2].person.Surname + " Ailesi")
            {
                string iş = personList2[0].Job;
                string iş1 = personList2[1].Job;
                richTextBox1.Text = "Ata Meslekleri: \n" + personList2[0].Job + "\n" + personList2[1].Job;
                //Console.WriteLine("Ata Meslekleri: \n" + personList2[0].Job + "\n" + personList2[1].Job);
                for (int i = 2; i < personList2.Count; i++)
                {
                    if (personList2[i].Job == iş)
                    {
                        richTextBox1.Text += "\n" + personList2[i].Name + " " + personList2[i].Surname + " " + personList2[i].Job;
                        //Console.WriteLine(personList2[i].Name + " " + personList2[i].Surname + " " + personList2[i].Job);
                    }
                    else if (personList2[i].Job == iş1)
                    {
                        richTextBox1.Text += "\n" + personList2[i].Name + " " + personList2[i].Surname + " " + personList2[i].Job;
                        // Console.WriteLine(personList2[i].Name + " " + personList2[i].Surname + " " + personList2[i].Job);
                    }
                }
            }
            else
            {
                string iş = personList3[0].Job;
                string iş1 = personList3[1].Job;
                richTextBox1.Text = "Ata Meslekleri: \n" + personList3[0].Job + "\n" + personList3[1].Job;
                //Console.WriteLine("Ata Meslekleri: \n" + personList3[0].Job + "\n" + personList3[1].Job);
                for (int i = 2; i < personList3.Count; i++)
                {
                    if (personList3[i].Job == iş)
                    {
                        richTextBox1.Text += "\n" + personList3[i].Name + " " + personList3[i].Surname + " " + personList3[i].Job;
                        //Console.WriteLine(personList3[i].Name + " " + personList3[i].Surname + " " + personList3[i].Job);
                    }
                    else if (personList3[i].Job == iş1)
                    {
                        richTextBox1.Text += "\n" + personList3[i].Name + " " + personList3[i].Surname + " " + personList3[i].Job;
                        // Console.WriteLine(personList3[i].Name + " " + personList3[i].Surname + " " + personList3[i].Job);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string isim = Interaction.InputBox("Lütfen İsim Giriniz: ", "Kişiden Sonra Nesil Sayısı Bulma", "", 0, 0);
            if (tabControl1.SelectedTab.Text == roots[0].person.Surname + " Ailesi")
            {
                findPerson(roots[0], isim);
            }
            else if (tabControl1.SelectedTab.Text == roots[1].person.Surname + " Ailesi")
            {
                findPerson(roots[1], isim);
            }
            else if (tabControl1.SelectedTab.Text == roots[2].person.Surname + " Ailesi")
            {
                findPerson(roots[2], isim);
            }
            else
            {
                findPerson(roots[3], isim);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Adaş Bul
            if (tabControl1.SelectedTab.Text == roots[0].person.Surname + " Ailesi")
            {
                List<string> nameList = new List<string>();
                nameList.Add(personList0[0].Name + " " + personList0[0].Surname + " -> " + personList0[0].Job);
                for (int i = 2; i < personList0.Count; i++)
                {
                    nameList.Add(personList0[i - 1].Name + " " + personList0[i - 1].Surname + " -> " + personList0[i - 1].Job);
                    for (int j = 0; j < nameList.Count; j++)
                    {
                        if (nameList[j].Contains(personList0[i].Name))
                        {
                            Console.WriteLine(nameList[j]);
                            Console.WriteLine(personList0[i].Name + " " + personList0[i].Surname + " -> " + personList0[i].Job);
                        }
                    }
                }
            }
            else if (tabControl1.SelectedTab.Text == roots[1].person.Surname + " Ailesi")
            {
                List<string> nameList = new List<string>();
                nameList.Add(personList1[0].Name + " " + personList1[0].Surname + " -> " + personList1[0].Job);
                for (int i = 2; i < personList1.Count; i++)
                {
                    nameList.Add(personList1[i - 1].Name + " " + personList1[i - 1].Surname + " -> " + personList1[i - 1].Job);
                    for (int j = 0; j < nameList.Count; j++)
                    {
                        if (nameList[j].Contains(personList1[i].Name))
                        {
                            Console.WriteLine(nameList[j]);
                            Console.WriteLine(personList1[i].Name + " " + personList1[i].Surname + " -> " + personList1[i].Job);
                        }
                    }
                }
            }
            else if (tabControl1.SelectedTab.Text == roots[2].person.Surname + " Ailesi")
            {
                List<string> nameList = new List<string>();
                nameList.Add(personList2[0].Name + " " + personList2[0].Surname + " -> " + personList2[0].Job);
                for (int i = 2; i < personList2.Count; i++)
                {
                    nameList.Add(personList2[i - 1].Name + " " + personList2[i - 1].Surname + " -> " + personList2[i - 1].Job);
                    for (int j = 0; j < nameList.Count; j++)
                    {
                        if (nameList[j].Contains(personList2[i].Name))
                        {
                            Console.WriteLine(nameList[j]);
                            Console.WriteLine(personList2[i].Name + " " + personList2[i].Surname + " -> " + personList2[i].Job);
                        }
                    }
                }
            }
            else
            {
                List<string> nameList = new List<string>();
                nameList.Add(personList3[0].Name + " " + personList3[0].Surname + " -> " + personList3[0].Job);
                for (int i = 2; i < personList3.Count; i++)
                {
                    nameList.Add(personList3[i - 1].Name + " " + personList3[i - 1].Surname + " -> " + personList3[i - 1].Job);
                    for (int j = 0; j < nameList.Count; j++)
                    {
                        if (nameList[j].Contains(personList3[i].Name))
                        {
                            Console.WriteLine(nameList[j]);
                            Console.WriteLine(personList3[i].Name + " " + personList3[i].Surname + " -> " + personList3[i].Job);
                        }
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            if (tabControl1.SelectedTab.Text == roots[0].person.Surname + " Ailesi")
            {
                List<Person> temp = dfs(roots[0]);
                foreach (Person v in temp)
                {
                    richTextBox1.Text += v.Name + " " + v.Surname + " " + (2022 - v.BirthDate[2]) + "\n";
                    Console.Write(v.Name + " " + v.Surname + " " + (2022 - v.BirthDate[2]) + "\n");
                }
            }
            else if (tabControl1.SelectedTab.Text == roots[1].person.Surname + " Ailesi")
            {
                List<Person> temp = dfs(roots[1]);
                foreach (Person v in temp)
                {
                    richTextBox1.Text += v.Name + " " + v.Surname + " " + (2022 - v.BirthDate[2]) + "\n";
                    Console.Write(v.Name + " " + v.Surname + " " + (2022 - v.BirthDate[2]) + "\n");
                }
            }
            else if (tabControl1.SelectedTab.Text == roots[2].person.Surname + " Ailesi")
            {
                List<Person> temp = dfs(roots[2]);
                foreach (Person v in temp)
                {
                    Console.Write(v.Name + " " + v.Surname + " " + (2022 - v.BirthDate[2]) + "\n");
                    richTextBox1.Text += v.Name + " " + v.Surname + " " + (2022 - v.BirthDate[2]) + "\n";
                }
            }
            else
            {
                List<Person> temp = dfs(roots[3]);
                foreach (Person v in temp)
                {
                    richTextBox1.Text += v.Name + " " + v.Surname + " " + (2022 - v.BirthDate[2]) + "\n";
                    Console.Write(v.Name + " " + v.Surname + " " + (2022 - v.BirthDate[2]) + "\n");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string isim = Interaction.InputBox("Lütfen İsim Giriniz: ", "Kişiye Özel Soy Ağacı Oluşturma", "", 0, 0);
            if (tabControl1.SelectedTab.Text == roots[0].person.Surname + " Ailesi")
            {
                specificFamilyTree(roots[0], isim);
            }
            else if (tabControl1.SelectedTab.Text == roots[1].person.Surname + " Ailesi")
            {
                specificFamilyTree(roots[1], isim);
            }
            else if (tabControl1.SelectedTab.Text == roots[2].person.Surname + " Ailesi")
            {
                specificFamilyTree(roots[2], isim);
            }
            else
            {
                specificFamilyTree(roots[3], isim);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            if (tabControl1.SelectedTab.Text == roots[0].person.Surname + " Ailesi")
            {
                uvey.Clear();
                bfs(roots[0]);
                for (int i = 0; i < uvey.Count; i++)
                {
                    richTextBox1.Text += uvey[i].Name + "\n";
                    //Console.WriteLine(uvey[i].Name);
                }
            }
            else if (tabControl1.SelectedTab.Text == roots[1].person.Surname + " Ailesi")
            {
                uvey.Clear();
                bfs(roots[1]);
                for (int i = 0; i < uvey.Count; i++)
                {
                    richTextBox1.Text += uvey[i].Name + "\n";
                    //Console.WriteLine(uvey[i].Name);
                }
            }
            else if (tabControl1.SelectedTab.Text == roots[2].person.Surname + " Ailesi")
            {
                uvey.Clear();
                bfs(roots[2]);
                for (int i = 0; i < uvey.Count; i++)
                {
                    richTextBox1.Text += uvey[i].Name + "\n";
                    //Console.WriteLine(uvey[i].Name);
                }
            }
            else
            {
                uvey.Clear();
                bfs(roots[3]);
                for (int i = 0; i < uvey.Count; i++)
                {
                    richTextBox1.Text += uvey[i].Name + "\n";
                    //Console.WriteLine(uvey[i].Name);
                }
            }
        }
    }
}