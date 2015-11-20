using Microsoft.Office.Interop.Outlook;
using System;
using System.ComponentModel;

namespace PF.Data.Outlook
{
    /*Description
    -----------------
    Interface to Outlook.

    Date        Author     Desc
    -------------------------------------------------------------------------------------------------------------------------------------------------
    01/09/2013  Dave       Creation
    */

    public class Appointment
    {
        /// <summary>
        /// Create a new Appointment using the Outlook form.
        /// </summary>
        public static void Create_Appointment()
        {
            _Items oItems = Outlook.Folder_Name_Locationfield.Items;
            _AppointmentItem oCalender = (_AppointmentItem)oItems.Add("IPM.Appointment");
            oCalender.Display(true);
        }

        /// <summary>
        /// Gets or Sets the Body Text
        /// </summary>
        public static string Appointment_body
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets if the Appointment is an AllDay Event
        /// </summary>
        [DefaultValue(true)]
        public static bool Appointment_All_Day_Event
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets if the Categories
        /// </summary>
        public static string Appointment_Categories
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets if the Subject
        /// </summary>
        public static string Appointment_Subject
        {
            get;
            set;
        }

        public static string Appointment_Location
        {
            get;
            set;
        }

        [DefaultValue(0)]
        public static int Appointment_ReminderMinutesBeforeStart
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets if the StartTime
        /// </summary>
        ///
        private static DateTime Appointment_Start_Time_field = DateTime.Now;

        public static DateTime Appointment_Start_Time
        {
            get
            {
                return Appointment_Start_Time_field;
            }
            set
            {
                Appointment_Start_Time_field = value;
            }
        }

        /// <summary>
        /// Gets or Sets if the EndTime
        /// </summary>
        ///
        private static DateTime Appointment_End_Time_field = DateTime.Now;

        public static DateTime Appointment_End_Time
        {
            get
            {
                return Appointment_End_Time_field;
            }
            set
            {
                Appointment_End_Time_field = value;
            }
        }

        public static bool Appointment_Reminder_Set
        {
            get;
            set;
        }

        [DefaultValue(false)]
        public static bool Appointment_Display
        {
            get;
            set;
        }

        private static bool Appointment_Save_field = true;

        public static bool Appointment_Save
        {
            get
            {
                return Appointment_Save_field;
            }
            set
            {
                Appointment_Save_field = value;
            }
        }

        public static void Create_Appointment2()
        {
            _Items oItems = Outlook.Folder_Name_Locationfield.Items;
            _AppointmentItem oCalender2 = (_AppointmentItem)oItems.Add();

            oCalender2.Body = Appointment_body;

            oCalender2.AllDayEvent = Appointment_All_Day_Event;

            oCalender2.Start = Convert.ToDateTime(Appointment_Start_Time_field);
            oCalender2.End = Convert.ToDateTime(Appointment_End_Time_field);

            oCalender2.Categories = Appointment_Categories;
            oCalender2.Subject = Appointment_Subject;
            oCalender2.Location = Appointment_Location;
            oCalender2.ReminderMinutesBeforeStart = Appointment_ReminderMinutesBeforeStart;
            oCalender2.ReminderSet = Appointment_Reminder_Set;

            if (Appointment_Display == true)
            {
                oCalender2.Display();
            }
            if (Appointment_Save_field == true)
            {
                oCalender2.Save();
            }
        }
    }
}