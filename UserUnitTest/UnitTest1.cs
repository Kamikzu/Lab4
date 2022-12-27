using System.Runtime.InteropServices;
using UsersClass;
using ExceptionUser;
using InitialParameters;

namespace UserUnitTests
{
    [TestClass]
    public class UserTests
    {
        private User user;

        [TestInitialize]
        public void Initialize()
        {
            user = new("Login", "John", "Doe");
        }

        [DataTestMethod]
        [DataRow("Basket", "Michael", "Jordan", "jorda@mail.ru", null)]
        [DataRow("Losun", "Jimmy", "Goodwing", null, "22.12.2022 0:00:00")]
        [DataRow("Horse", "Salvatore", "Ganacchi", "clow@mail.ru", "22.12.2022 0:00:00")]
        public void Constructor_SetValidValues_Tests(string login, string firstName, string lastName, string? email, string? birthDate)
        {
            DateTime? birthDateTime = birthDate is not null ? DateTime.Parse(birthDate) : null;

            user = new(login, firstName, lastName, email, birthDateTime);

            Assert.AreEqual(login, user.Login);
            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(birthDateTime, user.BirthDate);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("a@mail.ru")]
        [DataRow("alex@a.ru")]
        [DataRow("alex@aa.ss.dd.ff.gg.hh.jj.kk.ru")]
        public void Email_SetValidValues_Tests(string? target)
        {
            user.Email = target;

            Assert.AreEqual(target, user.Email);
        }

        [DataTestMethod]
        [DataRow("_aa@mail.ru")]
        [DataRow("aa_@mail.ru")]
        [DataRow("aa@.ru")]
        [DataRow("aa@w..ru.")]
        public void Email_SetInvalidValues_ThrowsException_Tests(string target)
        {
            var expected = UsersInitialParameters.ErrorStart + UsersInitialParameters.EmailFormat;

            var ex = Assert.ThrowsException<UserException>(() => user.Email = target);
            Assert.AreEqual(expected, ex.Message);
        }

        [DataTestMethod]
        [DataRow("Константин")]
        [DataRow("Jacob")]
        [DataRow("Лу")]
        [DataRow("Li")]
        [DataRow("Swosrtouoavtsnofedfmemfuoaldsngvimvgbbqirnbvvdlnzl")] 
        public void FirstName_SetValidValues_Tests(string target)
        {
            user.FirstName = target;

            Assert.AreEqual(target, user.FirstName);
        }

        [DataTestMethod]
        [DataRow("Dиана")]
        [DataRow("Dиана5")]
        [DataRow("89")]
        [DataRow("диана")]
        [DataRow("diana")]
        [DataRow("diana-алексеева")]
        [DataRow("diana-alexeeva")]
        [DataRow("diana alexeeva")]
        [DataRow("диана алексеева")]
        [DataRow("UWahnovlTcyFgjeMJcnJcoEcVmMNLsZEsZdcGNNxWNafUSWnEVd")] 
        public void FirstName_SetInvalidValues_ThrowsException_Tests(string target)
        {
            var expected = UsersInitialParameters.ErrorStart + UsersInitialParameters.FirstNameFormat;

            var ex = Assert.ThrowsException<UserException>(() => user.FirstName = target);
            Assert.AreEqual(expected, ex.Message);
        }

        [DataTestMethod]
        [DataRow("Хорошеева")]
        [DataRow("Ёлкин")]
        [DataRow("Римский-Корсаков")]
        [DataRow("Rimsky-Korsakov")]
        [DataRow("Williams")]
        [DataRow("Peters")] 
        [DataRow("Wihajgkjioixsmtqaiqvxuslnaaoqljzcrzivaylpxycgajvowofdlikupeajiwafawvaepzzjbkahrojzjthbuusxjoflvrdkgvtqedgoriwqzjjrewcxqgkgpkmfufzhkicecfyrgxpuujosfhauqieuzyqcqnmvpstltqngoxuavehgnqdxrzygmnopavlvbevr")]
        public void LastName_SetValidValues_Tests(string target)
        {
            user.LastName = target;

            Assert.AreEqual(target, user.LastName);
        }

        [DataTestMethod]
        [DataRow("константин")]
        [DataRow("Даниил2")]
        [DataRow("89")]
        [DataRow("сергей")]
        [DataRow("liam")]
        [DataRow("liam-дмитриеевна")]
        [DataRow("liam-mason")]
        [DataRow("William Ethan")]
        [DataRow("констанин-боярский")] 
        [DataRow("vTqedgORiwqzjjreWCxqGkGPKMfufZHKICECfyrgxpuUJOSFHAuqIEuzYQcQNmVpsTLTqNGOXuAveHgnqDXRZYGMNOPAVLVBEvrWIhAjgkjiOIxSmtqaIqVXUSlnAAoQLJzCRZiVayLpxyCgAjvowofdlIkUpEaJIwafAWvAEpZZjBkAhRoJzjthBUUSXjoFLvRdKgR")]
        public void LastName_SetInvalidValues_ThrowsException_Tests(string target)
        {
            var expected = UsersInitialParameters.ErrorStart + UsersInitialParameters.LastNameFormat;

            var ex = Assert.ThrowsException<UserException>(() => user.LastName = target);
            Assert.AreEqual(expected, ex.Message);
        }

        [DataTestMethod]
        [DataRow("WellDone")]
        [DataRow("v")]
        [DataRow("Welldonemyfriendsall")] 
        public void Login_SetValidValues_Tests(string target)
        {
            user.Login = target;

            Assert.AreEqual(target, user.Login);
        }

        [DataTestMethod]
        [DataRow("Саня")]
        [DataRow("СаняKrut")]
        [DataRow("Kiplingmethodsobadfoe")] 
        public void Login_SetInvalidValues_ThrowsException_Tests(string target)
        {
            var expected = UsersInitialParameters.ErrorStart + UsersInitialParameters.LoginFormat;

            var ex = Assert.ThrowsException<UserException>(() => user.Login = target);
            Assert.AreEqual(expected, ex.Message);
        }

        [TestMethod]
        public void BirthDate_SetNull_Test()
        {
            user.BirthDate = null;

            Assert.AreEqual(null, user.BirthDate);
        }

        [TestMethod]
        public void BirthDate_SetValidDate_Test()
        {
            DateTime date = DateTime.Parse("10-10-2010");

            user.BirthDate = date;

            Assert.AreEqual(date, user.BirthDate);
        }

        [TestMethod]
        public void BirthDate_SetInvalidDate_ThrowsException_Test()
        {
            DateTime date = DateTime.MaxValue;

            var expected = UsersInitialParameters.ErrorStart + UsersInitialParameters.BirthFormat;

            var ex = Assert.ThrowsException<UserException>(() => user.BirthDate = date);
            Assert.AreEqual(expected, ex.Message);
        }

        [DataTestMethod]
        [DataRow("alena@mail.ru", null)]
        [DataRow(null, "22-12-2022")]
        [DataRow("alena@mail.ru", "22-12-2022")] 
        public void Method_ToString_Tests(string? email, string? birthDate)
        {
            DateTime? birthDateTime = birthDate is not null ? DateTime.Parse(birthDate) : null;
            string output = $"{user.Login}, {user.FirstName}, {user.LastName}, {email}, {birthDate}";

            user.Email = email;
            user.BirthDate = birthDateTime;

            Assert.AreEqual(output, user.ToString());
        }

        [DataTestMethod]
        [DataRow("alenas@mail.ru", null)]
        [DataRow(null, "22-12-2022")]
        [DataRow("alenas@mail.ru", "22-12-2022")] 
        public void Method_FillFromString_Tests(string? email, string? birthDate)
        {
            DateTime? birthDateTime = birthDate is not null ? DateTime.Parse(birthDate) : null;

            User temp = new("Temp", "Temp", "Temp");
            temp.FillFromString($"{user.Login}, {user.FirstName}, {user.LastName}, {email}, {birthDate}");

            user.Email = email;
            user.BirthDate = birthDateTime;

            Assert.AreEqual(user.ToString(), temp.ToString());
        }
    }
}


