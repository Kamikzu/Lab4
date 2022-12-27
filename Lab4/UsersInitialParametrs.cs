using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialParameters
{
    public class UsersInitialParameters
    {
        public const int FirstNameLength = 50;
        public const int LastNameLength = 200;
        public const int LoginLength = 20;

        public const string FirstNamePattern = @"^(?:[А-ЯЁ][а-яё]+|[A-Z][a-z]+)$";
        public const string LastNamePattern = @"^(?:[А-ЯЁ][а-яё]+(-[А-ЯЁ][а-яё]+)?|[A-Z][a-z]+(-[A-Z][a-z]+)?)$";
        public const string EmailPattern = @"^[a-z0-9](?:[a-z0-9\._\-]*[a-z0-9])?\@" +
                    @"(?:(?:[a-z0-9][a-z0-9_\-]*[a-z0-9]|[a-z0-9])\.)+(?:[a-z]{2,6})$";
        public const string LoginPattern = @"^[a-zA-Z]*$";

        public const string ErrorStart = "Длина не соответствует установленым требованиям:\n";
        public const string FirstNameFormat = "Недопустимое имя, проверьте правильность написания";
        public const string LastNameFormat = "Недопустимая фамилия, проверьте правильность написания";
        public const string EmailFormat = "Недопустимая почта, проверьте правильность написания";
        public const string LoginFormat = "Недопустим логин";
        public const string BirthFormat = "Неправильный формат даты рождения";
        public const string DateFormat = "Ошибка в написании даты";
    }
}
