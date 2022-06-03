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
        /// Every time an IssuingInvoice entity is modified, a corresponding IssuingInvoice.Log
        /// is generated for the entity. This log is never generated by the
        /// user.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>ID[string]: unique id returned when the log is created. ex: "5656565656565656"</item>
        ///     <item>Invoice [IssuingInvoice]: IssuingInvoice entity to which the log refers to.</item>
        ///     <item>Type [string]: type of the IssuingInvoice event which triggered the log creation. ex: "created", "credited", "expired", "overdue", "paid"</item>
        ///     <item>Created [DateTime]: creation datetime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public class Log : Resource
        {
            public DateTime Created { get; }
            public string Type { get; }
            public IssuingInvoice Invoice { get; }

            /// <summary>
            /// IssuingInvoice.Log object
            /// <br/>
            /// Every time an IssuingInvoice entity is modified, a corresponding IssuingInvoice.Log
            /// is generated for the entity. This log is never generated by the
            /// user.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>id [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
            ///     <item>invoice [IssuingInvoice]: IssuingInvoice entity to which the log refers to.</item>
            ///     <item>type [string]: type of the IssuingInvoice event which triggered the log creation. ex: "created", "credited", "expired", "overdue", "paid"</item>
            ///     <item>created [DateTime]: creation datetime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Log(string id, DateTime created, string type, IssuingInvoice invoice) : base(id)
            {
                Created = created;
                Type = type;
                Invoice = invoice;
            }

            /// <summary>
            /// Retrieve a specific Log
            /// <br/>
            /// Receive a single Log object previously created by the Stark Infra API by passing its id
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
            ///     <item>Log object with updated attributes</item>
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
            /// Retrieve Logs
            /// <br/>
            /// Receive an IEnumerable of Log objects previously created in the Stark Infra API
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: new List<string>{  "created", "credited", "expired", "overdue", "paid" }</item>
            ///     <item>ids [list of strings, default null]: list of Logs ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of Log objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Log> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
                List<string> types = null, List<string> ids = null, User user = null)
            {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
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
            /// Retrieve paged Logs
            /// <br/>
            /// Receive a list of up to 100 Log objects previously created in the Stark Infra API and the cursor to the next page.
            /// Use this function instead of query if you want to manually page your invoices.
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
            ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: new List<string>{ "created", "credited", "expired", "overdue", "paid" }</item>
            ///     <item>ids [list of strings, default null]: list of Logs ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of Log objects with updated attributes and cursor to retrieve the next page of Log objects</item>
            /// </list>
            /// </summary>
            public static (List<Log> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
                DateTime? before = null, List<string> types = null, List<string> ids = null, User user = null)
            {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                (List<SubResource> page, string pageCursor) = Rest.GetPage(
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
                foreach (SubResource subResource in page)
                {
                    logs.Add(subResource as Log);
                }
                return (logs, pageCursor);
            }

            internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "IssuingInvoiceLog", resourceMaker: ResourceMaker);
            }

            internal static Resource ResourceMaker(dynamic json)
            {
                string id = json.id;
                string createdString = json.created;
                DateTime created = Checks.CheckDateTime(createdString);
                string type = json.type;
                IssuingInvoice invoice = IssuingInvoice.ResourceMaker(json.invoice);

                return new Log(id: id, created: created, type: type, invoice: invoice);
            }
        }
    }
}
