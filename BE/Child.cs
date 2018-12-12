using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BE
{
    public class Child
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                    throw new Exception("Id can't be negative");
                if (value > 999999999)
                    throw new Exception("Id can't be more than 9 digits");
                if (value < 1000)
                    throw new Exception("Id is too short");
                else
                    id = value;
            }
        }
        public int MotherId { get; set; }
        public string FirstName { get; set; }
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value < DateTime.Now)
                    birthDate = value;
                else
                    throw new Exception("birth date is not possible");
            }
        }
        public bool IsSpecialNeedsChild { get; set; }
        public string SpecialNeeds { get; set; }
        public bool IsFoodAllergy { get; set; }
        public string FoodAllergy { get; set; }
        public bool IsMedicinesAllergy { get; set; }
        public string MedicinesAllergy { get; set; }
        public bool IsBreastMilk { get; set; }
        public string Comments { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }

        public Child GetCopy()
        {
            return this.GetCopy<Child>();
        }
    }
}
