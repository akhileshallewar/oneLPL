using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementAPI.Common.Constants
{
    // Constants for maintaining the Stored Procedures

    public static class StoredProcedure
    {
        //store proc for Users
        public static string getUser = "OneLPL_GetUser"; //name of the stored procedure to fetch records
        public static string saveUser = "OneLPL_SaveUser"; //name of the stored procedure to save records

        //store proc for Announcement
        public static string getAnnouncement = "OneLPL_GetAnnouncement"; //name of the stored procedure to fetch records
        public static string saveAnnouncement = "OneLPL_SaveAnnouncement"; //name of the stored procedure to save records

    }
}

