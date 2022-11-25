namespace AutoCountMiddleWare
{
    public static class GlobalUtils
    {
        // global session
        public static AutoCount.Authentication.UserSession userSession;

        public static string NullToStr(object inObject)
        {
            try
            {
                if (inObject == null) return "";
                if (Convert.IsDBNull(inObject) == true) return "";
                return Convert.ToString(inObject);
            }
            catch { return ""; }
        }

        public static int NullToInt32(object inObject)
        {
            try
            {
                if (inObject == null) return 0;
                if (Convert.IsDBNull(inObject) == true) return 0;
                if (inObject.ToString() == "") return 0;
                return Convert.ToInt32(inObject);
            }
            catch { return 0; }
        }

        public static DateTime NullToDate(object inObject)
        {
            try
            {
                if (inObject == null) return DateTime.MinValue;
                if (Convert.IsDBNull(inObject) == true) return DateTime.MinValue;
                return Convert.ToDateTime(inObject);
            }
            catch { return DateTime.MinValue; }
        }

        public static DateTime NullToDate(object inObject, string DateFormat)
        {
            try
            {
                if (inObject == null) return DateTime.MinValue;
                if (Convert.IsDBNull(inObject) == true) return DateTime.MinValue;
                if (Convert.ToString(inObject) == "") return DateTime.MinValue;

                int Day = 0;
                int Month = 0;
                int Year = 0;
                string inDate = NullToStr(inObject);

                Day = NullToInt32(inDate.Substring(DateFormat.IndexOf("dd", StringComparison.OrdinalIgnoreCase), 2));
                Month = NullToInt32(inDate.Substring(DateFormat.IndexOf("MM", StringComparison.OrdinalIgnoreCase), 2));
                Year = NullToInt32(inDate.Substring(DateFormat.IndexOf("yyyy", StringComparison.OrdinalIgnoreCase), 4));
                if ((Day == 0) || (Month == 0) || (Year == 0)) return DateTime.MinValue;

                return new DateTime(Year, Month, Day);
            }
            catch { return DateTime.MinValue; }
        }

        public static DateTime NullToDateTime(object inObject, string DateTimeFormat)
        {
            try
            {
                if (inObject == null) return DateTime.MinValue;
                if (Convert.IsDBNull(inObject) == true) return DateTime.MinValue;
                if (Convert.ToString(inObject) == "") return DateTime.MinValue;

                int Day = 0;
                int Month = 0;
                int Year = 0;
                int Hour = 0;
                int Minute = 0;
                int Second = 0;
                string inDate = NullToStr(inObject);

                Day = NullToInt32(inDate.Substring(DateTimeFormat.IndexOf("dd"), 2));
                Month = NullToInt32(inDate.Substring(DateTimeFormat.IndexOf("MM"), 2));
                Year = NullToInt32(inDate.Substring(DateTimeFormat.IndexOf("yyyy"), 4));
                Hour = NullToInt32(inDate.Substring(DateTimeFormat.IndexOf("hh"), 2));
                Minute = NullToInt32(inDate.Substring(DateTimeFormat.IndexOf("mm"), 2));
                Second = NullToInt32(inDate.Substring(DateTimeFormat.IndexOf("ss"), 2));
                if (((Day == 0) || (Month == 0) || (Year == 0)) && (Hour == 0) && (Minute == 0) & (Second == 0)) return DateTime.MinValue;

                return new DateTime(Year, Month, Day, Hour, Minute, Second);
            }
            catch { return DateTime.MinValue; }
        }

        public static DateTime NullToDate(object inObject, string DateFormat, string TimeFormat)
        {
            try
            {
                if (inObject == null) return DateTime.MinValue;
                if (Convert.IsDBNull(inObject) == true) return DateTime.MinValue;
                if (Convert.ToString(inObject) == "") return DateTime.MinValue;

                int Day = 0;
                int Month = 0;
                int Year = 0;
                int Hour = 0;
                int Minute = 0;
                int Second = 0;
                string inDate = NullToStr(inObject);

                Day = NullToInt32(inDate.Substring(DateFormat.IndexOf("dd", StringComparison.OrdinalIgnoreCase), 2));
                Month = NullToInt32(inDate.Substring(DateFormat.IndexOf("MM", StringComparison.OrdinalIgnoreCase), 2));
                Year = NullToInt32(inDate.Substring(DateFormat.IndexOf("yyyy", StringComparison.OrdinalIgnoreCase), 4));
                Hour = NullToInt32(inDate.Substring(DateFormat.Length + 1 + TimeFormat.IndexOf("hh", StringComparison.OrdinalIgnoreCase), 2));
                Minute = NullToInt32(inDate.Substring(DateFormat.Length + 1 + TimeFormat.IndexOf("mm", StringComparison.OrdinalIgnoreCase), 2));
                Second = NullToInt32(inDate.Substring(DateFormat.Length + 1 + TimeFormat.IndexOf("ss", StringComparison.OrdinalIgnoreCase), 2));
                if (((Day == 0) || (Month == 0) || (Year == 0)) && (Hour == 0) && (Minute == 0) & (Second == 0)) return DateTime.MinValue;

                return new DateTime(Year, Month, Day, Hour, Minute, Second);
            }
            catch { return DateTime.MinValue; }
        }

        public static UInt64 NullToUInt64(object inObject)
        {
            try
            {
                if (inObject == null) return 0;
                if (Convert.IsDBNull(inObject) == true) return 0;
                if (inObject.ToString() == "") return 0;
                return Convert.ToUInt64(inObject);
            }
            catch { return 0; }
        }

        public static Int64 NullToInt64(object inObject)
        {
            try
            {
                if (inObject == null) return 0;
                if (Convert.IsDBNull(inObject) == true) return 0;
                if (inObject.ToString() == "") return 0;
                return Convert.ToInt64(inObject);
            }
            catch { return 0; }
        }

        public static double NullToDouble(object inObject)
        {
            try
            {
                if (inObject == null) return 0;
                if (Convert.IsDBNull(inObject) == true) return 0;
                if (inObject.ToString() == "") return 0;
                return Convert.ToDouble(inObject);
            }
            catch { return 0; }
        }

        public static decimal NullToDecimal(object inObject)
        {
            try
            {
                if (inObject == null) return 0;
                if (Convert.IsDBNull(inObject) == true) return 0;
                if (inObject.ToString() == "") return 0;
                return Convert.ToDecimal(inObject);
            }
            catch { return 0; }
        }

        public static int NullToCurrencyCent(object inObject)
        {
            try
            {
                if (inObject == null) return 0;
                if (Convert.IsDBNull(inObject) == true) return 0;
                if (inObject.ToString() == "") return 0;

                // first, strip off everything after third decimal
                decimal dVal2 = 0;
                decimal InputValue = Convert.ToDecimal(inObject) * 1000;
                dVal2 = Fix(InputValue);

                // now, round the result
                dVal2 /= 10;
                InputValue = Fix(dVal2 + (dVal2 > 0 ? (decimal)0.5 : (decimal)-0.5));

                return (int)InputValue;
            }
            catch { };

            return 0;
        }

        public static decimal Fix(decimal InputValue)
        {
            if (Math.Sign(InputValue) < 0)
            {
                // less than 0, negative values
                return Math.Ceiling(InputValue);
            }
            return Math.Floor(InputValue);
        }

        public static string NullToDateTimeStr(object inObject)
        {
            DateTime dateTime;

            try
            {
                if (inObject == null) return "";
                if (Convert.IsDBNull(inObject) == true) return "";
                dateTime = (DateTime)inObject;
                return dateTime.ToString("dd.MM.yyyy HH:mm:ss");
            }
            catch { };

            return "";
        }

        public static string CentToCurrency(object inObject)
        {
            try
            {
                if (inObject == null) return "0";
                if (Convert.IsDBNull(inObject) == true) return "0";
                if (inObject.ToString() == "") return "0";

                // first, strip off everything after third decimal
                decimal InputValue = Convert.ToDecimal(inObject) / 100;

                // we are done.
                return InputValue.ToString("0.00");
            }
            catch { };

            return "0";
        }

        public static bool IsNumeric(object inputObject)
        {
            string inputString = "";

            if (inputObject == null) return false;
            if (Convert.IsDBNull(inputObject) == true) return false;
            if (inputObject.ToString().Length == 0) return false;

            inputString = inputObject.ToString();

            for (int i = 0; i < inputString.Length; i++)
            {
                if (!char.IsNumber(inputString[i])) return false;
            }
            return true;
        }

        public static bool IsDate(object Expression)
        {
            DateTime retDate;

            return DateTime.TryParse(NullToStr(Expression), System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None, out retDate);
        }

        public static bool IsInteger(object Expression)
        {
            int retInt;

            return int.TryParse(NullToStr(Expression), System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.CurrentInfo, out retInt);
        }

        public static bool IsTime(object inputObject)
        {
            string inputString = "";

            if (inputObject == null) return false;
            if (Convert.IsDBNull(inputObject) == true) return false;
            if (inputObject.ToString().Length != 8) return false;

            inputString = inputObject.ToString();

            //Stunden
            for (int i = 0; i < 2; i++)
            {
                if (!char.IsNumber(inputString[i])) return false;
            }
            if (inputString[2].ToString() != (":")) return false;
            //Minuten
            for (int i = 3; i < 5; i++)
            {
                if (!char.IsNumber(inputString[i])) return false;
            }
            if (inputString[5].ToString() != ":") return false;
            //Sekunden
            for (int i = 6; i < 8; i++)
            {
                if (!char.IsNumber(inputString[i])) return false;
            }
            return true;
        }

    }
}
