﻿using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;
using System.Diagnostics;


namespace StarkInfra
{
    public partial class IssuingInvoice
    {
        /// <summary>
        /// IssuingInvoice.Log object
        /// <br/>
        /// Every time an IssuingInvoice entity is updated, a corresponding IssuingInvoice.Log is 
        /// generated for the entity. This log is never generated by the user, but it can be retrieved 
        /// to check additional information on the IssuingInvoice.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>ID[string]: unique id returned when the log is created. ex: "5656565656565656"</item>
        ///     <item>Invoice [IssuingInvoice]: IssuingInvoice entity to which the log refers to.</item>
        ///     <item>Type [string]: type of the IssuingInvoice event which triggered the log creation. ex: "created", "credited", "expired", "overdue", "paid"</item>
        ///     <item>Created [DateTime]: creation DateTime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public class Log : Resource
        {
            public IssuingInvoice Invoice { get; }
            public string Type { get; }
            public DateTime Created { get; }

            /// <summary>
            /// IssuingInvoice.Log object
            /// <br/>
            /// Every time an IssuingInvoice entity is updated, a corresponding IssuingInvoice.Log is 
            /// generated for the entity. This log is never generated by the user, but it can be retrieved 
            /// to check additional information on the IssuingInvoice.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>id [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
            ///     <item>invoice [IssuingInvoice]: IssuingInvoice entity to which the log refers to.</item>
            ///     <item>type [string]: type of the IssuingInvoice event which triggered the log creation. ex: "created", "credited", "expired", "overdue", "paid"</item>
            ///     <item>created [DateTime]: creation DateTime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Log(string id, IssuingInvoice invoice, string type, DateTime created) : base(id)
            {
                Invoice = invoice;
                Type = type;
                Created = created;
            }

            /// <summary>
            /// Retrieve a specific IssuingInvoice.Log by its id
            /// <br/>
            /// Receive a single IssuingInvoice.Log object previously created by the Stark Infra API by passing its id
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
            ///     <item>IssuingInvoice.Log object that corresponds to the given id.</item>
            /// </list>
            /// </summary>
            public static Log Get(string id, User user = null)
            {
                (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetId(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    id: id,
                    user: user
                ) as Log;
            }

            /// <summary>
            /// Retrieve IssuingInvoice.Log objects
            /// <br/>
            /// Receive an IEnumerable of IssuingInvoice.Log objects previously created in the Stark Infra API
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter for log event types. ex: new List<string>{  "created", "credited", "expired", "overdue", "paid" }</item>
            ///     <item>ids [list of strings, default null]: list of IssuingInvoice.Log objects ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>IEnumerable of IssuingInvoice.Log objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Log> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
                List<string> types = null, List<string> ids = null, User user = null)
            {
                (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "limit", limit },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "types", types },
                        { "ids", ids }
                    },
                    user: user
                ).Cast<Log>();
            }

            /// <summary>
            /// Retrieve paged IssuingInvoice.Log objects
            /// <br/>
            /// Receive a list of up to 100 IssuingInvoice.Log objects previously created in the Stark Infra API and the cursor to the next page.
            /// Use this function instead of query if you want to manually page your requests.
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
            ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: new List<string>{ "created", "credited", "expired", "overdue", "paid" }</item>
            ///     <item>ids [list of strings, default null]: list of IssuingInvoice.Log objects ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of IssuingInvoice.Log objects with updated attributes</item>
            ///     <item>cursor to retrieve the next page of IssuingInvoice.Log objects</item>
            /// </list>
            /// </summary>
            public static (List<Log> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
                DateTime? before = null, List<string> types = null, List<string> ids = null, User user = null)
            {
                (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
                (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "cursor", cursor },
                        { "limit", limit },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "types", types },
                        { "ids", ids }
                    },
                    user: user
                );
                List<Log> logs = new List<Log>();
                foreach (StarkCore.Utils.SubResource subResource in page)
                {
                    logs.Add(subResource as Log);
                }
                return (logs, pageCursor);
            }

            internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "IssuingInvoiceLog", resourceMaker: ResourceMaker);
            }

            internal static Resource ResourceMaker(dynamic json)
            {
                string id = json.id;
                IssuingInvoice invoice = IssuingInvoice.ResourceMaker(json.invoice);
                string type = json.type;
                string createdString = json.created;
                DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);

                return new Log(id: id, invoice: invoice,  type: type, created: created);
            }
        }
    }
}
