namespace AnnouncementAPI.Models.DTO
{
    public class GetAnnouncement
    {

        public int Announcement_ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Descriptions { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
