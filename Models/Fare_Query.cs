using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Railway_OnlineTicket.Models
{
    public class Fare_Query
    {
        [Key]
        public int ticketid { get; set; }
        public string journey_date { get; set; }
        public string journey_time { get; set; }
        public string station_from { get; set; }
        public string station_to { get; set; }
        public string train_name { get; set; }
        public int train_no { get; set; }
        public string departure_time { get; set; }
        public string Class { get; set; }
        public int unique_price { get; set; }
        public int Available_seat { get; set; }
        public string sell_seats { get; set; }
        public string total_sell { get; set; }
    }
}