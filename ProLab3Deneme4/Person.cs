
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLab3Deneme4
{
    public class Person
    {
        private double id;
        private string name;
        private string surname;
        private int[] birthDate;
        private string motherName;
        private string partnerName;
        private string fatherName;
        private string bloodGroup;
        private string job;
        private string gender;
        private Person partner;
        private string maritalStatus;
        private string maidenName;


        public Person(double id, string name, string surname, int[] birthDate, string partnername, string motherName, string fatherName, string bloodGroup, string job, string maritalStatus, string maidenName, string gender, string partnerName = null)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.birthDate = birthDate;
            this.partnerName = partnername;
            this.motherName = motherName;
            this.fatherName = fatherName;
            this.bloodGroup = bloodGroup;
            this.job = job;
            this.gender = gender;
            this.maritalStatus = maritalStatus;
            this.maidenName = maidenName;
            if (partnername != "")
            {
                if (gender == "Erkek")
                {
                    partner = new Person(-1, partnername, "", new int[3] { -1, -1, -1 }, "", "", "", "", "", "", "", "Kadın");
                    partner.Partner = this;
                }
                else
                {
                    partner = new Person(-1, partnername, "", new int[3] { -1, -1, -1 }, "", "", "", "", "", "", "", "Erkek");
                    partner.Partner = this;
                }
            }
        }

        public double Id { get => id; }

        public Person Partner { get => partner; set => partner = value; }

        public string Surname { get => surname; }
        public int[] BirthDate { get => birthDate; }
        public string MotherName { get => motherName; }
        public string FatherName { get => fatherName; }
        public string BloodGroup { get => bloodGroup; }
        public string Job { get => job; }
        public string Gender { get => gender; }
        public string MaritalStatus { get => maritalStatus; }
        public string MaidenName { get => maidenName; }
        public string Name { get => name; set => name = value; }
        public string PartnerName { get => partnerName; set => partnerName = value; }
    }
}
