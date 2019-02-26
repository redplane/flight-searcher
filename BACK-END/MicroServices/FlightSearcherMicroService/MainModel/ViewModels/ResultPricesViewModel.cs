using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MainModel.ViewModels
{
    
    public partial class ResultPricesViewModel
    {
        public Guid SessionKey { get; set; }
        public Query Query { get; set; }
        public Status Status { get; set; }
        public List<Itinerary> Itineraries { get; set; }
        public List<Leg> Legs { get; set; }
        public List<Segment> Segments { get; set; }
        public List<Carrier> Carriers { get; set; }
        public List<Agent> Agents { get; set; }
        public List<Place> Places { get; set; }
        public List<Currency> Currencies { get; set; }
    }

    public partial class Agent
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Uri ImageUrl { get; set; }
        public Status Status { get; set; }
        public bool OptimisedForMobile { get; set; }
        public AgentType Type { get; set; }
    }

    public partial class Carrier
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Uri ImageUrl { get; set; }
        public string DisplayCode { get; set; }
    }

    public partial class Currency
    {
        public string Code { get; set; }
        public string Symbol { get; set; }
        public string ThousandsSeparator { get; set; }
        public string DecimalSeparator { get; set; }
        public bool SymbolOnLeft { get; set; }
        public bool SpaceBetweenAmountAndSymbol { get; set; }
        public long RoundingCoefficient { get; set; }
        public long DecimalDigits { get; set; }
    }

    public partial class Itinerary
    {
        public string OutboundLegId { get; set; }
        public List<PricingOption> PricingOptions { get; set; }
        public BookingDetailsLink BookingDetailsLink { get; set; }
    }

    public partial class BookingDetailsLink
    {
        public string Uri { get; set; }
        public string Body { get; set; }
        public Method Method { get; set; }
    }

    public partial class PricingOption
    {
        public List<long> Agents { get; set; }
        public long QuoteAgeInMinutes { get; set; }
        public double Price { get; set; }
        public Uri DeeplinkUrl { get; set; }
    }

    public partial class Leg
    {
        public string Id { get; set; }
        public List<long> SegmentIds { get; set; }
        public long OriginStation { get; set; }
        public long DestinationStation { get; set; }
        public DateTimeOffset Departure { get; set; }
        public DateTimeOffset Arrival { get; set; }
        public long Duration { get; set; }
        public JourneyMode JourneyMode { get; set; }
        public List<long> Stops { get; set; }
        public List<long> Carriers { get; set; }
        public List<long> OperatingCarriers { get; set; }
        public Directionality Directionality { get; set; }
        public List<FlightNumber> FlightNumbers { get; set; }
    }

    public partial class FlightNumber
    {
        public long FlightNumberFlightNumber { get; set; }
        public long CarrierId { get; set; }
    }

    public partial class Place
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Code { get; set; }
        public PlaceType Type { get; set; }
        public string Name { get; set; }
    }

    public partial class Query
    {
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Locale { get; set; }
        public long Adults { get; set; }
        public long Children { get; set; }
        public long Infants { get; set; }
        public long OriginPlace { get; set; }
        public long DestinationPlace { get; set; }
        public DateTimeOffset OutboundDate { get; set; }
        public string LocationSchema { get; set; }
        public string CabinClass { get; set; }
        public bool GroupPricing { get; set; }
    }

    public partial class Segment
    {
        public long Id { get; set; }
        public long OriginStation { get; set; }
        public long DestinationStation { get; set; }
        public DateTimeOffset DepartureDateTime { get; set; }
        public DateTimeOffset ArrivalDateTime { get; set; }
        public long Carrier { get; set; }
        public long OperatingCarrier { get; set; }
        public long Duration { get; set; }
        public long FlightNumber { get; set; }
        public JourneyMode JourneyMode { get; set; }
        public Directionality Directionality { get; set; }
    }

    public enum Status { UpdatesComplete, UpdatesPending };

    public enum AgentType { Airline, TravelAgent };

    public enum Method { Put };

    public enum Directionality { Inbound, Outbound };

    public enum JourneyMode { Flight };

    public enum PlaceType { Airport, City, Country };

}
