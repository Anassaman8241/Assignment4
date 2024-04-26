using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    public class Client
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private int _weight = 0;
        private int _height = 0;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("First name is required.");
                }

                _firstName = value;
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Last name is required.");
                }

                _lastName = value;
            }
        }

        public int Weight
        {
            get { return _weight; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentNullException("Weight should be greater than 0.");
                }

                _weight = value;
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentNullException("Height should be greater than 0.");
                }

                _height = value;
            }
        }

        public double BMIScore
        {
            get
            {
                return (double)Weight / (Height * Height) * 703; ;
            }
        }

        public string BMIStatus
        {
            get
            {
                switch (BMIScore)
                {
                    case <= 18.4:
                        return "Underweight";
                    case <= 24.9:
                        return "Normal";
                    case <= 39.9:
                        return "Overweight";
                    default:
                        return "Obese";
                }
            }
        }

        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        public Client(string firstName, string lastName, int weight, int height)
        {
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Weight = weight;
            Height = height;
        }
    }
}
