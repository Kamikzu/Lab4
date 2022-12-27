using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExceptionUser;
using InitialParameters;

namespace UsersClass
{

    public class User
    {
        private string _firstname;
        private string _lastname;
        private string _email = null;
        private string _login;
        private DateTime? _datebirth = null;
        private DateTime _dateregistration;     
        public User(string login, string firstName, string lastName)
        {
            Fill(login, firstName, lastName, null, null);
        }

        public User(string login, string firstName, string lastname, string? email, DateTime? birthDate)
        {
            Fill(login, firstName, lastname, email, birthDate);
        }

        public User()
        {
        }

        private void Fill(string login, string firstName, string lastname, string? email, DateTime? birthDate)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastname;
            Email = email;
            BirthDate = birthDate;
            DateRegistration = DateTime.Now;
        }
        public void FillFromString(string str)
        {
            string[] splited = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (splited.Length != 5)
            {
                throw new UserException("Строка может содержать только 5 элементов,"
                + "возможно не правильный формат (Логин, Имя, Фамилия, Почта, Дата рождения");
            }

            Login = splited[0].Trim();
            FirstName = splited[1].Trim();
            LastName = splited[2].Trim();

            if ((splited[3] == " "))
            {
                Email = null;
            }
            else
            {
                Email = splited[3].Trim();
            }

            if (splited[4] == " ")
            {
                BirthDate = null;
            }
            else
            {
                if (DateTime.TryParse(splited[4].Trim(), out DateTime date))
                {
                    BirthDate = date;
                }
                else
                {
                    throw new UserException("Неправильный формат даты рождения!");
                }
            }
        }

        public override string ToString()
        {
            if (BirthDate is not null)
            {
                return $"{Login}, {FirstName}, {LastName}, {Email}, {BirthDate.Value.ToString("dd-MM-yyyy")}";
            }
            else
            {
                return $"{Login}, {FirstName}, {LastName}, {Email}, {BirthDate}";
            }
        }
        public string FirstName
        {
            set
            {
                if ((value.Length <= UsersInitialParameters.FirstNameLength) && Regex.IsMatch(value, UsersInitialParameters.FirstNamePattern))
                {
                    _firstname = value;
                }
                else
                {
                    throw new UserException(UsersInitialParameters.ErrorStart + UsersInitialParameters.FirstNameFormat);
                }
            }
            get
            {
                return _firstname;
            }
        }

        public string LastName
        {
            set
            {
                if ((value.Length <= UsersInitialParameters.LastNameLength) && Regex.IsMatch(value, UsersInitialParameters.LastNamePattern))
                {
                    _lastname = value;
                }
                else
                {
                    throw new UserException(UsersInitialParameters.ErrorStart + UsersInitialParameters.LastNameFormat);
                }
            }
            get
            {
                return _lastname;
            }
        }
        public string? Email
        {
            set
            {
                if (value is null)
                {
                    _email = null;
                    return;
                }

                if (Regex.IsMatch(value, UsersInitialParameters.EmailPattern))
                {
                    _email = value;
                }
                else
                {
                    throw new UserException(UsersInitialParameters.ErrorStart + UsersInitialParameters.EmailFormat);
                }
            }
            get
            {
                return _email;
            }
        }

        public string Login
        {
            set
            {
                if ((value.Length <= UsersInitialParameters.LoginLength) && Regex.IsMatch(value, UsersInitialParameters.LoginPattern))
                {
                    _login = value;
                }
                else
                {
                    throw new UserException(UsersInitialParameters.ErrorStart + UsersInitialParameters.LoginFormat);
                }
            }
            get
            {
                return _login;
            }
        }
        public DateTime? BirthDate
        {
            get {return _datebirth;}
            set
            {
                if (value is null)
                {
                    _datebirth = null;
                    return;
                }
                if ((value > DateTime.Now))
                {
                    throw new UserException(UsersInitialParameters.ErrorStart + UsersInitialParameters.BirthFormat);
                }
                else
                {
                    _datebirth = value;
                }
            }            
        }       
        public DateTime DateRegistration
        {
            set
            {
                _dateregistration = value;
            }
            get
            {
                return _dateregistration;
            }
        }
        public static DateTime StringToDate(string date)
        {
            if (DateTime.TryParse(date, out DateTime result))
                return result;
            else
            {
                throw new UserException("Ошибка в написании даты");
            }
        }
    }
}




