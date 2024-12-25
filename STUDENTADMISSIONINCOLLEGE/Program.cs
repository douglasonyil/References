// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;

namespace CollegeAdmissionSystem
{
    //Enum for Gender
    public enum Gender 
    {
        Select,
        Male,
        Female,
        Transgender
    }
    //Enu for Admission status
    public enum AdmissionStatus
    {
        Booked,
        Cancelled
    }

    //class for student details
    public class StudentDetails
    {
        public string StudentID { get; private set;}  //Unique identifier for each student
        public string StudentName { get; private set;}  //Name of the student
        public string FatherName { get; private set;}  //Father's name
        public DateTime DOB { get; private set;}  //Date of birth
        public Gender Gender { get; private set;}  //Gender of the student
        public double Physics {get; private set;}  //Physics marks
        public double Chemistry {get; private set;} //Chemistry marks
        public double Maths {get; private set;} //Math marks

        //static counters to generate unique student IDs
        private static int studentCount = 3000;
        //Constructor to initialize student details
        public StudentDetails(string studentName, string fatherName, DateTime dOB, Gender gender, double physics, double chemistry, double maths)
        {
            StudentID = "SF" + (++studentCount);  //Generate StudentID
            StudentName = studentName;  //Assign name
            FatherName = fatherName;  //Assign father's name
            DOB = dOB;  //Assign date of birth
            Gender = gender;  //Assign gender
            Physics = physics;  //Assign physics marks
            Chemistry = chemistry;  //Assign chemistry value
            Maths = maths; //Assign maths value

        }

        //Method to check if the student is eligible based on average marks
        public bool CheckEligibility(double cutoff)
        {
            double average = (Physics + Chemistry + Maths) / 3; //Calculate average
            return (average >= cutoff);  //Return true if eligible
        }
    }

    //Class to hold Departments Details
    public class DepartmentDetails
    {
        public string DepartmentID { get; private set;} //Unique identifier for each department
        public string DepartmentName { get; private set;} //Name of the department
        public int NumberOfSeats { get; set;} //Number of available seats in the department

        //static counter to generate unique student IDs
        private static int departmentCount = 100;

        public DepartmentDetails(string departmentName, int numberOfSeats)
        {
            DepartmentID = "DID" + (++departmentCount); //Generate DepartmentID
            DepartmentName = departmentName; //Assign department name
            NumberOfSeats = numberOfSeats;  //Assign number of seats
        }
    }

    //Class to hold admission details
    public class AdmissionDetails
    {
        //Properties of the AdmissionDetails class
        public string AdmissionID { get; private set;} //Unique identifier for each admission
        public string StudentID { get; private set;}  //ID of the student
        public string DepartmentID{ get; private set;} //ID of the department
        public DateTime AdmissionDate { get; private set;} //DateOnly of admission
        public AdmissionStatus Status { get; set;}  //Status of admission (Booked or cancelled)


        //static counter to generate unique admission IDs
        private static int admissionCount = 1000;

        //Constructor to initialize admission details
        public AdmissionDetails(string studentID, string departmentID)
        {
            AdmissionID = "AID" + (++admissionCount);  //Generate AdmissionID
            StudentID = studentID;  //Assign student ID
            DepartmentID = departmentID;  //Assign department ID
            AdmissionDate = DateTime.Now;  //Set admission date to now
            Status = AdmissionStatus.Booked; //Set initial status to Booked
        } 
    }
    //Class to manage overall application logic
    public class CollegeAdmissionApp
    {
        //Lists to hold data for students, departments, and admissions
        private List<StudentDetails> students = new List<StudentDetails>{};
        private List<DepartmentDetails> departments = new List<DepartmentDetails>{};
        private List<AdmissionDetails> admissions = new List<AdmissionDetails>{};
        private StudentDetails currentStudent; // Currently logged in student

        //Constructor to initialize  the application and departments
        public CollegeAdmissionApp()
        {
            InitializeDepartments(); //Initialize default departments
        }


        //Method to create default department details
        private void InitializeDepartments()
        {
            departments.Add(new DepartmentDetails("EEE", 29)); // Add Electrical Engineering department
            departments.Add(new DepartmentDetails("CSE", 29)); // Add Computer ScienceEngineering department
            departments.Add(new DepartmentDetails("MECH", 30)); // Add Mechanical Engineering department
            departments.Add(new DepartmentDetails("ECE", 30)); //Add Electronics and Communication department

        }  

        //Method to start the application
        public void Start()
        {
            //  Main loop for the application menu
            while(true)
            {
                //Display menu options
                System.Console.WriteLine("1. Student Registration");
                System.Console.WriteLine("2. Student Login");
                System.Console.WriteLine("3. Department wise seat availability");
                System.Console.WriteLine("4. Exit");
                System.Console.WriteLine( "Select an option:" );
                var choice = Console.ReadLine(); //Get user choice

                //Switch case to handle user choices
                switch (choice)
                {
                    case "1":
                        StudentRegistration();  //Handle student registration
                        break;
                    case "2":
                        StudentLogin();   //Handle student login
                        break;
                    case "3":
                        ShowDepartmentAvailability(); //Show available seats
                        break;
                    case "4":
                        System.Console.WriteLine("Exiting the application."); // EXit message
                        return; //Exit the application
                    
                    default:
                    System.Console.WriteLine("Invalid choice. Please try again."); // Handle invalid choice
                }
                
            }
        }

        //Method for student registration
        private void StudentRegistration()
        {
            System.Console.WriteLine("Enter Student Name:");
            string studentName = Console.ReadLine();  //Get student name
            System.Console.WriteLine("Enter Father's Name:");
            string fatherName = Console.ReadLine();  //Get father's name
            System.Console.WriteLine("Enter Date Of Birth(MM/DD/YY)");
            DateTime dob = DateTime.Parse(Console.ReadLine()); //Get date of birth
            System.Console.WriteLine("Select Gender (1. Male 2. Female 3. Transgender):");
            Gender gender = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine());  //Get gender selection
            System.Console.WriteLine("Enter Physics Marks:");
            double physics = Convert.ToDouble(Console.ReadLine());  // Get physics marks
            System.Console.WriteLine("Enter Chemistry Marks");
            double chemistry = Convert.ToDouble(Console.ReadLine());  //Get chemistry marks
            System.Console.WriteLine("Enter Maths Marks:");
            double maths = Convert.ToDouble(Console.ReadLine());  //Get maths marks

            //Create a new student object and add to the list
            var student = new StudentDetails(studentName, fatherName, dob, gender, physics, chemistry, maths);
            students.Add(student);  //Add student to the list
            System.Console.WriteLine($"Student Registered Succesfully! StudentID is {student.StudentID}");   //Confirmation message
                        
        }

        //Method for student login
        private void StudentLogin()
        {
            System.Console.WriteLine("Enter Student ID: ");
            string studentID = Console.ReadLine();  // Get student ID
            //Find the student in the list
            currentStudent = students.Find(s => s.StudentID.Equals(studentID, StringComparison.OrdinalIgnoreCase));

            if(currentStudent != null)  // If student is found
            {
                StudentMenu();  //Show student menu
            }
            else
            {
                System.Console.WriteLine("Invalid student ID. Please try again.");  //Handle invalid ID
            }            
        }

        //Method for displaying student menu option
        private void StudentMenu()
        {
            while (true)  //Loop for student menu
            {
                //Display menu options
                System.Console.WriteLine("1. Check Eligibility");
                System.Console.WriteLine("2. Show Details");
                System.Console.WriteLine("3. Take Admission");
                System.Console.WriteLine("4. Cancel Admission");
                System.Console.WriteLine("5. Show Admission Deatils");
                System.Console.WriteLine("6. Exit");
                System.Console.WriteLine("Select an option:");

                //Switch case to handle student menu choices
                Switch(choice)
                {
                    case "1":
                        CheckEligibility(); // Check Eligibility
                        break;

                    case "2":
                        ShowStudentsDetails(); //Show student details
                        break;

                    case "3":
                        TakeAdmission();
                        break;

                    case "4":
                        CancelAdmission();
                        break;

                    case "5":
                        ShowAdmissionDetails(); //Show admission details
                        break;

                    case "6":
                        return;  // Exit the Student Menu
                    default:
                        System.Console.WriteLine("Invalid choice. Please try again.");  // Handle invalid choice
                        break;
                }    
            }
        }

        //Method to check student eligibility
        private void CheckEligibility()
        {
            if (currentStudent.CheckEligibility(75.0))  //Check if eligible
            {
                System.Console.WriteLine("Student is eligible");  //Eligible message
            }
            else
            {
                System.Console.WriteLine("Student is not eligible.");  //Not eligible message
            }
        }

        //Method to show current student's details
        private void ShowStudentDetails()
        {
            //Display student details
            System.Console.WriteLine($"Student ID: {currentStudent.StudentID}");
            System.Console.WriteLine($"Student Name: {currentStudent.StudentName}");
            System.Console.WriteLine($"Father's Name: {currentStudent.FatherName}");
            System.Console.WriteLine($"DOB: {currentStudent.DOB.ToShortDateString()}");
            System.Console.WriteLine($"Gender: {currentStudent.Gender}");
            System.Console.WriteLine($"Physics: {currentStudent.Physics}");
            System.Console.WriteLine($"Chemistry: {currentStudent.Chemistry}");
            System.Console.WriteLine($"Maths: {currentStudent.Maths}");
        }

        //Method to take admission inti a department
        private void TakeAdmission()
        {
            System.Console.WriteLine("Available Departments:");
            foreach (var dept in departments)
            {
                //List available departments with the number of available seats
                System.Console.WriteLine($"{dept.DepartmentID}: {dept.DepartmentName} (Seats Available: {dept.NumberOfSeats})");
            }

            //Ask the student to select a department
            System.Console.WriteLine("Select Department ID: ");
            string departmentID = Console.ReadLine();
            
            var department = departments.Find(d => d.DepartmentID.Equals(departmentID, StringComparison.OrdinalIgnoreCase));
            if (department != null)  //If the department exists
            {
                if (currentStudent.CheckEligibility(75.0))  //If the student is eligible
                {
                    if (department.NumberOfSeats > 0)  // If seats are available
                    {
                        //Check if student has already taken admission
                        if (admissions.Find(a => a.StudentID == currentStudent.StudentID && a.Status == AdmissionStatus.Booked) == null)
                        {
                            //Proceed with admission
                            admissions.Add(new AdmissionDetails(currentStudent.StudentID, department.DepartmentID)); //Add new admission 
                            department.NumberOfSeats--;  // Reduce the available seat count
                            System.Console.WriteLine($"Admission took successfully. Your admission ID: AID{AdmissionDetails.AdmissionCount}");
                        }
                        else
                        {
                            //If the student has already been admitted
                            System.Console.WriteLine("You have already taken admission.");
                        }
                    }
                    else
                    {
                        //If no seats are available in the selected department
                        System.Console.WriteLine("No seats available in this department.");
                    }
                }
                else
                {
                    //If the student t is not eligible
                    System.Console.WriteLine("You are not eligible for admission.");
                }
            } 
            else
            {
                // If the department ID is invalid
                System.Console.WriteLine("Invalid Department ID.");
            }
        }
        //Method to cancel the student's admission
    }
}
