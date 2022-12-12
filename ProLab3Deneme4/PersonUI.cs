
namespace ProLab3Deneme4
{
    public partial class PersonUI : Panel
    {
        private Label namesurname;
        private Label birthdate;
        private Label bloodgroup;
        private Label job;
        public PersonUI(Person person)
        {
            InitializeComponent();

            if (person.Gender == "Erkek")
                BackColor = Color.LightBlue;
            else
                BackColor = Color.Pink;

            Size = new Size(170, 75);

            namesurname = new Label() { Font = new Font("Calibri", 12, FontStyle.Bold), Text = person.Name + " " + person.Surname, TextAlign = ContentAlignment.MiddleCenter, Width = 150, Height = 20, };
            namesurname.Location = new Point(Width / 2 - namesurname.Width / 2, 10);
            Controls.Add(namesurname);

            birthdate = new Label()
            {
                Font = new Font("Calibri", 9, FontStyle.Italic),
                Text = String.Format("{0:D2}/{1:D2}/{2:D2}", person.BirthDate[0], person.BirthDate[1], person.BirthDate[2]),
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 75,
                Height = 15
            };
            birthdate.Location = new Point(Width / 2 - birthdate.Width / 2, 30);
            Controls.Add(birthdate);

            bloodgroup = new Label() { Font = new Font("Calibri", 10, FontStyle.Italic), Text = person.BloodGroup, TextAlign = ContentAlignment.MiddleRight, Width = 50, Height = 15 };
            bloodgroup.Location = new Point(Width - bloodgroup.Width - 5, Height - bloodgroup.Height - 5);
            Controls.Add(bloodgroup);

            job = new Label() { Font = new Font("Calibri", 10, FontStyle.Italic), Text = person.Job, TextAlign = ContentAlignment.MiddleLeft, Width = 100, Height = 15 };
            job.Location = new Point(10, Height - job.Height - 5);
            Controls.Add(job);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
