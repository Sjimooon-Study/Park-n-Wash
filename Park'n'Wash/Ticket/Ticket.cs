﻿using Park_n_Wash.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park_n_Wash.Ticket
{
    public abstract class Ticket : BusinessEntity, ITicket
    {
        public int Id { get; }
        public DateTime StartTime { get; }
        public DateTime? EndTime { get; private set; }
        public double TotalHours { get; private set; }
        public ISlot ParkingSlot { get; }
        public bool Electric { get; }
        public double Price { get; protected set; }

        public Ticket(int ticketId, ISlot parkingSlot, bool includeCharging = false)
        {
            Id = ticketId;
            StartTime = DateTime.Now;
            TotalHours = 0;
            ParkingSlot = parkingSlot;
            Electric = includeCharging && ParkingSlot.HasCharger;
            Price = 0;
        }

        /// <summary>
        /// Calculate price, free parking slot, and mark as deleted.
        /// </summary>
        public virtual void CheckOut()
        {
            // Calculate price.
            EndTime = DateTime.Now;
            TotalHours = (EndTime.Value - StartTime).TotalHours;
            Price += ParkingSlot.PricePrHour * TotalHours;

            // TODO: Free parking slot.

            // Mark ticket as deleted.
            EntityState = EntityStateOption.Deleted;
        }

        public string PrintableString() =>
            $"# ##\n" +
            $"# ##  Ticket: {Id} Slot: {ParkingSlot.Id}\n" +
            $"# ##\n" +
            $"# ##  Start Time: {StartTime} End Time: {(EndTime.HasValue ? EndTime.Value.ToString() : "N/A")}\n" +
            $"# ##  Total hours: {TotalHours}\n" +
            $"# ##  Charging: {(Electric? "Y" : "N")}\n" +
            $"# ##\n" +
            $"# ##  Price: {Price} kr.\n" +
            $"# ##\n\n";

        public override bool Validate()
        {
            bool isValid = true;

            if (StartTime == null) isValid = false;
            if (ParkingSlot == null) isValid = false;
            if (!(Electric && ParkingSlot.HasCharger)) isValid = false;
            if (Price < 0) isValid = false;

            return isValid;
        }
    }
}
