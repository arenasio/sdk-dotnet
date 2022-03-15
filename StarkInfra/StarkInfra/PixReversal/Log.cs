﻿using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    public partial class PixReversal
    {
        /// <summary>
        /// PixReversal.Log object
        /// <br/>
        /// Every time a PixReversal entity is modified, a corresponding PixReversal.Log
        /// is generated for the entity. This log is never generated by the
        /// user.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>Id [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
        ///     <item>PixReversal [PixReversal]: PixReversal entity to which the log refers to.</item>
        ///     <item>Errors [list of strings]: list of errors linked to this BoletoPayment event.</item>
        ///     <item>Type [string]: type of the PixReversal event which triggered the log creation. ex: "processing" or "success"</item>
        ///     <item>Created [DateTime]: creation datetime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public class Log : Resource
        {
            public DateTime Created { get; }
            public string Type { get; }
            public List<Dictionary<string, object>>  Errors { get; }
            public PixReversal Reversal { get; }

            /// <summary>
            /// PixReversal.Log object
            /// <br/>
            /// Every time a PixReversal entity is modified, a corresponding PixReversal.Log
            /// is generated for the entity. This log is never generated by the
            /// user.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>id [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
            ///     <item>reversal [PixReversal]: PixReversal entity to which the log refers to.</item>
            ///     <item>errors [list of strings]: list of errors linked to this BoletoPayment event.</item>
            ///     <item>type [string]: type of the PixReversal event which triggered the log creation. ex: "processing" or "success"</item>
            ///     <item>created [DateTime]: creation datetime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Log(string id, DateTime created, string type, List<Dictionary<string, object>> errors, PixReversal reversal) : base(id)
            {
                Created = created;
                Type = type;
                Errors = errors;
                Reversal = reversal;
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
            ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
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
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: "success" or "failed"</item>
            ///     <item>reversalIds [list of strings, default null]: list of PixReversal ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
            ///     <item>user [Project object, default null]: Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of Log objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Log> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
                List<string> types = null, List<string> reversalIds = null, User user = null)
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
                        { "reversalIds", reversalIds }
                    },
                    user: user
                ).Cast<Log>();
            }

            /// <summary>
            /// Retrieve paged Logs
            /// <br/>
            /// Receive a list of up to 100 Log objects previously created in the Stark Infra API and the cursor to the next page.
            /// Use this function instead of query if you want to manually page your requests.
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: "success" or "failed"</item>
            ///     <item>reversalIds [list of strings, default null]: list of PixReversal ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
            ///     <item>user [Project object, default null]: Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of Log objects with updated attributes and cursor to retrieve the next page of Log objects</item>
            /// </list>
            /// </summary>
            public static (List<Log> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
                DateTime? before = null, List<string> types = null, List<string> reversalIds = null, User user = null)
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
                        { "reversalIds", reversalIds }
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
                return (resourceName: "PixReversalLog", resourceMaker: ResourceMaker);
            }

            internal static Utils.Resource ResourceMaker(dynamic json)
            {
                List<Dictionary<string, object>> errors = json.errors.ToObject<List<Dictionary<string, object>>>();
                string id = json.id;
                string createdString = json.created;
                DateTime created = Checks.CheckDateTime(createdString);
                string type = json.type;
                PixReversal reversal = PixReversal.ResourceMaker(json.reversal);

                return new Log(id: id, created: created, type: type, errors: errors, reversal: reversal);
            }
        }
    }
}
