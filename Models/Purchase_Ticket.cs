using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Railway_OnlineTicket.Models
{
    public class Purchase_Ticket
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Journey_date { get; set; }
        public string Station_from { get; set; }      
        public string Station_To { get; set; }
        public string train_name { get; set; }
        public string Class { get; set; }
        public int seats { get; set; }
        public int price { get; set; }
        public DateTime date { get; set; }
    }
}