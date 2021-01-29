﻿using Park_n_Wash.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park_n_Wash
{
    class TicketMachine
    {
        private SlotController _slotController;
        private TicketRepository _ticketRepository;

        public TicketMachine()
        {
            _slotController = new SlotController();
            _ticketRepository = new TicketRepository();
        }

        /// <summary>
        /// Run application UI, wait for user input and act accordingly.
        /// </summary>
        public void RunApplication()
        {
            Console.WriteLine("Welcome to Park'n'Wash\n");

            int userOption = UserInteraction.SelectOption(new List<string> { "Chech In", "Check Out" });

            if (userOption == 0)
            {
                // Check in.
                _slotController.GetAvailableSlots(out List<ISlot> normalSlots, out List<ISlot> handicapSlots, out List<ISlot> largeSlots, out List<ISlot> trailerSlots);

                userOption = UserInteraction.SelectOption(
                    new List<string> {
                        "Regular slots: " + normalSlots.Count + " (" + _slotController.ElectricCountAndSort(normalSlots) + " of which offers electric charging)",
                        "Handicap slots: " + handicapSlots.Count,
                        "Large slots (bus/lorry): " + largeSlots.Count,
                        "Trailer slots: " + trailerSlots.Count
                    },
                    "Available parking slots");
            }
            else if (userOption == 1)
            {
                // Check out.

            }
        }
    }
}
