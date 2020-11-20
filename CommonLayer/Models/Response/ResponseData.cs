using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models.Response
{
    public class ResponseData
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
