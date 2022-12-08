﻿using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    public partial class IssuingCard
    {
        /// <summary>
        /// IssuingCard.Log object
        /// <br/>
        /// Every time an IssuingCard entity is updated, a corresponding issuingcard.Log
        /// is generated for the entity. This log is never generated by the
        /// user, but it can be retrieved to check additional information
        /// on the IssuingCard.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>ID [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
        ///     <item>Card [IssuingCard]: IssuingCard entity to which the log refers to.</item>
        ///     <item>Type [string]: type of the IssuingCard event which triggered the log creation. ex: "blocked", "canceled", "created", "expired", "unblocked", "updated"</item>
        ///     <item>Created [DateTime]: creation DateTime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public class Log : Resource
        {
            public IssuingCard Card { get; }
            public string Type { get; }
            public DateTime Created { get; }

            /// <summary>
            /// IssuingCard.Log object
            /// <br/>
            /// Every time an IssuingCard entity is updated, a corresponding issuingcard.Log
            /// is generated for the entity. This log is never generated by the
            /// user, but it can be retrieved to check additional information
            /// on the IssuingCard.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>id [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
            ///     <item>card [IssuingCard]: IssuingCard entity to which the log refers to.</item>
            ///     <item>type [string]: type of the IssuingCard event which triggered the log creation. ex: "blocked", "canceled", "created", "expired", "unblocked", "updated"</item>
            ///     <item>created [DateTime]: creation DateTime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Log(string id, DateTime created, string type, IssuingCard card) : base(id)
            {
                Card = card;
                Type = type;
                Created = created;
            }

            /// <summary>
            /// Retrieve a specific IssuingCard.Log by its id
            /// <br/>
            /// Receive a single IssuingCard.Log object previously created by the Stark Infra API by its id
            /// <br/>
            /// Parameters (required):
            /// <list>
            ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
            /// </list>
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>IssuingCard.Log object that corresponds to the given id.</item>
            /// </list>
            /// </summary>
            public static Log Get(string id, User user = null)
            {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetId(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    id: id,
                    user: user
                ) as Log;
            }

            /// <summary>
            /// Retrieve IssuingCard.Log objects
            /// <br/>
            /// Receive an IEnumerable of IssuingCard.Log objects previously created in the Stark Infra API
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</list>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter for log event types. ex: new List<string>{  "blocked", "canceled", "created", "expired", "unblocked", "updated" }</item>
            ///     <item>cardIds [list of strings, default null]: list of IssuingCard ids to filter logs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>IEnumerable of IssuingCard.Log objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Log> Query(List<string> ids = null, int? limit = null, DateTime? after = null, DateTime? before = null,
                List<string> types = null, List<string> cardIds = null, User user = null)
            {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "ids", ids },
                        { "limit", limit },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "types", types },
                        { "cardIds", cardIds }
                    },
                    user: user
                ).Cast<Log>();
            }

            /// <summary>
            /// Retrieve paged IssuingCard.Log objects
            /// <br/>
            /// Receive a list of up to 100 Log objects previously created in the Stark Infra API and the cursor to the next page.
            /// Use this function instead of query if you want to manually page your cards.
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
            ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</list>
            ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter for log event types. ex: new List<string>{ "blocked", "canceled", "created", "expired", "unblocked", "updated" }</item>
            ///     <item>cardIds [list of strings, default null]: list of IssuingCard ids to filter logs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of Log objects with updated attributes</item>
            ///     <item>cursor to retrieve the next page of IssuingCard.Log objects</item>
            /// </list>
            /// </summary>
            public static (List<Log> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
                DateTime? before = null, List<string> ids = null, List<string> types = null, List<string> cardIds = null,
                User user = null)
            {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                (List<SubResource> page, string pageCursor) = Rest.GetPage(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "cursor", cursor },
                        { "ids", ids },
                        { "limit", limit },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "types", types },
                        { "cardIds", cardIds }
                    },
                    user: user
                );
                List<Log> logs = new List<Log>();
                foreach (SubResource subResource in page)
                {
                    logs.Add(subResource as Log);
                }
                return (logs, pageCursor);
            }

            internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "IssuingCardLog", resourceMaker: ResourceMaker);
            }

            internal static Utils.Resource ResourceMaker(dynamic json)
            {
                string id = json.id;
                IssuingCard card = IssuingCard.ResourceMaker(json.card);
                string type = json.type;
                string createdString = json.created;
                DateTime created = Checks.CheckDateTime(createdString);

                return new Log(id: id, card: card, type: type, created: created);
            }
        }
    }
}
