using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;
using System.Diagnostics;


namespace StarkInfra
{
    public partial class IssuingStock
    {
        /// <summary>
        /// IssuingStock.Log object
        /// <br/>
        /// Every time an IssuingStock entity is updated, a corresponding IssuingStock.Log
        /// is generated for the entity. This log is never generated by the
        /// user, but it can be retrieved to check additional information
        /// on the IssuingStock.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>Id[string]: unique id returned when the log is created. ex: "5656565656565656"</item>
        ///     <item>Stock [IssuingStock]: IssuingStock entity to which the log refers to.</item>
        ///     <item>Type [string]: type of the IssuingStock event which triggered the log creation. ex: "created", "spent", "restocked", "lost"</item>
        ///     <item>Count [Integer]: shift in stock balance. ex: 10</item>
        ///     <item>Created [DateTime]: creation datetime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public class Log : Resource
        {
            public IssuingStock Stock { get; }
            public string Type { get; }
            public int Count { get; }
            public DateTime Created { get; }

            /// <summary>
            /// IssuingStock.Log object
            /// <br/>
            /// Every time an IssuingStock entity is modified, a corresponding IssuingStock.Log
            /// is generated for the entity. This log is never generated by the
            /// user.
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>id [string]: unique id returned when the log is created. ex: "5656565656565656"</item>
            ///     <item>stock [IssuingStock object]: IssuingStock entity to which the log refers to.</item>
            ///     <item>type [string]: type of the IssuingStock event which triggered the log creation. ex: "created", "spent", "restocked", "lost"</item>
            ///     <item>count [integer]: shift in stock balance. ex: 10</item>
            ///     <item>created [DateTime]: creation datetime for the log. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Log(string id, int count, DateTime created, string type, IssuingStock stock) : base(id)
            {
                Created = created;
                Type = type;
                Count = count;
                Stock = stock;
            }

            /// <summary>
            /// Retrieve a specific IssuingStock.Log
            /// <br/>
            /// Receive a single IssuingStock.Log object previously created by the Stark Infra API by passing its id
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
            ///     <item>IssuingStock.Log object with updated attributes</item>
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
            /// Retrieve IssuingStock.Log objects
            /// <br/>
            /// Receive an IEnumerable of IssuingStock.Log objects previously created in the Stark Infra API
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: new List<string>{ "created", "spent", "restocked", "lost" }</item>
            ///     <item>stockIds [list of strings, default null]: list of IssuingStock ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>IEnumerable of IssuingStock.Log objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Log> Query(int? limit = null, List<string> ids = null, DateTime? after = null, DateTime? before = null,
                List<string> types = null, List<string> stockIds = null, User user = null)
            {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                return Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "limit", limit },
                        { "ids", ids },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "types", types },
                        { "stockIds", stockIds }
                    },
                    user: user
                ).Cast<Log>();
            }

            /// <summary>
            /// Retrieve paged IssuingStock.Log objects
            /// <br/>
            /// Receive a list of up to 100 IssuingStock.Log objects previously created in the Stark Infra API and the cursor to the next page.
            /// Use this function instead of query if you want to manually page your purchases.
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
            ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
            ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>types [list of strings, default null]: filter retrieved objects by types. ex: new List<string>{ "created", "spent", "restocked", "lost" }</item>
            ///     <item>stockIds [list of strings, default null]: list of IssuingStock ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of IssuingStock.Log objects with updated attributes</item>
            ///     <item>cursor to retrieve the next page of IssuingStock.Log objects</item>
            /// </list>
            /// </summary>
            public static (List<Log> page, string pageCursor) Page(string cursor = null, List<string> ids = null, int? limit = null, DateTime? after = null,
                DateTime? before = null, List<string> types = null, List<string> stockIds = null, User user = null)
            {
                (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
                (List<SubResource> page, string pageCursor) = Rest.GetPage(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "cursor", cursor },
                        { "limit", limit },
                        { "ids", ids },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "types", types },
                        { "stockIds", stockIds }
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
                return (resourceName: "IssuingStockLog", resourceMaker: ResourceMaker);
            }

            internal static Resource ResourceMaker(dynamic json)
            {
                string id = json.id;
                string createdString = json.created;
                DateTime created = Checks.CheckDateTime(createdString);
                string type = json.type;
                int count = json.count;
                IssuingStock stock = IssuingStock.ResourceMaker(json.stock);

                return new Log(id: id, created: created, type: type, count: count, stock: stock);
            }
        }
    }
}