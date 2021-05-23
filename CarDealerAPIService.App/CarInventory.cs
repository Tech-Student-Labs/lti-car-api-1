using System;

namespace CarDealerAPIService.App
{
    public class CarInventory
    {
        /*SAMPLE INVENTORY LISTING from chevrolet.com
        ---------------------------------
        |                               |
        |      pretty                   |
        |        picture                |
        |                               |
        |                               |
        |                               |
        ---------------------------------
        NAME IN REAL BIG FONT
        Exterior: Summit White
        Interior: Jet Black, Leather seating surfaces 1...
        
        Earnhardt Chevrolet
        8.1 miles away
        (bigger font)
        Total vehicle price:        $62,490
        */

        // TODO: Not needed but nice to have
        // public string carThumbnail { get; set; }

        public string name { get; set; }

        public string exterior { get; set; }

        public string interior { get; set; }

        // TODO: might not be necessary for our project?
        // public string dealerLocation { get; set; }

        // TODO: try this when we're done
        // public string distanceFromDealer { get; set; }
    }
}