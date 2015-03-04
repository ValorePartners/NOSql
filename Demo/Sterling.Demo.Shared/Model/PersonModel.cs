using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sterling.Demo.Shared
{
    public enum Gender{
        Male,
        Female
    }

    
	public class PersonModel
    {
        int id; 
        string firstName;
        string lastName;
        DateTime dOB;
        Gender sex;

		public int Id{ get; set; }

		public PersonModel(){
		}
     

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {

				firstName = value;
            }
        }
	
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {

				lastName = value;
            }
        }

        public DateTime DOB
        {
            get
            {
                return dOB;
            }
            set
            {

				dOB = value;
            }
        }

        public Gender Sex
        {
            get
            {
                return sex;
            }
            set
            {

				sex = value;
            }
        }


    }

}

