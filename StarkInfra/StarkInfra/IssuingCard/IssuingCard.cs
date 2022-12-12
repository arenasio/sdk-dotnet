﻿using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingCard object
    /// <br/>
    /// The IssuingCard object displays the information of the cards created in your Workspace.
    /// Sensitive information will only be returned when the "expand" parameter is used, to avoid security concerns.
    /// <br/>
    /// When you initialize a IssuingCard, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>HolderName [string]: card holder name. ex: "Tony Stark"</item>
    ///     <item>HolderTaxID [string]: card holder tax ID. ex: "012.345.678-90"</item>
    ///     <item>HolderExternalID [string] card holder unique id, generated by the user to avoid duplicated holders. ex: "my-entity/123"</item>
    ///     <item>DisplayName [string, default null]: card displayed name. ex: "ANTHONY STARK"</item>
    ///     <item>Rules [list of IssuingRule objects, default null]: [EXPANDABLE] list of card spending rules. ex: new List<IssuingRule>{ new IssuingRule() }</item>
    ///     <item>ProductID [string, default null]: card product ID to which the card is bound. ex: "53810200"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>StreetLine1 [string, default sub-issuer street line 1]: card holder main address. ex: "Av. Paulista, 200"</item>
    ///     <item>StreetLine2 [string, default sub-issuer street line 2]: card holder address complement. ex: "Apto. 123"</item>
    ///     <item>District [string, default sub-issuer district]: card holder address district / neighbourhood. ex: "Bela Vista"</item>
    ///     <item>City [string, default sub-issuer city]: card holder address city. ex: "Rio de Janeiro"</item>
    ///     <item>StateCode [string, default sub-issuer state code]: card holder address state. ex: "GO"</item>
    ///     <item>ZipCode [string, default sub-issuer zip code]: card holder address zip code. ex: "01311-200"</item>
    ///     <item>ID[string]: unique id returned when IssuingCard is created. ex: "5656565656565656"</item>
    ///     <item>HolderID [string]: card holder unique id. ex: "5656565656565656"</item>
    ///     <item>Type [string]: card type. ex: "virtual"</item>
    ///     <item>Status [string]: current IssuingCard status. ex: "active", "blocked", "canceled" or "expired"</item>
    ///     <item>Number [string]: [EXPANDABLE] masked card number. Expand to unmask the value. ex: "123".</item>
    ///     <item>SecurityCode [string]: [EXPANDABLE] masked card verification value (cvv). Expand to unmask the value. ex: "123".</item>
    ///     <item>Expiration [DateTime]: [EXPANDABLE] masked card expiration DateTime. Expand to unmask the value. ex: DateTime(2020, 3, 10, 10, 30, 0, 0).</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the IssuingCard. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the IssuingCard. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingCard : Resource
    {
        public string HolderName { get; }
        public string HolderTaxID { get; }
        public string HolderExternalID { get; }
        public string DisplayName { get; }
        public List<IssuingRule> Rules { get; }
        public string BinID { get; }
        public List<string> Tags { get; }
        public string StreetLine1 { get; }
        public string StreetLine2 { get; }
        public string District { get; }
        public string City { get; }
        public string StateCode { get; }
        public string ZipCode { get; }
        public string HolderID {  get; }
        public string Type { get; }
        public string Status { get; }
        public string Number { get; }
        public string SecurityCode { get; }
        public DateTime? Expiration { get; }
        public DateTime? Updated { get;  }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingCard object
        /// <br/>
        /// The IssuingCard object displays the information of the cards created in your Workspace.
        /// Sensitive information will only be returned when the "expand" parameter is used, to avoid security concerns.
        /// <br/>
        /// When you initialize a IssuingCard, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holderName [string]: card holder name. ex: "Tony Stark"</item>
        ///     <item>holderTaxID [string]: card holder tax ID. ex: "012.345.678-90"</item>
        ///     <item>holderExternalID [string] card holder unique id, generated by the user to avoid duplicated holders. ex: "my-entity/123"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>displayName [string, default null]: card displayed name. ex: "ANTHONY STARK"</item>
        ///     <item>rules [list of IssuingRule objects, default null]: [EXPANDABLE] list of card spending rules. ex: new List<IssuingRule>{ new IssuingRule() }</item>
        ///     <item>productID [string, default null]: card product ID to which the card is bound. ex: "53810200"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>streetLine1 [string, default sub-issuer street line 1]: card holder main address. ex: "Av. Paulista, 200"</item>
        ///     <item>streetLine2 [string, default sub-issuer street line 2]: card holder address complement. ex: "Apto. 123"</item>
        ///     <item>district [string, default sub-issuer district]: card holder address district / neighbourhood. ex: "Bela Vista"</item>
        ///     <item>city [string, default sub-issuer city]: card holder address city. ex: "Rio de Janeiro"</item>
        ///     <item>stateCode [string, default sub-issuer state code]: card holder address state. ex: "GO"</item>
        ///     <item>zipCode [string, default sub-issuer zip code]: card holder address zip code. ex: "01311-200"</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingCard is created. ex: "5656565656565656"</item>
        ///     <item>holderID [string]: card holder unique id. ex: "5656565656565656"</item>
        ///     <item>type [string]: card type. ex: "virtual"</item>
        ///     <item>status [string]: current IssuingCard status. ex: “active”, “blocked”, “canceled” or “expired"</item>
        ///     <item>number [string]: [EXPANDABLE] masked card number. Expand to unmask the value. ex: "123".</item>
        ///     <item>securityCode [string]: [EXPANDABLE] masked card verification value (cvv). Expand to unmask the value. ex: "123".</item>
        ///     <item>expiration [DateTime]: [EXPANDABLE] masked card expiration DateTime. Expand to unmask the value. ex: DateTime(2020, 3, 10, 10, 30, 0, 0).</item>
        ///     <item>updated [DateTime]: latest update DateTime for the IssuingCard. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the IssuingCard. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingCard(string holderName, string holderTaxID, string holderExternalID, string displayName = null, List<IssuingRule> rules = null, 
            string productID = null, List<string> tags = null, string streetLine1 = null, string streetLine2 = null, string district = null, string city = null, 
            string stateCode = null, string zipCode = null, string id = null, string holderID = null, string type = null, string status = null, 
            string number = null, string securityCode = null, DateTime? expiration = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            HolderID = holderID;
            HolderName = holderName;
            HolderTaxID = holderTaxID;
            HolderExternalID = holderExternalID;
            Type = type;
            DisplayName = displayName;
            Status = status;
            Rules = rules;
            BinID = productID;
            StreetLine1 = streetLine1;
            StreetLine2 = streetLine2;
            District = district;
            City = city;
            StateCode = stateCode;
            ZipCode = zipCode;
            Tags = tags;
            Number = number;
            SecurityCode = securityCode;
            Expiration = expiration;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create IssuingCard objects
        /// <br/>
        /// Send a list of IssuingCard objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>cards [list of IssuingCard objects]: list of IssuingCard objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules", "securityCode", "number", "expiration" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingCard objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingCard> Create(List<IssuingCard> cards, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: cards,
                query: parameters,
                user: user
            ).ToList().ConvertAll(o => (IssuingCard)o);
        }

        /// <summary>
        /// Create IssuingCard objects
        /// <br/>
        /// Send a list of IssuingCard dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>cards [list of Dictionaries]: list of dictionaries representing the IssuingCard objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules", "securityCode", "number", "expiration" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingCard objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingCard> Create(List<Dictionary<string, object>> cards, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: cards,
                query: parameters,
                user: user
            ).ToList().ConvertAll(o => (IssuingCard)o);
        }

        /// <summary>
        /// Retrieve a specific IssuingCard by its id
        /// <br/>
        /// Receive a single IssuingCard object previously created in the Stark Infra API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules", "securityCode", "number", "expiration" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IssuingCard object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IssuingCard Get(string id, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                query: parameters,
                user: user
            ) as IssuingCard;
        }

        /// <summary>
        /// Retrieve IssuingCard objects
        /// <br/>
        /// Receive an IEnumerable of IssuingCard objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "expired" or "canceled"</item>
        ///     <item>types [list of strings, default null]: card type. ex: new List<string>{ "virtual" }</item>
        ///     <item>holderIds [list of strings, default null]: card holder IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules", "securityCode", "number", "expiration" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingCard objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingCard> Query(int? limit = null, List<string> ids = null,  
            DateTime? after = null, DateTime? before = null, List<string> status = null, 
            List<string> types = null, List<string> holderIds = null, List<string> tags = null,
            List<string> expand = null, User user = null
        )
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "ids", ids },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "types", types },
                    { "holderIds", holderIds },
                    { "tags", tags },
                    { "expand", expand }
                },
                user: user
            ).Cast<IssuingCard>();
        }

        /// <summary>
        /// Retrieve paged IssuingCard objects
        /// <br/>
        /// Receive a list of up to 100 IssuingCard objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default 100]: filter for status of retrieved objects. ex: "active", "blocked", "expired" or "canceled"</item>
        ///     <item>types [list of strings, default null]: card type. ex: new List<string>{ "virtual" }</item>
        ///     <item>holderIds [list of strings, default null]: card holder IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules", "securityCode", "number", "expiration" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingCard objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingCard objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingCard> page, string pageCursor) Page(string cursor = null, 
            int? limit = null, List<string> ids = null, DateTime? after = null, 
            DateTime? before = null, List<string> status = null,List<string> types = null, 
            List<string> holderIds = null,List<string> tags = null, List<string> expand = null, 
            User user = null
        ) {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "ids", ids },
                    { "after", after },
                    { "before", before },
                    { "types", types },
                    { "holderIds", holderIds },
                    { "tags", tags },
                    { "expand", expand }
                },
                user: user
            );
            List<IssuingCard> cards = new List<IssuingCard>();
            foreach (SubResource subResource in page)
            {
                cards.Add(subResource as IssuingCard);
            }
            return (cards, pageCursor);
        }

        /// <summary>
        /// Update IssuingCard entity
        /// <br/>
        /// Update an IssuingCard by passing its id.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: IssuingCard id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string, default null]: You may block the IssuingCard by passing "blocked" or activate by passing "active" in the status</item>
        ///     <item>displayName [string, default null]: card displayed name. ex: "ANTHONY EDWARD"</item>
        ///     <item>rules [list of IssuingRule, default null]: list of IssuingRules with "amount": int, "currencyCode": string, "id": string, "interval": string, "name": string pairs.</item>
        ///     <item>tags [list of strings]: list of strings for tagging</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target IssuingCard with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingCard Update(string id, Dictionary<string, object> patchData, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as IssuingCard;
        }

        /// <summary>
        /// Cancel an IssuingCard entity
        /// <br/>
        /// Cancel an IssuingCard entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: IssuingCard unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled IssuingCard object</item>
        /// </list>
        /// </summary>
        public static IssuingCard Cancel(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingCard;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingCard", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string holderName = json.holderName;
            string holderTaxID = json.holderTaxId;
            string holderExternalID = json.holderExternalId;
            string displayName = json.displayName;
            List<IssuingRule> rules = IssuingRule.ParseRules(json.rules);
            string productID = json.binId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string streetLine1 = json.streetLine1;
            string streetLine2 = json.streetLine2;
            string district = json.district;
            string city = json.city;
            string stateCode = json.stateCode;
            string zipCode = json.zipCode;
            string id = json.id;
            string holderID = json.holderId;
            string type = json.type;
            string status = json.status;
            string number = json.number;
            string securityCode = json.securityCode;
            string expirationString = json.expiration;
            DateTime? expiration = null;
            if (expirationString[0] != '*') expiration = Checks.CheckDateTime(expirationString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);

            return new IssuingCard(
                holderName: holderName, holderTaxID: holderTaxID, holderExternalID: holderExternalID, 
                displayName: displayName, rules: rules, productID: productID, tags: tags, 
                streetLine1: streetLine1, streetLine2: streetLine2, district: district, 
                city: city, stateCode: stateCode, zipCode: zipCode, id: id, holderID: holderID, 
                type: type, status: status, number: number, securityCode: securityCode, 
                expiration: expiration, updated: updated, created: created
            );
        }
    }
}
