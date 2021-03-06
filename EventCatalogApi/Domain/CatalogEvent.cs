﻿using EventLite.Domain.EventLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventLite.Domain.EventLite
{
    public class CatalogEvent
    {
        // Primary key
        // Assigned by EntityFramework
        public int Id { get; set; }

        // Title of the event.
        // Ex. "14th Annual Soup Festival"
        public string Title { get; set; }

        // Plain text description of event
        // "Soup Festival is all about soup! The ultimate liquid meal..."
        public string Description { get; set; }

        // Start date and time of event (in time zone of venue's address)
        public DateTime Start { get; set; }

        // End date and time of event (in time zone of venue's address)
        public DateTime End { get; set; }
        // default is zero duration, that is, Start == End

        // Picture representing the event in .png file format
        // Deployed in container with service (in wwwroot\Pics)
        public string PictureUrl { get; set; }
        // QUESTION:
        // Is there a good alternative to hard-coding the
        // PicController.GetImage() API route into data in the db?
        // In JewelsOnContainer, the PictureUrls in the db look like this:
        // http://externalcatalogbaseurltobereplaced/api/pic/1
        // Is this approach considered typical/usual? What is industry standard?
        // Idea: Omit the "/api/pic" part from these strings in the db
        // and instead have CatalogController.ChangePictureUrl() add it back in?
        // Alongside that idea, seems also that it would be ideal for the  
        // PicController.GetImage() API route to be detected/queried rather than
        // hard-coded anywhere (even in ChangePictureUrl)
        // Is there a way to do that?
        // Probably second-best from that would be to have CatalogController
        // ask PicController what the GetImage() route is. Still the potential
        // for this to fall out of date, but more likely to be noticed and updated.
        // Surely, the route would not change after the API is shipped,
        // but during development, it seems like having this not
        // hard-coded would be beneficial.

        // Street address of the event's location
        public string VenueName { get; set; }
        public string VenueAddressLine1 { get; set; }
        public string VenueAddressLine2 { get; set; }
        public string VenueAddressLine3 { get; set; }
        public string VenueCity { get; set; }
        public string VenueStateProvince { get; set; }
        public string VenuePostalCode { get; set; }

        public string VenueMapUrl { get; set; }



        //public CatalogVenue CatalogVenue { get; set; }
        // TODO: Figure out about init list syntax (see CatalogSeed)
        //public CatalogVenue Venue2;
        //public CatalogVenue Venue3 { get { return new CatalogVenue(); } set { } }

        // "Fancy Food Company"
        public string HostOrganizer { get; set; }



        // Foreign keys
        public int CatalogFormatId { get; set; }
        public int CatalogTopicId { get; set; }

        // Navigational properties:
        public virtual CatalogFormat CatalogFormat { get; set; }
        public virtual CatalogTopic CatalogTopic { get; set; }



        // Total number of ticket sales allowed for this event
        // across all types of tickets offered.
        public int TotalTicketLimitAllTypes { get; set; }


        // SEE ALSO:
        // CatalogTicketType
        // Price, ticket limit and sales end date for each type of ticket offered
        // for this event.

        // Each CatalogEvent has one or more CatalogTicketType,
        // essentially, a collection of CatalogTicketType
        // Each CatalogTicketType is specified custom for each Event
        // That is, each CatalogEvent has many CatalogTicketType
        // Each CatalogTicketType has one CatalogEvent
        // (TicketType that happen to be identical between different CatalogEvent
        // are not associted in any way)

    }
}
